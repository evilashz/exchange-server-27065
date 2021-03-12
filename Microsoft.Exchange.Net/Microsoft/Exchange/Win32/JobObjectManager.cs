using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x02000B32 RID: 2866
	internal class JobObjectManager : DisposeTrackableBase
	{
		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06003DDB RID: 15835 RVA: 0x000A1A41 File Offset: 0x0009FC41
		// (set) Token: 0x06003DDC RID: 15836 RVA: 0x000A1A49 File Offset: 0x0009FC49
		internal CallbackMontiorEvent CallbackMonitorEvent
		{
			get
			{
				return this.callbackMonitorEvent;
			}
			set
			{
				this.callbackMonitorEvent = value;
			}
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x000A1A52 File Offset: 0x0009FC52
		public JobObjectManager(int workerMemoryLimit) : this(1010U, workerMemoryLimit)
		{
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x000A1A60 File Offset: 0x0009FC60
		public JobObjectManager(uint ioCompletionPortNumber, int workerMemoryLimit)
		{
			AppDomain.CurrentDomain.DomainUnload += this.OnAppDomainUnloadHandler;
			this.ioCompletionPortNumber = ioCompletionPortNumber;
			this.workerMemoryLimit = workerMemoryLimit;
			this.ioCompletionPort = NativeMethods.CreateIoCompletionPort(new SafeFileHandle(new IntPtr(-1), true), IoCompletionPort.InvalidHandle, new UIntPtr(this.ioCompletionPortNumber), 0U);
			if (this.ioCompletionPort.IsInvalid)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				this.TraceError(this, "Failed to create IO completion port. Error code is {0}.", new object[]
				{
					lastWin32Error
				});
				throw new Win32Exception(lastWin32Error, "Failed to create IO completion port. Error code is {0}.");
			}
			this.jobObjectCompletport = new NativeMethods.JobObjectAssociateCompletionPort(new IntPtr((long)((ulong)this.ioCompletionPortNumber)), this.ioCompletionPort.DangerousGetHandle());
		}

		// Token: 0x06003DDF RID: 15839 RVA: 0x000A1B28 File Offset: 0x0009FD28
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				if (this.ioCompletionPort != null)
				{
					this.StopProcessMonitor();
					this.ioCompletionPort.Dispose();
					this.ioCompletionPort = null;
				}
				AppDomain.CurrentDomain.DomainUnload -= this.OnAppDomainUnloadHandler;
			}
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x000A1B63 File Offset: 0x0009FD63
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<JobObjectManager>(this);
		}

		// Token: 0x06003DE1 RID: 15841 RVA: 0x000A1B6C File Offset: 0x0009FD6C
		public IDisposable CreateJobObject(SafeProcessHandle process, bool mayRunUnderAnotherJobObject)
		{
			JobObjectManager.JobObject jobObject = new JobObjectManager.JobObject(IntPtr.Zero, null, this.workerMemoryLimit, this.jobObjectCompletport);
			bool flag = false;
			try
			{
				jobObject.Tracer = this.tracer;
				if (jobObject.Add(process, mayRunUnderAnotherJobObject))
				{
					lock (this.myLock)
					{
						this.StartProcessMonitor();
					}
					flag = true;
				}
			}
			finally
			{
				if (!flag)
				{
					jobObject.Dispose();
					jobObject = null;
				}
			}
			return jobObject;
		}

		// Token: 0x06003DE2 RID: 15842 RVA: 0x000A1BFC File Offset: 0x0009FDFC
		private void StopProcessMonitor()
		{
			if (this.monitorThread != null)
			{
				if (this.ioCompletionPort != null)
				{
					this.ioCompletionPort.PostQueuedCompletionStatus(65534U, this.ioCompletionPortNumber);
				}
				this.monitorThread.Join();
				this.monitorThread = null;
			}
		}

		// Token: 0x06003DE3 RID: 15843 RVA: 0x000A1C38 File Offset: 0x0009FE38
		private void StartProcessMonitor()
		{
			if (this.monitorThread != null)
			{
				return;
			}
			this.monitorThread = new Thread(new ThreadStart(this.MemoryMonitorProc));
			this.monitorThread.Name = "Job Memory Monitor";
			this.monitorThread.IsBackground = true;
			this.monitorThread.Start();
		}

		// Token: 0x06003DE4 RID: 15844 RVA: 0x000A1C8C File Offset: 0x0009FE8C
		private void OnAppDomainUnloadHandler(object sender, EventArgs e)
		{
			if (!base.IsDisposed)
			{
				this.StopProcessMonitor();
			}
		}

		// Token: 0x06003DE5 RID: 15845 RVA: 0x000A1C9C File Offset: 0x0009FE9C
		private void MemoryMonitorProc()
		{
			if (this.callbackMonitorEvent != null)
			{
				try
				{
					this.callbackMonitorEvent(MonitorEvent.MonitorStart, new object[0]);
				}
				catch (Exception ex)
				{
					this.TraceError(this, "Job object monitor thread start callback function if job object failed by {0}", new object[]
					{
						ex
					});
				}
			}
			for (;;)
			{
				uint num = 0U;
				UIntPtr uintPtr = new UIntPtr(0U);
				int num2 = 0;
				try
				{
					this.ioCompletionPort.GetQueuedCompletionStatus(out num, out uintPtr, out num2, uint.MaxValue);
				}
				catch (Win32Exception ex2)
				{
					this.TraceError(this, "Call GetQueuedCompletionStatus failed when monitor the child process memory. error is {0}", new object[]
					{
						ex2.ToString()
					});
					break;
				}
				if (uintPtr.ToUInt32() == this.ioCompletionPortNumber)
				{
					if (num == 9U)
					{
						if (this.callbackMonitorEvent == null)
						{
							continue;
						}
						try
						{
							this.callbackMonitorEvent(MonitorEvent.ReachMemoryLimitation, new object[]
							{
								num2
							});
							continue;
						}
						catch (Exception ex3)
						{
							this.TraceError(this, "Memory exceeded limitation callback function if job object failed by {0}", new object[]
							{
								ex3
							});
							continue;
						}
					}
					if (num == 65534U)
					{
						break;
					}
				}
			}
			if (this.callbackMonitorEvent != null)
			{
				try
				{
					this.callbackMonitorEvent(MonitorEvent.MonitorStop, new object[0]);
				}
				catch (Exception ex4)
				{
					this.TraceError(this, "Job object monitor thread end callback function if job object failed by {0}", new object[]
					{
						ex4
					});
				}
			}
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x000A1E10 File Offset: 0x000A0010
		private void TraceError(object target, string formatString, params object[] args)
		{
			if (this.tracer != null)
			{
				this.tracer.TraceError((long)((target != null) ? target.GetHashCode() : 0), formatString, args);
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06003DE7 RID: 15847 RVA: 0x000A1E34 File Offset: 0x000A0034
		// (set) Token: 0x06003DE8 RID: 15848 RVA: 0x000A1E3C File Offset: 0x000A003C
		internal Trace Tracer
		{
			get
			{
				return this.tracer;
			}
			set
			{
				this.tracer = value;
			}
		}

		// Token: 0x040035BA RID: 13754
		private const uint defaultIoCompletionPortNumber = 1010U;

		// Token: 0x040035BB RID: 13755
		private IoCompletionPort ioCompletionPort;

		// Token: 0x040035BC RID: 13756
		private NativeMethods.JobObjectAssociateCompletionPort jobObjectCompletport;

		// Token: 0x040035BD RID: 13757
		private int workerMemoryLimit;

		// Token: 0x040035BE RID: 13758
		private uint ioCompletionPortNumber;

		// Token: 0x040035BF RID: 13759
		private Thread monitorThread;

		// Token: 0x040035C0 RID: 13760
		private Trace tracer;

		// Token: 0x040035C1 RID: 13761
		private object myLock = new object();

		// Token: 0x040035C2 RID: 13762
		private CallbackMontiorEvent callbackMonitorEvent;

		// Token: 0x02000B33 RID: 2867
		private sealed class JobObject : DisposeTrackableBase
		{
			// Token: 0x06003DE9 RID: 15849 RVA: 0x000A1E48 File Offset: 0x000A0048
			public JobObject(IntPtr jobAttributes, string name, int maxMemoryPerProcess, NativeMethods.JobObjectAssociateCompletionPort jobObjectCompletionPort)
			{
				if (maxMemoryPerProcess <= 0)
				{
					throw new ArgumentException("Invalid maximum memory per process.", "maxMemoryPerProcess");
				}
				this.safeJobHandle = NativeMethods.CreateJobObject(jobAttributes, name);
				if (this.safeJobHandle.IsInvalid)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					this.TraceError(this, "Failed to create Job object. Error code is {0}.", new object[]
					{
						lastWin32Error
					});
					throw new Win32Exception(lastWin32Error, "Failed to create Job object. Error code is {0}.");
				}
				this.jobObjectCompletionPort = jobObjectCompletionPort;
				this.safeJobHandle.SetUIRestrictions(JobObjectUILimit.ReadClipboard | JobObjectUILimit.SystemParameters | JobObjectUILimit.WriteClipboard | JobObjectUILimit.Desktop | JobObjectUILimit.DisplaySettings | JobObjectUILimit.ExitWindows | JobObjectUILimit.GlobalAtoms);
				NativeMethods.JOBOBJECT_EXTENDED_LIMIT_INFORMATION extendedLimits = default(NativeMethods.JOBOBJECT_EXTENDED_LIMIT_INFORMATION);
				extendedLimits.BasicLimitInformation.LimitFlags = 8448U;
				extendedLimits.ProcessMemoryLimit = new UIntPtr((uint)maxMemoryPerProcess);
				this.safeJobHandle.SetExtendedLimits(extendedLimits);
			}

			// Token: 0x17000F4B RID: 3915
			// (get) Token: 0x06003DEA RID: 15850 RVA: 0x000A1F02 File Offset: 0x000A0102
			// (set) Token: 0x06003DEB RID: 15851 RVA: 0x000A1F0A File Offset: 0x000A010A
			internal Trace Tracer
			{
				get
				{
					return this.tracer;
				}
				set
				{
					this.tracer = value;
				}
			}

			// Token: 0x06003DEC RID: 15852 RVA: 0x000A1F13 File Offset: 0x000A0113
			protected override void InternalDispose(bool isDisposing)
			{
				if (isDisposing && this.safeJobHandle != null)
				{
					this.safeJobHandle.Dispose();
					this.safeJobHandle = null;
				}
			}

			// Token: 0x06003DED RID: 15853 RVA: 0x000A1F32 File Offset: 0x000A0132
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<JobObjectManager.JobObject>(this);
			}

			// Token: 0x06003DEE RID: 15854 RVA: 0x000A1F3C File Offset: 0x000A013C
			internal bool Add(SafeProcessHandle process, bool ignoreAccessDeniedFailure)
			{
				if (process == null || process.IsInvalid)
				{
					throw new ArgumentException("The process handle is either null or invalid.");
				}
				this.SetCompletionPort();
				if (this.safeJobHandle.Add(process))
				{
					return true;
				}
				int lastWin32Error = Marshal.GetLastWin32Error();
				if ((lastWin32Error == 5 || lastWin32Error == 50) && ignoreAccessDeniedFailure)
				{
					this.TraceError(this, "AccessDenied or NotSupported when assigning process to job object", new object[0]);
					return false;
				}
				this.TraceError(this, "Failed to assign the process to the job object {0}", new object[]
				{
					lastWin32Error
				});
				throw new Win32Exception(lastWin32Error, "Failed to assign the process to the job object.");
			}

			// Token: 0x06003DEF RID: 15855 RVA: 0x000A1FC4 File Offset: 0x000A01C4
			private unsafe void SetCompletionPort()
			{
				bool flag;
				fixed (IntPtr* ptr = (IntPtr*)(&this.jobObjectCompletionPort))
				{
					flag = NativeMethods.SetInformationJobObject(this.safeJobHandle, NativeMethods.JOBOBJECTINFOCLASS.JobObjectAssociateCompletionPortInformation, (void*)ptr, Marshal.SizeOf(typeof(NativeMethods.JobObjectAssociateCompletionPort)));
				}
				if (!flag)
				{
					this.TraceError(this, "Call SetInformationJobObject() failed when configurate the Completion port", new object[0]);
					throw new Win32Exception("Call SetInformationJobObject() failed when configurate the Completion port");
				}
			}

			// Token: 0x06003DF0 RID: 15856 RVA: 0x000A2019 File Offset: 0x000A0219
			private void TraceError(object target, string formatString, params object[] args)
			{
				if (this.tracer != null)
				{
					this.tracer.TraceError((long)((target != null) ? target.GetHashCode() : 0), formatString, args);
				}
			}

			// Token: 0x040035C3 RID: 13763
			private const int Win32ErrorAccessDenied = 5;

			// Token: 0x040035C4 RID: 13764
			private const int Win32ErrorNotSupported = 50;

			// Token: 0x040035C5 RID: 13765
			private SafeJobHandle safeJobHandle;

			// Token: 0x040035C6 RID: 13766
			private NativeMethods.JobObjectAssociateCompletionPort jobObjectCompletionPort;

			// Token: 0x040035C7 RID: 13767
			private Trace tracer;

			// Token: 0x02000B34 RID: 2868
			[Flags]
			private enum JobObjectExtendedLimit : uint
			{
				// Token: 0x040035C9 RID: 13769
				LimitProcessMemory = 256U,
				// Token: 0x040035CA RID: 13770
				LimitJobMemory = 512U,
				// Token: 0x040035CB RID: 13771
				LimitDieOnUnhandledException = 1024U,
				// Token: 0x040035CC RID: 13772
				LimitBreakawayOK = 2048U,
				// Token: 0x040035CD RID: 13773
				LimitSilentBreakawayOK = 4096U,
				// Token: 0x040035CE RID: 13774
				LimitKillOnJobClose = 8192U
			}
		}

		// Token: 0x02000B35 RID: 2869
		private enum JobObjectMessage : uint
		{
			// Token: 0x040035D0 RID: 13776
			ProcessMemoryLimit = 9U,
			// Token: 0x040035D1 RID: 13777
			StopMonitoring = 65534U
		}
	}
}
