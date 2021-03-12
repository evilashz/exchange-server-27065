using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200021A RID: 538
	internal class UnroutableDestination : RoutingDestination
	{
		// Token: 0x060017C4 RID: 6084 RVA: 0x000608FF File Offset: 0x0005EAFF
		public UnroutableDestination(MailRecipientType destinationType, string identity, RoutingNextHop nextHop) : base(RouteInfo.CreateForUnroutableDestination(identity, nextHop))
		{
			this.destinationType = destinationType;
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x00060915 File Offset: 0x0005EB15
		public override MailRecipientType DestinationType
		{
			get
			{
				return this.destinationType;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x0006091D File Offset: 0x0005EB1D
		public override string StringIdentity
		{
			get
			{
				return base.RouteInfo.DestinationName;
			}
		}

		// Token: 0x04000B9E RID: 2974
		private MailRecipientType destinationType;
	}
}
