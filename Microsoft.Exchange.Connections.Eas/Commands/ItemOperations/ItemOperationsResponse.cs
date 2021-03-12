using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Response.ItemOperations;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.ItemOperations
{
	// Token: 0x02000053 RID: 83
	[XmlRoot(Namespace = "ItemOperations", ElementName = "ItemOperations")]
	[XmlType(Namespace = "ItemOperations", TypeName = "ItemOperationsResponse")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class ItemOperationsResponse : ItemOperations, IEasServerResponse<ItemOperationsStatus>, IHaveAnHttpStatus
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00004DC6 File Offset: 0x00002FC6
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00004DCE File Offset: 0x00002FCE
		HttpStatus IHaveAnHttpStatus.HttpStatus { get; set; }

		// Token: 0x0600018E RID: 398 RVA: 0x00004DD7 File Offset: 0x00002FD7
		bool IEasServerResponse<ItemOperationsStatus>.IsSucceeded(ItemOperationsStatus status)
		{
			return ItemOperationsStatus.Success == status;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00004DE0 File Offset: 0x00002FE0
		ItemOperationsStatus IEasServerResponse<ItemOperationsStatus>.ConvertStatusToEnum()
		{
			byte status = base.Status;
			if (!ItemOperationsResponse.StatusToEnumMap.ContainsKey(status))
			{
				return (ItemOperationsStatus)status;
			}
			return ItemOperationsResponse.StatusToEnumMap[status];
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00004E0E File Offset: 0x0000300E
		internal Properties GetCalendarItemProperties()
		{
			if (base.Response == null || base.Response.Fetches == null || base.Response.Fetches.Length == 0)
			{
				return null;
			}
			return base.Response.Fetches[0].Properties;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00004E48 File Offset: 0x00003048
		internal Properties GetMessageProperties(int index, out ItemOperationsStatus status)
		{
			if (base.Response == null || base.Response.Fetches == null || base.Response.Fetches.Length <= index || base.Response.Fetches[index] == null)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			Fetch fetch = base.Response.Fetches[index];
			byte status2 = fetch.Status;
			status = (ItemOperationsResponse.StatusToEnumMap.ContainsKey(status2) ? ItemOperationsResponse.StatusToEnumMap[status2] : ((ItemOperationsStatus)status2));
			return fetch.Properties;
		}

		// Token: 0x0400015E RID: 350
		private static readonly IReadOnlyDictionary<byte, ItemOperationsStatus> StatusToEnumMap = new Dictionary<byte, ItemOperationsStatus>
		{
			{
				1,
				ItemOperationsStatus.Success
			},
			{
				2,
				ItemOperationsStatus.ProtocolError
			},
			{
				3,
				ItemOperationsStatus.ServerError
			},
			{
				4,
				ItemOperationsStatus.DocLibAcccessError
			},
			{
				5,
				ItemOperationsStatus.DocLibAccessDenied
			},
			{
				6,
				ItemOperationsStatus.DocLibObjectNotFoundOrAccessDenied
			},
			{
				7,
				ItemOperationsStatus.DocLibFailedToConnectToServer
			},
			{
				8,
				ItemOperationsStatus.BadByteRange
			},
			{
				9,
				ItemOperationsStatus.BadStore
			},
			{
				10,
				ItemOperationsStatus.FileIsEmpty
			},
			{
				11,
				ItemOperationsStatus.RequestedDataSizeTooLarge
			},
			{
				12,
				ItemOperationsStatus.DownloadFailure
			},
			{
				14,
				ItemOperationsStatus.ItemConversionFailed
			},
			{
				15,
				ItemOperationsStatus.AfpInvalidAttachmentOrAttachmentId
			},
			{
				16,
				ItemOperationsStatus.ResourceAccessDenied
			},
			{
				17,
				ItemOperationsStatus.PartialSuccess
			},
			{
				18,
				ItemOperationsStatus.CredentialsRequired
			},
			{
				110,
				ItemOperationsStatus.ServerBusy
			},
			{
				155,
				ItemOperationsStatus.MissingElements
			},
			{
				156,
				ItemOperationsStatus.ActionNotSupported
			}
		};
	}
}
