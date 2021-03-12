using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200020C RID: 524
	internal class InternetMessageHeaderPropertyInformation : PropertyInformation
	{
		// Token: 0x06000D99 RID: 3481 RVA: 0x00043E08 File Offset: 0x00042008
		public InternetMessageHeaderPropertyInformation() : base("InternetMessageHeaders", null, ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2007, null, new DictionaryPropertyUriBase(DictionaryUriEnum.InternetMessageHeader), new PropertyCommand.CreatePropertyCommand(InternetMessageHeadersProperty.CreateCommand), true)
		{
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00043E40 File Offset: 0x00042040
		public override PropertyDefinition[] GetPropertyDefinitions(CommandSettings commandSettings)
		{
			DictionaryPropertyUri dictionaryPropertyUri = null;
			ToXmlCommandSettingsBase toXmlCommandSettingsBase = commandSettings as ToXmlCommandSettingsBase;
			if (toXmlCommandSettingsBase != null)
			{
				dictionaryPropertyUri = (DictionaryPropertyUri)toXmlCommandSettingsBase.PropertyPath;
			}
			else
			{
				ToServiceObjectCommandSettings toServiceObjectCommandSettings = commandSettings as ToServiceObjectCommandSettings;
				if (toServiceObjectCommandSettings != null)
				{
					dictionaryPropertyUri = (DictionaryPropertyUri)toServiceObjectCommandSettings.PropertyPath;
				}
			}
			if (dictionaryPropertyUri == null)
			{
				return null;
			}
			if (ExchangeVersion.Current == ExchangeVersion.Exchange2007)
			{
				return new PropertyDefinition[]
				{
					MessageItemSchema.TransportMessageHeaders,
					PropertyDefinitionHelper.GenerateInternetHeaderPropertyDefinition(dictionaryPropertyUri.Key)
				};
			}
			return new PropertyDefinition[]
			{
				MessageItemSchema.TransportMessageHeaders
			};
		}
	}
}
