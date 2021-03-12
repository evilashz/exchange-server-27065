using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Response.AirSync;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Sync
{
	// Token: 0x0200006F RID: 111
	[XmlRoot(Namespace = "AirSync", ElementName = "Sync")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "AirSync", TypeName = "SyncResponse")]
	public class SyncResponse : Sync, IEasServerResponse<SyncStatus>, IHaveAnHttpStatus
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x000057B1 File Offset: 0x000039B1
		public SyncResponse()
		{
			base.Collections = new List<Collection>();
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000057C4 File Offset: 0x000039C4
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x000057CC File Offset: 0x000039CC
		HttpStatus IHaveAnHttpStatus.HttpStatus { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000057E2 File Offset: 0x000039E2
		[XmlIgnore]
		public List<FetchResponse> Fetches
		{
			get
			{
				return base.Collections.SelectMany((Collection collection) => collection.Responses.Fetch).ToList<FetchResponse>();
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000581E File Offset: 0x00003A1E
		[XmlIgnore]
		public List<AddResponse> AddResponses
		{
			get
			{
				return base.Collections.SelectMany((Collection collection) => collection.Responses.Add).ToList<AddResponse>();
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000585A File Offset: 0x00003A5A
		[XmlIgnore]
		public List<ChangeResponse> ChangeResponses
		{
			get
			{
				return base.Collections.SelectMany((Collection collection) => collection.Responses.Change).ToList<ChangeResponse>();
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00005896 File Offset: 0x00003A96
		[XmlIgnore]
		public List<AddCommand> Additions
		{
			get
			{
				return base.Collections.SelectMany((Collection collection) => collection.Commands.Add).ToList<AddCommand>();
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001FD RID: 509 RVA: 0x000058D2 File Offset: 0x00003AD2
		[XmlIgnore]
		public List<ChangeCommand> Changes
		{
			get
			{
				return base.Collections.SelectMany((Collection c) => c.Commands.Change).ToList<ChangeCommand>();
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000590E File Offset: 0x00003B0E
		[XmlIgnore]
		public List<DeleteCommand> Deletions
		{
			get
			{
				return base.Collections.SelectMany((Collection c) => c.Commands.Delete).ToList<DeleteCommand>();
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000594A File Offset: 0x00003B4A
		public List<SoftDeleteCommand> GetAllSoftDeletions
		{
			get
			{
				return base.Collections.SelectMany((Collection c) => c.Commands.SoftDelete).ToList<SoftDeleteCommand>();
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000597C File Offset: 0x00003B7C
		public SyncStatus GetChangeResponseStatus(int index)
		{
			if (this.ChangeResponses == null || this.ChangeResponses.Count <= index || this.ChangeResponses[index] == null)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			ChangeResponse changeResponse = this.ChangeResponses[index];
			byte status = changeResponse.Status;
			if (!SyncResponse.StatusToEnumMap.ContainsKey(status))
			{
				return (SyncStatus)status;
			}
			return SyncResponse.StatusToEnumMap[status];
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000059E8 File Offset: 0x00003BE8
		public SyncStatus GetAddResponseStatus(int index)
		{
			if (this.AddResponses == null || this.AddResponses.Count <= index || this.AddResponses[index] == null)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			AddResponse addResponse = this.AddResponses[index];
			byte status = addResponse.Status;
			if (!SyncResponse.StatusToEnumMap.ContainsKey(status))
			{
				return (SyncStatus)status;
			}
			return SyncResponse.StatusToEnumMap[status];
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00005A52 File Offset: 0x00003C52
		bool IEasServerResponse<SyncStatus>.IsSucceeded(SyncStatus status)
		{
			return SyncStatus.Success == status;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00005A70 File Offset: 0x00003C70
		SyncStatus IEasServerResponse<SyncStatus>.ConvertStatusToEnum()
		{
			if (base.Collections.Count > 1)
			{
				IEnumerable<byte> source = from c in base.Collections
				where c.Status != 1
				select c.Status;
				if (source.Count<byte>() == 0)
				{
					return SyncStatus.Success;
				}
				return SyncStatus.CompositeStatusError;
			}
			else
			{
				if (base.Collections.Count == 0 && base.Status == 0)
				{
					return SyncStatus.Success;
				}
				byte b = (base.Collections.Count == 0) ? base.Status : base.Collections[0].Status;
				if (!SyncResponse.StatusToEnumMap.ContainsKey(b))
				{
					return (SyncStatus)b;
				}
				return SyncResponse.StatusToEnumMap[b];
			}
		}

		// Token: 0x040001B2 RID: 434
		private static readonly IReadOnlyDictionary<byte, SyncStatus> StatusToEnumMap = new Dictionary<byte, SyncStatus>
		{
			{
				1,
				SyncStatus.Success
			},
			{
				3,
				SyncStatus.InvalidSyncKey
			},
			{
				4,
				SyncStatus.ProtocolError
			},
			{
				5,
				SyncStatus.ServerError
			},
			{
				6,
				SyncStatus.ErrorInClientServerConversion
			},
			{
				7,
				SyncStatus.Conflict
			},
			{
				8,
				SyncStatus.SyncItemNotFound
			},
			{
				9,
				SyncStatus.OutOfDisk
			},
			{
				12,
				SyncStatus.FolderHierarchyChanged
			},
			{
				13,
				SyncStatus.IncompleteSyncCommand
			},
			{
				14,
				SyncStatus.InvalidWaitTime
			},
			{
				15,
				SyncStatus.SyncTooManyFolders
			},
			{
				16,
				SyncStatus.Retry
			},
			{
				110,
				SyncStatus.ServerBusy
			}
		};
	}
}
