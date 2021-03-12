using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AAA RID: 2730
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class PDLMembershipPropertyRule
	{
		// Token: 0x0600638C RID: 25484 RVA: 0x001A3B94 File Offset: 0x001A1D94
		public static bool UpdateProperties(ICorePropertyBag propertyBag)
		{
			ParticipantEntryId[] array;
			ParticipantEntryId[] array2;
			byte[][] array3;
			uint num;
			bool flag;
			DistributionList.GetEntryIds(propertyBag, out array, out array2, out array3, out num, out flag);
			return false;
		}

		// Token: 0x0400383B RID: 14395
		public static readonly PropertyReference[] PropertyReferences = new PropertyReference[]
		{
			new PropertyReference(InternalSchema.Members, PropertyAccess.Read),
			new PropertyReference(InternalSchema.OneOffMembers, PropertyAccess.Read),
			new PropertyReference(InternalSchema.DLStream, PropertyAccess.Read)
		};
	}
}
