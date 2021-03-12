using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000CB RID: 203
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class SupportRoleValue
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0001F383 File Offset: 0x0001D583
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x0001F38B File Offset: 0x0001D58B
		[XmlAttribute]
		public string RoleId
		{
			get
			{
				return this.roleIdField;
			}
			set
			{
				this.roleIdField = value;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0001F394 File Offset: 0x0001D594
		// (set) Token: 0x0600066E RID: 1646 RVA: 0x0001F39C File Offset: 0x0001D59C
		[XmlAttribute]
		public string ForeignPrincipalName
		{
			get
			{
				return this.foreignPrincipalNameField;
			}
			set
			{
				this.foreignPrincipalNameField = value;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x0001F3A5 File Offset: 0x0001D5A5
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x0001F3AD File Offset: 0x0001D5AD
		[XmlAttribute]
		public string ForeignPrincipalId
		{
			get
			{
				return this.foreignPrincipalIdField;
			}
			set
			{
				this.foreignPrincipalIdField = value;
			}
		}

		// Token: 0x0400034F RID: 847
		private string roleIdField;

		// Token: 0x04000350 RID: 848
		private string foreignPrincipalNameField;

		// Token: 0x04000351 RID: 849
		private string foreignPrincipalIdField;
	}
}
