using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000368 RID: 872
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class SyncFolderHierarchyType : BaseRequestType
	{
		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x00029AD1 File Offset: 0x00027CD1
		// (set) Token: 0x06001BDE RID: 7134 RVA: 0x00029AD9 File Offset: 0x00027CD9
		public FolderResponseShapeType FolderShape
		{
			get
			{
				return this.folderShapeField;
			}
			set
			{
				this.folderShapeField = value;
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x00029AE2 File Offset: 0x00027CE2
		// (set) Token: 0x06001BE0 RID: 7136 RVA: 0x00029AEA File Offset: 0x00027CEA
		public TargetFolderIdType SyncFolderId
		{
			get
			{
				return this.syncFolderIdField;
			}
			set
			{
				this.syncFolderIdField = value;
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x00029AF3 File Offset: 0x00027CF3
		// (set) Token: 0x06001BE2 RID: 7138 RVA: 0x00029AFB File Offset: 0x00027CFB
		public string SyncState
		{
			get
			{
				return this.syncStateField;
			}
			set
			{
				this.syncStateField = value;
			}
		}

		// Token: 0x0400128B RID: 4747
		private FolderResponseShapeType folderShapeField;

		// Token: 0x0400128C RID: 4748
		private TargetFolderIdType syncFolderIdField;

		// Token: 0x0400128D RID: 4749
		private string syncStateField;
	}
}
