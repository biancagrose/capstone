import sys
from PyQt5.QtWidgets import (
    QApplication, QMainWindow, QWidget, QHBoxLayout, QVBoxLayout,
    QLabel, QPushButton, QFrame
)
from PyQt5.QtCore import Qt


class ForensicAFIS(QMainWindow):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("Forensic AFIS")
        self.setMinimumSize(1200, 800)

        # CENTRAL WIDGET
        central = QWidget()
        central.setObjectName("MainContainer")
        self.setCentralWidget(central)

        main_layout = QVBoxLayout(central)
        main_layout.setContentsMargins(0, 0, 0, 0)

        # NAVBAR
        navbar = QFrame()
        navbar.setObjectName("NavBar")
        navbar_layout = QHBoxLayout(navbar)
        navbar.setFixedHeight(60)
        navbar_layout.setAlignment(Qt.AlignVCenter)

        title = QLabel("FORENSIC AFIS")
        title.setObjectName("NavTitle")

        navbar_layout.addWidget(title)

        navbar_layout.addStretch() 

        # BUTTON CONTAINER
        button_container = QWidget()
        button_container.setObjectName("NavButtonContainer")
        button_layout = QHBoxLayout(button_container)
        button_layout.setContentsMargins(0, 0, 0, 0)
        button_layout.setSpacing(20)

        for name in ["Dashboard", "Analysis", "Reports", "Settings"]:
            btn = QPushButton(name)
            btn.setObjectName("NavButton")
            button_layout.addWidget(btn)

        navbar_layout.addWidget(button_container)
        navbar_layout.addStretch()
        main_layout.addWidget(navbar)

        # CONTENT AREA
        content = QFrame()
        content.setObjectName("ContentArea")
        content_layout = QHBoxLayout(content)

        # LEFT PANEL
        left_panel = QFrame()
        left_panel.setObjectName("LeftPanel")
        left_layout = QVBoxLayout(left_panel)

        upload_box = self.create_card("Upload Fingerprint")
        left_layout.addWidget(upload_box)

        metadata_box = self.create_card("Sample Metadata")
        left_layout.addWidget(metadata_box)

        content_layout.addWidget(left_panel, 1)

        # CENTER PANEL
        center_panel = QFrame()
        center_panel.setObjectName("CenterPanel")
        center_layout = QVBoxLayout(center_panel)

        viewer = self.create_card("Evidence Image Viewer")
        viewer.setMinimumHeight(300)
        center_layout.addWidget(viewer)

        comparison = self.create_card("Comparison View")
        center_layout.addWidget(comparison)

        content_layout.addWidget(center_panel, 2)

        # RIGHT PANEL
        right_panel = QFrame()
        right_panel.setObjectName("RightPanel")
        right_layout = QVBoxLayout(right_panel)

        results = self.create_card("Analysis Results")
        right_layout.addWidget(results)

        actions = self.create_card("Actions")
        right_layout.addWidget(actions)

        content_layout.addWidget(right_panel, 1)

        main_layout.addWidget(content)

    def create_card(self, title):
        card = QFrame()
        card.setObjectName("Card")

        layout = QVBoxLayout(card)

        label = QLabel(title)
        label.setObjectName("CardTitle")

        content = QLabel("Content goes here")
        content.setObjectName("CardContent")
        content.setAlignment(Qt.AlignCenter)

        layout.addWidget(label)
        layout.addWidget(content)

        return card


if __name__ == "__main__":
    app = QApplication(sys.argv)

    window = ForensicAFIS()

    # LOAD STYLESHEET
    with open("Test python code v1\style.css", "r") as f:
        app.setStyleSheet(f.read())

    window.show()
    sys.exit(app.exec_())