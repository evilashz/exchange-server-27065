using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.PeopleRelevance;
using Microsoft.Exchange.Inference.MdbCommon;
using Microsoft.Exchange.Inference.Pipeline;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.Core.Pipeline;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PeopleRelevance
{
	// Token: 0x0200021C RID: 540
	internal sealed class PeopleRelevanceAssistant : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase, IDisposable
	{
		// Token: 0x0600149A RID: 5274 RVA: 0x00076680 File Offset: 0x00074880
		public PeopleRelevanceAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
			this.DiagnosticsSession = Microsoft.Exchange.Search.Core.Diagnostics.DiagnosticsSession.CreateComponentDiagnosticsSession(base.NonLocalizedName, ExTraceGlobals.GeneralTracer, (long)this.GetHashCode());
			this.DiagnosticsSession.SetDefaults(Guid.Parse("{D99DB294-33E5-410A-87D6-4FD458E58BDD}"), "PeopleRelevanceAssistant", "People Relevance Diagnostics Logs", Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\PeopleRelevance"), "PeopleRelevanceLogs_", "PeopleRelevanceAssistantLogs");
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x000766EC File Offset: 0x000748EC
		public void OnWorkCycleCheckpoint()
		{
			try
			{
				if (this.peopleRelevancePipeline == null)
				{
					PipelineContext pipelineContext;
					this.peopleRelevancePipeline = InferencePipelineUtil.CreateAndStartTrainingPipeline(base.DatabaseInfo.Guid.ToString(), this.DiagnosticsSession, "PeopleRelevancePipelineDefinition.xml", "PeopleRelevance", PeopleRelevanceAssistantType.InferencePipelineVersion, out pipelineContext);
				}
				this.DiagnosticsSession.Assert(this.peopleRelevancePipeline != null, "People Relevance Pipeline is null", new object[0]);
				if (this.peopleRelevancePipelineFeeder == null)
				{
					this.peopleRelevancePipelineFeeder = new PeopleRelevanceFeeder(this.peopleRelevancePipeline);
				}
			}
			catch (InvalidOperationException ex)
			{
				if (!(ex.InnerException is FileNotFoundException))
				{
					throw;
				}
				this.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Failures, "PeopleRelevanceAssistant.OnWorkCycleCheckpoint: FAILED to create people relevance pipeline because definition file doesn't exist.  Exception: {0}", new object[]
				{
					ex
				});
			}
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x000767B8 File Offset: 0x000749B8
		public void Dispose()
		{
			this.peopleRelevancePipelineFeeder = null;
			if (this.peopleRelevancePipeline != null)
			{
				this.peopleRelevancePipeline.Dispose();
				this.peopleRelevancePipeline = null;
			}
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x000767DB File Offset: 0x000749DB
		protected override void OnShutdownInternal()
		{
			this.DiagnosticsSession.TraceDebug("ShutDown is called on the service. Sending Abort processing signal to the pipeline", new object[0]);
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x000767F4 File Offset: 0x000749F4
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
			MailboxSession mailboxSession = invokeArgs.StoreSession as MailboxSession;
			if (mailboxSession == null)
			{
				return;
			}
			if (mailboxSession.MailboxOwner.RecipientTypeDetails != RecipientTypeDetails.UserMailbox && mailboxSession.MailboxOwner.RecipientTypeDetails != RecipientTypeDetails.LinkedMailbox)
			{
				this.DiagnosticsSession.TraceDebug<Guid, string, string>("Skipping mailbox with guid {0} and display name {1} since this is a {2} and not a UserMailbox or a LinkedMailbox", mailboxSession.MailboxGuid, mailboxSession.MailboxOwner.MailboxInfo.DisplayName, mailboxSession.MailboxOwner.RecipientTypeDetails.ToString());
				this.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "SkipMBP", new object[]
				{
					PeopleRelevanceAssistantLoggingHelper.BuildOperationSpecificString(mailboxSession.MailboxGuid.ToString(), mailboxSession.ServerFullyQualifiedDomainName, mailboxSession.MailboxOwner.RecipientTypeDetails.ToString(), mailboxSession.DisplayAddress, mailboxSession.OrganizationId.ToString())
				});
				return;
			}
			this.DiagnosticsSession.TraceDebug<IExchangePrincipal>("{0}: Begin process mailbox", mailboxSession.MailboxOwner);
			try
			{
				this.peopleRelevancePipelineFeeder.ProcessMailbox(mailboxSession, this.DiagnosticsSession, false);
				this.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "CompleteMBP", new object[]
				{
					PeopleRelevanceAssistantLoggingHelper.BuildOperationSpecificString(mailboxSession.MailboxGuid.ToString(), mailboxSession.ServerFullyQualifiedDomainName, mailboxSession.MailboxOwner.RecipientTypeDetails.ToString(), mailboxSession.DisplayAddress, mailboxSession.OrganizationId.ToString())
				});
			}
			catch (AbortOnProcessingRequestedException)
			{
				this.DiagnosticsSession.TraceDebug<IExchangePrincipal>("{0} : Aborting the mailbox because of a shutdown signal", mailboxSession.MailboxOwner);
				this.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Warnings, "AbortMBPS", new object[]
				{
					PeopleRelevanceAssistantLoggingHelper.BuildOperationSpecificString(mailboxSession.MailboxGuid.ToString(), mailboxSession.ServerFullyQualifiedDomainName, mailboxSession.MailboxOwner.RecipientTypeDetails.ToString(), mailboxSession.DisplayAddress, mailboxSession.OrganizationId.ToString())
				});
			}
			catch (Exception ex)
			{
				this.DiagnosticsSession.TraceDebug<IExchangePrincipal, Exception>("PeopleRelevanceAssistant.InvokeInternal: FAILED to process mailbox for user: {0}.  Exception: {1}", mailboxSession.MailboxOwner, ex);
				this.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Failures, "FailedMBP", new object[]
				{
					PeopleRelevanceAssistantLoggingHelper.BuildOperationSpecificString(mailboxSession.MailboxGuid.ToString(), mailboxSession.ServerFullyQualifiedDomainName, mailboxSession.MailboxOwner.RecipientTypeDetails.ToString(), mailboxSession.DisplayAddress, mailboxSession.OrganizationId.ToString(), ex.ToString())
				});
			}
			this.DiagnosticsSession.TraceDebug<IExchangePrincipal>("{0}: End process mailbox", mailboxSession.MailboxOwner);
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x00076AA0 File Offset: 0x00074CA0
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x00076AA8 File Offset: 0x00074CA8
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x00076AB0 File Offset: 0x00074CB0
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000C67 RID: 3175
		private readonly IDiagnosticsSession DiagnosticsSession;

		// Token: 0x04000C68 RID: 3176
		private PeopleRelevanceFeeder peopleRelevancePipelineFeeder;

		// Token: 0x04000C69 RID: 3177
		private Pipeline peopleRelevancePipeline;
	}
}
