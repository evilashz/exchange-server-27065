using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000151 RID: 337
	[XmlType(TypeName = "FiltersRequestTypeFilterActionsMoveToFolder", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class FiltersRequestTypeFilterActionsMoveToFolder
	{
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x0001CB09 File Offset: 0x0001AD09
		// (set) Token: 0x060009B4 RID: 2484 RVA: 0x0001CB11 File Offset: 0x0001AD11
		[XmlIgnore]
		public string FolderId
		{
			get
			{
				return this.internalFolderId;
			}
			set
			{
				this.internalFolderId = value;
			}
		}

		// Token: 0x04000562 RID: 1378
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "FolderId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		public string internalFolderId;
	}
}
