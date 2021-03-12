using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x0200012D RID: 301
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[XmlInclude(typeof(GetMailTipsResponseMessageType))]
	[XmlInclude(typeof(MailTipsResponseMessageType))]
	[DebuggerStepThrough]
	[Serializable]
	public class ResponseMessageType
	{
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x00024E82 File Offset: 0x00023082
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x00024E8A File Offset: 0x0002308A
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

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x00024E93 File Offset: 0x00023093
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x00024E9B File Offset: 0x0002309B
		public ResponseCodeType ResponseCode
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

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x00024EA4 File Offset: 0x000230A4
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x00024EAC File Offset: 0x000230AC
		[XmlIgnore]
		public bool ResponseCodeSpecified
		{
			get
			{
				return this.responseCodeFieldSpecified;
			}
			set
			{
				this.responseCodeFieldSpecified = value;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x00024EB5 File Offset: 0x000230B5
		// (set) Token: 0x0600082B RID: 2091 RVA: 0x00024EBD File Offset: 0x000230BD
		public int DescriptiveLinkKey
		{
			get
			{
				return this.descriptiveLinkKeyField;
			}
			set
			{
				this.descriptiveLinkKeyField = value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x00024EC6 File Offset: 0x000230C6
		// (set) Token: 0x0600082D RID: 2093 RVA: 0x00024ECE File Offset: 0x000230CE
		[XmlIgnore]
		public bool DescriptiveLinkKeySpecified
		{
			get
			{
				return this.descriptiveLinkKeyFieldSpecified;
			}
			set
			{
				this.descriptiveLinkKeyFieldSpecified = value;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x00024ED7 File Offset: 0x000230D7
		// (set) Token: 0x0600082F RID: 2095 RVA: 0x00024EDF File Offset: 0x000230DF
		public ResponseMessageTypeMessageXml MessageXml
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

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x00024EE8 File Offset: 0x000230E8
		// (set) Token: 0x06000831 RID: 2097 RVA: 0x00024EF0 File Offset: 0x000230F0
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

		// Token: 0x04000656 RID: 1622
		private string messageTextField;

		// Token: 0x04000657 RID: 1623
		private ResponseCodeType responseCodeField;

		// Token: 0x04000658 RID: 1624
		private bool responseCodeFieldSpecified;

		// Token: 0x04000659 RID: 1625
		private int descriptiveLinkKeyField;

		// Token: 0x0400065A RID: 1626
		private bool descriptiveLinkKeyFieldSpecified;

		// Token: 0x0400065B RID: 1627
		private ResponseMessageTypeMessageXml messageXmlField;

		// Token: 0x0400065C RID: 1628
		private ResponseClassType responseClassField;
	}
}
