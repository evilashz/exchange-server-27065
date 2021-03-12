using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003C0 RID: 960
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class RecurrenceRange
	{
		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06002BB5 RID: 11189 RVA: 0x000AE28D File Offset: 0x000AC48D
		// (set) Token: 0x06002BB6 RID: 11190 RVA: 0x000AE295 File Offset: 0x000AC495
		public virtual ExDateTime StartDate
		{
			get
			{
				return this.startDate;
			}
			protected set
			{
				ExTimeZoneHelperForMigrationOnly.CheckValidationLevel(value.HasTimeZone, ExTimeZoneHelperForMigrationOnly.ValidationLevel.Mid, "RecurrenceRange.StartDate_set: value has no time zone", new object[0]);
				this.startDate = value.Date;
			}
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x000AE2BC File Offset: 0x000AC4BC
		public virtual bool Equals(RecurrenceRange value)
		{
			return value != null && value.StartDate == this.startDate;
		}

		// Token: 0x04001864 RID: 6244
		private ExDateTime startDate = ExDateTime.MinValue;
	}
}
