using System;
using System.Security;
using System.Threading;

namespace System.Text
{
	// Token: 0x02000A3E RID: 2622
	internal sealed class InternalEncoderBestFitFallbackBuffer : EncoderFallbackBuffer
	{
		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x060066C5 RID: 26309 RVA: 0x0015A4A4 File Offset: 0x001586A4
		private static object InternalSyncObject
		{
			get
			{
				if (InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange<object>(ref InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject, value, null);
				}
				return InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject;
			}
		}

		// Token: 0x060066C6 RID: 26310 RVA: 0x0015A4D0 File Offset: 0x001586D0
		public InternalEncoderBestFitFallbackBuffer(InternalEncoderBestFitFallback fallback)
		{
			this.oFallback = fallback;
			if (this.oFallback.arrayBestFit == null)
			{
				object internalSyncObject = InternalEncoderBestFitFallbackBuffer.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (this.oFallback.arrayBestFit == null)
					{
						this.oFallback.arrayBestFit = fallback.encoding.GetBestFitUnicodeToBytesData();
					}
				}
			}
		}

		// Token: 0x060066C7 RID: 26311 RVA: 0x0015A550 File Offset: 0x00158750
		public override bool Fallback(char charUnknown, int index)
		{
			this.iCount = (this.iSize = 1);
			this.cBestFit = this.TryBestFit(charUnknown);
			if (this.cBestFit == '\0')
			{
				this.cBestFit = '?';
			}
			return true;
		}

		// Token: 0x060066C8 RID: 26312 RVA: 0x0015A58C File Offset: 0x0015878C
		public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
		{
			if (!char.IsHighSurrogate(charUnknownHigh))
			{
				throw new ArgumentOutOfRangeException("charUnknownHigh", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					55296,
					56319
				}));
			}
			if (!char.IsLowSurrogate(charUnknownLow))
			{
				throw new ArgumentOutOfRangeException("CharUnknownLow", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					56320,
					57343
				}));
			}
			this.cBestFit = '?';
			this.iCount = (this.iSize = 2);
			return true;
		}

		// Token: 0x060066C9 RID: 26313 RVA: 0x0015A62C File Offset: 0x0015882C
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

		// Token: 0x060066CA RID: 26314 RVA: 0x0015A663 File Offset: 0x00158863
		public override bool MovePrevious()
		{
			if (this.iCount >= 0)
			{
				this.iCount++;
			}
			return this.iCount >= 0 && this.iCount <= this.iSize;
		}

		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x060066CB RID: 26315 RVA: 0x0015A698 File Offset: 0x00158898
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

		// Token: 0x060066CC RID: 26316 RVA: 0x0015A6AB File Offset: 0x001588AB
		[SecuritySafeCritical]
		public override void Reset()
		{
			this.iCount = -1;
			this.charStart = null;
			this.bFallingBack = false;
		}

		// Token: 0x060066CD RID: 26317 RVA: 0x0015A6C4 File Offset: 0x001588C4
		private char TryBestFit(char cUnknown)
		{
			int num = 0;
			int num2 = this.oFallback.arrayBestFit.Length;
			int num3;
			while ((num3 = num2 - num) > 6)
			{
				int i = num3 / 2 + num & 65534;
				char c = this.oFallback.arrayBestFit[i];
				if (c == cUnknown)
				{
					return this.oFallback.arrayBestFit[i + 1];
				}
				if (c < cUnknown)
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
				if (this.oFallback.arrayBestFit[i] == cUnknown)
				{
					return this.oFallback.arrayBestFit[i + 1];
				}
			}
			return '\0';
		}

		// Token: 0x04002DA2 RID: 11682
		private char cBestFit;

		// Token: 0x04002DA3 RID: 11683
		private InternalEncoderBestFitFallback oFallback;

		// Token: 0x04002DA4 RID: 11684
		private int iCount = -1;

		// Token: 0x04002DA5 RID: 11685
		private int iSize;

		// Token: 0x04002DA6 RID: 11686
		private static object s_InternalSyncObject;
	}
}
