using System;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E7A RID: 3706
	internal static class ResponseStatusDataEntityConverter
	{
		// Token: 0x06006066 RID: 24678 RVA: 0x0012CF7C File Offset: 0x0012B17C
		internal static ResponseStatus ToResponseStatus(this ResponseStatus dataEntityResponseStatus)
		{
			if (dataEntityResponseStatus == null)
			{
				return null;
			}
			return new ResponseStatus
			{
				Response = EnumConverter.CastEnumType<ResponseType>(dataEntityResponseStatus.Response),
				Time = dataEntityResponseStatus.Time.ToDateTimeOffset()
			};
		}

		// Token: 0x06006067 RID: 24679 RVA: 0x0012CFC0 File Offset: 0x0012B1C0
		internal static ResponseStatus ToDataEntityResponseStatus(this ResponseStatus responseStatus)
		{
			if (responseStatus == null)
			{
				return null;
			}
			return new ResponseStatus
			{
				Response = EnumConverter.CastEnumType<ResponseType>(responseStatus.Response),
				Time = responseStatus.Time.ToExDateTime()
			};
		}
	}
}
