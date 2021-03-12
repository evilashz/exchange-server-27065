using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000147 RID: 327
	[XmlType(TypeName = "MoveToFolder", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class MoveToFolder
	{
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0001C69D File Offset: 0x0001A89D
		// (set) Token: 0x0600095E RID: 2398 RVA: 0x0001C6A5 File Offset: 0x0001A8A5
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

		// Token: 0x0400052F RID: 1327
		[XmlElement(ElementName = "FolderId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalFolderId;
	}
}
