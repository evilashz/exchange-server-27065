using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.NaturalLanguage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C5C RID: 3164
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class ExtractedNaturalLanguageProperty<TExtraction, TExtractionSet> : SmartPropertyDefinition where TExtractionSet : ExtractionSet<TExtraction>, new()
	{
		// Token: 0x06006F78 RID: 28536 RVA: 0x001DFFC8 File Offset: 0x001DE1C8
		internal ExtractedNaturalLanguageProperty(string displayName, NativeStorePropertyDefinition xmlExtractedProperty) : base(displayName, typeof(TExtraction[]), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(xmlExtractedProperty, PropertyDependencyType.NeedForRead)
		})
		{
			this.xmlExtractedProperty = xmlExtractedProperty;
		}

		// Token: 0x06006F79 RID: 28537 RVA: 0x001E0008 File Offset: 0x001DE208
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object value = propertyBag.GetValue(this.xmlExtractedProperty);
			if (!(value is string))
			{
				return null;
			}
			object extractions;
			try
			{
				XmlSerializer serializer = SerializerCache.GetSerializer(typeof(TExtractionSet));
				TExtractionSet textractionSet;
				using (StringReader stringReader = new StringReader((string)value))
				{
					textractionSet = (serializer.Deserialize(stringReader) as TExtractionSet);
				}
				extractions = textractionSet.Extractions;
			}
			catch (InvalidOperationException innerException)
			{
				throw new CorruptDataException(ServerStrings.CorruptNaturalLanguageProperty, innerException);
			}
			return extractions;
		}

		// Token: 0x0400439A RID: 17306
		private NativeStorePropertyDefinition xmlExtractedProperty;
	}
}
