using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service;
using Microsoft.Exchange.ProcessManager;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Win32;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Server.Storage.WorkerManager
{
	// Token: 0x02000005 RID: 5
	public sealed class WorkerManager : IWorkerManager
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000020D0 File Offset: 0x000002D0
		private WorkerManager()
		{
			this.lastGeneration = 0;
			this.workers = PersistentAvlTree<Guid, WorkerManager.Worker>.Empty;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020F5 File Offset: 0x000002F5
		public static IWorkerManager Instance
		{
			get
			{
				return WorkerManager.instance;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020FC File Offset: 0x000002FC
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002103 File Offset: 0x00000303
		internal static string WorkerArguments
		{
			get
			{
				return WorkerManager.workerArguments;
			}
			set
			{
				WorkerManager.workerArguments = value;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000210C File Offset: 0x0000030C
		public static ErrorCode Initialize()
		{
			bool flag = false;
			ErrorCode result;
			try
			{
				string value = ConfigurationSchema.WorkerEnvironmentSettings.Value;
				ErrorCode errorCode;
				if (value != null)
				{
					string[] array = value.Split(new char[]
					{
						';'
					}, StringSplitOptions.RemoveEmptyEntries);
					foreach (string text in array)
					{
						string[] array3 = text.Split(new char[]
						{
							'='
						});
						if (array3.Length == 2)
						{
							array3[0] = array3[0].Trim();
							array3[1] = array3[1].Trim();
						}
						if (array3.Length != 2 || array3[0].Length == 0)
						{
							errorCode = ErrorCode.CreateCallFailed((LID)58965U);
							WorkerManager.TraceError("Invalid WorkerEnvironmentSettings in app.config", errorCode);
							return errorCode;
						}
						WorkerManager.workerEnvironmentSettings.Add(new KeyValuePair<string, string>(array3[0], array3[1]));
					}
				}
				WorkerManager.instance = new WorkerManager();
				ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Starting worker manager.");
				errorCode = WorkerManager.instance.Start();
				flag = (ErrorCode.NoError == errorCode);
				result = errorCode;
			}
			finally
			{
				if (!flag)
				{
					WorkerManager.Terminate();
				}
			}
			return result;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002248 File Offset: 0x00000448
		public static void Terminate()
		{
			if (WorkerManager.instance != null)
			{
				ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Stopping worker manager.");
				WorkerManager.instance.Stop();
				WorkerManager.instance = null;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002274 File Offset: 0x00000474
		public ErrorCode StartWorker(string workerPath, Guid instance, Guid dagOrServerGuid, string instanceName, Action<IWorkerProcess> workerCompleteCallback, CancellationToken cancellationToken, out IWorkerProcess workerProcess)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			workerProcess = null;
			WorkerManager.Worker worker = null;
			bool flag = false;
			try
			{
				using (LockManager.Lock(this.syncRoot))
				{
					PersistentAvlTree<Guid, WorkerManager.Worker> persistentAvlTree = this.GetWorkerMap();
					if (persistentAvlTree == null)
					{
						errorCode = ErrorCode.CreateRpcServerUnavailable((LID)55960U);
						WorkerManager.TraceError("Worker manager is being shut down.", errorCode);
						return errorCode;
					}
					if (cancellationToken.IsCancellationRequested)
					{
						errorCode = ErrorCode.CreateRpcServerUnavailable((LID)58160U);
						WorkerManager.TraceError("Worker startup is cancelled.", errorCode);
						return errorCode;
					}
					if (persistentAvlTree.TryGetValue(instance, out worker))
					{
						flag = true;
					}
					else
					{
						worker = new WorkerManager.Worker(instance, dagOrServerGuid, instanceName, ++this.lastGeneration, workerCompleteCallback);
						errorCode = worker.StartProcess(workerPath, this.job);
						if (ErrorCode.NoError != errorCode)
						{
							WorkerManager.TraceError("Failed to start worker process.", errorCode);
							Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_WorkerProcessFailedToStart, new object[]
							{
								instance,
								((int)errorCode).ToString()
							});
							return errorCode;
						}
						persistentAvlTree = persistentAvlTree.Add(worker.InstanceId, worker);
						Interlocked.Exchange<PersistentAvlTree<Guid, WorkerManager.Worker>>(ref this.workers, persistentAvlTree);
						flag = true;
						if (ExTraceGlobals.StartupShutdownTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							StringBuilder stringBuilder = new StringBuilder(100);
							stringBuilder.Append("Worker process created: Worker:[");
							worker.AppendToString(stringBuilder);
							stringBuilder.Append("]");
							ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, stringBuilder.ToString());
						}
						Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_WorkerProcessStarted, new object[]
						{
							instance,
							worker.ProcessId.ToString()
						});
					}
				}
				errorCode = worker.WaitReady(cancellationToken);
				if (ErrorCode.NoError == errorCode)
				{
					workerProcess = worker;
				}
			}
			finally
			{
				if (!flag && worker != null)
				{
					worker.Close();
				}
			}
			return errorCode;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002494 File Offset: 0x00000694
		public void StopWorker(Guid instance, bool terminate)
		{
			PersistentAvlTree<Guid, WorkerManager.Worker> workerMap = this.GetWorkerMap();
			WorkerManager.Worker worker;
			if (workerMap != null && workerMap.TryGetValue(instance, out worker))
			{
				try
				{
					worker.SignalStop(true, terminate);
				}
				finally
				{
					this.UnregisterStoppedWorker(worker);
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024D8 File Offset: 0x000006D8
		public ErrorCode GetWorker(Guid instance, out IWorkerProcess workerProcess)
		{
			workerProcess = null;
			PersistentAvlTree<Guid, WorkerManager.Worker> workerMap = this.GetWorkerMap();
			WorkerManager.Worker worker;
			if (workerMap == null || !workerMap.TryGetValue(instance, out worker))
			{
				return ErrorCode.CreateRpcServerUnavailable((LID)39576U);
			}
			workerProcess = worker;
			return ErrorCode.NoError;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000026BC File Offset: 0x000008BC
		public IEnumerable<IWorkerProcess> GetActiveWorkers()
		{
			PersistentAvlTree<Guid, WorkerManager.Worker> map = this.GetWorkerMap();
			if (map != null)
			{
				foreach (WorkerManager.Worker worker in map.GetValuesLmr())
				{
					yield return worker;
				}
			}
			yield break;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000026DC File Offset: 0x000008DC
		private static void TraceError(string message, ErrorCode ec)
		{
			if (ExTraceGlobals.StartupShutdownTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(50);
				stringBuilder.Append(message);
				stringBuilder.Append(" [Error=");
				stringBuilder.Append(ec.ToString());
				stringBuilder.Append("]");
				ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, stringBuilder.ToString());
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002744 File Offset: 0x00000944
		private static void TraceException(string message, Exception ex)
		{
			if (ExTraceGlobals.StartupShutdownTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(200);
				stringBuilder.Append(message);
				stringBuilder.Append(" [Exception=");
				stringBuilder.Append(ex.ToString());
				stringBuilder.Append("]");
				ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, stringBuilder.ToString());
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000027B4 File Offset: 0x000009B4
		private ErrorCode Start()
		{
			ErrorCode errorCode = ErrorCode.NoError;
			CompletionNotificationTask<WorkerManager> completionNotificationTask = null;
			if (this.GetWorkerMap() == null)
			{
				errorCode = ErrorCode.CreateNotSupported((LID)64152U);
				WorkerManager.TraceError("Restarting worker manager after shut down is not supported.", errorCode);
				return errorCode;
			}
			if (this.jobComplete != null)
			{
				return ErrorCode.NoError;
			}
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				ManualResetEvent disposable = new ManualResetEvent(true);
				disposeGuard.Add<ManualResetEvent>(disposable);
				SafeJobHandle safeJobHandle = NativeMethods.CreateJobObject(IntPtr.Zero, null);
				if (safeJobHandle == null || safeJobHandle.IsInvalid)
				{
					errorCode = ErrorCode.CreateWithLid((LID)50840U, (ErrorCodeValue)Marshal.GetLastWin32Error());
					WorkerManager.TraceError("Failed to create job object.", errorCode);
					return errorCode;
				}
				disposeGuard.Add<SafeJobHandle>(safeJobHandle);
				IoCompletionPort ioCompletionPort = NativeMethods.CreateIoCompletionPort(new SafeFileHandle(new IntPtr(-1), true), IoCompletionPort.InvalidHandle, UIntPtr.Zero, 0U);
				if (ioCompletionPort == null)
				{
					errorCode = ErrorCode.CreateWithLid((LID)47768U, (ErrorCodeValue)Marshal.GetLastWin32Error());
					WorkerManager.TraceError("Failed to create completion port.", errorCode);
					return errorCode;
				}
				disposeGuard.Add<IoCompletionPort>(ioCompletionPort);
				NativeMethods.JobObjectAssociateCompletionPort completionPort = new NativeMethods.JobObjectAssociateCompletionPort(IntPtr.Zero, ioCompletionPort.DangerousGetHandle());
				int num = safeJobHandle.SetCompletionPort(completionPort);
				if (num != 0)
				{
					errorCode = ErrorCode.CreateWithLid((LID)34456U, (ErrorCodeValue)Marshal.GetLastWin32Error());
					WorkerManager.TraceError("Failed to associate completion port with job.", errorCode);
					return errorCode;
				}
				completionNotificationTask = new CompletionNotificationTask<WorkerManager>(delegate(WorkerManager context, Func<bool> shouldCallbackContinue, uint notification, uint completionKey, int data)
				{
					context.JobNotificationHandler((WorkerManager.JobNotification)notification, data);
				}, this, ioCompletionPort, uint.MaxValue, false);
				this.jobCompletionPort = ioCompletionPort;
				this.job = safeJobHandle;
				this.jobNotificationTask = completionNotificationTask;
				this.jobComplete = disposable;
				disposeGuard.Success();
			}
			completionNotificationTask.Start();
			return errorCode;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002984 File Offset: 0x00000B84
		private void Stop()
		{
			PersistentAvlTree<Guid, WorkerManager.Worker> workerMap = this.GetWorkerMap();
			using (LockManager.Lock(this.syncRoot))
			{
				if (workerMap == null)
				{
					return;
				}
				Interlocked.Exchange<PersistentAvlTree<Guid, WorkerManager.Worker>>(ref this.workers, null);
			}
			foreach (WorkerManager.Worker worker in workerMap.GetValuesLmr())
			{
				worker.SignalStop(false, false);
			}
			try
			{
				if (this.jobComplete != null)
				{
					while (!this.jobComplete.WaitOne(TimeSpan.FromMinutes(1.0)))
					{
						if (this.job != null && (this.lastKnownProcessesInJob == null || this.lastKnownProcessesInJob.Length >= 0))
						{
							this.lastKnownProcessesInJob = this.GetLastKnownProcessesInJob();
						}
					}
				}
				if (this.jobNotificationTask != null)
				{
					this.jobNotificationTask.Stop();
					this.jobNotificationTask.WaitForCompletion();
				}
			}
			finally
			{
				if (this.jobComplete != null)
				{
					this.jobComplete.Dispose();
					this.jobComplete = null;
				}
				if (this.jobNotificationTask != null)
				{
					this.jobNotificationTask.Dispose();
					this.jobNotificationTask = null;
				}
				if (this.jobCompletionPort != null)
				{
					this.jobCompletionPort.Dispose();
					this.jobCompletionPort = null;
				}
				if (this.job != null)
				{
					NativeMethods.TerminateJobObject(this.job, 0U);
					this.job.Dispose();
					this.job = null;
				}
				foreach (WorkerManager.Worker worker2 in workerMap.GetValuesLmr())
				{
					worker2.Close();
				}
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002B50 File Offset: 0x00000D50
		private KeyValuePair<int, string>[] GetLastKnownProcessesInJob()
		{
			if (this.job == null)
			{
				return null;
			}
			int[] assignedProcessIds = this.job.GetAssignedProcessIds();
			if (assignedProcessIds == null || assignedProcessIds.Length == 0)
			{
				return null;
			}
			List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>(assignedProcessIds.Length);
			foreach (int num in assignedProcessIds)
			{
				string value = "<UNKNOWN>";
				try
				{
					using (Process processById = Process.GetProcessById(num))
					{
						if (!processById.HasExited)
						{
							value = processById.MainModule.ModuleName;
						}
					}
				}
				catch (InvalidOperationException exception)
				{
					NullExecutionDiagnostics.Instance.OnExceptionCatch(exception);
				}
				catch (ArgumentException exception2)
				{
					NullExecutionDiagnostics.Instance.OnExceptionCatch(exception2);
				}
				catch (Win32Exception exception3)
				{
					NullExecutionDiagnostics.Instance.OnExceptionCatch(exception3);
				}
				list.Add(new KeyValuePair<int, string>(num, value));
			}
			return list.ToArray();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002C50 File Offset: 0x00000E50
		private void JobNotificationHandler(WorkerManager.JobNotification message, int workerProcessId)
		{
			if (ExTraceGlobals.StartupShutdownTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append("Job notification received:[Event=");
				stringBuilder.Append(message.ToString());
				stringBuilder.Append(", PID=");
				stringBuilder.Append(workerProcessId);
				stringBuilder.Append("]");
				ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, stringBuilder.ToString());
			}
			switch (message)
			{
			case WorkerManager.JobNotification.JobNoActiveProcesses:
				break;
			case WorkerManager.JobNotification.JobEndOfTime | WorkerManager.JobNotification.JobNoActiveProcesses:
				return;
			case WorkerManager.JobNotification.JobNewProcess:
				this.jobComplete.Reset();
				return;
			case WorkerManager.JobNotification.JobProcessExit:
			case WorkerManager.JobNotification.JobProcessAbnormalExit:
			{
				PersistentAvlTree<Guid, WorkerManager.Worker> workerMap = this.GetWorkerMap();
				if (workerMap == null)
				{
					return;
				}
				using (IEnumerator<WorkerManager.Worker> enumerator = workerMap.GetValuesLmr().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						WorkerManager.Worker worker = enumerator.Current;
						if (worker.ProcessId == workerProcessId)
						{
							this.UnregisterStoppedWorker(worker);
							Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_WorkerProcessStopped, new object[]
							{
								worker.InstanceId.ToString(),
								worker.ProcessId.ToString()
							});
							break;
						}
					}
					return;
				}
				break;
			}
			default:
				return;
			}
			this.jobComplete.Set();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002D98 File Offset: 0x00000F98
		private PersistentAvlTree<Guid, WorkerManager.Worker> GetWorkerMap()
		{
			return Interlocked.CompareExchange<PersistentAvlTree<Guid, WorkerManager.Worker>>(ref this.workers, null, null);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002DA8 File Offset: 0x00000FA8
		private void UnregisterStoppedWorker(WorkerManager.Worker worker)
		{
			Guid instanceId = worker.InstanceId;
			int processId = worker.ProcessId;
			string instanceName = worker.InstanceName;
			bool flag = false;
			try
			{
				Action<IWorkerProcess> action = null;
				using (LockManager.Lock(this.syncRoot))
				{
					WorkerManager.Worker objB = null;
					PersistentAvlTree<Guid, WorkerManager.Worker> persistentAvlTree = this.GetWorkerMap();
					if (persistentAvlTree != null && persistentAvlTree.TryGetValue(worker.InstanceId, out objB) && object.ReferenceEquals(worker, objB))
					{
						persistentAvlTree = persistentAvlTree.Remove(worker.InstanceId);
						Interlocked.Exchange<PersistentAvlTree<Guid, WorkerManager.Worker>>(ref this.workers, persistentAvlTree);
						action = worker.CompleteCallback;
						if (!worker.StopSignalled)
						{
							flag = true;
						}
						if (ExTraceGlobals.StartupShutdownTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							StringBuilder stringBuilder = new StringBuilder(100);
							stringBuilder.Append("Worker process stopped: [");
							worker.AppendToString(stringBuilder);
							stringBuilder.Append("]");
							ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, stringBuilder.ToString());
						}
					}
				}
				if (action != null)
				{
					action(worker);
				}
			}
			finally
			{
				worker.Close();
				if (flag)
				{
					this.PublishFailureItem(instanceId, instanceName);
				}
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002ED0 File Offset: 0x000010D0
		private void PublishFailureItem(Guid databaseGuid, string databaseName)
		{
			DatabaseFailureItem databaseFailureItem = new DatabaseFailureItem(FailureNameSpace.Store, FailureTag.UnexpectedDismount, databaseGuid)
			{
				ComponentName = "StoreService",
				InstanceName = databaseName
			};
			databaseFailureItem.Publish();
		}

		// Token: 0x04000006 RID: 6
		private static List<KeyValuePair<string, string>> workerEnvironmentSettings = new List<KeyValuePair<string, string>>();

		// Token: 0x04000007 RID: 7
		private static WorkerManager instance = null;

		// Token: 0x04000008 RID: 8
		private static string workerArguments = string.Empty;

		// Token: 0x04000009 RID: 9
		private object syncRoot = new object();

		// Token: 0x0400000A RID: 10
		private IoCompletionPort jobCompletionPort;

		// Token: 0x0400000B RID: 11
		private SafeJobHandle job;

		// Token: 0x0400000C RID: 12
		private CompletionNotificationTask<WorkerManager> jobNotificationTask;

		// Token: 0x0400000D RID: 13
		private ManualResetEvent jobComplete;

		// Token: 0x0400000E RID: 14
		private PersistentAvlTree<Guid, WorkerManager.Worker> workers;

		// Token: 0x0400000F RID: 15
		private int lastGeneration;

		// Token: 0x04000010 RID: 16
		private KeyValuePair<int, string>[] lastKnownProcessesInJob;

		// Token: 0x02000006 RID: 6
		[Flags]
		private enum JobNotification : uint
		{
			// Token: 0x04000013 RID: 19
			JobEndOfTime = 1U,
			// Token: 0x04000014 RID: 20
			JobEndOfProcessTime = 2U,
			// Token: 0x04000015 RID: 21
			JobActiveProcessLimit = 3U,
			// Token: 0x04000016 RID: 22
			JobNoActiveProcesses = 4U,
			// Token: 0x04000017 RID: 23
			JobNewProcess = 6U,
			// Token: 0x04000018 RID: 24
			JobProcessExit = 7U,
			// Token: 0x04000019 RID: 25
			JobProcessAbnormalExit = 8U,
			// Token: 0x0400001A RID: 26
			JobProcessMemoryLimit = 9U,
			// Token: 0x0400001B RID: 27
			JobMemoryLimit = 10U
		}

		// Token: 0x02000007 RID: 7
		private class Worker : IWorkerProcess
		{
			// Token: 0x06000020 RID: 32 RVA: 0x00002F1D File Offset: 0x0000111D
			public Worker(Guid instance, Guid dagOrServerGuid, string instanceName, int generation, Action<IWorkerProcess> workerCompleteCallback)
			{
				this.InstanceId = instance;
				this.DagOrServerGuid = dagOrServerGuid;
				this.InstanceName = instanceName;
				this.Generation = generation;
				this.ProcessId = -1;
				this.CompleteCallback = workerCompleteCallback;
			}

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000021 RID: 33 RVA: 0x00002F5C File Offset: 0x0000115C
			// (set) Token: 0x06000022 RID: 34 RVA: 0x00002F64 File Offset: 0x00001164
			public int ProcessId { get; private set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000023 RID: 35 RVA: 0x00002F6D File Offset: 0x0000116D
			// (set) Token: 0x06000024 RID: 36 RVA: 0x00002F75 File Offset: 0x00001175
			public Guid InstanceId { get; private set; }

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000025 RID: 37 RVA: 0x00002F7E File Offset: 0x0000117E
			// (set) Token: 0x06000026 RID: 38 RVA: 0x00002F86 File Offset: 0x00001186
			public Guid DagOrServerGuid { get; private set; }

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000027 RID: 39 RVA: 0x00002F8F File Offset: 0x0000118F
			// (set) Token: 0x06000028 RID: 40 RVA: 0x00002F97 File Offset: 0x00001197
			public int Generation { get; private set; }

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000029 RID: 41 RVA: 0x00002FA0 File Offset: 0x000011A0
			// (set) Token: 0x0600002A RID: 42 RVA: 0x00002FA8 File Offset: 0x000011A8
			public Action<IWorkerProcess> CompleteCallback { get; private set; }

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x0600002B RID: 43 RVA: 0x00002FB1 File Offset: 0x000011B1
			// (set) Token: 0x0600002C RID: 44 RVA: 0x00002FB9 File Offset: 0x000011B9
			public bool StopSignalled { get; private set; }

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600002D RID: 45 RVA: 0x00002FC2 File Offset: 0x000011C2
			// (set) Token: 0x0600002E RID: 46 RVA: 0x00002FCA File Offset: 0x000011CA
			public string InstanceName { get; private set; }

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x0600002F RID: 47 RVA: 0x00002FD3 File Offset: 0x000011D3
			// (set) Token: 0x06000030 RID: 48 RVA: 0x00002FDB File Offset: 0x000011DB
			public DatabaseType InstanceDBType { get; set; }

			// Token: 0x06000031 RID: 49 RVA: 0x00002FE4 File Offset: 0x000011E4
			public ErrorCode StartProcess(string workerPath, SafeJobHandle job)
			{
				ErrorCode errorCode = ErrorCode.NoError;
				Semaphore semaphore = null;
				SafeFileHandle safeFileHandle = null;
				SafeFileHandle safeFileHandle2 = null;
				PipeStream disposable = null;
				Process process = null;
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					using (DisposeGuard disposeGuard2 = default(DisposeGuard))
					{
						string text = null;
						semaphore = this.CreateNamedSemaphore("Global\\WorkerReadyKey-", out text);
						if (semaphore == null)
						{
							errorCode = ErrorCode.CreateCallFailed((LID)54936U);
							WorkerManager.TraceError("Failed to create a named semaphore: Ready Key", errorCode);
							return errorCode;
						}
						disposeGuard2.Add<Semaphore>(semaphore);
						using (DisposeGuard disposeGuard3 = default(DisposeGuard))
						{
							if (!PipeStream.TryCreatePipeHandles(out safeFileHandle2, out safeFileHandle, ExTraceGlobals.StartupShutdownTracer))
							{
								errorCode = ErrorCode.CreateCallFailed((LID)42648U);
								WorkerManager.TraceError("Failed to create a named semaphore: Ready Key", errorCode);
								return errorCode;
							}
							disposeGuard3.Add<SafeFileHandle>(safeFileHandle2);
							disposeGuard.Add<SafeFileHandle>(safeFileHandle);
							disposable = new PipeStream(safeFileHandle2, FileAccess.Write, false);
							disposeGuard3.Success();
							disposeGuard2.Add<PipeStream>(disposable);
						}
						ProcessStartInfo processStartInfo = new ProcessStartInfo();
						processStartInfo.FileName = workerPath;
						processStartInfo.CreateNoWindow = true;
						processStartInfo.UseShellExecute = false;
						foreach (KeyValuePair<string, string> keyValuePair in WorkerManager.workerEnvironmentSettings)
						{
							if (keyValuePair.Value == null || keyValuePair.Value.Length == 0)
							{
								processStartInfo.EnvironmentVariables.Remove(keyValuePair.Key);
							}
							else
							{
								processStartInfo.EnvironmentVariables[keyValuePair.Key] = keyValuePair.Value;
							}
						}
						processStartInfo.Arguments = string.Format("-id:{0} -dag:{1} -pipe:{2} -readykey:{3} {4}", new object[]
						{
							this.InstanceId.ToString(),
							this.DagOrServerGuid.ToString(),
							safeFileHandle.DangerousGetHandle().ToInt64(),
							text,
							WorkerManager.WorkerArguments
						});
						process = new Process();
						process.StartInfo = processStartInfo;
						bool flag = false;
						try
						{
							flag = process.Start();
						}
						catch (Win32Exception ex)
						{
							NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
							errorCode = ErrorCode.CreateWithLid((LID)59032U, (ErrorCodeValue)ex.NativeErrorCode);
							WorkerManager.TraceException("Failed to start the worker process", ex);
							return errorCode;
						}
						if (!flag)
						{
							errorCode = ErrorCode.CreateCallFailed((LID)38552U);
							WorkerManager.TraceError("Failed to start the worker process", errorCode);
							return errorCode;
						}
						errorCode = WorkerManager.Worker.AddToJobHandle(job, process);
						if (ErrorCode.NoError != errorCode)
						{
							return errorCode;
						}
						this.ProcessId = process.Id;
						this.workerProcess = process;
						this.workerControlPipe = disposable;
						this.workerReadyHandle = semaphore;
						disposeGuard2.Success();
					}
				}
				return ErrorCode.NoError;
			}

			// Token: 0x06000032 RID: 50 RVA: 0x0000333C File Offset: 0x0000153C
			public ErrorCode WaitReady(CancellationToken cancellationToken)
			{
				ErrorCode errorCode = ErrorCode.NoError;
				IntPtr[] array = cancellationToken.CanBeCanceled ? new IntPtr[3] : new IntPtr[2];
				using (LockManager.Lock(this.syncRoot))
				{
					array[0] = ((this.workerReadyHandle != null) ? this.workerReadyHandle.SafeWaitHandle.DangerousGetHandle() : IntPtr.Zero);
					array[1] = ((this.workerProcess != null) ? this.workerProcess.Handle : IntPtr.Zero);
					if (array.Length == 3)
					{
						array[2] = cancellationToken.WaitHandle.SafeWaitHandle.DangerousGetHandle();
					}
				}
				if (array[0] != IntPtr.Zero && array[1] != IntPtr.Zero && (array.Length == 2 || array[2] != IntPtr.Zero))
				{
					if (ExTraceGlobals.StartupShutdownTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						StringBuilder stringBuilder = new StringBuilder(100);
						stringBuilder.Append("Waiting for worker process to become ready: Worker:[");
						this.AppendToString(stringBuilder);
						stringBuilder.Append("]");
						ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, stringBuilder.ToString());
					}
					uint num = NativeMethods.WaitForMultipleObjects((uint)array.Length, array, false, uint.MaxValue);
					if (num != 0U)
					{
						errorCode = ErrorCode.CreateRpcServerUnavailable((LID)59768U);
					}
				}
				else
				{
					errorCode = ErrorCode.CreateRpcServerUnavailable((LID)43384U);
				}
				if (ErrorCode.NoError == errorCode)
				{
					if (ExTraceGlobals.StartupShutdownTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						StringBuilder stringBuilder2 = new StringBuilder(100);
						stringBuilder2.Append("Worker process is ready: Worker:[");
						this.AppendToString(stringBuilder2);
						stringBuilder2.Append("]");
						ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, stringBuilder2.ToString());
					}
				}
				else if (ExTraceGlobals.StartupShutdownTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					StringBuilder stringBuilder3 = new StringBuilder(100);
					stringBuilder3.Append("Worker process is not running: Worker:[");
					this.AppendToString(stringBuilder3);
					stringBuilder3.Append("]");
					ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, stringBuilder3.ToString());
				}
				return errorCode;
			}

			// Token: 0x06000033 RID: 51 RVA: 0x0000357C File Offset: 0x0000177C
			public void SignalStop(bool waitForExit, bool terminate)
			{
				int milliseconds = -1;
				using (LockManager.Lock(this.syncRoot))
				{
					if (this.workerProcess != null)
					{
						if (terminate)
						{
							if (ExTraceGlobals.FaultInjectionTracer.IsTraceEnabled(TraceType.FaultInjection))
							{
								ExTraceGlobals.FaultInjectionTracer.TraceTest<Guid>(3972410685U, this.InstanceId);
							}
							if (ExTraceGlobals.StartupShutdownTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								StringBuilder stringBuilder = new StringBuilder(100);
								stringBuilder.Append("Terminating worker process: Worker:[");
								this.AppendToString(stringBuilder);
								stringBuilder.Append("]");
								ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, stringBuilder.ToString());
							}
							try
							{
								this.workerProcess.Kill();
							}
							catch (InvalidOperationException exception)
							{
								NullExecutionDiagnostics.Instance.OnExceptionCatch(exception);
							}
							catch (Win32Exception exception2)
							{
								NullExecutionDiagnostics.Instance.OnExceptionCatch(exception2);
							}
							milliseconds = (int)TimeSpan.FromSeconds(10.0).TotalMilliseconds;
						}
						else
						{
							if (ExTraceGlobals.StartupShutdownTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								StringBuilder stringBuilder2 = new StringBuilder(100);
								stringBuilder2.Append("Sending stop signal: Worker:[");
								this.AppendToString(stringBuilder2);
								stringBuilder2.Append("]");
								ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, stringBuilder2.ToString());
							}
							WorkerManager.Worker.SendMessage(this.workerControlPipe, WorkerManager.Worker.Retire, 0, WorkerManager.Worker.Retire.Length);
						}
						this.StopSignalled = true;
						if (waitForExit || terminate)
						{
							if (ExTraceGlobals.StartupShutdownTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								StringBuilder stringBuilder3 = new StringBuilder(100);
								stringBuilder3.Append("Wait for worker process completion: Worker:[");
								this.AppendToString(stringBuilder3);
								stringBuilder3.Append("]");
								ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, stringBuilder3.ToString());
							}
							if (this.workerProcess.WaitForExit(milliseconds))
							{
								if (ExTraceGlobals.StartupShutdownTracer.IsTraceEnabled(TraceType.DebugTrace))
								{
									StringBuilder stringBuilder4 = new StringBuilder(100);
									stringBuilder4.Append("Worker process stoped: Worker:[");
									this.AppendToString(stringBuilder4);
									stringBuilder4.Append("]");
									ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, stringBuilder4.ToString());
								}
								Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_WorkerProcessStopped, new object[]
								{
									this.InstanceId.ToString(),
									this.ProcessId.ToString()
								});
							}
							else
							{
								if (ExTraceGlobals.StartupShutdownTracer.IsTraceEnabled(TraceType.DebugTrace))
								{
									StringBuilder stringBuilder5 = new StringBuilder(100);
									stringBuilder5.Append("Waiting for worker process to stop timed out after ");
									stringBuilder5.Append(milliseconds.ToString());
									stringBuilder5.Append("ms. Worker:[");
									this.AppendToString(stringBuilder5);
									stringBuilder5.Append("]");
									ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, stringBuilder5.ToString());
								}
								Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_WorkerProcessStopTimeout, new object[]
								{
									this.InstanceId.ToString(),
									this.ProcessId.ToString()
								});
							}
						}
					}
				}
			}

			// Token: 0x06000034 RID: 52 RVA: 0x000038B8 File Offset: 0x00001AB8
			public void Close()
			{
				using (LockManager.Lock(this.syncRoot))
				{
					if (this.workerProcess != null)
					{
						this.workerProcess.Dispose();
						this.workerProcess = null;
					}
					if (this.workerReadyHandle != null)
					{
						this.workerReadyHandle.Close();
						this.workerReadyHandle = null;
					}
					if (this.workerControlPipe != null)
					{
						this.workerControlPipe.Close();
						this.workerControlPipe = null;
					}
				}
			}

			// Token: 0x06000035 RID: 53 RVA: 0x00003940 File Offset: 0x00001B40
			public void AppendToString(StringBuilder sb)
			{
				sb.Append("PID=");
				sb.Append(this.ProcessId);
				sb.Append(", Generation=");
				sb.Append(this.Generation);
				sb.Append(", Instance=");
				sb.Append(this.InstanceId.ToString());
			}

			// Token: 0x06000036 RID: 54 RVA: 0x000039A8 File Offset: 0x00001BA8
			private static ErrorCode AddToJobHandle(SafeJobHandle job, Process process)
			{
				if (!NativeMethods.AssignProcessToJobObject(job, process.Handle))
				{
					ErrorCode errorCode = ErrorCode.CreateWithLid((LID)35192U, (ErrorCodeValue)Marshal.GetLastWin32Error());
					WorkerManager.TraceError("Failed to assing process to job handle", errorCode);
					return errorCode;
				}
				return ErrorCode.NoError;
			}

			// Token: 0x06000037 RID: 55 RVA: 0x000039EC File Offset: 0x00001BEC
			private static void SendMessage(PipeStream pipeStream, byte[] buffer, int offset, int count)
			{
				if (pipeStream != null)
				{
					try
					{
						pipeStream.Write(buffer, offset, count);
						ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Message was sent to worker process]");
					}
					catch (IOException ex)
					{
						NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
						WorkerManager.TraceException("Failed to send control command to the worker process. ", ex);
					}
				}
			}

			// Token: 0x06000038 RID: 56 RVA: 0x00003A44 File Offset: 0x00001C44
			private Semaphore CreateNamedSemaphore(string prefix, out string name)
			{
				bool flag = false;
				Semaphore semaphore = null;
				name = null;
				for (int i = 0; i < 10; i++)
				{
					name = prefix + Guid.NewGuid();
					semaphore = new Semaphore(0, 1, name, ref flag);
					if (flag)
					{
						break;
					}
					if (semaphore != null)
					{
						semaphore.Close();
						semaphore = null;
					}
				}
				return semaphore;
			}

			// Token: 0x0400001C RID: 28
			private static readonly byte[] Retire = new byte[]
			{
				82
			};

			// Token: 0x0400001D RID: 29
			private object syncRoot = new object();

			// Token: 0x0400001E RID: 30
			private Semaphore workerReadyHandle;

			// Token: 0x0400001F RID: 31
			private PipeStream workerControlPipe;

			// Token: 0x04000020 RID: 32
			private Process workerProcess;
		}
	}
}
