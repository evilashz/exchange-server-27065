using System;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000004 RID: 4
	internal static class ContentIndexingConnectionFactory
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000023DE File Offset: 0x000005DE
		internal static IDiagnosticsSession Diagnostics
		{
			get
			{
				return ContentIndexingConnectionFactory.diagnosticsSession;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023E8 File Offset: 0x000005E8
		internal static void InitializeIfNeeded()
		{
			if (ContentIndexingConnectionFactory.configuration != null)
			{
				return;
			}
			ContentIndexingConnectionFactory.configuration = new FlightingSearchConfig();
			ContentIndexingConnectionFactory.diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession("ContentIndexingConnection", ContentIndexingConnectionFactory.configuration.ServiceName, ExTraceGlobals.XSOMailboxSessionTracer, 0L);
			ContentIndexingConnectionFactory.Diagnostics.TraceDebug("ContentIndexingConnectionFactory initialized", new object[0]);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000243C File Offset: 0x0000063C
		internal static void ResetForTest(SearchConfig testConfiguration)
		{
			ContentIndexingConnectionFactory.configuration = testConfiguration;
			ContentIndexingConnectionFactory.DropConnection();
			ContentIndexingConnectionFactory.nextConnectionRetry = DateTime.MinValue;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002454 File Offset: 0x00000654
		internal static void Cleanup()
		{
			lock (ContentIndexingConnectionFactory.lockObject)
			{
				ContentIndexingConnectionFactory.DropConnection();
			}
			ContentIndexingConnectionFactory.Diagnostics.TraceDebug("ContentIndexingConnectionFactory cleaned up", new object[0]);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024A8 File Offset: 0x000006A8
		internal static ContentIndexingConnectionFactory.ConnectionUsageFrame GetConnection(out ContentIndexingConnection connectionForUse)
		{
			connectionForUse = null;
			ContentIndexingConnectionFactory.ConnectionUsageFrame result;
			lock (ContentIndexingConnectionFactory.lockObject)
			{
				if (ContentIndexingConnectionFactory.currentConnection == null)
				{
					if (ContentIndexingConnectionFactory.nextConnectionRetry > DateTime.UtcNow)
					{
						connectionForUse = null;
						return new ContentIndexingConnectionFactory.ConnectionUsageFrame(null);
					}
					bool flag2 = false;
					ContentIndexingConnection contentIndexingConnection = null;
					try
					{
						contentIndexingConnection = new ContentIndexingConnection(ContentIndexingConnectionFactory.configuration);
						ContentIndexingConnectionFactory.Diagnostics.TraceDebug<int>("New connection created: {0}", contentIndexingConnection.GetHashCode());
						ContentIndexingConnectionFactory.currentConnection = new ReferenceCount<ContentIndexingConnection>(contentIndexingConnection);
						contentIndexingConnection = null;
						flag2 = true;
					}
					catch (PerformingFastOperationException arg)
					{
						ContentIndexingConnectionFactory.Diagnostics.TraceDebug<PerformingFastOperationException>("GetConnection called. Connection creation failed with PerformingFastOperationException: {0}", arg);
					}
					catch (FastConnectionException arg2)
					{
						ContentIndexingConnectionFactory.Diagnostics.TraceDebug<FastConnectionException>("GetConnection called. Connection creation failed with FastConnectionException: {0}", arg2);
					}
					catch (IndexSystemNotFoundException arg3)
					{
						ContentIndexingConnectionFactory.Diagnostics.TraceDebug<IndexSystemNotFoundException>("GetConnection called. Connection creation failed with IndexSystemNotFoundException: {0}", arg3);
					}
					finally
					{
						if (flag2)
						{
							ContentIndexingConnectionFactory.nextConnectionRetry = DateTime.MinValue;
						}
						else
						{
							ContentIndexingConnectionFactory.nextConnectionRetry = DateTime.UtcNow + ContentIndexingConnectionFactory.configuration.ConnectionRetryDelay;
						}
						if (contentIndexingConnection != null)
						{
							contentIndexingConnection.Dispose();
						}
					}
				}
				if (ContentIndexingConnectionFactory.currentConnection != null)
				{
					connectionForUse = ContentIndexingConnectionFactory.currentConnection.ReferencedObject;
				}
				result = new ContentIndexingConnectionFactory.ConnectionUsageFrame(ContentIndexingConnectionFactory.currentConnection);
			}
			return result;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002608 File Offset: 0x00000808
		internal static void OnConnectionLevelFailure(ContentIndexingConnection connection, bool fatalFailure)
		{
			ContentIndexingConnectionFactory.Diagnostics.Assert(connection != null, "Null connection", new object[0]);
			lock (ContentIndexingConnectionFactory.lockObject)
			{
				bool flag2 = ContentIndexingConnectionFactory.currentConnection == null || ContentIndexingConnectionFactory.currentConnection.ReferencedObject == null;
				ContentIndexingConnectionFactory.Diagnostics.TraceDebug<int, int, bool>("OnConnectionLevelFailure called. Failed connection: {0}. Current connection: {1}. Fatal failure: {2}", connection.GetHashCode(), flag2 ? 0 : ContentIndexingConnectionFactory.currentConnection.ReferencedObject.GetHashCode(), fatalFailure);
				if (!flag2 && object.ReferenceEquals(connection, ContentIndexingConnectionFactory.currentConnection.ReferencedObject))
				{
					bool flag3 = false;
					if (fatalFailure)
					{
						flag3 = true;
					}
					else
					{
						int numberOfConnectionLevelFailures = connection.NumberOfConnectionLevelFailures;
						if (numberOfConnectionLevelFailures >= ContentIndexingConnectionFactory.configuration.ConnectionLevelFailuresThreshold)
						{
							ContentIndexingConnectionFactory.Diagnostics.TraceDebug<int, int>("Connection exceeded connection level threshold and will be dropped [{0}, {1}]", numberOfConnectionLevelFailures, connection.NumberOfConnectionLevelFailures);
							flag3 = true;
						}
					}
					if (flag3)
					{
						ContentIndexingConnectionFactory.DropConnection();
						ContentIndexingConnectionFactory.nextConnectionRetry = DateTime.UtcNow + ContentIndexingConnectionFactory.configuration.ConnectionRetryDelay;
					}
				}
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002710 File Offset: 0x00000910
		private static void DropConnection()
		{
			if (ContentIndexingConnectionFactory.currentConnection != null)
			{
				ContentIndexingConnectionFactory.currentConnection.Release();
				ContentIndexingConnectionFactory.currentConnection = null;
			}
		}

		// Token: 0x0400000D RID: 13
		private static SearchConfig configuration;

		// Token: 0x0400000E RID: 14
		private static IDiagnosticsSession diagnosticsSession;

		// Token: 0x0400000F RID: 15
		private static ReferenceCount<ContentIndexingConnection> currentConnection;

		// Token: 0x04000010 RID: 16
		private static object lockObject = new object();

		// Token: 0x04000011 RID: 17
		private static DateTime nextConnectionRetry = DateTime.MinValue;

		// Token: 0x02000005 RID: 5
		public struct ConnectionUsageFrame : IDisposable
		{
			// Token: 0x06000025 RID: 37 RVA: 0x00002740 File Offset: 0x00000940
			internal ConnectionUsageFrame(ReferenceCount<ContentIndexingConnection> connection)
			{
				this.connection = connection;
				if (connection != null)
				{
					this.connection.AddRef();
				}
			}

			// Token: 0x06000026 RID: 38 RVA: 0x00002758 File Offset: 0x00000958
			public void Dispose()
			{
				if (this.connection != null)
				{
					this.connection.Release();
				}
			}

			// Token: 0x04000012 RID: 18
			private ReferenceCount<ContentIndexingConnection> connection;
		}
	}
}
