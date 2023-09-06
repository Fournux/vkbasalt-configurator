#!/bin/sh
# This file is used to set environment variables and run the app

######### Set environment variables #########

# Set the backend to the current session type
# This is needed for the app to know which backend to use
# as for some reason the GDK_BACKEND environment variable
# is set to "x11" even when running on Wayland
GDK_BACKEND=$XDG_SESSION_TYPE 

######### Run the app #########
vkbasalt_configurator
