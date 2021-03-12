using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000FF RID: 255
	[DataContract]
	public class DatabaseCopyStatus
	{
		// Token: 0x06001EF4 RID: 7924 RVA: 0x0005CB90 File Offset: 0x0005AD90
		public DatabaseCopyStatus(DatabaseCopyStatusEntry statusEntry)
		{
			this.Name = statusEntry.Name;
			this.RawIdentity = statusEntry.Id.ObjectGuid.ToString();
			this.IsActive = statusEntry.ActiveCopy;
			this.StatusString = LocalizedDescriptionAttribute.FromEnum(typeof(CopyStatus), statusEntry.Status);
			this.CopyQueueLength = ((statusEntry.CopyQueueLength != null) ? statusEntry.CopyQueueLength.Value.ToString() : "0");
			this.ContentIndexStateString = LocalizedDescriptionAttribute.FromEnum(typeof(ContentIndexStatusType), statusEntry.ContentIndexState);
			this.SuspendComment = statusEntry.SuspendComment;
			this.CanSuspend = (statusEntry.Status == CopyStatus.Failed || statusEntry.Status == CopyStatus.Seeding || statusEntry.Status == CopyStatus.Healthy || statusEntry.Status == CopyStatus.Initializing || statusEntry.Status == CopyStatus.Resynchronizing || statusEntry.Status == CopyStatus.DisconnectedAndHealthy || statusEntry.Status == CopyStatus.DisconnectedAndResynchronizing);
			this.CanResume = (statusEntry.Status == CopyStatus.Suspended || statusEntry.Status == CopyStatus.FailedAndSuspended);
			this.CanRemove = (statusEntry.Status == CopyStatus.Failed || statusEntry.Status == CopyStatus.Seeding || statusEntry.Status == CopyStatus.Suspended || statusEntry.Status == CopyStatus.Healthy || statusEntry.Status == CopyStatus.Initializing || statusEntry.Status == CopyStatus.Resynchronizing || statusEntry.Status == CopyStatus.DisconnectedAndHealthy || statusEntry.Status == CopyStatus.FailedAndSuspended || statusEntry.Status == CopyStatus.DisconnectedAndResynchronizing || statusEntry.Status == CopyStatus.NonExchangeReplication || statusEntry.Status == CopyStatus.SeedingSource || statusEntry.Status == CopyStatus.Misconfigured);
			this.CanActivate = (statusEntry.Status == CopyStatus.Healthy || statusEntry.Status == CopyStatus.DisconnectedAndHealthy || statusEntry.Status == CopyStatus.DisconnectedAndResynchronizing || statusEntry.Status == CopyStatus.Initializing || statusEntry.Status == CopyStatus.Resynchronizing);
			this.CanUpdate = (statusEntry.Status == CopyStatus.Suspended || statusEntry.Status == CopyStatus.FailedAndSuspended);
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x0005CD94 File Offset: 0x0005AF94
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			DatabaseCopyStatus databaseCopyStatus = obj as DatabaseCopyStatus;
			return databaseCopyStatus != null && (string.Equals(this.Name, databaseCopyStatus.Name) && string.Equals(this.RawIdentity, databaseCopyStatus.RawIdentity) && string.Equals(this.StatusString, databaseCopyStatus.StatusString) && string.Equals(this.CopyQueueLength, databaseCopyStatus.CopyQueueLength) && string.Equals(this.ContentIndexStateString, databaseCopyStatus.ContentIndexStateString) && string.Equals(this.SuspendComment, databaseCopyStatus.SuspendComment) && this.IsActive == databaseCopyStatus.IsActive && this.CanSuspend == databaseCopyStatus.CanSuspend && this.CanResume == databaseCopyStatus.CanResume && this.CanRemove == databaseCopyStatus.CanRemove && this.CanActivate == databaseCopyStatus.CanActivate) && this.CanUpdate == databaseCopyStatus.CanUpdate;
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x0005CE83 File Offset: 0x0005B083
		public override int GetHashCode()
		{
			return this.RawIdentity.GetHashCode();
		}

		// Token: 0x170019F1 RID: 6641
		// (get) Token: 0x06001EF7 RID: 7927 RVA: 0x0005CE90 File Offset: 0x0005B090
		// (set) Token: 0x06001EF8 RID: 7928 RVA: 0x0005CE98 File Offset: 0x0005B098
		[DataMember]
		public string Name { get; set; }

		// Token: 0x170019F2 RID: 6642
		// (get) Token: 0x06001EF9 RID: 7929 RVA: 0x0005CEA1 File Offset: 0x0005B0A1
		// (set) Token: 0x06001EFA RID: 7930 RVA: 0x0005CEA9 File Offset: 0x0005B0A9
		[DataMember]
		public string RawIdentity { get; set; }

		// Token: 0x170019F3 RID: 6643
		// (get) Token: 0x06001EFB RID: 7931 RVA: 0x0005CEB2 File Offset: 0x0005B0B2
		// (set) Token: 0x06001EFC RID: 7932 RVA: 0x0005CEBA File Offset: 0x0005B0BA
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x170019F4 RID: 6644
		// (get) Token: 0x06001EFD RID: 7933 RVA: 0x0005CEC3 File Offset: 0x0005B0C3
		// (set) Token: 0x06001EFE RID: 7934 RVA: 0x0005CECB File Offset: 0x0005B0CB
		[DataMember]
		public string StatusString { get; set; }

		// Token: 0x170019F5 RID: 6645
		// (get) Token: 0x06001EFF RID: 7935 RVA: 0x0005CED4 File Offset: 0x0005B0D4
		// (set) Token: 0x06001F00 RID: 7936 RVA: 0x0005CEDC File Offset: 0x0005B0DC
		[DataMember]
		public string CopyQueueLength { get; set; }

		// Token: 0x170019F6 RID: 6646
		// (get) Token: 0x06001F01 RID: 7937 RVA: 0x0005CEE5 File Offset: 0x0005B0E5
		// (set) Token: 0x06001F02 RID: 7938 RVA: 0x0005CEED File Offset: 0x0005B0ED
		[DataMember]
		public string ContentIndexStateString { get; set; }

		// Token: 0x170019F7 RID: 6647
		// (get) Token: 0x06001F03 RID: 7939 RVA: 0x0005CEF6 File Offset: 0x0005B0F6
		// (set) Token: 0x06001F04 RID: 7940 RVA: 0x0005CEFE File Offset: 0x0005B0FE
		[DataMember]
		public string SuspendComment { get; set; }

		// Token: 0x170019F8 RID: 6648
		// (get) Token: 0x06001F05 RID: 7941 RVA: 0x0005CF07 File Offset: 0x0005B107
		// (set) Token: 0x06001F06 RID: 7942 RVA: 0x0005CF0F File Offset: 0x0005B10F
		[DataMember]
		public bool CanSuspend { get; set; }

		// Token: 0x170019F9 RID: 6649
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x0005CF18 File Offset: 0x0005B118
		// (set) Token: 0x06001F08 RID: 7944 RVA: 0x0005CF20 File Offset: 0x0005B120
		[DataMember]
		public bool CanResume { get; set; }

		// Token: 0x170019FA RID: 6650
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x0005CF29 File Offset: 0x0005B129
		// (set) Token: 0x06001F0A RID: 7946 RVA: 0x0005CF31 File Offset: 0x0005B131
		[DataMember]
		public bool CanRemove { get; set; }

		// Token: 0x170019FB RID: 6651
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x0005CF3A File Offset: 0x0005B13A
		// (set) Token: 0x06001F0C RID: 7948 RVA: 0x0005CF42 File Offset: 0x0005B142
		[DataMember]
		public bool CanActivate { get; set; }

		// Token: 0x170019FC RID: 6652
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x0005CF4B File Offset: 0x0005B14B
		// (set) Token: 0x06001F0E RID: 7950 RVA: 0x0005CF53 File Offset: 0x0005B153
		[DataMember]
		public bool CanUpdate { get; set; }
	}
}
