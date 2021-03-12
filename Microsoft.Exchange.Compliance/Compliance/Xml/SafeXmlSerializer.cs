using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Compliance.Xml
{
	// Token: 0x02000009 RID: 9
	internal class SafeXmlSerializer : XmlSerializer
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00003FB4 File Offset: 0x000021B4
		public SafeXmlSerializer()
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003FBC File Offset: 0x000021BC
		public SafeXmlSerializer(Type type) : base(type)
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003FC5 File Offset: 0x000021C5
		public SafeXmlSerializer(XmlTypeMapping xmlTypeMapping) : base(xmlTypeMapping)
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003FCE File Offset: 0x000021CE
		public SafeXmlSerializer(Type type, string defaultNamespace) : base(type, defaultNamespace)
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003FD8 File Offset: 0x000021D8
		public SafeXmlSerializer(Type type, Type[] extraTypes) : base(type, extraTypes)
		{
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003FE2 File Offset: 0x000021E2
		public SafeXmlSerializer(Type type, XmlAttributeOverrides overrides) : base(type, overrides)
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003FEC File Offset: 0x000021EC
		public SafeXmlSerializer(Type type, XmlRootAttribute root) : base(type, root)
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003FF6 File Offset: 0x000021F6
		public SafeXmlSerializer(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace) : base(type, overrides, extraTypes, root, defaultNamespace)
		{
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00004008 File Offset: 0x00002208
		public new object Deserialize(Stream stream)
		{
			return base.Deserialize(new XmlTextReader(stream)
			{
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			});
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00004034 File Offset: 0x00002234
		public new object Deserialize(TextReader txtReader)
		{
			return base.Deserialize(new XmlTextReader(txtReader)
			{
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			});
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000405D File Offset: 0x0000225D
		public new object Deserialize(XmlReader reader)
		{
			if (reader.Settings != null && reader.Settings.DtdProcessing != DtdProcessing.Prohibit)
			{
				throw new XmlDtdException();
			}
			return base.Deserialize(reader);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00004081 File Offset: 0x00002281
		public new object Deserialize(XmlReader reader, string encodingStyle)
		{
			if (reader.Settings != null && reader.Settings.DtdProcessing != DtdProcessing.Prohibit)
			{
				throw new XmlDtdException();
			}
			return base.Deserialize(reader, encodingStyle);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000040A6 File Offset: 0x000022A6
		public new object Deserialize(XmlReader reader, XmlDeserializationEvents events)
		{
			if (reader.Settings != null && reader.Settings.DtdProcessing != DtdProcessing.Prohibit)
			{
				throw new XmlDtdException();
			}
			return base.Deserialize(reader, events);
		}
	}
}
