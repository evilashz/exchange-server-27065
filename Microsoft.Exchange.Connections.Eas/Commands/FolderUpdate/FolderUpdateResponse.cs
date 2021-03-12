using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Response.FolderHierarchy;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.FolderUpdate
{
	// Token: 0x02000047 RID: 71
	[XmlType(Namespace = "FolderHierarchy", TypeName = "FolderUpdateResponse")]
	[XmlRoot(Namespace = "FolderHierarchy", ElementName = "FolderUpdate")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class FolderUpdateResponse : FolderUpdate, IEasServerResponse<FolderUpdateStatus>, IHaveAnHttpStatus
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00004B1A File Offset: 0x00002D1A
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00004B22 File Offset: 0x00002D22
		HttpStatus IHaveAnHttpStatus.HttpStatus { get; set; }

		// Token: 0x0600016C RID: 364 RVA: 0x00004B2B File Offset: 0x00002D2B
		bool IEasServerResponse<FolderUpdateStatus>.IsSucceeded(FolderUpdateStatus status)
		{
			return FolderUpdateStatus.Success == status;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00004B34 File Offset: 0x00002D34
		FolderUpdateStatus IEasServerResponse<FolderUpdateStatus>.ConvertStatusToEnum()
		{
			byte status = base.Status;
			if (!FolderUpdateResponse.StatusToEnumMap.ContainsKey(status))
			{
				return (FolderUpdateStatus)status;
			}
			return FolderUpdateResponse.StatusToEnumMap[status];
		}

		// Token: 0x04000145 RID: 325
		private static readonly IReadOnlyDictionary<byte, FolderUpdateStatus> StatusToEnumMap = new Dictionary<byte, FolderUpdateStatus>
		{
			{
				1,
				FolderUpdateStatus.Success
			},
			{
				2,
				FolderUpdateStatus.FolderExists
			},
			{
				4,
				FolderUpdateStatus.FolderNotFound
			},
			{
				5,
				FolderUpdateStatus.ParentFolderNotFound
			},
			{
				6,
				FolderUpdateStatus.ServerError
			},
			{
				9,
				FolderUpdateStatus.SyncKeyMismatchOrInvalid
			},
			{
				10,
				FolderUpdateStatus.IncorrectRequestFormat
			},
			{
				11,
				FolderUpdateStatus.UnknownError
			},
			{
				12,
				FolderUpdateStatus.CodeUnknown
			}
		};
	}
}
