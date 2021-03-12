using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.Xop
{
	// Token: 0x020001C7 RID: 455
	[XmlRoot(ElementName = "Include", Namespace = "http://www.w3.org/2004/08/xop/include", IsNullable = false)]
	[XmlType(TypeName = "Include", Namespace = "http://www.w3.org/2004/08/xop/include")]
	[Serializable]
	public class Include
	{
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0001EFF2 File Offset: 0x0001D1F2
		// (set) Token: 0x06000CD8 RID: 3288 RVA: 0x0001EFFA File Offset: 0x0001D1FA
		[XmlIgnore]
		public string href
		{
			get
			{
				return this.internalhref;
			}
			set
			{
				this.internalhref = value;
			}
		}

		// Token: 0x04000736 RID: 1846
		[XmlAttribute(AttributeName = "href", Form = XmlSchemaForm.Unqualified, DataType = "anyURI", Namespace = "http://www.w3.org/2004/08/xop/include")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalhref;

		// Token: 0x04000737 RID: 1847
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr;

		// Token: 0x04000738 RID: 1848
		[XmlAnyElement]
		public XmlElement[] Any;
	}
}
