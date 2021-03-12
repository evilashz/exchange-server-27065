using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000C4 RID: 196
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class AuthorizedPartyValue
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x0001F27F File Offset: 0x0001D47F
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x0001F287 File Offset: 0x0001D487
		[XmlAttribute]
		public string ForeignContextId
		{
			get
			{
				return this.foreignContextIdField;
			}
			set
			{
				this.foreignContextIdField = value;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x0001F290 File Offset: 0x0001D490
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x0001F298 File Offset: 0x0001D498
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

		// Token: 0x04000343 RID: 835
		private string foreignContextIdField;

		// Token: 0x04000344 RID: 836
		private string foreignPrincipalIdField;
	}
}
