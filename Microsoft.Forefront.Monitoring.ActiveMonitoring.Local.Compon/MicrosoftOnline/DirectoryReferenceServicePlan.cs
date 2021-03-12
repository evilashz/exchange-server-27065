using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200010D RID: 269
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class DirectoryReferenceServicePlan : DirectoryReference
	{
		// Token: 0x060007F9 RID: 2041 RVA: 0x00020138 File Offset: 0x0001E338
		public DirectoryReferenceServicePlan()
		{
			this.targetClassField = DirectoryObjectClass.ServicePlan;
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x00020148 File Offset: 0x0001E348
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x00020150 File Offset: 0x0001E350
		[XmlAttribute]
		public DirectoryObjectClass TargetClass
		{
			get
			{
				return this.targetClassField;
			}
			set
			{
				this.targetClassField = value;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x00020159 File Offset: 0x0001E359
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x00020161 File Offset: 0x0001E361
		[XmlIgnore]
		public bool TargetClassSpecified
		{
			get
			{
				return this.targetClassFieldSpecified;
			}
			set
			{
				this.targetClassFieldSpecified = value;
			}
		}

		// Token: 0x04000418 RID: 1048
		private DirectoryObjectClass targetClassField;

		// Token: 0x04000419 RID: 1049
		private bool targetClassFieldSpecified;
	}
}
