using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200010F RID: 271
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class DirectoryReferenceContact : DirectoryReference
	{
		// Token: 0x060007FE RID: 2046 RVA: 0x0002016A File Offset: 0x0001E36A
		public DirectoryReferenceContact()
		{
			this.targetClassField = DirectoryObjectClass.Contact;
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x00020179 File Offset: 0x0001E379
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x00020181 File Offset: 0x0001E381
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

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x0002018A File Offset: 0x0001E38A
		// (set) Token: 0x06000802 RID: 2050 RVA: 0x00020192 File Offset: 0x0001E392
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

		// Token: 0x04000433 RID: 1075
		private DirectoryObjectClass targetClassField;

		// Token: 0x04000434 RID: 1076
		private bool targetClassFieldSpecified;
	}
}
