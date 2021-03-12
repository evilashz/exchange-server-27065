using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001BD RID: 445
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class TrackingPropertyType
	{
		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x0002519E File Offset: 0x0002339E
		// (set) Token: 0x0600132C RID: 4908 RVA: 0x000251A6 File Offset: 0x000233A6
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

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x000251AF File Offset: 0x000233AF
		// (set) Token: 0x0600132E RID: 4910 RVA: 0x000251B7 File Offset: 0x000233B7
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

		// Token: 0x04000D55 RID: 3413
		private string nameField;

		// Token: 0x04000D56 RID: 3414
		private string valueField;
	}
}
