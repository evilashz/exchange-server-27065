using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters
{
	// Token: 0x02000077 RID: 119
	internal struct AttendeeTypeConverter : IAttendeeTypeConverter, IConverter<Microsoft.Exchange.Data.Storage.AttendeeType, Microsoft.Exchange.Entities.DataModel.Calendaring.AttendeeType>, IConverter<Microsoft.Exchange.Entities.DataModel.Calendaring.AttendeeType, Microsoft.Exchange.Data.Storage.AttendeeType>
	{
		// Token: 0x0600030F RID: 783 RVA: 0x0000B45D File Offset: 0x0000965D
		public Microsoft.Exchange.Entities.DataModel.Calendaring.AttendeeType Convert(Microsoft.Exchange.Data.Storage.AttendeeType value)
		{
			return AttendeeTypeConverter.mappingConverter.Convert(value);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000B46A File Offset: 0x0000966A
		public Microsoft.Exchange.Data.Storage.AttendeeType Convert(Microsoft.Exchange.Entities.DataModel.Calendaring.AttendeeType value)
		{
			return AttendeeTypeConverter.mappingConverter.Reverse(value);
		}

		// Token: 0x040000DB RID: 219
		private static SimpleMappingConverter<Microsoft.Exchange.Data.Storage.AttendeeType, Microsoft.Exchange.Entities.DataModel.Calendaring.AttendeeType> mappingConverter = SimpleMappingConverter<Microsoft.Exchange.Data.Storage.AttendeeType, Microsoft.Exchange.Entities.DataModel.Calendaring.AttendeeType>.CreateStrictConverter(new Tuple<Microsoft.Exchange.Data.Storage.AttendeeType, Microsoft.Exchange.Entities.DataModel.Calendaring.AttendeeType>[]
		{
			new Tuple<Microsoft.Exchange.Data.Storage.AttendeeType, Microsoft.Exchange.Entities.DataModel.Calendaring.AttendeeType>(Microsoft.Exchange.Data.Storage.AttendeeType.Required, Microsoft.Exchange.Entities.DataModel.Calendaring.AttendeeType.Required),
			new Tuple<Microsoft.Exchange.Data.Storage.AttendeeType, Microsoft.Exchange.Entities.DataModel.Calendaring.AttendeeType>(Microsoft.Exchange.Data.Storage.AttendeeType.Optional, Microsoft.Exchange.Entities.DataModel.Calendaring.AttendeeType.Optional),
			new Tuple<Microsoft.Exchange.Data.Storage.AttendeeType, Microsoft.Exchange.Entities.DataModel.Calendaring.AttendeeType>(Microsoft.Exchange.Data.Storage.AttendeeType.Resource, Microsoft.Exchange.Entities.DataModel.Calendaring.AttendeeType.Resource)
		});
	}
}
