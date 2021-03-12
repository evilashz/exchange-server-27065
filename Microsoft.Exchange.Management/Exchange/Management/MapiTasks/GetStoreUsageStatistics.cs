using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mapi;

namespace Microsoft.Exchange.Management.MapiTasks
{
	// Token: 0x02000486 RID: 1158
	[Cmdlet("Get", "StoreUsageStatistics", DefaultParameterSetName = "Identity")]
	public sealed class GetStoreUsageStatistics : GetStatisticsBase<GeneralMailboxIdParameter, MailboxResourceMonitor, MailboxResourceMonitor>
	{
		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x060028FB RID: 10491 RVA: 0x000A20A6 File Offset: 0x000A02A6
		// (set) Token: 0x060028FC RID: 10492 RVA: 0x000A20C0 File Offset: 0x000A02C0
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string Filter
		{
			get
			{
				return (string)base.Fields["Filter"];
			}
			set
			{
				MonadFilter monadFilter = new MonadFilter(value, this, ObjectSchema.GetInstance<MailboxResourceMonitorSchema>());
				this.inputFilter = monadFilter.InnerFilter;
				base.OptionalIdentityData.AdditionalFilter = monadFilter.InnerFilter;
				base.Fields["Filter"] = value;
			}
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x000A2108 File Offset: 0x000A0308
		protected override void WriteResult(IConfigurable dataObject)
		{
			MailboxResourceMonitor mailboxResourceMonitor = (MailboxResourceMonitor)dataObject;
			if (this.inputFilter != null)
			{
				if (OpathFilterEvaluator.FilterMatches(this.inputFilter, mailboxResourceMonitor))
				{
					base.WriteResult(dataObject);
					return;
				}
			}
			else
			{
				base.WriteResult(mailboxResourceMonitor);
			}
		}

		// Token: 0x04001E38 RID: 7736
		private QueryFilter inputFilter;
	}
}
