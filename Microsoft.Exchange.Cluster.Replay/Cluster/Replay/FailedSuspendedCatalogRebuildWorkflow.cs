using System;
using System.Linq;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.ActiveManager;
using Microsoft.Exchange.Search.Core.RpcEndpoint;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200028B RID: 651
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FailedSuspendedCatalogRebuildWorkflow : AutoReseedWorkflow
	{
		// Token: 0x0600193D RID: 6461 RVA: 0x000684FC File Offset: 0x000666FC
		public FailedSuspendedCatalogRebuildWorkflow(AutoReseedContext context, string workflowLaunchReason) : base(AutoReseedWorkflowType.FailedSuspendedCatalogRebuild, workflowLaunchReason, context)
		{
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x00068507 File Offset: 0x00066707
		public static IDisposable SetRebuildTestHook(Func<FailedSuspendedCatalogRebuildWorkflow, Exception> rebuildAction)
		{
			return FailedSuspendedCatalogRebuildWorkflow.hookableRebuildRpc.SetTestHook(rebuildAction);
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x00068514 File Offset: 0x00066714
		public static IDisposable SetMoveTestHook(Func<FailedSuspendedCatalogRebuildWorkflow, Exception> moveAction)
		{
			return FailedSuspendedCatalogRebuildWorkflow.hookableMove.SetTestHook(moveAction);
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x00068521 File Offset: 0x00066721
		protected override TimeSpan GetThrottlingInterval(AutoReseedWorkflowState state)
		{
			return TimeSpan.FromSeconds((double)RegistryParameters.AutoReseedCiRebuildFailedSuspendedThrottlingIntervalInSecs);
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001941 RID: 6465 RVA: 0x0006852E File Offset: 0x0006672E
		protected override bool IsDisabled
		{
			get
			{
				return RegistryParameters.AutoReseedCiRebuildFailedSuspendedDisabled;
			}
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x00068564 File Offset: 0x00066764
		protected override LocalizedString RunPrereqs(AutoReseedWorkflowState state)
		{
			LocalizedString result = base.RunPrereqs(state);
			if (!result.IsEmpty)
			{
				return result;
			}
			if (base.Context.CopyStatusesForTargetDatabase.Any((CopyStatusClientCachedEntry status) => status.Result == CopyStatusRpcResult.Success && status.CopyStatus.ActivationPreference < base.Context.TargetCopyStatus.CopyStatus.ActivationPreference))
			{
				AutoReseedWorkflow.Tracer.TraceDebug<string, Guid, string>((long)this.GetHashCode(), "AutoReseed workflow launcher detected failed catalog for database '{0}' [{1}]: {2}. However, the catalog doesn't qualify to be rebuilt.", base.Context.Database.Name, base.Context.Database.Guid, base.Context.TargetCopyStatus.CopyStatus.ErrorMessage);
				return ReplayStrings.AutoReseedCatalogSkipRebuild(base.Context.Database.Name, base.Context.TargetServerName.Fqdn);
			}
			return LocalizedString.Empty;
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00068618 File Offset: 0x00066818
		protected override Exception ExecuteInternal(AutoReseedWorkflowState state)
		{
			if (!base.Context.TargetCopyStatus.IsActive)
			{
				AutoReseedWorkflow.Tracer.TraceDebug<string, AmServerName>((long)this.GetHashCode(), "Database copy '{0}\\{1}' is not active. Switch over before rebuilding catalog.", base.Context.Database.Name, base.Context.TargetServerName);
				Exception ex = FailedSuspendedCatalogRebuildWorkflow.hookableMove.Value(this);
				if (ex != null)
				{
					return ex;
				}
			}
			return FailedSuspendedCatalogRebuildWorkflow.hookableRebuildRpc.Value(this);
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x00068694 File Offset: 0x00066894
		private static Exception RebuildIndexSystem(FailedSuspendedCatalogRebuildWorkflow workflow)
		{
			Exception result = null;
			SearchServiceRpcClient searchServiceRpcClient = null;
			bool flag = false;
			try
			{
				searchServiceRpcClient = RpcConnectionPool.GetSearchRpcClient();
				searchServiceRpcClient.RebuildIndexSystem(workflow.Context.Database.Guid);
				flag = true;
			}
			catch (RpcException ex)
			{
				result = ex;
			}
			finally
			{
				if (searchServiceRpcClient != null)
				{
					RpcConnectionPool.ReturnSearchRpcClientToCache(ref searchServiceRpcClient, !flag);
				}
			}
			return result;
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x000686FC File Offset: 0x000668FC
		private static Exception MoveDatabase(FailedSuspendedCatalogRebuildWorkflow workflow)
		{
			Exception result = null;
			try
			{
				AmDatabaseMoveResult amDatabaseMoveResult = null;
				AmDbActionCode actionCode = new AmDbActionCode(AmDbActionInitiator.Admin, AmDbActionReason.CatalogFailureItem, AmDbActionCategory.Move);
				string serverName;
				AmRpcClientHelper.MoveDatabaseEx(workflow.Context.Database, 0, 16, 0, null, workflow.Context.TargetServerName.Fqdn, false, 2, actionCode, ReplayStrings.AutoReseedMoveActiveBeforeRebuildCatalog, out serverName, ref amDatabaseMoveResult);
				AmRpcExceptionWrapper.Instance.ClientRethrowIfFailed(amDatabaseMoveResult.DbName, serverName, amDatabaseMoveResult.ErrorInfo);
			}
			catch (AmReplayServiceDownException ex)
			{
				result = ex;
			}
			catch (AmServerException ex2)
			{
				result = ex2;
			}
			catch (AmServerTransientException ex3)
			{
				result = ex3;
			}
			return result;
		}

		// Token: 0x04000A29 RID: 2601
		private static readonly Hookable<Func<FailedSuspendedCatalogRebuildWorkflow, Exception>> hookableRebuildRpc = Hookable<Func<FailedSuspendedCatalogRebuildWorkflow, Exception>>.Create(true, new Func<FailedSuspendedCatalogRebuildWorkflow, Exception>(FailedSuspendedCatalogRebuildWorkflow.RebuildIndexSystem));

		// Token: 0x04000A2A RID: 2602
		private static readonly Hookable<Func<FailedSuspendedCatalogRebuildWorkflow, Exception>> hookableMove = Hookable<Func<FailedSuspendedCatalogRebuildWorkflow, Exception>>.Create(true, new Func<FailedSuspendedCatalogRebuildWorkflow, Exception>(FailedSuspendedCatalogRebuildWorkflow.MoveDatabase));
	}
}
