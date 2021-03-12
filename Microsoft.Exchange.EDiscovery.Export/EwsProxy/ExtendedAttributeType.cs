using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001A1 RID: 417
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class ExtendedAttributeType
	{
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x000244FB File Offset: 0x000226FB
		// (set) Token: 0x060011AE RID: 4526 RVA: 0x00024503 File Offset: 0x00022703
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

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x0002450C File Offset: 0x0002270C
		// (set) Token: 0x060011B0 RID: 4528 RVA: 0x00024514 File Offset: 0x00022714
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

		// Token: 0x04000C1F RID: 3103
		private string nameField;

		// Token: 0x04000C20 RID: 3104
		private string valueField;
	}
}
