using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.MailboxReplicationService.Upgrade14to15;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200003C RID: 60
	[Cmdlet("Get", "UpgradeWorkItem", DefaultParameterSetName = "regularQuery")]
	public class GetUpgradeWorkItem : SymphonyTaskBase
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000DADC File Offset: 0x0000BCDC
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x0000DAE4 File Offset: 0x0000BCE4
		[Parameter(Mandatory = false, ParameterSetName = "regularQuery")]
		public string ForestName { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000DAED File Offset: 0x0000BCED
		// (set) Token: 0x060002EB RID: 747 RVA: 0x0000DAF5 File Offset: 0x0000BCF5
		[Parameter(Mandatory = false, ParameterSetName = "regularQuery")]
		public Unlimited<int> ResultSize
		{
			get
			{
				return this.resultSizeField;
			}
			set
			{
				this.resultSizeField = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000DAFE File Offset: 0x0000BCFE
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000DB06 File Offset: 0x0000BD06
		[Parameter(Mandatory = false, ParameterSetName = "regularQuery")]
		public WorkItemStatus[] Status { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000DB0F File Offset: 0x0000BD0F
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000DB17 File Offset: 0x0000BD17
		[Parameter(Mandatory = false, ParameterSetName = "regularQuery")]
		public string TenantTier { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000DB20 File Offset: 0x0000BD20
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x0000DB28 File Offset: 0x0000BD28
		[Parameter(Mandatory = false, ParameterSetName = "regularQuery")]
		public string Type { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000DB31 File Offset: 0x0000BD31
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x0000DB39 File Offset: 0x0000BD39
		[Parameter(Mandatory = true, ParameterSetName = "tenantQuery")]
		[Parameter(Mandatory = false, ParameterSetName = "WorkItemIDQUery")]
		public Guid Tenant { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000DB42 File Offset: 0x0000BD42
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000DB4A File Offset: 0x0000BD4A
		[Parameter(Mandatory = true, ParameterSetName = "WorkItemIDQUery")]
		public string WorkItemID { get; set; }

		// Token: 0x060002F6 RID: 758 RVA: 0x0000DC08 File Offset: 0x0000BE08
		protected override void InternalProcessRecord()
		{
			string parameterSetName;
			if ((parameterSetName = base.ParameterSetName) != null)
			{
				if (!(parameterSetName == "regularQuery"))
				{
					if (!(parameterSetName == "tenantQuery"))
					{
						if (!(parameterSetName == "WorkItemIDQUery"))
						{
							goto IL_247;
						}
						goto IL_208;
					}
				}
				else
				{
					WorkItemStatus[] array = base.UserSpecifiedParameters.Contains("Status") ? this.Status : ((WorkItemStatus[])Enum.GetValues(typeof(WorkItemStatus)));
					using (ProxyWrapper<UpgradeHandlerClient, IUpgradeHandler> workloadClient = new ProxyWrapper<UpgradeHandlerClient, IUpgradeHandler>(base.WorkloadUri, base.Certificate))
					{
						WorkItemStatus[] array2 = array;
						for (int i = 0; i < array2.Length; i++)
						{
							GetUpgradeWorkItem.<>c__DisplayClass4 CS$<>8__locals2 = new GetUpgradeWorkItem.<>c__DisplayClass4();
							CS$<>8__locals2.status = array2[i];
							WorkItemQueryResult queryResult = new WorkItemQueryResult();
							bool flag;
							do
							{
								workloadClient.CallSymphony(delegate
								{
									queryResult = workloadClient.Proxy.QueryWorkItems(this.ForestName, this.TenantTier, this.Type, CS$<>8__locals2.status, 1000, queryResult.Bookmark);
								}, base.WorkloadUri.ToString());
								flag = this.WriteWorkitems(queryResult.WorkItems);
							}
							while (queryResult.HasMoreResults && !flag);
							if (flag)
							{
								break;
							}
						}
						return;
					}
				}
				using (ProxyWrapper<UpgradeHandlerClient, IUpgradeHandler> workloadClient = new ProxyWrapper<UpgradeHandlerClient, IUpgradeHandler>(base.WorkloadUri, base.Certificate))
				{
					if (this.Tenant == Guid.Empty)
					{
						throw new InvalidTenantGuidException(this.Tenant.ToString());
					}
					WorkItemInfo[] retrieved = null;
					workloadClient.CallSymphony(delegate
					{
						retrieved = workloadClient.Proxy.QueryTenantWorkItems(this.Tenant);
					}, base.WorkloadUri.ToString());
					this.WriteWorkitems(retrieved);
					return;
				}
				IL_208:
				WorkItemInfo workItem;
				if (this.Tenant != Guid.Empty)
				{
					workItem = base.GetWorkitemByIdAndTenantId(this.WorkItemID, this.Tenant);
				}
				else
				{
					workItem = base.GetWorkItemById(this.WorkItemID);
				}
				this.WriteWorkItem(workItem);
				return;
			}
			IL_247:
			throw new ArgumentException("Invalid parameter set.");
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000DE84 File Offset: 0x0000C084
		private bool WriteWorkitems(IEnumerable<WorkItemInfo> workItems)
		{
			foreach (WorkItemInfo workItem in workItems)
			{
				if (!this.ResultSize.IsUnlimited && this.writtenCount >= this.ResultSize.Value)
				{
					this.WriteWarning(Strings.WarningMoreResultsAvailable);
					return true;
				}
				this.WriteWorkItem(workItem);
				this.writtenCount++;
			}
			return false;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000DF14 File Offset: 0x0000C114
		private void WriteWorkItem(WorkItemInfo workItem)
		{
			UpgradeWorkItem sendToPipeline = new UpgradeWorkItem(workItem);
			base.WriteObject(sendToPipeline);
		}

		// Token: 0x04000106 RID: 262
		private const int PageSize = 1000;

		// Token: 0x04000107 RID: 263
		private const string RegularQuery = "regularQuery";

		// Token: 0x04000108 RID: 264
		private const string TenantQuery = "tenantQuery";

		// Token: 0x04000109 RID: 265
		private const string WorkItemIDQuery = "WorkItemIDQUery";

		// Token: 0x0400010A RID: 266
		private int writtenCount;

		// Token: 0x0400010B RID: 267
		private Unlimited<int> resultSizeField = 100;
	}
}
