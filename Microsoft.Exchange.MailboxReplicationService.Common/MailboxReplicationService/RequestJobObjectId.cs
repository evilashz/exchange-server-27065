using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000209 RID: 521
	[Serializable]
	public class RequestJobObjectId : ObjectId
	{
		// Token: 0x06001AF9 RID: 6905 RVA: 0x000366B7 File Offset: 0x000348B7
		internal RequestJobObjectId(Guid requestGuid, Guid mdbGuid, IRequestIndexEntry indexEntry = null)
		{
			this.requestGuid = requestGuid;
			this.mdbGuid = mdbGuid;
			this.messageId = null;
			this.adUser = null;
			this.sourceUser = null;
			this.targetUser = null;
			this.indexEntry = indexEntry;
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x000366F0 File Offset: 0x000348F0
		internal RequestJobObjectId(Guid requestGuid, Guid mdbGuid, byte[] messageId)
		{
			this.requestGuid = requestGuid;
			this.mdbGuid = mdbGuid;
			this.messageId = messageId;
			this.adUser = null;
			this.sourceUser = null;
			this.targetUser = null;
			this.indexEntry = null;
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0003672C File Offset: 0x0003492C
		internal RequestJobObjectId(ADUser adUser)
		{
			if (adUser == null)
			{
				throw new ArgumentNullException("adUser", "An ADUser must be provided to this constructor");
			}
			this.adUser = adUser;
			this.sourceUser = null;
			this.targetUser = null;
			this.indexEntry = null;
			Guid a;
			Guid a2;
			RequestIndexEntryProvider.GetMoveGuids(adUser, out a, out a2);
			if (a != Guid.Empty && a2 != Guid.Empty)
			{
				this.requestGuid = a;
				this.mdbGuid = a2;
			}
			else
			{
				this.requestGuid = adUser.ExchangeGuid;
				this.mdbGuid = Guid.Empty;
			}
			this.messageId = null;
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06001AFC RID: 6908 RVA: 0x000367C0 File Offset: 0x000349C0
		// (set) Token: 0x06001AFD RID: 6909 RVA: 0x000367C8 File Offset: 0x000349C8
		public Guid RequestGuid
		{
			get
			{
				return this.requestGuid;
			}
			internal set
			{
				this.requestGuid = value;
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06001AFE RID: 6910 RVA: 0x000367D1 File Offset: 0x000349D1
		// (set) Token: 0x06001AFF RID: 6911 RVA: 0x000367D9 File Offset: 0x000349D9
		public Guid MdbGuid
		{
			get
			{
				return this.mdbGuid;
			}
			internal set
			{
				this.mdbGuid = value;
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x000367E2 File Offset: 0x000349E2
		// (set) Token: 0x06001B01 RID: 6913 RVA: 0x000367EA File Offset: 0x000349EA
		internal byte[] MessageId
		{
			get
			{
				return this.messageId;
			}
			set
			{
				this.messageId = value;
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x000367F3 File Offset: 0x000349F3
		// (set) Token: 0x06001B03 RID: 6915 RVA: 0x000367FB File Offset: 0x000349FB
		internal ADUser User
		{
			get
			{
				return this.adUser;
			}
			set
			{
				this.adUser = value;
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06001B04 RID: 6916 RVA: 0x00036804 File Offset: 0x00034A04
		// (set) Token: 0x06001B05 RID: 6917 RVA: 0x0003680C File Offset: 0x00034A0C
		internal ADUser SourceUser
		{
			get
			{
				return this.sourceUser;
			}
			set
			{
				this.sourceUser = value;
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06001B06 RID: 6918 RVA: 0x00036815 File Offset: 0x00034A15
		// (set) Token: 0x06001B07 RID: 6919 RVA: 0x0003681D File Offset: 0x00034A1D
		internal ADUser TargetUser
		{
			get
			{
				return this.targetUser;
			}
			set
			{
				this.targetUser = value;
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06001B08 RID: 6920 RVA: 0x00036826 File Offset: 0x00034A26
		internal IRequestIndexEntry IndexEntry
		{
			get
			{
				return this.indexEntry;
			}
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x00036830 File Offset: 0x00034A30
		public override byte[] GetBytes()
		{
			byte[] array = new byte[32];
			Array.Copy(this.requestGuid.ToByteArray(), array, 16);
			Array.Copy(this.mdbGuid.ToByteArray(), 0, array, 16, 16);
			return array;
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x0003686F File Offset: 0x00034A6F
		public override string ToString()
		{
			if (this.indexEntry != null)
			{
				return this.indexEntry.GetRequestIndexEntryId(null).ToString();
			}
			return string.Format("{0}\\{1}", this.mdbGuid, this.requestGuid);
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x000368AC File Offset: 0x00034AAC
		public bool Equals(RequestJobObjectId id)
		{
			return id != null && (this.requestGuid.Equals(id.RequestGuid) && this.mdbGuid.Equals(id.MdbGuid)) && (this.messageId == null || id.MessageId == null || CommonUtils.IsSameEntryId(this.messageId, id.MessageId));
		}

		// Token: 0x04000AF6 RID: 2806
		private Guid requestGuid;

		// Token: 0x04000AF7 RID: 2807
		private Guid mdbGuid;

		// Token: 0x04000AF8 RID: 2808
		private byte[] messageId;

		// Token: 0x04000AF9 RID: 2809
		[NonSerialized]
		private ADUser adUser;

		// Token: 0x04000AFA RID: 2810
		[NonSerialized]
		private ADUser sourceUser;

		// Token: 0x04000AFB RID: 2811
		[NonSerialized]
		private ADUser targetUser;

		// Token: 0x04000AFC RID: 2812
		[NonSerialized]
		private IRequestIndexEntry indexEntry;
	}
}
