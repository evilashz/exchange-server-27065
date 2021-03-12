using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000ED RID: 237
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class MigrationDetailValue
	{
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0001FC48 File Offset: 0x0001DE48
		// (set) Token: 0x0600076A RID: 1898 RVA: 0x0001FC50 File Offset: 0x0001DE50
		public XmlElement Data
		{
			get
			{
				return this.dataField;
			}
			set
			{
				this.dataField = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0001FC59 File Offset: 0x0001DE59
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x0001FC61 File Offset: 0x0001DE61
		[XmlAttribute]
		public int Bits
		{
			get
			{
				return this.bitsField;
			}
			set
			{
				this.bitsField = value;
			}
		}

		// Token: 0x040003CD RID: 973
		private XmlElement dataField;

		// Token: 0x040003CE RID: 974
		private int bitsField;
	}
}
