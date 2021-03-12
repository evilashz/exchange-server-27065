using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x02000190 RID: 400
	[XmlRoot(Namespace = "HMFOLDER:", IsNullable = false)]
	[XmlType(AnonymousType = true, Namespace = "HMFOLDER:")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ParentId
	{
		// Token: 0x06000B42 RID: 2882 RVA: 0x0001DD0E File Offset: 0x0001BF0E
		public ParentId()
		{
			this.isClientIdField = 0;
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x0001DD1D File Offset: 0x0001BF1D
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x0001DD25 File Offset: 0x0001BF25
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

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x0001DD2E File Offset: 0x0001BF2E
		// (set) Token: 0x06000B46 RID: 2886 RVA: 0x0001DD36 File Offset: 0x0001BF36
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

		// Token: 0x04000660 RID: 1632
		private byte isClientIdField;

		// Token: 0x04000661 RID: 1633
		private string valueField;
	}
}
