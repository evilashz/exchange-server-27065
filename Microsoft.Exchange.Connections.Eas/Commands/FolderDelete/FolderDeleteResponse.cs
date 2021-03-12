using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Response.FolderHierarchy;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.FolderDelete
{
	// Token: 0x0200003B RID: 59
	[XmlRoot(Namespace = "FolderHierarchy", ElementName = "FolderDelete")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "FolderHierarchy", TypeName = "FolderDeleteResponse")]
	public class FolderDeleteResponse : FolderDelete, IEasServerResponse<FolderDeleteStatus>, IHaveAnHttpStatus
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000482B File Offset: 0x00002A2B
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00004833 File Offset: 0x00002A33
		HttpStatus IHaveAnHttpStatus.HttpStatus { get; set; }

		// Token: 0x06000140 RID: 320 RVA: 0x0000483C File Offset: 0x00002A3C
		bool IEasServerResponse<FolderDeleteStatus>.IsSucceeded(FolderDeleteStatus status)
		{
			return FolderDeleteStatus.Success == status;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004844 File Offset: 0x00002A44
		FolderDeleteStatus IEasServerResponse<FolderDeleteStatus>.ConvertStatusToEnum()
		{
			byte status = base.Status;
			if (!FolderDeleteResponse.StatusToEnumMap.ContainsKey(status))
			{
				return (FolderDeleteStatus)status;
			}
			return FolderDeleteResponse.StatusToEnumMap[status];
		}

		// Token: 0x04000124 RID: 292
		private static readonly IReadOnlyDictionary<byte, FolderDeleteStatus> StatusToEnumMap = new Dictionary<byte, FolderDeleteStatus>
		{
			{
				1,
				FolderDeleteStatus.Success
			},
			{
				3,
				FolderDeleteStatus.SystemFolder
			},
			{
				4,
				FolderDeleteStatus.FolderNotFound
			},
			{
				6,
				FolderDeleteStatus.ServerError
			},
			{
				9,
				FolderDeleteStatus.SyncKeyMismatchOrInvalid
			},
			{
				10,
				FolderDeleteStatus.IncorrectRequestFormat
			},
			{
				11,
				FolderDeleteStatus.UnknownError
			},
			{
				12,
				FolderDeleteStatus.CodeUnknown
			}
		};
	}
}
