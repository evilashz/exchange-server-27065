using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200084B RID: 2123
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IRecipientChangeTracker
	{
		// Token: 0x06004F1E RID: 20254
		void AddRecipient(CoreRecipient coreRecipient, out bool considerRecipientModified);

		// Token: 0x06004F1F RID: 20255
		void RemoveAddedRecipient(CoreRecipient coreRecipient);

		// Token: 0x06004F20 RID: 20256
		void RemoveUnchangedRecipient(CoreRecipient coreRecipient);

		// Token: 0x06004F21 RID: 20257
		void RemoveModifiedRecipient(CoreRecipient coreRecipient);

		// Token: 0x06004F22 RID: 20258
		void OnModifyRecipient(CoreRecipient coreRecipient);
	}
}
