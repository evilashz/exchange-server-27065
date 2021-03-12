using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConnectionHandler : BaseObject, IConnectionHandler, IDisposable
	{
		// Token: 0x0600005A RID: 90 RVA: 0x000047D5 File Offset: 0x000029D5
		private ConnectionHandler(IConnection connectionHandlerConnection)
		{
			this.connection = connectionHandlerConnection;
			this.ropHandler = new RopHandler(this);
			this.notificationHandler = new NotificationHandler(this);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00004812 File Offset: 0x00002A12
		public IConnection Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000481A File Offset: 0x00002A1A
		public IRopHandler RopHandler
		{
			get
			{
				return this.ropHandler;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00004822 File Offset: 0x00002A22
		public INotificationHandler NotificationHandler
		{
			get
			{
				return this.notificationHandler;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000482A File Offset: 0x00002A2A
		public static ConnectionHandler Create(IConnection connectionHandlerConnection)
		{
			return new ConnectionHandler(connectionHandlerConnection);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004834 File Offset: 0x00002A34
		public void BeginRopProcessing(AuxiliaryData auxiliaryData)
		{
			bool flag = false;
			lock (this.logons)
			{
				flag = (this.logons.Count > 0);
			}
			if (flag)
			{
				this.lastTimeConnectionHadLogons = DateTime.UtcNow;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004890 File Offset: 0x00002A90
		public void EndRopProcessing(AuxiliaryData auxiliaryData)
		{
			bool flag = false;
			lock (this.logons)
			{
				flag = (this.logons.Count > 0);
			}
			if (!flag && Configuration.ServiceConfiguration.EnableSmartConnectionTearDown && DateTime.UtcNow - this.lastTimeConnectionHadLogons > ConnectionHandler.MaximumLifetimeForAConnectionWithoutLogons)
			{
				throw new SessionDeadException("Connection doesn't have any open logons, but has client activity. This may be masking synchronization stalls. Dropping a connection.");
			}
			if (this.notificationHandler.HasPendingNotifications())
			{
				this.notificationHandler.InvokeCallback();
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000492C File Offset: 0x00002B2C
		public void LogInputRops(IEnumerable<RopId> rops)
		{
			ProtocolLog.LogInputRops(rops);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004934 File Offset: 0x00002B34
		public void LogPrepareForRop(RopId ropId)
		{
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004936 File Offset: 0x00002B36
		public void LogCompletedRop(RopId ropId, ErrorCode errorCode)
		{
			ProtocolLog.LogOutputRop(ropId, errorCode);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000493F File Offset: 0x00002B3F
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.ropHandler);
			base.InternalDispose();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004952 File Offset: 0x00002B52
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ConnectionHandler>(this);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000495C File Offset: 0x00002B5C
		internal bool ForAnyLogon(Func<Logon, bool> logonDelegate)
		{
			lock (this.logons)
			{
				foreach (Logon arg in this.logons)
				{
					if (logonDelegate(arg))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000049E4 File Offset: 0x00002BE4
		internal void AddLogon(Logon logon)
		{
			lock (this.logons)
			{
				this.logons.Add(logon);
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004A2C File Offset: 0x00002C2C
		internal void RemoveLogon(Logon logon)
		{
			lock (this.logons)
			{
				this.logons.Remove(logon);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00004A74 File Offset: 0x00002C74
		private static TimeSpan MaximumLifetimeForAConnectionWithoutLogons
		{
			get
			{
				return TimeSpan.FromMilliseconds(2.0 * Configuration.ServiceConfiguration.RpcPollsMax.TotalMilliseconds);
			}
		}

		// Token: 0x04000047 RID: 71
		public static readonly HandlerFactory Factory = new HandlerFactory(ConnectionHandler.Create);

		// Token: 0x04000048 RID: 72
		private readonly IConnection connection;

		// Token: 0x04000049 RID: 73
		private readonly HashSet<Logon> logons = new HashSet<Logon>();

		// Token: 0x0400004A RID: 74
		private readonly RopHandler ropHandler;

		// Token: 0x0400004B RID: 75
		private readonly NotificationHandler notificationHandler;

		// Token: 0x0400004C RID: 76
		private DateTime lastTimeConnectionHadLogons = DateTime.UtcNow;
	}
}
