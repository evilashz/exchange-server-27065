using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009F3 RID: 2547
	[Cmdlet("Remove", "FederationTrust", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveFederationTrust : RemoveSystemConfigurationObjectTask<FederationTrustIdParameter, FederationTrust>
	{
		// Token: 0x17001B44 RID: 6980
		// (get) Token: 0x06005B1F RID: 23327 RVA: 0x0017D3BC File Offset: 0x0017B5BC
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveFederationTrust(base.DataObject.Name);
			}
		}

		// Token: 0x06005B20 RID: 23328 RVA: 0x0017D3D0 File Offset: 0x0017B5D0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			FederatedOrganizationId[] array = this.IsAnyoneRelyingOnThisTrust();
			if (array == null || array.Length == 0)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (FederatedOrganizationId federatedOrganizationId in array)
			{
				stringBuilder.Append(federatedOrganizationId.DistinguishedName + "\n");
			}
			base.WriteError(new OrgsStillUsingThisTrustException(base.DataObject.Name, stringBuilder.ToString()), ErrorCategory.InvalidOperation, base.DataObject.Identity);
		}

		// Token: 0x06005B21 RID: 23329 RVA: 0x0017D460 File Offset: 0x0017B660
		private FederatedOrganizationId[] IsAnyoneRelyingOnThisTrust()
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, FederatedOrganizationIdSchema.DelegationTrustLink, base.DataObject.Id);
			return base.GlobalConfigSession.Find<FederatedOrganizationId>(null, QueryScope.SubTree, filter, null, 0);
		}
	}
}
