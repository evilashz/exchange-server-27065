using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200035C RID: 860
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetSharingMetadataType : BaseRequestType
	{
		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06001B98 RID: 7064 RVA: 0x0002988C File Offset: 0x00027A8C
		// (set) Token: 0x06001B99 RID: 7065 RVA: 0x00029894 File Offset: 0x00027A94
		public FolderIdType IdOfFolderToShare
		{
			get
			{
				return this.idOfFolderToShareField;
			}
			set
			{
				this.idOfFolderToShareField = value;
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06001B9A RID: 7066 RVA: 0x0002989D File Offset: 0x00027A9D
		// (set) Token: 0x06001B9B RID: 7067 RVA: 0x000298A5 File Offset: 0x00027AA5
		public string SenderSmtpAddress
		{
			get
			{
				return this.senderSmtpAddressField;
			}
			set
			{
				this.senderSmtpAddressField = value;
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06001B9C RID: 7068 RVA: 0x000298AE File Offset: 0x00027AAE
		// (set) Token: 0x06001B9D RID: 7069 RVA: 0x000298B6 File Offset: 0x00027AB6
		[XmlArrayItem("SmtpAddress", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] Recipients
		{
			get
			{
				return this.recipientsField;
			}
			set
			{
				this.recipientsField = value;
			}
		}

		// Token: 0x0400126B RID: 4715
		private FolderIdType idOfFolderToShareField;

		// Token: 0x0400126C RID: 4716
		private string senderSmtpAddressField;

		// Token: 0x0400126D RID: 4717
		private string[] recipientsField;
	}
}
