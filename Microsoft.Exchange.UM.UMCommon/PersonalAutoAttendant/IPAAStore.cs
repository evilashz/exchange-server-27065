using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x02000107 RID: 263
	internal interface IPAAStore : IDisposeTrackable, IDisposable
	{
		// Token: 0x0600088E RID: 2190
		IList<PersonalAutoAttendant> GetAutoAttendants(PAAValidationMode validationMode);

		// Token: 0x0600088F RID: 2191
		bool TryGetAutoAttendants(PAAValidationMode validationMode, out IList<PersonalAutoAttendant> autoAttendants);

		// Token: 0x06000890 RID: 2192
		IList<PersonalAutoAttendant> GetAutoAttendants(PAAValidationMode validationMode, out PAAStoreStatus storeStatus);

		// Token: 0x06000891 RID: 2193
		PersonalAutoAttendant GetAutoAttendant(Guid identity, PAAValidationMode validationMode);

		// Token: 0x06000892 RID: 2194
		bool TryGetAutoAttendant(Guid identity, PAAValidationMode validationMode, out PersonalAutoAttendant autoAttendant);

		// Token: 0x06000893 RID: 2195
		void DeleteAutoAttendant(Guid identity);

		// Token: 0x06000894 RID: 2196
		void DeleteGreeting(PersonalAutoAttendant paa);

		// Token: 0x06000895 RID: 2197
		void Save(IList<PersonalAutoAttendant> autoAttendants);

		// Token: 0x06000896 RID: 2198
		GreetingBase OpenGreeting(PersonalAutoAttendant paa);

		// Token: 0x06000897 RID: 2199
		void DeletePAAConfiguration();

		// Token: 0x06000898 RID: 2200
		void GetUserPermissions(out bool enabledForPersonalAutoAttendant, out bool enabledForOutdialing);

		// Token: 0x06000899 RID: 2201
		bool Validate(PersonalAutoAttendant paa, PAAValidationMode validationMode);

		// Token: 0x0600089A RID: 2202
		IList<string> GetExtensionsInPrimaryDialPlan();

		// Token: 0x0600089B RID: 2203
		bool ValidatePhoneNumberForOutdialing(string number, out IDataValidationResult result);

		// Token: 0x0600089C RID: 2204
		bool ValidateADContactForOutdialing(string legacyExchangeDN, out IDataValidationResult result);

		// Token: 0x0600089D RID: 2205
		bool ValidateADContactForTransferToMailbox(string legacyExchangeDN, out IDataValidationResult result);

		// Token: 0x0600089E RID: 2206
		bool ValidateContactItemCallerId(StoreObjectId storeId, out IDataValidationResult result);

		// Token: 0x0600089F RID: 2207
		bool ValidateADContactCallerId(string exchangeLegacyDN, out IDataValidationResult result);

		// Token: 0x060008A0 RID: 2208
		bool ValidatePhoneNumberCallerId(string number, out IDataValidationResult result);

		// Token: 0x060008A1 RID: 2209
		bool ValidateContactFolderCallerId(out IDataValidationResult result);

		// Token: 0x060008A2 RID: 2210
		bool ValidatePersonaContactCallerId(string emailAddress, out IDataValidationResult result);

		// Token: 0x060008A3 RID: 2211
		bool ValidateExtensions(IList<string> extensions, out PAAValidationResult result, out string extensionInError);
	}
}
