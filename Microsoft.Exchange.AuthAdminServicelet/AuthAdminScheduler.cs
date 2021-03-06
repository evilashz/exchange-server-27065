using System;
using System.Threading;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Servicelets.AuthAdmin.Messages;

namespace Microsoft.Exchange.Servicelets.AuthAdmin
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AuthAdminScheduler : CacheScheduler
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000225E File Offset: 0x0000045E
		internal AuthAdminScheduler(AuthAdminContext context, WaitHandle stopEvent) : base(context, stopEvent)
		{
			this.authAdminAuthConfig = new AuthAdminAuthConfig(context, stopEvent);
			this.authAdminCertificates = new AuthAdminCertificates(context, stopEvent);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002284 File Offset: 0x00000484
		protected override AnchorJobProcessorResult ProcessEntry(CacheEntryBase cacheEntry)
		{
			AuthAdminContext authAdminContext = base.Context as AuthAdminContext;
			AnchorUtil.AssertOrThrow(authAdminContext != null, "expect to have a valid AuthAdminContext", new object[0]);
			authAdminContext.Logger.Log(MigrationEventType.Information, "Checking if mailbox {0} active on current server", new object[]
			{
				cacheEntry
			});
			AnchorJobProcessorResult anchorJobProcessorResult = base.ProcessEntry(cacheEntry);
			if (anchorJobProcessorResult == AnchorJobProcessorResult.Deleted)
			{
				authAdminContext.Logger.Log(MigrationEventType.Information, "Mailbox {0} is not active on current server, skipping Auth Admin tasks", new object[]
				{
					cacheEntry
				});
				return anchorJobProcessorResult;
			}
			authAdminContext.Logger.Log(MigrationEventType.Information, "Mailbox {0} is active on current server", new object[]
			{
				cacheEntry
			});
			try
			{
				authAdminContext.Logger.Log(MigrationEventType.Information, "Starting Auth Admin tasks", new object[0]);
				ITopologyConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 81, "ProcessEntry", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ServiceHost\\Servicelets\\AuthAdmin\\Program\\AuthAdminScheduler.cs");
				this.authAdminAuthConfig.DoScheduledWork(session);
				this.authAdminCertificates.DoScheduledWork(session);
			}
			catch (ADTransientException ex)
			{
				authAdminContext.Logger.LogTerseEvent(MigrationEventType.Error, MSExchangeAuthAdminEventLogConstants.Tuple_TransientException, new string[]
				{
					AnchorLogger.GetDiagnosticInfo(ex, null)
				});
				throw;
			}
			catch (DataSourceTransientException ex2)
			{
				authAdminContext.Logger.LogTerseEvent(MigrationEventType.Error, MSExchangeAuthAdminEventLogConstants.Tuple_TransientException, new string[]
				{
					AnchorLogger.GetDiagnosticInfo(ex2, null)
				});
				throw;
			}
			catch (Exception ex3)
			{
				authAdminContext.Logger.LogTerseEvent(MigrationEventType.Error, MSExchangeAuthAdminEventLogConstants.Tuple_PermanentException, new string[]
				{
					AnchorLogger.GetDiagnosticInfo(ex3, null)
				});
			}
			finally
			{
				authAdminContext.Logger.LogTerseEvent(MigrationEventType.Information, MSExchangeAuthAdminEventLogConstants.Tuple_AuthAdminCompleted, new string[]
				{
					cacheEntry.ToString()
				});
			}
			return AnchorJobProcessorResult.Waiting;
		}

		// Token: 0x04000006 RID: 6
		private AuthAdminAuthConfig authAdminAuthConfig;

		// Token: 0x04000007 RID: 7
		private AuthAdminCertificates authAdminCertificates;
	}
}
