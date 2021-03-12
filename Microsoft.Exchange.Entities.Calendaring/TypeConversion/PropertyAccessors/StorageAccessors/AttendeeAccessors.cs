using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyAccessors.StorageAccessors
{
	// Token: 0x0200008E RID: 142
	internal static class AttendeeAccessors
	{
		// Token: 0x040000FB RID: 251
		public static readonly IStoragePropertyAccessor<Attendee, ExDateTime> ReplyTime = new DefaultStoragePropertyAccessor<Attendee, ExDateTime>(RecipientSchema.RecipientTrackStatusTime, false);
	}
}
