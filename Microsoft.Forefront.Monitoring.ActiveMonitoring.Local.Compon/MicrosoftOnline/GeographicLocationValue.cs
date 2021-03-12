using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000F1 RID: 241
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class GeographicLocationValue
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x0001FCDF File Offset: 0x0001DEDF
		// (set) Token: 0x0600077C RID: 1916 RVA: 0x0001FCE7 File Offset: 0x0001DEE7
		[XmlAttribute]
		public string Region
		{
			get
			{
				return this.regionField;
			}
			set
			{
				this.regionField = value;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x0001FCF0 File Offset: 0x0001DEF0
		// (set) Token: 0x0600077E RID: 1918 RVA: 0x0001FCF8 File Offset: 0x0001DEF8
		[XmlAttribute]
		public string Country
		{
			get
			{
				return this.countryField;
			}
			set
			{
				this.countryField = value;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x0001FD01 File Offset: 0x0001DF01
		// (set) Token: 0x06000780 RID: 1920 RVA: 0x0001FD09 File Offset: 0x0001DF09
		[XmlAttribute]
		public string State
		{
			get
			{
				return this.stateField;
			}
			set
			{
				this.stateField = value;
			}
		}

		// Token: 0x040003D4 RID: 980
		private string regionField;

		// Token: 0x040003D5 RID: 981
		private string countryField;

		// Token: 0x040003D6 RID: 982
		private string stateField;
	}
}
