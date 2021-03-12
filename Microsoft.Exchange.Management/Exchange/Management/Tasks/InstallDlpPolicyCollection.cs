using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002D4 RID: 724
	[Cmdlet("Install", "DlpPolicyCollection")]
	public sealed class InstallDlpPolicyCollection : NewMultitenancySystemConfigurationObjectTask<ADComplianceProgramCollection>
	{
		// Token: 0x06001948 RID: 6472 RVA: 0x000710F4 File Offset: 0x0006F2F4
		protected override IConfigurable PrepareDataObject()
		{
			ADComplianceProgramCollection adcomplianceProgramCollection = (ADComplianceProgramCollection)base.PrepareDataObject();
			ADObjectId adobjectId = base.CurrentOrgContainerId;
			adobjectId = adobjectId.GetChildId("Transport Settings");
			adobjectId = adobjectId.GetChildId("Rules");
			adobjectId = adobjectId.GetChildId(base.Name);
			adcomplianceProgramCollection.SetId(adobjectId);
			return adcomplianceProgramCollection;
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x00071141 File Offset: 0x0006F341
		protected override void InternalProcessRecord()
		{
			if (base.DataSession.Read<ADComplianceProgramCollection>(this.DataObject.Id) == null)
			{
				base.InternalProcessRecord();
				return;
			}
			base.WriteVerbose(Strings.RuleCollectionAlreadyExists(base.Name));
		}
	}
}
