using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x02000191 RID: 401
	[XmlRoot(Namespace = "HMMAIL:", IsNullable = false)]
	[XmlType(AnonymousType = true, Namespace = "HMMAIL:")]
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[Serializable]
	public class FolderId
	{
		// Token: 0x06000B47 RID: 2887 RVA: 0x0001DD3F File Offset: 0x0001BF3F
		public FolderId()
		{
			this.isClientIdField = 0;
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0001DD4E File Offset: 0x0001BF4E
		// (set) Token: 0x06000B49 RID: 2889 RVA: 0x0001DD56 File Offset: 0x0001BF56
		[XmlAttribute]
		[DefaultValue(typeof(byte), "0")]
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

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x0001DD5F File Offset: 0x0001BF5F
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x0001DD67 File Offset: 0x0001BF67
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

		// Token: 0x04000662 RID: 1634
		private byte isClientIdField;

		// Token: 0x04000663 RID: 1635
		private string valueField;
	}
}
