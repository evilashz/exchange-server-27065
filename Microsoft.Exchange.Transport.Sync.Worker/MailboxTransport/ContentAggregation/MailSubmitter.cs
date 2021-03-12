using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200000C RID: 12
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MailSubmitter
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060000E8 RID: 232 RVA: 0x0000685C File Offset: 0x00004A5C
		// (remove) Token: 0x060000E9 RID: 233 RVA: 0x00006894 File Offset: 0x00004A94
		private event EventHandler<EventArgs> AggregatedMailSubmitted;

		// Token: 0x060000EA RID: 234
		public abstract TransportMailItem CreateNewMail();

		// Token: 0x060000EB RID: 235
		public abstract Stream GetWriteStream(TransportMailItem mailItem);

		// Token: 0x060000EC RID: 236
		public abstract Exception SubmitMail(string componentId, SyncLogSession syncLogSession, TransportMailItem mailItem, ISyncEmail syncEmail, IList<string> recipients, string deliveryFolder, Guid subscriptionGuid, string cloudId, string cloudVersion, DateTime originalReceivedTime, MsgTrackReceiveInfo msgTrackInfo);

		// Token: 0x060000ED RID: 237 RVA: 0x000068CC File Offset: 0x00004ACC
		public void EnsureRegisteredEventHandler(EventHandler<EventArgs> aggregatedMailSubmitted)
		{
			if (this.AggregatedMailSubmitted == null)
			{
				lock (this.syncObject)
				{
					if (this.AggregatedMailSubmitted == null)
					{
						this.AggregatedMailSubmitted += aggregatedMailSubmitted;
					}
				}
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006920 File Offset: 0x00004B20
		protected void TriggerMailSubmittedEvent(object sender, EventArgs eventArgs)
		{
			EventHandler<EventArgs> aggregatedMailSubmitted = this.AggregatedMailSubmitted;
			if (aggregatedMailSubmitted != null)
			{
				aggregatedMailSubmitted(sender, eventArgs);
			}
		}

		// Token: 0x0400008F RID: 143
		private object syncObject = new object();
	}
}
