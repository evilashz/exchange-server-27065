using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x02000102 RID: 258
	[XmlType(TypeName = "List", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class List
	{
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x0001AF21 File Offset: 0x00019121
		// (set) Token: 0x060007A8 RID: 1960 RVA: 0x0001AF29 File Offset: 0x00019129
		[XmlIgnore]
		public string name
		{
			get
			{
				return this.internalname;
			}
			set
			{
				this.internalname = value;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x0001AF32 File Offset: 0x00019132
		// (set) Token: 0x060007AA RID: 1962 RVA: 0x0001AF4D File Offset: 0x0001914D
		[XmlIgnore]
		public StatusType Set
		{
			get
			{
				if (this.internalSet == null)
				{
					this.internalSet = new StatusType();
				}
				return this.internalSet;
			}
			set
			{
				this.internalSet = value;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0001AF56 File Offset: 0x00019156
		// (set) Token: 0x060007AC RID: 1964 RVA: 0x0001AF71 File Offset: 0x00019171
		[XmlIgnore]
		public StatusType Add
		{
			get
			{
				if (this.internalAdd == null)
				{
					this.internalAdd = new StatusType();
				}
				return this.internalAdd;
			}
			set
			{
				this.internalAdd = value;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0001AF7A File Offset: 0x0001917A
		// (set) Token: 0x060007AE RID: 1966 RVA: 0x0001AF95 File Offset: 0x00019195
		[XmlIgnore]
		public StatusType Delete
		{
			get
			{
				if (this.internalDelete == null)
				{
					this.internalDelete = new StatusType();
				}
				return this.internalDelete;
			}
			set
			{
				this.internalDelete = value;
			}
		}

		// Token: 0x04000434 RID: 1076
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlAttribute(AttributeName = "name", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = "HMSETTINGS:")]
		public string internalname;

		// Token: 0x04000435 RID: 1077
		[XmlElement(Type = typeof(StatusType), ElementName = "Set", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public StatusType internalSet;

		// Token: 0x04000436 RID: 1078
		[XmlElement(Type = typeof(StatusType), ElementName = "Add", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public StatusType internalAdd;

		// Token: 0x04000437 RID: 1079
		[XmlElement(Type = typeof(StatusType), ElementName = "Delete", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public StatusType internalDelete;
	}
}
