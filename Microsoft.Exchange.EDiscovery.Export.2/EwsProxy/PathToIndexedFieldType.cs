using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000E3 RID: 227
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class PathToIndexedFieldType : BasePathToElementType
	{
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0002068E File Offset: 0x0001E88E
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x00020696 File Offset: 0x0001E896
		[XmlAttribute]
		public DictionaryURIType FieldURI
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

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x0002069F File Offset: 0x0001E89F
		// (set) Token: 0x06000A4F RID: 2639 RVA: 0x000206A7 File Offset: 0x0001E8A7
		[XmlAttribute]
		public string FieldIndex
		{
			get
			{
				return this.fieldIndexField;
			}
			set
			{
				this.fieldIndexField = value;
			}
		}

		// Token: 0x04000622 RID: 1570
		private DictionaryURIType fieldURIField;

		// Token: 0x04000623 RID: 1571
		private string fieldIndexField;
	}
}
