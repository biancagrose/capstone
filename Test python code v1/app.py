import sys
import os
import subprocess
import tempfile
import shutil
import re
import math

from dataclasses import dataclass
from typing import List

from PyQt5.QtWidgets import (
    QApplication, QMainWindow, QWidget, QVBoxLayout, QHBoxLayout,
    QPushButton, QLabel, QFileDialog, QGraphicsView, QGraphicsScene,
    QSlider, QTabWidget, QTextEdit
)
from PyQt5.QtCore import Qt
from PyQt5.QtGui import QPixmap, QImage, QPen, QColor

from PIL import Image


# =========================================================
# DATA MODEL
# =========================================================

@dataclass
class MinutiaePoint:
    x: int
    y: int
    direction: float
    quality: float
    type: str


# =========================================================
# BACKEND (EXE + PARSING)
# =========================================================

class FingerprintBackend:
    def __init__(self, app_path, temp_dir):
        self.app_path = app_path
        self.temp_dir = temp_dir

    def run_mindtct(self, image_path: str, out_prefix: str):
        exe = os.path.join(self.app_path, "mindtct.exe")
        subprocess.run(
            [exe, image_path, out_prefix],
            creationflags=subprocess.CREATE_NO_WINDOW
        )

    def parse_min_file(self, min_file: str) -> List[MinutiaePoint]:
        pattern = re.compile(
            r"(\d+)\s*,\s*(\d+)\s*:\s*([\d.]+)\s*:\s*([\d.]+)\s*:\s*(\w+)"
        )

        points = []

        if not os.path.exists(min_file):
            return points

        with open(min_file, "r") as f:
            for line in f:
                m = pattern.search(line)
                if m:
                    points.append(MinutiaePoint(
                        x=int(m.group(1)),
                        y=int(m.group(2)),
                        direction=float(m.group(3)),
                        quality=float(m.group(4)),
                        type=m.group(5)
                    ))

        return points


# =========================================================
# CONTROLLER
# =========================================================

class FingerprintController:
    def __init__(self, backend: FingerprintBackend):
        self.backend = backend
        self.image_path = ""
        self.points: List[MinutiaePoint] = []

    def load_image(self, path: str):
        self.image_path = path

    def process(self):
        if not self.image_path:
            return []

        prefix = os.path.splitext(os.path.basename(self.image_path))[0]
        out_prefix = os.path.join(self.backend.temp_dir, prefix)

        self.backend.run_mindtct(self.image_path, out_prefix)

        min_file = out_prefix + ".min"
        self.points = self.backend.parse_min_file(min_file)

        return self.points


# =========================================================
# UI
# =========================================================

