using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AD7 RID: 2775
	[Cmdlet("Remove", "MSERVEntry", DefaultParameterSetName = "ExternalDirectoryOrganizationIdParameterSet", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveMSERVEntry : ManageMSERVEntryBase
	{
		// Token: 0x17001DEB RID: 7659
		// (get) Token: 0x0600629C RID: 25244 RVA: 0x0019B824 File Offset: 0x00199A24
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				string id;
				if (base.Fields.IsModified("ExternalDirectoryOrganizationId"))
				{
					id = ((Guid)base.Fields["ExternalDirectoryOrganizationId"]).ToString();
				}
				else if (base.Fields.IsModified("DomainName"))
				{
					id = ((SmtpDomain)base.Fields["DomainName"]).Domain;
				}
				else
				{
					id = (string)base.Fields["Address"];
				}
				return Strings.ConfirmationMessageRemoveMservEntry(id);
			}
		}

		// Token: 0x0600629D RID: 25245 RVA: 0x0019B8D0 File Offset: 0x00199AD0
		protected override void InternalProcessRecord()
		{
			if (base.Fields.IsModified("ExternalDirectoryOrganizationId"))
			{
				base.ProcessExternalOrgIdParameter((string address, int partnerId) => base.RemoveMservEntry(address));
				return;
			}
			if (base.Fields.IsModified("DomainName"))
			{
				base.ProcessDomainNameParameter((string address, int partnerId) => base.RemoveMservEntry(address));
				return;
			}
			base.ProcessAddressParameter((string address, int partnerId) => base.RemoveMservEntry(address));
		}
	}
}
