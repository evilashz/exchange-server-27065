using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x0200003F RID: 63
	internal sealed class ScheduleItem : BackgroundJobBackendBase
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00007D1B File Offset: 0x00005F1B
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00007D2D File Offset: 0x00005F2D
		public Guid ScheduleId
		{
			get
			{
				return (Guid)this[ScheduleItem.ScheduleIdProperty];
			}
			set
			{
				this[ScheduleItem.ScheduleIdProperty] = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00007D40 File Offset: 0x00005F40
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00007D52 File Offset: 0x00005F52
		public Guid BackgroundJobId
		{
			get
			{
				return (Guid)this[ScheduleItem.BackgroundJobIdProperty];
			}
			set
			{
				this[ScheduleItem.BackgroundJobIdProperty] = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00007D65 File Offset: 0x00005F65
		// (set) Token: 0x06000222 RID: 546 RVA: 0x00007D77 File Offset: 0x00005F77
		public SchedulingType SchedulingType
		{
			get
			{
				return (SchedulingType)this[ScheduleItem.SchedulingTypeProperty];
			}
			set
			{
				this[ScheduleItem.SchedulingTypeProperty] = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00007D8A File Offset: 0x00005F8A
		public bool HasStartTime
		{
			get
			{
				return this[ScheduleItem.StartTimeProperty] != null;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00007D9D File Offset: 0x00005F9D
		// (set) Token: 0x06000225 RID: 549 RVA: 0x00007DAF File Offset: 0x00005FAF
		public DateTime StartTime
		{
			get
			{
				return (DateTime)this[ScheduleItem.StartTimeProperty];
			}
			set
			{
				this[ScheduleItem.StartTimeProperty] = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00007DC2 File Offset: 0x00005FC2
		public bool HasSchedulingInterval
		{
			get
			{
				return this[ScheduleItem.SchedulingIntervalProperty] != null;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00007DD5 File Offset: 0x00005FD5
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00007DE7 File Offset: 0x00005FE7
		public int SchedulingInterval
		{
			get
			{
				return (int)this[ScheduleItem.SchedulingIntervalProperty];
			}
			set
			{
				this[ScheduleItem.SchedulingIntervalProperty] = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00007DFA File Offset: 0x00005FFA
		public bool HasScheduleDaysSet
		{
			get
			{
				return this[ScheduleItem.ScheduleDaysSetProperty] != null;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00007E0D File Offset: 0x0000600D
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00007E1F File Offset: 0x0000601F
		public Days ScheduleDaysSet
		{
			get
			{
				return (Days)this[ScheduleItem.ScheduleDaysSetProperty];
			}
			set
			{
				this[ScheduleItem.ScheduleDaysSetProperty] = (byte)value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00007E32 File Offset: 0x00006032
		public bool HasLastScheduledTime
		{
			get
			{
				return this[ScheduleItem.LastScheduledTimeProperty] != null;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00007E45 File Offset: 0x00006045
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00007E57 File Offset: 0x00006057
		public DateTime LastScheduledTime
		{
			get
			{
				return (DateTime)this[ScheduleItem.LastScheduledTimeProperty];
			}
			set
			{
				this[ScheduleItem.LastScheduledTimeProperty] = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00007E6A File Offset: 0x0000606A
		public DateTime CreatedDatetime
		{
			get
			{
				return (DateTime)this[ScheduleItem.CreatedDatetimeProperty];
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00007E7C File Offset: 0x0000607C
		public DateTime ChangedDatetime
		{
			get
			{
				return (DateTime)this[ScheduleItem.ChangedDatetimeProperty];
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00007E8E File Offset: 0x0000608E
		// (set) Token: 0x06000232 RID: 562 RVA: 0x00007EA0 File Offset: 0x000060A0
		public long DCSelectionSet
		{
			get
			{
				return (long)this[ScheduleItem.DCSelectionSetProperty];
			}
			set
			{
				this[ScheduleItem.DCSelectionSetProperty] = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00007EB3 File Offset: 0x000060B3
		// (set) Token: 0x06000234 RID: 564 RVA: 0x00007EBB File Offset: 0x000060BB
		public List<long> DCCollectionSet
		{
			get
			{
				return this.dataCenterIdCollection;
			}
			set
			{
				this.dataCenterIdCollection = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00007EC4 File Offset: 0x000060C4
		// (set) Token: 0x06000236 RID: 566 RVA: 0x00007ED6 File Offset: 0x000060D6
		public Regions RegionSelectionSet
		{
			get
			{
				return (Regions)this[ScheduleItem.RegionSelectionSetProperty];
			}
			set
			{
				this[ScheduleItem.RegionSelectionSetProperty] = (int)value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00007EE9 File Offset: 0x000060E9
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00007EFB File Offset: 0x000060FB
		public string TargetMachineName
		{
			get
			{
				return (string)this[ScheduleItem.TargetMachineNameProperty];
			}
			set
			{
				this[ScheduleItem.TargetMachineNameProperty] = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00007F09 File Offset: 0x00006109
		// (set) Token: 0x0600023A RID: 570 RVA: 0x00007F1B File Offset: 0x0000611B
		public byte InstancesToRun
		{
			get
			{
				return (byte)this[ScheduleItem.InstancesToRunProperty];
			}
			set
			{
				this[ScheduleItem.InstancesToRunProperty] = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00007F2E File Offset: 0x0000612E
		// (set) Token: 0x0600023C RID: 572 RVA: 0x00007F40 File Offset: 0x00006140
		public Guid RoleId
		{
			get
			{
				return (Guid)this[ScheduleItem.RoleIdProperty];
			}
			set
			{
				this[ScheduleItem.RoleIdProperty] = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00007F53 File Offset: 0x00006153
		// (set) Token: 0x0600023E RID: 574 RVA: 0x00007F65 File Offset: 0x00006165
		public bool SingleInstancePerMachine
		{
			get
			{
				return (bool)this[ScheduleItem.SingleInstancePerMachineProperty];
			}
			set
			{
				this[ScheduleItem.SingleInstancePerMachineProperty] = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00007F78 File Offset: 0x00006178
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00007F8A File Offset: 0x0000618A
		public SchedulingStrategyType SchedulingStrategy
		{
			get
			{
				return (SchedulingStrategyType)this[ScheduleItem.SchedulingStrategyProperty];
			}
			set
			{
				this[ScheduleItem.SchedulingStrategyProperty] = (byte)value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00007F9D File Offset: 0x0000619D
		// (set) Token: 0x06000242 RID: 578 RVA: 0x00007FAF File Offset: 0x000061AF
		public int Timeout
		{
			get
			{
				return (int)this[ScheduleItem.TimeoutProperty];
			}
			set
			{
				this[ScheduleItem.TimeoutProperty] = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00007FC2 File Offset: 0x000061C2
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00007FD4 File Offset: 0x000061D4
		public byte MaxLocalRetryCount
		{
			get
			{
				return (byte)this[ScheduleItem.MaxLocalRetryCountProperty];
			}
			set
			{
				this[ScheduleItem.MaxLocalRetryCountProperty] = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00007FE7 File Offset: 0x000061E7
		// (set) Token: 0x06000246 RID: 582 RVA: 0x00007FF9 File Offset: 0x000061F9
		public short MaxFailoverCount
		{
			get
			{
				return (short)this[ScheduleItem.MaxFailoverCountProperty];
			}
			set
			{
				this[ScheduleItem.MaxFailoverCountProperty] = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000800C File Offset: 0x0000620C
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000801E File Offset: 0x0000621E
		public Guid NextActiveJobId
		{
			get
			{
				return (Guid)this[ScheduleItem.NextActiveJobIdProperty];
			}
			set
			{
				this[ScheduleItem.NextActiveJobIdProperty] = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00008031 File Offset: 0x00006231
		// (set) Token: 0x0600024A RID: 586 RVA: 0x00008043 File Offset: 0x00006243
		public bool Active
		{
			get
			{
				return (bool)this[ScheduleItem.ActiveProperty];
			}
			set
			{
				this[ScheduleItem.ActiveProperty] = value;
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00008056 File Offset: 0x00006256
		public void AddDCId(long dcId)
		{
			this.DCSelectionSet |= dcId;
			if (!this.dataCenterIdCollection.Contains(dcId))
			{
				this.dataCenterIdCollection.Add(dcId);
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00008080 File Offset: 0x00006280
		public bool RemoveDCId(long dcId)
		{
			this.DCSelectionSet &= ~dcId;
			return this.dataCenterIdCollection.Remove(dcId);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000809D File Offset: 0x0000629D
		public bool ContainsDCId(long dcId)
		{
			return this.dataCenterIdCollection.Contains(dcId);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000080AB File Offset: 0x000062AB
		public void ClearDCIds()
		{
			this.dataCenterIdCollection.Clear();
		}

		// Token: 0x0600024F RID: 591 RVA: 0x000080B8 File Offset: 0x000062B8
		public void PrepareForSerialization()
		{
			Guid scheduleId = this.ScheduleId;
			BatchPropertyTable batchPropertyTable = new BatchPropertyTable();
			foreach (long num in this.dataCenterIdCollection)
			{
				batchPropertyTable.AddPropertyValue(Guid.NewGuid(), DataCenterDefinition.DataCenterIdProperty, num);
			}
			this[ScheduleItem.SchedIdToDCIdMappingProperty] = batchPropertyTable;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00008134 File Offset: 0x00006334
		public void Deserialize()
		{
		}

		// Token: 0x04000168 RID: 360
		internal static readonly BackgroundJobBackendPropertyDefinition ScheduleIdProperty = ScheduleItemProperties.ScheduleIdProperty;

		// Token: 0x04000169 RID: 361
		internal static readonly BackgroundJobBackendPropertyDefinition BackgroundJobIdProperty = ScheduleItemProperties.BackgroundJobIdProperty;

		// Token: 0x0400016A RID: 362
		internal static readonly BackgroundJobBackendPropertyDefinition SchedulingTypeProperty = ScheduleItemProperties.SchedulingTypeProperty;

		// Token: 0x0400016B RID: 363
		internal static readonly BackgroundJobBackendPropertyDefinition StartTimeProperty = ScheduleItemProperties.StartTimeProperty;

		// Token: 0x0400016C RID: 364
		internal static readonly BackgroundJobBackendPropertyDefinition SchedulingIntervalProperty = ScheduleItemProperties.SchedulingIntervalProperty;

		// Token: 0x0400016D RID: 365
		internal static readonly BackgroundJobBackendPropertyDefinition ScheduleDaysSetProperty = ScheduleItemProperties.ScheduleDaysSetProperty;

		// Token: 0x0400016E RID: 366
		internal static readonly BackgroundJobBackendPropertyDefinition DCSelectionSetProperty = ScheduleItemProperties.DCSelectionSetProperty;

		// Token: 0x0400016F RID: 367
		internal static readonly BackgroundJobBackendPropertyDefinition RegionSelectionSetProperty = ScheduleItemProperties.RegionSelectionSetProperty;

		// Token: 0x04000170 RID: 368
		internal static readonly BackgroundJobBackendPropertyDefinition TargetMachineNameProperty = ScheduleItemProperties.TargetMachineNameProperty;

		// Token: 0x04000171 RID: 369
		internal static readonly BackgroundJobBackendPropertyDefinition InstancesToRunProperty = ScheduleItemProperties.InstancesToRunProperty;

		// Token: 0x04000172 RID: 370
		internal static readonly BackgroundJobBackendPropertyDefinition RoleIdProperty = ScheduleItemProperties.RoleIdProperty;

		// Token: 0x04000173 RID: 371
		internal static readonly BackgroundJobBackendPropertyDefinition SingleInstancePerMachineProperty = ScheduleItemProperties.SingleInstancePerMachineProperty;

		// Token: 0x04000174 RID: 372
		internal static readonly BackgroundJobBackendPropertyDefinition SchedulingStrategyProperty = ScheduleItemProperties.SchedulingStrategyProperty;

		// Token: 0x04000175 RID: 373
		internal static readonly BackgroundJobBackendPropertyDefinition TimeoutProperty = ScheduleItemProperties.TimeoutProperty;

		// Token: 0x04000176 RID: 374
		internal static readonly BackgroundJobBackendPropertyDefinition MaxLocalRetryCountProperty = ScheduleItemProperties.MaxLocalRetryCountProperty;

		// Token: 0x04000177 RID: 375
		internal static readonly BackgroundJobBackendPropertyDefinition MaxFailoverCountProperty = ScheduleItemProperties.MaxFailoverCountProperty;

		// Token: 0x04000178 RID: 376
		internal static readonly BackgroundJobBackendPropertyDefinition NextActiveJobIdProperty = ScheduleItemProperties.NextActiveJobIdProperty;

		// Token: 0x04000179 RID: 377
		internal static readonly BackgroundJobBackendPropertyDefinition LastScheduledTimeProperty = ScheduleItemProperties.LastScheduledTimeProperty;

		// Token: 0x0400017A RID: 378
		internal static readonly BackgroundJobBackendPropertyDefinition CreatedDatetimeProperty = ScheduleItemProperties.CreatedDatetimeProperty;

		// Token: 0x0400017B RID: 379
		internal static readonly BackgroundJobBackendPropertyDefinition ChangedDatetimeProperty = ScheduleItemProperties.ChangedDatetimeProperty;

		// Token: 0x0400017C RID: 380
		internal static readonly BackgroundJobBackendPropertyDefinition ActiveProperty = ScheduleItemProperties.ActiveProperty;

		// Token: 0x0400017D RID: 381
		internal static readonly BackgroundJobBackendPropertyDefinition SchedIdToDCIdMappingProperty = ScheduleItemProperties.SchedIdToDCIdMappingProperty;

		// Token: 0x0400017E RID: 382
		private List<long> dataCenterIdCollection = new List<long>();
	}
}
