using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x0200011A RID: 282
	[XmlType(TypeName = "FiltersRequestTypeFilterActions", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class FiltersRequestTypeFilterActions
	{
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x0001B8B9 File Offset: 0x00019AB9
		// (set) Token: 0x06000854 RID: 2132 RVA: 0x0001B8D4 File Offset: 0x00019AD4
		[XmlIgnore]
		public FiltersRequestTypeFilterActionsMoveToFolder MoveToFolder
		{
			get
			{
				if (this.internalMoveToFolder == null)
				{
					this.internalMoveToFolder = new FiltersRequestTypeFilterActionsMoveToFolder();
				}
				return this.internalMoveToFolder;
			}
			set
			{
				this.internalMoveToFolder = value;
			}
		}

		// Token: 0x04000488 RID: 1160
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(FiltersRequestTypeFilterActionsMoveToFolder), ElementName = "MoveToFolder", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public FiltersRequestTypeFilterActionsMoveToFolder internalMoveToFolder;
	}
}
