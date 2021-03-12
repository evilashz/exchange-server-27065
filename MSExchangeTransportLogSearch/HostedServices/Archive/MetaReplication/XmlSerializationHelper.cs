using System;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;

namespace Microsoft.Exchange.HostedServices.Archive.MetaReplication
{
	// Token: 0x02000049 RID: 73
	public static class XmlSerializationHelper
	{
		// Token: 0x0600019E RID: 414 RVA: 0x0000B75C File Offset: 0x0000995C
		public static void Serialize(object content, XmlWriter writer)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			XmlSerializationHelper.Serialize(content.GetType(), content, writer);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000B787 File Offset: 0x00009987
		public static void Serialize<T>(T content, XmlWriter writer)
		{
			XmlSerializationHelper.Serialize(typeof(T), content, writer);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000B7A0 File Offset: 0x000099A0
		public static object Deserialize(Type type, XmlReader xmlReader)
		{
			SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(type);
			return safeXmlSerializer.Deserialize(xmlReader);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000B7BC File Offset: 0x000099BC
		private static void Serialize(Type type, object content, XmlWriter writer)
		{
			SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(type);
			safeXmlSerializer.Serialize(writer, content);
		}
	}
}
