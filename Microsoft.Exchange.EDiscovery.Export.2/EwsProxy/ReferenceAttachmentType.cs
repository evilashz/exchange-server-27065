using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000128 RID: 296
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ReferenceAttachmentType : AttachmentType
	{
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x00021FCF File Offset: 0x000201CF
		// (set) Token: 0x06000D49 RID: 3401 RVA: 0x00021FD7 File Offset: 0x000201D7
		public string AttachLongPathName
		{
			get
			{
				return this.attachLongPathNameField;
			}
			set
			{
				this.attachLongPathNameField = value;
			}
		}

		// Token: 0x04000933 RID: 2355
		private string attachLongPathNameField;
	}
}
