using System;
using System.Security;
using System.Threading;

namespace System.Text
{
	// Token: 0x02000A33 RID: 2611
	internal sealed class InternalDecoderBestFitFallbackBuffer : DecoderFallbackBuffer
	{
		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x06006669 RID: 26217 RVA: 0x00159208 File Offset: 0x00157408
		private static object InternalSyncObject
		{
			get
			{
				if (InternalDecoderBestFitFallbackBuffer.s_InternalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange<object>(ref InternalDecoderBestFitFallbackBuffer.s_InternalSyncObject, value, null);
				}
				return InternalDecoderBestFitFallbackBuffer.s_InternalSyncObject;
			}
		}

		// Token: 0x0600666A RID: 26218 RVA: 0x00159234 File Offset: 0x00157434
		public InternalDecoderBestFitFallbackBuffer(InternalDecoderBestFitFallback fallback)
		{
			this.oFallback = fallback;
			if (this.oFallback.arrayBestFit == null)
			{
				object internalSyncObject = InternalDecoderBestFitFallbackBuffer.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (this.oFallback.arrayBestFit == null)
					{
						this.oFallback.arrayBestFit = fallback.encoding.GetBestFitBytesToUnicodeData();
					}
				}
			}
		}

		// Token: 0x0600666B RID: 26219 RVA: 0x001592B4 File Offset: 0x001574B4
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			this.cBestFit = this.TryBestFit(bytesUnknown);
			if (this.cBestFit == '\0')
			{
				this.cBestFit = this.oFallback.cReplacement;
			}
			this.iCount = (this.iSize = 1);
			return true;
		}

		// Token: 0x0600666C RID: 26220 RVA: 0x001592F8 File Offset: 0x001574F8
		public override char GetNextChar()
		{
			this.iCount--;
			if (this.iCount < 0)
			{
				return '\0';
			}
			if (this.iCount == 2147483647)
			{
				this.iCount = -1;
				return '\0';
			}
			return this.cBestFit;
		}

		// Token: 0x0600666D RID: 26221 RVA: 0x0015932F File Offset: 0x0015752F
		public override bool MovePrevious()
		{
			if (this.iCount >= 0)
			{
				this.iCount++;
			}
			return this.iCount >= 0 && this.iCount <= this.iSize;
		}

		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x0600666E RID: 26222 RVA: 0x00159364 File Offset: 0x00157564
		public override int Remaining
		{
			get
			{
				if (this.iCount <= 0)
				{
					return 0;
				}
				return this.iCount;
			}
		}

		// Token: 0x0600666F RID: 26223 RVA: 0x00159377 File Offset: 0x00157577
		[SecuritySafeCritical]
		public override void Reset()
		{
			this.iCount = -1;
			this.byteStart = null;
		}

		// Token: 0x06006670 RID: 26224 RVA: 0x00159388 File Offset: 0x00157588
		[SecurityCritical]
		internal unsafe override int InternalFallback(byte[] bytes, byte* pBytes)
		{
			return 1;
		}

		// Token: 0x06006671 RID: 26225 RVA: 0x0015938C File Offset: 0x0015758C
		private char TryBestFit(byte[] bytesCheck)
		{
			int num = 0;
			int num2 = this.oFallback.arrayBestFit.Length;
			if (num2 == 0)
			{
				return '\0';
			}
			if (bytesCheck.Length == 0 || bytesCheck.Length > 2)
			{
				return '\0';
			}
			char c;
			if (bytesCheck.Length == 1)
			{
				c = (char)bytesCheck[0];
			}
			else
			{
				c = (char)(((int)bytesCheck[0] << 8) + (int)bytesCheck[1]);
			}
			if (c < this.oFallback.arrayBestFit[0] || c > this.oFallback.arrayBestFit[num2 - 2])
			{
				return '\0';
			}
			int num3;
			while ((num3 = num2 - num) > 6)
			{
				int i = num3 / 2 + num & 65534;
				char c2 = this.oFallback.arrayBestFit[i];
				if (c2 == c)
				{
					return this.oFallback.arrayBestFit[i + 1];
				}
				if (c2 < c)
				{
					num = i;
				}
				else
				{
					num2 = i;
				}
			}
			for (int i = num; i < num2; i += 2)
			{
				if (this.oFallback.arrayBestFit[i] == c)
				{
					return this.oFallback.arrayBestFit[i + 1];
				}
			}
			return '\0';
		}

		// Token: 0x04002D88 RID: 11656
		internal char cBestFit;

		// Token: 0x04002D89 RID: 11657
		internal int iCount = -1;

		// Token: 0x04002D8A RID: 11658
		internal int iSize;

		// Token: 0x04002D8B RID: 11659
		private InternalDecoderBestFitFallback oFallback;

		// Token: 0x04002D8C RID: 11660
		private static object s_InternalSyncObject;
	}
}
