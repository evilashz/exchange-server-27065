using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C92 RID: 3218
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class OutlookSearchFolderClsIdProperty : SmartPropertyDefinition
	{
		// Token: 0x06007086 RID: 28806 RVA: 0x001F2804 File Offset: 0x001F0A04
		internal OutlookSearchFolderClsIdProperty() : base("OutlookSearchFolderClsIdProperty", typeof(Guid), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.ExtendedFolderFlagsInternal, PropertyDependencyType.AllRead)
		})
		{
		}

		// Token: 0x06007087 RID: 28807 RVA: 0x001F2844 File Offset: 0x001F0A44
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object obj = ExtendedFolderFlagsProperty.DecodeFolderFlags(propertyBag.GetValue(InternalSchema.ExtendedFolderFlagsInternal));
			ExtendedFolderFlagsProperty.ParsedFlags parsedFlags = obj as ExtendedFolderFlagsProperty.ParsedFlags;
			if (parsedFlags != null)
			{
				byte[] b;
				if (parsedFlags.TryGetValue(ExtendedFolderFlagsProperty.FlagTag.Clsid, out b))
				{
					try
					{
						return new Guid(b);
					}
					catch (ArgumentException)
					{
						return new PropertyError(this, PropertyErrorCode.CorruptedData);
					}
				}
				return new PropertyError(this, PropertyErrorCode.NotFound);
			}
			return obj;
		}

		// Token: 0x06007088 RID: 28808 RVA: 0x001F28B0 File Offset: 0x001F0AB0
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			byte[] value2 = ((Guid)value).ToByteArray();
			ExtendedFolderFlagsProperty.ParsedFlags parsedFlags = ExtendedFolderFlagsProperty.DecodeFolderFlags(propertyBag.GetValue(InternalSchema.ExtendedFolderFlagsInternal)) as ExtendedFolderFlagsProperty.ParsedFlags;
			if (parsedFlags == null)
			{
				parsedFlags = new ExtendedFolderFlagsProperty.ParsedFlags();
			}
			parsedFlags[ExtendedFolderFlagsProperty.FlagTag.Clsid] = value2;
			propertyBag.SetValueWithFixup(InternalSchema.ExtendedFolderFlagsInternal, ExtendedFolderFlagsProperty.EncodeFolderFlags(parsedFlags));
		}
	}
}
