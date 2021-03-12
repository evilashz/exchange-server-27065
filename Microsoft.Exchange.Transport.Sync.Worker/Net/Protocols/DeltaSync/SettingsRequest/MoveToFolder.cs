using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x0200010B RID: 267
	[XmlType(TypeName = "MoveToFolder", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class MoveToFolder
	{
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x0001B309 File Offset: 0x00019509
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x0001B311 File Offset: 0x00019511
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

		// Token: 0x04000450 RID: 1104
		[XmlElement(ElementName = "FolderId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalFolderId;
	}
}
