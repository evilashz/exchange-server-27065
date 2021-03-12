using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x0200055A RID: 1370
	[EventSource(Name = "System.Threading.Tasks.TplEventSource", Guid = "2e5dba47-a3d2-4d16-8ee0-6671ffdcd7b5", LocalizationResources = "mscorlib")]
	internal sealed class TplEtwProvider : EventSource
	{
		// Token: 0x0600415C RID: 16732 RVA: 0x000F2FD8 File Offset: 0x000F11D8
		protected override void OnEventCommand(EventCommandEventArgs command)
		{
			if (command.Command == EventCommand.Enable)
			{
				AsyncCausalityTracer.EnableToETW(true);
			}
			else if (command.Command == EventCommand.Disable)
			{
				AsyncCausalityTracer.EnableToETW(false);
			}
			if (base.IsEnabled(EventLevel.Informational, (EventKeywords)128L))
			{
				ActivityTracker.Instance.Enable();
			}
			else
			{
				this.TasksSetActivityIds = base.IsEnabled(EventLevel.Informational, (EventKeywords)65536L);
			}
			this.Debug = base.IsEnabled(EventLevel.Informational, (EventKeywords)131072L);
			this.DebugActivityId = base.IsEnabled(EventLevel.Informational, (EventKeywords)262144L);
		}

		// Token: 0x0600415D RID: 16733 RVA: 0x000F305B File Offset: 0x000F125B
		private TplEtwProvider()
		{
		}

		// Token: 0x0600415E RID: 16734 RVA: 0x000F3064 File Offset: 0x000F1264
		[SecuritySafeCritical]
		[Event(1, Level = EventLevel.Informational, ActivityOptions = EventActivityOptions.Recursive, Task = (EventTask)1, Opcode = EventOpcode.Start)]
		public unsafe void ParallelLoopBegin(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID, TplEtwProvider.ForkJoinOperationType OperationType, long InclusiveFrom, long ExclusiveTo)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)4L))
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)6) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr->Size = 4;
				ptr->DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID));
				ptr[1].Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&OriginatingTaskID));
				ptr[2].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&ForkJoinContextID));
				ptr[3].Size = 4;
				ptr[3].DataPointer = (IntPtr)((void*)(&OperationType));
				ptr[4].Size = 8;
				ptr[4].DataPointer = (IntPtr)((void*)(&InclusiveFrom));
				ptr[5].Size = 8;
				ptr[5].DataPointer = (IntPtr)((void*)(&ExclusiveTo));
				base.WriteEventCore(1, 6, ptr);
			}
		}

		// Token: 0x0600415F RID: 16735 RVA: 0x000F317C File Offset: 0x000F137C
		[SecuritySafeCritical]
		[Event(2, Level = EventLevel.Informational, Task = (EventTask)1, Opcode = EventOpcode.Stop)]
		public unsafe void ParallelLoopEnd(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID, long TotalIterations)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)4L))
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr->Size = 4;
				ptr->DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID));
				ptr[1].Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&OriginatingTaskID));
				ptr[2].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&ForkJoinContextID));
				ptr[3].Size = 8;
				ptr[3].DataPointer = (IntPtr)((void*)(&TotalIterations));
				base.WriteEventCore(2, 4, ptr);
			}
		}

		// Token: 0x06004160 RID: 16736 RVA: 0x000F3244 File Offset: 0x000F1444
		[SecuritySafeCritical]
		[Event(3, Level = EventLevel.Informational, ActivityOptions = EventActivityOptions.Recursive, Task = (EventTask)2, Opcode = EventOpcode.Start)]
		public unsafe void ParallelInvokeBegin(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID, TplEtwProvider.ForkJoinOperationType OperationType, int ActionCount)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)4L))
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)5) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr->Size = 4;
				ptr->DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID));
				ptr[1].Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&OriginatingTaskID));
				ptr[2].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&ForkJoinContextID));
				ptr[3].Size = 4;
				ptr[3].DataPointer = (IntPtr)((void*)(&OperationType));
				ptr[4].Size = 4;
				ptr[4].DataPointer = (IntPtr)((void*)(&ActionCount));
				base.WriteEventCore(3, 5, ptr);
			}
		}

		// Token: 0x06004161 RID: 16737 RVA: 0x000F3332 File Offset: 0x000F1532
		[Event(4, Level = EventLevel.Informational, Task = (EventTask)2, Opcode = EventOpcode.Stop)]
		public void ParallelInvokeEnd(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)4L))
			{
				base.WriteEvent(4, OriginatingTaskSchedulerID, OriginatingTaskID, ForkJoinContextID);
			}
		}

		// Token: 0x06004162 RID: 16738 RVA: 0x000F3351 File Offset: 0x000F1551
		[Event(5, Level = EventLevel.Verbose, ActivityOptions = EventActivityOptions.Recursive, Task = (EventTask)5, Opcode = EventOpcode.Start)]
		public void ParallelFork(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Verbose, (EventKeywords)4L))
			{
				base.WriteEvent(5, OriginatingTaskSchedulerID, OriginatingTaskID, ForkJoinContextID);
			}
		}

		// Token: 0x06004163 RID: 16739 RVA: 0x000F3370 File Offset: 0x000F1570
		[Event(6, Level = EventLevel.Verbose, Task = (EventTask)5, Opcode = EventOpcode.Stop)]
		public void ParallelJoin(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Verbose, (EventKeywords)4L))
			{
				base.WriteEvent(6, OriginatingTaskSchedulerID, OriginatingTaskID, ForkJoinContextID);
			}
		}

		// Token: 0x06004164 RID: 16740 RVA: 0x000F3390 File Offset: 0x000F1590
		[SecuritySafeCritical]
		[Event(7, Task = (EventTask)6, Version = 1, Opcode = EventOpcode.Send, Level = EventLevel.Informational, Keywords = (EventKeywords)3L)]
		public unsafe void TaskScheduled(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID, int CreatingTaskID, int TaskCreationOptions, int appDomain)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)3L))
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)5) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr->Size = 4;
				ptr->DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID));
				ptr[1].Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&OriginatingTaskID));
				ptr[2].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&TaskID));
				ptr[3].Size = 4;
				ptr[3].DataPointer = (IntPtr)((void*)(&CreatingTaskID));
				ptr[4].Size = 4;
				ptr[4].DataPointer = (IntPtr)((void*)(&TaskCreationOptions));
				if (this.TasksSetActivityIds)
				{
					Guid guid = TplEtwProvider.CreateGuidForTaskID(TaskID);
					base.WriteEventWithRelatedActivityIdCore(7, &guid, 5, ptr);
					return;
				}
				base.WriteEventCore(7, 5, ptr);
			}
		}

		// Token: 0x06004165 RID: 16741 RVA: 0x000F349A File Offset: 0x000F169A
		[Event(8, Level = EventLevel.Informational, Keywords = (EventKeywords)2L)]
		public void TaskStarted(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID)
		{
			if (base.IsEnabled(EventLevel.Informational, (EventKeywords)2L))
			{
				base.WriteEvent(8, OriginatingTaskSchedulerID, OriginatingTaskID, TaskID);
			}
		}

		// Token: 0x06004166 RID: 16742 RVA: 0x000F34B4 File Offset: 0x000F16B4
		[SecuritySafeCritical]
		[Event(9, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)64L)]
		public unsafe void TaskCompleted(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID, bool IsExceptional)
		{
			if (base.IsEnabled(EventLevel.Informational, (EventKeywords)2L))
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData))];
				int num = IsExceptional ? 1 : 0;
				ptr->Size = 4;
				ptr->DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID));
				ptr[1].Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&OriginatingTaskID));
				ptr[2].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&TaskID));
				ptr[3].Size = 4;
				ptr[3].DataPointer = (IntPtr)((void*)(&num));
				base.WriteEventCore(9, 4, ptr);
			}
		}

		// Token: 0x06004167 RID: 16743 RVA: 0x000F3578 File Offset: 0x000F1778
		[SecuritySafeCritical]
		[Event(10, Version = 3, Task = (EventTask)4, Opcode = EventOpcode.Send, Level = EventLevel.Informational, Keywords = (EventKeywords)3L)]
		public unsafe void TaskWaitBegin(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID, TplEtwProvider.TaskWaitBehavior Behavior, int ContinueWithTaskID, int appDomain)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)3L))
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)5) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr->Size = 4;
				ptr->DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID));
				ptr[1].Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&OriginatingTaskID));
				ptr[2].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&TaskID));
				ptr[3].Size = 4;
				ptr[3].DataPointer = (IntPtr)((void*)(&Behavior));
				ptr[4].Size = 4;
				ptr[4].DataPointer = (IntPtr)((void*)(&ContinueWithTaskID));
				if (this.TasksSetActivityIds)
				{
					Guid guid = TplEtwProvider.CreateGuidForTaskID(TaskID);
					base.WriteEventWithRelatedActivityIdCore(10, &guid, 5, ptr);
					return;
				}
				base.WriteEventCore(10, 5, ptr);
			}
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x000F3684 File Offset: 0x000F1884
		[Event(11, Level = EventLevel.Verbose, Keywords = (EventKeywords)2L)]
		public void TaskWaitEnd(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Verbose, (EventKeywords)2L))
			{
				base.WriteEvent(11, OriginatingTaskSchedulerID, OriginatingTaskID, TaskID);
			}
		}

		// Token: 0x06004169 RID: 16745 RVA: 0x000F36A4 File Offset: 0x000F18A4
		[Event(13, Level = EventLevel.Verbose, Keywords = (EventKeywords)64L)]
		public void TaskWaitContinuationComplete(int TaskID)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Verbose, (EventKeywords)2L))
			{
				base.WriteEvent(13, TaskID);
			}
		}

		// Token: 0x0600416A RID: 16746 RVA: 0x000F36C2 File Offset: 0x000F18C2
		[Event(19, Level = EventLevel.Verbose, Keywords = (EventKeywords)64L)]
		public void TaskWaitContinuationStarted(int TaskID)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Verbose, (EventKeywords)2L))
			{
				base.WriteEvent(19, TaskID);
			}
		}

		// Token: 0x0600416B RID: 16747 RVA: 0x000F36E0 File Offset: 0x000F18E0
		[SecuritySafeCritical]
		[Event(12, Task = (EventTask)7, Opcode = EventOpcode.Send, Level = EventLevel.Informational, Keywords = (EventKeywords)3L)]
		public unsafe void AwaitTaskContinuationScheduled(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ContinuwWithTaskId)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)3L))
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr->Size = 4;
				ptr->DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID));
				ptr[1].Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&OriginatingTaskID));
				ptr[2].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&ContinuwWithTaskId));
				if (this.TasksSetActivityIds)
				{
					Guid guid = TplEtwProvider.CreateGuidForTaskID(ContinuwWithTaskId);
					base.WriteEventWithRelatedActivityIdCore(12, &guid, 3, ptr);
					return;
				}
				base.WriteEventCore(12, 3, ptr);
			}
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x000F379C File Offset: 0x000F199C
		[SecuritySafeCritical]
		[Event(14, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)8L)]
		public unsafe void TraceOperationBegin(int TaskID, string OperationName, long RelatedContext)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)8L))
			{
				fixed (string text = OperationName)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2 = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData))];
					ptr2->Size = 4;
					ptr2->DataPointer = (IntPtr)((void*)(&TaskID));
					ptr2[1].Size = (OperationName.Length + 1) * 2;
					ptr2[1].DataPointer = (IntPtr)((void*)ptr);
					ptr2[2].Size = 8;
					ptr2[2].DataPointer = (IntPtr)((void*)(&RelatedContext));
					base.WriteEventCore(14, 3, ptr2);
				}
			}
		}

		// Token: 0x0600416D RID: 16749 RVA: 0x000F3852 File Offset: 0x000F1A52
		[SecuritySafeCritical]
		[Event(16, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)16L)]
		public void TraceOperationRelation(int TaskID, CausalityRelation Relation)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)16L))
			{
				base.WriteEvent(16, TaskID, (int)Relation);
			}
		}

		// Token: 0x0600416E RID: 16750 RVA: 0x000F3872 File Offset: 0x000F1A72
		[SecuritySafeCritical]
		[Event(15, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)8L)]
		public void TraceOperationEnd(int TaskID, AsyncCausalityStatus Status)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)8L))
			{
				base.WriteEvent(15, TaskID, (int)Status);
			}
		}

		// Token: 0x0600416F RID: 16751 RVA: 0x000F3891 File Offset: 0x000F1A91
		[SecuritySafeCritical]
		[Event(17, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)32L)]
		public void TraceSynchronousWorkBegin(int TaskID, CausalitySynchronousWork Work)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)32L))
			{
				base.WriteEvent(17, TaskID, (int)Work);
			}
		}

		// Token: 0x06004170 RID: 16752 RVA: 0x000F38B4 File Offset: 0x000F1AB4
		[SecuritySafeCritical]
		[Event(18, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)32L)]
		public unsafe void TraceSynchronousWorkEnd(CausalitySynchronousWork Work)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)32L))
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)1) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr->Size = 4;
				ptr->DataPointer = (IntPtr)((void*)(&Work));
				base.WriteEventCore(18, 1, ptr);
			}
		}

		// Token: 0x06004171 RID: 16753 RVA: 0x000F3900 File Offset: 0x000F1B00
		[NonEvent]
		[SecuritySafeCritical]
		public unsafe void RunningContinuation(int TaskID, object Object)
		{
			this.RunningContinuation(TaskID, (long)((ulong)(*(IntPtr*)((void*)JitHelpers.UnsafeCastToStackPointer<object>(ref Object)))));
		}

		// Token: 0x06004172 RID: 16754 RVA: 0x000F3917 File Offset: 0x000F1B17
		[Event(20, Keywords = (EventKeywords)131072L)]
		private void RunningContinuation(int TaskID, long Object)
		{
			if (this.Debug)
			{
				base.WriteEvent(20, (long)TaskID, Object);
			}
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x000F392C File Offset: 0x000F1B2C
		[NonEvent]
		[SecuritySafeCritical]
		public unsafe void RunningContinuationList(int TaskID, int Index, object Object)
		{
			this.RunningContinuationList(TaskID, Index, (long)((ulong)(*(IntPtr*)((void*)JitHelpers.UnsafeCastToStackPointer<object>(ref Object)))));
		}

		// Token: 0x06004174 RID: 16756 RVA: 0x000F3944 File Offset: 0x000F1B44
		[Event(21, Keywords = (EventKeywords)131072L)]
		public void RunningContinuationList(int TaskID, int Index, long Object)
		{
			if (this.Debug)
			{
				base.WriteEvent(21, (long)TaskID, (long)Index, Object);
			}
		}

		// Token: 0x06004175 RID: 16757 RVA: 0x000F395B File Offset: 0x000F1B5B
		[Event(22, Keywords = (EventKeywords)131072L)]
		public void DebugMessage(string Message)
		{
			base.WriteEvent(22, Message);
		}

		// Token: 0x06004176 RID: 16758 RVA: 0x000F3966 File Offset: 0x000F1B66
		[Event(23, Keywords = (EventKeywords)131072L)]
		public void DebugFacilityMessage(string Facility, string Message)
		{
			base.WriteEvent(23, Facility, Message);
		}

		// Token: 0x06004177 RID: 16759 RVA: 0x000F3972 File Offset: 0x000F1B72
		[Event(24, Keywords = (EventKeywords)131072L)]
		public void DebugFacilityMessage1(string Facility, string Message, string Value1)
		{
			base.WriteEvent(24, Facility, Message, Value1);
		}

		// Token: 0x06004178 RID: 16760 RVA: 0x000F397F File Offset: 0x000F1B7F
		[Event(25, Keywords = (EventKeywords)262144L)]
		public void SetActivityId(Guid NewId)
		{
			if (this.DebugActivityId)
			{
				base.WriteEvent(25, new object[]
				{
					NewId
				});
			}
		}

		// Token: 0x06004179 RID: 16761 RVA: 0x000F39A0 File Offset: 0x000F1BA0
		[Event(26, Keywords = (EventKeywords)131072L)]
		public void NewID(int TaskID)
		{
			if (this.Debug)
			{
				base.WriteEvent(26, TaskID);
			}
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x000F39B4 File Offset: 0x000F1BB4
		internal static Guid CreateGuidForTaskID(int taskID)
		{
			uint s_currentPid = EventSource.s_currentPid;
			int domainID = Thread.GetDomainID();
			return new Guid(taskID, (short)domainID, (short)(domainID >> 16), (byte)s_currentPid, (byte)(s_currentPid >> 8), (byte)(s_currentPid >> 16), (byte)(s_currentPid >> 24), byte.MaxValue, 220, 215, 181);
		}

		// Token: 0x04001AE5 RID: 6885
		internal bool TasksSetActivityIds;

		// Token: 0x04001AE6 RID: 6886
		internal bool Debug;

		// Token: 0x04001AE7 RID: 6887
		private bool DebugActivityId;

		// Token: 0x04001AE8 RID: 6888
		public static TplEtwProvider Log = new TplEtwProvider();

		// Token: 0x04001AE9 RID: 6889
		private const EventKeywords ALL_KEYWORDS = EventKeywords.All;

		// Token: 0x04001AEA RID: 6890
		private const int PARALLELLOOPBEGIN_ID = 1;

		// Token: 0x04001AEB RID: 6891
		private const int PARALLELLOOPEND_ID = 2;

		// Token: 0x04001AEC RID: 6892
		private const int PARALLELINVOKEBEGIN_ID = 3;

		// Token: 0x04001AED RID: 6893
		private const int PARALLELINVOKEEND_ID = 4;

		// Token: 0x04001AEE RID: 6894
		private const int PARALLELFORK_ID = 5;

		// Token: 0x04001AEF RID: 6895
		private const int PARALLELJOIN_ID = 6;

		// Token: 0x04001AF0 RID: 6896
		private const int TASKSCHEDULED_ID = 7;

		// Token: 0x04001AF1 RID: 6897
		private const int TASKSTARTED_ID = 8;

		// Token: 0x04001AF2 RID: 6898
		private const int TASKCOMPLETED_ID = 9;

		// Token: 0x04001AF3 RID: 6899
		private const int TASKWAITBEGIN_ID = 10;

		// Token: 0x04001AF4 RID: 6900
		private const int TASKWAITEND_ID = 11;

		// Token: 0x04001AF5 RID: 6901
		private const int AWAITTASKCONTINUATIONSCHEDULED_ID = 12;

		// Token: 0x04001AF6 RID: 6902
		private const int TASKWAITCONTINUATIONCOMPLETE_ID = 13;

		// Token: 0x04001AF7 RID: 6903
		private const int TASKWAITCONTINUATIONSTARTED_ID = 19;

		// Token: 0x04001AF8 RID: 6904
		private const int TRACEOPERATIONSTART_ID = 14;

		// Token: 0x04001AF9 RID: 6905
		private const int TRACEOPERATIONSTOP_ID = 15;

		// Token: 0x04001AFA RID: 6906
		private const int TRACEOPERATIONRELATION_ID = 16;

		// Token: 0x04001AFB RID: 6907
		private const int TRACESYNCHRONOUSWORKSTART_ID = 17;

		// Token: 0x04001AFC RID: 6908
		private const int TRACESYNCHRONOUSWORKSTOP_ID = 18;

		// Token: 0x02000BFA RID: 3066
		public enum ForkJoinOperationType
		{
			// Token: 0x04003620 RID: 13856
			ParallelInvoke = 1,
			// Token: 0x04003621 RID: 13857
			ParallelFor,
			// Token: 0x04003622 RID: 13858
			ParallelForEach
		}

		// Token: 0x02000BFB RID: 3067
		public enum TaskWaitBehavior
		{
			// Token: 0x04003624 RID: 13860
			Synchronous = 1,
			// Token: 0x04003625 RID: 13861
			Asynchronous
		}

		// Token: 0x02000BFC RID: 3068
		public class Tasks
		{
			// Token: 0x04003626 RID: 13862
			public const EventTask Loop = (EventTask)1;

			// Token: 0x04003627 RID: 13863
			public const EventTask Invoke = (EventTask)2;

			// Token: 0x04003628 RID: 13864
			public const EventTask TaskExecute = (EventTask)3;

			// Token: 0x04003629 RID: 13865
			public const EventTask TaskWait = (EventTask)4;

			// Token: 0x0400362A RID: 13866
			public const EventTask ForkJoin = (EventTask)5;

			// Token: 0x0400362B RID: 13867
			public const EventTask TaskScheduled = (EventTask)6;

			// Token: 0x0400362C RID: 13868
			public const EventTask AwaitTaskContinuationScheduled = (EventTask)7;

			// Token: 0x0400362D RID: 13869
			public const EventTask TraceOperation = (EventTask)8;

			// Token: 0x0400362E RID: 13870
			public const EventTask TraceSynchronousWork = (EventTask)9;
		}

		// Token: 0x02000BFD RID: 3069
		public class Keywords
		{
			// Token: 0x0400362F RID: 13871
			public const EventKeywords TaskTransfer = (EventKeywords)1L;

			// Token: 0x04003630 RID: 13872
			public const EventKeywords Tasks = (EventKeywords)2L;

			// Token: 0x04003631 RID: 13873
			public const EventKeywords Parallel = (EventKeywords)4L;

			// Token: 0x04003632 RID: 13874
			public const EventKeywords AsyncCausalityOperation = (EventKeywords)8L;

			// Token: 0x04003633 RID: 13875
			public const EventKeywords AsyncCausalityRelation = (EventKeywords)16L;

			// Token: 0x04003634 RID: 13876
			public const EventKeywords AsyncCausalitySynchronousWork = (EventKeywords)32L;

			// Token: 0x04003635 RID: 13877
			public const EventKeywords TaskStops = (EventKeywords)64L;

			// Token: 0x04003636 RID: 13878
			public const EventKeywords TasksFlowActivityIds = (EventKeywords)128L;

			// Token: 0x04003637 RID: 13879
			public const EventKeywords TasksSetActivityIds = (EventKeywords)65536L;

			// Token: 0x04003638 RID: 13880
			public const EventKeywords Debug = (EventKeywords)131072L;

			// Token: 0x04003639 RID: 13881
			public const EventKeywords DebugActivityId = (EventKeywords)262144L;
		}
	}
}
