using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.Nspi;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200050C RID: 1292
	internal class NspiRpcClientConnection : IDisposeTrackable, IDisposable
	{
		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x06003929 RID: 14633 RVA: 0x000DC8F8 File Offset: 0x000DAAF8
		private static NspiRpcClientConnectionPerformanceCountersInstance PerfCounterInstance
		{
			get
			{
				if (NspiRpcClientConnection.perfCounterInstance == null)
				{
					lock (NspiRpcClientConnection.perfCounterInstanceInitLock)
					{
						if (NspiRpcClientConnection.perfCounterInstance == null)
						{
							using (Process currentProcess = Process.GetCurrentProcess())
							{
								string instanceName = string.Format("{0} ({1})", currentProcess.MainModule.ModuleName, currentProcess.Id);
								NspiRpcClientConnection.perfCounterInstance = NspiRpcClientConnectionPerformanceCounters.GetInstance(instanceName);
							}
						}
					}
				}
				return NspiRpcClientConnection.perfCounterInstance;
			}
		}

		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x0600392A RID: 14634 RVA: 0x000DC990 File Offset: 0x000DAB90
		public NspiRpcClient RpcClient
		{
			get
			{
				return this.nspiRpcClient;
			}
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x000DC998 File Offset: 0x000DAB98
		private NspiRpcClientConnection()
		{
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x000DC9AC File Offset: 0x000DABAC
		public static NspiRpcClientConnection GetNspiRpcClientConnection(string domainController)
		{
			NspiRpcClientConnection nspiRpcClientConnection = new NspiRpcClientConnection();
			int hashCode = nspiRpcClientConnection.GetHashCode();
			int num = 0;
			while (!nspiRpcClientConnection.bound)
			{
				try
				{
					NspiRpcClientConnection.TraceDebug(hashCode, "Creating NspiRpcClient and attempting bind to domain controller {0}", new object[]
					{
						domainController
					});
					nspiRpcClientConnection.nspiRpcClient = new NspiRpcClient(domainController, "ncacn_ip_tcp", null);
					using (SafeRpcMemoryHandle safeRpcMemoryHandle = new SafeRpcMemoryHandle(36))
					{
						using (SafeRpcMemoryHandle safeRpcMemoryHandle2 = new SafeRpcMemoryHandle(16))
						{
							IntPtr ptr = safeRpcMemoryHandle.DangerousGetHandle();
							Marshal.WriteInt32(ptr, 0, 0);
							Marshal.WriteInt32(ptr, 4, 0);
							Marshal.WriteInt32(ptr, 8, 0);
							Marshal.WriteInt32(ptr, 12, 0);
							Marshal.WriteInt32(ptr, 16, 0);
							Marshal.WriteInt32(ptr, 20, 0);
							Marshal.WriteInt32(ptr, 24, 1252);
							Marshal.WriteInt32(ptr, 28, 1033);
							Marshal.WriteInt32(ptr, 32, 1033);
							int num2 = nspiRpcClientConnection.nspiRpcClient.Bind(0, safeRpcMemoryHandle.DangerousGetHandle(), safeRpcMemoryHandle2.DangerousGetHandle());
							if (num2 != 0)
							{
								NspiRpcClientConnection.TraceError(hashCode, "Bind returned non-zero SCODE {0}", new object[]
								{
									num2
								});
								throw new NspiFailureException(num2);
							}
							NspiRpcClientConnection.TraceDebug(hashCode, "Bind to domain controller {0} succeeded", new object[]
							{
								domainController
							});
							nspiRpcClientConnection.bound = true;
							NspiRpcClientConnection.PerfCounterInstance.NumberOfOpenConnections.Increment();
						}
					}
				}
				catch (RpcException ex)
				{
					num++;
					if (ex.ErrorCode != 1753 && ex.ErrorCode != 1727)
					{
						NspiRpcClientConnection.TraceError(hashCode, "Caught RpcException \"{0}\" with error code {1}.  We will not retry the bind.", new object[]
						{
							ex.Message,
							ex.ErrorCode
						});
						throw new ADOperationException(DirectoryStrings.NspiRpcError(ex.Message), ex);
					}
					if (num >= 3)
					{
						NspiRpcClientConnection.TraceError(hashCode, "Caught RpcException \"{0}\" with error code {1}.  Out of retries; giving up.", new object[]
						{
							ex.Message,
							ex.ErrorCode
						});
						throw new ADTransientException(DirectoryStrings.NspiRpcError(ex.Message), ex);
					}
					NspiRpcClientConnection.TraceWarning(hashCode, "Caught RpcException \"{0}\" with error code {1}.  We will retry the bind; this is retry {2}.", new object[]
					{
						ex.Message,
						ex.ErrorCode,
						num
					});
					Thread.Sleep(1000);
				}
				finally
				{
					if (nspiRpcClientConnection != null && !nspiRpcClientConnection.bound)
					{
						NspiRpcClientConnection.TraceDebug(hashCode, "Disposing the NspiRpcClient because we did not successfully bind", new object[0]);
						nspiRpcClientConnection.nspiRpcClient.Dispose();
						nspiRpcClientConnection.nspiRpcClient = null;
					}
				}
			}
			return nspiRpcClientConnection;
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x000DCCAC File Offset: 0x000DAEAC
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiRpcClientConnection>(this);
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x000DCCB4 File Offset: 0x000DAEB4
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x000DCCC9 File Offset: 0x000DAEC9
		public virtual void Dispose()
		{
			NspiRpcClientConnection.TraceDebug(this.GetHashCode(), "Disposing by calling Dispose()", new object[0]);
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x000DCCF0 File Offset: 0x000DAEF0
		public void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
						this.disposeTracker = null;
					}
					if (this.nspiRpcClient != null && this.bound)
					{
						try
						{
							this.nspiRpcClient.Unbind();
							this.bound = false;
							NspiRpcClientConnection.PerfCounterInstance.NumberOfOpenConnections.Decrement();
						}
						catch (RpcException)
						{
						}
						this.nspiRpcClient.Dispose();
						this.nspiRpcClient = null;
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x000DCD84 File Offset: 0x000DAF84
		private static void TraceDebug(int hashcode, string trace, params object[] args)
		{
			ExTraceGlobals.NspiRpcClientConnectionTracer.TraceDebug((long)hashcode, trace, args);
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x000DCD94 File Offset: 0x000DAF94
		private static void TraceWarning(int hashcode, string trace, params object[] args)
		{
			ExTraceGlobals.NspiRpcClientConnectionTracer.TraceWarning((long)hashcode, trace, args);
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x000DCDA4 File Offset: 0x000DAFA4
		private static void TraceError(int hashcode, string trace, params object[] args)
		{
			ExTraceGlobals.NspiRpcClientConnectionTracer.TraceError((long)hashcode, trace, args);
		}

		// Token: 0x04002707 RID: 9991
		public const string DefaultProtocolSequence = "ncacn_ip_tcp";

		// Token: 0x04002708 RID: 9992
		public const int DefaultCodePage = 1252;

		// Token: 0x04002709 RID: 9993
		public const int DefaultTemplateLocale = 1033;

		// Token: 0x0400270A RID: 9994
		public const int DefaultSortLocale = 1033;

		// Token: 0x0400270B RID: 9995
		private const int RPC_S_CALL_FAILED_DNE = 1727;

		// Token: 0x0400270C RID: 9996
		private const int EPT_S_NOT_REGISTERED = 1753;

		// Token: 0x0400270D RID: 9997
		private const int MaxConnectionRetries = 3;

		// Token: 0x0400270E RID: 9998
		private const int ConnectionRetryInterval = 1000;

		// Token: 0x0400270F RID: 9999
		private static NspiRpcClientConnectionPerformanceCountersInstance perfCounterInstance;

		// Token: 0x04002710 RID: 10000
		private static object perfCounterInstanceInitLock = new object();

		// Token: 0x04002711 RID: 10001
		private DisposeTracker disposeTracker;

		// Token: 0x04002712 RID: 10002
		private bool disposed;

		// Token: 0x04002713 RID: 10003
		private bool bound;

		// Token: 0x04002714 RID: 10004
		private NspiRpcClient nspiRpcClient;
	}
}
