using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Response.GetItemEstimate;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.GetItemEstimate
{
	// Token: 0x0200004D RID: 77
	[XmlRoot(Namespace = "GetItemEstimate", ElementName = "GetItemEstimate")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "GetItemEstimate", TypeName = "GetItemEstimateResponse")]
	public class GetItemEstimateResponse : GetItemEstimate, IEasServerResponse<GetItemEstimateStatus>, IHaveAnHttpStatus
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00004C52 File Offset: 0x00002E52
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00004C5A File Offset: 0x00002E5A
		HttpStatus IHaveAnHttpStatus.HttpStatus { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00004C64 File Offset: 0x00002E64
		internal int? Estimate
		{
			get
			{
				if (base.Response == null || base.Response.Collection == null)
				{
					return null;
				}
				return new int?(base.Response.Collection.Estimate);
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00004CA5 File Offset: 0x00002EA5
		bool IEasServerResponse<GetItemEstimateStatus>.IsSucceeded(GetItemEstimateStatus status)
		{
			return GetItemEstimateStatus.Success == status;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00004CAC File Offset: 0x00002EAC
		GetItemEstimateStatus IEasServerResponse<GetItemEstimateStatus>.ConvertStatusToEnum()
		{
			byte status = base.Response.Status;
			if (!GetItemEstimateResponse.StatusToEnumMap.ContainsKey(status))
			{
				return (GetItemEstimateStatus)status;
			}
			return GetItemEstimateResponse.StatusToEnumMap[status];
		}

		// Token: 0x04000154 RID: 340
		private static readonly IReadOnlyDictionary<byte, GetItemEstimateStatus> StatusToEnumMap = new Dictionary<byte, GetItemEstimateStatus>
		{
			{
				1,
				GetItemEstimateStatus.Success
			},
			{
				2,
				GetItemEstimateStatus.InvalidCollection
			},
			{
				3,
				GetItemEstimateStatus.SyncNotPrimed
			},
			{
				4,
				GetItemEstimateStatus.InvalidSyncKey
			}
		};
	}
}
