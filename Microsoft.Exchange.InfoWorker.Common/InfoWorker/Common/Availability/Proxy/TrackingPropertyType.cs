using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x020002E5 RID: 741
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class TrackingPropertyType
	{
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060015AF RID: 5551 RVA: 0x00066DA1 File Offset: 0x00064FA1
		// (set) Token: 0x060015B0 RID: 5552 RVA: 0x00066DA9 File Offset: 0x00064FA9
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

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x00066DB2 File Offset: 0x00064FB2
		// (set) Token: 0x060015B2 RID: 5554 RVA: 0x00066DBA File Offset: 0x00064FBA
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

		// Token: 0x04000E2B RID: 3627
		private string nameField;

		// Token: 0x04000E2C RID: 3628
		private string valueField;
	}
}
