using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000E5 RID: 229
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class PathToUnindexedFieldType : BasePathToElementType
	{
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x000206B8 File Offset: 0x0001E8B8
		// (set) Token: 0x06000A52 RID: 2642 RVA: 0x000206C0 File Offset: 0x0001E8C0
		[XmlAttribute]
		public UnindexedFieldURIType FieldURI
		{
			get
			{
				return this.fieldURIField;
			}
			set
			{
				this.fieldURIField = value;
			}
		}

		// Token: 0x0400062F RID: 1583
		private UnindexedFieldURIType fieldURIField;
	}
}
