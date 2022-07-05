import time

import cv2
from cvzone.HandTrackingModule import HandDetector
import socket

# Parameters
width, height = 1280, 720

# Webcam
cap = cv2.VideoCapture(0)
cap.set(3, width)
cap.set(4, height)

# Hand Detector
detector = HandDetector(maxHands=2, detectionCon=0.8)
gestureTreshold = 600

# Communication
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort = ("127.0.0.1", 5059)

flashlight = 1

while True:
    # Get the frame from the webcam
    success, img = cap.read()
    img = cv2.flip(img, 1)
    # Hands
    hands, img = detector.findHands(img)
    cv2.line(img, (0, gestureTreshold), (width, gestureTreshold), (0,255,0), 10)

    data = []
    # Landmark values - (x, y, z) * 21 per hand
    rotation = 0
    direction = 0
    if hands:
        for hand in hands:
            cx, cy = hand['center']

            if cy <= gestureTreshold:
                fingers = detector.fingersUp(hand)
                # Rotation
                if hand['type'] == "Left":
                    if fingers == [1,1,0,0,1] or fingers == [0,1,0,0,1] or fingers == [0,0,0,0,1]:
                        if flashlight == 1:
                            print("Taschenlampe aus")
                            flashlight = 0
                        elif flashlight == 0:
                            print("Taschenlampe an")
                            flashlight = 1
                        time.sleep(0.5)
                    if fingers == [0,1,0,0,0] or fingers == [1,1,0,0,0] or fingers == [1,1,0,0,1] or fingers == [0,1,0,0,1]:
                        lmList = hand['lmList']
                        indexBottom = lmList[5]
                        indexTop = lmList[8]

                        if abs(indexTop[0] - indexBottom[0]) <= 20:
                            rotation = 3
                            print("Keine Rotation")
                        elif -20 > indexTop[0] - indexBottom[0] >= -60:
                            rotation = 2
                            print("Langsame Rotation links")
                        elif 20 < indexTop[0] - indexBottom[0] <= 60:
                            rotation = 4
                            print("Langsame Rotation rechts")
                        elif indexTop[0] - indexBottom[0] < -60:
                            rotation = 1
                            print("Schnelle Rotation links")
                        elif indexTop[0] - indexBottom[0] > 60:
                            rotation = 5
                            print("Schnelle Rotation rechts")
                        else:
                            rotation = 0
                            print("Ungültig")

                # Translation
                if hand['type'] == "Right":
                    if fingers == [0, 1, 0, 0, 0] or fingers == [1, 1, 0, 0, 0]:
                        lmList = hand['lmList']
                        indexBottom = lmList[5]
                        indexTop = lmList[8]

                        if abs(indexTop[0] - indexBottom[0]) <= 60:
                            print("Bewegung nach vorne")
                            direction = 3
                        elif indexTop[0] - indexBottom[0] < -60:
                            print("Bewegung nach links")
                            direction = 1
                        elif indexTop[0] - indexBottom[0] > 60:
                            print("Bewegung nach rechts")
                            direction = 5
                        else:
                            direction = 0
                            print("Ungültig")

                    if fingers == [0, 1, 1, 0, 0] or fingers == [1, 1, 1, 0, 0]:
                        print("Bewegung nach hinten")
                        direction = -1



        direction_rotation_flashlight = str(direction) + "," + str(rotation) + "," + str(flashlight)
        # print(direction_rotation_flashlight)
        sock.sendto(str.encode(direction_rotation_flashlight), serverAddressPort)



    img = cv2.resize(img,(0,0), None, 0.5, 0.5)
    cv2.imshow("Image", img)
    key = cv2.waitKey(1)
    if key == ord('q'):
        break
