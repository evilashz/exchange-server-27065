using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002EC RID: 748
	[Cmdlet("Uninstall", "OrganizationalUnit", SupportsShouldProcess = true)]
	public sealed class UninstallOrganizationalUnitTask : RemoveSystemConfigurationObjectTask<OrganizationalUnitIdParameter, ADOrganizationalUnit>
	{
		// Token: 0x060019C0 RID: 6592 RVA: 0x00072BDC File Offset: 0x00070DDC
		protected override IConfigDataProvider CreateSession()
		{
			IConfigurationSession configurationSession = (IConfigurationSession)base.CreateSession();
			configurationSession.UseConfigNC = false;
			return configurationSession;
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x00072C10 File Offset: 0x00070E10
		protected override void InternalProcessRecord()
		{
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			configurationSession.DeleteTree(base.DataObject, delegate(ADTreeDeleteNotFinishedException de)
			{
				if (de != null)
				{
					base.WriteVerbose(de.LocalizedString);
				}
			});
		}
	}
}
