using System;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x020000FA RID: 250
	internal enum PAAValidationResult
	{
		// Token: 0x040004B0 RID: 1200
		Valid,
		// Token: 0x040004B1 RID: 1201
		ParseError,
		// Token: 0x040004B2 RID: 1202
		SipUriInNonSipDialPlan,
		// Token: 0x040004B3 RID: 1203
		PermissionCheckFailure,
		// Token: 0x040004B4 RID: 1204
		NonExistentContact,
		// Token: 0x040004B5 RID: 1205
		NoValidPhones,
		// Token: 0x040004B6 RID: 1206
		NonExistentDefaultContactsFolder,
		// Token: 0x040004B7 RID: 1207
		NonExistentDirectoryUser,
		// Token: 0x040004B8 RID: 1208
		NonMailboxDirectoryUser,
		// Token: 0x040004B9 RID: 1209
		NonExistentPersona,
		// Token: 0x040004BA RID: 1210
		InvalidExtension
	}
}
