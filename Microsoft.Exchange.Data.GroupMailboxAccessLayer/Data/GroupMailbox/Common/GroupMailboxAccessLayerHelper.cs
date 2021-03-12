using System;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;

namespace Microsoft.Exchange.Data.GroupMailbox.Common
{
	// Token: 0x02000028 RID: 40
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class GroupMailboxAccessLayerHelper
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00009968 File Offset: 0x00007B68
		public static void ExecuteOperationWithRetry(IExtensibleLogger logger, string context, Action action, Predicate<Exception> shouldCatchException)
		{
			GroupMailboxAccessLayerHelper.<>c__DisplayClass6 CS$<>8__locals1 = new GroupMailboxAccessLayerHelper.<>c__DisplayClass6();
			CS$<>8__locals1.logger = logger;
			CS$<>8__locals1.context = context;
			CS$<>8__locals1.action = action;
			CS$<>8__locals1.shouldCatchException = shouldCatchException;
			CS$<>8__locals1.retryCount = 0;
			CS$<>8__locals1.attemptOperation = true;
			while (CS$<>8__locals1.attemptOperation)
			{
				ILUtil.DoTryFilterCatch(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<ExecuteOperationWithRetry>b__0)), new FilterDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<ExecuteOperationWithRetry>b__1)), new CatchDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<ExecuteOperationWithRetry>b__2)));
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000099F0 File Offset: 0x00007BF0
		internal static bool GetDomainControllerAffinityForOrganization(OrganizationId orgId, out ADServerInfo preferredDomainController)
		{
			if (orgId == null || orgId.ConfigurationUnit == null || orgId == OrganizationId.ForestWideOrgId)
			{
				preferredDomainController = null;
				return false;
			}
			ADRunspaceServerSettingsProvider instance = ADRunspaceServerSettingsProvider.GetInstance();
			bool flag;
			preferredDomainController = instance.GetGcFromToken(orgId.PartitionId.ForestFQDN, RunspaceServerSettings.GetTokenForOrganization(orgId), out flag, true);
			return true;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00009A44 File Offset: 0x00007C44
		private static void LogException(IExtensibleLogger logger, string context, Exception exception)
		{
			GroupMailboxAccessLayerHelper.Tracer.TraceError<string, Exception>(0L, "GroupMailboxHelper.LogException: Exception found while executing {0}. Exception: {1}.", context, exception);
			logger.LogEvent(new SchemaBasedLogEvent<MailboxAssociationLogSchema.Error>
			{
				{
					MailboxAssociationLogSchema.Error.Context,
					context
				},
				{
					MailboxAssociationLogSchema.Error.Exception,
					exception
				}
			});
		}

		// Token: 0x04000094 RID: 148
		private const int MaximumTransientFailureRetries = 3;

		// Token: 0x04000095 RID: 149
		private static readonly Trace Tracer = ExTraceGlobals.GroupMailboxAccessLayerTracer;
	}
}
