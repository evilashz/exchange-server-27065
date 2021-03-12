using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000F3 RID: 243
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class DistinguishedFolderIdType : BaseFolderIdType
	{
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00020ED5 File Offset: 0x0001F0D5
		// (set) Token: 0x06000B47 RID: 2887 RVA: 0x00020EDD File Offset: 0x0001F0DD
		public EmailAddressType Mailbox
		{
			get
			{
				return this.mailboxField;
			}
			set
			{
				this.mailboxField = value;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x00020EE6 File Offset: 0x0001F0E6
		// (set) Token: 0x06000B49 RID: 2889 RVA: 0x00020EEE File Offset: 0x0001F0EE
		[XmlAttribute]
		public DistinguishedFolderIdNameType Id
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

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x00020EF7 File Offset: 0x0001F0F7
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x00020EFF File Offset: 0x0001F0FF
		[XmlAttribute]
		public string ChangeKey
		{
			get
			{
				return this.changeKeyField;
			}
			set
			{
				this.changeKeyField = value;
			}
		}

		// Token: 0x04000802 RID: 2050
		private EmailAddressType mailboxField;

		// Token: 0x04000803 RID: 2051
		private DistinguishedFolderIdNameType idField;

		// Token: 0x04000804 RID: 2052
		private string changeKeyField;
	}
}
