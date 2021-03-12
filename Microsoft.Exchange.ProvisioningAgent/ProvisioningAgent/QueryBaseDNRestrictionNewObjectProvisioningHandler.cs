using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x0200003C RID: 60
	internal class QueryBaseDNRestrictionNewObjectProvisioningHandler : ProvisioningHandlerBase
	{
		// Token: 0x0600018E RID: 398 RVA: 0x00009654 File Offset: 0x00007854
		public override ProvisioningValidationError[] Validate(IConfigurable readOnlyIConfigurable)
		{
			ADObject adobject;
			if (readOnlyIConfigurable is ADPresentationObject)
			{
				adobject = ((ADPresentationObject)readOnlyIConfigurable).DataObject;
			}
			else
			{
				adobject = (ADObject)readOnlyIConfigurable;
			}
			ADUser aduser = adobject as ADUser;
			if (aduser == null || !aduser.QueryBaseDNRestrictionEnabled || ADObjectId.Equals(aduser.QueryBaseDN, aduser.Id))
			{
				return null;
			}
			this.savedAdUser = aduser;
			return null;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000096B0 File Offset: 0x000078B0
		public override void OnComplete(bool succeeded, Exception e)
		{
			if (!succeeded)
			{
				return;
			}
			if (this.savedAdUser == null)
			{
				return;
			}
			string domainController = base.UserSpecifiedParameters["DomainController"] as string;
			IRecipientSession recipientSession = this.GetRecipientSession(domainController, this.savedAdUser.OrganizationId);
			ADUser aduser = (ADUser)recipientSession.Read<ADUser>(this.savedAdUser.Id);
			if (aduser != null)
			{
				aduser.QueryBaseDN = aduser.Id;
				recipientSession.Save(aduser);
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00009720 File Offset: 0x00007920
		private IRecipientSession GetRecipientSession(string domainController, OrganizationId organizationId)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), organizationId, null, false);
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(domainController, false, ConsistencyMode.FullyConsistent, sessionSettings, 119, "GetRecipientSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ProvisioningAgent\\QueryBaseDNRestrictionNewObjectProvisioningHandler.cs");
		}

		// Token: 0x040000BC RID: 188
		private ADUser savedAdUser;
	}
}
