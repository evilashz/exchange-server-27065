using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200022B RID: 555
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class SyncFolderHierarchyDeleteType
	{
		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x0002639A File Offset: 0x0002459A
		// (set) Token: 0x0600154F RID: 5455 RVA: 0x000263A2 File Offset: 0x000245A2
		public FolderIdType FolderId
		{
			get
			{
				return this.folderIdField;
			}
			set
			{
				this.folderIdField = value;
			}
		}

		// Token: 0x04000EAB RID: 3755
		private FolderIdType folderIdField;
	}
}
