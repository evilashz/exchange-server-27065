using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Compliance.Xml;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DBA RID: 3514
	[XmlRoot(ElementName = "SharedFolderData", Namespace = "http://schemas.microsoft.com/exchange/sharing/2008")]
	[Serializable]
	public sealed class SharedFolderData
	{
		// Token: 0x17002045 RID: 8261
		// (get) Token: 0x060078B7 RID: 30903 RVA: 0x0021536B File Offset: 0x0021356B
		// (set) Token: 0x060078B8 RID: 30904 RVA: 0x00215373 File Offset: 0x00213573
		[XmlElement]
		public string DataType { get; set; }

		// Token: 0x17002046 RID: 8262
		// (get) Token: 0x060078B9 RID: 30905 RVA: 0x0021537C File Offset: 0x0021357C
		// (set) Token: 0x060078BA RID: 30906 RVA: 0x00215384 File Offset: 0x00213584
		[XmlElement]
		public string SharingUrl { get; set; }

		// Token: 0x17002047 RID: 8263
		// (get) Token: 0x060078BB RID: 30907 RVA: 0x0021538D File Offset: 0x0021358D
		// (set) Token: 0x060078BC RID: 30908 RVA: 0x00215395 File Offset: 0x00213595
		[XmlElement]
		public string FederationUri { get; set; }

		// Token: 0x17002048 RID: 8264
		// (get) Token: 0x060078BD RID: 30909 RVA: 0x0021539E File Offset: 0x0021359E
		// (set) Token: 0x060078BE RID: 30910 RVA: 0x002153A6 File Offset: 0x002135A6
		[XmlElement]
		public string FolderId { get; set; }

		// Token: 0x17002049 RID: 8265
		// (get) Token: 0x060078BF RID: 30911 RVA: 0x002153AF File Offset: 0x002135AF
		// (set) Token: 0x060078C0 RID: 30912 RVA: 0x002153B7 File Offset: 0x002135B7
		[XmlElement]
		public string SenderSmtpAddress { get; set; }

		// Token: 0x1700204A RID: 8266
		// (get) Token: 0x060078C1 RID: 30913 RVA: 0x002153C0 File Offset: 0x002135C0
		// (set) Token: 0x060078C2 RID: 30914 RVA: 0x002153C8 File Offset: 0x002135C8
		[XmlArrayItem("Recipient")]
		[XmlArray("Recipients")]
		public SharedFolderDataRecipient[] Recipients { get; set; }

		// Token: 0x060078C3 RID: 30915 RVA: 0x002153D4 File Offset: 0x002135D4
		public static SharedFolderData DeserializeFromXmlELement(XmlElement xmlElement)
		{
			new SharedFolderData();
			XmlNodeReader reader = new XmlNodeReader(xmlElement);
			SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(typeof(SharedFolderData));
			return safeXmlSerializer.Deserialize(reader) as SharedFolderData;
		}

		// Token: 0x060078C4 RID: 30916 RVA: 0x0021540C File Offset: 0x0021360C
		public XmlElement SerializeToXmlElement()
		{
			XmlDocument xmlDocument = new SafeXmlDocument();
			using (MemoryStream memoryStream = new MemoryStream())
			{
				SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(typeof(SharedFolderData));
				safeXmlSerializer.Serialize(memoryStream, this);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				xmlDocument.Load(memoryStream);
			}
			return xmlDocument.DocumentElement;
		}
	}
}
