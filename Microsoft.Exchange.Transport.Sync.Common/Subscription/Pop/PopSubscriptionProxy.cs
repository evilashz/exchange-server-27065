using System;
using System.Collections.Generic;
using System.Security;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop
{
	// Token: 0x020000F0 RID: 240
	[Serializable]
	public sealed class PopSubscriptionProxy : PimSubscriptionProxy
	{
		// Token: 0x06000727 RID: 1831 RVA: 0x00021239 File Offset: 0x0001F439
		public PopSubscriptionProxy() : this(new PopAggregationSubscription())
		{
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00021246 File Offset: 0x0001F446
		internal PopSubscriptionProxy(PopAggregationSubscription subscription) : base(subscription)
		{
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x00021256 File Offset: 0x0001F456
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x00021268 File Offset: 0x0001F468
		public Fqdn IncomingServer
		{
			get
			{
				return ((PopAggregationSubscription)base.Subscription).PopServer;
			}
			set
			{
				((PopAggregationSubscription)base.Subscription).PopServer = value;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0002127B File Offset: 0x0001F47B
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x0002128D File Offset: 0x0001F48D
		public int IncomingPort
		{
			get
			{
				return ((PopAggregationSubscription)base.Subscription).PopPort;
			}
			set
			{
				((PopAggregationSubscription)base.Subscription).PopPort = value;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x000212A0 File Offset: 0x0001F4A0
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x000212B9 File Offset: 0x0001F4B9
		public string IncomingUserName
		{
			get
			{
				return base.RedactIfNeeded(((PopAggregationSubscription)base.Subscription).PopLogonName, false);
			}
			set
			{
				((PopAggregationSubscription)base.Subscription).PopLogonName = value;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x000212CC File Offset: 0x0001F4CC
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x000212DE File Offset: 0x0001F4DE
		public SecurityMechanism IncomingSecurity
		{
			get
			{
				return ((PopAggregationSubscription)base.Subscription).PopSecurity;
			}
			set
			{
				((PopAggregationSubscription)base.Subscription).PopSecurity = value;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x000212F1 File Offset: 0x0001F4F1
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x00021303 File Offset: 0x0001F503
		public AuthenticationMechanism IncomingAuthentication
		{
			get
			{
				return ((PopAggregationSubscription)base.Subscription).PopAuthentication;
			}
			set
			{
				((PopAggregationSubscription)base.Subscription).PopAuthentication = value;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x00021316 File Offset: 0x0001F516
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x00021328 File Offset: 0x0001F528
		public bool LeaveOnServer
		{
			get
			{
				return ((PopAggregationSubscription)base.Subscription).ShouldLeaveOnServer;
			}
			set
			{
				((PopAggregationSubscription)base.Subscription).ShouldLeaveOnServer = value;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0002133B File Offset: 0x0001F53B
		// (set) Token: 0x06000736 RID: 1846 RVA: 0x00021343 File Offset: 0x0001F543
		public bool ServerSupportsMirroring
		{
			get
			{
				return this.serverSupportsMirroring;
			}
			set
			{
				this.serverSupportsMirroring = value;
			}
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0002134C File Offset: 0x0001F54C
		public override ValidationError[] Validate()
		{
			ICollection<ValidationError> collection = PopSubscriptionValidator.Validate(this);
			ValidationError[] array = new ValidationError[collection.Count];
			collection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00021375 File Offset: 0x0001F575
		public override void ResetChangeTracking()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0002137C File Offset: 0x0001F57C
		public void SetPassword(SecureString password)
		{
			base.Subscription.LogonPasswordSecured = password;
		}

		// Token: 0x040003DF RID: 991
		private bool serverSupportsMirroring = true;
	}
}
