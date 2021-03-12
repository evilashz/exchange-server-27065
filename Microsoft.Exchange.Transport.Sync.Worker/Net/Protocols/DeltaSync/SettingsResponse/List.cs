using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x0200013E RID: 318
	[XmlType(TypeName = "List", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class List
	{
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0001C2B6 File Offset: 0x0001A4B6
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x0001C2BE File Offset: 0x0001A4BE
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

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0001C2C7 File Offset: 0x0001A4C7
		// (set) Token: 0x0600091D RID: 2333 RVA: 0x0001C2E2 File Offset: 0x0001A4E2
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

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0001C2EB File Offset: 0x0001A4EB
		// (set) Token: 0x0600091F RID: 2335 RVA: 0x0001C306 File Offset: 0x0001A506
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

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x0001C30F File Offset: 0x0001A50F
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x0001C32A File Offset: 0x0001A52A
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

		// Token: 0x04000513 RID: 1299
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlAttribute(AttributeName = "name", Form = XmlSchemaForm.Unqualified, DataType = "string", Namespace = "HMSETTINGS:")]
		public string internalname;

		// Token: 0x04000514 RID: 1300
		[XmlElement(Type = typeof(StatusType), ElementName = "Set", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public StatusType internalSet;

		// Token: 0x04000515 RID: 1301
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(StatusType), ElementName = "Add", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public StatusType internalAdd;

		// Token: 0x04000516 RID: 1302
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(StatusType), ElementName = "Delete", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public StatusType internalDelete;
	}
}
