using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000032 RID: 50
	[Cmdlet("Remove", "Subscription", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveSubscription : RemoveSubscriptionBase<PimSubscriptionProxy>
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00009EE5 File Offset: 0x000080E5
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00009EFC File Offset: 0x000080FC
		[Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = "Identity")]
		public MailboxIdParameter Mailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Mailbox"];
			}
			set
			{
				base.Fields["Mailbox"] = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00009F0F File Offset: 0x0000810F
		protected override AggregationType AggregationType
		{
			get
			{
				return AggregationType.Migration;
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00009F13 File Offset: 0x00008113
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is FailedDeleteAggregationSubscriptionException;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00009F29 File Offset: 0x00008129
		protected override MailboxIdParameter GetMailboxIdParameter()
		{
			if (this.Mailbox == null)
			{
				this.Mailbox = base.GetMailboxIdParameter();
			}
			return this.Mailbox;
		}
	}
}
