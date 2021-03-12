using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Response.FolderHierarchy;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.FolderCreate
{
	// Token: 0x02000035 RID: 53
	[XmlRoot(Namespace = "FolderHierarchy", ElementName = "FolderCreate")]
	[XmlType(Namespace = "FolderHierarchy", TypeName = "FolderCreateResponse")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class FolderCreateResponse : FolderCreate, IEasServerResponse<FolderCreateStatus>, IHaveAnHttpStatus
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000046FD File Offset: 0x000028FD
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00004705 File Offset: 0x00002905
		HttpStatus IHaveAnHttpStatus.HttpStatus { get; set; }

		// Token: 0x0600012E RID: 302 RVA: 0x0000470E File Offset: 0x0000290E
		bool IEasServerResponse<FolderCreateStatus>.IsSucceeded(FolderCreateStatus status)
		{
			return FolderCreateStatus.Success == status;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004714 File Offset: 0x00002914
		FolderCreateStatus IEasServerResponse<FolderCreateStatus>.ConvertStatusToEnum()
		{
			byte status = base.Status;
			if (!FolderCreateResponse.StatusToEnumMap.ContainsKey(status))
			{
				return (FolderCreateStatus)status;
			}
			return FolderCreateResponse.StatusToEnumMap[status];
		}

		// Token: 0x04000115 RID: 277
		private static readonly IReadOnlyDictionary<byte, FolderCreateStatus> StatusToEnumMap = new Dictionary<byte, FolderCreateStatus>
		{
			{
				1,
				FolderCreateStatus.Success
			},
			{
				2,
				FolderCreateStatus.FolderExists
			},
			{
				5,
				FolderCreateStatus.ParentFolderNotFound
			},
			{
				6,
				FolderCreateStatus.ServerError
			},
			{
				9,
				FolderCreateStatus.SyncKeyMismatchOrInvalid
			},
			{
				10,
				FolderCreateStatus.IncorrectRequestFormat
			},
			{
				11,
				FolderCreateStatus.UnknownError
			},
			{
				12,
				FolderCreateStatus.CodeUnknown
			}
		};
	}
}
