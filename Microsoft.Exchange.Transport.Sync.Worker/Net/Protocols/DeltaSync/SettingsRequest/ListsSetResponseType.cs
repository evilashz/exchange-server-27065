using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x02000101 RID: 257
	[XmlType(TypeName = "ListsSetResponseType", Namespace = "HMSETTINGS:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ListsSetResponseType
	{
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x0001AEDD File Offset: 0x000190DD
		// (set) Token: 0x060007A3 RID: 1955 RVA: 0x0001AEE5 File Offset: 0x000190E5
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

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x0001AEF5 File Offset: 0x000190F5
		// (set) Token: 0x060007A5 RID: 1957 RVA: 0x0001AF10 File Offset: 0x00019110
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

		// Token: 0x04000431 RID: 1073
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalStatus;

		// Token: 0x04000432 RID: 1074
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalStatusSpecified;

		// Token: 0x04000433 RID: 1075
		[XmlElement(Type = typeof(List), ElementName = "List", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ListCollection internalListCollection;
	}
}
