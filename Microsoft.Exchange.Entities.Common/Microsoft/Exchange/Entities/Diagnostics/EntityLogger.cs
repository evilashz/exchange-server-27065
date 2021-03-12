using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using DataContractSerializerProject;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.Diagnostics
{
	// Token: 0x02000017 RID: 23
	public class EntityLogger
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00003C64 File Offset: 0x00001E64
		public static string GetLoggingDetails(IPropertyChangeTracker<PropertyDefinition> entity)
		{
			DataContractSerializer dataContractSerializer = new DataContractSerializer(entity.GetType(), EntityLogger.LoggingSerializerSettings);
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				using (EntityXmlTextWriter entityXmlTextWriter = new EntityXmlTextWriter(stringWriter))
				{
					dataContractSerializer.WriteObject(entityXmlTextWriter, entity);
					result = stringWriter.ToString();
				}
			}
			return result;
		}

		// Token: 0x04000034 RID: 52
		private static readonly DataContractSerializerSettings LoggingSerializerSettings = new DataContractSerializerSettings
		{
			DataContractSurrogate = PropertyChangeTrackingObject.LoggingSurrogate,
			PreserveObjectReferences = true,
			DataContractResolver = new EntityLogger.EntityLoggerDataContractResolver(),
			RootName = new XmlDictionaryString(XmlDictionary.Empty, "Entity", 0)
		};

		// Token: 0x02000018 RID: 24
		private class EntityLoggerDataContractResolver : DataContractResolver
		{
			// Token: 0x06000095 RID: 149 RVA: 0x00003D28 File Offset: 0x00001F28
			public override bool TryResolveType(Type type, Type declaredType, DataContractResolver knownTypeResolver, out XmlDictionaryString typeName, out XmlDictionaryString typeNamespace)
			{
				string value = XmlConvert.EncodeName(type.Name);
				typeName = new XmlDictionaryString(XmlDictionary.Empty, value, 0);
				typeNamespace = new XmlDictionaryString(XmlDictionary.Empty, string.Empty, 0);
				return true;
			}

			// Token: 0x06000096 RID: 150 RVA: 0x00003D64 File Offset: 0x00001F64
			public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
			{
				throw new NotImplementedException();
			}
		}
	}
}
