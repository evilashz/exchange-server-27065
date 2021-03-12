using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000EE RID: 238
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class XmlValueMigrationDetail
	{
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0001FC72 File Offset: 0x0001DE72
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x0001FC7A File Offset: 0x0001DE7A
		public MigrationDetailValue MigrationDetail
		{
			get
			{
				return this.migrationDetailField;
			}
			set
			{
				this.migrationDetailField = value;
			}
		}

		// Token: 0x040003CF RID: 975
		private MigrationDetailValue migrationDetailField;
	}
}
