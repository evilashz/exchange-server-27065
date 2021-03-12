using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Storage.VersionedXml
{
	// Token: 0x02000EBF RID: 3775
	[Serializable]
	public abstract class VersionedXmlBase
	{
		// Token: 0x0600826A RID: 33386 RVA: 0x0023982F File Offset: 0x00237A2F
		protected VersionedXmlBase()
		{
		}

		// Token: 0x0600826B RID: 33387 RVA: 0x00239837 File Offset: 0x00237A37
		protected VersionedXmlBase(Version version)
		{
			if (null == version)
			{
				throw new ArgumentNullException("version");
			}
			this.Version = version.ToString(2);
		}

		// Token: 0x1700228D RID: 8845
		// (get) Token: 0x0600826C RID: 33388 RVA: 0x00239860 File Offset: 0x00237A60
		// (set) Token: 0x0600826D RID: 33389 RVA: 0x00239868 File Offset: 0x00237A68
		[XmlAttribute("Version")]
		public string Version { get; set; }

		// Token: 0x0600826E RID: 33390 RVA: 0x00239874 File Offset: 0x00237A74
		internal static void Serialize(Stream stream, VersionedXmlBase obj)
		{
			if (stream == null)
			{
				throw new CustomSerializationException(ServerStrings.ErrorInvalidConfigurationXml, new ArgumentNullException("stream"));
			}
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			VersionedXmlBase.Serialize(stream, obj);
			try
			{
				stream.SetLength(stream.Position);
			}
			catch (NotSupportedException)
			{
			}
		}

		// Token: 0x0600826F RID: 33391 RVA: 0x002398D0 File Offset: 0x00237AD0
		internal static VersionedXmlBase Deserialize(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanSeek)
			{
				throw new ArgumentException("stream");
			}
			long position = stream.Position;
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Auto;
			string text = null;
			Version version = null;
			using (XmlReader xmlReader = XmlReader.Create(stream, xmlReaderSettings))
			{
				while (xmlReader.Read())
				{
					if (XmlNodeType.Element == xmlReader.NodeType)
					{
						text = xmlReader.Name;
						string attribute = xmlReader.GetAttribute("Version");
						if (!string.IsNullOrEmpty(attribute))
						{
							version = new Version(attribute);
							break;
						}
						break;
					}
				}
			}
			if (string.IsNullOrEmpty(text) || null == version)
			{
				return null;
			}
			stream.Seek(position, SeekOrigin.Begin);
			return (VersionedXmlBase)VersionedXmlBase.Deserialize(VersionedXmlTypeFactory.GetTypeInstance(text, version), stream);
		}

		// Token: 0x06008270 RID: 33392 RVA: 0x002399AC File Offset: 0x00237BAC
		private static string GetXmlString(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				VersionedXmlBase.Serialize(memoryStream, obj);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				@string = Encoding.UTF8.GetString(memoryStream.ToArray());
			}
			return @string;
		}

		// Token: 0x06008271 RID: 33393 RVA: 0x00239A0C File Offset: 0x00237C0C
		private static string GetBase64XmlString(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				VersionedXmlBase.Serialize(memoryStream, obj);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				result = Convert.ToBase64String(memoryStream.ToArray());
			}
			return result;
		}

		// Token: 0x06008272 RID: 33394 RVA: 0x00239A68 File Offset: 0x00237C68
		public static VersionedXmlBase Parse(string xml)
		{
			if (string.IsNullOrEmpty(xml))
			{
				throw new CustomSerializationException(ServerStrings.ErrorInvalidConfigurationXml, new ArgumentNullException("xml"));
			}
			VersionedXmlBase result;
			using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
			{
				result = VersionedXmlBase.Deserialize(memoryStream);
			}
			return result;
		}

		// Token: 0x06008273 RID: 33395 RVA: 0x00239AC8 File Offset: 0x00237CC8
		public static VersionedXmlBase ParseFromBase64(string base64)
		{
			if (string.IsNullOrEmpty(base64))
			{
				throw new CustomSerializationException(ServerStrings.ErrorInvalidConfigurationXml, new ArgumentNullException("base64"));
			}
			VersionedXmlBase result;
			using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(base64)))
			{
				result = VersionedXmlBase.Deserialize(memoryStream);
			}
			return result;
		}

		// Token: 0x06008274 RID: 33396 RVA: 0x00239B24 File Offset: 0x00237D24
		public override string ToString()
		{
			return VersionedXmlBase.GetXmlString(this);
		}

		// Token: 0x06008275 RID: 33397 RVA: 0x00239B2C File Offset: 0x00237D2C
		public string ToBase64String()
		{
			return VersionedXmlBase.GetBase64XmlString(this);
		}

		// Token: 0x06008276 RID: 33398 RVA: 0x00239B34 File Offset: 0x00237D34
		private static void Serialize(Stream stream, object obj)
		{
			VersionedXmlTypeFactory.GetXmlSerializer(obj.GetType()).Serialize(stream, obj);
		}

		// Token: 0x06008277 RID: 33399 RVA: 0x00239B48 File Offset: 0x00237D48
		private static object Deserialize(Type type, Stream stream)
		{
			XmlReaderSettings xmlReaderSettings = null;
			XmlSchema xmlSchema = null;
			XmlSerializer xmlSerializer = VersionedXmlTypeFactory.GetXmlSerializer(type, out xmlSchema);
			if (xmlSchema != null)
			{
				xmlReaderSettings = new XmlReaderSettings();
				xmlReaderSettings.ValidationType = ValidationType.Schema;
				xmlReaderSettings.ValidationFlags = (XmlSchemaValidationFlags.ProcessInlineSchema | XmlSchemaValidationFlags.ProcessSchemaLocation | XmlSchemaValidationFlags.ReportValidationWarnings | XmlSchemaValidationFlags.ProcessIdentityConstraints | XmlSchemaValidationFlags.AllowXmlAttributes);
				xmlReaderSettings.Schemas.Add(xmlSchema);
			}
			object result;
			using (XmlReader xmlReader = XmlReader.Create(stream, xmlReaderSettings))
			{
				result = xmlSerializer.Deserialize(xmlReader);
			}
			return result;
		}
	}
}
