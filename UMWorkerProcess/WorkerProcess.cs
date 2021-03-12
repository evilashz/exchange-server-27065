using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Security.Cryptography;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Exchange.UM.UMCore.Exceptions;
using Microsoft.Exchange.UM.UMWorkerProcess.Exceptions;
using Microsoft.Rtc.Signaling;

namespace Microsoft.Exchange.UM.UMWorkerProcess
{
	// Token: 0x02000003 RID: 3
	public sealed class WorkerProcess
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000023C4 File Offset: 0x000005C4
		[SecurityCritical]
		[HandleProcessCorruptedStateExceptions]
		internal static void MainUnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs eventArgs)
		{
			int num = Interlocked.Exchange(ref WorkerProcess.main.busyUnhandledException, 1);
			if (num == 1)
			{
				return;
			}
			object exceptionObject = eventArgs.ExceptionObject;
			CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "Unhandled Exception Received --- {0}", new object[]
			{
				eventArgs.IsTerminating ? "Terminating CLR" : "NOT Terminating CLR"
			});
			WorkerProcess.ProcessException(exceptionObject, false, true, true);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000242C File Offset: 0x0000062C
		internal static void ProcessException(object exceptionObject, bool fatalException, bool unhandled, bool sendWatsonReport)
		{
			if (fatalException)
			{
				Utils.SafelyReleaseAndCloseNamedSemaphore(WorkerProcess.main.fatalHandle);
				WorkerProcess.main.fatalHandle = null;
			}
			else
			{
				UmServiceGlobals.ArePerfCountersEnabled = false;
				Utils.SafelyReleaseAndCloseNamedSemaphore(WorkerProcess.main.resetHandle);
				WorkerProcess.main.resetHandle = null;
			}
			Exception ex = exceptionObject as Exception;
			if (ex != null && unhandled)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessUnhandledException, null, new object[]
				{
					ex.ToString()
				});
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "Exception Information {0}", new object[]
				{
					ex.ToString()
				});
				CallIdTracer.Flush();
			}
			if (ex != null && sendWatsonReport)
			{
				ExceptionHandling.SendWatsonWithExtraData(ex, fatalException);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002534 File Offset: 0x00000734
		internal static void Main(string[] args)
		{
			ProcessLog.WriteLine("WorkerProcess::Main", new object[0]);
			if (Privileges.RemoveAllExcept(new string[]
			{
				"SeAuditPrivilege",
				"SeChangeNotifyPrivilege",
				"SeCreateGlobalPrivilege",
				"SeIncreaseQuotaPrivilege",
				"SeAssignPrimaryTokenPrivilege"
			}) == 0)
			{
				AppDomain.CurrentDomain.UnhandledException += WorkerProcess.MainUnhandledExceptionHandler;
				WorkerProcess.main = new WorkerProcess();
				WorkerProcess.main.Run(args);
				WorkerProcess.killTimer = new Timer(delegate(object state)
				{
					CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "Current process has not terminated as requested - watson and kill process", new object[0]);
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_WatsoningDueToWorkerProcessNotTerminating, null, new object[0]);
					ExceptionHandling.SendWatsonWithExtraData(new WatsoningDueToWorkerProcessNotTerminating(), true);
					CallIdTracer.Flush();
					Utils.KillThisProcess();
				}, null, 60000L, -1L);
			}
			CallIdTracer.Flush();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002648 File Offset: 0x00000848
		internal void Retire(bool requestWatson)
		{
			ProcessLog.WriteLine("WorkerProcess::Retire", new object[0]);
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Asked to retire", new object[0]);
			if (this.isStopped)
			{
				return;
			}
			if (!this.retireHandle.Reset())
			{
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "Retire: failed to reset the event for the main thread to wait on", new object[0]);
				Utils.KillThisProcess();
			}
			UmServiceGlobals.UmRetire(delegate
			{
				if (requestWatson)
				{
					ExceptionHandling.SendWatsonWithExtraData(new WatsoningDueToRecycling(), false);
				}
				if (!this.retireHandle.Set())
				{
					CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "Retire: failed to notify that the final call has ended", new object[0]);
					Utils.KillThisProcess();
				}
			});
			if (!this.ReleaseStopHandle())
			{
				Utils.KillThisProcess();
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000026E8 File Offset: 0x000008E8
		internal void Activate()
		{
			ProcessLog.WriteLine("WorkerProcess::Activate", new object[0]);
			this.isPassive = false;
			WorkerProcess.Initialize();
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Asked to activate", new object[0]);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002724 File Offset: 0x00000924
		private static void NewConnection(IAsyncResult asyncResult)
		{
			WorkerProcess workerProcess = (WorkerProcess)asyncResult.AsyncState;
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "In NewConnection in workerprocess", new object[0]);
			Socket socket = workerProcess.listenSocket;
			Socket socket2 = null;
			if (socket == null)
			{
				return;
			}
			try
			{
				socket2 = socket.EndAccept(asyncResult);
			}
			catch (SocketException)
			{
				return;
			}
			catch (ObjectDisposedException)
			{
				return;
			}
			if (!((IPEndPoint)socket2.LocalEndPoint).Address.Equals(((IPEndPoint)socket2.RemoteEndPoint).Address) || (workerProcess.controlObject != null && workerProcess.controlObject.IsConnected))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Diconnecting NewConnection in workerprocess", new object[0]);
				socket2.Close();
				return;
			}
			workerProcess.controlObject = new ControlObject(socket2, workerProcess);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000027FC File Offset: 0x000009FC
		private static void Initialize()
		{
			ProcessLog.WriteLine("WorkerProcess::Initialize", new object[0]);
			UmServiceGlobals.StartUMComponents(StartupStage.WPActivation);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002814 File Offset: 0x00000A14
		private static void Stop()
		{
			ProcessLog.WriteLine("WorkerProcess::Stop", new object[0]);
			try
			{
				UmServiceGlobals.UmUninitialize();
				ProcessLog.WriteLine("Stop: ServiceGlobals uninitialized.", new object[0]);
			}
			catch (Exception ex)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStopFailure, null, new object[]
				{
					ex.ToString()
				});
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "UmUnInitialize failed, error={0}", new object[]
				{
					ex.ToString()
				});
				throw;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "UM WorkerProcess succesfully stopped", new object[0]);
			ProcessLog.WriteLine("Stop: Success.", new object[0]);
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStopSuccess, null, new object[0]);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000028E8 File Offset: 0x00000AE8
		private void Run(string[] args)
		{
			ProcessLog.WriteLine("WorkerProcess::Run", new object[0]);
			if (InstrumentationCollector.Start(new WPInstrumentationStrategy()))
			{
				ProcessLog.WriteLine("Run: Started InstrumentationCollector", new object[0]);
			}
			int num = 0;
			int num2 = 0;
			bool flag = false;
			string name = null;
			string name2 = null;
			string name3 = null;
			string name4 = null;
			string fullName = Assembly.GetExecutingAssembly().FullName;
			CallIdTracer.TracePfd(ExTraceGlobals.ServiceStartTracer, 0, "PFD UMS {0} - Starting UMWorkerProcess: {1}", new object[]
			{
				12346,
				fullName
			});
			foreach (string text in args)
			{
				if (text.StartsWith("-stopkey:", StringComparison.InvariantCulture))
				{
					name = text.Remove(0, "-stopkey:".Length);
				}
				else if (text.StartsWith("-resetkey:", StringComparison.InvariantCulture))
				{
					name2 = text.Remove(0, "-resetkey:".Length);
				}
				else if (text.StartsWith("-readykey:", StringComparison.InvariantCulture))
				{
					name3 = text.Remove(0, "-readykey:".Length);
				}
				else if (text.StartsWith("-fatalkey:", StringComparison.InvariantCulture))
				{
					name4 = text.Remove(0, "-fatalkey:".Length);
				}
				else if (text.StartsWith("-port:", StringComparison.InvariantCulture))
				{
					num = int.Parse(text.Remove(0, "-port:".Length), CultureInfo.InvariantCulture);
				}
				else if (text.StartsWith("-passive", StringComparison.InvariantCulture))
				{
					flag = true;
				}
				else if (text.StartsWith("-sipport:", StringComparison.InvariantCulture))
				{
					num2 = int.Parse(text.Remove(0, "-sipport:".Length), CultureInfo.InvariantCulture);
					this.sipport = num2;
				}
				else if (text.StartsWith("-tempdir:", StringComparison.InvariantCulture))
				{
					string text2 = Path.Combine(Utils.GetExchangeDirectory(), text.Remove(0, "-tempdir:".Length));
					Utils.CleanUMTempDirectory(text2);
					Utils.UMTempPath = Path.Combine(text2, Guid.NewGuid().ToString());
					try
					{
						Directory.CreateDirectory(Utils.UMTempPath);
						this.activeWPFile = File.Create(Path.Combine(Utils.UMTempPath, "wp.active"), 1, FileOptions.DeleteOnClose);
					}
					catch (IOException ex)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "Could not create the wp.active file. {0}", new object[]
						{
							ex
						});
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
						{
							ex.Message
						});
						Utils.KillThisProcess();
					}
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "arg={0}, temppath={1}", new object[]
					{
						text,
						Utils.UMTempPath
					});
				}
				else
				{
					if (text.StartsWith("-startupMode:", StringComparison.InvariantCulture))
					{
						try
						{
							UmServiceGlobals.StartupMode = (UMStartupMode)Enum.Parse(typeof(UMStartupMode), text.Remove(0, "-startupMode:".Length), true);
							goto IL_465;
						}
						catch (ArgumentException ex2)
						{
							UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
							{
								Strings.WPInvalidArguments(ex2.Message)
							});
							CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "Exception while parsing the startupMode parameter " + ex2.ToString(), new object[0]);
							Utils.KillThisProcess();
							goto IL_465;
						}
					}
					if (text.StartsWith("-thumbprint:", StringComparison.InvariantCulture))
					{
						string text3 = text.Remove(0, "-thumbprint:".Length);
						try
						{
							CertificateUtils.UMCertificate = CertificateUtils.FindCertByThumbprint(text3);
						}
						catch (CryptographicException ex3)
						{
							UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
							{
								ex3.Message
							});
						}
						catch (SecurityException ex4)
						{
							UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
							{
								ex4.Message
							});
						}
						if (CertificateUtils.UMCertificate == null)
						{
							UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
							{
								Strings.WPUnableToFindCertificate(text3)
							});
							Utils.KillThisProcess();
						}
					}
					else if (text.StartsWith("-perfenabled:", StringComparison.InvariantCulture))
					{
						if (int.Parse(text.Remove(0, "-perfenabled:".Length), CultureInfo.InvariantCulture) == 0)
						{
							UmServiceGlobals.ArePerfCountersEnabled = false;
						}
						else
						{
							UmServiceGlobals.ArePerfCountersEnabled = true;
							AvailabilityCounters.TotalWorkerProcessCallCount.RawValue = 0L;
						}
					}
				}
				IL_465:;
			}
			this.isPassive = flag;
			if ((this.resetHandle = this.OpenSemaphore(name2)) == null || (this.stopHandle = this.OpenSemaphore(name)) == null || (this.fatalHandle = this.OpenSemaphore(name4)) == null || (this.readyHandle = this.OpenSemaphore(name3)) == null)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
				{
					Strings.WPSemaphoreOpenFailure
				});
				ProcessLog.WriteLine("Run: Failed to open semaphores.", new object[0]);
				Utils.KillThisProcess();
			}
			if (num <= 0 || num > 65535)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
				{
					Strings.WPInvalidControlPort(num, 0, 65535)
				});
				ProcessLog.WriteLine("Run: Failed to establish a control port.", new object[0]);
				Utils.KillThisProcess();
			}
			if (num2 <= 0 || num2 > 65535)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
				{
					Strings.WPInvalidSipPort(num2, 0, 65535)
				});
				ProcessLog.WriteLine("Run: Failed to establish a sip port.", new object[0]);
				Utils.KillThisProcess();
			}
			IPEndPoint ipendPoint = new IPEndPoint(Utils.GetLoopbackControlIPAddress(), num);
			CallIdTracer.TracePfd(ExTraceGlobals.ServiceStartTracer, 0, "PFD UMS {0} - Allocating socket for new connections.", new object[]
			{
				8250
			});
			try
			{
				this.listenSocket = new Socket(ipendPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				this.listenSocket.Bind(ipendPoint);
				this.listenSocket.Listen(int.MaxValue);
				this.listenSocket.BeginAccept(WorkerProcess.newConnection, this);
				ProcessLog.WriteLine("Run: Accepting control socket connections.", new object[0]);
			}
			catch (SocketException ex5)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
				{
					ex5.Message
				});
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "New Socket failed, error={0}", new object[]
				{
					ex5.ToString()
				});
				ProcessLog.WriteLine("Run: Failed to accept control socket connections.", new object[0]);
				Utils.KillThisProcess();
			}
			bool flag2 = false;
			try
			{
				Globals.InitializeMultiPerfCounterInstance("UM");
				ProcessLog.WriteLine("Run: Initialized multi-performance counter instance for UM.", new object[0]);
				UmServiceGlobals.UmInitialize(num2);
				ProcessLog.WriteLine("Run: Initialized ServiceGlobals.", new object[0]);
				if (!this.isPassive)
				{
					WorkerProcess.Initialize();
				}
				flag2 = true;
			}
			catch (ConfigurationException ex6)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
				{
					ex6.Message
				});
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "UmInitialize failed, error={0}", new object[]
				{
					ex6
				});
				WorkerProcess.ProcessException(ex6, true, false, false);
			}
			catch (UnableToInitializeResourceException ex7)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
				{
					ex7.Message
				});
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "UmInitialize failed, error={0}", new object[]
				{
					ex7
				});
				WorkerProcess.ProcessException(ex7, false, false, false);
			}
			catch (XmlSchemaValidationException ex8)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMInvalidSchema, null, new object[]
				{
					ex8.SourceUri,
					ex8.LineNumber,
					ex8.Message
				});
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "UmInitialize failed, error={0}", new object[]
				{
					ex8.ToString()
				});
				WorkerProcess.ProcessException(ex8, true, false, true);
			}
			catch (XmlException ex9)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMInvalidSchema, null, new object[]
				{
					ex9.SourceUri,
					ex9.LineNumber,
					ex9.Message
				});
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "UmInitialize failed, error={0}", new object[]
				{
					ex9.ToString()
				});
				WorkerProcess.ProcessException(ex9, true, false, true);
			}
			catch (DirectoryNotFoundException ex10)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
				{
					Strings.WPDirectoryNotFound(ex10.Message)
				});
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "UmInitialize failed, error={0}", new object[]
				{
					ex10.ToString()
				});
				WorkerProcess.ProcessException(ex10, true, false, true);
			}
			catch (FileNotFoundException ex11)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
				{
					Strings.WPFileNotFound(ex11.FileName)
				});
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "UmInitialize failed, error={0}", new object[]
				{
					ex11.ToString()
				});
				WorkerProcess.ProcessException(ex11, true, false, true);
			}
			catch (ResourceDirectoryNotFoundException ex12)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
				{
					ex12.Message
				});
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "UmInitialize failed, error={0}", new object[]
				{
					ex12.ToString()
				});
				WorkerProcess.ProcessException(ex12, true, false, true);
			}
			catch (FsmConfigurationException ex13)
			{
				if (ex13.InnerException != null)
				{
					if (ex13.InnerException is FileNotFoundException)
					{
						string fileName = ((FileNotFoundException)ex13.InnerException).FileName;
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
						{
							Strings.WPFileNotFound(fileName)
						});
					}
					else
					{
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
						{
							ex13.Message
						});
					}
				}
				else
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
					{
						ex13.Message
					});
				}
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "UmInitialize failed, error={0}", new object[]
				{
					ex13.ToString()
				});
				WorkerProcess.ProcessException(ex13, true, false, true);
			}
			catch (ExchangeServerNotFoundException ex14)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
				{
					ex14.Message
				});
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "UmInitialize failed, error={0}", new object[]
				{
					ex14
				});
				WorkerProcess.ProcessException(ex14, false, false, false);
			}
			catch (IOException ex15)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "Could not initialize directory. {0}", new object[]
				{
					ex15
				});
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
				{
					ex15.Message
				});
				WorkerProcess.ProcessException(ex15, true, false, true);
			}
			catch (ADTransientException ex16)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
				{
					ex16.Message
				});
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "UmInitialize failed, error={0}", new object[]
				{
					ex16.ToString()
				});
				WorkerProcess.ProcessException(ex16, false, false, false);
			}
			catch (ConnectionFailureException ex17)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UnableToGetSocket, null, new object[]
				{
					this.sipport
				});
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "UmInitialize failed, error={0}", new object[]
				{
					ex17.ToString()
				});
				WorkerProcess.ProcessException(ex17, false, false, false);
			}
			catch (TlsFailureException ex18)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
				{
					ex18.Message
				});
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "UmInitialize failed, error={0}", new object[]
				{
					ex18.ToString()
				});
				WorkerProcess.ProcessException(ex18, true, false, false);
			}
			catch (AcmException ex19)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "Could not initialize ACM. {0}", new object[]
				{
					ex19
				});
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
				{
					ex19.Message
				});
				WorkerProcess.ProcessException(ex19, true, false, true);
			}
			finally
			{
				if (!flag2)
				{
					ProcessLog.WriteLine("Initialization failed. Killing the process", new object[0]);
					Utils.KillThisProcess();
				}
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Service started as {0}", new object[]
			{
				flag ? "passive" : "active"
			});
			CallIdTracer.TracePfd(ExTraceGlobals.ServiceStartTracer, 0, "PFD UMS {0} - Service startup completed as {1}.", new object[]
			{
				14906,
				flag ? "passive" : "active"
			});
			string text4 = (UmServiceGlobals.StartupMode == UMStartupMode.Dual) ? Strings.Ports(this.sipport, this.sipport + 1).ToString() : this.sipport.ToString();
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartSuccess, null, new object[]
			{
				UmServiceGlobals.StartupMode,
				text4
			});
			ProcessLog.WriteLine("Run: Success.", new object[0]);
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Signal UM service that worker process is ready", new object[0]);
			Utils.SafelyReleaseAndCloseNamedSemaphore(this.readyHandle);
			this.readyHandle = null;
			Semaphore semaphore = Interlocked.Exchange<Semaphore>(ref this.fatalHandle, null);
			if (semaphore != null)
			{
				semaphore.Close();
			}
			this.retireHandle = new ManualResetEvent(true);
			ProcessLog.WriteLine("Run: WaitForStop.", new object[0]);
			if (this.stopHandle != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Wait for shutdown signal to exit", new object[0]);
				this.stopHandle.WaitOne();
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Received a signal to shutdown", new object[0]);
			ProcessLog.WriteLine("Run: Received shutdown event.", new object[0]);
			Semaphore semaphore2 = Interlocked.Exchange<Semaphore>(ref this.stopHandle, null);
			if (semaphore2 != null)
			{
				semaphore2.Close();
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "shutdown control object", new object[0]);
			this.isStopped = true;
			ControlObject controlObject = this.controlObject;
			if (controlObject != null)
			{
				this.controlObject.Shutdown();
			}
			Socket socket = this.listenSocket;
			if (socket != null)
			{
				this.listenSocket = null;
				socket.Close();
			}
			if (InstrumentationCollector.Stop())
			{
				ProcessLog.WriteLine("Run: Stopped InstrumentationCollector", new object[0]);
			}
			ProcessLog.WriteLine("Run: Waiting for retire to complete.", new object[0]);
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Wait for retire to complete", new object[0]);
			this.retireHandle.WaitOne();
			WorkerProcess.Stop();
			ProcessLog.WriteLine("Run: Stop completed.", new object[0]);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00003A5C File Offset: 0x00001C5C
		private Semaphore OpenSemaphore(string name)
		{
			Semaphore result = null;
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "Trying to open semaphore with name {0}", new object[]
			{
				name
			});
			if (name != null)
			{
				try
				{
					result = Semaphore.OpenExisting(name);
				}
				catch (WaitHandleCannotBeOpenedException ex)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
					{
						this.sipport,
						ex.ToString()
					});
					CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "Semaphore {0} couldnt be opened, error={1}", new object[]
					{
						name,
						ex.ToString()
					});
				}
				catch (UnauthorizedAccessException ex2)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessStartFailure, null, new object[]
					{
						this.sipport,
						ex2.ToString()
					});
					CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "Semaphore {0} couldnt be opened, error={1}", new object[]
					{
						name,
						ex2.ToString()
					});
				}
			}
			return result;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00003B88 File Offset: 0x00001D88
		private bool ReleaseStopHandle()
		{
			Semaphore semaphore = Interlocked.Exchange<Semaphore>(ref this.stopHandle, null);
			if (semaphore != null)
			{
				try
				{
					semaphore.Release();
				}
				catch (ArgumentOutOfRangeException ex)
				{
					CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "ReleaseStopHandle: error={0}", new object[]
					{
						ex
					});
					return false;
				}
				catch (SemaphoreFullException ex2)
				{
					CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "ReleaseStopHandle: error={0}", new object[]
					{
						ex2
					});
					return false;
				}
				return true;
			}
			return true;
		}

		// Token: 0x04000003 RID: 3
		private const long KillTimerPeriodMilliseconds = 60000L;

		// Token: 0x04000004 RID: 4
		private static readonly AsyncCallback newConnection = new AsyncCallback(WorkerProcess.NewConnection);

		// Token: 0x04000005 RID: 5
		private static WorkerProcess main;

		// Token: 0x04000006 RID: 6
		private static Timer killTimer;

		// Token: 0x04000007 RID: 7
		private bool isPassive;

		// Token: 0x04000008 RID: 8
		private bool isStopped;

		// Token: 0x04000009 RID: 9
		private int busyUnhandledException;

		// Token: 0x0400000A RID: 10
		private int sipport;

		// Token: 0x0400000B RID: 11
		private Semaphore resetHandle;

		// Token: 0x0400000C RID: 12
		private Semaphore stopHandle;

		// Token: 0x0400000D RID: 13
		private Semaphore readyHandle;

		// Token: 0x0400000E RID: 14
		private Semaphore fatalHandle;

		// Token: 0x0400000F RID: 15
		private ManualResetEvent retireHandle;

		// Token: 0x04000010 RID: 16
		private ControlObject controlObject;

		// Token: 0x04000011 RID: 17
		private Socket listenSocket;

		// Token: 0x04000012 RID: 18
		private FileStream activeWPFile;
	}
}
