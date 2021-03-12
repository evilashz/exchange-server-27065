using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x020000A3 RID: 163
	public sealed class ThreadManager : IStoreSimpleQueryTarget<ThreadManager.ThreadDiagnosticInfo>, IStoreQueryTargetBase<ThreadManager.ThreadDiagnosticInfo>, IStoreSimpleQueryTarget<ThreadManager.ProcessThreadInfo>, IStoreQueryTargetBase<ThreadManager.ProcessThreadInfo>
	{
		// Token: 0x060007D9 RID: 2009 RVA: 0x00015D71 File Offset: 0x00013F71
		private ThreadManager()
		{
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00015D8B File Offset: 0x00013F8B
		public static ThreadManager Instance
		{
			get
			{
				return ThreadManager.instance;
			}
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00015D92 File Offset: 0x00013F92
		internal static void Initialize()
		{
			ThreadManager.threadHangDetectionTimeout = Hookable<TimeSpan>.Create(false, ConfigurationSchema.ThreadHangDetectionTimeout.Value);
			ThreadManager.instance = new ThreadManager();
			ThreadManager.instance.StartScavengerTask();
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00015DBD File Offset: 0x00013FBD
		internal static void Terminate()
		{
			if (ThreadManager.instance != null)
			{
				ThreadManager.instance.StopScavengerTask();
			}
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00015DD0 File Offset: 0x00013FD0
		public static IDisposable SetThreadHangDetectionTimeoutTestHook(TimeSpan newTimeout)
		{
			return ThreadManager.threadHangDetectionTimeout.SetTestHook(newTimeout);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00015DE0 File Offset: 0x00013FE0
		public static ThreadManager.MethodFrame NewMethodFrame(string methodName)
		{
			ThreadManager.ThreadInfo threadInfo;
			return ThreadManager.NewMethodFrame(methodName, out threadInfo);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00015DF5 File Offset: 0x00013FF5
		public static ThreadManager.MethodFrame NewMethodFrame(string methodName, ThreadManager.TimeoutDefinition timeoutDefinition)
		{
			return new ThreadManager.MethodFrame(methodName, timeoutDefinition);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00015E00 File Offset: 0x00014000
		public static ThreadManager.MethodFrame NewMethodFrame(string methodName, out ThreadManager.ThreadInfo currentThreadInfo)
		{
			ThreadManager.MethodFrame result = new ThreadManager.MethodFrame(methodName);
			currentThreadInfo = result.CurrentThreadInfo;
			return result;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00015E20 File Offset: 0x00014020
		public static ThreadManager.MethodFrame NewMethodFrame(Delegate methodDelegate)
		{
			ThreadManager.ThreadInfo threadInfo;
			return ThreadManager.NewMethodFrame(methodDelegate, out threadInfo);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00015E35 File Offset: 0x00014035
		public static ThreadManager.MethodFrame NewMethodFrame(Delegate methodDelegate, out ThreadManager.ThreadInfo currentThreadInfo)
		{
			return ThreadManager.NewMethodFrame(ThreadManager.GetMethodNameFromDelegate(methodDelegate), out currentThreadInfo);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00015E44 File Offset: 0x00014044
		public static void MarkCurrentThreadAsLongRunning()
		{
			bool flag;
			ThreadManager.ThreadInfo orCreateCurrentThreadDiagnosticInfo = ThreadManager.Instance.GetOrCreateCurrentThreadDiagnosticInfo(out flag);
			Globals.AssertRetail(!flag, "This method should be called only when we already have ThreadInfo");
			orCreateCurrentThreadDiagnosticInfo.LongRunningThread = true;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00015E74 File Offset: 0x00014074
		public ThreadManager.ThreadInfo GetOrCreateCurrentThreadDiagnosticInfo(out bool created)
		{
			ThreadManager.ThreadInfo threadInfo = new ThreadManager.ThreadInfo();
			ThreadManager.ThreadInfo orAdd = this.threadList.GetOrAdd(Environment.CurrentManagedThreadId, threadInfo);
			created = (orAdd == threadInfo);
			return orAdd;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00015EA0 File Offset: 0x000140A0
		public void RemoveCurrentThreadDiagnosticInfo()
		{
			ThreadManager.ThreadInfo threadInfo;
			bool assertCondition = this.threadList.TryRemove(Environment.CurrentManagedThreadId, out threadInfo);
			Globals.AssertRetail(assertCondition, "Current ThreadInfo doesn't exist");
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00015ECE File Offset: 0x000140CE
		internal void ExecuteScavengerForTest()
		{
			this.ThreadListScavenger(null, null, () => true);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00015EF5 File Offset: 0x000140F5
		private static string GetMethodNameFromDelegate(Delegate delegateMethod)
		{
			return delegateMethod.GetMethodInfo().Name;
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00015F02 File Offset: 0x00014102
		private void StartScavengerTask()
		{
			if (this.threadListScavengerTask == null)
			{
				this.threadListScavengerTask = new RecurringTask<object>(new Task<object>.TaskCallback(this.ThreadListScavenger), null, ThreadManager.ThreadListScavengerInterval);
				this.threadListScavengerTask.Start();
			}
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00015F34 File Offset: 0x00014134
		private void StopScavengerTask()
		{
			if (this.threadListScavengerTask != null)
			{
				this.threadListScavengerTask.Stop();
				this.threadListScavengerTask.Dispose();
				this.threadListScavengerTask = null;
			}
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00015F5C File Offset: 0x0001415C
		private void ThreadListScavenger(TaskExecutionDiagnosticsProxy diagnosticsContext, object unused, Func<bool> shouldCallbackContinue)
		{
			if (!shouldCallbackContinue() || ErrorHelper.IsDebuggerAttached())
			{
				return;
			}
			DateTime utcNow = DateTime.UtcNow;
			foreach (KeyValuePair<int, ThreadManager.ThreadInfo> keyValuePair in this.threadList)
			{
				ThreadManager.ThreadInfo value = keyValuePair.Value;
				TimeSpan timeSpan = utcNow - value.StartUtcTime;
				if (!value.LongRunningThread)
				{
					if (value.TimeoutDefinition != null && timeSpan >= value.TimeoutDefinition.TimeoutValue)
					{
						value.TimeoutDefinition.TimeoutCrashAction(value);
						Globals.AssertRetail(false, "We should not reach this point");
					}
					if (timeSpan >= ThreadManager.threadHangDetectionTimeout.Value)
					{
						throw new InvalidOperationException(string.Format("Possible hang detected. Operation: {0}. Execution time: {1}. Client: {2}. MailboxGuid: {3}", new object[]
						{
							value.MethodName,
							timeSpan,
							value.Client,
							value.MailboxGuid
						}));
					}
				}
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x00016074 File Offset: 0x00014274
		string IStoreQueryTargetBase<ThreadManager.ThreadDiagnosticInfo>.Name
		{
			get
			{
				return "ThreadDiagnosticInfo";
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x0001607B File Offset: 0x0001427B
		Type[] IStoreQueryTargetBase<ThreadManager.ThreadDiagnosticInfo>.ParameterTypes
		{
			get
			{
				return Array<Type>.Empty;
			}
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0001626C File Offset: 0x0001446C
		IEnumerable<ThreadManager.ThreadDiagnosticInfo> IStoreSimpleQueryTarget<ThreadManager.ThreadDiagnosticInfo>.GetRows(object[] parameters)
		{
			foreach (ThreadManager.ThreadInfo threadInfo in this.threadList.Values)
			{
				Thread thread = threadInfo.Thread;
				Globals.AssertRetail(thread.IsAlive, "If this thread is not alive then how it is still in the collection?");
				threadInfo.Priority = thread.Priority;
				threadInfo.Status = thread.ThreadState;
				yield return threadInfo;
			}
			yield break;
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x00016289 File Offset: 0x00014489
		string IStoreQueryTargetBase<ThreadManager.ProcessThreadInfo>.Name
		{
			get
			{
				return "ProcessThreadInfo";
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x00016290 File Offset: 0x00014490
		Type[] IStoreQueryTargetBase<ThreadManager.ProcessThreadInfo>.ParameterTypes
		{
			get
			{
				return Array<Type>.Empty;
			}
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000164A4 File Offset: 0x000146A4
		IEnumerable<ThreadManager.ProcessThreadInfo> IStoreSimpleQueryTarget<ThreadManager.ProcessThreadInfo>.GetRows(object[] parameters)
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				ProcessThreadCollection threadCollection = currentProcess.Threads;
				foreach (object obj in threadCollection)
				{
					ProcessThread thread = (ProcessThread)obj;
					yield return new ThreadManager.ProcessThreadInfo(thread);
				}
			}
			yield break;
		}

		// Token: 0x040006F0 RID: 1776
		private const int ThreadListInitialCapacity = 50;

		// Token: 0x040006F1 RID: 1777
		private static ThreadManager instance;

		// Token: 0x040006F2 RID: 1778
		private static readonly TimeSpan ThreadListScavengerInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x040006F3 RID: 1779
		private static Hookable<TimeSpan> threadHangDetectionTimeout;

		// Token: 0x040006F4 RID: 1780
		private ConcurrentDictionary<int, ThreadManager.ThreadInfo> threadList = new ConcurrentDictionary<int, ThreadManager.ThreadInfo>(Environment.ProcessorCount, 50);

		// Token: 0x040006F5 RID: 1781
		private RecurringTask<object> threadListScavengerTask;

		// Token: 0x020000A4 RID: 164
		internal sealed class ProcessThreadInfo
		{
			// Token: 0x170001D6 RID: 470
			// (get) Token: 0x060007F3 RID: 2035 RVA: 0x000164D6 File Offset: 0x000146D6
			// (set) Token: 0x060007F4 RID: 2036 RVA: 0x000164DE File Offset: 0x000146DE
			public int NativeId { get; private set; }

			// Token: 0x170001D7 RID: 471
			// (get) Token: 0x060007F5 RID: 2037 RVA: 0x000164E7 File Offset: 0x000146E7
			// (set) Token: 0x060007F6 RID: 2038 RVA: 0x000164EF File Offset: 0x000146EF
			public int BasePriority { get; private set; }

			// Token: 0x170001D8 RID: 472
			// (get) Token: 0x060007F7 RID: 2039 RVA: 0x000164F8 File Offset: 0x000146F8
			// (set) Token: 0x060007F8 RID: 2040 RVA: 0x00016500 File Offset: 0x00014700
			public int CurrentPriority { get; private set; }

			// Token: 0x170001D9 RID: 473
			// (get) Token: 0x060007F9 RID: 2041 RVA: 0x00016509 File Offset: 0x00014709
			// (set) Token: 0x060007FA RID: 2042 RVA: 0x00016511 File Offset: 0x00014711
			public System.Diagnostics.ThreadState State { get; private set; }

			// Token: 0x170001DA RID: 474
			// (get) Token: 0x060007FB RID: 2043 RVA: 0x0001651A File Offset: 0x0001471A
			// (set) Token: 0x060007FC RID: 2044 RVA: 0x00016522 File Offset: 0x00014722
			public DateTime StartTime { get; private set; }

			// Token: 0x170001DB RID: 475
			// (get) Token: 0x060007FD RID: 2045 RVA: 0x0001652B File Offset: 0x0001472B
			// (set) Token: 0x060007FE RID: 2046 RVA: 0x00016533 File Offset: 0x00014733
			public TimeSpan TotalProcessorTime { get; private set; }

			// Token: 0x170001DC RID: 476
			// (get) Token: 0x060007FF RID: 2047 RVA: 0x0001653C File Offset: 0x0001473C
			// (set) Token: 0x06000800 RID: 2048 RVA: 0x00016544 File Offset: 0x00014744
			public TimeSpan UserProcessorTime { get; private set; }

			// Token: 0x06000801 RID: 2049 RVA: 0x00016550 File Offset: 0x00014750
			public ProcessThreadInfo(ProcessThread processThread)
			{
				try
				{
					this.NativeId = processThread.Id;
					this.BasePriority = processThread.BasePriority;
					this.CurrentPriority = processThread.CurrentPriority;
					this.StartTime = processThread.StartTime;
					this.State = processThread.ThreadState;
					this.TotalProcessorTime = processThread.TotalProcessorTime;
					this.UserProcessorTime = processThread.UserProcessorTime;
				}
				catch (Win32Exception exception)
				{
					NullExecutionDiagnostics.Instance.OnExceptionCatch(exception);
				}
			}
		}

		// Token: 0x020000A5 RID: 165
		public class ThreadDiagnosticInfo
		{
			// Token: 0x170001DD RID: 477
			// (get) Token: 0x06000802 RID: 2050 RVA: 0x000165D8 File Offset: 0x000147D8
			// (set) Token: 0x06000803 RID: 2051 RVA: 0x000165E0 File Offset: 0x000147E0
			[Queryable(Index = 0)]
			public int NativeId { get; internal set; }

			// Token: 0x170001DE RID: 478
			// (get) Token: 0x06000804 RID: 2052 RVA: 0x000165E9 File Offset: 0x000147E9
			// (set) Token: 0x06000805 RID: 2053 RVA: 0x000165F1 File Offset: 0x000147F1
			[Queryable]
			public int ThreadId { get; internal set; }

			// Token: 0x170001DF RID: 479
			// (get) Token: 0x06000806 RID: 2054 RVA: 0x000165FA File Offset: 0x000147FA
			// (set) Token: 0x06000807 RID: 2055 RVA: 0x00016602 File Offset: 0x00014802
			[Queryable]
			public ThreadPriority Priority { get; internal set; }

			// Token: 0x170001E0 RID: 480
			// (get) Token: 0x06000808 RID: 2056 RVA: 0x0001660B File Offset: 0x0001480B
			// (set) Token: 0x06000809 RID: 2057 RVA: 0x00016613 File Offset: 0x00014813
			[Queryable]
			public string MethodName { get; internal set; }

			// Token: 0x170001E1 RID: 481
			// (get) Token: 0x0600080A RID: 2058 RVA: 0x0001661C File Offset: 0x0001481C
			// (set) Token: 0x0600080B RID: 2059 RVA: 0x00016629 File Offset: 0x00014829
			[Queryable]
			public string Client
			{
				get
				{
					return this.clientInformation.Client;
				}
				internal set
				{
					this.clientInformation.Client = value;
				}
			}

			// Token: 0x170001E2 RID: 482
			// (get) Token: 0x0600080C RID: 2060 RVA: 0x00016637 File Offset: 0x00014837
			// (set) Token: 0x0600080D RID: 2061 RVA: 0x00016644 File Offset: 0x00014844
			[Queryable(Visibility = Visibility.Redacted)]
			public string User
			{
				get
				{
					return this.clientInformation.User;
				}
				internal set
				{
					this.clientInformation.User = value;
				}
			}

			// Token: 0x170001E3 RID: 483
			// (get) Token: 0x0600080E RID: 2062 RVA: 0x00016652 File Offset: 0x00014852
			// (set) Token: 0x0600080F RID: 2063 RVA: 0x0001665F File Offset: 0x0001485F
			[Queryable]
			public Guid UserGuid
			{
				get
				{
					return this.clientInformation.UserGuid;
				}
				internal set
				{
					this.clientInformation.UserGuid = value;
				}
			}

			// Token: 0x170001E4 RID: 484
			// (get) Token: 0x06000810 RID: 2064 RVA: 0x0001666D File Offset: 0x0001486D
			// (set) Token: 0x06000811 RID: 2065 RVA: 0x0001667A File Offset: 0x0001487A
			[Queryable(Visibility = Visibility.Redacted)]
			public string Mailbox
			{
				get
				{
					return this.clientInformation.Mailbox;
				}
				internal set
				{
					this.clientInformation.Mailbox = value;
				}
			}

			// Token: 0x170001E5 RID: 485
			// (get) Token: 0x06000812 RID: 2066 RVA: 0x00016688 File Offset: 0x00014888
			// (set) Token: 0x06000813 RID: 2067 RVA: 0x00016695 File Offset: 0x00014895
			[Queryable]
			public Guid MailboxGuid
			{
				get
				{
					return this.clientInformation.MailboxGuid;
				}
				internal set
				{
					this.clientInformation.MailboxGuid = value;
				}
			}

			// Token: 0x170001E6 RID: 486
			// (get) Token: 0x06000814 RID: 2068 RVA: 0x000166A3 File Offset: 0x000148A3
			// (set) Token: 0x06000815 RID: 2069 RVA: 0x000166AB File Offset: 0x000148AB
			[Queryable]
			public System.Threading.ThreadState Status { get; internal set; }

			// Token: 0x170001E7 RID: 487
			// (get) Token: 0x06000816 RID: 2070 RVA: 0x000166B4 File Offset: 0x000148B4
			// (set) Token: 0x06000817 RID: 2071 RVA: 0x000166BC File Offset: 0x000148BC
			[Queryable]
			public DateTime StartUtcTime { get; internal set; }

			// Token: 0x170001E8 RID: 488
			// (get) Token: 0x06000818 RID: 2072 RVA: 0x000166C5 File Offset: 0x000148C5
			[Queryable]
			public TimeSpan Duration
			{
				get
				{
					return DateTime.UtcNow - this.StartUtcTime;
				}
			}

			// Token: 0x06000819 RID: 2073 RVA: 0x000166D7 File Offset: 0x000148D7
			public ThreadDiagnosticInfo()
			{
				this.ResetClientInformation();
				this.NativeId = ThreadManager.ThreadDiagnosticInfo.GetCurrentWinThreadId();
				this.ThreadId = Thread.CurrentThread.ManagedThreadId;
				this.StartUtcTime = DateTime.UtcNow;
			}

			// Token: 0x170001E9 RID: 489
			// (get) Token: 0x0600081A RID: 2074 RVA: 0x0001670B File Offset: 0x0001490B
			// (set) Token: 0x0600081B RID: 2075 RVA: 0x00016713 File Offset: 0x00014913
			internal ThreadManager.ThreadDiagnosticInfo.ClientInformation ClientInfo
			{
				get
				{
					return this.clientInformation;
				}
				set
				{
					this.clientInformation = value;
				}
			}

			// Token: 0x0600081C RID: 2076 RVA: 0x0001671C File Offset: 0x0001491C
			internal void SetClientInformation(ThreadManager.ThreadDiagnosticInfo.ClientInformation clientInformation)
			{
				this.clientInformation = clientInformation;
			}

			// Token: 0x0600081D RID: 2077 RVA: 0x00016725 File Offset: 0x00014925
			internal void ResetClientInformation()
			{
				this.Client = string.Empty;
				this.User = string.Empty;
				this.UserGuid = Guid.Empty;
				this.Mailbox = string.Empty;
				this.MailboxGuid = Guid.Empty;
			}

			// Token: 0x0600081E RID: 2078
			[DllImport("Kernel32", EntryPoint = "GetCurrentThreadId", ExactSpelling = true)]
			public static extern int GetCurrentWinThreadId();

			// Token: 0x040006FE RID: 1790
			private ThreadManager.ThreadDiagnosticInfo.ClientInformation clientInformation;

			// Token: 0x020000A6 RID: 166
			internal struct ClientInformation
			{
				// Token: 0x170001EA RID: 490
				// (get) Token: 0x0600081F RID: 2079 RVA: 0x0001675E File Offset: 0x0001495E
				// (set) Token: 0x06000820 RID: 2080 RVA: 0x00016766 File Offset: 0x00014966
				public string Client { get; internal set; }

				// Token: 0x170001EB RID: 491
				// (get) Token: 0x06000821 RID: 2081 RVA: 0x0001676F File Offset: 0x0001496F
				// (set) Token: 0x06000822 RID: 2082 RVA: 0x00016777 File Offset: 0x00014977
				public string User { get; internal set; }

				// Token: 0x170001EC RID: 492
				// (get) Token: 0x06000823 RID: 2083 RVA: 0x00016780 File Offset: 0x00014980
				// (set) Token: 0x06000824 RID: 2084 RVA: 0x00016788 File Offset: 0x00014988
				public Guid UserGuid { get; internal set; }

				// Token: 0x170001ED RID: 493
				// (get) Token: 0x06000825 RID: 2085 RVA: 0x00016791 File Offset: 0x00014991
				// (set) Token: 0x06000826 RID: 2086 RVA: 0x00016799 File Offset: 0x00014999
				public string Mailbox { get; internal set; }

				// Token: 0x170001EE RID: 494
				// (get) Token: 0x06000827 RID: 2087 RVA: 0x000167A2 File Offset: 0x000149A2
				// (set) Token: 0x06000828 RID: 2088 RVA: 0x000167AA File Offset: 0x000149AA
				public Guid MailboxGuid { get; internal set; }
			}
		}

		// Token: 0x020000A7 RID: 167
		public sealed class ThreadInfo : ThreadManager.ThreadDiagnosticInfo
		{
			// Token: 0x170001EF RID: 495
			// (get) Token: 0x06000829 RID: 2089 RVA: 0x000167B3 File Offset: 0x000149B3
			public Thread Thread
			{
				get
				{
					return this.thread;
				}
			}

			// Token: 0x170001F0 RID: 496
			// (get) Token: 0x0600082A RID: 2090 RVA: 0x000167BB File Offset: 0x000149BB
			// (set) Token: 0x0600082B RID: 2091 RVA: 0x000167C3 File Offset: 0x000149C3
			public bool LongRunningThread { get; set; }

			// Token: 0x170001F1 RID: 497
			// (get) Token: 0x0600082C RID: 2092 RVA: 0x000167CC File Offset: 0x000149CC
			// (set) Token: 0x0600082D RID: 2093 RVA: 0x000167D4 File Offset: 0x000149D4
			public ThreadManager.TimeoutDefinition TimeoutDefinition
			{
				get
				{
					return this.timeoutDefinition;
				}
				set
				{
					this.timeoutDefinition = value;
				}
			}

			// Token: 0x0600082E RID: 2094 RVA: 0x000167DD File Offset: 0x000149DD
			public ThreadInfo()
			{
				this.thread = Thread.CurrentThread;
			}

			// Token: 0x0400070A RID: 1802
			private Thread thread;

			// Token: 0x0400070B RID: 1803
			private ThreadManager.TimeoutDefinition timeoutDefinition;
		}

		// Token: 0x020000A8 RID: 168
		public class TimeoutDefinition
		{
			// Token: 0x0600082F RID: 2095 RVA: 0x000167F0 File Offset: 0x000149F0
			internal TimeoutDefinition(TimeSpan timeoutValue, Action<ThreadManager.ThreadInfo> timeoutCrashAction)
			{
				this.timeoutValue = timeoutValue;
				this.timeoutCrashAction = timeoutCrashAction;
			}

			// Token: 0x170001F2 RID: 498
			// (get) Token: 0x06000830 RID: 2096 RVA: 0x00016806 File Offset: 0x00014A06
			internal TimeSpan TimeoutValue
			{
				get
				{
					return this.timeoutValue;
				}
			}

			// Token: 0x170001F3 RID: 499
			// (get) Token: 0x06000831 RID: 2097 RVA: 0x0001680E File Offset: 0x00014A0E
			internal Action<ThreadManager.ThreadInfo> TimeoutCrashAction
			{
				get
				{
					return this.timeoutCrashAction;
				}
			}

			// Token: 0x0400070D RID: 1805
			private readonly TimeSpan timeoutValue;

			// Token: 0x0400070E RID: 1806
			private readonly Action<ThreadManager.ThreadInfo> timeoutCrashAction;
		}

		// Token: 0x020000A9 RID: 169
		public struct MethodFrame : IDisposable
		{
			// Token: 0x06000832 RID: 2098 RVA: 0x00016816 File Offset: 0x00014A16
			internal MethodFrame(string methodName)
			{
				this = new ThreadManager.MethodFrame(methodName, null);
			}

			// Token: 0x06000833 RID: 2099 RVA: 0x00016820 File Offset: 0x00014A20
			internal MethodFrame(string methodName, ThreadManager.TimeoutDefinition timeoutDefinition)
			{
				this.threadInfo = ThreadManager.Instance.GetOrCreateCurrentThreadDiagnosticInfo(out this.topLevelMethod);
				this.originalMethodName = this.threadInfo.MethodName;
				this.originalClientInformation = this.threadInfo.ClientInfo;
				this.originalTimeoutDefinition = this.threadInfo.TimeoutDefinition;
				this.threadInfo.MethodName = methodName;
				this.threadInfo.TimeoutDefinition = timeoutDefinition;
			}

			// Token: 0x170001F4 RID: 500
			// (get) Token: 0x06000834 RID: 2100 RVA: 0x0001688E File Offset: 0x00014A8E
			internal ThreadManager.ThreadInfo CurrentThreadInfo
			{
				get
				{
					return this.threadInfo;
				}
			}

			// Token: 0x06000835 RID: 2101 RVA: 0x00016898 File Offset: 0x00014A98
			public void Dispose()
			{
				if (this.threadInfo != null)
				{
					if (this.topLevelMethod)
					{
						ThreadManager.Instance.RemoveCurrentThreadDiagnosticInfo();
					}
					else
					{
						this.threadInfo.MethodName = this.originalMethodName;
						this.threadInfo.ClientInfo = this.originalClientInformation;
						this.threadInfo.TimeoutDefinition = this.originalTimeoutDefinition;
					}
					this.threadInfo = null;
				}
			}

			// Token: 0x0400070F RID: 1807
			private ThreadManager.ThreadInfo threadInfo;

			// Token: 0x04000710 RID: 1808
			private bool topLevelMethod;

			// Token: 0x04000711 RID: 1809
			private string originalMethodName;

			// Token: 0x04000712 RID: 1810
			private ThreadManager.TimeoutDefinition originalTimeoutDefinition;

			// Token: 0x04000713 RID: 1811
			private ThreadManager.ThreadDiagnosticInfo.ClientInformation originalClientInformation;
		}
	}
}
