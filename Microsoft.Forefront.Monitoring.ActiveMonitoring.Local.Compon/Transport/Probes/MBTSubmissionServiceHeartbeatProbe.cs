using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Management.MailboxTransportSubmission.MapiProbe;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Transport.Probes
{
	// Token: 0x02000275 RID: 629
	public class MBTSubmissionServiceHeartbeatProbe : ProbeWorkItem
	{
		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x0003E205 File Offset: 0x0003C405
		private IResultsChecker ResultsCheckerInstance
		{
			get
			{
				if (this.resultsCheckerInstance == null)
				{
					this.resultsCheckerInstance = new ResultsChecker();
				}
				return this.resultsCheckerInstance;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x0003E220 File Offset: 0x0003C420
		private long SequenceNumber
		{
			get
			{
				return this.seqNumber;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x0003E228 File Offset: 0x0003C428
		private bool FirstRun
		{
			get
			{
				return this.firstRun;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060014B9 RID: 5305 RVA: 0x0003E230 File Offset: 0x0003C430
		private ProbeResult ProbeResultInstance
		{
			get
			{
				if (this.probeResultInstance == null)
				{
					this.probeResultInstance = base.Result;
				}
				return this.probeResultInstance;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x0003E24C File Offset: 0x0003C44C
		private MailboxDatabaseSelectionResult MailboxDatabaseSelectionResult
		{
			get
			{
				return this.mailboxDatabaseSelectionResult;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x0003E254 File Offset: 0x0003C454
		private string LamNotificationId
		{
			get
			{
				return this.lamNotificationId;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x0003E25C File Offset: 0x0003C45C
		private ITracer TracerInstance
		{
			get
			{
				if (this.tracerInstance == null)
				{
					this.tracerInstance = new Tracer();
				}
				return this.tracerInstance;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x0003E277 File Offset: 0x0003C477
		private string FullLamNotificationId
		{
			get
			{
				if (string.IsNullOrEmpty(this.fullLamNotificationId))
				{
					this.fullLamNotificationId = string.Format("MBTSubmission/StoreDriverSubmission/{0}", this.LamNotificationId);
				}
				return this.fullLamNotificationId;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x0003E2A2 File Offset: 0x0003C4A2
		private SendMapiMailDefinition SendMapiMailDefinitionInstance
		{
			get
			{
				if (this.sendMapiMailDefinitionInstance == null)
				{
					this.sendMapiMailDefinitionInstance = SendMapiMailDefinitionFactory.CreateMapiMailInstance(this.LamNotificationId, base.Definition);
				}
				return this.sendMapiMailDefinitionInstance;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x0003E2C9 File Offset: 0x0003C4C9
		private IMailboxProvider MailboxProviderInstance
		{
			get
			{
				if (this.mailboxProviderInstance == null)
				{
					this.mailboxProviderInstance = MailboxProvider.GetInstance();
				}
				return this.mailboxProviderInstance;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x0003E2E4 File Offset: 0x0003C4E4
		private IMapiMessageSubmitter MapiMessageSubmitterInstance
		{
			get
			{
				if (this.mapiMessageSubmitterInstance == null)
				{
					this.mapiMessageSubmitterInstance = MapiMessageSubmitter.CreateInstance();
				}
				return this.mapiMessageSubmitterInstance;
			}
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0003E300 File Offset: 0x0003C500
		internal void DoWorkInternal(CancellationToken cancellationToken)
		{
			this.TraceDebug("MBTSubmissionServiceHeartbeatProbe started. This performs - 1. Submits a new message to Store 2. checks the crimson channel for MBTSubmissionServiceNotifyMapiLogger Event.");
			if (!TransportProbeCommon.IsProbeExecutionEnabled())
			{
				this.TraceDebug("MBTSubmissionServiceHeartbeatProbe skipped as probe is disabled.");
				return;
			}
			this.Initialize();
			this.TraceDebug(string.Format("Sequence # = {0}. First Run? = {1}.", this.SequenceNumber, this.FirstRun));
			bool potentialForAlertBasedOnCurrentRun = false;
			bool potentialForAlertBasedOneventlog = false;
			Exception ex = null;
			Exception ex2 = null;
			DateTime utcNow = DateTime.UtcNow;
			bool flag = this.SendMapiMessage(ref potentialForAlertBasedOnCurrentRun, ref ex);
			if (flag)
			{
				this.ProbeResultInstance.StateAttribute1 = this.SendMapiMailDefinitionInstance.MessageSubject;
				this.ProbeResultInstance.StateAttribute2 = this.SendMapiMailDefinitionInstance.SenderEmailAddress;
				this.ProbeResultInstance.StateAttribute3 = this.SendMapiMailDefinitionInstance.SenderMbxGuid.ToString();
				this.ProbeResultInstance.StateAttribute4 = this.SendMapiMailDefinitionInstance.SenderMdbGuid.ToString();
				this.ProbeResultInstance.StateAttribute5 = this.SendMapiMailDefinitionInstance.RecipientEmailAddress;
				try
				{
					potentialForAlertBasedOneventlog = this.RaiseAlertBasedOnEventLog(cancellationToken);
				}
				catch (Exception ex3)
				{
					ex2 = ex3;
					potentialForAlertBasedOneventlog = true;
				}
				this.ProbeResultInstance.StateAttribute14 = ((ex2 == null) ? string.Empty : ex2.ToString());
				this.ProbeResultInstance.StateAttribute15 = ((ex == null) ? string.Empty : ex.ToString());
				this.PerformProbeFinalAction(potentialForAlertBasedOneventlog, potentialForAlertBasedOnCurrentRun, ex2, ex, utcNow);
				return;
			}
			this.ProbeResultInstance.StateAttribute13 = "Unable to Send Mapi Message";
			this.TraceDebug(string.Format("Send Mapi Message exception {0}", ex));
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0003E48C File Offset: 0x0003C68C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				this.DoWorkInternal(cancellationToken);
			}
			catch (EndpointManagerEndpointUninitializedException)
			{
				ProbeResult result = base.Result;
				result.ExecutionContext += " Probe ended due to EndpointManagerEndpointUninitializedException, ignoring exception and treating as transient";
			}
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0003E4E8 File Offset: 0x0003C6E8
		private bool SendMapiMessage(ref bool potentialForAlertBasedOnCurrentRun, ref Exception currentRunException)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			ICollection<MailboxDatabaseInfo> collection;
			this.mailboxDatabaseSelectionResult = this.MailboxProviderInstance.GetAllMailboxDatabaseInfo(out collection);
			this.ProbeResultInstance.StateAttribute11 = this.MailboxDatabaseSelectionResult.ToString();
			if (this.mailboxDatabaseSelectionResult == MailboxDatabaseSelectionResult.Success)
			{
				Random rnd = new Random((int)DateTime.UtcNow.Ticks & 65535);
				collection = (from MailboxDatabaseInfo in collection
				orderby rnd.Next()
				select MailboxDatabaseInfo).ToList<MailboxDatabaseInfo>();
				foreach (MailboxDatabaseInfo mailboxDatabaseInfo in collection)
				{
					if (DirectoryAccessor.Instance.IsDatabaseCopyActiveOnLocalServer(mailboxDatabaseInfo.MailboxDatabaseGuid))
					{
						this.SendMapiMailDefinitionInstance.SenderMbxGuid = mailboxDatabaseInfo.MonitoringAccountMailboxGuid;
						this.SendMapiMailDefinitionInstance.SenderMdbGuid = mailboxDatabaseInfo.MailboxDatabaseGuid;
						this.SendMapiMailDefinitionInstance.SenderEmailAddress = string.Format("{0}@{1}", mailboxDatabaseInfo.MonitoringAccount, mailboxDatabaseInfo.MonitoringAccountDomain);
						this.SendMapiMailDefinitionInstance.RecipientEmailAddress = string.Format("{0}@{1}", mailboxDatabaseInfo.MonitoringAccount, mailboxDatabaseInfo.MonitoringAccountDomain);
						try
						{
							Guid guid;
							this.MapiMessageSubmitterInstance.SendMapiMessage(this.FullLamNotificationId, this.SendMapiMailDefinitionInstance, out empty2, out empty, out guid);
							if (!string.IsNullOrEmpty(empty) && !string.IsNullOrEmpty(empty2))
							{
								potentialForAlertBasedOnCurrentRun = false;
							}
							else
							{
								this.TraceError(string.Format("SendMail returned but either internetMessageId:{0} or entryId:{1} was null or empty", empty ?? string.Empty, empty2 ?? string.Empty));
							}
							this.ProbeResultInstance.StateAttribute21 = "Mapi Message Sent Successfully";
							return true;
						}
						catch (Exception ex)
						{
							currentRunException = ex;
							this.LogSendMailException(ex);
							ProbeResult probeResult = this.ProbeResultInstance;
							probeResult.StateAttribute12 = probeResult.StateAttribute12 + ex.GetType().FullName + "\n";
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0003E708 File Offset: 0x0003C908
		private bool RaiseAlertBasedOnEventLog(CancellationToken cancellationToken)
		{
			if (this.FirstRun)
			{
				this.TraceDebug("No events to verify ");
				return false;
			}
			XmlDocument xmlDocument = new SafeXmlDocument();
			xmlDocument.LoadXml(base.Definition.ExtensionAttributes);
			XmlElement xmlElement = Utils.CheckXmlElement(xmlDocument.SelectSingleNode("//MBTSubmissionServiceHeartbeatProbeParam"), "//MBTSubmissionServiceHeartbeatProbeParam");
			string attribute = xmlElement.GetAttribute("NumofMinutesToLookBack");
			int numofMinutesToLookBack;
			if (!int.TryParse(attribute, out numofMinutesToLookBack))
			{
				numofMinutesToLookBack = 15;
			}
			List<ProbeResult> previousProbeResults = this.ResultsCheckerInstance.GetPreviousProbeResults(cancellationToken, numofMinutesToLookBack, "MailboxTransport/MBTSubmissionServiceNotifyMapiLogger", MBTSubmissionServiceHeartbeatProbe.traceContext);
			this.TraceDebug(string.Format("# of previous results: {0}. ", previousProbeResults.Count));
			if (previousProbeResults == null || previousProbeResults.Count == 0)
			{
				this.TraceDebug("Could Not Find any Submission service heartbeat event");
				return true;
			}
			return false;
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0003E7C0 File Offset: 0x0003C9C0
		private string GenerateLamNotificationId(int probeId, long sequence)
		{
			return Guid.Parse(string.Format("{0:X8}-{1:X4}-{2:X4}-{3:X4}-{4:X12}", new object[]
			{
				probeId,
				0,
				0,
				0,
				(int)sequence
			})).ToString();
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0003E820 File Offset: 0x0003CA20
		private void Initialize()
		{
			this.probeId = base.Id;
			this.seqNumber = ProbeRunSequence.GetProbeRunSequenceNumber(this.probeId.ToString(), out this.firstRun);
			this.lamNotificationId = this.GenerateLamNotificationId(this.probeId, this.SequenceNumber);
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x0003E86D File Offset: 0x0003CA6D
		private void TraceDebug(string info)
		{
			ProbeResult probeResult = this.ProbeResultInstance;
			probeResult.ExecutionContext = probeResult.ExecutionContext + info + " ";
			this.TracerInstance.TraceDebug(info);
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0003E897 File Offset: 0x0003CA97
		private void TraceError(string error)
		{
			ProbeResult probeResult = this.ProbeResultInstance;
			probeResult.ExecutionContext = probeResult.ExecutionContext + error + " ";
			this.TracerInstance.TraceError(error);
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0003E8C1 File Offset: 0x0003CAC1
		private void LogSendMailException(Exception e)
		{
			this.LogException(e, "SendMail");
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0003E8D0 File Offset: 0x0003CAD0
		private void LogException(Exception e, string context)
		{
			if (e.Message == "Cancellation requested.")
			{
				this.TraceError("Cancellation requested.");
				return;
			}
			string text = string.Format("{0} failed. Exception: {1}.", context, e.ToString());
			ProbeResult probeResult = this.ProbeResultInstance;
			probeResult.FailureContext += text;
			this.TracerInstance.TraceError(text);
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0003E930 File Offset: 0x0003CB30
		private void PerformProbeFinalAction(bool potentialForAlertBasedOneventlog, bool potentialForAlertBasedOnCurrentRun, Exception eventlogException, Exception currentRunException, DateTime timeMessageSentToStore)
		{
			if (!potentialForAlertBasedOneventlog && !potentialForAlertBasedOnCurrentRun)
			{
				string info = string.Format("MBTSubmissionServiceHeartbeatProbe run finished successfully for both checkeventlog and SendCurrentMail at {0}.", timeMessageSentToStore);
				this.TraceDebug(info);
				return;
			}
			if (potentialForAlertBasedOnCurrentRun && potentialForAlertBasedOneventlog)
			{
				ProbeResult probeResult = this.ProbeResultInstance;
				probeResult.FailureContext += "MBTSubmissionServiceHeartbeatProbe finished with both checkeventlog and SendMail failure.";
				this.TraceError("MBTSubmissionServiceHeartbeatProbe finished with both checkeventlog and SendMail failure.");
				if (eventlogException == null)
				{
					throw new ApplicationException("MBTSubmissionServiceHeartbeatProbe finished with both checkeventlog and SendMail failure.");
				}
				throw eventlogException;
			}
			else
			{
				if (potentialForAlertBasedOneventlog)
				{
					ProbeResult probeResult2 = this.ProbeResultInstance;
					probeResult2.FailureContext += "MBTSubmissionServiceHeartbeatProbe finished with checkeventlog failure.";
					this.TraceError("MBTSubmissionServiceHeartbeatProbe finished with checkeventlog failure.");
					if (eventlogException == null)
					{
						eventlogException = new ApplicationException("MBTSubmissionServiceHeartbeatProbe finished with checkeventlog failure.");
					}
					throw eventlogException;
				}
				return;
			}
		}

		// Token: 0x04000A04 RID: 2564
		internal const string StoreDriverSubmissionNotificationIdPrefix = "MBTSubmission/StoreDriverSubmission";

		// Token: 0x04000A05 RID: 2565
		private const string StoreDriverSubmissionNotificationIdFormat = "MBTSubmission/StoreDriverSubmission/{0}";

		// Token: 0x04000A06 RID: 2566
		private const int DefaultNumberOfMinutesToLookBackVerifyEventLog = 15;

		// Token: 0x04000A07 RID: 2567
		private const string CancellationMessage = "Cancellation requested.";

		// Token: 0x04000A08 RID: 2568
		private const string ResultName = "MailboxTransport/MBTSubmissionServiceNotifyMapiLogger";

		// Token: 0x04000A09 RID: 2569
		private const string NumberOfMinutesToLookBackXmlAttribute = "NumofMinutesToLookBack";

		// Token: 0x04000A0A RID: 2570
		private const bool NeedsInitalization = true;

		// Token: 0x04000A0B RID: 2571
		private const string ProbeParamXmlNodeString = "//MBTSubmissionServiceHeartbeatProbeParam";

		// Token: 0x04000A0C RID: 2572
		private static TracingContext traceContext = new TracingContext();

		// Token: 0x04000A0D RID: 2573
		private long seqNumber;

		// Token: 0x04000A0E RID: 2574
		private int probeId;

		// Token: 0x04000A0F RID: 2575
		private bool firstRun;

		// Token: 0x04000A10 RID: 2576
		private ProbeResult probeResultInstance;

		// Token: 0x04000A11 RID: 2577
		private ITracer tracerInstance;

		// Token: 0x04000A12 RID: 2578
		private MailboxDatabaseSelectionResult mailboxDatabaseSelectionResult;

		// Token: 0x04000A13 RID: 2579
		private string lamNotificationId;

		// Token: 0x04000A14 RID: 2580
		private string fullLamNotificationId;

		// Token: 0x04000A15 RID: 2581
		private IMapiMessageSubmitter mapiMessageSubmitterInstance;

		// Token: 0x04000A16 RID: 2582
		private SendMapiMailDefinition sendMapiMailDefinitionInstance;

		// Token: 0x04000A17 RID: 2583
		private IMailboxProvider mailboxProviderInstance;

		// Token: 0x04000A18 RID: 2584
		private IResultsChecker resultsCheckerInstance;
	}
}
