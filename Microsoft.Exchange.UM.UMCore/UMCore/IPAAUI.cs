using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.UM.PersonalAutoAttendant;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000293 RID: 659
	internal interface IPAAUI
	{
		// Token: 0x0600136E RID: 4974
		void SetADTransferTargetMenuItem(int key, string type, string context, string legacyExchangeDN, ADRecipient transferTarget);

		// Token: 0x0600136F RID: 4975
		void SetPhoneNumberTransferMenuItem(int key, string type, string context, string phoneNumberString);

		// Token: 0x06001370 RID: 4976
		void SetFindMeMenuItem(int key, string type, string context);

		// Token: 0x06001371 RID: 4977
		void SetMenuItemTransferToVoiceMail();

		// Token: 0x06001372 RID: 4978
		void SetADTransferTarget(ADRecipient transferTarget);

		// Token: 0x06001373 RID: 4979
		void SetTransferToMailboxEnabled();

		// Token: 0x06001374 RID: 4980
		void SetInvalidADContact();

		// Token: 0x06001375 RID: 4981
		void SetTransferToVoiceMessageEnabled();

		// Token: 0x06001376 RID: 4982
		void SetBlindTransferEnabled(bool enabled, PhoneNumber target);

		// Token: 0x06001377 RID: 4983
		void SetPermissionCheckFailure();

		// Token: 0x06001378 RID: 4984
		void SetFindMeNumbers(FindMe[] numbers);
	}
}
