using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000182 RID: 386
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class TestSubscriptionSnapshot : SubscriptionSnapshot, ISerializable
	{
		// Token: 0x06001207 RID: 4615 RVA: 0x0004B9E0 File Offset: 0x00049BE0
		public TestSubscriptionSnapshot(SerializationInfo info, StreamingContext context)
		{
			this.Deserialize(info);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x0004B9F0 File Offset: 0x00049BF0
		public TestSubscriptionSnapshot(Guid? id, SnapshotStatus status, bool isInitialSyncComplete, ExDateTime createTime, ExDateTime? lastUpdateTime, ExDateTime? lastSyncTime, LocalizedString? errorMessage, string batchName) : base(null, status, isInitialSyncComplete, createTime, lastUpdateTime, lastSyncTime, errorMessage, batchName)
		{
			this.Id = id;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x0004BA17 File Offset: 0x00049C17
		private TestSubscriptionSnapshot()
		{
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x0004BA20 File Offset: 0x00049C20
		// (set) Token: 0x0600120B RID: 4619 RVA: 0x0004BA54 File Offset: 0x00049C54
		public new Guid? Id
		{
			get
			{
				if (base.Id == null)
				{
					return null;
				}
				return new Guid?(((MRSSubscriptionId)base.Id).Id);
			}
			private set
			{
				Guid? guid = value;
				if (guid == null)
				{
					base.Id = null;
					return;
				}
				MailboxData mailboxData = new MailboxData(guid.Value, "legDN", new ADObjectId(guid.Value), guid.Value);
				mailboxData.Update("identifier", OrganizationId.ForestWideOrgId);
				ISubscriptionId id = new MRSSubscriptionId(guid.Value, MigrationType.ExchangeOutlookAnywhere, mailboxData);
				base.Id = id;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x0004BABF File Offset: 0x00049CBF
		// (set) Token: 0x0600120D RID: 4621 RVA: 0x0004BAC7 File Offset: 0x00049CC7
		public new bool IsInitialSyncComplete
		{
			get
			{
				return base.IsInitialSyncComplete;
			}
			set
			{
				base.IsInitialSyncComplete = value;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x0004BAD0 File Offset: 0x00049CD0
		// (set) Token: 0x0600120F RID: 4623 RVA: 0x0004BAD8 File Offset: 0x00049CD8
		public new SnapshotStatus Status
		{
			get
			{
				return base.Status;
			}
			set
			{
				base.Status = value;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x0004BAE1 File Offset: 0x00049CE1
		// (set) Token: 0x06001211 RID: 4625 RVA: 0x0004BAE9 File Offset: 0x00049CE9
		public new long NumItemsSkipped
		{
			get
			{
				return base.NumItemsSkipped;
			}
			set
			{
				base.NumItemsSkipped = value;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x0004BAF2 File Offset: 0x00049CF2
		// (set) Token: 0x06001213 RID: 4627 RVA: 0x0004BAFA File Offset: 0x00049CFA
		public new long NumItemsSynced
		{
			get
			{
				return base.NumItemsSynced;
			}
			set
			{
				base.NumItemsSynced = value;
			}
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0004BB04 File Offset: 0x00049D04
		public new static TestSubscriptionSnapshot CreateFailed(LocalizedString errorMessage)
		{
			return new TestSubscriptionSnapshot
			{
				Status = SnapshotStatus.Failed,
				ErrorMessage = new LocalizedString?(errorMessage)
			};
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0004BB2C File Offset: 0x00049D2C
		public static TestSubscriptionSnapshot CreateId(Guid id)
		{
			return new TestSubscriptionSnapshot
			{
				Id = new Guid?(id),
				Status = SnapshotStatus.InProgress
			};
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0004BB53 File Offset: 0x00049D53
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.Serialize(info);
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x0004BB5C File Offset: 0x00049D5C
		private void Deserialize(SerializationInfo info)
		{
			this.Id = (Guid?)info.GetValue("Id", typeof(Guid?));
			this.Status = (SnapshotStatus)info.GetUInt32("Status");
			this.IsInitialSyncComplete = info.GetBoolean("IsInitialSyncComplete");
			base.CreateTime = (ExDateTime)info.GetDateTime("CreateTime");
			DateTime? dateTime = (DateTime?)info.GetValue("LastUpdateTime", typeof(DateTime?));
			if (dateTime != null)
			{
				base.LastUpdateTime = new ExDateTime?((ExDateTime)dateTime.Value);
			}
			DateTime? dateTime2 = (DateTime?)info.GetValue("LastSyncTime", typeof(DateTime?));
			if (dateTime2 != null)
			{
				base.LastSyncTime = new ExDateTime?((ExDateTime)dateTime2.Value);
			}
			base.ErrorMessage = (LocalizedString?)info.GetValue("ErrorMessage", typeof(LocalizedString?));
			this.NumItemsSkipped = info.GetInt64("NumItemsSkipped");
			this.NumItemsSynced = info.GetInt64("NumItemsSynced");
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0004BC7C File Offset: 0x00049E7C
		private void Serialize(SerializationInfo info)
		{
			info.AddValue("Id", this.Id, typeof(Guid?));
			info.AddValue("Status", (uint)this.Status);
			info.AddValue("IsInitialSyncComplete", this.IsInitialSyncComplete);
			info.AddValue("CreateTime", (DateTime)base.CreateTime);
			info.AddValue("LastUpdateTime", (DateTime?)base.LastUpdateTime);
			info.AddValue("LastSyncTime", (DateTime?)base.LastSyncTime);
			info.AddValue("ErrorMessage", base.ErrorMessage, typeof(LocalizedString?));
			info.AddValue("NumItemsSkipped", this.NumItemsSkipped);
			info.AddValue("NumItemsSynced", this.NumItemsSynced);
		}
	}
}
