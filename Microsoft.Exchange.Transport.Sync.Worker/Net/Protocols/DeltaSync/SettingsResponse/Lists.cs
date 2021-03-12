using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000162 RID: 354
	[XmlType(TypeName = "Lists", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class Lists
	{
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0001D462 File Offset: 0x0001B662
		// (set) Token: 0x06000A65 RID: 2661 RVA: 0x0001D46A File Offset: 0x0001B66A
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

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x0001D47A File Offset: 0x0001B67A
		// (set) Token: 0x06000A67 RID: 2663 RVA: 0x0001D495 File Offset: 0x0001B695
		[XmlIgnore]
		public ListsGet Get
		{
			get
			{
				if (this.internalGet == null)
				{
					this.internalGet = new ListsGet();
				}
				return this.internalGet;
			}
			set
			{
				this.internalGet = value;
			}
		}

		// Token: 0x040005C5 RID: 1477
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalStatus;

		// Token: 0x040005C6 RID: 1478
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalStatusSpecified;

		// Token: 0x040005C7 RID: 1479
		[XmlElement(Type = typeof(ListsGet), ElementName = "Get", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ListsGet internalGet;
	}
}
