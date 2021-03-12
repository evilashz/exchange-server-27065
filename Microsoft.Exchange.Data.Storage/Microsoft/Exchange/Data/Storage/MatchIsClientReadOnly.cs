using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000683 RID: 1667
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MatchIsClientReadOnly : IValidator
	{
		// Token: 0x06004471 RID: 17521 RVA: 0x00123DCC File Offset: 0x00121FCC
		public bool Validate(DefaultFolderContext context, PropertyBag propertyBag)
		{
			ExtendedFolderFlags? valueAsNullable = propertyBag.GetValueAsNullable<ExtendedFolderFlags>(FolderSchema.ExtendedFolderFlags);
			return valueAsNullable != null && (valueAsNullable.Value & ExtendedFolderFlags.ReadOnly) == ExtendedFolderFlags.ReadOnly;
		}

		// Token: 0x06004472 RID: 17522 RVA: 0x00123E00 File Offset: 0x00122000
		public void SetProperties(DefaultFolderContext context, Folder folder)
		{
			ExtendedFolderFlags? valueAsNullable = folder.GetValueAsNullable<ExtendedFolderFlags>(FolderSchema.ExtendedFolderFlags);
			if (valueAsNullable == null)
			{
				folder[FolderSchema.ExtendedFolderFlags] = ExtendedFolderFlags.ReadOnly;
				return;
			}
			folder[FolderSchema.ExtendedFolderFlags] = (ExtendedFolderFlags.ReadOnly | valueAsNullable.Value);
		}
	}
}
