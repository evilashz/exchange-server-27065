using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200006C RID: 108
	public static class DxSerializationUtil
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0000F830 File Offset: 0x0000DA30
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x0000F837 File Offset: 0x0000DA37
		public static bool UseBinarySerialize { get; set; } = true;

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000F840 File Offset: 0x0000DA40
		public static MemoryStream Serialize<T>(T obj) where T : class
		{
			MemoryStream result;
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				if (DxSerializationUtil.UseBinarySerialize)
				{
					DxBinarySerializationUtil.Serialize(memoryStream, obj);
				}
				else
				{
					DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T), null, int.MaxValue, false, false, null, new DxSerializationUtil.SharedTypeResolver());
					dataContractSerializer.WriteObject(memoryStream, obj);
				}
				result = memoryStream;
			}
			catch (Exception ex)
			{
				EventLogger.LogErr("Serialize<T> err: {0}", new object[]
				{
					ex
				});
				throw new DxStoreSerializeException(ex.Message, ex);
			}
			return result;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000F8D0 File Offset: 0x0000DAD0
		public static MemoryStream SerializeMessage(HttpRequest msg)
		{
			return DxSerializationUtil.Serialize<HttpRequest>(msg);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000F8D8 File Offset: 0x0000DAD8
		public static T Deserialize<T>(Stream stream) where T : class
		{
			T result;
			try
			{
				if (DxSerializationUtil.UseBinarySerialize)
				{
					result = (T)((object)DxBinarySerializationUtil.Deserialize(stream));
				}
				else
				{
					DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T), null, int.MaxValue, false, false, null, new DxSerializationUtil.SharedTypeResolver());
					result = (T)((object)dataContractSerializer.ReadObject(stream));
				}
			}
			catch (Exception ex)
			{
				EventLogger.LogErr("Deserialize<T> err: {0}", new object[]
				{
					ex
				});
				throw new DxStoreSerializeException(ex.Message, ex);
			}
			return result;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000F95C File Offset: 0x0000DB5C
		public static T TryDeserialize<T>(Stream stream, out Exception ex) where T : class
		{
			ex = null;
			try
			{
				return DxSerializationUtil.Deserialize<T>(stream);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			return default(T);
		}

		// Token: 0x0200006D RID: 109
		public class SharedTypeResolver : DataContractResolver
		{
			// Token: 0x060004A8 RID: 1192 RVA: 0x0000F998 File Offset: 0x0000DB98
			public override bool TryResolveType(Type dataContractType, Type declaredType, DataContractResolver knownTypeResolver, out XmlDictionaryString typeName, out XmlDictionaryString typeNamespace)
			{
				if (!knownTypeResolver.TryResolveType(dataContractType, declaredType, null, out typeName, out typeNamespace))
				{
					XmlDictionary xmlDictionary = new XmlDictionary();
					typeName = xmlDictionary.Add(dataContractType.FullName);
					typeNamespace = xmlDictionary.Add(dataContractType.Assembly.FullName);
				}
				return true;
			}

			// Token: 0x060004A9 RID: 1193 RVA: 0x0000F9E0 File Offset: 0x0000DBE0
			public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
			{
				Type type = knownTypeResolver.ResolveName(typeName, typeNamespace, declaredType, null);
				if (type == null)
				{
					if (typeNamespace.StartsWith("http:", StringComparison.OrdinalIgnoreCase))
					{
						return null;
					}
					try
					{
						string typeName2 = typeName + ", " + typeNamespace;
						type = Type.GetType(typeName2);
					}
					catch (Exception ex)
					{
						EventLogger.LogErr("ResolveName err: {0}", new object[]
						{
							ex
						});
					}
				}
				return type;
			}
		}
	}
}
