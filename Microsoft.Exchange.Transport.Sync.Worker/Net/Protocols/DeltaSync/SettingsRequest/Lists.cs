using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000FF RID: 255
	[XmlType(TypeName = "Lists", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class Lists
	{
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0001AEA9 File Offset: 0x000190A9
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x0001AEC4 File Offset: 0x000190C4
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

		// Token: 0x04000430 RID: 1072
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(ListsGet), ElementName = "Get", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public ListsGet internalGet;
	}
}
