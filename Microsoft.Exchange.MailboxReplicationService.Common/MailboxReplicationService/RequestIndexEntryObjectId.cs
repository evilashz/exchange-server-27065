using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001DB RID: 475
	[Serializable]
	public sealed class RequestIndexEntryObjectId : ObjectId
	{
		// Token: 0x0600139A RID: 5018 RVA: 0x0002C7A9 File Offset: 0x0002A9A9
		public RequestIndexEntryObjectId(Guid requestGuid, MRSRequestType requestType, OrganizationId organizationId, RequestIndexId indexId, RequestBase owner = null)
		{
			this.RequestGuid = requestGuid;
			this.RequestType = requestType;
			this.OrganizationId = organizationId;
			this.IndexId = indexId;
			this.RequestObject = owner;
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0002C7D6 File Offset: 0x0002A9D6
		public RequestIndexEntryObjectId(Guid requestGuid, Guid targetExchangeGuid, MRSRequestType requestType, OrganizationId organizationId, RequestIndexId indexId, RequestBase owner = null) : this(requestGuid, requestType, organizationId, indexId, owner)
		{
			this.TargetExchangeGuid = targetExchangeGuid;
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x0002C7ED File Offset: 0x0002A9ED
		// (set) Token: 0x0600139D RID: 5021 RVA: 0x0002C7F5 File Offset: 0x0002A9F5
		public Guid RequestGuid { get; private set; }

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x0002C7FE File Offset: 0x0002A9FE
		// (set) Token: 0x0600139F RID: 5023 RVA: 0x0002C806 File Offset: 0x0002AA06
		public MRSRequestType RequestType { get; private set; }

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x0002C80F File Offset: 0x0002AA0F
		// (set) Token: 0x060013A1 RID: 5025 RVA: 0x0002C817 File Offset: 0x0002AA17
		public RequestIndexId IndexId { get; private set; }

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x0002C820 File Offset: 0x0002AA20
		// (set) Token: 0x060013A3 RID: 5027 RVA: 0x0002C828 File Offset: 0x0002AA28
		public OrganizationId OrganizationId { get; private set; }

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x0002C831 File Offset: 0x0002AA31
		// (set) Token: 0x060013A5 RID: 5029 RVA: 0x0002C839 File Offset: 0x0002AA39
		public RequestBase RequestObject { get; private set; }

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x0002C842 File Offset: 0x0002AA42
		// (set) Token: 0x060013A7 RID: 5031 RVA: 0x0002C84A File Offset: 0x0002AA4A
		public Guid TargetExchangeGuid { get; private set; }

		// Token: 0x060013A8 RID: 5032 RVA: 0x0002C854 File Offset: 0x0002AA54
		public override byte[] GetBytes()
		{
			return this.RequestGuid.ToByteArray();
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0002C870 File Offset: 0x0002AA70
		public override string ToString()
		{
			if (this.RequestObject != null)
			{
				return this.RequestObject.ToString();
			}
			if (this.OrganizationId != null && this.OrganizationId.OrganizationalUnit != null)
			{
				return string.Format("{0}\\{1}", this.OrganizationId.OrganizationalUnit.Name, this.RequestGuid.ToString());
			}
			return string.Format("{0}", this.RequestGuid);
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0002C8F0 File Offset: 0x0002AAF0
		public override bool Equals(object obj)
		{
			RequestIndexEntryObjectId requestIndexEntryObjectId = obj as RequestIndexEntryObjectId;
			if (requestIndexEntryObjectId != null)
			{
				bool flag = (this.IndexId != null) ? this.IndexId.Equals(requestIndexEntryObjectId.IndexId) : (requestIndexEntryObjectId.IndexId == null);
				return this.RequestGuid == requestIndexEntryObjectId.RequestGuid && this.RequestType == requestIndexEntryObjectId.RequestType && this.OrganizationId == requestIndexEntryObjectId.OrganizationId && flag && this.TargetExchangeGuid == requestIndexEntryObjectId.TargetExchangeGuid;
			}
			return false;
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0002C978 File Offset: 0x0002AB78
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
