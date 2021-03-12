using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000A8 RID: 168
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class PeriodType
	{
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x0001FB3B File Offset: 0x0001DD3B
		// (set) Token: 0x060008F5 RID: 2293 RVA: 0x0001FB43 File Offset: 0x0001DD43
		[XmlAttribute(DataType = "duration")]
		public string Bias
		{
			get
			{
				return this.biasField;
			}
			set
			{
				this.biasField = value;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x0001FB4C File Offset: 0x0001DD4C
		// (set) Token: 0x060008F7 RID: 2295 RVA: 0x0001FB54 File Offset: 0x0001DD54
		[XmlAttribute]
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x0001FB5D File Offset: 0x0001DD5D
		// (set) Token: 0x060008F9 RID: 2297 RVA: 0x0001FB65 File Offset: 0x0001DD65
		[XmlAttribute]
		public string Id
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

		// Token: 0x04000363 RID: 867
		private string biasField;

		// Token: 0x04000364 RID: 868
		private string nameField;

		// Token: 0x04000365 RID: 869
		private string idField;
	}
}
