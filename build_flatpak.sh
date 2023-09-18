#!/bin/sh

if ! command -v flatpak-builder &> /dev/null
then
    echo "flatpak-builder could not be found"
    exit
fi

if ! command -v dotnet-warp &> /dev/null
then
    echo "dotnet-warp could not be found"
    echo "Please install it with 'dotnet tool install -g dotnet-warp'"
    echo "and make sure it is in your \$PATH"
    exit
fi

# Create a new build directory
rm -r flatpak_build/ # Remove the old build directory
mkdir -p flatpak_build/

# Build the app
dotnet-warp -o flatpak_build/vkbasalt_configurator
cp -r data/* flatpak_build/ # Copy the data directory to the build directory

# Build the flatpak
cd flatpak_build/
flatpak-builder --repo=repo --force-clean build-dir lu.fournux.vkbasalt.configurator.yml --install --user