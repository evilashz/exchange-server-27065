using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Response.FolderHierarchy;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.FolderSync
{
	// Token: 0x02000041 RID: 65
	[XmlRoot(Namespace = "FolderHierarchy", ElementName = "FolderSync")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "FolderHierarchy", TypeName = "FolderSyncResponse")]
	public class FolderSyncResponse : FolderSync, IEasServerResponse<FolderSyncStatus>, IHaveAnHttpStatus
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000049D2 File Offset: 0x00002BD2
		// (set) Token: 0x06000155 RID: 341 RVA: 0x000049DA File Offset: 0x00002BDA
		HttpStatus IHaveAnHttpStatus.HttpStatus { get; set; }

		// Token: 0x06000156 RID: 342 RVA: 0x000049E3 File Offset: 0x00002BE3
		bool IEasServerResponse<FolderSyncStatus>.IsSucceeded(FolderSyncStatus status)
		{
			return FolderSyncStatus.Success == status;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000049EC File Offset: 0x00002BEC
		FolderSyncStatus IEasServerResponse<FolderSyncStatus>.ConvertStatusToEnum()
		{
			byte status = base.Status;
			if (!FolderSyncResponse.StatusToEnumMap.ContainsKey(status))
			{
				return (FolderSyncStatus)status;
			}
			return FolderSyncResponse.StatusToEnumMap[status];
		}

		// Token: 0x04000135 RID: 309
		private static readonly IReadOnlyDictionary<byte, FolderSyncStatus> StatusToEnumMap = new Dictionary<byte, FolderSyncStatus>
		{
			{
				1,
				FolderSyncStatus.Success
			},
			{
				6,
				FolderSyncStatus.ServerError
			},
			{
				9,
				FolderSyncStatus.SyncKeyMismatchOrInvalid
			},
			{
				10,
				FolderSyncStatus.IncorrectRequestFormat
			},
			{
				11,
				FolderSyncStatus.UnknownError
			},
			{
				12,
				FolderSyncStatus.CodeUnknown
			},
			{
				110,
				FolderSyncStatus.ServerBusy
			}
		};
	}
}
