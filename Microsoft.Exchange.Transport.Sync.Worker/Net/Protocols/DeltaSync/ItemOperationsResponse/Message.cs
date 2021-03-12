using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.Xop;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsResponse
{
	// Token: 0x020000CF RID: 207
	[XmlType(TypeName = "Message", Namespace = "ItemOperations:")]
	[Serializable]
	public class Message
	{
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x0001A582 File Offset: 0x00018782
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x0001A58A File Offset: 0x0001878A
		internal Stream EmailMessage
		{
			get
			{
				return this.emailMessage;
			}
			set
			{
				this.emailMessage = value;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0001A593 File Offset: 0x00018793
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x0001A5AE File Offset: 0x000187AE
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

		// Token: 0x040003C2 RID: 962
		private Stream emailMessage;

		// Token: 0x040003C3 RID: 963
		[XmlElement(Type = typeof(Include), ElementName = "Include", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/2004/08/xop/include")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Include internalInclude;
	}
}
