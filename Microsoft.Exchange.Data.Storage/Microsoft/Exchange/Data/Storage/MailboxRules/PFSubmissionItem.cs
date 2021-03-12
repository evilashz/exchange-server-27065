using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BF7 RID: 3063
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PFSubmissionItem : ISubmissionItem, IDisposable
	{
		// Token: 0x06006D3C RID: 27964 RVA: 0x001D266F File Offset: 0x001D086F
		public PFSubmissionItem(PFRuleEvaluationContext context, MessageItem item)
		{
			this.context = context;
			this.item = item;
			this.submissionTime = DateTime.UtcNow;
		}

		// Token: 0x17001DCA RID: 7626
		// (get) Token: 0x06006D3D RID: 27965 RVA: 0x001D2690 File Offset: 0x001D0890
		public string SourceServerFqdn
		{
			get
			{
				return this.context.LocalServerFqdn;
			}
		}

		// Token: 0x17001DCB RID: 7627
		// (get) Token: 0x06006D3E RID: 27966 RVA: 0x001D269D File Offset: 0x001D089D
		public IPAddress SourceServerNetworkAddress
		{
			get
			{
				return this.context.LocalServerNetworkAddress;
			}
		}

		// Token: 0x17001DCC RID: 7628
		// (get) Token: 0x06006D3F RID: 27967 RVA: 0x001D26AA File Offset: 0x001D08AA
		public DateTime OriginalCreateTime
		{
			get
			{
				return this.submissionTime;
			}
		}

		// Token: 0x06006D40 RID: 27968 RVA: 0x001D26B2 File Offset: 0x001D08B2
		public void Submit()
		{
			this.item.Send(SubmitMessageFlags.IgnoreSendAsRight);
		}

		// Token: 0x06006D41 RID: 27969 RVA: 0x001D26C0 File Offset: 0x001D08C0
		public void Submit(ProxyAddress sender, IEnumerable<Participant> recipients)
		{
			Result<ADRawEntry> result = this.context.RecipientCache.FindAndCacheRecipient(sender);
			if (result.Data != null)
			{
				Participant sender2 = new Participant(result.Data);
				this.item.Sender = sender2;
				foreach (Participant participant in recipients)
				{
					Recipient recipient = this.item.Recipients.Add(participant, RecipientItemType.Bcc);
					recipient[ItemSchema.Responsibility] = true;
				}
				this.item.Send(SubmitMessageFlags.IgnoreSendAsRight);
				return;
			}
			if (ProviderError.NotFound == result.Error)
			{
				this.context.TraceError<ProxyAddress>("Public folder rule submission: Sender '{0}' doesn't exist in AD.", sender);
			}
			else
			{
				this.context.TraceError<ProxyAddress, ProviderError>("Public folder rule submission: Sender '{0}' look up failed: {1}", sender, result.Error);
			}
			this.context.TraceDebug("Public folder rule submission: Message will not be submitted.");
		}

		// Token: 0x06006D42 RID: 27970 RVA: 0x001D27B8 File Offset: 0x001D09B8
		public void Dispose()
		{
		}

		// Token: 0x04003E2A RID: 15914
		private PFRuleEvaluationContext context;

		// Token: 0x04003E2B RID: 15915
		private MessageItem item;

		// Token: 0x04003E2C RID: 15916
		private DateTime submissionTime;
	}
}
