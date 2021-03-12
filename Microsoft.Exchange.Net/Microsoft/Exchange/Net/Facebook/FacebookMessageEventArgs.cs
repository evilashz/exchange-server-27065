using System;
using System.ServiceModel.Channels;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Facebook
{
	// Token: 0x02000722 RID: 1826
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FacebookMessageEventArgs : EventArgs
	{
		// Token: 0x060022A9 RID: 8873 RVA: 0x0004743B File Offset: 0x0004563B
		public FacebookMessageEventArgs(Message messageTransferred)
		{
			ArgumentValidator.ThrowIfNull("MessageTransferred", messageTransferred);
			this.messageTransferred = messageTransferred;
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x060022AA RID: 8874 RVA: 0x00047455 File Offset: 0x00045655
		public Message MessageTransferred
		{
			get
			{
				return this.messageTransferred;
			}
		}

		// Token: 0x040020EA RID: 8426
		private Message messageTransferred;
	}
}
