using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000146 RID: 326
	[XmlType(TypeName = "Actions", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class Actions
	{
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0001C671 File Offset: 0x0001A871
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x0001C68C File Offset: 0x0001A88C
		[XmlIgnore]
		public MoveToFolder MoveToFolder
		{
			get
			{
				if (this.internalMoveToFolder == null)
				{
					this.internalMoveToFolder = new MoveToFolder();
				}
				return this.internalMoveToFolder;
			}
			set
			{
				this.internalMoveToFolder = value;
			}
		}

		// Token: 0x0400052E RID: 1326
		[XmlElement(Type = typeof(MoveToFolder), ElementName = "MoveToFolder", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public MoveToFolder internalMoveToFolder;
	}
}
