using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200000E RID: 14
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class RetryableWorkQueueEventArgs : EventArgs
	{
		// Token: 0x060000FA RID: 250 RVA: 0x00006AEF File Offset: 0x00004CEF
		internal RetryableWorkQueueEventArgs(int difference)
		{
			this.difference = difference;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00006AFE File Offset: 0x00004CFE
		internal int Difference
		{
			get
			{
				return this.difference;
			}
		}

		// Token: 0x04000099 RID: 153
		internal static readonly RetryableWorkQueueEventArgs IncrementByOneEventArgs = new RetryableWorkQueueEventArgs(1);

		// Token: 0x0400009A RID: 154
		internal static readonly RetryableWorkQueueEventArgs DecrementByOneEventArgs = new RetryableWorkQueueEventArgs(-1);

		// Token: 0x0400009B RID: 155
		private int difference;
	}
}
