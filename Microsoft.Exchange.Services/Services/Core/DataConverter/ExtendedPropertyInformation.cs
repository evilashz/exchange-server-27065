using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200020B RID: 523
	internal class ExtendedPropertyInformation : PropertyInformation
	{
		// Token: 0x06000D96 RID: 3478 RVA: 0x00043CB8 File Offset: 0x00041EB8
		public ExtendedPropertyInformation() : base("ExtendedProperty", ServiceXml.GetFullyQualifiedName("ExtendedProperty"), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2007, null, ExtendedPropertyUri.Placeholder, new PropertyCommand.CreatePropertyCommand(ExtendedPropertyProperty.CreateCommand), true)
		{
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00043CF8 File Offset: 0x00041EF8
		public override PropertyDefinition[] GetPropertyDefinitions(CommandSettings commandSettings)
		{
			ExtendedPropertyUri extendedPropertyUri = null;
			ToXmlCommandSettingsBase toXmlCommandSettingsBase = commandSettings as ToXmlCommandSettingsBase;
			if (toXmlCommandSettingsBase != null)
			{
				extendedPropertyUri = (ExtendedPropertyUri)toXmlCommandSettingsBase.PropertyPath;
			}
			else
			{
				ToServiceObjectCommandSettingsBase toServiceObjectCommandSettingsBase = commandSettings as ToServiceObjectCommandSettingsBase;
				if (toServiceObjectCommandSettingsBase != null)
				{
					extendedPropertyUri = (ExtendedPropertyUri)toServiceObjectCommandSettingsBase.PropertyPath;
				}
				else
				{
					SetCommandSettings setCommandSettings = commandSettings as SetCommandSettings;
					if (setCommandSettings != null)
					{
						if (setCommandSettings.ServiceObject != null)
						{
							PropertyInformation propertyInfo = (setCommandSettings.ServiceObject is BaseFolderType) ? BaseFolderSchema.ExtendedProperty : ItemSchema.ExtendedProperty;
							ExtendedPropertyType[] valueOrDefault = setCommandSettings.ServiceObject.GetValueOrDefault<ExtendedPropertyType[]>(propertyInfo);
							if (valueOrDefault != null)
							{
								PropertyDefinition[] array = new PropertyDefinition[valueOrDefault.Length];
								for (int i = 0; i < valueOrDefault.Length; i++)
								{
									array[i] = valueOrDefault[i].ExtendedFieldURI.ToPropertyDefinition();
								}
								return array;
							}
						}
						else
						{
							extendedPropertyUri = this.GetPropertyUri(setCommandSettings);
						}
					}
					else
					{
						UpdateCommandSettings updateCommandSettings = commandSettings as UpdateCommandSettings;
						extendedPropertyUri = (ExtendedPropertyUri)updateCommandSettings.PropertyUpdate.PropertyPath;
					}
				}
			}
			return new PropertyDefinition[]
			{
				extendedPropertyUri.ToPropertyDefinition()
			};
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00043DEA File Offset: 0x00041FEA
		private ExtendedPropertyUri GetPropertyUri(SetCommandSettings setCommandSettings)
		{
			return ExtendedPropertyUri.Parse(setCommandSettings.ServiceProperty["ExtendedFieldURI", "http://schemas.microsoft.com/exchange/services/2006/types"]);
		}
	}
}
