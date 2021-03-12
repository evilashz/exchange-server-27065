using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000AB1 RID: 2737
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LicensingRpcParameters : RpcParameters
	{
		// Token: 0x17001B8C RID: 7052
		// (get) Token: 0x060063EE RID: 25582 RVA: 0x001A773C File Offset: 0x001A593C
		public RmsClientManagerContext ClientManagerContext
		{
			get
			{
				if (this.clientManagerContext == null)
				{
					OrganizationId organizationId = base.GetParameterValue("OrgId") as OrganizationId;
					if (organizationId == null)
					{
						throw new ArgumentNullException("orgId");
					}
					object parameterValue = base.GetParameterValue("ContextId");
					RmsClientManagerContext.ContextId contextId = (RmsClientManagerContext.ContextId)parameterValue;
					string contextValue = base.GetParameterValue("ContextValue") as string;
					parameterValue = base.GetParameterValue("TransactioniId");
					Guid transactionId = (Guid)parameterValue;
					Guid externalDirectoryOrgId = (Guid)base.GetParameterValue("ExternalDirectoryOrgId");
					this.clientManagerContext = new RmsClientManagerContext(organizationId, contextId, contextValue, transactionId, externalDirectoryOrgId);
				}
				return this.clientManagerContext;
			}
		}

		// Token: 0x060063EF RID: 25583 RVA: 0x001A77DB File Offset: 0x001A59DB
		public LicensingRpcParameters(byte[] data) : base(data)
		{
		}

		// Token: 0x060063F0 RID: 25584 RVA: 0x001A77E4 File Offset: 0x001A59E4
		public LicensingRpcParameters(RmsClientManagerContext rmsClientManagerContext)
		{
			if (rmsClientManagerContext == null)
			{
				throw new ArgumentNullException("rmsClientManagerContext");
			}
			if (rmsClientManagerContext.OrgId == null)
			{
				throw new ArgumentNullException("rmsClientManagerContext.OrgId");
			}
			base.SetParameterValue("OrgId", rmsClientManagerContext.OrgId);
			base.SetParameterValue("ContextId", rmsClientManagerContext.ContextID);
			base.SetParameterValue("ContextValue", rmsClientManagerContext.ContextValue);
			base.SetParameterValue("TransactioniId", rmsClientManagerContext.TransactionId);
			base.SetParameterValue("ExternalDirectoryOrgId", rmsClientManagerContext.ExternalDirectoryOrgId);
		}

		// Token: 0x040038A5 RID: 14501
		private const string OrgIdParameterName = "OrgId";

		// Token: 0x040038A6 RID: 14502
		private const string ContextIdParameterName = "ContextId";

		// Token: 0x040038A7 RID: 14503
		private const string ContextValueParameterName = "ContextValue";

		// Token: 0x040038A8 RID: 14504
		private const string TransactionIdParameterName = "TransactioniId";

		// Token: 0x040038A9 RID: 14505
		private const string ExternalDirectoryOrgIdParameterName = "ExternalDirectoryOrgId";

		// Token: 0x040038AA RID: 14506
		private RmsClientManagerContext clientManagerContext;
	}
}
