using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000150 RID: 336
	[XmlType(TypeName = "FiltersRequestTypeFilterActions", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class FiltersRequestTypeFilterActions
	{
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0001CADD File Offset: 0x0001ACDD
		// (set) Token: 0x060009B1 RID: 2481 RVA: 0x0001CAF8 File Offset: 0x0001ACF8
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

		// Token: 0x04000561 RID: 1377
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(FiltersRequestTypeFilterActionsMoveToFolder), ElementName = "MoveToFolder", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public FiltersRequestTypeFilterActionsMoveToFolder internalMoveToFolder;
	}
}
