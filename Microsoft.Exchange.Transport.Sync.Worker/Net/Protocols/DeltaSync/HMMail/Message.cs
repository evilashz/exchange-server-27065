using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.Xop;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail
{
	// Token: 0x0200009F RID: 159
	[XmlRoot(ElementName = "Message", Namespace = "HMMAIL:", IsNullable = false)]
	[Serializable]
	public class Message
	{
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x00019A1B File Offset: 0x00017C1B
		// (set) Token: 0x06000608 RID: 1544 RVA: 0x00019A36 File Offset: 0x00017C36
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

		// Token: 0x04000378 RID: 888
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Include), ElementName = "Include", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/2004/08/xop/include")]
		public Include internalInclude;
	}
}
