import sys
import re
import os
import subprocess
import tempfile
import shutil
import time
import math
from PyQt5.QtWidgets import QTextEdit, QSplitter
from PyQt5.QtGui import QBrush
from PyQt5.QtWidgets import (QApplication, QMainWindow, QWidget, QVBoxLayout, 
                             QHBoxLayout, QPushButton, QLabel, QFileDialog, 
                             QMessageBox, QSlider, QLineEdit, QGroupBox, QRadioButton,
                             QGraphicsView, QGraphicsScene, QGraphicsEllipseItem,
                             QGraphicsLineItem, QDialog, QFormLayout, QDialogButtonBox)
from PyQt5.QtCore import Qt, QPointF, QRectF
from PyQt5.QtGui import QPixmap, QImage, QPainter, QPen, QColor
from PIL import Image, ImageEnhance, ImageOps

APP_VERSION = "FpMV (Fingerprint Minutiae Viewer) - Python Port"

class RawDimensionDialog(QDialog):
    """Equivalent to Form2 for requesting RAW image dimensions."""
    def __init__(self, file_path, file_size, parent=None):
        super().__init__(parent)
        self.setWindowTitle("Set RAW Dimension")
        self.file_size = file_size
        self.width_val = 0
        self.height_val = 0

        layout = QFormLayout(self)
        self.width_input = QLineEdit(self)
        self.height_input = QLineEdit(self)
        layout.addRow("Image width:", self.width_input)
        layout.addRow("Image height:", self.height_input)

        buttons = QDialogButtonBox(QDialogButtonBox.Ok | QDialogButtonBox.Cancel, self)
        buttons.accepted.connect(self.validate_and_accept)
        buttons.rejected.connect(self.reject)
        layout.addWidget(buttons)

    def validate_and_accept(self):
        try:
            w = int(self.width_input.text())
            h = int(self.height_input.text())
            if w * h != self.file_size:
                QMessageBox.critical(self, "Invalid Range Error!", 
                                     f"Width multiplied by height must equal the file size!\n{w} x {h} != {self.file_size}")
                return
            self.width_val = w
            self.height_val = h
            self.accept()
        except ValueError:
            QMessageBox.critical(self, "Invalid Input", "Width and height must be integers.")

class InteractiveGraphicsView(QGraphicsView):
    """Handles Zooming and Panning (Mouse Wheel / Drag)."""
    def __init__(self, scene, parent=None):
        super().__init__(scene, parent)
        self.setDragMode(QGraphicsView.ScrollHandDrag)
        self.setTransformationAnchor(QGraphicsView.AnchorUnderMouse)

    def wheelEvent(self, event):
        zoom_in_factor = 1.25
        zoom_out_factor = 1 / zoom_in_factor
        if event.angleDelta().y() > 0:
            zoom_factor = zoom_in_factor
        else:
            zoom_factor = zoom_out_factor
        self.scale(zoom_factor, zoom_factor)

