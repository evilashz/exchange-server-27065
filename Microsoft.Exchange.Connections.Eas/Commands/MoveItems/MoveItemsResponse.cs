using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Response.Move;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.MoveItems
{
	// Token: 0x02000059 RID: 89
	[XmlType(Namespace = "Move", TypeName = "MoveItemsResponse")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlRoot(Namespace = "Move", ElementName = "MoveItems")]
	public class MoveItemsResponse : MoveItems, IEasServerResponse<MoveItemsStatus>, IHaveAnHttpStatus
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00005030 File Offset: 0x00003230
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00005038 File Offset: 0x00003238
		HttpStatus IHaveAnHttpStatus.HttpStatus { get; set; }

		// Token: 0x0600019E RID: 414 RVA: 0x00005041 File Offset: 0x00003241
		bool IEasServerResponse<MoveItemsStatus>.IsSucceeded(MoveItemsStatus status)
		{
			return MoveItemsStatus.Success == status;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00005060 File Offset: 0x00003260
		MoveItemsStatus IEasServerResponse<MoveItemsStatus>.ConvertStatusToEnum()
		{
			if (base.Responses.Length > 1)
			{
				IEnumerable<byte> source = from c in base.Responses
				where c.Status != 3
				select c.Status;
				if (source.Count<byte>() == 0)
				{
					return MoveItemsStatus.Success;
				}
				return MoveItemsStatus.CompositeStatusError;
			}
			else
			{
				byte status = base.Responses[0].Status;
				if (!MoveItemsResponse.StatusToEnumMap.ContainsKey(status))
				{
					return (MoveItemsStatus)status;
				}
				return MoveItemsResponse.StatusToEnumMap[status];
			}
		}

		// Token: 0x04000177 RID: 375
		private static readonly IReadOnlyDictionary<byte, MoveItemsStatus> StatusToEnumMap = new Dictionary<byte, MoveItemsStatus>
		{
			{
				1,
				MoveItemsStatus.InvalidSourceId
			},
			{
				2,
				MoveItemsStatus.InvalidDestinationId
			},
			{
				3,
				MoveItemsStatus.Success
			},
			{
				4,
				MoveItemsStatus.SourceDestinationIdentical
			},
			{
				5,
				MoveItemsStatus.CannotMove
			},
			{
				7,
				MoveItemsStatus.Retry
			}
		};
	}
}
