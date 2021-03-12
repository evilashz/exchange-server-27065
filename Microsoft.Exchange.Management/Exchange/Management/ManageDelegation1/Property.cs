using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.ManageDelegation1
{
	// Token: 0x02000DAD RID: 3501
	[XmlType(Namespace = "http://domains.live.com/Service/ManageDelegation/V1.0")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class Property
	{
		// Token: 0x170029BD RID: 10685
		// (get) Token: 0x0600861B RID: 34331 RVA: 0x00224F23 File Offset: 0x00223123
		// (set) Token: 0x0600861C RID: 34332 RVA: 0x00224F2B File Offset: 0x0022312B
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

		// Token: 0x170029BE RID: 10686
		// (get) Token: 0x0600861D RID: 34333 RVA: 0x00224F34 File Offset: 0x00223134
		// (set) Token: 0x0600861E RID: 34334 RVA: 0x00224F3C File Offset: 0x0022313C
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

		// Token: 0x0400413C RID: 16700
		private string nameField;

		// Token: 0x0400413D RID: 16701
		private string valueField;
	}
}
