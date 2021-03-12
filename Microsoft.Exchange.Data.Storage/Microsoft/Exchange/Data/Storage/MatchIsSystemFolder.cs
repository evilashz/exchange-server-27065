using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000684 RID: 1668
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MatchIsSystemFolder : IValidator
	{
		// Token: 0x06004474 RID: 17524 RVA: 0x00123E58 File Offset: 0x00122058
		public bool Validate(DefaultFolderContext context, PropertyBag propertyBag)
		{
			SystemFolderFlags? valueAsNullable = propertyBag.GetValueAsNullable<SystemFolderFlags>(InternalSchema.SystemFolderFlags);
			return valueAsNullable != null && (valueAsNullable.Value & SystemFolderFlags.SystemFolder) == SystemFolderFlags.SystemFolder;
		}

		// Token: 0x06004475 RID: 17525 RVA: 0x00123E88 File Offset: 0x00122088
		public void SetProperties(DefaultFolderContext context, Folder folder)
		{
			SystemFolderFlags systemFolderFlags = SystemFolderFlags.SystemFolder;
			SystemFolderFlags? valueAsNullable = folder.GetValueAsNullable<SystemFolderFlags>(InternalSchema.SystemFolderFlags);
			if (valueAsNullable != null)
			{
				systemFolderFlags |= valueAsNullable.Value;
			}
			folder[InternalSchema.SystemFolderFlags] = systemFolderFlags;
		}
	}
}
