using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200036D RID: 877
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class CreateManagedFolderRequestType : BaseRequestType
	{
		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x00029B92 File Offset: 0x00027D92
		// (set) Token: 0x06001BF5 RID: 7157 RVA: 0x00029B9A File Offset: 0x00027D9A
		[XmlArrayItem("FolderName", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] FolderNames
		{
			get
			{
				return this.folderNamesField;
			}
			set
			{
				this.folderNamesField = value;
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x00029BA3 File Offset: 0x00027DA3
		// (set) Token: 0x06001BF7 RID: 7159 RVA: 0x00029BAB File Offset: 0x00027DAB
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

		// Token: 0x04001294 RID: 4756
		private string[] folderNamesField;

		// Token: 0x04001295 RID: 4757
		private EmailAddressType mailboxField;
	}
}
