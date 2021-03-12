using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002D9 RID: 729
	[Cmdlet("install", "RuleCollection")]
	public sealed class InstallRuleCollection : NewMultitenancySystemConfigurationObjectTask<TransportRuleCollection>
	{
		// Token: 0x06001960 RID: 6496 RVA: 0x000715A4 File Offset: 0x0006F7A4
		protected override IConfigurable PrepareDataObject()
		{
			TransportRuleCollection transportRuleCollection = (TransportRuleCollection)base.PrepareDataObject();
			ADObjectId adobjectId = base.CurrentOrgContainerId;
			adobjectId = adobjectId.GetChildId("Transport Settings");
			adobjectId = adobjectId.GetChildId("Rules");
			adobjectId = adobjectId.GetChildId(base.Name);
			transportRuleCollection.SetId(adobjectId);
			return transportRuleCollection;
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x000715F1 File Offset: 0x0006F7F1
		protected override void InternalProcessRecord()
		{
			if (base.DataSession.Read<TransportRuleCollection>(this.DataObject.Id) == null)
			{
				base.InternalProcessRecord();
				return;
			}
			base.WriteVerbose(Strings.RuleCollectionAlreadyExists(base.Name));
		}
	}
}
