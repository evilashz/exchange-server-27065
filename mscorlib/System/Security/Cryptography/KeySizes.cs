using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000242 RID: 578
	[ComVisible(true)]
	public sealed class KeySizes
	{
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060020A8 RID: 8360 RVA: 0x000727E7 File Offset: 0x000709E7
		public int MinSize
		{
			get
			{
				return this.m_minSize;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060020A9 RID: 8361 RVA: 0x000727EF File Offset: 0x000709EF
		public int MaxSize
		{
			get
			{
				return this.m_maxSize;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x000727F7 File Offset: 0x000709F7
		public int SkipSize
		{
			get
			{
				return this.m_skipSize;
			}
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x000727FF File Offset: 0x000709FF
		public KeySizes(int minSize, int maxSize, int skipSize)
		{
			this.m_minSize = minSize;
			this.m_maxSize = maxSize;
			this.m_skipSize = skipSize;
		}

		// Token: 0x04000BDE RID: 3038
		private int m_minSize;

		// Token: 0x04000BDF RID: 3039
		private int m_maxSize;

		// Token: 0x04000BE0 RID: 3040
		private int m_skipSize;
	}
}
