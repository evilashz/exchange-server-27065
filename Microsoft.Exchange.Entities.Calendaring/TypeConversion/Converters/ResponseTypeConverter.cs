using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters
{
	// Token: 0x0200008D RID: 141
	internal struct ResponseTypeConverter : IResponseTypeConverter, IConverter<ResponseType, ResponseType>, IConverter<ResponseType, ResponseType>
	{
		// Token: 0x0600035B RID: 859 RVA: 0x0000C35F File Offset: 0x0000A55F
		public ResponseType Convert(ResponseType value)
		{
			return ResponseTypeConverter.mappingConverter.Convert(value);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000C36C File Offset: 0x0000A56C
		public ResponseType Convert(ResponseType value)
		{
			return ResponseTypeConverter.mappingConverter.Reverse(value);
		}

		// Token: 0x040000FA RID: 250
		private static SimpleMappingConverter<ResponseType, ResponseType> mappingConverter = SimpleMappingConverter<ResponseType, ResponseType>.CreateStrictConverter(new Tuple<ResponseType, ResponseType>[]
		{
			new Tuple<ResponseType, ResponseType>(ResponseType.None, ResponseType.None),
			new Tuple<ResponseType, ResponseType>(ResponseType.Organizer, ResponseType.Organizer),
			new Tuple<ResponseType, ResponseType>(ResponseType.Tentative, ResponseType.TentativelyAccepted),
			new Tuple<ResponseType, ResponseType>(ResponseType.Accept, ResponseType.Accepted),
			new Tuple<ResponseType, ResponseType>(ResponseType.Decline, ResponseType.Declined),
			new Tuple<ResponseType, ResponseType>(ResponseType.NotResponded, ResponseType.NotResponded)
		});
	}
}
