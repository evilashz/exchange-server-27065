using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x0200010C RID: 268
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class ResponseMessage
	{
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0001F7E8 File Offset: 0x0001D9E8
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x0001F7F0 File Offset: 0x0001D9F0
		public string MessageText
		{
			get
			{
				return this.messageTextField;
			}
			set
			{
				this.messageTextField = value;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0001F7F9 File Offset: 0x0001D9F9
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x0001F801 File Offset: 0x0001DA01
		public string ResponseCode
		{
			get
			{
				return this.responseCodeField;
			}
			set
			{
				this.responseCodeField = value;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x0001F80A File Offset: 0x0001DA0A
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x0001F812 File Offset: 0x0001DA12
		public string DescriptiveLink
		{
			get
			{
				return this.descriptiveLinkField;
			}
			set
			{
				this.descriptiveLinkField = value;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0001F81B File Offset: 0x0001DA1B
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x0001F823 File Offset: 0x0001DA23
		[XmlAnyElement]
		public XmlNode MessageXml
		{
			get
			{
				return this.messageXmlField;
			}
			set
			{
				this.messageXmlField = value;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x0001F82C File Offset: 0x0001DA2C
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x0001F834 File Offset: 0x0001DA34
		public Path Path
		{
			get
			{
				return this.pathField;
			}
			set
			{
				this.pathField = value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x0001F83D File Offset: 0x0001DA3D
		// (set) Token: 0x06000742 RID: 1858 RVA: 0x0001F845 File Offset: 0x0001DA45
		[XmlAttribute]
		public ResponseClassType ResponseClass
		{
			get
			{
				return this.responseClassField;
			}
			set
			{
				this.responseClassField = value;
			}
		}

		// Token: 0x04000459 RID: 1113
		public const string ExceptionTypeNodeName = "ExceptionType";

		// Token: 0x0400045A RID: 1114
		public const string ExceptionCodeNodeName = "ExceptionCode";

		// Token: 0x0400045B RID: 1115
		public const string ExceptionServerNodeName = "ExceptionServerName";

		// Token: 0x0400045C RID: 1116
		public const string ExceptionMessageNodeName = "ExceptionMessage";

		// Token: 0x0400045D RID: 1117
		private string messageTextField;

		// Token: 0x0400045E RID: 1118
		private string responseCodeField;

		// Token: 0x0400045F RID: 1119
		private string descriptiveLinkField;

		// Token: 0x04000460 RID: 1120
		private XmlNode messageXmlField;

		// Token: 0x04000461 RID: 1121
		private Path pathField;

		// Token: 0x04000462 RID: 1122
		private ResponseClassType responseClassField;
	}
}
