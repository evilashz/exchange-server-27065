using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000103 RID: 259
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class CompanyVerifiedDomainValue : CompanyDomainValue
	{
		// Token: 0x060007CE RID: 1998 RVA: 0x0001FFA7 File Offset: 0x0001E1A7
		public CompanyVerifiedDomainValue()
		{
			this.defaultField = false;
			this.initialField = false;
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x0001FFBD File Offset: 0x0001E1BD
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x0001FFC5 File Offset: 0x0001E1C5
		[XmlAttribute]
		[DefaultValue(false)]
		public bool Default
		{
			get
			{
				return this.defaultField;
			}
			set
			{
				this.defaultField = value;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0001FFCE File Offset: 0x0001E1CE
		// (set) Token: 0x060007D2 RID: 2002 RVA: 0x0001FFD6 File Offset: 0x0001E1D6
		[XmlAttribute]
		[DefaultValue(false)]
		public bool Initial
		{
			get
			{
				return this.initialField;
			}
			set
			{
				this.initialField = value;
			}
		}

		// Token: 0x04000403 RID: 1027
		private bool defaultField;

		// Token: 0x04000404 RID: 1028
		private bool initialField;
	}
}
