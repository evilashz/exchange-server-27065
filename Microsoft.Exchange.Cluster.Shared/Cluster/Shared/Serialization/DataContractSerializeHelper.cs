using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security;
using System.ServiceModel;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Shared.Serialization
{
	// Token: 0x020000AC RID: 172
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class DataContractSerializeHelper
	{
		// Token: 0x06000667 RID: 1639 RVA: 0x000179DC File Offset: 0x00015BDC
		public static Exception SerializeToXmlString(object toSerialize, out string xmlString)
		{
			xmlString = null;
			string tmpStr = null;
			Exception ex = DataContractSerializeHelper.HandleException(delegate
			{
				XmlWriterSettings settings = new XmlWriterSettings
				{
					Indent = true,
					IndentChars = "\t"
				};
				DataContractSerializer dataContractSerializer = new DataContractSerializer(toSerialize.GetType());
				StringBuilder stringBuilder = new StringBuilder();
				using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, settings))
				{
					using (XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateDictionaryWriter(xmlWriter))
					{
						dataContractSerializer.WriteObject(xmlDictionaryWriter, toSerialize);
						xmlDictionaryWriter.Flush();
						tmpStr = stringBuilder.ToString();
					}
				}
			});
			if (ex == null)
			{
				xmlString = tmpStr;
			}
			return ex;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00017AA0 File Offset: 0x00015CA0
		public static Exception DeserializeFromXmlString<T>(string xml, out T deserializedObj)
		{
			deserializedObj = default(T);
			T tmpObj = default(T);
			Exception ex = DataContractSerializeHelper.HandleException(delegate
			{
				DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));
				using (StringReader stringReader = new StringReader(xml))
				{
					using (XmlReader xmlReader = XmlReader.Create(stringReader))
					{
						tmpObj = (T)((object)dataContractSerializer.ReadObject(xmlReader));
					}
				}
			});
			if (ex == null)
			{
				deserializedObj = tmpObj;
			}
			return ex;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00017BF8 File Offset: 0x00015DF8
		public static Exception SerializeToXmlFile(object toSerialize, string fileFullPath)
		{
			return DataContractSerializeHelper.HandleException(delegate
			{
				string directoryName = Path.GetDirectoryName(fileFullPath);
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				if (File.Exists(fileFullPath))
				{
					File.Delete(fileFullPath);
				}
				using (FileStream fileStream = File.OpenWrite(fileFullPath))
				{
					XmlWriterSettings settings = new XmlWriterSettings
					{
						Indent = true,
						IndentChars = "\t",
						Encoding = Encoding.UTF8
					};
					DataContractSerializer dataContractSerializer = new DataContractSerializer(toSerialize.GetType());
					using (XmlWriter xmlWriter = XmlWriter.Create(fileStream, settings))
					{
						using (XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateDictionaryWriter(xmlWriter))
						{
							dataContractSerializer.WriteObject(xmlDictionaryWriter, toSerialize);
							xmlDictionaryWriter.Flush();
						}
					}
				}
			});
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00017CB0 File Offset: 0x00015EB0
		public static Exception DeserializeFromXmlFile<T>(string fileFullPath, out T deserializedObj)
		{
			deserializedObj = default(T);
			T tmpObj = default(T);
			Exception ex = DataContractSerializeHelper.HandleException(delegate
			{
				using (FileStream fileStream = File.OpenRead(fileFullPath))
				{
					using (XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas()))
					{
						DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));
						tmpObj = (T)((object)dataContractSerializer.ReadObject(xmlDictionaryReader, true));
					}
				}
			});
			if (ex == null)
			{
				deserializedObj = tmpObj;
			}
			return ex;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00017D00 File Offset: 0x00015F00
		public static Exception HandleException(Action operation)
		{
			Exception result = null;
			try
			{
				operation();
			}
			catch (IOException ex)
			{
				result = ex;
			}
			catch (SecurityException ex2)
			{
				result = ex2;
			}
			catch (UnauthorizedAccessException ex3)
			{
				result = ex3;
			}
			catch (InvalidDataContractException ex4)
			{
				result = ex4;
			}
			catch (SerializationException ex5)
			{
				result = ex5;
			}
			catch (QuotaExceededException ex6)
			{
				result = ex6;
			}
			catch (XmlException ex7)
			{
				result = ex7;
			}
			return result;
		}
	}
}
