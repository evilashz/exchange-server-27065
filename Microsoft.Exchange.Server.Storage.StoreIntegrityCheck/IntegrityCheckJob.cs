using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreIntegrityCheck;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x0200001F RID: 31
	public class IntegrityCheckJob : IIntegrityCheckJob, IJobStateTracker, IJobProgressTracker
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00005720 File Offset: 0x00003920
		public IntegrityCheckJob(Guid jobGuid, Guid requestGuid, int mailboxNumber, Guid mailboxGuid, bool detectOnly, TaskId taskId, DateTime creationTime, JobSource jobSource, JobPriority jobPriority)
		{
			this.jobGuid = jobGuid;
			this.requestGuid = requestGuid;
			this.mailboxNumber = mailboxNumber;
			this.mailboxGuid = mailboxGuid;
			this.taskId = taskId;
			this.detectOnly = detectOnly;
			this.creationTime = creationTime;
			this.jobSource = jobSource;
			this.jobPriority = jobPriority;
			this.jobState = 0L;
			this.progressInfo = new ProgressInfo();
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000578B File Offset: 0x0000398B
		[Queryable]
		public Guid JobGuid
		{
			get
			{
				return this.jobGuid;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00005793 File Offset: 0x00003993
		[Queryable]
		public Guid RequestGuid
		{
			get
			{
				return this.requestGuid;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000579B File Offset: 0x0000399B
		[Queryable]
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000057A3 File Offset: 0x000039A3
		[Queryable]
		public int MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000057AB File Offset: 0x000039AB
		[Queryable]
		public TaskId TaskId
		{
			get
			{
				return this.taskId;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000057B3 File Offset: 0x000039B3
		[Queryable]
		public bool DetectOnly
		{
			get
			{
				return this.detectOnly;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000057BB File Offset: 0x000039BB
		[Queryable]
		public DateTime CreationTime
		{
			get
			{
				return this.creationTime;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000057C3 File Offset: 0x000039C3
		[Queryable]
		public JobSource Source
		{
			get
			{
				return this.jobSource;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000057CB File Offset: 0x000039CB
		[Queryable]
		public JobPriority Priority
		{
			get
			{
				return this.jobPriority;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000057D3 File Offset: 0x000039D3
		[Queryable]
		public JobState State
		{
			get
			{
				return (JobState)Interlocked.Read(ref this.jobState);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000057E1 File Offset: 0x000039E1
		[Queryable]
		public short Progress
		{
			get
			{
				return this.progressInfo.Progress;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000057EE File Offset: 0x000039EE
		public ProgressInfo CurrentProgress
		{
			get
			{
				return Interlocked.CompareExchange<ProgressInfo>(ref this.progressInfo, null, null);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000057FD File Offset: 0x000039FD
		[Queryable]
		public TimeSpan TimeInServer
		{
			get
			{
				return this.progressInfo.TimeInServer;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000580A File Offset: 0x00003A0A
		[Queryable]
		public DateTime? CompletedTime
		{
			get
			{
				return this.progressInfo.CompletedTime;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00005817 File Offset: 0x00003A17
		[Queryable]
		public DateTime? LastExecutionTime
		{
			get
			{
				return this.progressInfo.LastExecutionTime;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00005824 File Offset: 0x00003A24
		[Queryable]
		public int CorruptionsDetected
		{
			get
			{
				return this.progressInfo.CorruptionsDetected;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00005831 File Offset: 0x00003A31
		[Queryable]
		public int CorruptionsFixed
		{
			get
			{
				return this.progressInfo.CorruptionsFixed;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000583E File Offset: 0x00003A3E
		[Queryable]
		public ErrorCode Error
		{
			get
			{
				return this.progressInfo.Error;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000584B File Offset: 0x00003A4B
		void IJobProgressTracker.Report(ProgressInfo newProgressInfo)
		{
			Interlocked.Exchange<ProgressInfo>(ref this.progressInfo, newProgressInfo);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000585A File Offset: 0x00003A5A
		void IJobStateTracker.MoveToState(JobState state)
		{
			Interlocked.Exchange(ref this.jobState, (long)state);
			if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.OnlineIsintegTracer.TraceDebug<Guid, JobState>(0L, "Job {0} entered state {1}", this.JobGuid, state);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005890 File Offset: 0x00003A90
		public Properties GetProperties(StorePropTag[] propTags)
		{
			ProgressInfo currentProgress = this.CurrentProgress;
			Properties result = new Properties(propTags.Length);
			int i = 0;
			while (i < propTags.Length)
			{
				StorePropTag storePropTag = propTags[i];
				uint propTag = storePropTag.PropTag;
				if (propTag <= 268959747U)
				{
					if (propTag <= 268632067U)
					{
						if (propTag <= 268501064U)
						{
							if (propTag != 268435528U)
							{
								if (propTag != 268501064U)
								{
									goto IL_398;
								}
								result.Add(storePropTag, this.JobGuid);
							}
							else
							{
								result.Add(storePropTag, this.MailboxGuid);
							}
						}
						else if (propTag != 268566531U)
						{
							if (propTag != 268632067U)
							{
								goto IL_398;
							}
							result.Add(storePropTag, (int)this.TaskId);
						}
						else
						{
							result.Add(storePropTag, this.detectOnly ? 1 : 0);
						}
					}
					else if (propTag <= 268763200U)
					{
						if (propTag != 268697602U)
						{
							if (propTag != 268763200U)
							{
								goto IL_398;
							}
							result.Add(storePropTag, this.CreationTime);
						}
						else
						{
							result.Add(storePropTag, (short)this.State);
						}
					}
					else if (propTag != 268828736U)
					{
						if (propTag != 268894272U)
						{
							if (propTag != 268959747U)
							{
								goto IL_398;
							}
							result.Add(storePropTag, currentProgress.CorruptionsDetected);
						}
						else if (currentProgress.LastExecutionTime != null)
						{
							result.Add(storePropTag, currentProgress.LastExecutionTime);
						}
						else
						{
							result.Add(storePropTag.ConvertToError(), LegacyHelper.BoxedErrorCodeNotFound);
						}
					}
					else if (currentProgress.CompletedTime != null)
					{
						result.Add(storePropTag, currentProgress.CompletedTime);
					}
					else
					{
						result.Add(storePropTag.ConvertToError(), LegacyHelper.BoxedErrorCodeNotFound);
					}
				}
				else if (propTag <= 269222146U)
				{
					if (propTag <= 269090888U)
					{
						if (propTag != 269025283U)
						{
							if (propTag != 269090888U)
							{
								goto IL_398;
							}
							result.Add(storePropTag, this.RequestGuid);
						}
						else
						{
							result.Add(storePropTag, currentProgress.CorruptionsFixed);
						}
					}
					else if (propTag != 269156354U)
					{
						if (propTag != 269222146U)
						{
							goto IL_398;
						}
						if (currentProgress.Corruptions != null)
						{
							result.Add(storePropTag, IntegrityCheckJob.SerializedCorruptions(currentProgress.Corruptions));
						}
						else
						{
							result.Add(storePropTag.ConvertToError(), LegacyHelper.BoxedErrorCodeNotFound);
						}
					}
					else
					{
						result.Add(storePropTag, currentProgress.Progress);
					}
				}
				else if (propTag <= 269352962U)
				{
					if (propTag != 269287426U)
					{
						if (propTag != 269352962U)
						{
							goto IL_398;
						}
						result.Add(storePropTag, (short)this.Priority);
					}
					else
					{
						result.Add(storePropTag, (short)this.Source);
					}
				}
				else if (propTag != 269418501U)
				{
					if (propTag != 269484035U)
					{
						if (propTag != 269549571U)
						{
							goto IL_398;
						}
						result.Add(storePropTag, (int)currentProgress.Error);
					}
					else
					{
						result.Add(storePropTag, this.MailboxNumber);
					}
				}
				else
				{
					result.Add(storePropTag, currentProgress.TimeInServer.TotalMilliseconds);
				}
				IL_3CA:
				i++;
				continue;
				IL_398:
				result.Add(storePropTag.ConvertToError(), LegacyHelper.BoxedErrorCodeNotFound);
				if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.OnlineIsintegTracer.TraceError<StorePropTag>(0L, "Unrecognized property {0}", storePropTag);
					goto IL_3CA;
				}
				goto IL_3CA;
			}
			return result;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005C78 File Offset: 0x00003E78
		internal static byte[] SerializedCorruptions(IList<Corruption> corruptions)
		{
			if (corruptions == null)
			{
				return null;
			}
			int num = Math.Min(corruptions.Count, 1129) * 58;
			byte[] array = new byte[num];
			int num2 = 0;
			foreach (Corruption corruption in corruptions)
			{
				ExchangeId exchangeId = corruption.FolderId ?? ExchangeId.Zero;
				ExchangeId exchangeId2 = corruption.MessageId ?? ExchangeId.Zero;
				num2 += ParseSerialize.SerializeInt32((int)corruption.CorruptionType, array, num2);
				num2 += ExchangeIdHelpers.To26ByteArray(exchangeId.Replid, exchangeId.Guid, exchangeId.Counter, array, num2);
				num2 += ExchangeIdHelpers.To26ByteArray(exchangeId2.Replid, exchangeId2.Guid, exchangeId2.Counter, array, num2);
				num2 += ParseSerialize.SerializeInt16(corruption.IsFixed ? 1 : 0, array, num2);
				if (num2 >= num)
				{
					break;
				}
			}
			return array;
		}

		// Token: 0x0400006C RID: 108
		private readonly Guid jobGuid;

		// Token: 0x0400006D RID: 109
		private readonly Guid requestGuid;

		// Token: 0x0400006E RID: 110
		private readonly Guid mailboxGuid;

		// Token: 0x0400006F RID: 111
		private readonly int mailboxNumber;

		// Token: 0x04000070 RID: 112
		private readonly TaskId taskId;

		// Token: 0x04000071 RID: 113
		private readonly bool detectOnly;

		// Token: 0x04000072 RID: 114
		private readonly DateTime creationTime;

		// Token: 0x04000073 RID: 115
		private readonly JobSource jobSource;

		// Token: 0x04000074 RID: 116
		private readonly JobPriority jobPriority;

		// Token: 0x04000075 RID: 117
		private long jobState;

		// Token: 0x04000076 RID: 118
		private ProgressInfo progressInfo;
	}
}
