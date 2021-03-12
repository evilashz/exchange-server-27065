using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A47 RID: 2631
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class MigrationXmlSerializer
	{
		// Token: 0x17001A7F RID: 6783
		// (get) Token: 0x0600603A RID: 24634 RVA: 0x00195E58 File Offset: 0x00194058
		private static XmlWriterSettings XmlWriterSettings
		{
			get
			{
				if (MigrationXmlSerializer.xmlWriterSettings == null)
				{
					MigrationXmlSerializer.xmlWriterSettings = new XmlWriterSettings
					{
						OmitXmlDeclaration = true,
						Encoding = new UTF8Encoding(false),
						CheckCharacters = false
					};
				}
				return MigrationXmlSerializer.xmlWriterSettings;
			}
		}

		// Token: 0x17001A80 RID: 6784
		// (get) Token: 0x0600603B RID: 24635 RVA: 0x00195E98 File Offset: 0x00194098
		private static XmlReaderSettings XmlReaderSettings
		{
			get
			{
				if (MigrationXmlSerializer.xmlReaderSettings == null)
				{
					MigrationXmlSerializer.xmlReaderSettings = new XmlReaderSettings
					{
						CheckCharacters = false,
						DtdProcessing = DtdProcessing.Ignore,
						XmlResolver = null
					};
				}
				return MigrationXmlSerializer.xmlReaderSettings;
			}
		}

		// Token: 0x0600603C RID: 24636 RVA: 0x00195ED4 File Offset: 0x001940D4
		public static string Serialize(object obj)
		{
			Util.ThrowOnNullArgument(obj, "obj");
			StringBuilder stringBuilder = new StringBuilder(2048);
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, MigrationXmlSerializer.XmlWriterSettings))
			{
				try
				{
					IXmlSerializable xmlSerializable = obj as IXmlSerializable;
					if (xmlSerializable != null)
					{
						xmlSerializable.WriteXml(xmlWriter);
					}
					else
					{
						XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
						xmlSerializer.Serialize(xmlWriter, obj);
					}
				}
				catch (InvalidOperationException innerException)
				{
					throw new MigrationDataCorruptionException(string.Format("Couldn't serialize object of type {0}", obj.GetType()), innerException);
				}
				catch (ArgumentException innerException2)
				{
					throw new MigrationDataCorruptionException(string.Format("Couldn't serialize object of type {0}", obj.GetType()), innerException2);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600603D RID: 24637 RVA: 0x00195F9C File Offset: 0x0019419C
		public static T Deserialize<T>(string rawXml) where T : IXmlSerializable, new()
		{
			Util.ThrowOnNullOrEmptyArgument(rawXml, "rawXml");
			T t = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
			MigrationXmlSerializer.Deserialize(rawXml, new Action<XmlReader>(t.ReadXml));
			return t;
		}

		// Token: 0x0600603E RID: 24638 RVA: 0x00196020 File Offset: 0x00194220
		public static object Deserialize(string rawXml, Type type)
		{
			Util.ThrowOnNullOrEmptyArgument(rawXml, "rawXml");
			Util.ThrowOnNullArgument(type, "type");
			object obj = null;
			MigrationXmlSerializer.Deserialize(rawXml, delegate(XmlReader xmlReader)
			{
				XmlSerializer xmlSerializer = new XmlSerializer(type);
				obj = xmlSerializer.Deserialize(xmlReader);
			});
			if (obj == null)
			{
				throw new MigrationDataCorruptionException("couldn't deserialize object of type " + type);
			}
			return obj;
		}

		// Token: 0x0600603F RID: 24639 RVA: 0x00196094 File Offset: 0x00194294
		private static void Deserialize(string rawXml, Action<XmlReader> deserialize)
		{
			using (StringReader stringReader = new StringReader(rawXml))
			{
				using (XmlReader xmlReader = XmlReader.Create(stringReader, MigrationXmlSerializer.XmlReaderSettings))
				{
					try
					{
						deserialize(xmlReader);
					}
					catch (InvalidOperationException innerException)
					{
						string internalDetails = string.Format("Couldn't deserialize string: '{0}'", (rawXml.Length > 128) ? (rawXml.Substring(0, 128) + "...") : rawXml);
						throw new MigrationDataCorruptionException(internalDetails, innerException);
					}
				}
			}
		}

		// Token: 0x040036D3 RID: 14035
		private const int DefaultXmlSize = 2048;

		// Token: 0x040036D4 RID: 14036
		private static XmlWriterSettings xmlWriterSettings;

		// Token: 0x040036D5 RID: 14037
		private static XmlReaderSettings xmlReaderSettings;
	}
}
