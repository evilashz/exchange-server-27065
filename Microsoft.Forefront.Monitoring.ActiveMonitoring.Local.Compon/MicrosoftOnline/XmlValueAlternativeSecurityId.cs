using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000AE RID: 174
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class XmlValueAlternativeSecurityId
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0001EF93 File Offset: 0x0001D193
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x0001EF9B File Offset: 0x0001D19B
		public AlternativeSecurityIdValue Id
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		// Token: 0x0400030D RID: 781
		private AlternativeSecurityIdValue idField;
	}
}
