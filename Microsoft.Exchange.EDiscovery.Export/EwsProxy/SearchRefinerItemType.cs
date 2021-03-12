using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001A6 RID: 422
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SearchRefinerItemType
	{
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x0002476E File Offset: 0x0002296E
		// (set) Token: 0x060011F8 RID: 4600 RVA: 0x00024776 File Offset: 0x00022976
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

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x0002477F File Offset: 0x0002297F
		// (set) Token: 0x060011FA RID: 4602 RVA: 0x00024787 File Offset: 0x00022987
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

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x00024790 File Offset: 0x00022990
		// (set) Token: 0x060011FC RID: 4604 RVA: 0x00024798 File Offset: 0x00022998
		public long Count
		{
			get
			{
				return this.countField;
			}
			set
			{
				this.countField = value;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x000247A1 File Offset: 0x000229A1
		// (set) Token: 0x060011FE RID: 4606 RVA: 0x000247A9 File Offset: 0x000229A9
		public string Token
		{
			get
			{
				return this.tokenField;
			}
			set
			{
				this.tokenField = value;
			}
		}

		// Token: 0x04000C45 RID: 3141
		private string nameField;

		// Token: 0x04000C46 RID: 3142
		private string valueField;

		// Token: 0x04000C47 RID: 3143
		private long countField;

		// Token: 0x04000C48 RID: 3144
		private string tokenField;
	}
}
