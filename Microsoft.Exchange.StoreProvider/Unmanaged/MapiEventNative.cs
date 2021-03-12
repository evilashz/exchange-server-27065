using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000289 RID: 649
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct MapiEventNative
	{
		// Token: 0x04001112 RID: 4370
		public static readonly int SizeOf = Marshal.SizeOf(typeof(MapiEventNative));

		// Token: 0x04001113 RID: 4371
		internal ulong llEventCounter;

		// Token: 0x04001114 RID: 4372
		internal uint ulMask;

		// Token: 0x04001115 RID: 4373
		internal Guid mailboxGuid;

		// Token: 0x04001116 RID: 4374
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 60)]
		internal string rgchObjectClass;

		// Token: 0x04001117 RID: 4375
		internal long ftCreate;

		// Token: 0x04001118 RID: 4376
		internal uint ulItemType;

		// Token: 0x04001119 RID: 4377
		internal CountAndPtr binEidItem;

		// Token: 0x0400111A RID: 4378
		internal CountAndPtr binEidParent;

		// Token: 0x0400111B RID: 4379
		internal CountAndPtr binEidOldItem;

		// Token: 0x0400111C RID: 4380
		internal CountAndPtr binEidOldParent;

		// Token: 0x0400111D RID: 4381
		internal int lItemCount;

		// Token: 0x0400111E RID: 4382
		internal int lUnreadCount;

		// Token: 0x0400111F RID: 4383
		internal uint ulFlags;

		// Token: 0x04001120 RID: 4384
		internal ulong ullExtendedFlags;

		// Token: 0x04001121 RID: 4385
		internal uint ulClientType;

		// Token: 0x04001122 RID: 4386
		internal CountAndPtr binSid;

		// Token: 0x04001123 RID: 4387
		internal uint ulDocId;

		// Token: 0x04001124 RID: 4388
		internal uint ulMailboxNumber;

		// Token: 0x04001125 RID: 4389
		internal CountAndPtr binTenantHintBob;

		// Token: 0x04001126 RID: 4390
		internal Guid unifiedMailboxGuid;
	}
}