class MainWindow(QMainWindow):
    def __init__(self):
        super().__init__()

        self.setWindowTitle("Fingerprint Multi-View System")
        self.resize(1100, 800)

        # setup
        self.app_path = os.path.dirname(os.path.abspath(__file__))
        self.temp_dir = os.path.join(tempfile.gettempdir(), "fp_app")
        os.makedirs(self.temp_dir, exist_ok=True)

        # architecture
        self.backend = FingerprintBackend(self.app_path, self.temp_dir)
        self.controller = FingerprintController(self.backend)

        self.pil_image = None
        self.points = []

        self.init_ui()

    # ---------------- UI ----------------

    def init_ui(self):
        central = QWidget()
        self.setCentralWidget(central)
        layout = QHBoxLayout(central)

        # LEFT CONTROLS
        controls = QVBoxLayout()

        btn_open = QPushButton("Open Image")
        btn_open.clicked.connect(self.open_image)

        btn_run = QPushButton("Run Detection")
        btn_run.clicked.connect(self.run_pipeline)

        controls.addWidget(btn_open)
        controls.addWidget(btn_run)

        controls.addWidget(QLabel("Quality Threshold"))

        self.slider = QSlider(Qt.Horizontal)
        self.slider.setRange(0, 100)
        self.slider.setValue(0)
        self.slider.valueChanged.connect(self.update_views)

        controls.addWidget(self.slider)

        layout.addLayout(controls, 1)
    
        from PyQt5.QtWidgets import QSplitter

        # ---------------- RIGHT SIDE SPLIT VIEWS ----------------
        splitter = QSplitter(Qt.Horizontal)

        # RAW VIEW
        self.raw_scene = QGraphicsScene()
        self.raw_view = QGraphicsView(self.raw_scene)
        splitter.addWidget(self.raw_view)

        # FILTERED VIEW
        self.filtered_scene = QGraphicsScene()
        self.filtered_view = QGraphicsView(self.filtered_scene)
        splitter.addWidget(self.filtered_view)

        # REPORT VIEW
        self.report = QTextEdit()
        self.report.setReadOnly(True)
        splitter.addWidget(self.report)

        # optional sizing balance
        splitter.setSizes([400, 400, 300])

        layout.addWidget(splitter, 4)

        self.raw_label = QLabel("RAW VIEW")
        self.filtered_label = QLabel("FILTERED VIEW")
        self.report_label = QLabel("FORENSIC REPORT")

    # ---------------- FILE ----------------

    def open_image(self):
        path, _ = QFileDialog.getOpenFileName(
            self, "Open Image", "", "Images (*.png *.jpg *.bmp *.tif *.wsq *.raw)"
        )

        if not path:
            return

        self.controller.load_image(path)
        self.pil_image = Image.open(path).convert("L")

        self.display_image_base()

    # ---------------- PIPELINE ----------------

    def run_pipeline(self):
        self.points = self.controller.process()
        self.update_views()

    # ---------------- BASE IMAGE ----------------

    def display_image_base(self):
        if not self.pil_image:
            return

        img = self.pil_image.convert("RGBA")
        data = img.tobytes("raw", "RGBA")

        qimg = QImage(data, img.width, img.height, QImage.Format_RGBA8888)
        pixmap = QPixmap.fromImage(qimg)

        self.raw_scene.clear()
        self.filtered_scene.clear()

        self.raw_scene.addPixmap(pixmap)
        self.filtered_scene.addPixmap(pixmap)

        self.raw_scene.setSceneRect(0, 0, pixmap.width(), pixmap.height())
        self.filtered_scene.setSceneRect(0, 0, pixmap.width(), pixmap.height())

        self.raw_view.fitInView(self.raw_scene.sceneRect(), Qt.KeepAspectRatio)
        self.filtered_view.fitInView(self.filtered_scene.sceneRect(), Qt.KeepAspectRatio)

    # ---------------- UPDATE ALL VIEWS ----------------

    def update_views(self):
        self.draw_raw()
        self.draw_filtered()
        self.draw_report()

    # ---------------- RAW VIEW ----------------

    def draw_raw(self):
        self.raw_scene.clear()
        self.display_image_base()

        pen = QPen(QColor(255, 0, 0), 2)

        for p in self.points:
            self.raw_scene.addEllipse(p.x - 3, p.y - 3, 6, 6, pen)

    # ---------------- FILTERED VIEW ----------------

    def draw_filtered(self):
        self.filtered_scene.clear()
        self.display_image_base()

        threshold = self.slider.value() / 100.0

        for p in self.points:
            if p.quality < threshold:
                continue

            color = QColor(255, 0, 0) if p.type == "RIG" else QColor(0, 255, 0)
            pen = QPen(color, 2)

            self.filtered_scene.addEllipse(p.x - 3, p.y - 3, 6, 6, pen)

    # ---------------- REPORT ----------------

    def draw_report(self):
        self.report.clear()

        threshold = self.slider.value() / 100.0
        filtered = [p for p in self.points if p.quality >= threshold]

        rig = len([p for p in filtered if p.type == "RIG"])
        bif = len([p for p in filtered if p.type == "BIF"])

        avg_q = sum(p.quality for p in filtered) / len(filtered) if filtered else 0

        self.report.append("FORENSIC REPORT")
        self.report.append("----------------")
        self.report.append(f"Total points: {len(self.points)}")
        self.report.append(f"Filtered: {len(filtered)}")
        self.report.append(f"RIG: {rig}")
        self.report.append(f"BIF: {bif}")
        self.report.append(f"Avg quality: {avg_q:.3f}")

    def apply_dark_theme(self):
        self.setStyleSheet("""
            QMainWindow {
                background-color: #1e1e1e;
            }

            QLabel {
                color: #d6d6d6;
                font-size: 12px;
            }

            QPushButton {
                background-color: #2d2d2d;
                color: #ffffff;
                border: 1px solid #444;
                padding: 6px;
                border-radius: 6px;
            }

            QPushButton:hover {
                background-color: #3a3a3a;
            }

            QSlider::groove:horizontal {
                height: 6px;
                background: #333;
                border-radius: 3px;
            }

            QSlider::handle:horizontal {
                background: #00aaff;
                width: 12px;
                border-radius: 6px;
            }

            QTextEdit {
                background-color: #121212;
                color: #e6e6e6;
                border: 1px solid #333;
            }

            QGraphicsView {
                background-color: #0f0f0f;
                border: 1px solid #333;
            }
        """)
        self.apply_dark_theme()

# =========================================================
# MAIN
# =========================================================

if __name__ == "__main__":
    app = QApplication(sys.argv)
    
    window = MainWindow()
    window.show()
    sys.exit(app.exec_())