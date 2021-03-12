using System;
using Microsoft.Exchange.Data.ApplicationLogic.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200072B RID: 1835
	internal class ContactIdentity : RecipientIdentity
	{
		// Token: 0x0600379B RID: 14235 RVA: 0x000C5AB6 File Offset: 0x000C3CB6
		private ContactIdentity(ADContact adContact)
		{
			this.adRecipient = adContact;
			this.masterAccountSid = adContact.MasterAccountSid;
			this.sid = this.masterAccountSid;
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x000C5AE0 File Offset: 0x000C3CE0
		public static bool TryCreate(ADRecipient adRecipient, out ContactIdentity contactIdentity)
		{
			contactIdentity = null;
			ADContact adcontact = adRecipient as ADContact;
			if (adcontact != null)
			{
				if (RecipientHelper.TryGetMasterAccountSid(adRecipient) == null)
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<ADContact>(0L, "adContact {0} does not have a valid MasterAccountSid", adcontact);
				}
				else
				{
					contactIdentity = new ContactIdentity(adcontact);
				}
			}
			else
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<ADRecipient>(0L, "adRecipient {0} is not ADContact", adRecipient);
			}
			return contactIdentity != null;
		}
	}
}
