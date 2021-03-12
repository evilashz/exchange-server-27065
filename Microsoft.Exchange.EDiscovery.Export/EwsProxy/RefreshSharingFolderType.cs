using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200035B RID: 859
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class RefreshSharingFolderType : BaseRequestType
	{
		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06001B95 RID: 7061 RVA: 0x00029873 File Offset: 0x00027A73
		// (set) Token: 0x06001B96 RID: 7062 RVA: 0x0002987B File Offset: 0x00027A7B
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

		// Token: 0x0400126A RID: 4714
		private FolderIdType sharingFolderIdField;
	}
}
