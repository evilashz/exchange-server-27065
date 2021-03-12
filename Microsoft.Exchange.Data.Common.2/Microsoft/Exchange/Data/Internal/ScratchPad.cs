using System;
using System.Diagnostics;
using System.Text;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x02000144 RID: 324
	internal static class ScratchPad
	{
		// Token: 0x06000C94 RID: 3220 RVA: 0x0006E8C2 File Offset: 0x0006CAC2
		public static void Begin()
		{
			if (ScratchPad.pad == null)
			{
				ScratchPad.pad = new ScratchPad.ScratchPadContainer();
				return;
			}
			ScratchPad.pad.AddRef();
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0006E8E0 File Offset: 0x0006CAE0
		public static void End()
		{
			if (ScratchPad.pad != null && ScratchPad.pad.Release())
			{
				ScratchPad.pad = null;
			}
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0006E8FB File Offset: 0x0006CAFB
		public static byte[] GetByteBuffer(int size)
		{
			if (ScratchPad.pad == null)
			{
				return new byte[size];
			}
			return ScratchPad.pad.GetByteBuffer(size);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0006E916 File Offset: 0x0006CB16
		[Conditional("DEBUG")]
		public static void ReleaseByteBuffer()
		{
			if (ScratchPad.pad != null)
			{
				ScratchPad.pad.ReleaseByteBuffer();
			}
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0006E929 File Offset: 0x0006CB29
		public static char[] GetCharBuffer(int size)
		{
			if (ScratchPad.pad == null)
			{
				return new char[size];
			}
			return ScratchPad.pad.GetCharBuffer(size);
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0006E944 File Offset: 0x0006CB44
		[Conditional("DEBUG")]
		public static void ReleaseCharBuffer()
		{
			if (ScratchPad.pad != null)
			{
				ScratchPad.pad.ReleaseCharBuffer();
			}
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0006E957 File Offset: 0x0006CB57
		public static StringBuilder GetStringBuilder()
		{
			return ScratchPad.GetStringBuilder(16);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0006E960 File Offset: 0x0006CB60
		public static StringBuilder GetStringBuilder(int initialCapacity)
		{
			if (ScratchPad.pad == null)
			{
				return new StringBuilder(initialCapacity);
			}
			return ScratchPad.pad.GetStringBuilder(initialCapacity);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0006E97B File Offset: 0x0006CB7B
		public static void ReleaseStringBuilder()
		{
			if (ScratchPad.pad != null)
			{
				ScratchPad.pad.ReleaseStringBuilder();
			}
		}

		// Token: 0x04000F16 RID: 3862
		[ThreadStatic]
		private static ScratchPad.ScratchPadContainer pad;

		// Token: 0x02000145 RID: 325
		private class ScratchPadContainer
		{
			// Token: 0x06000C9D RID: 3229 RVA: 0x0006E98E File Offset: 0x0006CB8E
			public ScratchPadContainer()
			{
				this.refCount = 1;
			}

			// Token: 0x06000C9E RID: 3230 RVA: 0x0006E99D File Offset: 0x0006CB9D
			public void AddRef()
			{
				this.refCount++;
			}

			// Token: 0x06000C9F RID: 3231 RVA: 0x0006E9AD File Offset: 0x0006CBAD
			public bool Release()
			{
				this.refCount--;
				return this.refCount == 0;
			}

			// Token: 0x06000CA0 RID: 3232 RVA: 0x0006E9C6 File Offset: 0x0006CBC6
			public byte[] GetByteBuffer(int size)
			{
				if (this.byteBuffer == null || this.byteBuffer.Length < size)
				{
					this.byteBuffer = new byte[size];
				}
				return this.byteBuffer;
			}

			// Token: 0x06000CA1 RID: 3233 RVA: 0x0006E9ED File Offset: 0x0006CBED
			public void ReleaseByteBuffer()
			{
			}

			// Token: 0x06000CA2 RID: 3234 RVA: 0x0006E9EF File Offset: 0x0006CBEF
			public char[] GetCharBuffer(int size)
			{
				if (this.charBuffer == null || this.charBuffer.Length < size)
				{
					this.charBuffer = new char[size];
				}
				return this.charBuffer;
			}

			// Token: 0x06000CA3 RID: 3235 RVA: 0x0006EA16 File Offset: 0x0006CC16
			public void ReleaseCharBuffer()
			{
			}

			// Token: 0x06000CA4 RID: 3236 RVA: 0x0006EA18 File Offset: 0x0006CC18
			public StringBuilder GetStringBuilder(int initialCapacity)
			{
				if (initialCapacity <= 512)
				{
					if (this.stringBuilder == null)
					{
						this.stringBuilder = new StringBuilder(512);
					}
					else
					{
						this.stringBuilder.Length = 0;
					}
					return this.stringBuilder;
				}
				return new StringBuilder(initialCapacity);
			}

			// Token: 0x06000CA5 RID: 3237 RVA: 0x0006EA55 File Offset: 0x0006CC55
			public void ReleaseStringBuilder()
			{
				if (this.stringBuilder != null && (this.stringBuilder.Capacity > 512 || this.stringBuilder.Length * 2 >= this.stringBuilder.Capacity + 1))
				{
					this.stringBuilder = null;
				}
			}

			// Token: 0x04000F17 RID: 3863
			public const int ScratchStringBuilderCapacity = 512;

			// Token: 0x04000F18 RID: 3864
			private int refCount;

			// Token: 0x04000F19 RID: 3865
			private byte[] byteBuffer;

			// Token: 0x04000F1A RID: 3866
			private char[] charBuffer;

			// Token: 0x04000F1B RID: 3867
			private StringBuilder stringBuilder;
		}
	}
}
