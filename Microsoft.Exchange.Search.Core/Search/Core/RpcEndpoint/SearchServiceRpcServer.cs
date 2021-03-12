using System;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.Search;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Core.RpcEndpoint
{
	// Token: 0x020000B8 RID: 184
	internal sealed class SearchServiceRpcServer : SearchRpcServer
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0001226E File Offset: 0x0001046E
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x00012275 File Offset: 0x00010475
		public static SearchServiceRpcServer Server { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0001227D File Offset: 0x0001047D
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x00012285 File Offset: 0x00010485
		public SearchServiceRpcServer.HandleRecordDocumentProcessing RecordDocumentProcessingHandler { get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0001228E File Offset: 0x0001048E
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x00012296 File Offset: 0x00010496
		public SearchServiceRpcServer.HandleRecordDocumentFailure RecordDocumentFailureHandler { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0001229F File Offset: 0x0001049F
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x000122A7 File Offset: 0x000104A7
		public Action UpdateIndexSystemsHandler { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x000122B0 File Offset: 0x000104B0
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x000122B8 File Offset: 0x000104B8
		public Action<Guid> ResumeIndexingHandler { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x000122C1 File Offset: 0x000104C1
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x000122C9 File Offset: 0x000104C9
		public Action<Guid> RebuildIndexSystemHandler { get; set; }

		// Token: 0x060005A8 RID: 1448 RVA: 0x000122D2 File Offset: 0x000104D2
		public override void RecordDocumentProcessing(Guid mdbGuid, Guid instance, Guid correlationId, long docId)
		{
			if (this.RecordDocumentProcessingHandler != null)
			{
				this.RecordDocumentProcessingHandler(mdbGuid, instance, correlationId, docId);
				return;
			}
			SearchServiceRpcServer.tracer.TraceError<Guid>((long)SearchServiceRpcServer.tracingContext, "No Handler Registered - Failed to track document with CorrelationId {0}", correlationId);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00012304 File Offset: 0x00010504
		public override void RecordDocumentFailure(Guid mdbGuid, Guid correlationId, long docId, string errorMessage)
		{
			if (this.RecordDocumentFailureHandler != null)
			{
				SearchServiceRpcServer.tracer.TraceDebug<Guid>((long)SearchServiceRpcServer.tracingContext, "Handling RPC - RecordDocumentFailure for CorrelationId: {0}", correlationId);
				this.RecordDocumentFailureHandler(mdbGuid, correlationId, docId, errorMessage);
				return;
			}
			SearchServiceRpcServer.tracer.TraceError<Guid, long, string>((long)SearchServiceRpcServer.tracingContext, "No Handler Registered - Failed to record a permanent poison failure for document. MdbGuid: {0}, DocId: {1}, ErrorMessage {2}", mdbGuid, docId, errorMessage);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00012359 File Offset: 0x00010559
		public override void UpdateIndexSystems()
		{
			if (this.UpdateIndexSystemsHandler != null)
			{
				this.UpdateIndexSystemsHandler();
				return;
			}
			SearchServiceRpcServer.tracer.TraceError((long)SearchServiceRpcServer.tracingContext, "No Handler Registered - Failed to update index systems");
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00012384 File Offset: 0x00010584
		public override void ResumeIndexing(Guid databaseGuid)
		{
			if (this.ResumeIndexingHandler != null)
			{
				this.ResumeIndexingHandler(databaseGuid);
				return;
			}
			SearchServiceRpcServer.tracer.TraceError<Guid>((long)SearchServiceRpcServer.tracingContext, "No Handler Registered - Failed to resume indexing on database {0}", databaseGuid);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x000123B1 File Offset: 0x000105B1
		public override void RebuildIndexSystem(Guid databaseGuid)
		{
			if (this.RebuildIndexSystemHandler != null)
			{
				this.RebuildIndexSystemHandler(databaseGuid);
				return;
			}
			SearchServiceRpcServer.tracer.TraceError<Guid>((long)SearchServiceRpcServer.tracingContext, "No Handler Registered - Failed to rebuild index system for database {0}", databaseGuid);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x000123E0 File Offset: 0x000105E0
		internal static void StartServer()
		{
			ObjectSecurity serverAdminSecurity = SearchServiceRpcServer.GetServerAdminSecurity();
			if (serverAdminSecurity != null)
			{
				SearchServiceRpcServer.Server = (SearchServiceRpcServer)RpcServerBase.RegisterServer(typeof(SearchServiceRpcServer), serverAdminSecurity, 1);
				SearchServiceRpcServer.tracingContext = SearchServiceRpcServer.Server.GetHashCode();
				SearchServiceRpcServer.tracer.TraceDebug((long)SearchServiceRpcServer.tracingContext, "Rpc Endpoint successfully Registered.");
				return;
			}
			throw new ComponentFailedPermanentException(Strings.RpcEndpointFailedToRegister);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00012440 File Offset: 0x00010640
		internal static void StopServer()
		{
			RpcServerBase.StopServer(SearchRpcServer.RpcIntfHandle);
			SearchServiceRpcServer.Server = null;
			SearchServiceRpcServer.tracer.TraceDebug((long)SearchServiceRpcServer.tracingContext, "Rpc Endpoint stopped.");
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00012570 File Offset: 0x00010770
		private static ObjectSecurity GetServerAdminSecurity()
		{
			FileSecurity securityDescriptor = null;
			ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 278, "GetServerAdminSecurity", "f:\\15.00.1497\\sources\\dev\\Search\\src\\Core\\RpcEndpoint\\SearchServiceRpcServer.cs");
				Server server = null;
				try
				{
					server = topologyConfigurationSession.FindLocalServer();
				}
				catch (LocalServerNotFoundException)
				{
					return;
				}
				catch (ADTopologyPermanentException innerException)
				{
					throw new ComponentFailedPermanentException(innerException);
				}
				RawSecurityDescriptor rawSecurityDescriptor = server.ReadSecurityDescriptor();
				if (rawSecurityDescriptor != null)
				{
					securityDescriptor = new FileSecurity();
					byte[] array = new byte[rawSecurityDescriptor.BinaryLength];
					rawSecurityDescriptor.GetBinaryForm(array, 0);
					securityDescriptor.SetSecurityDescriptorBinaryForm(array);
					IRootOrganizationRecipientSession rootOrganizationRecipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 309, "GetServerAdminSecurity", "f:\\15.00.1497\\sources\\dev\\Search\\src\\Core\\RpcEndpoint\\SearchServiceRpcServer.cs");
					SecurityIdentifier exchangeServersUsgSid = rootOrganizationRecipientSession.GetExchangeServersUsgSid();
					FileSystemAccessRule fileSystemAccessRule = new FileSystemAccessRule(exchangeServersUsgSid, FileSystemRights.ReadData, AccessControlType.Allow);
					securityDescriptor.SetAccessRule(fileSystemAccessRule);
					SecurityIdentifier identity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
					fileSystemAccessRule = new FileSystemAccessRule(identity, FileSystemRights.ReadData, AccessControlType.Allow);
					securityDescriptor.AddAccessRule(fileSystemAccessRule);
					return;
				}
			}, 3);
			return securityDescriptor;
		}

		// Token: 0x0400028E RID: 654
		private static readonly Trace tracer = ExTraceGlobals.SearchRpcServerTracer;

		// Token: 0x0400028F RID: 655
		private static int tracingContext;

		// Token: 0x020000B9 RID: 185
		// (Invoke) Token: 0x060005B3 RID: 1459
		public delegate void HandleRecordDocumentProcessing(Guid mdbGuid, Guid flowInstance, Guid correlationId, long docId);

		// Token: 0x020000BA RID: 186
		// (Invoke) Token: 0x060005B7 RID: 1463
		public delegate void HandleRecordDocumentFailure(Guid mdbGuid, Guid correlationId, long docId, string errorMessage);
	}
}
