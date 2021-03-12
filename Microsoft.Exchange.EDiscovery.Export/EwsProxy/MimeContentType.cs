using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000123 RID: 291
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class MimeContentType
	{
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x00021E5B File Offset: 0x0002005B
		// (set) Token: 0x06000D1D RID: 3357 RVA: 0x00021E63 File Offset: 0x00020063
		[XmlAttribute]
		public string CharacterSet
		{
			get
			{
				return this.characterSetField;
			}
			set
			{
				this.characterSetField = value;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x00021E6C File Offset: 0x0002006C
		// (set) Token: 0x06000D1F RID: 3359 RVA: 0x00021E74 File Offset: 0x00020074
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

		// Token: 0x0400091A RID: 2330
		private string characterSetField;

		// Token: 0x0400091B RID: 2331
		private string valueField;
	}
}
