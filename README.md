# Video-2-Any-WPF

## Introduction

A video transcoding software made using iNROKE.UI.WPF controls.

## Usage

This software is deployed by Click Once service which provided by Visual Studio. You can get it on this website. With the help of the service, you can immediately update your V2A as soon as the time when we release the latest version.

This is the website: https://zmrj.wingsfrontier.top/Video-2-Any-WPF/Publish.html

Before use it, make sure that your computer has installed .Net 8.0 desktop runtime. We provided the latest version of the runtime on the website. You can click install directly. If you are a developer, you can just click the start to run this setup.exe to use this software.

## Preset

The default transcoding preset for this software is Default. The Default preset will not change the resolution and bitrate of your source video. If you use the other preset, you can adjust the resolution and bitrate of the video to compress the length of the file. By default, we calculate the scaling ratio based on the resolution height.

If you are an expert, you can write a default file yourself, in the format of Json text.

```json
{
  "Height": 1080,
  "Fps": 60,
  "VideoBitRate": 2500,
  "Rate": "veryfast"
}
```

Here is a VeryFast_1080p60.json file as an example, where Height represents the resolution height, Fps represents the frame rate of the converted video, VideoBitRate represents the code rate of the converted video, and Rate represents the FFmpeg transcoding process time rate.

After setting the source directory and save directory, you can choose to start directly or add it to the queue for processing together.

## Language Attention

We are sorry that this App does not currently support English and localization, but we will try our best to launch multi-language features and localization resources in the next version. Thank you for your support!