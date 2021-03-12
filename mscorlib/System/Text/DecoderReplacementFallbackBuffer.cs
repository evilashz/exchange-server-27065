using System;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A3A RID: 2618
	public sealed class DecoderReplacementFallbackBuffer : DecoderFallbackBuffer
	{
		// Token: 0x0600669D RID: 26269 RVA: 0x0015999F File Offset: 0x00157B9F
		public DecoderReplacementFallbackBuffer(DecoderReplacementFallback fallback)
		{
			this.strDefault = fallback.DefaultString;
		}

		// Token: 0x0600669E RID: 26270 RVA: 0x001599C1 File Offset: 0x00157BC1
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			if (this.fallbackCount >= 1)
			{
				base.ThrowLastBytesRecursive(bytesUnknown);
			}
			if (this.strDefault.Length == 0)
			{
				return false;
			}
			this.fallbackCount = this.strDefault.Length;
			this.fallbackIndex = -1;
			return true;
		}

		// Token: 0x0600669F RID: 26271 RVA: 0x001599FC File Offset: 0x00157BFC
		public override char GetNextChar()
		{
			this.fallbackCount--;
			this.fallbackIndex++;
			if (this.fallbackCount < 0)
			{
				return '\0';
			}
			if (this.fallbackCount == 2147483647)
			{
				this.fallbackCount = -1;
				return '\0';
			}
			return this.strDefault[this.fallbackIndex];
		}

		// Token: 0x060066A0 RID: 26272 RVA: 0x00159A57 File Offset: 0x00157C57
		public override bool MovePrevious()
		{
			if (this.fallbackCount >= -1 && this.fallbackIndex >= 0)
			{
				this.fallbackIndex--;
				this.fallbackCount++;
				return true;
			}
			return false;
		}

		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x060066A1 RID: 26273 RVA: 0x00159A8A File Offset: 0x00157C8A
		public override int Remaining
		{
			get
			{
				if (this.fallbackCount >= 0)
				{
					return this.fallbackCount;
				}
				return 0;
			}
		}

		// Token: 0x060066A2 RID: 26274 RVA: 0x00159A9D File Offset: 0x00157C9D
		[SecuritySafeCritical]
		public override void Reset()
		{
			this.fallbackCount = -1;
			this.fallbackIndex = -1;
			this.byteStart = null;
		}

		// Token: 0x060066A3 RID: 26275 RVA: 0x00159AB5 File Offset: 0x00157CB5
		[SecurityCritical]
		internal unsafe override int InternalFallback(byte[] bytes, byte* pBytes)
		{
			return this.strDefault.Length;
		}

		// Token: 0x04002D96 RID: 11670
		private string strDefault;

		// Token: 0x04002D97 RID: 11671
		private int fallbackCount = -1;

		// Token: 0x04002D98 RID: 11672
		private int fallbackIndex = -1;
	}
}
