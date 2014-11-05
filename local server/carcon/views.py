import time
import socket
from flask import request, url_for, render_template, redirect, send_file
from flask import Response

from carcon import app


_last_image = None
_last_update = 0

@app.route('/')
def home():
    """Show the home page."""
    return render_template('console.html')


@app.route('/upload-image/', methods=['POST'])
def upload():
    """Receive current camera image."""
    global _last_image
    global _last_update
    _last_image = request.files['file'].read()
    _last_update = time.time()
    return 'ok'


@app.route('/image.jpg')
def get_image():
    """Return the last image uploaded or redirect to offline image."""
    if _last_image is None or \
            time.time() - _last_update > app.config['OFFLINE_TIMEOUT']:
        return redirect(url_for('static', filename='offline.jpg'))
    else:
        return Response(_last_image, mimetype='image/jpeg')


@app.route('/cmd/<cmd>')
def send_cmd(cmd):
    """Send a command to forwarding server."""
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    cmd = "%s\r\n" % cmd  # Append a Windows line break.
    sock.sendto(cmd.encode('utf-8'), app.config['FORWARDING_SERVER'])
    return '', 204  # 204 No Content


