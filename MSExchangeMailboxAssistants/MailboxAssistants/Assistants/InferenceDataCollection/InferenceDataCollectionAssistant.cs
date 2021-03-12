using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.InferenceDataCollection;
using Microsoft.Exchange.Inference.Common.Diagnostics;
using Microsoft.Exchange.Inference.DataAnalysis;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.InferenceDataCollection
{
	// Token: 0x02000215 RID: 533
	internal sealed class InferenceDataCollectionAssistant : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase
	{
		// Token: 0x06001453 RID: 5203 RVA: 0x000755E3 File Offset: 0x000737E3
		public InferenceDataCollectionAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName, IDiagnosticLogger diagnosticLogger, ICollectionContext collectionContext) : base(databaseInfo, name, nonLocalizedName)
		{
			this.tracer = ExTraceGlobals.GeneralTracer;
			this.tracer.TraceFunction((long)this.GetHashCode(), "InferenceDataCollectionAssistant.InferenceDataCollectionAssistant");
			this.diagnosticLogger = diagnosticLogger;
			this.collectionContext = collectionContext;
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x00075620 File Offset: 0x00073820
		public static void PostInferenceDataCollectionSuccessNotification(Guid mailboxGuid)
		{
			InferenceDataCollectionAssistant.PostInferenceDataCollectionNotification("InferenceDataCollectionSuccessNotification", mailboxGuid.ToString());
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x00075639 File Offset: 0x00073839
		public static void PostInferenceDataCollectionProgressNotification(Guid mailboxGuid, string status)
		{
			InferenceDataCollectionAssistant.PostInferenceDataCollectionNotification("InferenceDataCollectionProgressNotification", string.Format("{0}/{1}", mailboxGuid.ToString(), status));
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x0007565D File Offset: 0x0007385D
		public static void PostInferenceDataCollectionNotification(string notificationName, string additionalInfo)
		{
			EventNotificationItem.Publish(ExchangeComponent.Inference.Name, ExchangeComponent.Inference.Name, string.Format("{0}/{1}", notificationName, additionalInfo), notificationName, ResultSeverityLevel.Informational, true);
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x00075687 File Offset: 0x00073887
		public void OnWorkCycleCheckpoint()
		{
			this.tracer.TraceFunction((long)this.GetHashCode(), "InferenceDataCollectionAssistant.OnWorkCycleCheckpoint");
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x000756A0 File Offset: 0x000738A0
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x000756A4 File Offset: 0x000738A4
		public override AssistantTaskContext InitialStep(AssistantTaskContext context)
		{
			this.tracer.TraceFunction((long)this.GetHashCode(), "InferenceDataCollectionAssistant.InitialStep");
			StoreSession storeSession = context.Args.StoreSession;
			if (storeSession.MailboxOwner.RecipientTypeDetails != RecipientTypeDetails.UserMailbox && storeSession.MailboxOwner.RecipientTypeDetails != RecipientTypeDetails.LinkedMailbox && storeSession.MailboxOwner.RecipientTypeDetails != RecipientTypeDetails.TeamMailbox && storeSession.MailboxOwner.RecipientTypeDetails != RecipientTypeDetails.GroupMailbox && storeSession.MailboxOwner.RecipientTypeDetails != RecipientTypeDetails.SharedMailbox)
			{
				this.diagnosticLogger.LogInformation("Skipping mailbox with guid {0} and display name {1} since this is a {2}, which we are not interested in.", new object[]
				{
					storeSession.MailboxGuid,
					storeSession.MailboxOwner.MailboxInfo.DisplayName,
					storeSession.MailboxOwner.RecipientTypeDetails.ToString()
				});
				return null;
			}
			if (this.collectionContext.DatacenterID == null)
			{
				this.collectionContext.DatacenterID = this.GetForestFQDN();
			}
			this.diagnosticLogger.LogInformation("The local forest fqdn {0}", new object[]
			{
				this.collectionContext.DatacenterID
			});
			this.diagnosticLogger.LogInformation("Initiating collection for mailbox with guid {0} and display name {1}.", new object[]
			{
				storeSession.MailboxGuid,
				storeSession.MailboxOwner.MailboxInfo.DisplayName
			});
			this.diagnosticLogger.LogDebug("Yielding with context for initial collection step.", new object[0]);
			return new InferenceDataCollectionTaskContext(context.MailboxData, context.Job, new AssistantStep(this.CollectionStep), new MailboxProcessingState());
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0007583C File Offset: 0x00073A3C
		private AssistantTaskContext CollectionStep(AssistantTaskContext context)
		{
			this.tracer.TraceFunction((long)this.GetHashCode(), "InferenceDataCollectionAssistant.CollectionStep");
			ExAssert.RetailAssert(context != null, "Collection step invoked with a null task context");
			StoreSession storeSession = context.Args.StoreSession;
			InferenceDataCollectionTaskContext inferenceDataCollectionTaskContext = context as InferenceDataCollectionTaskContext;
			ExAssert.RetailAssert(inferenceDataCollectionTaskContext != null, "Collection step invoked with an invalid task context. {0}", new object[]
			{
				context.GetType().FullName
			});
			this.diagnosticLogger.LogInformation("Starting collection task. CollectionGuid={0}, Init={1}, Watermark={2}, MailboxGuid={3}, Name={4}", new object[]
			{
				inferenceDataCollectionTaskContext.MailboxProcessingState.CollectionGuid,
				inferenceDataCollectionTaskContext.MailboxProcessingState.IsInitialized,
				(inferenceDataCollectionTaskContext.MailboxProcessingState.Watermark == null) ? "None" : inferenceDataCollectionTaskContext.MailboxProcessingState.Watermark.DocumentId.ToString(),
				storeSession.MailboxGuid,
				storeSession.MailboxOwner.MailboxInfo.DisplayName
			});
			Exception exception = null;
			MailboxProcessor.ProcessingResult processingResult;
			try
			{
				MailboxProcessor mailboxProcessor = new MailboxProcessor(this.collectionContext, storeSession, inferenceDataCollectionTaskContext.MailboxProcessingState);
				processingResult = mailboxProcessor.Process();
			}
			catch (NonUniqueRecipientException ex)
			{
				processingResult = 1;
				exception = ex;
			}
			AssistantTaskContext result;
			if (processingResult == 2)
			{
				this.diagnosticLogger.LogInformation("Yielding collection task. CollectionGuid={0}, Init={1}, Watermark={2}, MailboxGuid={3}, Name={4}", new object[]
				{
					inferenceDataCollectionTaskContext.MailboxProcessingState.CollectionGuid,
					inferenceDataCollectionTaskContext.MailboxProcessingState.IsInitialized,
					(inferenceDataCollectionTaskContext.MailboxProcessingState.Watermark == null) ? "None" : inferenceDataCollectionTaskContext.MailboxProcessingState.Watermark.DocumentId.ToString(),
					storeSession.MailboxGuid,
					storeSession.MailboxOwner.MailboxInfo.DisplayName
				});
				result = new InferenceDataCollectionTaskContext(context.MailboxData, context.Job, new AssistantStep(this.CollectionStep), inferenceDataCollectionTaskContext.MailboxProcessingState);
			}
			else
			{
				storeSession.Mailbox[MailboxSchema.InferenceDataCollectionProcessingState] = new byte[]
				{
					1
				};
				storeSession.Mailbox.Save();
				if (processingResult == 1)
				{
					this.diagnosticLogger.LogError("Failed collection task. CollectionGuid={0}, Init={1}, Watermark={2}, MailboxGuid={3}, Name={4}, Exception={5}", new object[]
					{
						inferenceDataCollectionTaskContext.MailboxProcessingState.CollectionGuid,
						inferenceDataCollectionTaskContext.MailboxProcessingState.IsInitialized,
						(inferenceDataCollectionTaskContext.MailboxProcessingState.Watermark == null) ? "None" : inferenceDataCollectionTaskContext.MailboxProcessingState.Watermark.DocumentId.ToString(),
						storeSession.MailboxGuid,
						storeSession.MailboxOwner.MailboxInfo.DisplayName,
						Util.StringizeException(exception)
					});
				}
				else
				{
					this.diagnosticLogger.LogInformation("Finished collection task. CollectionGuid={0}, Init={1}, Watermark={2}, MailboxGuid={3}, Name={4}", new object[]
					{
						inferenceDataCollectionTaskContext.MailboxProcessingState.CollectionGuid,
						inferenceDataCollectionTaskContext.MailboxProcessingState.IsInitialized,
						(inferenceDataCollectionTaskContext.MailboxProcessingState.Watermark == null) ? "None" : inferenceDataCollectionTaskContext.MailboxProcessingState.Watermark.DocumentId.ToString(),
						storeSession.MailboxGuid,
						storeSession.MailboxOwner.MailboxInfo.DisplayName
					});
				}
				result = null;
			}
			InferenceDataCollectionAssistant.PostInferenceDataCollectionProgressNotification(storeSession.MailboxGuid, processingResult.ToString());
			if (processingResult == null)
			{
				InferenceDataCollectionAssistant.PostInferenceDataCollectionSuccessNotification(storeSession.MailboxGuid);
			}
			return result;
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x00075C04 File Offset: 0x00073E04
		private string GetForestFQDN()
		{
			this.diagnosticLogger.LogDebug("Computing forest FQDN.", new object[0]);
			string forestFqdn = string.Empty;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ADForest localForest = ADForest.GetLocalForest();
				forestFqdn = localForest.Fqdn;
			}, 1);
			if (!adoperationResult.Succeeded)
			{
				this.diagnosticLogger.LogError("Unable to get the local ADForest {0}", new object[]
				{
					adoperationResult.Exception.Message
				});
			}
			return forestFqdn;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x00075C7F File Offset: 0x00073E7F
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x00075C87 File Offset: 0x00073E87
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x00075C8F File Offset: 0x00073E8F
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000C45 RID: 3141
		public const string DataCollectionSuccessNotificationName = "InferenceDataCollectionSuccessNotification";

		// Token: 0x04000C46 RID: 3142
		public const string DataCollectionProgressNotificationName = "InferenceDataCollectionProgressNotification";

		// Token: 0x04000C47 RID: 3143
		private readonly Trace tracer;

		// Token: 0x04000C48 RID: 3144
		private readonly IDiagnosticLogger diagnosticLogger;

		// Token: 0x04000C49 RID: 3145
		private readonly ICollectionContext collectionContext;
	}
}