class MinutiaeVisualizerWindow(QDialog):
    """A standalone window to visualize the minutiae data and raw text."""
    def __init__(self, minutiae_data, img_width, img_height, image_name="Unknown Image", parent=None):
        super().__init__(parent)
        self.setWindowTitle("Minutiae Data Explorer")
        self.resize(900, 600)
        self.image_name = image_name 
        
        layout = QVBoxLayout(self)

        # Export Button
        btn_layout = QHBoxLayout()
        btn_export = QPushButton("Export to TXT")
        btn_export.clicked.connect(self.export_data)
        btn_layout.addWidget(btn_export)
        btn_layout.addStretch() 
        layout.addLayout(btn_layout)
        
        splitter = QSplitter(Qt.Horizontal)
        layout.addWidget(splitter)
        
        # Left Side: Raw Data Text
        self.text_box = QTextEdit()
        self.text_box.setReadOnly(True)
        self.text_box.setFontFamily("Courier")
        splitter.addWidget(self.text_box)
        
        # Right Side: 2D Scatter Plot
        self.scene = QGraphicsScene()
        self.view = InteractiveGraphicsView(self.scene)
        self.view.setBackgroundBrush(Qt.white) 
        splitter.addWidget(self.view)
        
        splitter.setSizes([300, 600])
        self.scene.addRect(0, 0, img_width, img_height, QPen(Qt.black, 1, Qt.DashLine))
        self.plot_minutiae(minutiae_data)

    def export_data(self):
        """Saves the contents of the text box to a plain text file."""
        if not self.text_box.toPlainText().strip():
            QMessageBox.warning(self, "No Data", "There is no data to export.")
            return
            
        # Extract the base name (e.g., 'fingerprint1.png' -> 'fingerprint1') and add .txt
        base_name = os.path.splitext(self.image_name)[0]
        default_save_name = f"{base_name}.txt"
            
        file_path, _ = QFileDialog.getSaveFileName(self, "Save Minutiae Data", default_save_name, "Text Files (*.txt);;All Files (*)")
        if file_path:
            try:
                with open(file_path, 'w') as f:
                    f.write(f"Source Image: {self.image_name}\n")
                    f.write("-" * 65 + "\n")
                    f.write("Index -> (X,Y)  Dir: Direction  Qual: Quality  Type: RIG/BIF\n")
                    f.write("-" * 65 + "\n")
                    f.write(self.text_box.toPlainText())
                QMessageBox.information(self, "Success", f"Data exported successfully to:\n{file_path}")
            except Exception as e:
                QMessageBox.critical(self, "Error", f"Failed to save file:\n{str(e)}")

    def plot_minutiae(self, minutiae_data):
        radius = 4
        line_len = 14
        
        for index, m in enumerate(minutiae_data):
            x, y = m["x"], m["y"]
            direction = m["dir"]
            qual = m["rel"]
            typ = m["typ"]
            
            self.text_box.append(f"{index} -> ({x},{y})  Dir: {direction}  Qual: {qual}  Type: {typ}")

            color = QColor(255, 0, 0) if typ == "RIG" else QColor(0, 255, 0)
            pen = QPen(color, 2)
            brush = QBrush(color) if typ == "RIG" else QBrush(Qt.NoBrush)
            
            if typ == "BIF":
                item = self.scene.addRect(x - radius, y - radius, radius * 2, radius * 2, pen)
            else:
                item = self.scene.addEllipse(x - radius, y - radius, radius * 2, radius * 2, pen, brush)
            
            angle = direction * 11.25 - 90
            rad_angle = math.radians(angle)
            x2 = x + (math.cos(rad_angle) * line_len)
            y2 = y + (math.sin(rad_angle) * line_len)
            tail = self.scene.addLine(x, y, x2, y2, pen)
            
            tooltip_text = f"Type: {typ}\nX: {x}, Y: {y}\nDirection: {direction}\nQuality: {qual}"
            item.setToolTip(tooltip_text)
            tail.setToolTip(tooltip_text)
            
        self.view.fitInView(self.scene.itemsBoundingRect(), Qt.KeepAspectRatio)


