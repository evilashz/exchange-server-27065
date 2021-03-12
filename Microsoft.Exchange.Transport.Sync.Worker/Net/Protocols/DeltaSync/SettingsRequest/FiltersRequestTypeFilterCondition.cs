using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x02000118 RID: 280
	[XmlType(TypeName = "FiltersRequestTypeFilterCondition", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class FiltersRequestTypeFilterCondition
	{
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x0001B831 File Offset: 0x00019A31
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x0001B84C File Offset: 0x00019A4C
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

		// Token: 0x04000482 RID: 1154
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(FiltersRequestTypeFilterConditionClause), ElementName = "Clause", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public FiltersRequestTypeFilterConditionClause internalClause;
	}
}
