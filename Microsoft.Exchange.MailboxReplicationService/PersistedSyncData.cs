using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000051 RID: 81
	public sealed class PersistedSyncData : XMLSerializableBase
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x000194BF File Offset: 0x000176BF
		public PersistedSyncData() : this(Guid.Empty)
		{
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000194CC File Offset: 0x000176CC
		internal PersistedSyncData(Guid moveRequestGuid)
		{
			this.MoveRequestGuid = moveRequestGuid;
			this.BadItems = new EntryIdMap<BadItemMarker>();
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x000194E6 File Offset: 0x000176E6
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x000194EE File Offset: 0x000176EE
		[XmlElement(ElementName = "MoveRequestGuid")]
		public Guid MoveRequestGuid { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x000194F7 File Offset: 0x000176F7
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x000194FF File Offset: 0x000176FF
		[XmlIgnore]
		public SyncStage SyncStage { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00019508 File Offset: 0x00017708
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x00019510 File Offset: 0x00017710
		[XmlElement(ElementName = "SyncStage")]
		public int SyncStageInt
		{
			get
			{
				return (int)this.SyncStage;
			}
			set
			{
				this.SyncStage = (SyncStage)value;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00019519 File Offset: 0x00017719
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x00019521 File Offset: 0x00017721
		[XmlIgnore]
		public EntryIdMap<BadItemMarker> BadItems { get; private set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0001952C File Offset: 0x0001772C
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x0001959C File Offset: 0x0001779C
		[XmlArray("BadItems")]
		[XmlArrayItem("BadItem")]
		public BadItemMarker[] BadItemsArray
		{
			get
			{
				BadItemMarker[] array = new BadItemMarker[this.BadItems.Count];
				int num = 0;
				foreach (BadItemMarker badItemMarker in this.BadItems.Values)
				{
					array[num++] = badItemMarker;
				}
				return array;
			}
			set
			{
				this.BadItems.Clear();
				if (value != null)
				{
					for (int i = 0; i < value.Length; i++)
					{
						BadItemMarker badItemMarker = value[i];
						this.BadItems.Add(badItemMarker.EntryId, badItemMarker);
					}
				}
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x000195DD File Offset: 0x000177DD
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x000195E5 File Offset: 0x000177E5
		[XmlIgnore]
		public PostMoveCleanupStatusFlags CompletedCleanupTasks { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x000195EE File Offset: 0x000177EE
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x000195F6 File Offset: 0x000177F6
		[XmlElement(ElementName = "CompletedCleanupTasks")]
		public int CompletedCleanupTasksInt
		{
			get
			{
				return (int)this.CompletedCleanupTasks;
			}
			set
			{
				this.CompletedCleanupTasks = (PostMoveCleanupStatusFlags)value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x000195FF File Offset: 0x000177FF
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x00019607 File Offset: 0x00017807
		[XmlElement(ElementName = "CleanupRetryAttempts")]
		public int CleanupRetryAttempts { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00019610 File Offset: 0x00017810
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x00019618 File Offset: 0x00017818
		[XmlElement(ElementName = "MailboxSignatureVersion")]
		public uint MailboxSignatureVersion { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00019621 File Offset: 0x00017821
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x00019629 File Offset: 0x00017829
		[XmlElement(ElementName = "InternalLegacyExchangeDN")]
		public string InternalLegacyExchangeDN { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00019632 File Offset: 0x00017832
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x0001963A File Offset: 0x0001783A
		[XmlElement(ElementName = "ExternalLegacyExchangeDN")]
		public string ExternalLegacyExchangeDN { get; set; }

		// Token: 0x0600043F RID: 1087 RVA: 0x00019643 File Offset: 0x00017843
		internal static PersistedSyncData Deserialize(string data)
		{
			return XMLSerializableBase.Deserialize<PersistedSyncData>(data, true);
		}
	}
}
