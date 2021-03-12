using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000163 RID: 355
	[XmlType(TypeName = "ListsGet", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class ListsGet
	{
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x0001D4A6 File Offset: 0x0001B6A6
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x0001D4C1 File Offset: 0x0001B6C1
		[XmlIgnore]
		public ListsGetResponseType Lists
		{
			get
			{
				if (this.internalLists == null)
				{
					this.internalLists = new ListsGetResponseType();
				}
				return this.internalLists;
			}
			set
			{
				this.internalLists = value;
			}
		}

		// Token: 0x040005C8 RID: 1480
		[XmlElement(Type = typeof(ListsGetResponseType), ElementName = "Lists", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ListsGetResponseType internalLists;
	}
}
