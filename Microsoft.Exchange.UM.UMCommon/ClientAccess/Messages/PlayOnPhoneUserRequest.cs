using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.ClientAccess.Messages
{
	// Token: 0x02000114 RID: 276
	[Serializable]
	public abstract class PlayOnPhoneUserRequest : PlayOnPhoneRequest
	{
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00023BA9 File Offset: 0x00021DA9
		internal ADRecipient CallerInfo
		{
			get
			{
				if (this.callerInfo == null)
				{
					this.GetCallerInfo();
				}
				return this.callerInfo;
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00023BC0 File Offset: 0x00021DC0
		private void GetCallerInfo()
		{
			if (string.IsNullOrEmpty(base.ProxyAddress))
			{
				throw new InvalidADRecipientException();
			}
			IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromTenantGuid(base.TenantGuid);
			ADRecipient adrecipient = iadrecipientLookup.LookupByObjectId(new ADObjectId(base.UserObjectGuid));
			if (adrecipient == null)
			{
				throw new InvalidADRecipientException();
			}
			this.callerInfo = adrecipient;
		}

		// Token: 0x0400051D RID: 1309
		protected ADRecipient callerInfo;
	}
}
