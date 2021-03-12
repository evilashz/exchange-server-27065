using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002AA RID: 682
	[DataContract]
	public class PimSubscriptionRow : BaseRow
	{
		// Token: 0x06002BAE RID: 11182 RVA: 0x000882AC File Offset: 0x000864AC
		public PimSubscriptionRow(PimSubscriptionProxy subscription) : base(((AggregationSubscriptionIdentity)subscription.Identity).ToIdentity(subscription.EmailAddress.ToString()), subscription)
		{
			this.PimSubscriptionProxy = subscription;
		}

		// Token: 0x17001D88 RID: 7560
		// (get) Token: 0x06002BAF RID: 11183 RVA: 0x000882EB File Offset: 0x000864EB
		// (set) Token: 0x06002BB0 RID: 11184 RVA: 0x000882F3 File Offset: 0x000864F3
		public PimSubscriptionProxy PimSubscriptionProxy { get; private set; }

		// Token: 0x17001D89 RID: 7561
		// (get) Token: 0x06002BB1 RID: 11185 RVA: 0x000882FC File Offset: 0x000864FC
		// (set) Token: 0x06002BB2 RID: 11186 RVA: 0x00088322 File Offset: 0x00086522
		[DataMember]
		public string EmailAddress
		{
			get
			{
				return this.PimSubscriptionProxy.EmailAddress.ToString();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001D8A RID: 7562
		// (get) Token: 0x06002BB3 RID: 11187 RVA: 0x00088329 File Offset: 0x00086529
		protected bool ShowWarningIcon
		{
			get
			{
				return this.PimSubscriptionProxy.IsWarningStatus || this.PimSubscriptionProxy.IsErrorStatus;
			}
		}

		// Token: 0x17001D8B RID: 7563
		// (get) Token: 0x06002BB4 RID: 11188 RVA: 0x00088345 File Offset: 0x00086545
		// (set) Token: 0x06002BB5 RID: 11189 RVA: 0x0008835A File Offset: 0x0008655A
		[DataMember]
		public string StatusIcon
		{
			get
			{
				if (!this.ShowWarningIcon)
				{
					return string.Empty;
				}
				return "Warning.gif";
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001D8C RID: 7564
		// (get) Token: 0x06002BB6 RID: 11190 RVA: 0x00088361 File Offset: 0x00086561
		// (set) Token: 0x06002BB7 RID: 11191 RVA: 0x0008837B File Offset: 0x0008657B
		[DataMember]
		public string StatusIconAlt
		{
			get
			{
				if (!this.ShowWarningIcon)
				{
					return string.Empty;
				}
				return OwaOptionStrings.WarningAlt;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001D8D RID: 7565
		// (get) Token: 0x06002BB8 RID: 11192 RVA: 0x00088382 File Offset: 0x00086582
		// (set) Token: 0x06002BB9 RID: 11193 RVA: 0x0008838F File Offset: 0x0008658F
		[DataMember]
		public string StatusDescription
		{
			get
			{
				return this.PimSubscriptionProxy.StatusDescription;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001D8E RID: 7566
		// (get) Token: 0x06002BBA RID: 11194 RVA: 0x00088396 File Offset: 0x00086596
		// (set) Token: 0x06002BBB RID: 11195 RVA: 0x000883AD File Offset: 0x000865AD
		[DataMember]
		public string SubscriptionType
		{
			get
			{
				return this.PimSubscriptionProxy.SubscriptionType.ToString();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001D8F RID: 7567
		// (get) Token: 0x06002BBC RID: 11196 RVA: 0x000883B4 File Offset: 0x000865B4
		// (set) Token: 0x06002BBD RID: 11197 RVA: 0x000883C1 File Offset: 0x000865C1
		[DataMember]
		public bool IsValid
		{
			get
			{
				return this.PimSubscriptionProxy.IsValid;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001D90 RID: 7568
		// (get) Token: 0x06002BBE RID: 11198 RVA: 0x000883C8 File Offset: 0x000865C8
		// (set) Token: 0x06002BBF RID: 11199 RVA: 0x000883D8 File Offset: 0x000865D8
		[DataMember]
		public bool SendAsEnabled
		{
			get
			{
				return this.PimSubscriptionProxy.SendAsState == SendAsState.Enabled;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001D91 RID: 7569
		// (get) Token: 0x06002BC0 RID: 11200 RVA: 0x000883DF File Offset: 0x000865DF
		// (set) Token: 0x06002BC1 RID: 11201 RVA: 0x000883F6 File Offset: 0x000865F6
		[DataMember]
		public string VerificationEmailState
		{
			get
			{
				return this.PimSubscriptionProxy.VerificationEmailState.ToString();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001D92 RID: 7570
		// (get) Token: 0x06002BC2 RID: 11202 RVA: 0x00088400 File Offset: 0x00086600
		// (set) Token: 0x06002BC3 RID: 11203 RVA: 0x00088456 File Offset: 0x00086656
		public string VerificationFeedbackString
		{
			get
			{
				string result = null;
				switch (this.PimSubscriptionProxy.VerificationEmailState)
				{
				case Microsoft.Exchange.Transport.Sync.Common.Subscription.VerificationEmailState.EmailSent:
					result = OwaOptionStrings.VerificationEmailSucceeded(this.EmailAddress);
					break;
				case Microsoft.Exchange.Transport.Sync.Common.Subscription.VerificationEmailState.EmailFailedToSend:
					result = OwaOptionStrings.VerificationEmailFailedToSend(this.EmailAddress);
					break;
				}
				return result;
			}
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}
