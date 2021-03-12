using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000006 RID: 6
	[XmlType(Namespace = "http://domains.live.com/Service/DomainServices/V1.0")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class Property
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000068CB File Offset: 0x00004ACB
		// (set) Token: 0x0600018B RID: 395 RVA: 0x000068D3 File Offset: 0x00004AD3
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

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000068DC File Offset: 0x00004ADC
		// (set) Token: 0x0600018D RID: 397 RVA: 0x000068E4 File Offset: 0x00004AE4
		public string Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x04000067 RID: 103
		private string nameField;

		// Token: 0x04000068 RID: 104
		private string valueField;
	}
}
