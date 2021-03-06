﻿using System;
using Duality.Drawing;
using OpenTK.Graphics.ES30;

namespace Duality.Backend.Android.OpenTK
{
	public class NativeGraphicsBuffer : INativeGraphicsBuffer
	{
		private static NativeGraphicsBuffer boundVertex = null;
		private static NativeGraphicsBuffer boundIndex = null;
		
		public static void Bind(GraphicsBufferType type, NativeGraphicsBuffer buffer)
		{
			if (GetBound(type) == buffer) return;
			SetBound(type, buffer);

			BufferTarget target = ToOpenTKBufferType(type);
			GL.BindBuffer(target, buffer != null ? buffer.Handle : 0);
		}
		private static void SetBound(GraphicsBufferType type, NativeGraphicsBuffer buffer)
		{
			if (type == GraphicsBufferType.Vertex) boundVertex = buffer;
			else if (type == GraphicsBufferType.Index) boundIndex = buffer;
			else return;
		}
		private static NativeGraphicsBuffer GetBound(GraphicsBufferType type)
		{
			if (type == GraphicsBufferType.Vertex) return boundVertex;
			else if (type == GraphicsBufferType.Index) return boundIndex;
			else return null;
		}

		internal static void RebindIndexBuffer()
		{
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, boundIndex != null ? boundIndex.Handle : 0);
		}


		private int handle = 0;
		private GraphicsBufferType type = GraphicsBufferType.Vertex;
		private int bufferSize = 0;

		public int Handle
		{
			get { return this.handle; }
		}
		public GraphicsBufferType BufferType
		{
			get { return this.type; }
		}
		public int Length
		{
			get { return this.bufferSize; }
		}

		public NativeGraphicsBuffer(GraphicsBufferType type)
		{
			GL.GenBuffers(1, out this.handle);
			this.type = type;
		}

		public void SetupEmpty(int size)
		{
			if (size < 0) throw new ArgumentException("Size cannot be less than zero.");

			NativeGraphicsBuffer prevBound = GetBound(this.type);
			Bind(this.type, this);

			BufferTarget target = ToOpenTKBufferType(this.type);
			GL.BufferData(target, new IntPtr(size), IntPtr.Zero, BufferUsage.StreamDraw);

			this.bufferSize = size;

			Bind(this.type, prevBound);
		}
		public void LoadData(IntPtr data, int size)
		{
			if (size < 0) throw new ArgumentException("Size cannot be less than zero.");

			NativeGraphicsBuffer prevBound = GetBound(this.type);
			Bind(this.type, this);

			BufferTarget target = ToOpenTKBufferType(this.type);
			GL.BufferData(target, new IntPtr(size), IntPtr.Zero, BufferUsage.StreamDraw);
			GL.BufferData(target, new IntPtr(size), data, BufferUsage.StreamDraw);

			this.bufferSize = size;

			Bind(this.type, prevBound);
		}
		public void LoadSubData(IntPtr offset, IntPtr data, int size)
		{
			if (size < 0) throw new ArgumentException("Size cannot be less than zero.");
			if (this.bufferSize == 0) throw new InvalidOperationException(string.Format(
				"Cannot update {0}, because its storage was not initialized yet.",
				typeof(NativeGraphicsBuffer).Name));
			if ((uint)offset + size > this.bufferSize) throw new ArgumentException(string.Format(
				"Cannot update {0} with offset {1} and size {2}, as this exceeds the internal " +
				"storage size {3}.",
				typeof(NativeGraphicsBuffer).Name,
				offset,
				size,
				this.bufferSize));

			NativeGraphicsBuffer prevBound = GetBound(this.type);
			Bind(this.type, this);

			BufferTarget target = ToOpenTKBufferType(this.type);
			GL.BufferSubData(target, offset, new IntPtr(size), data);

			Bind(this.type, prevBound);
		}
		public void Dispose()
		{
			if (DualityApp.ExecContext != DualityApp.ExecutionContext.Terminated &&
				this.handle != 0)
			{
				//DefaultOpenTKBackendPlugin.GuardSingleThreadState();
				GL.DeleteBuffers(1, ref this.handle);
			}
			this.handle = 0;
			this.bufferSize = 0;
		}

		private static BufferTarget ToOpenTKBufferType(GraphicsBufferType value)
		{
			switch (value)
			{
				case GraphicsBufferType.Vertex: return BufferTarget.ArrayBuffer;
				case GraphicsBufferType.Index: return BufferTarget.ElementArrayBuffer;
			}

			return BufferTarget.ArrayBuffer;
		}
	}
}