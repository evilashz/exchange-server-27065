using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management.Automation;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Assistants.Diagnostics;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x0200017D RID: 381
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderSplitProcessor : PublicFolderProcessor, IDisposeTrackable, IDisposable
	{
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x0005A67B File Offset: 0x0005887B
		// (set) Token: 0x06000F24 RID: 3876 RVA: 0x0005A683 File Offset: 0x00058883
		internal IPublicFolderSplitState SplitState
		{
			get
			{
				return this.splitState;
			}
			set
			{
				this.splitState = value;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (set) Token: 0x06000F25 RID: 3877 RVA: 0x0005A68C File Offset: 0x0005888C
		internal bool EnabledForTest
		{
			set
			{
				this.enabledForTest = value;
			}
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0005A698 File Offset: 0x00058898
		public PublicFolderSplitProcessor(IPublicFolderSession publicFolderSession, ITracer tracer) : this(publicFolderSession, tracer, new XSOFactory(), new PublicFolderSplitLogger(publicFolderSession, "PublicFolderSplitLog"), new PublicFolderSplitLogger(publicFolderSession, "PublicFolderSplitHealth"), new AssistantRunspaceFactory())
		{
			this.quotaVerifier = new SplitQuotaVerifier(publicFolderSession, this.logger, this.powershellFactory);
			this.operationFactory = new SplitOperationFactory(publicFolderSession, this.logger, this.powershellFactory, this.xsoFactory);
			this.splitStateAdapter = new SplitStateAdapter(publicFolderSession, this.xsoFactory, this.logger);
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0005A71C File Offset: 0x0005891C
		internal PublicFolderSplitProcessor(IPublicFolderSession publicFolderSession, ITracer tracer, IPublicFolderMailboxLoggerBase logger, IPublicFolderMailboxLoggerBase completionLogger) : this(publicFolderSession, tracer, new XSOFactory(), logger, completionLogger, new AssistantRunspaceFactory())
		{
			this.quotaVerifier = new SplitQuotaVerifier(publicFolderSession, this.logger, this.powershellFactory);
			this.operationFactory = new SplitOperationFactory(publicFolderSession, this.logger, this.powershellFactory, this.xsoFactory);
			this.splitStateAdapter = new SplitStateAdapter(publicFolderSession, this.xsoFactory, this.logger);
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0005A78C File Offset: 0x0005898C
		internal PublicFolderSplitProcessor(IPublicFolderSession publicFolderSession, ITracer tracer, IPublicFolderMailboxLoggerBase logger, IPublicFolderMailboxLoggerBase completionLogger, IAssistantRunspaceFactory powershellFactory) : this(publicFolderSession, tracer, new XSOFactory(), logger, completionLogger, powershellFactory)
		{
			this.quotaVerifier = new SplitQuotaVerifier(publicFolderSession, this.logger, this.powershellFactory);
			this.operationFactory = new SplitOperationFactory(publicFolderSession, this.logger, this.powershellFactory, this.xsoFactory);
			this.splitStateAdapter = new SplitStateAdapter(publicFolderSession, this.xsoFactory, this.logger);
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0005A7F9 File Offset: 0x000589F9
		internal PublicFolderSplitProcessor(IPublicFolderSession publicFolderSession, ITracer tracer, IXSOFactory xsoFactory, ISplitQuotaVerifier quotaVerifier, IPublicFolderMailboxLoggerBase logger, IPublicFolderMailboxLoggerBase completionLogger, ISplitStateAdapter adapter, ISplitOperationFactory operationFactory, IAssistantRunspaceFactory powershellFactory) : this(publicFolderSession, tracer, xsoFactory, logger, completionLogger, powershellFactory)
		{
			this.splitStateAdapter = adapter;
			this.quotaVerifier = quotaVerifier;
			this.operationFactory = operationFactory;
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0005A822 File Offset: 0x00058A22
		private PublicFolderSplitProcessor(IPublicFolderSession publicFolderSession, ITracer tracer, IXSOFactory xsoFactory, IPublicFolderMailboxLoggerBase logger, IPublicFolderMailboxLoggerBase completionLogger, IAssistantRunspaceFactory powershellFactory) : base(publicFolderSession, tracer)
		{
			this.disposeTracker = this.GetDisposeTracker();
			this.logger = logger;
			this.completionLogger = completionLogger;
			this.xsoFactory = xsoFactory;
			this.powershellFactory = powershellFactory;
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0005A858 File Offset: 0x00058A58
		internal static XElement GetDiagnosticInfo(DiagnosticsArgument arguments)
		{
			XElement xelement = new XElement("PublicFolderSplit");
			if (arguments.HasArgument("mailbox"))
			{
				Guid argument = arguments.GetArgument<Guid>("mailbox");
				if (PublicFolderSplitProcessor.SplitStates.ContainsKey(argument))
				{
					PublicFolderSplitProcessor.AddSplitStateDiagnostic(argument, PublicFolderSplitProcessor.SplitStates[argument], xelement);
				}
				else
				{
					string splitDate = PublicFolderSplitProcessor.SplitDates.ContainsKey(argument) ? PublicFolderSplitProcessor.SplitDates[argument].ToString() : string.Empty;
					PublicFolderSplitProcessor.AddSplitDateDiagnostic(argument, splitDate, xelement);
				}
				return xelement;
			}
			if (arguments.HasArgument("recent"))
			{
				foreach (Guid guid in PublicFolderSplitProcessor.SplitStates.Keys)
				{
					PublicFolderSplitProcessor.AddSplitStateDiagnostic(guid, PublicFolderSplitProcessor.SplitStates[guid], xelement);
				}
			}
			if (arguments.HasArgument("old"))
			{
				foreach (Guid guid2 in PublicFolderSplitProcessor.SplitDates.Keys)
				{
					PublicFolderSplitProcessor.AddSplitDateDiagnostic(guid2, PublicFolderSplitProcessor.SplitDates[guid2].ToString(), xelement);
				}
			}
			return xelement;
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0005A9C0 File Offset: 0x00058BC0
		public override void Invoke()
		{
			string text = null;
			if (!this.IsSplitEnabled(out text))
			{
				if (text != null)
				{
					this.logger.LogEvent(LogEventType.Verbose, string.Format("PublicFolderSplitProcessor::{0}::Invoke - Split processing disabled for mailbox {1} for reason {2}", this.GetHashCode(), this.publicFolderSession.DisplayAddress, text));
				}
				return;
			}
			this.logger.LogEvent(LogEventType.Verbose, string.Format("PublicFolderSplitProcessor::{0}::Invoke - Begin processing {1}", this.GetHashCode(), this.publicFolderSession.DisplayAddress));
			try
			{
				this.ReadSplitState();
				ISplitOperation splitOperation = null;
				bool flag = false;
				while (!flag && this.IsAnyProcessingNeeded(out splitOperation))
				{
					splitOperation.Invoke();
					flag = this.CheckPoint(splitOperation);
				}
			}
			catch (SplitProcessorException ex)
			{
				this.logger.LogEvent(LogEventType.Error, string.Format("PublicFolderSplitProcessor::{0}::Invoke - Unable to process {1}. Exception {2}", this.GetHashCode(), this.publicFolderSession.DisplayAddress, ex));
				if (!ex.IsTransient)
				{
					this.MarkOverallSplitJobComplete(ex, null);
				}
				try
				{
					this.SaveSplitState();
				}
				catch (SplitProcessorException arg)
				{
					this.logger.LogEvent(LogEventType.Error, string.Format("PublicFolderSplitProcessor::{0}::Invoke - Unable to save the state for {1}. Exception {2}", this.GetHashCode(), this.publicFolderSession.DisplayAddress, arg));
				}
			}
			catch (RuntimeException ex2)
			{
				this.LogCompletion(ex2);
				throw new AIGrayException(ex2);
			}
			catch (Exception unexpectedError)
			{
				this.LogCompletion(unexpectedError);
				throw;
			}
			if (this.splitState.ProgressState == SplitProgressState.SplitCompleted)
			{
				this.LogCompletion(null);
			}
			this.logger.LogEvent(LogEventType.Verbose, string.Format("PublicFolderSplitProcessor::{0}::Invoke - End processing {1}", this.GetHashCode(), this.publicFolderSession.DisplayAddress));
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0005AB70 File Offset: 0x00058D70
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<PublicFolderSplitProcessor>(this);
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0005AB78 File Offset: 0x00058D78
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0005AB8D File Offset: 0x00058D8D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x0005AB9C File Offset: 0x00058D9C
		public void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					if (this.powershellFactory != null)
					{
						this.powershellFactory.Dispose();
					}
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
						this.disposeTracker = null;
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x0005ABE8 File Offset: 0x00058DE8
		private bool IsSplitEnabled(out string reason)
		{
			reason = null;
			if (this.enabledForTest)
			{
				return true;
			}
			if (!Globals.IsDatacenter)
			{
				reason = null;
				return false;
			}
			if (!PublicFolderSplitConfig.Instance.SplitProcessingEnabled)
			{
				reason = "Disabled by config";
				return false;
			}
			return this.IsFlightingEnabled(out reason);
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0005AC20 File Offset: 0x00058E20
		private bool IsFlightingEnabled(out string reason)
		{
			reason = null;
			ADRecipient adrecipient = DirectoryHelper.ReadADRecipient(this.publicFolderSession.MailboxOwner.MailboxInfo.MailboxGuid, this.publicFolderSession.MailboxOwner.MailboxInfo.IsArchive, this.publicFolderSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid));
			if (adrecipient == null)
			{
				reason = string.Format("ReadADRecipient returned null. MailboxOwner: {0} Organization: {1} LegacyExchangeDN: {2}", this.publicFolderSession.MailboxOwner.ToString(), this.publicFolderSession.MailboxOwner.MailboxInfo.OrganizationId.ToString(), this.publicFolderSession.MailboxOwner.LegacyDn);
				return false;
			}
			ADUser aduser = adrecipient as ADUser;
			if (aduser == null)
			{
				reason = string.Format("Failed to cast ADRecipient to ADUser. MailboxOwner: {0} Organization: {1} LegacyExchangeDN: {2} OU: {3}", new object[]
				{
					this.publicFolderSession.MailboxOwner.ToString(),
					this.publicFolderSession.MailboxOwner.MailboxInfo.OrganizationId.ToString(),
					adrecipient.LegacyExchangeDN,
					adrecipient.OU
				});
				return false;
			}
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(aduser.GetContext(null), null, null);
			bool enabled = snapshot.MailboxAssistants.PublicFolderSplit.Enabled;
			if (!enabled)
			{
				reason = string.Format("ADUser not flighted, as per the VariantConfigurationSnapshot. MailboxOwner: {0} Organization: {1} LegacyExchangeDN: {2} OU: {3}", new object[]
				{
					this.publicFolderSession.MailboxOwner.ToString(),
					this.publicFolderSession.MailboxOwner.MailboxInfo.OrganizationId.ToString(),
					aduser.LegacyExchangeDN,
					aduser.OU
				});
			}
			return enabled;
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0005ADA4 File Offset: 0x00058FA4
		private static void AddSplitStateDiagnostic(Guid mailbox, IPublicFolderSplitState splitState, XElement splitDiagnostic)
		{
			XElement xelement = new XElement("PublicFolderSplitState");
			xelement.Add(new XElement("MailboxGuid", mailbox.ToString()));
			xelement.Add(splitState.ToXElement());
			splitDiagnostic.Add(xelement);
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0005ADF8 File Offset: 0x00058FF8
		private static void AddSplitDateDiagnostic(Guid mailbox, string splitDate, XElement splitDiagnostic)
		{
			XElement xelement = new XElement("PublicFolderSplitDate");
			xelement.Add(new XElement("MailboxGuid", mailbox.ToString()));
			xelement.Add(new XElement("SplitDate", splitDate));
			splitDiagnostic.Add(xelement);
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0005AE54 File Offset: 0x00059054
		private void RecordDiagnosticSplitDate(DateTime splitDate)
		{
			PublicFolderSplitProcessor.SplitDates[this.publicFolderSession.MailboxGuid] = splitDate;
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0005AE6C File Offset: 0x0005906C
		private void RecordDiagnosticSplitState()
		{
			PublicFolderSplitProcessor.SplitStates[this.publicFolderSession.MailboxGuid] = this.splitState;
			PublicFolderSplitProcessor.SplitDates.Remove(this.publicFolderSession.MailboxGuid);
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0005AEA0 File Offset: 0x000590A0
		private void ReadSplitState()
		{
			Exception innerException = null;
			this.splitState = this.splitStateAdapter.ReadFromStore(out innerException);
			if (this.splitState == null)
			{
				this.RecordDiagnosticSplitDate(DateTime.MaxValue);
				throw new SplitProcessorException("Error loading the split state", innerException);
			}
			if (this.splitState.ProgressState == SplitProgressState.SplitCompleted)
			{
				if (DateTime.UtcNow - this.splitState.OverallSplitState.CompletedTime > PublicFolderSplitConfig.Instance.MaxJobAgeForDiagnostics)
				{
					this.RecordDiagnosticSplitDate(this.splitState.OverallSplitState.CompletedTime);
				}
				else
				{
					this.RecordDiagnosticSplitState();
				}
				this.splitState.PreviousSplitJobState = null;
				this.splitState = new PublicFolderSplitState
				{
					PreviousSplitJobState = this.splitState
				};
				return;
			}
			if (this.splitState.ProgressState == SplitProgressState.SplitNotStarted)
			{
				this.RecordDiagnosticSplitDate(DateTime.MinValue);
				return;
			}
			this.RecordDiagnosticSplitState();
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0005AF80 File Offset: 0x00059180
		private void SaveSplitState()
		{
			if (this.splitState.ProgressState == SplitProgressState.SplitNotStarted)
			{
				return;
			}
			Exception ex = this.splitStateAdapter.SaveToStore(this.splitState);
			if (ex != null)
			{
				throw new SplitProcessorException("Error saving the split state", ex);
			}
			this.RecordDiagnosticSplitState();
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0005AFC4 File Offset: 0x000591C4
		private bool CheckPoint(ISplitOperation operation)
		{
			bool result = false;
			if (operation.OperationState.CompletedTime != DateTime.MinValue)
			{
				if (operation.OperationState.Error != null || this.splitState.ProgressState == SplitProgressState.MoveContentCompleted)
				{
					result = true;
					this.MarkOverallSplitJobComplete(operation.OperationState.Error, operation.OperationState.ErrorDetails, operation.Name);
				}
			}
			else
			{
				result = true;
			}
			this.SaveSplitState();
			return result;
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0005B038 File Offset: 0x00059238
		private void MarkOverallSplitJobComplete(Exception error, string errorDetails, string component)
		{
			SplitProcessorException splitError = null;
			if (error != null)
			{
				splitError = new SplitProcessorException(string.Format("Error in operation {0}.", component), error);
			}
			this.MarkOverallSplitJobComplete(splitError, errorDetails);
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0005B064 File Offset: 0x00059264
		private void MarkOverallSplitJobComplete(SplitProcessorException splitError, string errorDetails)
		{
			this.splitState.ProgressState = SplitProgressState.SplitCompleted;
			this.splitState.OverallSplitState.CompletedTime = DateTime.UtcNow;
			if (splitError != null)
			{
				this.splitState.OverallSplitState.Error = splitError;
				this.splitState.OverallSplitState.ErrorDetails = errorDetails;
			}
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0005B0B8 File Offset: 0x000592B8
		private void LogCompletion(Exception unexpectedError = null)
		{
			LogEventType eventType = LogEventType.Statistics;
			if (this.splitState.OverallSplitState.Error != null || unexpectedError != null)
			{
				eventType = LogEventType.Error;
				if (this.splitState.OverallSplitState.Error != null)
				{
					Exception innerException = this.splitState.OverallSplitState.Error.InnerException;
					if (innerException != null && innerException.Data != null && innerException.Data.Contains("SplitPublicFolderMailbox:ErrorOut:ErrorReason"))
					{
						object obj = innerException.Data["SplitPublicFolderMailbox:ErrorOut:ErrorReason"];
						if (obj is string)
						{
							string value = (string)innerException.Data["SplitPublicFolderMailbox:ErrorOut:ErrorReason"];
							bool flag = "publicFoldersToProcess".Equals(value, StringComparison.InvariantCultureIgnoreCase) || "TryAccomodateUnassignedParentWithAssignedSubfolders".Equals(value, StringComparison.InvariantCultureIgnoreCase) || "SelectFoldersToMove".Equals(value, StringComparison.InvariantCultureIgnoreCase);
							if (flag)
							{
								eventType = LogEventType.Warning;
							}
						}
						this.completionLogger.LogEvent(LogEventType.Verbose, string.Format("ER={0}", (obj != null) ? obj.ToString() : "null"));
					}
				}
			}
			this.completionLogger.LogEvent(eventType, string.Format("JS={0},JC={1},JE={2},IS={3},IC={4},IE={5},PS={6},PC={7},PE={8},MS={9},MC={10},ME={11},UE={12}", new object[]
			{
				this.splitState.OverallSplitState.StartTime,
				this.splitState.OverallSplitState.CompletedTime,
				this.splitState.OverallSplitState.Error,
				this.splitState.IdentifyTargetMailboxState.StartTime,
				this.splitState.IdentifyTargetMailboxState.CompletedTime,
				this.splitState.IdentifyTargetMailboxState.Error,
				this.splitState.PrepareTargetMailboxState.StartTime,
				this.splitState.PrepareTargetMailboxState.CompletedTime,
				this.splitState.PrepareTargetMailboxState.Error,
				this.splitState.MoveContentState.StartTime,
				this.splitState.MoveContentState.CompletedTime,
				this.splitState.MoveContentState.Error,
				unexpectedError
			}));
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0005B2FC File Offset: 0x000594FC
		private bool IsAnyProcessingNeeded(out ISplitOperation nextOperation)
		{
			nextOperation = null;
			bool result = false;
			switch (this.splitState.ProgressState)
			{
			case SplitProgressState.SplitNotStarted:
				if (this.IsSplitNeeded())
				{
					result = true;
					nextOperation = this.operationFactory.CreateIdentifyTargetMailboxOperation(this.splitState);
					this.splitState.ProgressState = SplitProgressState.SplitNeeded;
					this.splitState.OverallSplitState.StartTime = DateTime.UtcNow;
					this.SaveSplitState();
				}
				break;
			case SplitProgressState.SplitNeeded:
			case SplitProgressState.IdentifyTargetMailboxStarted:
				result = true;
				nextOperation = this.operationFactory.CreateIdentifyTargetMailboxOperation(this.splitState);
				break;
			case SplitProgressState.IdentifyTargetMailboxCompleted:
			case SplitProgressState.PrepareTargetMailboxStarted:
				result = true;
				nextOperation = this.operationFactory.CreatePrepareTargetMailboxOperation(this.splitState);
				break;
			case SplitProgressState.PrepareTargetMailboxCompleted:
			case SplitProgressState.PrepareSplitPlanStarted:
				result = true;
				nextOperation = this.operationFactory.CreatePrepareSplitPlanOperation(this.splitState);
				break;
			case SplitProgressState.PrepareSplitPlanCompleted:
			case SplitProgressState.MoveContentStarted:
				result = true;
				nextOperation = this.operationFactory.CreateMoveContentOperation(this.splitState);
				break;
			}
			this.logger.LogEvent(LogEventType.Verbose, string.Format("PublicFolderSplitProcessor::{0}::{1}::IsAnyProcessingNeeded - Processing needed {2}, Next operation {3}", new object[]
			{
				this.GetHashCode(),
				this.publicFolderSession.DisplayAddress,
				result.ToString(),
				(nextOperation == null) ? string.Empty : nextOperation.Name
			}));
			return result;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0005B446 File Offset: 0x00059646
		internal bool IsSplitNeeded()
		{
			return this.quotaVerifier.IsSplitNeeded();
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0005B453 File Offset: 0x00059653
		[Conditional("DEBUG")]
		private static void SleepToAttachDebugger()
		{
			if (PublicFolderSplitConfig.Instance.SleepTimeBeforeSplitProcessingStarts > TimeSpan.Zero)
			{
				Thread.Sleep(PublicFolderSplitConfig.Instance.SleepTimeBeforeSplitProcessingStarts);
			}
		}

		// Token: 0x0400099D RID: 2461
		private const string LogComponent = "PublicFolderSplitLog";

		// Token: 0x0400099E RID: 2462
		private const string HealthLogComponent = "PublicFolderSplitHealth";

		// Token: 0x0400099F RID: 2463
		private const string SplitCmdletErrorReason = "SplitPublicFolderMailbox:ErrorOut:ErrorReason";

		// Token: 0x040009A0 RID: 2464
		private static Dictionary<Guid, IPublicFolderSplitState> SplitStates = new Dictionary<Guid, IPublicFolderSplitState>();

		// Token: 0x040009A1 RID: 2465
		private static Dictionary<Guid, DateTime> SplitDates = new Dictionary<Guid, DateTime>();

		// Token: 0x040009A2 RID: 2466
		private readonly IPublicFolderMailboxLoggerBase logger;

		// Token: 0x040009A3 RID: 2467
		private readonly IPublicFolderMailboxLoggerBase completionLogger;

		// Token: 0x040009A4 RID: 2468
		private readonly IXSOFactory xsoFactory;

		// Token: 0x040009A5 RID: 2469
		private readonly ISplitQuotaVerifier quotaVerifier;

		// Token: 0x040009A6 RID: 2470
		private readonly ISplitStateAdapter splitStateAdapter;

		// Token: 0x040009A7 RID: 2471
		private readonly ISplitOperationFactory operationFactory;

		// Token: 0x040009A8 RID: 2472
		private readonly IAssistantRunspaceFactory powershellFactory;

		// Token: 0x040009A9 RID: 2473
		private bool disposed;

		// Token: 0x040009AA RID: 2474
		private DisposeTracker disposeTracker;

		// Token: 0x040009AB RID: 2475
		private bool enabledForTest;

		// Token: 0x040009AC RID: 2476
		private IPublicFolderSplitState splitState;
	}
}
