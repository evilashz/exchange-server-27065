using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000EF RID: 239
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class LicenseUnitsDetailValue
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x0001FC8B File Offset: 0x0001DE8B
		// (set) Token: 0x06000772 RID: 1906 RVA: 0x0001FC93 File Offset: 0x0001DE93
		[XmlAttribute]
		public int Enabled
		{
			get
			{
				return this.enabledField;
			}
			set
			{
				this.enabledField = value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x0001FC9C File Offset: 0x0001DE9C
		// (set) Token: 0x06000774 RID: 1908 RVA: 0x0001FCA4 File Offset: 0x0001DEA4
		[XmlAttribute]
		public int Warning
		{
			get
			{
				return this.warningField;
			}
			set
			{
				this.warningField = value;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x0001FCAD File Offset: 0x0001DEAD
		// (set) Token: 0x06000776 RID: 1910 RVA: 0x0001FCB5 File Offset: 0x0001DEB5
		[XmlAttribute]
		public int Suspended
		{
			get
			{
				return this.suspendedField;
			}
			set
			{
				this.suspendedField = value;
			}
		}

		// Token: 0x040003D0 RID: 976
		private int enabledField;

		// Token: 0x040003D1 RID: 977
		private int warningField;

		// Token: 0x040003D2 RID: 978
		private int suspendedField;
	}
}
