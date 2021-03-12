using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x0200011B RID: 283
	[XmlType(TypeName = "FiltersRequestTypeFilterActionsMoveToFolder", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class FiltersRequestTypeFilterActionsMoveToFolder
	{
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x0001B8E5 File Offset: 0x00019AE5
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x0001B8ED File Offset: 0x00019AED
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

		// Token: 0x04000489 RID: 1161
		[XmlElement(ElementName = "FolderId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalFolderId;
	}
}
