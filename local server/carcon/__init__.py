"""Carcon - Lego car console server.

Powered by Flask, Python 3.3+.
"""
from flask import Flask

app = Flask(__name__)
app.config.from_object('carcon.config')

import carcon.views  # Must import after app configured.

