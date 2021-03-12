using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000F5 RID: 245
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class XmlValueDirSyncStatus
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x0001FD6E File Offset: 0x0001DF6E
		// (set) Token: 0x0600078D RID: 1933 RVA: 0x0001FD76 File Offset: 0x0001DF76
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

		// Token: 0x040003E1 RID: 993
		private DirSyncStatusValue dirSyncStatusField;
	}
}
