using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x0200003B RID: 59
	internal class QueryBaseDNRestrictionModifyObjectProvisioningHandler : ProvisioningHandlerBase
	{
		// Token: 0x0600018C RID: 396 RVA: 0x000095C4 File Offset: 0x000077C4
		public sealed override bool UpdateAffectedIConfigurable(IConfigurable writeableIConfigurable)
		{
			ADObject adobject;
			if (writeableIConfigurable is ADPresentationObject)
			{
				adobject = ((ADPresentationObject)writeableIConfigurable).DataObject;
			}
			else
			{
				adobject = (ADObject)writeableIConfigurable;
			}
			ADUser aduser = adobject as ADUser;
			if (aduser == null)
			{
				return false;
			}
			if (!aduser.QueryBaseDNRestrictionEnabled && ADObjectId.Equals(aduser.QueryBaseDN, aduser.Id))
			{
				aduser.QueryBaseDN = null;
				return true;
			}
			if (aduser.QueryBaseDNRestrictionEnabled && !ADObjectId.Equals(aduser.QueryBaseDN, aduser.Id))
			{
				aduser.QueryBaseDN = aduser.Id;
				return true;
			}
			return false;
		}
	}
}
