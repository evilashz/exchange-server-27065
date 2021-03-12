using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C71 RID: 3185
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class IsOutlookSearchFolderProperty : SmartPropertyDefinition
	{
		// Token: 0x06006FF6 RID: 28662 RVA: 0x001F012C File Offset: 0x001EE32C
		internal IsOutlookSearchFolderProperty() : base("IsOutlookSearchFolderProperty", typeof(bool), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.ExtendedFolderFlagsInternal, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06006FF7 RID: 28663 RVA: 0x001F016C File Offset: 0x001EE36C
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object obj = ExtendedFolderFlagsProperty.DecodeFolderFlags(propertyBag.GetValue(InternalSchema.ExtendedFolderFlagsInternal));
			ExtendedFolderFlagsProperty.ParsedFlags parsedFlags = obj as ExtendedFolderFlagsProperty.ParsedFlags;
			return parsedFlags != null && parsedFlags.ContainsKey(ExtendedFolderFlagsProperty.FlagTag.Clsid);
		}
	}
}
