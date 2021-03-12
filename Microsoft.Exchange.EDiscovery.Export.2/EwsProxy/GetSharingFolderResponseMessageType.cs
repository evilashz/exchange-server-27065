using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001E2 RID: 482
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetSharingFolderResponseMessageType : ResponseMessageType
	{
		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x000257DD File Offset: 0x000239DD
		// (set) Token: 0x060013EA RID: 5098 RVA: 0x000257E5 File Offset: 0x000239E5
		public FolderIdType SharingFolderId
		{
			get
			{
				return this.sharingFolderIdField;
			}
			set
			{
				this.sharingFolderIdField = value;
			}
		}

		// Token: 0x04000DC0 RID: 3520
		private FolderIdType sharingFolderIdField;
	}
}
