using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.Xop;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SendRequest
{
	// Token: 0x020000D9 RID: 217
	[XmlType(TypeName = "Item", Namespace = "Send:")]
	[Serializable]
	public class Item
	{
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x0001A90E File Offset: 0x00018B0E
		// (set) Token: 0x0600072D RID: 1837 RVA: 0x0001A929 File Offset: 0x00018B29
		[XmlIgnore]
		public Include Include
		{
			get
			{
				if (this.internalInclude == null)
				{
					this.internalInclude = new Include();
				}
				return this.internalInclude;
			}
			set
			{
				this.internalInclude = value;
			}
		}

		// Token: 0x040003DC RID: 988
		[XmlElement(Type = typeof(Include), ElementName = "Include", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/2004/08/xop/include")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Include internalInclude;
	}
}
