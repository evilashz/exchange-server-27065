using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000781 RID: 1921
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncStateSaveConflictException : SaveConflictException
	{
		// Token: 0x060048DE RID: 18654 RVA: 0x00131927 File Offset: 0x0012FB27
		public SyncStateSaveConflictException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060048DF RID: 18655 RVA: 0x00131931 File Offset: 0x0012FB31
		public SyncStateSaveConflictException(LocalizedString message) : base(message)
		{
		}
	}
}
