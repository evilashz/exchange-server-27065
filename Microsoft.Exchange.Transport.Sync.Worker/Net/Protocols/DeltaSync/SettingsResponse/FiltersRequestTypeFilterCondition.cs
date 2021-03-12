using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x0200014E RID: 334
	[XmlType(TypeName = "FiltersRequestTypeFilterCondition", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class FiltersRequestTypeFilterCondition
	{
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x0001CA55 File Offset: 0x0001AC55
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x0001CA70 File Offset: 0x0001AC70
		[XmlIgnore]
		public FiltersRequestTypeFilterConditionClause Clause
		{
			get
			{
				if (this.internalClause == null)
				{
					this.internalClause = new FiltersRequestTypeFilterConditionClause();
				}
				return this.internalClause;
			}
			set
			{
				this.internalClause = value;
			}
		}

		// Token: 0x0400055B RID: 1371
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(FiltersRequestTypeFilterConditionClause), ElementName = "Clause", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public FiltersRequestTypeFilterConditionClause internalClause;
	}
}
