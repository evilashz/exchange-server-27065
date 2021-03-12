using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000032 RID: 50
	[Flags]
	[DataContract]
	internal enum EnumerateFolderHierarchyFlags
	{
		// Token: 0x040001A6 RID: 422
		[EnumMember]
		None = 0,
		// Token: 0x040001A7 RID: 423
		[EnumMember]
		IncludeExtendedData = 1,
		// Token: 0x040001A8 RID: 424
		[EnumMember]
		WellKnownPublicFoldersOnly = 2
	}
}
