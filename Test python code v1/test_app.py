import sys
from PyQt5.QtWidgets import (
    QApplication, QMainWindow, QPushButton, QLabel,
    QFileDialog, QVBoxLayout, QWidget, QTabWidget
)
from PyQt5.QtGui import QPixmap
from PyQt5.QtCore import Qt

class Playground(QMainWindow):

    def __init__(self):
        super().__init__()

        self.setWindowTitle("Blood Analysis UI Playground")
        self.setGeometry(100, 100, 800, 600)

        # Tabs
        self.tabs = QTabWidget()
        self.setCentralWidget(self.tabs)

        # Tab 1: Image Viewer
        self.image_tab = QWidget()
        self.tabs.addTab(self.image_tab, "Image Viewer")

        self.image_layout = QVBoxLayout()

        self.image_label = QLabel("Load an image")
        self.image_label.setAlignment(Qt.AlignCenter)

        self.load_btn = QPushButton("Load Image")
        self.load_btn.clicked.connect(self.load_image)

        self.image_layout.addWidget(self.image_label)
        self.image_layout.addWidget(self.load_btn)
        self.image_tab.setLayout(self.image_layout)

        # Tab 2: Dummy Controls
        self.control_tab = QWidget()
        self.tabs.addTab(self.control_tab, "Controls")

        self.control_layout = QVBoxLayout()

        self.test_label = QLabel("Press button to simulate analysis")
        self.test_btn = QPushButton("Run Analysis")
        self.test_btn.clicked.connect(self.fake_analysis)

        self.control_layout.addWidget(self.test_label)
        self.control_layout.addWidget(self.test_btn)
        self.control_tab.setLayout(self.control_layout)

    def load_image(self):
        file_name, _ = QFileDialog.getOpenFileName(self, "Open Image", "", "Images (*.png *.jpg *.bmp)")
        if file_name:
            pixmap = QPixmap(file_name)
            self.image_label.setPixmap(pixmap.scaled(
                self.image_label.width(),
                self.image_label.height(),
                Qt.KeepAspectRatio
            ))

    def fake_analysis(self):
        self.test_label.setText("Analysis complete (totally real, definitely not fake).")

def load_stylesheet(app, path):
    with open(path, "r") as f:
        app.setStyleSheet(f.read())

if __name__ == "__main__":
    app = QApplication(sys.argv)

    load_stylesheet(app, "Test python code v1\style.qss.css")

    window = Playground()
    window.show()

    sys.exit(app.exec_())