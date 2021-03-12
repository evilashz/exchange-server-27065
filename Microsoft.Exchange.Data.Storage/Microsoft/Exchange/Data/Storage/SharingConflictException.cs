using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DC4 RID: 3524
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class SharingConflictException : StorageTransientException
	{
		// Token: 0x06007907 RID: 30983 RVA: 0x00216B63 File Offset: 0x00214D63
		public SharingConflictException() : this(null)
		{
		}

		// Token: 0x06007908 RID: 30984 RVA: 0x00216B6C File Offset: 0x00214D6C
		public SharingConflictException(ConflictResolutionResult conflictResolutionResult) : base(ServerStrings.SharingConflictException)
		{
			this.ConflictResolutionResult = conflictResolutionResult;
		}

		// Token: 0x1700205C RID: 8284
		// (get) Token: 0x06007909 RID: 30985 RVA: 0x00216B80 File Offset: 0x00214D80
		// (set) Token: 0x0600790A RID: 30986 RVA: 0x00216B88 File Offset: 0x00214D88
		public ConflictResolutionResult ConflictResolutionResult { get; private set; }
	}
}
