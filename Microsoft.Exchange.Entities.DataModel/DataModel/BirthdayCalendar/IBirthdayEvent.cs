using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel.BirthdayCalendar
{
	// Token: 0x02000021 RID: 33
	public interface IBirthdayEvent : IStorageEntity, IEntity, IPropertyChangeTracker<PropertyDefinition>, IVersioned
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008A RID: 138
		// (set) Token: 0x0600008B RID: 139
		string Attribution { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008C RID: 140
		// (set) Token: 0x0600008D RID: 141
		ExDateTime Birthday { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008E RID: 142
		// (set) Token: 0x0600008F RID: 143
		string Subject { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000090 RID: 144
		bool IsBirthYearKnown { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000091 RID: 145
		// (set) Token: 0x06000092 RID: 146
		bool IsWritable { get; set; }
	}
}
