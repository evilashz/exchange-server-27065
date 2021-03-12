using System;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.BirthdayCalendar
{
	// Token: 0x0200001C RID: 28
	public interface IBirthdayContact : IStorageEntity, IEntity, IPropertyChangeTracker<PropertyDefinition>, IVersioned
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000057 RID: 87
		// (set) Token: 0x06000058 RID: 88
		ExDateTime? Birthday { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000059 RID: 89
		// (set) Token: 0x0600005A RID: 90
		string DisplayName { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005B RID: 91
		// (set) Token: 0x0600005C RID: 92
		string Attribution { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005D RID: 93
		// (set) Token: 0x0600005E RID: 94
		bool ShouldHideBirthday { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005F RID: 95
		// (set) Token: 0x06000060 RID: 96
		bool IsWritable { get; set; }
	}
}
