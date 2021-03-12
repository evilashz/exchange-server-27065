using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Win32;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x0200081A RID: 2074
	internal class WorkerInstance
	{
		// Token: 0x06002BC1 RID: 11201 RVA: 0x000615FC File Offset: 0x0005F7FC
		internal WorkerInstance(bool passive, WorkerInstance.WorkerContacted workerContactedDelegate, WorkerInstance.WorkerExited workerExitedDelegate, int thrashCrashCount, Microsoft.Exchange.Diagnostics.Trace tracer)
		{
			this.diag = tracer;
			this.workerContactedDelegate = workerContactedDelegate;
			this.workerExitedDelegate = workerExitedDelegate;
			this.workerIsActive = !passive;
			this.thrashCrashCount = thrashCrashCount;
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06002BC2 RID: 11202 RVA: 0x0006162C File Offset: 0x0005F82C
		internal int Pid
		{
			get
			{
				return this.pid;
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06002BC3 RID: 11203 RVA: 0x00061634 File Offset: 0x0005F834
		// (set) Token: 0x06002BC4 RID: 11204 RVA: 0x0006163C File Offset: 0x0005F83C
		internal bool IsActive
		{
			get
			{
				return this.workerIsActive;
			}
			set
			{
				this.workerIsActive = value;
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06002BC5 RID: 11205 RVA: 0x00061645 File Offset: 0x0005F845
		internal bool Exited
		{
			get
			{
				return this.exited;
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06002BC6 RID: 11206 RVA: 0x0006164D File Offset: 0x0005F84D
		internal bool IsConnected
		{
			get
			{
				return this.pipeConnected;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06002BC7 RID: 11207 RVA: 0x00061655 File Offset: 0x0005F855
		internal bool SignaledReady
		{
			get
			{
				return this.signaledReady;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06002BC8 RID: 11208 RVA: 0x0006165D File Offset: 0x0005F85D
		internal Process Process
		{
			get
			{
				return this.process;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06002BC9 RID: 11209 RVA: 0x00061665 File Offset: 0x0005F865
		internal bool ResetRequested
		{
			get
			{
				return this.resetRequested;
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06002BCA RID: 11210 RVA: 0x0006166D File Offset: 0x0005F86D
		// (set) Token: 0x06002BCB RID: 11211 RVA: 0x00061675 File Offset: 0x0005F875
		internal int ThrashCrashCount
		{
			get
			{
				return this.thrashCrashCount;
			}
			set
			{
				this.thrashCrashCount = value;
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06002BCC RID: 11212 RVA: 0x0006167E File Offset: 0x0005F87E
		// (set) Token: 0x06002BCD RID: 11213 RVA: 0x00061686 File Offset: 0x0005F886
		internal DateTime StartTime
		{
			get
			{
				return this.startTime;
			}
			set
			{
				this.startTime = value;
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06002BCE RID: 11214 RVA: 0x00061690 File Offset: 0x0005F890
		internal string StdOutText
		{
			get
			{
				if (this.stdOutStream == null)
				{
					return string.Empty;
				}
				this.stdOutStream.Seek(0L, SeekOrigin.Begin);
				StreamReader streamReader = new StreamReader(this.stdOutStream);
				return streamReader.ReadToEnd();
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x06002BCF RID: 11215 RVA: 0x000616CC File Offset: 0x0005F8CC
		internal string StdErrText
		{
			get
			{
				if (this.stdErrStream == null)
				{
					return string.Empty;
				}
				this.stdErrStream.Seek(0L, SeekOrigin.Begin);
				StreamReader streamReader = new StreamReader(this.stdErrStream);
				return streamReader.ReadToEnd();
			}
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x00061708 File Offset: 0x0005F908
		internal bool Start(string pathName, bool paused, bool serviceListening, SafeJobHandle jobObject)
		{
			string text = null;
			this.stopHandle = this.CreateNamedSemaphore("Global\\ExchangeStopKey-", out text);
			if (this.stopHandle == null)
			{
				this.diag.TraceError(0L, "Failed to create a named semaphore: Stop Key");
				return false;
			}
			string text2 = null;
			this.hangHandle = this.CreateNamedSemaphore("Global\\ExchangeHangKey-", out text2);
			if (this.hangHandle == null)
			{
				this.diag.TraceError(0L, "Failed to create a named semaphore: Hang Key");
				return false;
			}
			string text3 = null;
			this.resetHandle = this.CreateNamedSemaphore("Global\\ExchangeResetKey-", out text3);
			if (this.resetHandle == null)
			{
				this.diag.TraceError(0L, "Failed to create a named semaphore: Reset Key");
				return false;
			}
			string text4 = null;
			this.readyHandle = this.CreateNamedSemaphore("Global\\ExchangeReadyKey-", out text4);
			if (this.readyHandle == null)
			{
				this.diag.TraceError(0L, "Failed to create a named semaphore: Ready Key");
				return false;
			}
			SafeFileHandle handle;
			SafeFileHandle safeFileHandle;
			if (!PipeStream.TryCreatePipeHandles(out handle, out safeFileHandle, this.diag))
			{
				throw new InvalidOperationException("Pipe for service-worker communication cannot be created.");
			}
			this.controlPipeStream = new PipeStream(handle, FileAccess.Write, false);
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.FileName = pathName;
			processStartInfo.CreateNoWindow = true;
			processStartInfo.UseShellExecute = false;
			processStartInfo.RedirectStandardError = true;
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.Arguments = string.Format("-pipe:{0} -stopkey:{1} -resetkey:{2} -readykey:{3} -hangkey:{4}", new object[]
			{
				safeFileHandle.DangerousGetHandle().ToInt64(),
				text,
				text3,
				text4,
				text2
			});
			if (!this.workerIsActive)
			{
				ProcessStartInfo processStartInfo2 = processStartInfo;
				processStartInfo2.Arguments += " -passive";
			}
			if (paused)
			{
				ProcessStartInfo processStartInfo3 = processStartInfo;
				processStartInfo3.Arguments += " -paused";
			}
			if (!serviceListening)
			{
				ProcessStartInfo processStartInfo4 = processStartInfo;
				processStartInfo4.Arguments += " -workerListening";
			}
			this.diag.TraceDebug<string>(0L, "Worker commandline: {0}", processStartInfo.Arguments);
			lock (this)
			{
				this.process = new Process();
				this.process.StartInfo = processStartInfo;
				this.process.EnableRaisingEvents = true;
				this.process.Exited += this.OnExited;
				this.startTime = DateTime.UtcNow;
				this.monitorResetThread = new Thread(new ThreadStart(this.MonitorResetHandle));
				this.monitorResetThread.Start();
				Thread thread = new Thread(new ThreadStart(this.WaitForProcessReady));
				thread.Start();
				this.diag.TraceDebug(0L, "About to start the worker process");
				bool flag2 = false;
				try
				{
					flag2 = this.process.Start();
					Thread.Sleep(1000);
				}
				catch (Win32Exception ex)
				{
					this.diag.TraceError<int, int>(0L, "Win32Exception while trying to start the worker process (error code={0}, native error code={1})", ex.ErrorCode, ex.NativeErrorCode);
				}
				if (!flag2)
				{
					this.diag.TraceError(0L, "Failed to start the worker process");
					return false;
				}
				this.pid = this.process.Id;
				this.diag.TraceDebug<int>(0L, "Started the worker process (pid={0})", this.pid);
				if (!jobObject.IsInvalid && !jobObject.Add(this.process))
				{
					this.diag.TraceError(0L, "AssignProcessToJobObject() failed");
					return false;
				}
				this.stdErrBuffer = new byte[4096];
				this.stdErrStream = new MemoryStream(4096);
				this.stdOutBuffer = new byte[4096];
				this.stdOutStream = new MemoryStream(4096);
				this.PendStdXxxRead(this.process.StandardError);
				this.PendStdXxxRead(this.process.StandardOutput);
				safeFileHandle.Close();
			}
			return true;
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x00061AF0 File Offset: 0x0005FCF0
		internal void Stop()
		{
			if (!this.stopping)
			{
				this.stopping = true;
				this.diag.TraceDebug<int>(0L, "Stop worker instance (pid={0})", this.Pid);
				PipeStream pipeStream = this.controlPipeStream;
				if (pipeStream != null)
				{
					try
					{
						pipeStream.Flush();
						pipeStream.Dispose();
						this.controlPipeStream = null;
					}
					catch (IOException)
					{
					}
					catch (ObjectDisposedException)
					{
					}
				}
				this.SignalStop();
			}
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x00061B6C File Offset: 0x0005FD6C
		internal void CloseProcess(out string hasExited)
		{
			hasExited = "No value";
			lock (this)
			{
				if (this.process != null)
				{
					this.diag.TraceDebug<int>(0L, "Closing process (pid={0})", this.Pid);
					try
					{
						hasExited = this.process.HasExited.ToString();
					}
					catch (Exception ex)
					{
						hasExited = ex.ToString();
					}
					this.process.Close();
					this.process = null;
				}
			}
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x00061C08 File Offset: 0x0005FE08
		internal void SignalStop()
		{
			if (this.stopHandle != null)
			{
				try
				{
					this.stopHandle.Release();
					this.diag.TraceDebug<int>(0L, "Signaled stop to worker instance (pid={0})", this.Pid);
					return;
				}
				catch (SemaphoreFullException)
				{
					this.diag.TraceDebug<int>(0L, "Worker instance (pid={0}) stopHandle already signaled", this.Pid);
					return;
				}
			}
			this.diag.TraceDebug<int>(0L, "Ignored to signal stop to worker instance (pid={0}) as the stop handle is null", this.Pid);
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x00061C88 File Offset: 0x0005FE88
		internal void SignalHang()
		{
			Semaphore semaphore = Interlocked.Exchange<Semaphore>(ref this.hangHandle, null);
			if (semaphore != null)
			{
				semaphore.Release();
				this.diag.TraceDebug<int>(0L, "Signaled hang to worker instance (pid={0})", this.Pid);
			}
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x00061CC4 File Offset: 0x0005FEC4
		internal bool SendMessage(byte[] buffer, int offset, int count)
		{
			bool flag = false;
			PipeStream pipeStream = this.controlPipeStream;
			if (this.pipeConnected && pipeStream != null)
			{
				try
				{
					pipeStream.Write(buffer, offset, count);
					flag = true;
					this.diag.TraceDebug(0L, "Message sent to worker instance");
				}
				catch (IOException)
				{
				}
				catch (ObjectDisposedException)
				{
				}
				if (!flag)
				{
					this.pipeConnected = false;
					try
					{
						pipeStream.Flush();
						pipeStream.Close();
						this.controlPipeStream = null;
					}
					catch (IOException)
					{
					}
					catch (ObjectDisposedException)
					{
					}
				}
			}
			return flag;
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x00061D64 File Offset: 0x0005FF64
		private void WaitForProcessReady()
		{
			this.diag.TraceDebug<int>(0L, "Wait for process (pid={0}) to signal ready", this.pid);
			this.readyHandle.WaitOne();
			this.signaledReady = true;
			Semaphore semaphore = Interlocked.Exchange<Semaphore>(ref this.readyHandle, null);
			if (semaphore != null)
			{
				semaphore.Close();
			}
			if (this.stopping)
			{
				return;
			}
			this.diag.TracePfd<int, int>(0L, "PFD ETS {0} Process (pid={1}) signaled ready", 25111, this.pid);
			this.pipeConnected = true;
			if (this.workerContactedDelegate != null)
			{
				this.workerContactedDelegate(this);
			}
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x00061DF8 File Offset: 0x0005FFF8
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

		// Token: 0x06002BD8 RID: 11224 RVA: 0x00061E48 File Offset: 0x00060048
		private void CallWorkerExitedDelegate(bool resetRequested)
		{
			PipeStream pipeStream = this.controlPipeStream;
			if (pipeStream != null)
			{
				try
				{
					pipeStream.Flush();
					pipeStream.Dispose();
				}
				catch (IOException)
				{
				}
				catch (ObjectDisposedException)
				{
				}
			}
			this.workerExitedDelegate(this, resetRequested);
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x00061E9C File Offset: 0x0006009C
		private void OnExited(object sender, EventArgs e)
		{
			this.diag.TraceDebug<int>(0L, "Process {0} exited", this.pid);
			this.exited = true;
			this.stopping = true;
			this.CallWorkerExitedDelegate(false);
			Semaphore semaphore = Interlocked.Exchange<Semaphore>(ref this.readyHandle, null);
			if (semaphore != null)
			{
				semaphore.Release();
			}
			semaphore = Interlocked.Exchange<Semaphore>(ref this.resetHandle, null);
			if (semaphore != null)
			{
				semaphore.Release();
			}
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x00061F04 File Offset: 0x00060104
		private void MonitorResetHandle()
		{
			this.resetHandle.WaitOne();
			if (this.stopping)
			{
				return;
			}
			this.diag.TraceDebug<int>(0L, "Process {0} requested reset", this.pid);
			this.resetRequested = true;
			this.stopping = true;
			Semaphore semaphore = Interlocked.Exchange<Semaphore>(ref this.resetHandle, null);
			if (semaphore != null)
			{
				semaphore.Close();
			}
			this.CallWorkerExitedDelegate(true);
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x00061F6C File Offset: 0x0006016C
		private void PendStdXxxRead(StreamReader streamReader)
		{
			try
			{
				if (streamReader == this.process.StandardError)
				{
					streamReader.BaseStream.BeginRead(this.stdErrBuffer, 0, this.stdErrBuffer.Length, new AsyncCallback(this.OnAsyncReadComplete), streamReader);
				}
				else if (streamReader == this.process.StandardOutput)
				{
					streamReader.BaseStream.BeginRead(this.stdOutBuffer, 0, this.stdOutBuffer.Length, new AsyncCallback(this.OnAsyncReadComplete), streamReader);
				}
			}
			catch (IOException)
			{
				this.diag.TraceDebug(0L, "PendStdXxxRead failed: System.IO.IOException");
			}
			catch (InvalidOperationException)
			{
				this.diag.TraceDebug(0L, "PendStdXxxRead failed: InvalidOperationException");
			}
			catch (OperationCanceledException)
			{
				this.diag.TraceDebug(0L, "PendStdXxxRead failed: OperationCanceledException");
			}
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x00062050 File Offset: 0x00060250
		private void OnAsyncReadComplete(IAsyncResult res)
		{
			int num = 0;
			bool flag = false;
			StreamReader streamReader = res.AsyncState as StreamReader;
			try
			{
				num = streamReader.BaseStream.EndRead(res);
			}
			catch (IOException)
			{
				flag = true;
			}
			catch (InvalidOperationException)
			{
				flag = true;
			}
			catch (OperationCanceledException)
			{
				flag = true;
			}
			if (flag || num == 0)
			{
				return;
			}
			lock (this)
			{
				if (this.process != null)
				{
					bool flag3 = false;
					bool flag4 = false;
					try
					{
						flag3 = (streamReader == this.process.StandardError);
						flag4 = (streamReader == this.process.StandardOutput);
					}
					catch (InvalidOperationException)
					{
						return;
					}
					if (flag3)
					{
						if (this.stdErrStream.Position > (long)(16 * this.stdErrBuffer.Length))
						{
							this.stdErrStream.Seek(0L, SeekOrigin.Begin);
						}
						this.stdErrStream.Write(this.stdErrBuffer, 0, num);
						this.stdErrStream.SetLength((long)num);
						string stdErrText = this.StdErrText;
						this.diag.TraceDebug<string>(0L, "StdErr: {0}", stdErrText);
						this.stdErrStream.Seek(0L, SeekOrigin.Begin);
						this.PendStdXxxRead(streamReader);
					}
					else if (flag4)
					{
						if (this.stdOutStream.Position > (long)(16 * this.stdOutBuffer.Length))
						{
							this.stdOutStream.Seek(0L, SeekOrigin.Begin);
						}
						this.stdOutStream.Write(this.stdOutBuffer, 0, num);
						this.stdOutStream.SetLength((long)num);
						string stdOutText = this.StdOutText;
						this.diag.TraceDebug<string>(0L, "StdOut: {0}", stdOutText);
						this.stdOutStream.Seek(0L, SeekOrigin.Begin);
						this.PendStdXxxRead(streamReader);
					}
				}
			}
		}

		// Token: 0x04002612 RID: 9746
		private const long TraceId = 0L;

		// Token: 0x04002613 RID: 9747
		private const int MaxConnectAttempts = 5;

		// Token: 0x04002614 RID: 9748
		private const int ConnectAttemptInterval = 1000;

		// Token: 0x04002615 RID: 9749
		private Process process;

		// Token: 0x04002616 RID: 9750
		private int pid;

		// Token: 0x04002617 RID: 9751
		private DateTime startTime;

		// Token: 0x04002618 RID: 9752
		private int thrashCrashCount;

		// Token: 0x04002619 RID: 9753
		private Thread monitorResetThread;

		// Token: 0x0400261A RID: 9754
		private Semaphore stopHandle;

		// Token: 0x0400261B RID: 9755
		private Semaphore hangHandle;

		// Token: 0x0400261C RID: 9756
		private Semaphore resetHandle;

		// Token: 0x0400261D RID: 9757
		private Semaphore readyHandle;

		// Token: 0x0400261E RID: 9758
		private PipeStream controlPipeStream;

		// Token: 0x0400261F RID: 9759
		private bool workerIsActive;

		// Token: 0x04002620 RID: 9760
		private bool exited;

		// Token: 0x04002621 RID: 9761
		private bool resetRequested;

		// Token: 0x04002622 RID: 9762
		private bool signaledReady;

		// Token: 0x04002623 RID: 9763
		private bool pipeConnected;

		// Token: 0x04002624 RID: 9764
		private MemoryStream stdErrStream;

		// Token: 0x04002625 RID: 9765
		private MemoryStream stdOutStream;

		// Token: 0x04002626 RID: 9766
		private byte[] stdErrBuffer;

		// Token: 0x04002627 RID: 9767
		private byte[] stdOutBuffer;

		// Token: 0x04002628 RID: 9768
		private Microsoft.Exchange.Diagnostics.Trace diag;

		// Token: 0x04002629 RID: 9769
		private bool stopping;

		// Token: 0x0400262A RID: 9770
		private WorkerInstance.WorkerContacted workerContactedDelegate;

		// Token: 0x0400262B RID: 9771
		private WorkerInstance.WorkerExited workerExitedDelegate;

		// Token: 0x0200081B RID: 2075
		// (Invoke) Token: 0x06002BDE RID: 11230
		internal delegate void WorkerContacted(WorkerInstance workerInstance);

		// Token: 0x0200081C RID: 2076
		// (Invoke) Token: 0x06002BE2 RID: 11234
		internal delegate void WorkerExited(WorkerInstance workerInstance, bool resetRequested);
	}
}
