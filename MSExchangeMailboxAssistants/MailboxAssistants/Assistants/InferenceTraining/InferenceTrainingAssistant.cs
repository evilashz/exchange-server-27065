using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Clutter;
using Microsoft.Exchange.Data.Storage.Inference.GroupingModel;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.InferenceTraining;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Inference.EventLog;
using Microsoft.Exchange.Inference.Learning;
using Microsoft.Exchange.Inference.Mdb;
using Microsoft.Exchange.Inference.Mdb.OutlookActivity;
using Microsoft.Exchange.Inference.MdbCommon;
using Microsoft.Exchange.Inference.Pipeline;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.Core.Pipeline;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.InferenceTraining
{
	// Token: 0x020001C7 RID: 455
	internal sealed class InferenceTrainingAssistant : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase, IDisposable
	{
		// Token: 0x06001188 RID: 4488 RVA: 0x000666AC File Offset: 0x000648AC
		static InferenceTrainingAssistant()
		{
			DiagnosticsSessionFactory.SetDefaults(Guid.Parse("83AAE9D3-D243-482C-A39E-BFA5BC8F1113"), "InferenceTrainingAssistant", "Inference Diagnostics Logs", Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\InferenceTraining"), "Inference_", "InferenceLogs");
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x00066710 File Offset: 0x00064910
		public InferenceTrainingAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName, InferenceTrainingStatusLogger trainingStatusLogger, InferenceTruthLabelsStatusLogger truthLabelsStatusLogger, GroupingModelTrainingConfiguration groupingModelTrainingConfiguration, GroupingModelTrainingStatusLogger groupingModelTrainingStatusLogger, bool isTruthLabelsLoggingEnabled) : base(databaseInfo, name, nonLocalizedName)
		{
			this.DiagnosticsSession = Microsoft.Exchange.Search.Core.Diagnostics.DiagnosticsSession.CreateComponentDiagnosticsSession(base.NonLocalizedName, ExTraceGlobals.AssistantTracer, (long)this.GetHashCode());
			this.trainingStatusLogger = trainingStatusLogger;
			this.truthLabelsStatusLogger = truthLabelsStatusLogger;
			this.groupingModelTrainingStatusLogger = groupingModelTrainingStatusLogger;
			this.isTruthLabelsLoggingEnabled = isTruthLabelsLoggingEnabled;
			this.orgContentExtractor = new OrganizationContentExtractor();
			this.groupingModelTrainingPipeline = new GroupingModelTrainingPipeline(groupingModelTrainingConfiguration, this.DiagnosticsSession, this.groupingModelTrainingStatusLogger);
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x00066787 File Offset: 0x00064987
		public static Hookable<IGroupingModelConfiguration> HookableGroupingModelConfiguration
		{
			get
			{
				return InferenceTrainingAssistant.hookableGroupingModelConfiguration;
			}
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00066790 File Offset: 0x00064990
		public void OnWorkCycleCheckpoint()
		{
			if (this.trainingPipeline == null)
			{
				this.trainingPipeline = InferencePipelineUtil.CreateAndStartTrainingPipeline(base.DatabaseInfo.Guid.ToString(), this.DiagnosticsSession, "InferenceTrainingPipelineDefinition.xml", "Training", InferenceTrainingAssistant.InferencePipelineVersion, out this.trainingPipelineContext);
				InferenceModel.GetInstance(this.trainingPipelineContext.GetProperty<string>(DocumentSchema.PipelineInstanceName)).Reset();
			}
			this.DiagnosticsSession.Assert(this.trainingPipeline != null, "Training Pipeline is null", new object[0]);
			this.DiagnosticsSession.Assert(this.trainingPipelineContext != null, "Training Pipeline Context is null", new object[0]);
			if (this.trainingFeeder == null)
			{
				this.trainingFeeder = new InferenceTrainingFeeder(this.trainingPipeline, this.trainingPipelineContext, "InferenceTrainingAssistant", this.trainingStatusLogger, this.truthLabelsStatusLogger);
			}
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00066872 File Offset: 0x00064A72
		public void Dispose()
		{
			this.trainingFeeder = null;
			if (this.trainingPipeline != null)
			{
				this.trainingPipeline.Dispose();
				this.trainingPipeline = null;
			}
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00066895 File Offset: 0x00064A95
		protected override void OnShutdownInternal()
		{
			this.DiagnosticsSession.TraceDebug("ShutDown is called on the service. Sending Abort processing signal to the pipeline", new object[0]);
			if (this.trainingPipelineContext != null)
			{
				InferencePipelineUtil.SetAbortOnProcessing(this.trainingPipelineContext);
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x000668D4 File Offset: 0x00064AD4
		public override AssistantTaskContext InitialStep(AssistantTaskContext context)
		{
			DateTime utcNow = DateTime.UtcNow;
			DateTime? dateTime = null;
			DateTime? dateTime2 = null;
			DateTime? dateTime3 = null;
			DateTime? dateTime4 = null;
			Guid guid = Guid.Empty;
			AssistantTaskContext result;
			try
			{
				this.ValidateContext(context);
				guid = context.Args.StoreSession.MailboxGuid;
				ExDateTime? valueOrDefault = context.Args.StoreSession.Mailbox.GetValueOrDefault<ExDateTime?>(MailboxSchema.InferenceTrainingLastAttemptTimestamp, null);
				if (valueOrDefault != null)
				{
					dateTime = new DateTime?(valueOrDefault.Value.UniversalTime);
				}
				valueOrDefault = context.Args.StoreSession.Mailbox.GetValueOrDefault<ExDateTime?>(MailboxSchema.InferenceTrainingLastSuccessTimestamp, null);
				if (valueOrDefault != null)
				{
					dateTime2 = new DateTime?(valueOrDefault.Value.UniversalTime);
				}
				valueOrDefault = context.Args.StoreSession.Mailbox.GetValueOrDefault<ExDateTime?>(MailboxSchema.InferenceTruthLoggingLastAttemptTimestamp, null);
				if (valueOrDefault != null)
				{
					dateTime3 = new DateTime?(valueOrDefault.Value.UniversalTime);
				}
				valueOrDefault = context.Args.StoreSession.Mailbox.GetValueOrDefault<ExDateTime?>(MailboxSchema.InferenceTruthLoggingLastSuccessTimestamp, null);
				if (valueOrDefault != null)
				{
					dateTime4 = new DateTime?(valueOrDefault.Value.UniversalTime);
				}
				MailboxSession mailboxSession = context.Args.StoreSession as MailboxSession;
				if (mailboxSession == null)
				{
					string text = "Reason=NonMailboxSession";
					this.trainingStatusLogger.LogStatus(guid, 4, new DateTime?(utcNow), dateTime, dateTime2, text);
					this.truthLabelsStatusLogger.LogStatus(mailboxSession.MailboxGuid, 4, new DateTime?(utcNow), dateTime3, dateTime4, text);
					this.groupingModelTrainingStatusLogger.LogStatus(guid, 4, text);
					result = null;
				}
				else if (mailboxSession.MailboxOwner.RecipientTypeDetails != RecipientTypeDetails.UserMailbox && mailboxSession.MailboxOwner.RecipientTypeDetails != RecipientTypeDetails.LinkedMailbox)
				{
					string text2 = string.Format("Reason=NonUserMailbox#Name={0}#Type={1}", mailboxSession.MailboxOwner.MailboxInfo.DisplayName, mailboxSession.MailboxOwner.RecipientTypeDetails.ToString());
					this.trainingStatusLogger.LogStatus(mailboxSession.MailboxGuid, 4, new DateTime?(utcNow), dateTime, dateTime2, text2);
					this.truthLabelsStatusLogger.LogStatus(mailboxSession.MailboxGuid, 4, new DateTime?(utcNow), dateTime3, dateTime4, text2);
					this.groupingModelTrainingStatusLogger.LogStatus(mailboxSession.MailboxGuid, 4, text2);
					result = null;
				}
				else
				{
					VariantConfigurationSnapshot flightFeatures = FlightModule.GetFlightFeatures(mailboxSession);
					Exception ex;
					OrganizationContext organizationInformation = this.orgContentExtractor.GetOrganizationInformation(mailboxSession.MailboxOwner, new ADRecipientInfo.TraceDelegate(this.DiagnosticsSession.TraceDebug), new ADRecipientInfo.TraceDelegate(this.DiagnosticsSession.TraceError), ref ex);
					FolderDataSelectionConfig.RefreshSettings();
					List<Exception> list = new List<Exception>();
					if (ex != null)
					{
						list.Add(ex);
					}
					OutlookActivityManager.SafeProcess(mailboxSession);
					ActivityHistory activityHistory = new ActivityHistory(mailboxSession, utcNow);
					ModelVersionSelector modelVersionSelector = InferenceXsoUtil.CreateModelVersionSelector(ServerModelConfigurationWrapper.CurrentWrapper, mailboxSession, delegate(string str)
					{
						this.DiagnosticsSession.TraceDebug(str, new object[0]);
					});
					result = new InferenceTrainingTaskContext(context.MailboxData, context.Job, new AssistantStep(this.TrainStep), new MailboxTrainingState(modelVersionSelector, organizationInformation, activityHistory, utcNow, dateTime, dateTime2, flightFeatures, list), new MailboxTruthLoggingState(utcNow, dateTime3, dateTime4, activityHistory));
				}
			}
			catch (Exception ex2)
			{
				string text3 = string.Format("Reason=InitialStepFailed#Exception={0}", InferenceCommonUtility.StringizeException(ex2));
				this.trainingStatusLogger.LogStatus(guid, 3, new DateTime?(utcNow), dateTime, dateTime2, text3);
				this.truthLabelsStatusLogger.LogStatus(guid, 3, new DateTime?(utcNow), dateTime3, dateTime4, text3);
				this.groupingModelTrainingStatusLogger.LogStatus(guid, 3, text3);
				throw;
			}
			return result;
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00066CA0 File Offset: 0x00064EA0
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00066CD0 File Offset: 0x00064ED0
		private AssistantTaskContext TrainStep(AssistantTaskContext context)
		{
			Guid guid = Guid.Empty;
			MailboxTrainingState mailboxTrainingState = null;
			MailboxTruthLoggingState mailboxTruthLoggingState = null;
			AssistantTaskContext result;
			try
			{
				InferenceTrainingTaskContext assistantTaskContext = null;
				this.ValidateContext(context, delegate(AssistantTaskContext taskContext)
				{
					assistantTaskContext = (taskContext as InferenceTrainingTaskContext);
					ExAssert.RetailAssert(assistantTaskContext != null, "Assistant did not return InferenceTrainingTaskContext");
				});
				MailboxSession mailboxSession = assistantTaskContext.Args.StoreSession as MailboxSession;
				guid = assistantTaskContext.Args.StoreSession.MailboxGuid;
				mailboxTrainingState = assistantTaskContext.MailboxTrainingState;
				mailboxTruthLoggingState = assistantTaskContext.MailboxTruthLoggingState;
				if (mailboxTrainingState.PrepareNextTrainingStep())
				{
					this.DiagnosticsSession.TraceDebug<IExchangePrincipal>("{0}: Begin process mailbox for training", mailboxSession.MailboxOwner);
					this.trainingFeeder.TrainMailbox(mailboxSession, mailboxTrainingState);
					this.DiagnosticsSession.TraceDebug<IExchangePrincipal>("{0}: End process mailbox for training", mailboxSession.MailboxOwner);
					mailboxTrainingState.MarkTrainingStepAsCompleted();
					mailboxSession.Mailbox.Save();
					result = new InferenceTrainingTaskContext(assistantTaskContext.MailboxData, assistantTaskContext.Job, new AssistantStep(this.TrainStep), mailboxTrainingState, mailboxTruthLoggingState);
				}
				else
				{
					this.DiagnosticsSession.LogEvent(MSExchangeInferenceEventLogConstants.Tuple_TrainingStatisticsForMailbox, new object[]
					{
						mailboxSession.MailboxGuid.ToString()
					});
					result = new InferenceTrainingTaskContext(assistantTaskContext.MailboxData, assistantTaskContext.Job, new AssistantStep(this.TruthLogStep), mailboxTrainingState, mailboxTruthLoggingState);
				}
			}
			catch (Exception ex)
			{
				string text = string.Format("Reason=TrainStepFailed#Exception={0}", InferenceCommonUtility.StringizeException(ex));
				this.trainingStatusLogger.LogStatus(guid, 3, new DateTime?((mailboxTrainingState == null) ? DateTime.UtcNow : mailboxTrainingState.CurrentAttemptTimestamp), (mailboxTrainingState == null) ? null : mailboxTrainingState.LastAttemptTimestamp, (mailboxTrainingState == null) ? null : mailboxTrainingState.LastSuccessTimestamp, text);
				this.truthLabelsStatusLogger.LogStatus(guid, 3, new DateTime?((mailboxTruthLoggingState == null) ? DateTime.UtcNow : mailboxTruthLoggingState.CurrentAttemptTimestamp), (mailboxTruthLoggingState == null) ? null : mailboxTruthLoggingState.LastAttemptTimestamp, (mailboxTruthLoggingState == null) ? null : mailboxTruthLoggingState.LastSuccessTimestamp, text);
				this.groupingModelTrainingStatusLogger.LogStatus(guid, 3, text);
				if (ex is AbortOnProcessingRequestedException)
				{
					result = null;
				}
				else
				{
					if (!(ex is QuotaExceededException))
					{
						throw;
					}
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00066F64 File Offset: 0x00065164
		private AssistantTaskContext TruthLogStep(AssistantTaskContext context)
		{
			Guid guid = Guid.Empty;
			MailboxTruthLoggingState mailboxTruthLoggingState = null;
			AssistantTaskContext result;
			try
			{
				InferenceTrainingTaskContext assistantTaskContext = null;
				this.ValidateContext(context, delegate(AssistantTaskContext taskContext)
				{
					assistantTaskContext = (taskContext as InferenceTrainingTaskContext);
					ExAssert.RetailAssert(assistantTaskContext != null, "Assistant did not return InferenceTrainingTaskContext");
				});
				MailboxSession mailboxSession = assistantTaskContext.Args.StoreSession as MailboxSession;
				MailboxTrainingState mailboxTrainingState = assistantTaskContext.MailboxTrainingState;
				mailboxTruthLoggingState = assistantTaskContext.MailboxTruthLoggingState;
				guid = assistantTaskContext.Args.StoreSession.MailboxGuid;
				if (this.isTruthLabelsLoggingEnabled)
				{
					this.DiagnosticsSession.TraceDebug<IExchangePrincipal>("{0}: Begin process mailbox for truth labels logging", mailboxSession.MailboxOwner);
					this.trainingFeeder.LogTruthLabels(mailboxSession, mailboxTruthLoggingState);
					mailboxSession.Mailbox.Save();
					this.DiagnosticsSession.TraceDebug<IExchangePrincipal>("{0}: End process mailbox for truth labels logging", mailboxSession.MailboxOwner);
				}
				else
				{
					this.truthLabelsStatusLogger.LogStatus(guid, 5, new DateTime?((mailboxTruthLoggingState == null) ? DateTime.UtcNow : mailboxTruthLoggingState.CurrentAttemptTimestamp), (mailboxTruthLoggingState == null) ? null : mailboxTruthLoggingState.LastAttemptTimestamp, (mailboxTruthLoggingState == null) ? null : mailboxTruthLoggingState.LastSuccessTimestamp, "Truth labels logging is disabled");
				}
				result = new InferenceTrainingTaskContext(assistantTaskContext.MailboxData, assistantTaskContext.Job, new AssistantStep(this.GroupingModelTrainingStep), mailboxTrainingState, mailboxTruthLoggingState);
			}
			catch (Exception ex)
			{
				string text = string.Format("Reason=TruthLogStepFailed#Exception={0}", InferenceCommonUtility.StringizeException(ex));
				this.truthLabelsStatusLogger.LogStatus(guid, 3, new DateTime?((mailboxTruthLoggingState == null) ? DateTime.UtcNow : mailboxTruthLoggingState.CurrentAttemptTimestamp), (mailboxTruthLoggingState == null) ? null : mailboxTruthLoggingState.LastAttemptTimestamp, (mailboxTruthLoggingState == null) ? null : mailboxTruthLoggingState.LastSuccessTimestamp, text);
				this.groupingModelTrainingStatusLogger.LogStatus(guid, 3, text);
				if (ex is OutOfMemoryException || ex is StackOverflowException || ex is AccessViolationException)
				{
					throw;
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00067194 File Offset: 0x00065394
		private AssistantTaskContext GroupingModelTrainingStep(AssistantTaskContext context)
		{
			Guid guid = Guid.Empty;
			try
			{
				InferenceTrainingTaskContext assistantTaskContext = null;
				this.ValidateContext(context, delegate(AssistantTaskContext taskContext)
				{
					assistantTaskContext = (taskContext as InferenceTrainingTaskContext);
					ExAssert.RetailAssert(assistantTaskContext != null, "Assistant did not return InferenceTrainingTaskContext");
				});
				MailboxSession mailboxSession = assistantTaskContext.Args.StoreSession as MailboxSession;
				MailboxTrainingState mailboxTrainingState = assistantTaskContext.MailboxTrainingState;
				guid = assistantTaskContext.Args.StoreSession.MailboxGuid;
				if (mailboxTrainingState.FlightFeatures.Inference.InferenceGroupingModel.Enabled)
				{
					this.DiagnosticsSession.TraceDebug<IExchangePrincipal>("{0}: Begin process mailbox for grouping model", mailboxSession.MailboxOwner);
					GroupingModelVersionSelector groupingModelVersionSelector = new GroupingModelVersionSelector(InferenceTrainingAssistant.HookableGroupingModelConfiguration.Value);
					GroupingModelTrainingContext groupingModelTrainingContext = new GroupingModelTrainingContext
					{
						ModelVersion = groupingModelVersionSelector.GetModelVersionToTrain()
					};
					this.groupingModelTrainingPipeline.TrainGroupingModel(mailboxSession, groupingModelTrainingContext);
					this.DiagnosticsSession.TraceDebug<IExchangePrincipal>("{0}: End process mailbox for grouping model", mailboxSession.MailboxOwner);
				}
				else
				{
					this.groupingModelTrainingStatusLogger.LogStatus(guid, 4, "User is not a member of grouping model flight");
				}
			}
			catch (Exception ex)
			{
				string text = string.Format("Reason=GroupingModelTrainingStepFailed#Exception={0}", InferenceCommonUtility.StringizeException(ex));
				this.groupingModelTrainingStatusLogger.LogStatus(guid, 3, text);
				if (ex is OutOfMemoryException || ex is StackOverflowException || ex is AccessViolationException)
				{
					throw;
				}
				if (ex is QuotaExceededException)
				{
					return null;
				}
			}
			return null;
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00067300 File Offset: 0x00065500
		private void ValidateContext(AssistantTaskContext context)
		{
			this.ValidateContext(context, null);
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0006730C File Offset: 0x0006550C
		private void ValidateContext(AssistantTaskContext context, Action<AssistantTaskContext> actionDelegate)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (context.Args == null)
			{
				throw new ArgumentNullException("context.Args");
			}
			if (context.Args.StoreSession == null)
			{
				throw new ArgumentNullException("context.Args.StoreSession");
			}
			if (actionDelegate != null)
			{
				actionDelegate(context);
			}
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0006735C File Offset: 0x0006555C
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00067364 File Offset: 0x00065564
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x0006736C File Offset: 0x0006556C
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000AE7 RID: 2791
		internal const string PipelineDefinitionFileName = "InferenceTrainingPipelineDefinition.xml";

		// Token: 0x04000AE8 RID: 2792
		internal const string TrainingPipelineName = "Training";

		// Token: 0x04000AE9 RID: 2793
		internal static readonly Version InferencePipelineVersion = new Version(ModelConfiguration.MaxSupportedSerializationVersion, 0);

		// Token: 0x04000AEA RID: 2794
		private static readonly Hookable<IGroupingModelConfiguration> hookableGroupingModelConfiguration = Hookable<IGroupingModelConfiguration>.Create(true, GroupingModelConfiguration.LoadFromFile().AsReadOnly());

		// Token: 0x04000AEB RID: 2795
		private readonly IDiagnosticsSession DiagnosticsSession;

		// Token: 0x04000AEC RID: 2796
		private InferenceTrainingFeeder trainingFeeder;

		// Token: 0x04000AED RID: 2797
		private Pipeline trainingPipeline;

		// Token: 0x04000AEE RID: 2798
		private PipelineContext trainingPipelineContext;

		// Token: 0x04000AEF RID: 2799
		private GroupingModelTrainingPipeline groupingModelTrainingPipeline;

		// Token: 0x04000AF0 RID: 2800
		private readonly InferenceTrainingStatusLogger trainingStatusLogger;

		// Token: 0x04000AF1 RID: 2801
		private readonly InferenceTruthLabelsStatusLogger truthLabelsStatusLogger;

		// Token: 0x04000AF2 RID: 2802
		private readonly GroupingModelTrainingStatusLogger groupingModelTrainingStatusLogger;

		// Token: 0x04000AF3 RID: 2803
		private readonly bool isTruthLabelsLoggingEnabled;

		// Token: 0x04000AF4 RID: 2804
		private readonly OrganizationContentExtractor orgContentExtractor;
	}
}
