using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x0200013D RID: 317
	[XmlType(TypeName = "ListsSetResponseType", Namespace = "HMSETTINGS:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ListsSetResponseType
	{
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x0001C272 File Offset: 0x0001A472
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x0001C27A File Offset: 0x0001A47A
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

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0001C28A File Offset: 0x0001A48A
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x0001C2A5 File Offset: 0x0001A4A5
		[XmlIgnore]
		public ListCollection ListCollection
		{
			get
			{
				if (this.internalListCollection == null)
				{
					this.internalListCollection = new ListCollection();
				}
				return this.internalListCollection;
			}
			set
			{
				this.internalListCollection = value;
			}
		}

		// Token: 0x04000510 RID: 1296
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalStatus;

		// Token: 0x04000511 RID: 1297
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalStatusSpecified;

		// Token: 0x04000512 RID: 1298
		[XmlElement(Type = typeof(List), ElementName = "List", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ListCollection internalListCollection;
	}
}
