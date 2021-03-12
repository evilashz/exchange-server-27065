using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.MailboxTransport.StoreDriver;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x0200000B RID: 11
	internal sealed class SubmissionConnectionPool
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00003C56 File Offset: 0x00001E56
		private SubmissionConnectionPool()
		{
			this.connections = new Dictionary<string, SubmissionConnection>();
			this.connectionsSyncObject = new object();
			TraceHelper.SubmissionConnectionPoolTracer.TracePass(TraceHelper.MessageProbeActivityId, 0L, "SubmissionConnectionPool: Instance created.");
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003C8C File Offset: 0x00001E8C
		public static SubmissionConnectionWrapper GetConnection(string server, string database)
		{
			SubmissionConnection connection = SubmissionConnectionPool.instance.InternalGetConnection(server, database);
			return new SubmissionConnectionWrapper(connection);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003CAE File Offset: 0x00001EAE
		public static void ExpireOldConnections()
		{
			SubmissionConnectionPool.instance.InternalExpireOldConnections();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003CBC File Offset: 0x00001EBC
		public bool CanStopConnection(SubmissionConnection connection)
		{
			bool result;
			lock (this.connectionsSyncObject)
			{
				if (connection.IsInUse)
				{
					TraceHelper.SubmissionConnectionPoolTracer.TracePass<int, string>(TraceHelper.MessageProbeActivityId, (long)this.GetHashCode(), "SubmissionConnectionPool.CanStopConnection: Thread {0}, No removing in-use connection: {1}.", Thread.CurrentThread.ManagedThreadId, connection.ToString());
					result = false;
				}
				else if (connection.HasReachedSubmissionLimit)
				{
					TraceHelper.SubmissionConnectionPoolTracer.TracePass<int, string>(TraceHelper.MessageProbeActivityId, (long)this.GetHashCode(), "SubmissionConnectionPool.CanStopConnection: Thread {0}, Connection not in use, and reached limit, OK to stop: {1}.", Thread.CurrentThread.ManagedThreadId, connection.ToString());
					result = true;
				}
				else if (connection.Failures > 0)
				{
					if (this.connections.ContainsKey(connection.Key))
					{
						this.connections.Remove(connection.Key);
						TraceHelper.SubmissionConnectionPoolTracer.TracePass<int, string>(TraceHelper.MessageProbeActivityId, (long)this.GetHashCode(), "SubmissionConnectionPool.CanStopConnection: Thread {0}, Removed connection: {1}.", Thread.CurrentThread.ManagedThreadId, connection.ToString());
						result = true;
					}
					else
					{
						TraceHelper.SubmissionConnectionPoolTracer.TracePass<int, string>(TraceHelper.MessageProbeActivityId, (long)this.GetHashCode(), "SubmissionConnectionPool.CanStopConnection: Thread {0}, Connection was already removed: {1}.", Thread.CurrentThread.ManagedThreadId, connection.ToString());
						result = false;
					}
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003E08 File Offset: 0x00002008
		internal static void TestReset()
		{
			SubmissionConnectionPool.instance.InternalTestReset();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003E14 File Offset: 0x00002014
		private void InternalTestReset()
		{
			lock (this.connectionsSyncObject)
			{
				this.connections.Clear();
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003E5C File Offset: 0x0000205C
		private SubmissionConnection InternalGetConnection(string server, string database)
		{
			string connectionKey = this.GetConnectionKey(server, database);
			TraceHelper.SubmissionConnectionPoolTracer.TracePass<int, string, string>(TraceHelper.MessageProbeActivityId, (long)this.GetHashCode(), "SubmissionConnectionPool.InternalGetConnection: Thread {0}, Server {1}, Database {2}).", Thread.CurrentThread.ManagedThreadId, server, database);
			bool flag = false;
			SubmissionConnection submissionConnection;
			lock (this.connectionsSyncObject)
			{
				if (this.connections.TryGetValue(connectionKey, out submissionConnection))
				{
					submissionConnection.SubmissionStarted();
					TraceHelper.SubmissionConnectionPoolTracer.TracePass<int, string>(TraceHelper.MessageProbeActivityId, (long)this.GetHashCode(), "SubmissionConnectionPool.InternalGetConnection: Thread {0}, using existing connection: {1}.", Thread.CurrentThread.ManagedThreadId, submissionConnection.ToString());
					if (submissionConnection.HasReachedSubmissionLimit)
					{
						TraceHelper.SubmissionConnectionPoolTracer.TracePass<int, string>(TraceHelper.MessageProbeActivityId, 0L, "SubmissionConnectionPool.InternalGetConnection: Thread {0}, connection has reached its message limit and will be removed from the pool: {1}.", Thread.CurrentThread.ManagedThreadId, submissionConnection.ToString());
						this.connections.Remove(connectionKey);
					}
				}
				else
				{
					submissionConnection = new SubmissionConnection(connectionKey, this, server, database);
					submissionConnection.SubmissionStarted();
					flag = true;
					this.connections[connectionKey] = submissionConnection;
					TraceHelper.SubmissionConnectionPoolTracer.TracePass<int, string>(TraceHelper.MessageProbeActivityId, (long)this.GetHashCode(), "SubmissionConnectionPool.InternalGetConnection: Thread {0}, created new connection {1}.", Thread.CurrentThread.ManagedThreadId, submissionConnection.ToString());
				}
			}
			if (flag)
			{
				submissionConnection.StartConnection();
			}
			return submissionConnection;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003F9C File Offset: 0x0000219C
		private void InternalExpireOldConnections()
		{
			List<SubmissionConnection> list = null;
			lock (this.connectionsSyncObject)
			{
				foreach (string key in this.connections.Keys)
				{
					SubmissionConnection submissionConnection = this.connections[key];
					if (submissionConnection.TimeoutElapsed && !submissionConnection.IsInUse)
					{
						if (list == null)
						{
							list = new List<SubmissionConnection>();
						}
						TraceHelper.SubmissionConnectionPoolTracer.TracePass<int, string>(TraceHelper.MessageProbeActivityId, (long)this.GetHashCode(), "SubmissionConnectionPool.ExpireOldConnections: Thread {0}, Adding to expired list: {1}.", Thread.CurrentThread.ManagedThreadId, submissionConnection.ToString());
						list.Add(submissionConnection);
					}
				}
				if (list != null)
				{
					foreach (SubmissionConnection submissionConnection2 in list)
					{
						TraceHelper.SubmissionConnectionPoolTracer.TracePass<int, string>(TraceHelper.MessageProbeActivityId, (long)this.GetHashCode(), "SubmissionConnectionPool.ExpireOldConnections: Thread {0}, Removing connection: {1}.", Thread.CurrentThread.ManagedThreadId, submissionConnection2.ToString());
						this.connections.Remove(submissionConnection2.Key);
					}
				}
			}
			if (list != null)
			{
				foreach (SubmissionConnection submissionConnection3 in list)
				{
					TraceHelper.SubmissionConnectionPoolTracer.TracePass<int, string>(TraceHelper.MessageProbeActivityId, (long)this.GetHashCode(), "SubmissionConnectionPool.ExpireOldConnections: Thread {0}, Invoking TimeOutExpired for: {1}.", Thread.CurrentThread.ManagedThreadId, submissionConnection3.ToString());
					submissionConnection3.TimeoutExpired();
				}
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000418C File Offset: 0x0000238C
		private string GetConnectionKey(string server, string database)
		{
			return string.Format("Server: {0}, Database: {1}", server, database);
		}

		// Token: 0x04000019 RID: 25
		private static readonly SubmissionConnectionPool instance = new SubmissionConnectionPool();

		// Token: 0x0400001A RID: 26
		private readonly object connectionsSyncObject;

		// Token: 0x0400001B RID: 27
		private Dictionary<string, SubmissionConnection> connections;
	}
}
