using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters
{
	// Token: 0x02000079 RID: 121
	internal struct GlobalObjectIdConverter : IConverter<GlobalObjectId, string>, IConverter<string, GlobalObjectId>
	{
		// Token: 0x06000314 RID: 788 RVA: 0x0000B4F0 File Offset: 0x000096F0
		public string Convert(GlobalObjectId value)
		{
			if (value == null)
			{
				throw new ExArgumentNullException("value");
			}
			return value.ToString();
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000B513 File Offset: 0x00009713
		public GlobalObjectId Convert(string value)
		{
			if (value == null)
			{
				throw new ExArgumentNullException(value);
			}
			if (value.Length == 0)
			{
				throw new CorruptDataException(CalendaringStrings.ErrorInvalidIdentifier);
			}
			return new GlobalObjectId(value);
		}
	}
}
