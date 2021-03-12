using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008BC RID: 2236
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ContextDirSyncStatus
	{
		// Token: 0x17002756 RID: 10070
		// (get) Token: 0x06006E8B RID: 28299 RVA: 0x00176464 File Offset: 0x00174664
		// (set) Token: 0x06006E8C RID: 28300 RVA: 0x0017646C File Offset: 0x0017466C
		[XmlElement(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11", Order = 0)]
		public DirectoryPropertyXmlDirSyncStatus DirSyncStatus
		{
			get
			{
				return this.dirSyncStatusField;
			}
			set
			{
				this.dirSyncStatusField = value;
			}
		}

		// Token: 0x17002757 RID: 10071
		// (get) Token: 0x06006E8D RID: 28301 RVA: 0x00176475 File Offset: 0x00174675
		// (set) Token: 0x06006E8E RID: 28302 RVA: 0x0017647D File Offset: 0x0017467D
		[XmlAttribute]
		public string ContextId
		{
			get
			{
				return this.contextIdField;
			}
			set
			{
				this.contextIdField = value;
			}
		}

		// Token: 0x040047DF RID: 18399
		private DirectoryPropertyXmlDirSyncStatus dirSyncStatusField;

		// Token: 0x040047E0 RID: 18400
		private string contextIdField;
	}
}
