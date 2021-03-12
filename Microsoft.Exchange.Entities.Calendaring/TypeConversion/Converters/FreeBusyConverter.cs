using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters
{
	// Token: 0x02000078 RID: 120
	internal struct FreeBusyConverter : IConverter<BusyType, FreeBusyStatus>, IConverter<FreeBusyStatus, BusyType>
	{
		// Token: 0x06000312 RID: 786 RVA: 0x0000B4D3 File Offset: 0x000096D3
		public FreeBusyStatus Convert(BusyType value)
		{
			return FreeBusyConverter.mappingConverter.Convert(value);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000B4E0 File Offset: 0x000096E0
		public BusyType Convert(FreeBusyStatus value)
		{
			return FreeBusyConverter.mappingConverter.Reverse(value);
		}

		// Token: 0x040000DC RID: 220
		private static SimpleMappingConverter<BusyType, FreeBusyStatus> mappingConverter = SimpleMappingConverter<BusyType, FreeBusyStatus>.CreateStrictConverter(new Tuple<BusyType, FreeBusyStatus>[]
		{
			new Tuple<BusyType, FreeBusyStatus>(BusyType.Unknown, FreeBusyStatus.Unknown),
			new Tuple<BusyType, FreeBusyStatus>(BusyType.Free, FreeBusyStatus.Free),
			new Tuple<BusyType, FreeBusyStatus>(BusyType.Tentative, FreeBusyStatus.Tentative),
			new Tuple<BusyType, FreeBusyStatus>(BusyType.Busy, FreeBusyStatus.Busy),
			new Tuple<BusyType, FreeBusyStatus>(BusyType.OOF, FreeBusyStatus.Oof),
			new Tuple<BusyType, FreeBusyStatus>(BusyType.WorkingElseWhere, FreeBusyStatus.WorkingElsewhere)
		});
	}
}
