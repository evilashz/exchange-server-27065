using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200019A RID: 410
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PendingRequestChannelLogEvent : ILogEvent
	{
		// Token: 0x06000ECE RID: 3790 RVA: 0x000390DD File Offset: 0x000372DD
		internal PendingRequestChannelLogEvent(string smtpAddress, string channelId)
		{
			this.smtpAddress = smtpAddress;
			this.channelId = channelId;
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x000390F3 File Offset: 0x000372F3
		public string EventId
		{
			get
			{
				return "PendingRequestChannel";
			}
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x000390FC File Offset: 0x000372FC
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			return new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("SmtpAddress", ExtensibleLogger.FormatPIIValue(this.smtpAddress)),
				new KeyValuePair<string, object>("ChannelId", this.channelId)
			};
		}

		// Token: 0x040008FD RID: 2301
		private readonly string smtpAddress;

		// Token: 0x040008FE RID: 2302
		private readonly string channelId;
	}
}
