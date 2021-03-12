using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x0200010A RID: 266
	[XmlType(TypeName = "Actions", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class Actions
	{
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0001B2DD File Offset: 0x000194DD
		// (set) Token: 0x060007E8 RID: 2024 RVA: 0x0001B2F8 File Offset: 0x000194F8
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

		// Token: 0x0400044F RID: 1103
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(MoveToFolder), ElementName = "MoveToFolder", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public MoveToFolder internalMoveToFolder;
	}
}
