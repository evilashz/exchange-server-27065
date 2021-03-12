using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200000A RID: 10
	[KnownType(typeof(DxStoreCommand.CreateKey))]
	[KnownType(typeof(DxStoreCommand.ExecuteBatch))]
	[KnownType(typeof(DxStoreCommand.ApplySnapshot))]
	[KnownType(typeof(DxStoreCommand.PromoteToLeader))]
	[KnownType(typeof(DxStoreCommand.DeleteKey))]
	[KnownType(typeof(DxStoreCommand.SetProperty))]
	[KnownType(typeof(DxStoreCommand.DummyCommand))]
	[KnownType(typeof(DxStoreCommand.DeleteProperty))]
	[Serializable]
	public class DxStoreCommand
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000021ED File Offset: 0x000003ED
		public DxStoreCommand()
		{
			this.CommandId = Guid.NewGuid();
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002200 File Offset: 0x00000400
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002208 File Offset: 0x00000408
		[DataMember]
		public bool IsNotifyInitiator { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002211 File Offset: 0x00000411
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002219 File Offset: 0x00000419
		[DataMember]
		public string Initiator { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002222 File Offset: 0x00000422
		// (set) Token: 0x06000026 RID: 38 RVA: 0x0000222A File Offset: 0x0000042A
		[DataMember]
		public DateTimeOffset TimeInitiated { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002233 File Offset: 0x00000433
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000223B File Offset: 0x0000043B
		[DataMember]
		public Guid CommandId { get; set; }

		// Token: 0x06000029 RID: 41 RVA: 0x00002244 File Offset: 0x00000444
		public virtual string GetDebugString()
		{
			return string.Format("Id={0}, From={1}, Time={2}, IsNotify={3}", new object[]
			{
				this.CommandId,
				this.Initiator,
				this.TimeInitiated.ToShortString(),
				this.IsNotifyInitiator
			});
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002296 File Offset: 0x00000496
		public virtual WellKnownCommandName GetTypeId()
		{
			return WellKnownCommandName.Unknown;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002299 File Offset: 0x00000499
		public void Initialize(string self, WriteOptions options)
		{
			this.TimeInitiated = DateTimeOffset.Now;
			this.Initiator = self;
			if (options != null)
			{
				this.IsNotifyInitiator = options.IsWaitRequired();
			}
		}

		// Token: 0x0200000B RID: 11
		[Serializable]
		public class CreateKey : DxStoreCommand
		{
			// Token: 0x1700000C RID: 12
			// (get) Token: 0x0600002C RID: 44 RVA: 0x000022BC File Offset: 0x000004BC
			// (set) Token: 0x0600002D RID: 45 RVA: 0x000022C4 File Offset: 0x000004C4
			[DataMember]
			public string KeyName { get; set; }

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x0600002E RID: 46 RVA: 0x000022CD File Offset: 0x000004CD
			// (set) Token: 0x0600002F RID: 47 RVA: 0x000022D5 File Offset: 0x000004D5
			[DataMember]
			public string SubkeyName { get; set; }

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x06000030 RID: 48 RVA: 0x000022DE File Offset: 0x000004DE
			public string FullKeyName
			{
				get
				{
					return Utils.CombinePathNullSafe(this.KeyName, this.SubkeyName);
				}
			}

			// Token: 0x06000031 RID: 49 RVA: 0x000022F1 File Offset: 0x000004F1
			public override string GetDebugString()
			{
				return string.Format("{0}, ParentKey={1}, SubKey={2}", base.GetDebugString(), this.KeyName, this.SubkeyName);
			}

			// Token: 0x06000032 RID: 50 RVA: 0x0000230F File Offset: 0x0000050F
			public override WellKnownCommandName GetTypeId()
			{
				return WellKnownCommandName.CreateKey;
			}
		}

		// Token: 0x0200000C RID: 12
		[Serializable]
		public class DeleteKey : DxStoreCommand
		{
			// Token: 0x1700000F RID: 15
			// (get) Token: 0x06000034 RID: 52 RVA: 0x0000231A File Offset: 0x0000051A
			// (set) Token: 0x06000035 RID: 53 RVA: 0x00002322 File Offset: 0x00000522
			[DataMember]
			public string KeyName { get; set; }

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x06000036 RID: 54 RVA: 0x0000232B File Offset: 0x0000052B
			// (set) Token: 0x06000037 RID: 55 RVA: 0x00002333 File Offset: 0x00000533
			[DataMember]
			public string SubkeyName { get; set; }

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000038 RID: 56 RVA: 0x0000233C File Offset: 0x0000053C
			public string FullKeyName
			{
				get
				{
					return Utils.CombinePathNullSafe(this.KeyName, this.SubkeyName);
				}
			}

			// Token: 0x06000039 RID: 57 RVA: 0x0000234F File Offset: 0x0000054F
			public override string GetDebugString()
			{
				return string.Format("{0}, ParentKey={1}, SubKey={2}", base.GetDebugString(), this.KeyName, this.SubkeyName);
			}

			// Token: 0x0600003A RID: 58 RVA: 0x0000236D File Offset: 0x0000056D
			public override WellKnownCommandName GetTypeId()
			{
				return WellKnownCommandName.DeleteKey;
			}
		}

		// Token: 0x0200000D RID: 13
		[Serializable]
		public class SetProperty : DxStoreCommand
		{
			// Token: 0x17000012 RID: 18
			// (get) Token: 0x0600003C RID: 60 RVA: 0x00002378 File Offset: 0x00000578
			// (set) Token: 0x0600003D RID: 61 RVA: 0x00002380 File Offset: 0x00000580
			[DataMember]
			public string KeyName { get; set; }

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x0600003E RID: 62 RVA: 0x00002389 File Offset: 0x00000589
			// (set) Token: 0x0600003F RID: 63 RVA: 0x00002391 File Offset: 0x00000591
			[DataMember]
			public string Name { get; set; }

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000040 RID: 64 RVA: 0x0000239A File Offset: 0x0000059A
			// (set) Token: 0x06000041 RID: 65 RVA: 0x000023A2 File Offset: 0x000005A2
			[DataMember]
			public PropertyValue Value { get; set; }

			// Token: 0x06000042 RID: 66 RVA: 0x000023AB File Offset: 0x000005AB
			public override WellKnownCommandName GetTypeId()
			{
				return WellKnownCommandName.SetProperty;
			}

			// Token: 0x06000043 RID: 67 RVA: 0x000023B0 File Offset: 0x000005B0
			public override string GetDebugString()
			{
				return string.Format("{0}, Key={1}, Property={2}, {3}", new object[]
				{
					base.GetDebugString(),
					this.KeyName,
					this.Name,
					this.Value.GetDebugString()
				});
			}
		}

		// Token: 0x0200000E RID: 14
		[Serializable]
		public class DeleteProperty : DxStoreCommand
		{
			// Token: 0x17000015 RID: 21
			// (get) Token: 0x06000045 RID: 69 RVA: 0x00002400 File Offset: 0x00000600
			// (set) Token: 0x06000046 RID: 70 RVA: 0x00002408 File Offset: 0x00000608
			[DataMember]
			public string KeyName { get; set; }

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x06000047 RID: 71 RVA: 0x00002411 File Offset: 0x00000611
			// (set) Token: 0x06000048 RID: 72 RVA: 0x00002419 File Offset: 0x00000619
			[DataMember]
			public string Name { get; set; }

			// Token: 0x06000049 RID: 73 RVA: 0x00002422 File Offset: 0x00000622
			public override WellKnownCommandName GetTypeId()
			{
				return WellKnownCommandName.DeleteProperty;
			}

			// Token: 0x0600004A RID: 74 RVA: 0x00002425 File Offset: 0x00000625
			public override string GetDebugString()
			{
				return string.Format("{0}, Key={1}, Property={2}, ", this.KeyName, this.Name, base.GetDebugString());
			}
		}

		// Token: 0x0200000F RID: 15
		[Serializable]
		public class ExecuteBatch : DxStoreCommand
		{
			// Token: 0x17000017 RID: 23
			// (get) Token: 0x0600004C RID: 76 RVA: 0x0000244B File Offset: 0x0000064B
			// (set) Token: 0x0600004D RID: 77 RVA: 0x00002453 File Offset: 0x00000653
			[DataMember]
			public string KeyName { get; set; }

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x0600004E RID: 78 RVA: 0x0000245C File Offset: 0x0000065C
			// (set) Token: 0x0600004F RID: 79 RVA: 0x00002464 File Offset: 0x00000664
			[DataMember]
			public DxStoreBatchCommand[] Commands { get; set; }

			// Token: 0x06000050 RID: 80 RVA: 0x0000246D File Offset: 0x0000066D
			public override WellKnownCommandName GetTypeId()
			{
				return WellKnownCommandName.ExecuteBatch;
			}

			// Token: 0x06000051 RID: 81 RVA: 0x00002470 File Offset: 0x00000670
			public override string GetDebugString()
			{
				return string.Format("{0}, Key={1}, CommandsCount={2}, ", this.KeyName, this.Commands.Length, base.GetDebugString());
			}
		}

		// Token: 0x02000010 RID: 16
		[Serializable]
		public class ApplySnapshot : DxStoreCommand
		{
			// Token: 0x17000019 RID: 25
			// (get) Token: 0x06000053 RID: 83 RVA: 0x0000249D File Offset: 0x0000069D
			// (set) Token: 0x06000054 RID: 84 RVA: 0x000024A5 File Offset: 0x000006A5
			[DataMember]
			public InstanceSnapshotInfo SnapshotInfo { get; set; }

			// Token: 0x06000055 RID: 85 RVA: 0x000024AE File Offset: 0x000006AE
			public override WellKnownCommandName GetTypeId()
			{
				return WellKnownCommandName.ApplySnapshot;
			}

			// Token: 0x06000056 RID: 86 RVA: 0x000024B4 File Offset: 0x000006B4
			public override string GetDebugString()
			{
				return string.Format("{0}, Key={1}, Size={2}, IsCompressed={3}", new object[]
				{
					base.GetDebugString(),
					this.SnapshotInfo.FullKeyName,
					this.SnapshotInfo.Snapshot.Length,
					this.SnapshotInfo.IsCompressed
				});
			}
		}

		// Token: 0x02000011 RID: 17
		[Serializable]
		public class PromoteToLeader : DxStoreCommand
		{
			// Token: 0x06000058 RID: 88 RVA: 0x0000251D File Offset: 0x0000071D
			public override WellKnownCommandName GetTypeId()
			{
				return WellKnownCommandName.PromoteToLeader;
			}
		}

		// Token: 0x02000012 RID: 18
		[Serializable]
		public class DummyCommand : DxStoreCommand
		{
			// Token: 0x1700001A RID: 26
			// (get) Token: 0x0600005A RID: 90 RVA: 0x00002528 File Offset: 0x00000728
			// (set) Token: 0x0600005B RID: 91 RVA: 0x00002530 File Offset: 0x00000730
			[DataMember]
			public Guid OriginalDbCommandId { get; set; }

			// Token: 0x0600005C RID: 92 RVA: 0x00002539 File Offset: 0x00000739
			public override WellKnownCommandName GetTypeId()
			{
				return WellKnownCommandName.DummyCmd;
			}

			// Token: 0x0600005D RID: 93 RVA: 0x0000253C File Offset: 0x0000073C
			public override string GetDebugString()
			{
				return string.Format("{0}, OrgCmdId={1}", base.GetDebugString(), this.OriginalDbCommandId);
			}
		}
	}
}
