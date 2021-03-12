using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000023 RID: 35
	internal class MailboxCreationTimeProvisioningHandler : ProvisioningHandlerBase
	{
		// Token: 0x06000119 RID: 281 RVA: 0x000067E0 File Offset: 0x000049E0
		public override bool UpdateAffectedIConfigurable(IConfigurable writeableIConfigurable)
		{
			if (base.UserSpecifiedParameters["Credential"] != null)
			{
				return false;
			}
			ADPresentationObject adpresentationObject = writeableIConfigurable as ADPresentationObject;
			ADUser aduser;
			if (adpresentationObject != null)
			{
				aduser = (adpresentationObject.DataObject as ADUser);
			}
			else
			{
				aduser = (writeableIConfigurable as ADUser);
			}
			return aduser != null && aduser.SetWhenMailboxCreatedIfNotSet();
		}

		// Token: 0x04000097 RID: 151
		private const string MailboxParameterSetArbitration = "Arbitration";

		// Token: 0x04000098 RID: 152
		private const string MailboxParameterSetDiscovery = "Discovery";
	}
}
