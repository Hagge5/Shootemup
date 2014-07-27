﻿//////////////////////////////////////////////////////////////////////////
////License:  The MIT License (MIT)
////Copyright (c) 2010 David Amador (http://www.david-amador.com)
////
////Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
////
////The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
////
////THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IndependentResolutionRendering
{
	class Resolution
	{
		private static Resolution _instance;
		public static Resolution Instance
		{
			get
			{
				if (_instance == null)
					throw new NullReferenceException("Needs to be constructed");
				return _instance;
			}
		}

		public Color ClearColor { get; set; }
		private GraphicsDeviceManager _Device = null;
		private int _Width = 800;
		private int _Height = 600;
		private int _VWidth = 1024;
		private int _VHeight = 768;
		private Matrix _ScaleMatrix;
		private bool _FullScreen = false;
		private bool _dirtyMatrix = true;

		public Matrix TransformationMatrix
		{
			get
			{
				if (_dirtyMatrix) RecreateScaleMatrix();
				return _ScaleMatrix;
			}		
		}

		public Resolution(GraphicsDeviceManager device)
		{
			_instance = this;
			_Width = device.PreferredBackBufferWidth;
			_Height = device.PreferredBackBufferHeight;
			_Device = device;
			_dirtyMatrix = true;
			ApplyResolutionSettings();
		}

		public Resolution(GraphicsDeviceManager device, Color clearColor)
		{
			ClearColor = clearColor;
			_instance = this;
			_Width = device.PreferredBackBufferWidth;
			_Height = device.PreferredBackBufferHeight;
			_Device = device;
			_dirtyMatrix = true;
			ApplyResolutionSettings();
		}

		public void SetResolution(int Width, int Height, bool FullScreen)
		{
			_Width = Width;
			_Height = Height;

			_FullScreen = FullScreen;

		   ApplyResolutionSettings();
		}

		public void SetVirtualResolution(int Width, int Height)
		{
			_VWidth = Width;
			_VHeight = Height;

			_dirtyMatrix = true;
		}

		private void ApplyResolutionSettings()
	   {

#if XBOX360
		   _FullScreen = true;
#endif

		   // If we aren't using a full screen mode, the height and width of the window can
		   // be set to anything equal to or smaller than the actual screen size.
		   if (_FullScreen == false)
		   {
			   if ((_Width <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
				   && (_Height <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height))
			   {
				   _Device.PreferredBackBufferWidth = _Width;
				   _Device.PreferredBackBufferHeight = _Height;
				   _Device.IsFullScreen = _FullScreen;
				   _Device.ApplyChanges();
			   }
		   }
		   else
		   {
			   // If we are using full screen mode, we should check to make sure that the display
			   // adapter can handle the video mode we are trying to set.  To do this, we will
			   // iterate through the display modes supported by the adapter and check them against
			   // the mode we want to set.
			   foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
			   {
				   // Check the width and height of each mode against the passed values
				   if ((dm.Width == _Width) && (dm.Height == _Height))
				   {
					   // The mode is supported, so set the buffer formats, apply changes and return
					   _Device.PreferredBackBufferWidth = _Width;
					   _Device.PreferredBackBufferHeight = _Height;
					   _Device.IsFullScreen = _FullScreen;
					   _Device.ApplyChanges();
				   }
			   }
		   }

		   _dirtyMatrix = true;

		   _Width =   _Device.PreferredBackBufferWidth;
		   _Height = _Device.PreferredBackBufferHeight;
	   }

		/// <summary>
		/// Sets the device to use the draw pump
		/// Sets correct aspect ratio
		/// </summary>
		public void BeginDraw()
		{
			//The necessary calculations
			Calculate();
			// This way we are gonna have black bars if aspect ratio requires it and
			// the clear color on the rest
			_Device.GraphicsDevice.Clear(ClearColor);
		}

		private void RecreateScaleMatrix()
		{
			_dirtyMatrix = false;
			_ScaleMatrix = Matrix.CreateScale(
						   (float)_Device.GraphicsDevice.Viewport.Width / _VWidth,
						   (float)_Device.GraphicsDevice.Viewport.Width / _VWidth,
						   1f);
		}

		/// <summary>
		/// Get virtual Mode Aspect Ratio
		/// </summary>
		/// <returns>aspect ratio</returns>
		public float GetVirtualAspectRatio()
		{
			return (float)_VWidth / (float)_VHeight;
		}

		private void Calculate()
		{
			float targetAspectRatio = GetVirtualAspectRatio();
			// figure out the largest area that fits in this resolution at the desired aspect ratio
			int width = _Device.PreferredBackBufferWidth;
			int height = (int)(width / targetAspectRatio + .5f);
			bool changed = false;
			
			if (height > _Device.PreferredBackBufferHeight)
			{
				height = _Device.PreferredBackBufferHeight;
				// PillarBox
				width = (int)(height * targetAspectRatio + .5f);
				changed = true;
			}

			// set up the new viewport centered in the backbuffer
			Viewport viewport = new Viewport();

			viewport.X = (_Device.PreferredBackBufferWidth / 2) - (width / 2);
			viewport.Y = (_Device.PreferredBackBufferHeight / 2) - (height / 2);
			viewport.Width = width;
			viewport.Height = height;
			viewport.MinDepth = 0;
			viewport.MaxDepth = 1;

			if (changed)
			{
				_dirtyMatrix = true;
			}

			_Device.GraphicsDevice.Viewport = viewport;
		}

	}
}
