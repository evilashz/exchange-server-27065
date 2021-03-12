using System;
using System.Collections.Generic;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000328 RID: 808
	internal sealed class GetServerTimeZones : SingleStepServiceCommand<GetServerTimeZonesRequest, GetServerTimeZoneResultType>
	{
		// Token: 0x060016DA RID: 5850 RVA: 0x000793C4 File Offset: 0x000775C4
		public GetServerTimeZones(CallContext callContext, GetServerTimeZonesRequest request) : base(callContext, request)
		{
			this.timeZoneIds = request.Id;
			this.returnFullTimeZoneData = request.ReturnFullTimeZoneData;
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x000793E8 File Offset: 0x000775E8
		internal override IExchangeWebMethodResponse GetResponse()
		{
			GetServerTimeZonesResponse getServerTimeZonesResponse = new GetServerTimeZonesResponse();
			getServerTimeZonesResponse.ProcessServiceResult<GetServerTimeZoneResultType>(base.Result);
			return getServerTimeZonesResponse;
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x00079408 File Offset: 0x00077608
		internal override ServiceResult<GetServerTimeZoneResultType> Execute()
		{
			GetServerTimeZoneResultType serverTimeZonesResult = this.GetServerTimeZonesResult(this.timeZoneIds, this.returnFullTimeZoneData);
			return new ServiceResult<GetServerTimeZoneResultType>(serverTimeZonesResult);
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x00079430 File Offset: 0x00077630
		private GetServerTimeZoneResultType GetServerTimeZonesResult(string[] timeZoneIds, bool returnFullTimeZoneData)
		{
			List<TimeZoneDefinitionType> list = new List<TimeZoneDefinitionType>();
			if (timeZoneIds != null)
			{
				foreach (string text in timeZoneIds)
				{
					ExTimeZone exchTimeZone;
					if (!string.IsNullOrEmpty(text) && ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(text, out exchTimeZone))
					{
						list.Add(new TimeZoneDefinitionType(exchTimeZone));
					}
				}
			}
			else
			{
				foreach (ExTimeZone exchTimeZone2 in ExTimeZoneEnumerator.Instance)
				{
					list.Add(new TimeZoneDefinitionType(exchTimeZone2));
				}
			}
			return new GetServerTimeZoneResultType(this.returnFullTimeZoneData, list.ToArray());
		}

		// Token: 0x04000F6A RID: 3946
		private string[] timeZoneIds;

		// Token: 0x04000F6B RID: 3947
		private bool returnFullTimeZoneData;
	}
}
