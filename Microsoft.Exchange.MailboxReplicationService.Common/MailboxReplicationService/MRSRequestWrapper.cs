using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001D5 RID: 469
	[Serializable]
	internal class MRSRequestWrapper : MRSRequest, IRequestIndexEntry, IConfigurable
	{
		// Token: 0x06001341 RID: 4929 RVA: 0x0002BA85 File Offset: 0x00029C85
		public MRSRequestWrapper()
		{
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0002BA90 File Offset: 0x00029C90
		internal MRSRequestWrapper(IConfigurationSession session, MRSRequestType type, string commonName)
		{
			base.RequestType = type;
			ADObjectId relativeContainerId = ADHandler.GetRelativeContainerId(type);
			base.SetId(session, relativeContainerId, commonName);
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x0002BABA File Offset: 0x00029CBA
		// (set) Token: 0x06001344 RID: 4932 RVA: 0x0002BAC2 File Offset: 0x00029CC2
		public Guid RequestGuid
		{
			get
			{
				return base.MailboxMoveRequestGuid;
			}
			set
			{
				base.MailboxMoveRequestGuid = value;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x0002BACB File Offset: 0x00029CCB
		// (set) Token: 0x06001346 RID: 4934 RVA: 0x0002BAD3 File Offset: 0x00029CD3
		public new string Name
		{
			get
			{
				return base.DisplayName;
			}
			set
			{
				base.DisplayName = value;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x0002BADC File Offset: 0x00029CDC
		// (set) Token: 0x06001348 RID: 4936 RVA: 0x0002BAE4 File Offset: 0x00029CE4
		public RequestStatus Status
		{
			get
			{
				return base.MailboxMoveStatus;
			}
			set
			{
				base.MailboxMoveStatus = value;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x0002BAED File Offset: 0x00029CED
		// (set) Token: 0x0600134A RID: 4938 RVA: 0x0002BAF5 File Offset: 0x00029CF5
		public RequestFlags Flags
		{
			get
			{
				return base.MailboxMoveFlags;
			}
			set
			{
				base.MailboxMoveFlags = value;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x0002BAFE File Offset: 0x00029CFE
		// (set) Token: 0x0600134C RID: 4940 RVA: 0x0002BB06 File Offset: 0x00029D06
		public string RemoteHostName
		{
			get
			{
				return base.MailboxMoveRemoteHostName;
			}
			set
			{
				base.MailboxMoveRemoteHostName = value;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x0002BB0F File Offset: 0x00029D0F
		// (set) Token: 0x0600134E RID: 4942 RVA: 0x0002BB17 File Offset: 0x00029D17
		public string BatchName
		{
			get
			{
				return base.MailboxMoveBatchName;
			}
			set
			{
				base.MailboxMoveBatchName = value;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x0002BB20 File Offset: 0x00029D20
		// (set) Token: 0x06001350 RID: 4944 RVA: 0x0002BB28 File Offset: 0x00029D28
		public ADObjectId SourceMDB
		{
			get
			{
				return base.MailboxMoveSourceMDB;
			}
			set
			{
				base.MailboxMoveSourceMDB = value;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001351 RID: 4945 RVA: 0x0002BB31 File Offset: 0x00029D31
		// (set) Token: 0x06001352 RID: 4946 RVA: 0x0002BB39 File Offset: 0x00029D39
		public ADObjectId TargetMDB
		{
			get
			{
				return base.MailboxMoveTargetMDB;
			}
			set
			{
				base.MailboxMoveTargetMDB = value;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x0002BB42 File Offset: 0x00029D42
		// (set) Token: 0x06001354 RID: 4948 RVA: 0x0002BB4A File Offset: 0x00029D4A
		public ADObjectId StorageMDB
		{
			get
			{
				return base.MailboxMoveStorageMDB;
			}
			set
			{
				base.MailboxMoveStorageMDB = value;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x0002BB53 File Offset: 0x00029D53
		// (set) Token: 0x06001356 RID: 4950 RVA: 0x0002BB5B File Offset: 0x00029D5B
		public string FilePath
		{
			get
			{
				return base.MailboxMoveFilePath;
			}
			set
			{
				base.MailboxMoveFilePath = value;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001357 RID: 4951 RVA: 0x0002BB64 File Offset: 0x00029D64
		// (set) Token: 0x06001358 RID: 4952 RVA: 0x0002BB6C File Offset: 0x00029D6C
		public MRSRequestType Type
		{
			get
			{
				return base.RequestType;
			}
			set
			{
				base.RequestType = value;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x0002BB75 File Offset: 0x00029D75
		// (set) Token: 0x0600135A RID: 4954 RVA: 0x0002BB7D File Offset: 0x00029D7D
		public ADObjectId TargetUserId
		{
			get
			{
				return base.MailboxMoveTargetUser;
			}
			set
			{
				base.MailboxMoveTargetUser = value;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x0600135B RID: 4955 RVA: 0x0002BB86 File Offset: 0x00029D86
		// (set) Token: 0x0600135C RID: 4956 RVA: 0x0002BB8E File Offset: 0x00029D8E
		public ADObjectId SourceUserId
		{
			get
			{
				return base.MailboxMoveSourceUser;
			}
			set
			{
				base.MailboxMoveSourceUser = value;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x0002BB97 File Offset: 0x00029D97
		public RequestIndexId RequestIndexId
		{
			get
			{
				return MRSRequestWrapper.indexId;
			}
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0002BB9E File Offset: 0x00029D9E
		public RequestJobObjectId GetRequestJobId()
		{
			return new RequestJobObjectId(this.RequestGuid, (this.StorageMDB == null) ? Guid.Empty : this.StorageMDB.ObjectGuid, this);
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0002BBC6 File Offset: 0x00029DC6
		public RequestIndexEntryObjectId GetRequestIndexEntryId(RequestBase owner)
		{
			return new RequestIndexEntryObjectId(this.RequestGuid, this.Type, base.OrganizationId, MRSRequestWrapper.indexId, owner);
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x0002BBF2 File Offset: 0x00029DF2
		DateTime? IRequestIndexEntry.get_WhenChanged()
		{
			return base.WhenChanged;
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0002BBFA File Offset: 0x00029DFA
		DateTime? IRequestIndexEntry.get_WhenCreated()
		{
			return base.WhenCreated;
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0002BC02 File Offset: 0x00029E02
		DateTime? IRequestIndexEntry.get_WhenChangedUTC()
		{
			return base.WhenChangedUTC;
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x0002BC0A File Offset: 0x00029E0A
		DateTime? IRequestIndexEntry.get_WhenCreatedUTC()
		{
			return base.WhenCreatedUTC;
		}

		// Token: 0x04000A01 RID: 2561
		private static RequestIndexId indexId = new RequestIndexId(RequestIndexLocation.AD);
	}
}
