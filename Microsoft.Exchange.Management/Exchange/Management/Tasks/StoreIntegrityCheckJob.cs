using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000D9F RID: 3487
	[Serializable]
	public class StoreIntegrityCheckJob : ConfigurableObject
	{
		// Token: 0x17002989 RID: 10633
		// (get) Token: 0x06008593 RID: 34195 RVA: 0x00222BC7 File Offset: 0x00220DC7
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return StoreIntegrityCheckJob.schema;
			}
		}

		// Token: 0x1700298A RID: 10634
		// (get) Token: 0x06008594 RID: 34196 RVA: 0x00222BD0 File Offset: 0x00220DD0
		public override ObjectId Identity
		{
			get
			{
				if (!(this.requestGuid != Guid.Empty))
				{
					return null;
				}
				if (this.jobGuid == Guid.Empty)
				{
					return new StoreIntegrityCheckJobIdentity(this.databaseId.Guid, this.requestGuid);
				}
				return new StoreIntegrityCheckJobIdentity(this.databaseId.Guid, this.requestGuid, this.jobGuid);
			}
		}

		// Token: 0x1700298B RID: 10635
		// (get) Token: 0x06008595 RID: 34197 RVA: 0x00222C36 File Offset: 0x00220E36
		public MailboxId Mailbox
		{
			get
			{
				if (this.mailboxGuid != Guid.Empty)
				{
					return new MailboxId(this.databaseId, this.mailboxGuid);
				}
				return null;
			}
		}

		// Token: 0x1700298C RID: 10636
		// (get) Token: 0x06008596 RID: 34198 RVA: 0x00222C5D File Offset: 0x00220E5D
		public JobSource Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700298D RID: 10637
		// (get) Token: 0x06008597 RID: 34199 RVA: 0x00222C65 File Offset: 0x00220E65
		public JobPriority Priority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x1700298E RID: 10638
		// (get) Token: 0x06008598 RID: 34200 RVA: 0x00222C6D File Offset: 0x00220E6D
		public bool DetectOnly
		{
			get
			{
				return (this.flags & JobFlags.DetectOnly) == JobFlags.DetectOnly;
			}
		}

		// Token: 0x1700298F RID: 10639
		// (get) Token: 0x06008599 RID: 34201 RVA: 0x00222C7A File Offset: 0x00220E7A
		public JobState JobState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17002990 RID: 10640
		// (get) Token: 0x0600859A RID: 34202 RVA: 0x00222C82 File Offset: 0x00220E82
		public short Progress
		{
			get
			{
				return this.progress;
			}
		}

		// Token: 0x17002991 RID: 10641
		// (get) Token: 0x0600859B RID: 34203 RVA: 0x00222C8A File Offset: 0x00220E8A
		public MailboxCorruptionType[] Tasks
		{
			get
			{
				return this.tasks;
			}
		}

		// Token: 0x17002992 RID: 10642
		// (get) Token: 0x0600859C RID: 34204 RVA: 0x00222C92 File Offset: 0x00220E92
		public DateTime? CreationTime
		{
			get
			{
				return this.creationTime;
			}
		}

		// Token: 0x17002993 RID: 10643
		// (get) Token: 0x0600859D RID: 34205 RVA: 0x00222C9A File Offset: 0x00220E9A
		public DateTime? FinishTime
		{
			get
			{
				return this.finishTime;
			}
		}

		// Token: 0x17002994 RID: 10644
		// (get) Token: 0x0600859E RID: 34206 RVA: 0x00222CA2 File Offset: 0x00220EA2
		public DateTime? LastExecutionTime
		{
			get
			{
				return this.lastExecutionTime;
			}
		}

		// Token: 0x17002995 RID: 10645
		// (get) Token: 0x0600859F RID: 34207 RVA: 0x00222CAA File Offset: 0x00220EAA
		public int CorruptionsDetected
		{
			get
			{
				return this.corruptionsDetected;
			}
		}

		// Token: 0x17002996 RID: 10646
		// (get) Token: 0x060085A0 RID: 34208 RVA: 0x00222CB2 File Offset: 0x00220EB2
		public int? ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x17002997 RID: 10647
		// (get) Token: 0x060085A1 RID: 34209 RVA: 0x00222CBA File Offset: 0x00220EBA
		public int CorruptionsFixed
		{
			get
			{
				return this.corruptionsFixed;
			}
		}

		// Token: 0x17002998 RID: 10648
		// (get) Token: 0x060085A2 RID: 34210 RVA: 0x00222CC2 File Offset: 0x00220EC2
		public TimeSpan? TimeInServer
		{
			get
			{
				return this.timeInServer;
			}
		}

		// Token: 0x17002999 RID: 10649
		// (get) Token: 0x060085A3 RID: 34211 RVA: 0x00222CCA File Offset: 0x00220ECA
		public StoreIntegrityCheckJob.Corruption[] Corruptions
		{
			get
			{
				return this.corruptions;
			}
		}

		// Token: 0x060085A4 RID: 34212 RVA: 0x00222CD2 File Offset: 0x00220ED2
		public StoreIntegrityCheckJob() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x060085A5 RID: 34213 RVA: 0x00222CDF File Offset: 0x00220EDF
		internal StoreIntegrityCheckJob(SimpleProviderPropertyBag bag) : base(bag)
		{
		}

		// Token: 0x060085A6 RID: 34214 RVA: 0x00222CE8 File Offset: 0x00220EE8
		internal StoreIntegrityCheckJob(DatabaseId databaseId, Guid requestGuid, JobFlags flags, MailboxCorruptionType[] taskIds) : this()
		{
			this.creationTime = new DateTime?(DateTime.UtcNow.ToLocalTime());
			this.flags = flags;
			this.tasks = taskIds;
			this.timeInServer = null;
		}

		// Token: 0x060085A7 RID: 34215 RVA: 0x00222D30 File Offset: 0x00220F30
		internal StoreIntegrityCheckJob(DatabaseId databaseId, Guid mailboxGuid, Guid requestGuid, Guid jobGuid, JobFlags flags, MailboxCorruptionType[] taskIds, JobState state, JobSource source, JobPriority priority, short progress, DateTime? creationTime, DateTime? lastExecutionTime, DateTime? finishTime, TimeSpan? timeInServer, int? errorCode, int corruptionsDetected, int corruptionsFixed, StoreIntegrityCheckJob.Corruption[] corruptions) : this()
		{
			this.databaseId = databaseId;
			this.mailboxGuid = mailboxGuid;
			this.requestGuid = requestGuid;
			this.jobGuid = jobGuid;
			this.flags = flags;
			this.tasks = taskIds;
			this.state = state;
			this.source = source;
			this.priority = priority;
			this.progress = progress;
			this.creationTime = creationTime;
			this.lastExecutionTime = lastExecutionTime;
			this.finishTime = finishTime;
			this.timeInServer = timeInServer;
			this.errorCode = errorCode;
			this.corruptions = corruptions;
			this.corruptionsDetected = corruptionsDetected;
			this.corruptionsFixed = corruptionsFixed;
		}

		// Token: 0x060085A8 RID: 34216 RVA: 0x00222DD0 File Offset: 0x00220FD0
		internal StoreIntegrityCheckJob(DatabaseId databaseId, PropValue[] propValues) : base(new SimpleProviderPropertyBag())
		{
			this.databaseId = databaseId;
			byte[] buffer = null;
			foreach (PropValue propValue in propValues)
			{
				if (!propValue.IsError() && !propValue.IsNull())
				{
					PropTag propTag = propValue.PropTag;
					if (propTag <= PropTag.IsIntegJobLastExecutionTime)
					{
						if (propTag <= PropTag.IsIntegJobTask)
						{
							if (propTag <= PropTag.IsIntegJobGuid)
							{
								if (propTag != PropTag.IsIntegJobMailboxGuid)
								{
									if (propTag == PropTag.IsIntegJobGuid)
									{
										this.jobGuid = propValue.GetGuid();
									}
								}
								else
								{
									this.mailboxGuid = propValue.GetGuid();
								}
							}
							else if (propTag != PropTag.IsIntegJobFlags)
							{
								if (propTag == PropTag.IsIntegJobTask)
								{
									this.tasks = new MailboxCorruptionType[]
									{
										(MailboxCorruptionType)propValue.GetInt()
									};
								}
							}
							else
							{
								this.flags = (JobFlags)propValue.GetInt();
							}
						}
						else if (propTag <= PropTag.IsIntegJobCreationTime)
						{
							if (propTag != PropTag.IsIntegJobState)
							{
								if (propTag == PropTag.IsIntegJobCreationTime)
								{
									this.creationTime = new DateTime?(propValue.GetDateTime().ToLocalTime());
								}
							}
							else
							{
								this.state = (JobState)propValue.GetShort();
							}
						}
						else if (propTag != PropTag.IsIntegJobFinishTime)
						{
							if (propTag == PropTag.IsIntegJobLastExecutionTime)
							{
								this.lastExecutionTime = new DateTime?(propValue.GetDateTime().ToLocalTime());
							}
						}
						else
						{
							this.finishTime = new DateTime?(propValue.GetDateTime().ToLocalTime());
						}
					}
					else if (propTag <= PropTag.IsIntegJobProgress)
					{
						if (propTag <= PropTag.IsIntegJobCorruptionsFixed)
						{
							if (propTag != PropTag.IsIntegJobCorruptionsDetected)
							{
								if (propTag == PropTag.IsIntegJobCorruptionsFixed)
								{
									this.corruptionsFixed = propValue.GetInt();
								}
							}
							else
							{
								this.corruptionsDetected = propValue.GetInt();
							}
						}
						else if (propTag != PropTag.IsIntegJobRequestGuid)
						{
							if (propTag == PropTag.IsIntegJobProgress)
							{
								this.progress = propValue.GetShort();
							}
						}
						else
						{
							this.requestGuid = propValue.GetGuid();
						}
					}
					else if (propTag <= PropTag.IsIntegJobSource)
					{
						if (propTag != PropTag.IsIntegJobCorruptions)
						{
							if (propTag == PropTag.IsIntegJobSource)
							{
								this.source = (JobSource)propValue.GetShort();
							}
						}
						else
						{
							buffer = propValue.GetBytes();
						}
					}
					else if (propTag != PropTag.IsIntegJobPriority)
					{
						if (propTag != PropTag.IsIntegJobTimeInServer)
						{
							if (propTag == PropTag.RtfSyncTrailingCount)
							{
								this.errorCode = new int?(propValue.GetInt());
								if (this.errorCode != null && this.errorCode == 0)
								{
									this.errorCode = null;
								}
							}
						}
						else
						{
							this.timeInServer = new TimeSpan?(TimeSpan.FromMilliseconds(propValue.GetDouble()));
						}
					}
					else
					{
						this.priority = (JobPriority)propValue.GetShort();
					}
				}
			}
			if (this.mailboxGuid != Guid.Empty)
			{
				this.corruptions = this.DeserializeCorruptions(this.mailboxGuid, buffer);
			}
		}

		// Token: 0x060085A9 RID: 34217 RVA: 0x00223128 File Offset: 0x00221328
		internal static StoreIntegrityCheckJob Aggregate(IList<StoreIntegrityCheckJob> jobs)
		{
			if (jobs == null || jobs.Count == 0)
			{
				return null;
			}
			if (jobs.Count == 1)
			{
				return jobs[0];
			}
			HashSet<MailboxCorruptionType> hashSet = new HashSet<MailboxCorruptionType>();
			TimeSpan timeSpan = default(TimeSpan);
			List<StoreIntegrityCheckJob.Corruption> list = new List<StoreIntegrityCheckJob.Corruption>();
			DatabaseId databaseId = jobs[0].databaseId;
			Guid guid = jobs[0].requestGuid;
			Guid empty = jobs[0].mailboxGuid;
			Guid guid2 = jobs[0].jobGuid;
			JobFlags jobFlags = JobFlags.None;
			JobSource jobSource = jobs[0].source;
			JobPriority jobPriority = jobs[0].priority;
			JobState jobState = jobs[0].state;
			DateTime? dateTime = null;
			DateTime? dateTime2 = null;
			int num = 0;
			int num2 = 0;
			DateTime? dateTime3 = jobs[0].finishTime;
			long num3 = 0L;
			bool flag = false;
			for (int i = 0; i < jobs.Count; i++)
			{
				StoreIntegrityCheckJob storeIntegrityCheckJob = jobs[i];
				if (storeIntegrityCheckJob.mailboxGuid != empty)
				{
					empty = Guid.Empty;
				}
				if (!hashSet.Contains(storeIntegrityCheckJob.Tasks[0]))
				{
					hashSet.Add(storeIntegrityCheckJob.Tasks[0]);
				}
				if (dateTime == null)
				{
					dateTime = storeIntegrityCheckJob.creationTime;
				}
				else if (dateTime != null && storeIntegrityCheckJob.creationTime != null && dateTime.Value > storeIntegrityCheckJob.creationTime.Value)
				{
					dateTime = storeIntegrityCheckJob.creationTime;
				}
				if (dateTime2 == null)
				{
					dateTime2 = storeIntegrityCheckJob.lastExecutionTime;
				}
				if (dateTime2 != null && storeIntegrityCheckJob.lastExecutionTime != null && dateTime2 < storeIntegrityCheckJob.lastExecutionTime.Value)
				{
					dateTime2 = storeIntegrityCheckJob.lastExecutionTime;
				}
				if (dateTime3 != null && storeIntegrityCheckJob.finishTime != null)
				{
					if (dateTime3.Value < storeIntegrityCheckJob.finishTime.Value)
					{
						dateTime3 = storeIntegrityCheckJob.finishTime;
					}
				}
				else
				{
					dateTime3 = null;
				}
				if (storeIntegrityCheckJob.TimeInServer != null)
				{
					timeSpan += storeIntegrityCheckJob.TimeInServer.Value;
				}
				jobFlags |= storeIntegrityCheckJob.flags;
				if (storeIntegrityCheckJob.state != jobState)
				{
					if ((storeIntegrityCheckJob.state == JobState.Succeeded || storeIntegrityCheckJob.state == JobState.Failed) && (jobState == JobState.Succeeded || jobState == JobState.Failed))
					{
						jobState = JobState.Failed;
					}
					else
					{
						jobState = JobState.Running;
					}
				}
				num3 += (long)storeIntegrityCheckJob.progress;
				if (storeIntegrityCheckJob.corruptions != null)
				{
					list.AddRange(storeIntegrityCheckJob.corruptions);
				}
				num += storeIntegrityCheckJob.CorruptionsDetected;
				num2 += storeIntegrityCheckJob.CorruptionsFixed;
				if (storeIntegrityCheckJob.errorCode != null && storeIntegrityCheckJob.errorCode.Value != 0)
				{
					flag = true;
				}
			}
			num3 /= (long)jobs.Count;
			int? num4 = null;
			if (flag)
			{
				num4 = new int?(-2147467259);
			}
			return new StoreIntegrityCheckJob(databaseId, empty, guid, Guid.Empty, jobFlags, hashSet.ToArray<MailboxCorruptionType>(), jobState, jobSource, jobPriority, (short)num3, dateTime, dateTime2, dateTime3, new TimeSpan?(timeSpan), num4, num, num2, list.ToArray());
		}

		// Token: 0x060085AA RID: 34218 RVA: 0x00223480 File Offset: 0x00221680
		private StoreIntegrityCheckJob.Corruption[] DeserializeCorruptions(Guid mailboxGuid, byte[] buffer)
		{
			int num = 58;
			if (buffer == null)
			{
				return null;
			}
			int num2 = buffer.Length / num;
			StoreIntegrityCheckJob.Corruption[] array = new StoreIntegrityCheckJob.Corruption[num2];
			for (int i = 0; i < num2; i++)
			{
				int num3 = num * i;
				array[i] = default(StoreIntegrityCheckJob.Corruption);
				array[i].MailboxGuid = mailboxGuid;
				array[i].CorruptionType = (CorruptionType)BitConverter.ToInt32(buffer, num3);
				num3 += 4;
				array[i].FolderId = BitConverter.ToString(buffer, num3, 26).Replace("-", string.Empty);
				num3 += 26;
				array[i].MessageId = BitConverter.ToString(buffer, num3, 26).Replace("-", string.Empty);
				num3 += 26;
				array[i].IsFixed = (BitConverter.ToInt16(buffer, num3) != 0);
			}
			return array;
		}

		// Token: 0x040040CB RID: 16587
		private static ObjectSchema schema = ObjectSchema.GetInstance<StoreIntegrityCheckRequestSchema>();

		// Token: 0x040040CC RID: 16588
		private readonly DatabaseId databaseId;

		// Token: 0x040040CD RID: 16589
		private readonly int corruptionsDetected;

		// Token: 0x040040CE RID: 16590
		private readonly int corruptionsFixed;

		// Token: 0x040040CF RID: 16591
		private readonly Guid requestGuid;

		// Token: 0x040040D0 RID: 16592
		private readonly Guid mailboxGuid;

		// Token: 0x040040D1 RID: 16593
		private readonly Guid jobGuid;

		// Token: 0x040040D2 RID: 16594
		private JobFlags flags;

		// Token: 0x040040D3 RID: 16595
		private readonly MailboxCorruptionType[] tasks;

		// Token: 0x040040D4 RID: 16596
		private readonly JobState state;

		// Token: 0x040040D5 RID: 16597
		private readonly JobSource source;

		// Token: 0x040040D6 RID: 16598
		private readonly JobPriority priority;

		// Token: 0x040040D7 RID: 16599
		private readonly short progress;

		// Token: 0x040040D8 RID: 16600
		private readonly DateTime? creationTime;

		// Token: 0x040040D9 RID: 16601
		private readonly DateTime? finishTime;

		// Token: 0x040040DA RID: 16602
		private readonly DateTime? lastExecutionTime;

		// Token: 0x040040DB RID: 16603
		private readonly TimeSpan? timeInServer;

		// Token: 0x040040DC RID: 16604
		private readonly int? errorCode;

		// Token: 0x040040DD RID: 16605
		private StoreIntegrityCheckJob.Corruption[] corruptions;

		// Token: 0x02000DA0 RID: 3488
		[Serializable]
		public struct Corruption
		{
			// Token: 0x1700299A RID: 10650
			// (get) Token: 0x060085AC RID: 34220 RVA: 0x0022356C File Offset: 0x0022176C
			// (set) Token: 0x060085AD RID: 34221 RVA: 0x00223574 File Offset: 0x00221774
			public Guid MailboxGuid { get; internal set; }

			// Token: 0x1700299B RID: 10651
			// (get) Token: 0x060085AE RID: 34222 RVA: 0x0022357D File Offset: 0x0022177D
			// (set) Token: 0x060085AF RID: 34223 RVA: 0x00223585 File Offset: 0x00221785
			public CorruptionType CorruptionType { get; internal set; }

			// Token: 0x1700299C RID: 10652
			// (get) Token: 0x060085B0 RID: 34224 RVA: 0x0022358E File Offset: 0x0022178E
			// (set) Token: 0x060085B1 RID: 34225 RVA: 0x00223596 File Offset: 0x00221796
			public string FolderId { get; internal set; }

			// Token: 0x1700299D RID: 10653
			// (get) Token: 0x060085B2 RID: 34226 RVA: 0x0022359F File Offset: 0x0022179F
			// (set) Token: 0x060085B3 RID: 34227 RVA: 0x002235A7 File Offset: 0x002217A7
			public string MessageId { get; internal set; }

			// Token: 0x1700299E RID: 10654
			// (get) Token: 0x060085B4 RID: 34228 RVA: 0x002235B0 File Offset: 0x002217B0
			// (set) Token: 0x060085B5 RID: 34229 RVA: 0x002235B8 File Offset: 0x002217B8
			public bool IsFixed { get; internal set; }

			// Token: 0x060085B6 RID: 34230 RVA: 0x002235C1 File Offset: 0x002217C1
			public override string ToString()
			{
				return Strings.ISIntegCorruptionFormat(this.CorruptionType.ToString(), this.IsFixed);
			}
		}
	}
}
