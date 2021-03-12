using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x0200034D RID: 845
	[Cmdlet("Get", "MSOFullSyncObjectRequest", DefaultParameterSetName = "ServiceInstanceIdParameterSet")]
	public sealed class GetMSOFullSyncObjectRequest : GetTaskBase<FullSyncObjectRequest>
	{
		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06001D30 RID: 7472 RVA: 0x000810AD File Offset: 0x0007F2AD
		// (set) Token: 0x06001D31 RID: 7473 RVA: 0x000810C4 File Offset: 0x0007F2C4
		[Parameter(ParameterSetName = "IdentityParameterSet", Mandatory = true)]
		public SyncObjectId Identity
		{
			get
			{
				return (SyncObjectId)base.Fields["SyncObjectIdParameter"];
			}
			set
			{
				base.Fields["SyncObjectIdParameter"] = value;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06001D32 RID: 7474 RVA: 0x000810D7 File Offset: 0x0007F2D7
		// (set) Token: 0x06001D33 RID: 7475 RVA: 0x000810EE File Offset: 0x0007F2EE
		[Parameter(ParameterSetName = "ServiceInstanceIdParameterSet", Mandatory = false)]
		public ServiceInstanceId ServiceInstanceId
		{
			get
			{
				return (ServiceInstanceId)base.Fields["ServiceInstanceIdParameter"];
			}
			set
			{
				base.Fields["ServiceInstanceIdParameter"] = value;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06001D34 RID: 7476 RVA: 0x00081104 File Offset: 0x0007F304
		protected override QueryFilter InternalFilter
		{
			get
			{
				if (base.ParameterSetName == "IdentityParameterSet")
				{
					return new ComparisonFilter(ComparisonOperator.Equal, SimpleProviderObjectSchema.Identity, this.Identity);
				}
				if (this.ServiceInstanceId != null)
				{
					return new ComparisonFilter(ComparisonOperator.Equal, FullSyncObjectRequestSchema.ServiceInstanceId, this.ServiceInstanceId.InstanceId);
				}
				return null;
			}
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x00081155 File Offset: 0x0007F355
		protected override IConfigDataProvider CreateSession()
		{
			return new FullSyncObjectRequestDataProvider(true, (this.ServiceInstanceId != null) ? this.ServiceInstanceId.InstanceId : null);
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x00081173 File Offset: 0x0007F373
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is RidMasterConfigException;
		}

		// Token: 0x04001888 RID: 6280
		private const string SyncObjectIdParameter = "SyncObjectIdParameter";

		// Token: 0x04001889 RID: 6281
		private const string ServiceInstanceIdParameter = "ServiceInstanceIdParameter";

		// Token: 0x0400188A RID: 6282
		private const string ServiceInstanceIdParameterSet = "ServiceInstanceIdParameterSet";

		// Token: 0x0400188B RID: 6283
		private const string IdentityParameterSet = "IdentityParameterSet";
	}
}
