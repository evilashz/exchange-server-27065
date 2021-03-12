using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008BD RID: 2237
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class XmlValueDirSyncStatus
	{
		// Token: 0x17002758 RID: 10072
		// (get) Token: 0x06006E90 RID: 28304 RVA: 0x0017648E File Offset: 0x0017468E
		// (set) Token: 0x06006E91 RID: 28305 RVA: 0x00176496 File Offset: 0x00174696
		[XmlElement(Order = 0)]
		public DirSyncStatusValue DirSyncStatus
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

		// Token: 0x040047E1 RID: 18401
		private DirSyncStatusValue dirSyncStatusField;
	}
}
