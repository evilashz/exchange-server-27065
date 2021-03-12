using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000106 RID: 262
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class XmlValueCompanyPartnership
	{
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00020010 File Offset: 0x0001E210
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x00020018 File Offset: 0x0001E218
		[XmlArrayItem("Partnership", IsNullable = false)]
		public PartnershipValue[] Partnerships
		{
			get
			{
				return this.partnershipsField;
			}
			set
			{
				this.partnershipsField = value;
			}
		}

		// Token: 0x0400040B RID: 1035
		private PartnershipValue[] partnershipsField;
	}
}
