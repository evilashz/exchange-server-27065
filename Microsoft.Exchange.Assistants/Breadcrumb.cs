using System;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000095 RID: 149
	internal sealed class Breadcrumb
	{
		// Token: 0x06000480 RID: 1152 RVA: 0x000184B8 File Offset: 0x000166B8
		public Breadcrumb(string message)
		{
			if (message.Length > 128)
			{
				this.message = message.Substring(0, 128);
				return;
			}
			this.message = message;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000184F2 File Offset: 0x000166F2
		public override string ToString()
		{
			return this.timeDropped + " " + this.message;
		}

		// Token: 0x0400028E RID: 654
		internal const int MaxCharactersPerLine = 128;

		// Token: 0x0400028F RID: 655
		private DateTime timeDropped = DateTime.UtcNow;

		// Token: 0x04000290 RID: 656
		private string message;
	}
}
