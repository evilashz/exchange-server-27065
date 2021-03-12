using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x02000171 RID: 369
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[XmlType(AnonymousType = true, Namespace = "HMMAIL:")]
	[XmlRoot(Namespace = "HMMAIL:", IsNullable = false)]
	[DesignerCategory("code")]
	[Serializable]
	public class FolderId
	{
		// Token: 0x06000AB5 RID: 2741 RVA: 0x0001D844 File Offset: 0x0001BA44
		public FolderId()
		{
			this.isClientIdField = 0;
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x0001D853 File Offset: 0x0001BA53
		// (set) Token: 0x06000AB7 RID: 2743 RVA: 0x0001D85B File Offset: 0x0001BA5B
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

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x0001D864 File Offset: 0x0001BA64
		// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x0001D86C File Offset: 0x0001BA6C
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

		// Token: 0x040005F2 RID: 1522
		private byte isClientIdField;

		// Token: 0x040005F3 RID: 1523
		private string valueField;
	}
}
