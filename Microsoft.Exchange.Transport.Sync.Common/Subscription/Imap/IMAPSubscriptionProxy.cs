using System;
using System.Collections.Generic;
using System.Security;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Imap
{
	// Token: 0x020000E6 RID: 230
	[Serializable]
	public sealed class IMAPSubscriptionProxy : PimSubscriptionProxy
	{
		// Token: 0x060006F0 RID: 1776 RVA: 0x00020CD7 File Offset: 0x0001EED7
		public IMAPSubscriptionProxy() : this(new IMAPAggregationSubscription())
		{
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00020CE4 File Offset: 0x0001EEE4
		internal IMAPSubscriptionProxy(IMAPAggregationSubscription subscription) : base(subscription)
		{
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x00020CED File Offset: 0x0001EEED
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x00020CFF File Offset: 0x0001EEFF
		public Fqdn IncomingServer
		{
			get
			{
				return ((IMAPAggregationSubscription)base.Subscription).IMAPServer;
			}
			set
			{
				((IMAPAggregationSubscription)base.Subscription).IMAPServer = value;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x00020D12 File Offset: 0x0001EF12
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x00020D24 File Offset: 0x0001EF24
		public int IncomingPort
		{
			get
			{
				return ((IMAPAggregationSubscription)base.Subscription).IMAPPort;
			}
			set
			{
				((IMAPAggregationSubscription)base.Subscription).IMAPPort = value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x00020D37 File Offset: 0x0001EF37
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x00020D50 File Offset: 0x0001EF50
		public string IncomingUserName
		{
			get
			{
				return base.RedactIfNeeded(((IMAPAggregationSubscription)base.Subscription).IMAPLogOnName, false);
			}
			set
			{
				((IMAPAggregationSubscription)base.Subscription).IMAPLogOnName = value;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x00020D63 File Offset: 0x0001EF63
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x00020D75 File Offset: 0x0001EF75
		public IMAPSecurityMechanism IncomingSecurity
		{
			get
			{
				return ((IMAPAggregationSubscription)base.Subscription).IMAPSecurity;
			}
			set
			{
				((IMAPAggregationSubscription)base.Subscription).IMAPSecurity = value;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00020D88 File Offset: 0x0001EF88
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x00020D9A File Offset: 0x0001EF9A
		public IMAPAuthenticationMechanism IncomingAuthentication
		{
			get
			{
				return ((IMAPAggregationSubscription)base.Subscription).IMAPAuthentication;
			}
			set
			{
				((IMAPAggregationSubscription)base.Subscription).IMAPAuthentication = value;
			}
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00020DB0 File Offset: 0x0001EFB0
		public static IMAPSubscriptionProxy ShallowCopy(IMAPSubscriptionProxy incoming)
		{
			return new IMAPSubscriptionProxy
			{
				IncomingServer = incoming.IncomingServer,
				IncomingPort = incoming.IncomingPort,
				IncomingUserName = incoming.IncomingUserName,
				IncomingSecurity = incoming.IncomingSecurity,
				IncomingAuthentication = incoming.IncomingAuthentication,
				Name = incoming.Name,
				DisplayName = incoming.DisplayName,
				EmailAddress = incoming.EmailAddress
			};
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00020E24 File Offset: 0x0001F024
		public override ValidationError[] Validate()
		{
			ICollection<ValidationError> collection = IMAPSubscriptionValidator.Validate(this);
			ValidationError[] array = new ValidationError[collection.Count];
			collection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00020E4D File Offset: 0x0001F04D
		public override void ResetChangeTracking()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00020E54 File Offset: 0x0001F054
		public void SetPassword(SecureString password)
		{
			base.Subscription.LogonPasswordSecured = password;
		}
	}
}
