using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000253 RID: 595
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class ClientAccessTokenType
	{
		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x00026B5F File Offset: 0x00024D5F
		// (set) Token: 0x0600163B RID: 5691 RVA: 0x00026B67 File Offset: 0x00024D67
		public string Id
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x00026B70 File Offset: 0x00024D70
		// (set) Token: 0x0600163D RID: 5693 RVA: 0x00026B78 File Offset: 0x00024D78
		public ClientAccessTokenTypeType TokenType
		{
			get
			{
				return this.tokenTypeField;
			}
			set
			{
				this.tokenTypeField = value;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x00026B81 File Offset: 0x00024D81
		// (set) Token: 0x0600163F RID: 5695 RVA: 0x00026B89 File Offset: 0x00024D89
		public string TokenValue
		{
			get
			{
				return this.tokenValueField;
			}
			set
			{
				this.tokenValueField = value;
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x00026B92 File Offset: 0x00024D92
		// (set) Token: 0x06001641 RID: 5697 RVA: 0x00026B9A File Offset: 0x00024D9A
		[XmlElement(DataType = "integer")]
		public string TTL
		{
			get
			{
				return this.tTLField;
			}
			set
			{
				this.tTLField = value;
			}
		}

		// Token: 0x04000F3B RID: 3899
		private string idField;

		// Token: 0x04000F3C RID: 3900
		private ClientAccessTokenTypeType tokenTypeField;

		// Token: 0x04000F3D RID: 3901
		private string tokenValueField;

		// Token: 0x04000F3E RID: 3902
		private string tTLField;
	}
}
