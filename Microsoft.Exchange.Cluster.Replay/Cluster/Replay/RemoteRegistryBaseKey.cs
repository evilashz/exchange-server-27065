using System;
using System.IO;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200012C RID: 300
	internal sealed class RemoteRegistryBaseKey : IDisposable
	{
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x000329E5 File Offset: 0x00030BE5
		public RegistryKey Key
		{
			get
			{
				return this.m_remoteKey;
			}
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x000329F0 File Offset: 0x00030BF0
		public void Open(RegistryHive hive, string serverName)
		{
			this.m_openCalled = true;
			int remoteRegistryTimeoutInMsec = RegistryParameters.RemoteRegistryTimeoutInMsec;
			RemoteRegistryBaseKey.AsyncOpenTask asyncOpenTask = new RemoteRegistryBaseKey.AsyncOpenTask(this.DoOpen);
			this.m_asyncRefCount = 2;
			IAsyncResult asyncResult = asyncOpenTask.BeginInvoke(hive, serverName, new AsyncCallback(RemoteRegistryBaseKey.OpenCompletion), asyncOpenTask);
			WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
			if (!asyncWaitHandle.WaitOne(remoteRegistryTimeoutInMsec, false) && Interlocked.Decrement(ref this.m_asyncRefCount) > 0)
			{
				throw new RemoteRegistryTimedOutException(serverName, remoteRegistryTimeoutInMsec / 1000);
			}
			asyncOpenTask.EndInvoke(asyncResult);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00032A68 File Offset: 0x00030C68
		private static void OpenCompletion(IAsyncResult ar)
		{
			RemoteRegistryBaseKey.AsyncOpenTask asyncOpenTask = (RemoteRegistryBaseKey.AsyncOpenTask)ar.AsyncState;
			RemoteRegistryBaseKey remoteRegistryBaseKey = (RemoteRegistryBaseKey)asyncOpenTask.Target;
			if (Interlocked.Decrement(ref remoteRegistryBaseKey.m_asyncRefCount) > 0)
			{
				return;
			}
			Exception ex = null;
			try
			{
				asyncOpenTask.EndInvoke(ar);
			}
			catch (IOException ex2)
			{
				ex = ex2;
			}
			catch (SecurityException ex3)
			{
				ex = ex3;
			}
			catch (UnauthorizedAccessException ex4)
			{
				ex = ex4;
			}
			finally
			{
				if (ex != null)
				{
					ExTraceGlobals.StateTracer.TraceDebug<string>((long)remoteRegistryBaseKey.GetHashCode(), "RemoteRegistryBaseKey hit exception after being abandoned: {0}", ex.Message);
				}
				remoteRegistryBaseKey.Dispose();
			}
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00032B18 File Offset: 0x00030D18
		private void DoOpen(RegistryHive hive, string serverName)
		{
			RegistryKey remoteKey = RegistryKey.OpenRemoteBaseKey(hive, serverName);
			lock (this)
			{
				this.m_remoteKey = remoteKey;
			}
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00032B5C File Offset: 0x00030D5C
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00032B68 File Offset: 0x00030D68
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				lock (this)
				{
					if (this.m_remoteKey != null)
					{
						this.m_remoteKey.Close();
					}
					this.m_remoteKey = null;
				}
			}
		}

		// Token: 0x040004BF RID: 1215
		private RegistryKey m_remoteKey;

		// Token: 0x040004C0 RID: 1216
		private int m_asyncRefCount = 2;

		// Token: 0x040004C1 RID: 1217
		private bool m_openCalled;

		// Token: 0x0200012D RID: 301
		// (Invoke) Token: 0x06000B76 RID: 2934
		private delegate void AsyncOpenTask(RegistryHive hive, string serverName);
	}
}
