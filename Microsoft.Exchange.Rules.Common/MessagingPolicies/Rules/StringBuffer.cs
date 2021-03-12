using System;
using Microsoft.Exchange.TextMatching;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200000F RID: 15
	internal sealed class StringBuffer : ITextInputBuffer
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002DE3 File Offset: 0x00000FE3
		public StringBuffer(string text)
		{
			this.text = text;
			this.index = -1;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002DFC File Offset: 0x00000FFC
		public int NextChar
		{
			get
			{
				if (this.index == -1)
				{
					this.index++;
					return 1;
				}
				if (string.IsNullOrEmpty(this.text) || this.index == this.text.Length)
				{
					return -1;
				}
				return (int)char.ToLowerInvariant(this.text[this.index++]);
			}
		}

		// Token: 0x0400001E RID: 30
		private string text;

		// Token: 0x0400001F RID: 31
		private int index;
	}
}