class MainWindow(QMainWindow):
    def __init__(self):
        super().__init__()
        self.setWindowTitle(APP_VERSION)
        self.resize(1000, 800)
        self.setAcceptDrops(True)
        
        self.app_path = os.path.dirname(os.path.abspath(__file__))
        self.temp_dir = os.path.join(tempfile.gettempdir(), "FpMV", str(int(time.time())))
        os.makedirs(self.temp_dir, exist_ok=True)
        
        self.current_image_path = ""
        self.original_image_name = "" 
        self.minutiae_data = [] 
        self.pil_image = None
        
        self.init_ui()

    def open_minutiae_explorer(self):
        if not self.minutiae_data:
            QMessageBox.warning(self, "No Data", "No minutiae data found. Please load an image and run detection first.")
            return
            
        w = int(self.lbl_width.text())
        h = int(self.lbl_height.text())
        
        display_name = self.original_image_name if self.original_image_name else "Unknown Image"
        self.explorer_window = MinutiaeVisualizerWindow(self.minutiae_data, w, h, display_name, self)
        self.explorer_window.show()
        
    def init_ui(self):
        central_widget = QWidget()
        self.setCentralWidget(central_widget)
        main_layout = QHBoxLayout(central_widget)
        
        control_layout = QVBoxLayout()
        control_layout.setAlignment(Qt.AlignTop)
        
        btn_open = QPushButton("Open Image")
        btn_open.clicked.connect(self.open_file_dialog)
        control_layout.addWidget(btn_open)

        info_group = QGroupBox("Image Info")
        info_layout = QFormLayout()
        self.lbl_width = QLabel("0")
        self.lbl_height = QLabel("0")
        self.lbl_total_min = QLabel("0")
        self.lbl_disp_min = QLabel("0")
        info_layout.addRow("Width:", self.lbl_width)
        info_layout.addRow("Height:", self.lbl_height)
        info_layout.addRow("Total Minutiae:", self.lbl_total_min)
        info_layout.addRow("Displayed:", self.lbl_disp_min)
        info_group.setLayout(info_layout)
        control_layout.addWidget(info_group)

        display_group = QGroupBox("Display Settings")
        disp_layout = QFormLayout()
        
        self.quality_slider = QSlider(Qt.Horizontal)
        self.quality_slider.setRange(0, 99)
        self.quality_slider.valueChanged.connect(self.update_overlay)
        
        self.opacity_slider = QSlider(Qt.Horizontal)
        self.opacity_slider.setRange(0, 100)
        self.opacity_slider.setValue(100)
        self.opacity_slider.valueChanged.connect(self.update_image_display)

        self.contrast_slider = QSlider(Qt.Horizontal)
        self.contrast_slider.setRange(-100, 100)
        self.contrast_slider.setValue(0)
        self.contrast_slider.valueChanged.connect(self.update_image_display)

        self.brightness_slider = QSlider(Qt.Horizontal)
        self.brightness_slider.setRange(-100, 100)
        self.brightness_slider.setValue(0)
        self.brightness_slider.valueChanged.connect(self.update_image_display)

        disp_layout.addRow("Minutiae Quality:", self.quality_slider)
        disp_layout.addRow("Image Opacity:", self.opacity_slider)
        disp_layout.addRow("Contrast:", self.contrast_slider)
        disp_layout.addRow("Brightness:", self.brightness_slider)
        display_group.setLayout(disp_layout)
        control_layout.addWidget(display_group)

        btn_min_detect = QPushButton("Min. Detect")
        btn_min_detect.clicked.connect(self.run_minutiae_detection)
        
        btn_nfiq = QPushButton("NFIQ Score")
        btn_nfiq.clicked.connect(self.run_nfiq)
        
        btn_show_data = QPushButton("View Minutiae Explorer")
        btn_show_data.clicked.connect(self.open_minutiae_explorer)
        
        self.lbl_nfiq = QLabel("N/A")
        
        action_layout = QHBoxLayout()
        action_layout.addWidget(btn_min_detect)
        action_layout.addWidget(btn_nfiq)
        action_layout.addWidget(btn_show_data)
        action_layout.addWidget(self.lbl_nfiq)
        control_layout.addLayout(action_layout)

        main_layout.addLayout(control_layout, 1)

        self.scene = QGraphicsScene()
        self.view = InteractiveGraphicsView(self.scene)
        main_layout.addWidget(self.view, 4)
        
        self.image_item = None
        self.minutiae_items = []

    def dragEnterEvent(self, event):
        if event.mimeData().hasUrls():
            event.accept()
        else:
            event.ignore()

    def dropEvent(self, event):
        urls = event.mimeData().urls()
        if urls:
            file_path = urls[0].toLocalFile()
            self.process_file(file_path)

    def open_file_dialog(self):
        file_path, _ = QFileDialog.getOpenFileName(self, "Open Image File", "", "Images (*.bmp *.gif *.jpg *.png *.raw *.tif *.wsq)")
        if file_path:
            self.process_file(file_path)

    def process_file(self, file_path):
        self.current_image_path = file_path
        self.original_image_name = os.path.basename(file_path)
        ext = os.path.splitext(file_path)[1].lower()
        prefix = os.path.splitext(self.original_image_name)[0]
        
        png_path = os.path.join(self.temp_dir, f"{prefix}.png")

        if ext == '.wsq':
            self.convert_wsq(file_path, prefix)
        elif ext == '.raw':
            self.convert_raw(file_path, prefix, png_path)
        else:
            shutil.copy(file_path, png_path)
            self.current_image_path = png_path

        self.load_image(self.current_image_path)
        self.run_minutiae_detection()

    def convert_wsq(self, input_path, prefix):
        dwsq_exe = os.path.join(self.app_path, "dwsq.exe")
        if not os.path.exists(dwsq_exe):
            QMessageBox.critical(self, "Error", "dwsq.exe not found.")
            return

        target_wsq = os.path.join(self.temp_dir, f"{prefix}.wsq")
        shutil.copy(input_path, target_wsq)
        
        subprocess.run([dwsq_exe, "raw", target_wsq, "-r"], creationflags=subprocess.CREATE_NO_WINDOW)
        raw_path = os.path.join(self.temp_dir, f"{prefix}.raw")
        
        ncm_path = os.path.join(self.temp_dir, f"{prefix}.ncm")
        w, h = 0, 0
        if os.path.exists(ncm_path):
            with open(ncm_path, 'r') as f:
                for line in f:
                    if "PIX_WIDTH" in line:
                        w = int(line.split()[1])
                    if "PIX_HEIGHT" in line:
                        h = int(line.split()[1])
                        
        if w > 0 and h > 0:
            self.process_raw_to_png(raw_path, os.path.join(self.temp_dir, f"{prefix}.png"), w, h)
            self.current_image_path = os.path.join(self.temp_dir, f"{prefix}.png")

    def convert_raw(self, input_path, prefix, png_path):
        file_size = os.path.getsize(input_path)
        dialog = RawDimensionDialog(input_path, file_size, self)
        if dialog.exec_() == QDialog.Accepted:
            self.process_raw_to_png(input_path, png_path, dialog.width_val, dialog.height_val)
            self.current_image_path = png_path

    def process_raw_to_png(self, raw_path, png_path, width, height):
        with open(raw_path, "rb") as f:
            raw_data = f.read()
        image = Image.frombytes("L", (width, height), raw_data)
        image.save(png_path, "PNG")

    def load_image(self, path):
        try:
            self.pil_image = Image.open(path).convert("L") 
            self.lbl_width.setText(str(self.pil_image.width))
            self.lbl_height.setText(str(self.pil_image.height))
            self.update_image_display()
        except Exception as e:
            QMessageBox.critical(self, "Error", f"Failed to load image: {str(e)}")

    def update_image_display(self):
        if not self.pil_image:
            return

        img = self.pil_image.copy()

        c_val = self.contrast_slider.value()
        img = ImageEnhance.Contrast(img).enhance(1.0 + (c_val / 100.0))

        b_val = self.brightness_slider.value()
        img = ImageEnhance.Brightness(img).enhance(1.0 + (b_val / 100.0))

        img = img.convert("RGBA")
        data = img.tobytes("raw", "RGBA")
        qim = QImage(data, img.width, img.height, QImage.Format_RGBA8888)

        pixmap = QPixmap.fromImage(qim)

        opacity = self.opacity_slider.value() / 100.0

        self.scene.clear()
        self.minutiae_items.clear()

        self.image_item = self.scene.addPixmap(pixmap)
        self.image_item.setOpacity(opacity)

        # CRITICAL FIX: lock coordinate system to image pixels
        self.scene.setSceneRect(0, 0, pixmap.width(), pixmap.height())

        self.update_overlay()
        self.view.fitInView(self.scene.sceneRect(), Qt.KeepAspectRatio)

    def run_minutiae_detection(self):
        if not self.current_image_path: return
        
        mindtct_exe = os.path.join(self.app_path, "mindtct.exe")
        if not os.path.exists(mindtct_exe):
            QMessageBox.critical(self, "Error", "mindtct.exe not found.")
            return

        prefix = os.path.splitext(os.path.basename(self.current_image_path))[0]
        out_prefix = os.path.join(self.temp_dir, f"{prefix}_iafis")
        
        try:
            subprocess.run([mindtct_exe, self.current_image_path, out_prefix], 
                           creationflags=subprocess.CREATE_NO_WINDOW)
            self.parse_minutiae(out_prefix + ".min")
            self.update_overlay()
        except Exception as e:
            QMessageBox.critical(self, "Execution Error", str(e))


    def parse_minutiae(self, min_file):
        self.minutiae_data.clear()
        if not os.path.exists(min_file):
            return

        pattern = re.compile(
            r"(\d+)\s*,\s*(\d+)\s*:\s*([\d.]+)\s*:\s*([\d.]+)\s*:\s*(\w+)"
        )

        with open(min_file, 'r') as f:
            lines = f.readlines()

        if len(lines) > 2:
            try:
                total_min = int(lines[2].split()[0])
                self.lbl_total_min.setText(str(total_min))
            except:
                self.lbl_total_min.setText("N/A")

        for line in lines[4:]:
            match = pattern.search(line)
            if match:
                x = int(match.group(1))
                y = int(match.group(2))
                direction = float(match.group(3))
                quality = float(match.group(4))
                m_type = match.group(5)

                self.minutiae_data.append({
                    "x": x,
                    "y": y,
                    "dir": direction,
                    "rel": quality,
                    "typ": m_type
                })

    def update_overlay(self):
        for item in self.minutiae_items:
            self.scene.removeItem(item)
        self.minutiae_items.clear()

        if not self.minutiae_data:
            return

        quality_threshold = self.quality_slider.value() / 100.0
        displayed = 0

        radius = 4
        line_len = 14

        for m in self.minutiae_data:
            if m["rel"] < quality_threshold:
                continue

            displayed += 1

            x, y = m["x"], m["y"]

            angle = math.radians(m["dir"] * 11.25 - 90)
            x2 = x + math.cos(angle) * line_len
            y2 = y + math.sin(angle) * line_len

            if m["typ"] == "RIG":
                color = QColor(255, 0, 0)
            elif m["typ"] == "BIF":
                color = QColor(0, 255, 0)
            else:
                color = QColor(0, 0, 255)

            pen = QPen(color, 2)

            if m["typ"] == "BIF":
                item = self.scene.addRect(x - radius, y - radius, radius * 2, radius * 2, pen)
            else:
                item = self.scene.addEllipse(x - radius, y - radius, radius * 2, radius * 2, pen)

            line = self.scene.addLine(x, y, x2, y2, pen)

            self.minutiae_items.extend([item, line])

        self.lbl_disp_min.setText(str(displayed))


    def run_nfiq(self):
        if not self.current_image_path: return
        nfiq_exe = os.path.join(self.app_path, "nfiq.exe")
        if not os.path.exists(nfiq_exe):
            QMessageBox.critical(self, "Error", "nfiq.exe not found.")
            return

        try:
            result = subprocess.run([nfiq_exe, self.current_image_path], 
                                    capture_output=True, text=True, creationflags=subprocess.CREATE_NO_WINDOW)
            self.lbl_nfiq.setText(result.stdout.strip() or "N/A")
        except Exception as e:
            QMessageBox.critical(self, "NFIQ Error", str(e))

    def closeEvent(self, event):
        if os.path.exists(self.temp_dir):
            shutil.rmtree(self.temp_dir, ignore_errors=True)
        event.accept()

if __name__ == "__main__":
    app = QApplication(sys.argv)
    window = MainWindow()
    window.show()
    sys.exit(app.exec_())