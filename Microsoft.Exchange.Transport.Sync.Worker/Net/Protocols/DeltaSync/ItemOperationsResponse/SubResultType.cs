using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsResponse
{
	// Token: 0x020000C9 RID: 201
	[XmlType(TypeName = "SubResultType", Namespace = "ItemOperations:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class SubResultType
	{
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0001A384 File Offset: 0x00018584
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x0001A38C File Offset: 0x0001858C
		[XmlIgnore]
		public int Status
		{
			get
			{
				return this.internalStatus;
			}
			set
			{
				this.internalStatus = value;
				this.internalStatusSpecified = true;
			}
		}

		// Token: 0x040003AE RID: 942
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "ItemOperations:")]
		public int internalStatus;

		// Token: 0x040003AF RID: 943
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalStatusSpecified;
	}
}
