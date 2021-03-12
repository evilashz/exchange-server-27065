using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200046E RID: 1134
	public enum SpamFilteringAction
	{
		// Token: 0x040022AA RID: 8874
		[LocDescription(DirectoryStrings.IDs.SpamFilteringActionJmf)]
		MoveToJmf,
		// Token: 0x040022AB RID: 8875
		[LocDescription(DirectoryStrings.IDs.SpamFilteringActionAddXHeader)]
		AddXHeader,
		// Token: 0x040022AC RID: 8876
		[LocDescription(DirectoryStrings.IDs.SpamFilteringActionModifySubject)]
		ModifySubject,
		// Token: 0x040022AD RID: 8877
		[LocDescription(DirectoryStrings.IDs.SpamFilteringActionRedirect)]
		Redirect,
		// Token: 0x040022AE RID: 8878
		[LocDescription(DirectoryStrings.IDs.SpamFilteringActionDelete)]
		Delete,
		// Token: 0x040022AF RID: 8879
		[LocDescription(DirectoryStrings.IDs.SpamFilteringActionQuarantine)]
		Quarantine
	}
}
