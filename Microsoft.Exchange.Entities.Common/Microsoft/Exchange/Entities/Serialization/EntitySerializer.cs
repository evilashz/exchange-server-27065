using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Entities.DataModel;

namespace Microsoft.Exchange.Entities.Serialization
{
	// Token: 0x02000055 RID: 85
	public class EntitySerializer
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x000069D8 File Offset: 0x00004BD8
		public static void Serialize<T>(T obj, Stream stream)
		{
			DataContractSerializer dataContractSerializer = new DataContractSerializer(obj.GetType(), EntitySerializer.DefaultSerializerSettings);
			dataContractSerializer.WriteObject(stream, obj);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00006A0C File Offset: 0x00004C0C
		public static T Deserialize<T>(Stream stream)
		{
			DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T), EntitySerializer.DefaultSerializerSettings);
			return (T)((object)dataContractSerializer.ReadObject(stream));
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00006A3C File Offset: 0x00004C3C
		public static string Serialize<T>(T obj)
		{
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				EntitySerializer.Serialize<T>(obj, memoryStream);
				@string = Encoding.UTF8.GetString(memoryStream.ToArray());
			}
			return @string;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00006A84 File Offset: 0x00004C84
		public static T Deserialize<T>(string serializedObject)
		{
			T result;
			using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(serializedObject)))
			{
				result = EntitySerializer.Deserialize<T>(memoryStream);
			}
			return result;
		}

		// Token: 0x04000086 RID: 134
		private static readonly DataContractSerializerSettings DefaultSerializerSettings = new DataContractSerializerSettings
		{
			DataContractSurrogate = PropertyChangeTrackingObject.DataContractSurrogate,
			PreserveObjectReferences = true,
			DataContractResolver = new EntitySerializer.EntityDataContractResolver()
		};

		// Token: 0x02000056 RID: 86
		private class EntityDataContractResolver : DataContractResolver
		{
			// Token: 0x060001CA RID: 458 RVA: 0x00006B08 File Offset: 0x00004D08
			public override bool TryResolveType(Type type, Type declaredType, DataContractResolver knownTypeResolver, out XmlDictionaryString typeName, out XmlDictionaryString typeNamespace)
			{
				typeName = new XmlDictionaryString(XmlDictionary.Empty, type.FullName, 0);
				string name = type.Assembly.GetName().Name;
				if (name == EntitySerializer.EntityDataContractResolver.MscorlibAssemblyName)
				{
					typeNamespace = XmlDictionaryString.Empty;
				}
				else
				{
					typeNamespace = new XmlDictionaryString(XmlDictionary.Empty, type.Assembly.FullName, 0);
				}
				return true;
			}

			// Token: 0x060001CB RID: 459 RVA: 0x00006B6C File Offset: 0x00004D6C
			public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
			{
				Type type = Type.GetType(typeName, false);
				if (type == null)
				{
					Assembly assembly = Assembly.Load(typeNamespace);
					type = assembly.GetType(typeName, false);
				}
				return type ?? knownTypeResolver.ResolveName(typeName, typeNamespace, declaredType, null);
			}

			// Token: 0x04000087 RID: 135
			private static readonly string MscorlibAssemblyName = typeof(int).Assembly.GetName().Name;
		}
	}
}
