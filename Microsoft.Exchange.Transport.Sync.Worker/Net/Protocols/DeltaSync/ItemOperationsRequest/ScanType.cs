using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsRequest
{
	// Token: 0x020000B8 RID: 184
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[XmlType(TypeName = "ScanType", Namespace = "ItemOperations:")]
	[Serializable]
	public class ScanType : ItemOpsBaseType
	{
		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0001A069 File Offset: 0x00018269
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x0001A071 File Offset: 0x00018271
		[XmlIgnore]
		public ResponseContentTypeType ResponseContentType
		{
			get
			{
				return this.internalResponseContentType;
			}
			set
			{
				this.internalResponseContentType = value;
				this.internalResponseContentTypeSpecified = true;
			}
		}

		// Token: 0x0400039B RID: 923
		[XmlElement(ElementName = "ResponseContentType", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ResponseContentTypeType internalResponseContentType;

		// Token: 0x0400039C RID: 924
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalResponseContentTypeSpecified;
	}
}
