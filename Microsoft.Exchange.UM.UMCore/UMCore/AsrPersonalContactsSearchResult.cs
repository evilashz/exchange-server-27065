using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000022 RID: 34
	internal class AsrPersonalContactsSearchResult : AsrSearchResult
	{
		// Token: 0x0600019D RID: 413 RVA: 0x00007CEC File Offset: 0x00005EEC
		internal AsrPersonalContactsSearchResult(IUMRecognitionPhrase recognitionPhrase, UMSubscriber subscriber)
		{
			string id = (string)recognitionPhrase["ContactId"];
			this.selectedUser = ContactSearchItem.GetSelectedSearchItemFromId(subscriber, id);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00007D20 File Offset: 0x00005F20
		internal override void SetManagerVariables(ActivityManager manager, BaseUMCallSession vo)
		{
			manager.WriteVariable("resultType", ResultType.PersonalContact);
			manager.WriteVariable("resultTypeString", ResultType.PersonalContact.ToString());
			manager.WriteVariable("selectedUser", this.selectedUser);
			manager.WriteVariable("directorySearchResult", this.selectedUser);
			manager.WriteVariable("selectedPhoneNumber", null);
		}

		// Token: 0x0400007F RID: 127
		private ContactSearchItem selectedUser;
	}
}
