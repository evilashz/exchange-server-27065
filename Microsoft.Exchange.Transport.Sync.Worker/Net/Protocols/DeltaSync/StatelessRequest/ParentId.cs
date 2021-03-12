using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x02000170 RID: 368
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[XmlType(AnonymousType = true, Namespace = "HMFOLDER:")]
	[XmlRoot(Namespace = "HMFOLDER:", IsNullable = false)]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class ParentId
	{
		// Token: 0x06000AB0 RID: 2736 RVA: 0x0001D813 File Offset: 0x0001BA13
		public ParentId()
		{
			this.isClientIdField = 0;
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x0001D822 File Offset: 0x0001BA22
		// (set) Token: 0x06000AB2 RID: 2738 RVA: 0x0001D82A File Offset: 0x0001BA2A
		[DefaultValue(typeof(byte), "0")]
		[XmlAttribute]
		public byte isClientId
		{
			get
			{
				return this.isClientIdField;
			}
			set
			{
				this.isClientIdField = value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x0001D833 File Offset: 0x0001BA33
		// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x0001D83B File Offset: 0x0001BA3B
		[XmlText]
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

		// Token: 0x040005F0 RID: 1520
		private byte isClientIdField;

		// Token: 0x040005F1 RID: 1521
		private string valueField;
	}
}
