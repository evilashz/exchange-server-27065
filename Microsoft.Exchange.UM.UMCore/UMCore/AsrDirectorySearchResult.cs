using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000020 RID: 32
	internal class AsrDirectorySearchResult : AsrSearchResult
	{
		// Token: 0x06000197 RID: 407 RVA: 0x00007B74 File Offset: 0x00005D74
		internal AsrDirectorySearchResult(IUMRecognitionPhrase recognitionPhrase, Guid tenantGuid)
		{
			string text = (string)recognitionPhrase["ObjectGuid"];
			if (!string.IsNullOrEmpty(text))
			{
				IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromTenantGuid(tenantGuid);
				this.selectedRecipient = iadrecipientLookup.LookupByObjectId(new ADObjectId(new Guid(text)));
			}
			if (this.selectedRecipient == null)
			{
				throw new InvalidObjectGuidException(text);
			}
			IADOrgPerson iadorgPerson = this.selectedRecipient as IADOrgPerson;
			if (iadorgPerson != null && !string.IsNullOrEmpty(iadorgPerson.Phone))
			{
				this.selectedPhoneNumber = PhoneNumber.Parse(iadorgPerson.Phone);
				return;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00007BFB File Offset: 0x00005DFB
		public ADRecipient Recipient
		{
			get
			{
				return this.selectedRecipient;
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00007C04 File Offset: 0x00005E04
		internal override void SetManagerVariables(ActivityManager manager, BaseUMCallSession vo)
		{
			ContactSearchItem varValue = ContactSearchItem.CreateFromRecipient(this.selectedRecipient);
			manager.WriteVariable("resultType", ResultType.DirectoryContact);
			manager.WriteVariable("resultTypeString", ResultType.DirectoryContact.ToString());
			manager.WriteVariable("selectedUser", varValue);
			manager.WriteVariable("directorySearchResult", varValue);
			manager.WriteVariable("selectedPhoneNumber", this.selectedPhoneNumber);
		}

		// Token: 0x0400007C RID: 124
		private ADRecipient selectedRecipient;

		// Token: 0x0400007D RID: 125
		private PhoneNumber selectedPhoneNumber;
	}
}
