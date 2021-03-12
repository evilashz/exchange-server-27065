using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AD5 RID: 2773
	[Cmdlet("Get", "MSERVEntry", DefaultParameterSetName = "ExternalDirectoryOrganizationIdParameterSet")]
	public sealed class GetMSERVEntry : ManageMSERVEntryBase
	{
		// Token: 0x0600628C RID: 25228 RVA: 0x0019B488 File Offset: 0x00199688
		protected override void InternalProcessRecord()
		{
			MSERVEntry sendToPipeline = new MSERVEntry();
			if (base.Fields.IsModified("ExternalDirectoryOrganizationId"))
			{
				sendToPipeline = base.ProcessExternalOrgIdParameter((string address, int partnerId) => base.ReadMservEntry(address));
			}
			else if (base.Fields.IsModified("DomainName"))
			{
				sendToPipeline = base.ProcessDomainNameParameter((string address, int partnerId) => base.ReadMservEntry(address));
			}
			else
			{
				sendToPipeline = base.ProcessAddressParameter((string address, int partnerId) => base.ReadMservEntry(address));
			}
			base.WriteObject(sendToPipeline);
		}
	}
}
