using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x020000EC RID: 236
	internal interface IDataValidator : IDisposeTrackable, IDisposable
	{
		// Token: 0x060007BD RID: 1981
		bool ValidateADContactForOutdialing(string legacyExchangeDN, out IDataValidationResult result);

		// Token: 0x060007BE RID: 1982
		bool ValidateADContactForTransferToMailbox(string legacyExchangeDN, out IDataValidationResult result);

		// Token: 0x060007BF RID: 1983
		bool ValidatePhoneNumberForOutdialing(string phoneNumber, out IDataValidationResult result);

		// Token: 0x060007C0 RID: 1984
		bool ValidateContactItemCallerId(StoreObjectId storeId, out IDataValidationResult result);

		// Token: 0x060007C1 RID: 1985
		bool ValidateADContactCallerId(string exchangeLegacyDN, out IDataValidationResult result);

		// Token: 0x060007C2 RID: 1986
		bool ValidatePersonaContactCallerId(string emailAddress, out IDataValidationResult result);

		// Token: 0x060007C3 RID: 1987
		bool ValidatePhoneNumberCallerId(string number, out IDataValidationResult result);

		// Token: 0x060007C4 RID: 1988
		bool ValidateContactFolderCallerId(out IDataValidationResult result);

		// Token: 0x060007C5 RID: 1989
		bool ValidateExtensions(IList<string> extensions, out PAAValidationResult result, out string extensionInError);
	}
}
