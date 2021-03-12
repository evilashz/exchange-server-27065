using System;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000D6 RID: 214
	internal class ConnectionInfo
	{
		// Token: 0x06000AAC RID: 2732 RVA: 0x0002F8CE File Offset: 0x0002DACE
		internal ConnectionInfo(ADServerInfo serverInfo)
		{
			this.serverInfo = serverInfo;
			this.state = 0;
			ExTraceGlobals.ConnectionDetailsTracer.TraceDebug<string>((long)this.GetHashCode(), "Creating ConnectionInfo for {0}", serverInfo.FqdnPlusPort);
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x0002F900 File Offset: 0x0002DB00
		internal ADServerInfo ADServerInfo
		{
			get
			{
				return this.serverInfo;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x0002F908 File Offset: 0x0002DB08
		// (set) Token: 0x06000AAF RID: 2735 RVA: 0x0002F910 File Offset: 0x0002DB10
		internal PooledLdapConnection PooledLdapConnection
		{
			get
			{
				return this.connection;
			}
			set
			{
				ExTraceGlobals.ConnectionDetailsTracer.TraceDebug<int, string>((long)this.GetHashCode(), "Adding PooledLdapConnection {0} to ConnectionInfo for {1}", value.GetHashCode(), this.serverInfo.FqdnPlusPort);
				this.connection = value;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x0002F940 File Offset: 0x0002DB40
		internal ConnectionState ConnectionState
		{
			get
			{
				return (ConnectionState)this.state;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x0002F948 File Offset: 0x0002DB48
		internal Exception LastLdapException
		{
			get
			{
				return this.lastLdapException;
			}
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0002F950 File Offset: 0x0002DB50
		internal void MakeEmpty()
		{
			ExTraceGlobals.ConnectionTracer.TraceDebug<string, int>((long)this.GetHashCode(), "Emptying ConnectionInfo for {0}, conn={1}", this.serverInfo.FqdnPlusPort, this.PooledLdapConnection.GetHashCode());
			this.state = 0;
			this.connection.ReturnToPool();
			this.connection = null;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0002F9A4 File Offset: 0x0002DBA4
		internal void MakeDisconnected()
		{
			if (this.PooledLdapConnection != null)
			{
				ExTraceGlobals.ConnectionTracer.TraceDebug<string, int>((long)this.GetHashCode(), "Disconnecting ConnectionInfo for {0}, conn={1}", this.serverInfo.FqdnPlusPort, this.PooledLdapConnection.GetHashCode());
			}
			else
			{
				ExTraceGlobals.ConnectionTracer.TraceDebug<string>((long)this.GetHashCode(), "No connections for {0}", this.serverInfo.FqdnPlusPort);
			}
			this.state = 3;
			if (this.connection != null)
			{
				this.connection.ReturnToPool();
				this.connection = null;
			}
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0002FA2C File Offset: 0x0002DC2C
		internal bool TryMakeConnecting()
		{
			bool flag = 0 == Interlocked.CompareExchange(ref this.state, 1, 0);
			ExTraceGlobals.ConnectionTracer.TraceDebug<string, string>((long)this.GetHashCode(), "TryMakeConnecting {0} {1}", this.serverInfo.FqdnPlusPort, flag ? "succeeded" : "failed");
			return flag;
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0002FA7B File Offset: 0x0002DC7B
		internal void MakeConnected()
		{
			ExTraceGlobals.ConnectionTracer.TraceDebug<string, int>((long)this.GetHashCode(), "Connecting ConnectionInfo for {0}, conn={1}", this.serverInfo.FqdnPlusPort, this.PooledLdapConnection.GetHashCode());
			Interlocked.Exchange(ref this.state, 2);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0002FAB8 File Offset: 0x0002DCB8
		internal bool TrySetNamingContexts()
		{
			this.serverInfo = this.PooledLdapConnection.ADServerInfo;
			ADErrorRecord aderrorRecord;
			bool flag = this.PooledLdapConnection.TrySetNamingContexts(out aderrorRecord);
			if (!flag)
			{
				this.lastLdapException = aderrorRecord.InnerException;
			}
			return flag;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002FAF4 File Offset: 0x0002DCF4
		internal bool TryBindWithRetry(int maxBindRetryAttempts)
		{
			ADErrorRecord aderrorRecord;
			bool flag = this.PooledLdapConnection.TryBindWithRetry(maxBindRetryAttempts, out aderrorRecord);
			if (!flag)
			{
				this.lastLdapException = aderrorRecord.InnerException;
			}
			return flag;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002FB20 File Offset: 0x0002DD20
		internal bool TryCreatePooledLdapConnection(ADServerRole role, bool isNotify, NetworkCredential networkCredential)
		{
			bool result = false;
			try
			{
				this.connection = new PooledLdapConnection(this.ADServerInfo, role, isNotify, networkCredential);
				result = true;
			}
			catch (LdapException ex)
			{
				this.lastLdapException = ex;
			}
			return result;
		}

		// Token: 0x04000417 RID: 1047
		private ADServerInfo serverInfo;

		// Token: 0x04000418 RID: 1048
		private PooledLdapConnection connection;

		// Token: 0x04000419 RID: 1049
		private int state;

		// Token: 0x0400041A RID: 1050
		private Exception lastLdapException;
	}
}
