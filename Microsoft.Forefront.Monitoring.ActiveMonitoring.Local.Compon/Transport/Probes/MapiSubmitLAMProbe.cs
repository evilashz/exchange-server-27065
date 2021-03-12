using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.MailboxTransportSubmission.MapiProbe;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Transport.Probes
{
	// Token: 0x02000273 RID: 627
	public class MapiSubmitLAMProbe : ProbeWorkItem
	{
		// Token: 0x06001490 RID: 5264 RVA: 0x0003D150 File Offset: 0x0003B350
		public MapiSubmitLAMProbe()
		{
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x0003D160 File Offset: 0x0003B360
		internal MapiSubmitLAMProbe(int workItemId, ProbeResult resultInstance, SendMapiMailDefinition sendMapiMailDefinitionInstance, IMapiMessageSubmitter mapiMessageSubmitterInstance, IResultsChecker resultsCheckerInstance, ITracer tracerInstance, int seqNum, bool firstRun)
		{
			this.resultsCheckerInstance = resultsCheckerInstance;
			this.tracerInstance = tracerInstance;
			this.seqNumber = (long)seqNum;
			this.firstRun = firstRun;
			this.lamNotificationId = MapiSubmitLAMProbe.GenerateLamNotificationId(workItemId, (long)seqNum);
			this.previousLamNotificationId = MapiSubmitLAMProbe.GenerateLamNotificationId(workItemId, (long)(seqNum - 1));
			this.probeResultInstance = resultInstance;
			this.sendMapiMailDefinitionInstance = sendMapiMailDefinitionInstance;
			this.mapiMessageSubmitterInstance = mapiMessageSubmitterInstance;
			this.probeId = workItemId;
			this.needsInitalization = false;
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x0003D1DF File Offset: 0x0003B3DF
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

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x0003D1FC File Offset: 0x0003B3FC
		private SendMapiMailDefinition SendMapiMailDefinitionInstance
		{
			get
			{
				if (this.sendMapiMailDefinitionInstance == null)
				{
					MailboxSelectionResult mailboxSelectionResult;
					this.sendMapiMailDefinitionInstance = SendMapiMailDefinitionFactory.CreateInstance(this.LamNotificationId, base.Definition, this.MailboxProviderInstance, out mailboxSelectionResult);
					this.mailboxSelectionResult = mailboxSelectionResult;
				}
				return this.sendMapiMailDefinitionInstance;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x0003D23D File Offset: 0x0003B43D
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

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x0003D258 File Offset: 0x0003B458
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

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x0003D273 File Offset: 0x0003B473
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

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x0003D28E File Offset: 0x0003B48E
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

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x0003D2A9 File Offset: 0x0003B4A9
		private bool FirstRun
		{
			get
			{
				return this.firstRun;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001499 RID: 5273 RVA: 0x0003D2B1 File Offset: 0x0003B4B1
		private long SequenceNumber
		{
			get
			{
				return this.seqNumber;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x0003D2B9 File Offset: 0x0003B4B9
		private int ProbeId
		{
			get
			{
				return this.probeId;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x0003D2C1 File Offset: 0x0003B4C1
		private string LamNotificationId
		{
			get
			{
				return this.lamNotificationId;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x0003D2C9 File Offset: 0x0003B4C9
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

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x0003D2F4 File Offset: 0x0003B4F4
		private string PreviousLamNotificationId
		{
			get
			{
				return this.previousLamNotificationId;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x0003D2FC File Offset: 0x0003B4FC
		private MailboxSelectionResult MailboxSelectionResult
		{
			get
			{
				return this.mailboxSelectionResult;
			}
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0003D37C File Offset: 0x0003B57C
		public override void PopulateDefinition<ProbeDefinition>(ProbeDefinition definition, Dictionary<string, string> propertyBag)
		{
			XmlDocument xmlDocument = new SafeXmlDocument();
			xmlDocument.LoadXml(definition.ExtensionAttributes);
			XmlNode node = xmlDocument.SelectSingleNode("//WorkContext/SendMapiMail/Message");
			List<KeyValuePair<string, string>> list = (from p in propertyBag
			where MapiSubmitLAMProbe.wellKnownProperties.Contains(p.Key)
			select p).ToList<KeyValuePair<string, string>>();
			list.ForEach(delegate(KeyValuePair<string, string> p)
			{
				(node.Attributes[p.Key] ?? node.Attributes.Append(node.OwnerDocument.CreateAttribute(p.Key))).Value = p.Value;
			});
			definition.ExtensionAttributes = xmlDocument.InnerXml;
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x0003D3F8 File Offset: 0x0003B5F8
		internal void DoWorkInternal(CancellationToken cancellationToken)
		{
			this.TraceDebug("MapiSubmitLAMProbe started. This performs - 1. Submits a new message to Store 2. Checks results from previous Send Mail operation.");
			if (!TransportProbeCommon.IsProbeExecutionEnabled())
			{
				this.TraceDebug("MapiSubmitLAMProbe skipped as probe is disabled.");
				return;
			}
			if (this.needsInitalization)
			{
				this.Initialize();
			}
			this.TraceDebug(string.Format("Sequence # = {0}. First Run? = {1}.", this.SequenceNumber, this.FirstRun));
			this.ProbeResultInstance.StateAttribute1 = this.SendMapiMailDefinitionInstance.MessageSubject;
			this.ProbeResultInstance.StateAttribute2 = this.SendMapiMailDefinitionInstance.SenderEmailAddress;
			this.ProbeResultInstance.StateAttribute3 = this.SendMapiMailDefinitionInstance.SenderMbxGuid.ToString();
			this.ProbeResultInstance.StateAttribute4 = this.SendMapiMailDefinitionInstance.SenderMdbGuid.ToString();
			this.ProbeResultInstance.StateAttribute12 = this.SendMapiMailDefinitionInstance.RecipientEmailAddress;
			this.ProbeResultInstance.StateAttribute21 = string.Format("MessageClass:{0};MessageBody:{1}", this.SendMapiMailDefinitionInstance.MessageClass, this.SendMapiMailDefinitionInstance.MessageBody);
			this.ProbeResultInstance.StateAttribute22 = string.Format("DoNotDeliver:{0};DropMessageInHub:{1};DeleteAfterSubmit:{2}", this.SendMapiMailDefinitionInstance.DoNotDeliver.ToString(), this.SendMapiMailDefinitionInstance.DropMessageInHub.ToString(), this.SendMapiMailDefinitionInstance.DeleteAfterSubmit.ToString());
			this.ProbeResultInstance.StateAttribute25 = this.SequenceNumber.ToString();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool potentialForAlertBasedOnCurrentRun = false;
			bool potentialForAlertBasedOnPreviousRun = false;
			Exception ex = null;
			Exception ex2 = null;
			string empty = string.Empty;
			string stateAttribute = string.Empty;
			string empty2 = string.Empty;
			Exception ex3 = null;
			string empty3 = string.Empty;
			DateTime utcNow = DateTime.UtcNow;
			double sampleValue = 0.0;
			bool flag4 = false;
			try
			{
				stateAttribute = this.GetPreviousSuccessfulMailLatencies(cancellationToken, out empty2);
			}
			catch (Exception ex4)
			{
				ex3 = ex4;
				this.LogGetLatencyException(ex4);
			}
			try
			{
				potentialForAlertBasedOnPreviousRun = this.RaiseAlertBasedOnPreviousRun(cancellationToken, empty2, out flag, out flag2);
			}
			catch (Exception ex5)
			{
				ex = ex5;
				this.LogCheckMailException(ex5);
			}
			try
			{
				flag3 = this.SendMail(cancellationToken, out empty3, out empty, out utcNow, out sampleValue);
				if (flag3)
				{
					if (!string.IsNullOrEmpty(empty) && !string.IsNullOrEmpty(empty3))
					{
						potentialForAlertBasedOnCurrentRun = false;
					}
					else
					{
						this.TraceError(string.Format("SendMail returned but either internetMessageId:{0} or entryId:{1} was null or empty", empty ?? string.Empty, empty3 ?? string.Empty));
					}
				}
			}
			catch (WrongServerException ex6)
			{
				ex2 = ex6;
				this.LogSendMailException(ex6);
				flag4 = true;
			}
			catch (MailboxOfflineException ex7)
			{
				ex2 = ex7;
				this.LogSendMailException(ex7);
				flag4 = true;
			}
			catch (StorageTransientException ex8)
			{
				ex2 = ex8;
				this.LogSendMailException(ex8);
				flag4 = true;
			}
			catch (StoragePermanentException ex9)
			{
				ex2 = ex9;
				this.LogSendMailException(ex9);
				flag4 = true;
			}
			catch (Exception ex10)
			{
				ex2 = ex10;
				this.LogSendMailException(ex10);
			}
			this.ProbeResultInstance.StateAttribute5 = empty;
			this.ProbeResultInstance.StateAttribute11 = empty3;
			this.ProbeResultInstance.StateAttribute13 = utcNow.ToString();
			this.ProbeResultInstance.StateAttribute14 = ((ex == null) ? string.Empty : ex.ToString());
			this.ProbeResultInstance.StateAttribute15 = ((ex2 == null) ? string.Empty : ex2.ToString());
			this.ProbeResultInstance.StateAttribute23 = stateAttribute;
			this.ProbeResultInstance.StateAttribute24 = ((ex3 == null) ? string.Empty : ex3.ToString());
			this.ProbeResultInstance.SampleValue = sampleValue;
			this.ProbeResultInstance.StateAttribute7 = (double)(flag ? 0 : 1);
			this.ProbeResultInstance.StateAttribute8 = (double)(flag2 ? 1 : 0);
			this.ProbeResultInstance.StateAttribute9 = (double)(flag3 ? 1 : 0);
			if (this.MailboxSelectionResult == MailboxSelectionResult.NoMonitoringMDBs)
			{
				this.ProbeResultInstance.StateAttribute9 = 2.0;
				ProbeResult probeResult = this.ProbeResultInstance;
				probeResult.StateAttribute15 += this.MailboxSelectionResult;
				potentialForAlertBasedOnCurrentRun = false;
			}
			if (this.MailboxSelectionResult == MailboxSelectionResult.NoMonitoringMDBsAreActive)
			{
				this.ProbeResultInstance.StateAttribute9 = 3.0;
				ProbeResult probeResult2 = this.ProbeResultInstance;
				probeResult2.StateAttribute15 += this.MailboxSelectionResult;
				potentialForAlertBasedOnCurrentRun = false;
			}
			if (!flag3 && flag4)
			{
				this.ProbeResultInstance.StateAttribute9 = 4.0;
				potentialForAlertBasedOnCurrentRun = false;
			}
			this.TracerInstance.TraceDebug(this.ProbeResultInstance.ExecutionContext);
			this.PerformProbeFinalAction(potentialForAlertBasedOnPreviousRun, potentialForAlertBasedOnCurrentRun, ex, ex2, utcNow);
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x0003D8A0 File Offset: 0x0003BAA0
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

		// Token: 0x060014A2 RID: 5282 RVA: 0x0003D8E4 File Offset: 0x0003BAE4
		protected void VerifyPreviousRunMail(CancellationToken cancellationToken, out bool noPreviousMail, out bool startedSMTPOutOperation, out bool finishedSMTPOutOperation, out bool smtpOutThrewException, out bool sendAsCheckCalledDuringPreviousRun, out bool calledDoneWithMessage)
		{
			noPreviousMail = false;
			sendAsCheckCalledDuringPreviousRun = false;
			startedSMTPOutOperation = false;
			finishedSMTPOutOperation = false;
			smtpOutThrewException = false;
			calledDoneWithMessage = false;
			if (this.FirstRun)
			{
				noPreviousMail = true;
				this.TraceDebug("No previous results to verify. ");
				return;
			}
			if (this.ResultsCheckerInstance.LastSendMailFailed(cancellationToken, Settings.DeploymentId, this.SequenceNumber - 1L, 60, this.ProbeResultInstance.ResultName, this.probeId, MapiSubmitLAMProbe.traceContext))
			{
				noPreviousMail = true;
				this.TraceDebug("Previous mail submission to store failed, no verification required.");
				return;
			}
			this.TraceDebug("Previous mail submission to store was successful. Results - ");
			List<ProbeResult> previousResults = this.ResultsCheckerInstance.GetPreviousResults(cancellationToken, Settings.DeploymentId, this.PreviousLamNotificationId, 60, MapiSubmitLAMProbe.traceContext);
			this.TraceDebug(string.Format("# of previous results: {0}. ", previousResults.Count));
			List<Stage> stagesInExtensionXml = this.GetStagesInExtensionXml(previousResults);
			if (stagesInExtensionXml == null || stagesInExtensionXml.Count == 0)
			{
				this.TraceDebug("Could Not Find stages that ran. ");
				return;
			}
			this.TraceDebug("Examining stages that ran. Found - ");
			foreach (Stage stage in stagesInExtensionXml)
			{
				this.TraceDebug(string.Format("{0}; ", stage.ToString()));
				Stage stage2 = stage;
				if (stage2 != Stage.SendAsCheck)
				{
					switch (stage2)
					{
					case Stage.StartedSMTPOutOperation:
						startedSMTPOutOperation = true;
						break;
					case Stage.FinishedSMTPOutOperation:
						finishedSMTPOutOperation = true;
						break;
					case Stage.SMTPOutThrewException:
						smtpOutThrewException = true;
						break;
					case Stage.DoneWithMessage:
						calledDoneWithMessage = true;
						break;
					}
				}
				else
				{
					sendAsCheckCalledDuringPreviousRun = true;
				}
			}
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x0003DA68 File Offset: 0x0003BC68
		protected string GetPreviousSuccessfulMailLatencies(CancellationToken cancellationToken, out string immediatePreviousMessageIdPlusLatency)
		{
			immediatePreviousMessageIdPlusLatency = string.Empty;
			List<ProbeResult> previousNSpecificStageResults = this.ResultsCheckerInstance.GetPreviousNSpecificStageResults(cancellationToken, Settings.DeploymentId, "MBTSubmission/StoreDriverSubmission", 1440, 5, "<StateAttribute5>EventHandled</StateAttribute5>", MapiSubmitLAMProbe.traceContext);
			if (previousNSpecificStageResults == null || previousNSpecificStageResults.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ProbeResult probeResult in previousNSpecificStageResults)
			{
				if (!string.IsNullOrWhiteSpace(probeResult.ExtensionXml))
				{
					XmlNode propertiesNode = this.GetPropertiesNode(probeResult.ExtensionXml);
					if (propertiesNode != null)
					{
						stringBuilder.Append(this.GetPropertyRaw(propertiesNode, "StateAttribute3", string.Empty));
						stringBuilder.Append(",,");
						stringBuilder.Append(this.GetPropertyRaw(propertiesNode, "StateAttribute2", string.Empty));
						stringBuilder.Append(";;");
						if (string.IsNullOrEmpty(immediatePreviousMessageIdPlusLatency))
						{
							immediatePreviousMessageIdPlusLatency = stringBuilder.ToString();
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x0003DB74 File Offset: 0x0003BD74
		protected bool SendMail(CancellationToken cancellationToken, out string entryId, out string internetMessageId, out DateTime timeMessageSentToStore, out double timeToSendTheMessageToStore)
		{
			entryId = string.Empty;
			internetMessageId = string.Empty;
			timeMessageSentToStore = DateTime.UtcNow;
			timeToSendTheMessageToStore = 0.0;
			this.TraceDebug("In SendMail - ");
			cancellationToken.ThrowIfCancellationRequested();
			this.TraceDebug(string.Format("NotificationID={0}", this.LamNotificationId));
			DateTime utcNow = DateTime.UtcNow;
			switch (this.MailboxSelectionResult)
			{
			case MailboxSelectionResult.Success:
				this.TraceDebug("Sending mail.");
				break;
			case MailboxSelectionResult.NoMonitoringMDBs:
				this.TraceDebug("No MDB was found. LocalEndpointManager.Instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend did not return any mdb. Sender and Recipient values are not set. SendMail Skipped.");
				return false;
			case MailboxSelectionResult.NoMonitoringMDBsAreActive:
				this.TraceDebug("No active MDB was found. Sender and Recipient values are not set");
				return false;
			}
			Guid guid;
			this.MapiMessageSubmitterInstance.SendMapiMessage(this.FullLamNotificationId, this.SendMapiMailDefinitionInstance, out entryId, out internetMessageId, out guid);
			DateTime utcNow2 = DateTime.UtcNow;
			this.TraceDebug("SendMail finished.");
			timeMessageSentToStore = utcNow2;
			timeToSendTheMessageToStore = (utcNow2 - utcNow).TotalMilliseconds;
			return true;
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x0003DC60 File Offset: 0x0003BE60
		private static string GenerateLamNotificationId(int probeId, long sequence)
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

		// Token: 0x060014A6 RID: 5286 RVA: 0x0003DCC0 File Offset: 0x0003BEC0
		private void Initialize()
		{
			this.probeId = base.Id;
			this.seqNumber = ProbeRunSequence.GetProbeRunSequenceNumber(this.probeId.ToString(), out this.firstRun);
			this.lamNotificationId = MapiSubmitLAMProbe.GenerateLamNotificationId(this.probeId, this.SequenceNumber);
			this.previousLamNotificationId = MapiSubmitLAMProbe.GenerateLamNotificationId(this.probeId, this.SequenceNumber - 1L);
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x0003DD28 File Offset: 0x0003BF28
		private List<Stage> GetStagesInExtensionXml(List<ProbeResult> results)
		{
			List<Stage> list = new List<Stage>();
			foreach (ProbeResult probeResult in results)
			{
				if (!string.IsNullOrWhiteSpace(probeResult.ExtensionXml))
				{
					XmlNode propertiesNode = this.GetPropertiesNode(probeResult.ExtensionXml);
					if (propertiesNode != null)
					{
						list.Add(this.GetProperty(propertiesNode, "StateAttribute5", Stage.None));
					}
				}
			}
			return list;
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0003DDA8 File Offset: 0x0003BFA8
		private XmlNode GetPropertiesNode(string extensionXml)
		{
			if (string.IsNullOrWhiteSpace(extensionXml))
			{
				return null;
			}
			byte[] bytes = Encoding.Default.GetBytes(extensionXml);
			XmlNode result;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
				safeXmlDocument.Load(memoryStream);
				XmlElement documentElement = safeXmlDocument.DocumentElement;
				result = documentElement.SelectSingleNode("/Properties");
			}
			return result;
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0003DE14 File Offset: 0x0003C014
		private Stage GetProperty(XmlNode properties, string propertyName, Stage defaultValue)
		{
			string propertyRaw = this.GetPropertyRaw(properties, propertyName, string.Empty);
			if (string.IsNullOrEmpty(propertyRaw))
			{
				return defaultValue;
			}
			return (Stage)Enum.Parse(typeof(Stage), propertyRaw);
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x0003DE50 File Offset: 0x0003C050
		private string GetPropertyRaw(XmlNode properties, string propertyName, string defaultValue)
		{
			if (properties == null)
			{
				return defaultValue;
			}
			XmlNode xmlNode = properties.SelectSingleNode(propertyName);
			if (xmlNode == null)
			{
				return defaultValue;
			}
			return xmlNode.InnerText;
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x0003DE75 File Offset: 0x0003C075
		private void LogCheckMailException(Exception e)
		{
			this.LogException(e, "Checkmail");
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0003DE83 File Offset: 0x0003C083
		private void LogSendMailException(Exception e)
		{
			this.LogException(e, "SendMail");
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0003DE91 File Offset: 0x0003C091
		private void LogGetLatencyException(Exception e)
		{
			this.LogException(e, "getlatency");
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0003DEA0 File Offset: 0x0003C0A0
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

		// Token: 0x060014AF RID: 5295 RVA: 0x0003DF00 File Offset: 0x0003C100
		private void TraceDebug(string info)
		{
			ProbeResult probeResult = this.ProbeResultInstance;
			probeResult.ExecutionContext = probeResult.ExecutionContext + info + " ";
			this.TracerInstance.TraceDebug(info);
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0003DF2A File Offset: 0x0003C12A
		private void TraceError(string error)
		{
			ProbeResult probeResult = this.ProbeResultInstance;
			probeResult.ExecutionContext = probeResult.ExecutionContext + error + " ";
			this.TracerInstance.TraceError(error);
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0003DF54 File Offset: 0x0003C154
		private bool RaiseAlertBasedOnSALatency(string previousLatencyCompoundString)
		{
			if (string.IsNullOrEmpty(previousLatencyCompoundString))
			{
				return false;
			}
			string[] array = previousLatencyCompoundString.Split(new char[]
			{
				',',
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length != 2)
			{
				return false;
			}
			string text = array[1].ToUpper();
			if (text.Contains("SA="))
			{
				int num = 0;
				int num2 = text.IndexOf("SA=", StringComparison.OrdinalIgnoreCase);
				int num3 = text.IndexOf("|", num2, StringComparison.OrdinalIgnoreCase);
				if (num3 < 0)
				{
					num3 = text.Length - 2;
				}
				return int.TryParse(text.Substring(num2 + 6, num3 - num2 - 6), out num) && num < 300;
			}
			return false;
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x0003DFFC File Offset: 0x0003C1FC
		private bool RaiseAlertBasedOnPreviousRun(CancellationToken cancellationToken, string immediatePreviousMessageIdPlusLatency, out bool noPreviousMail, out bool previousRunCalledDoneWithMessage)
		{
			noPreviousMail = false;
			previousRunCalledDoneWithMessage = false;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			this.VerifyPreviousRunMail(cancellationToken, out noPreviousMail, out flag2, out flag3, out flag4, out flag, out previousRunCalledDoneWithMessage);
			if (noPreviousMail)
			{
				return false;
			}
			this.TraceDebug("Previous SendMail failure - ");
			if (!flag)
			{
				this.TraceDebug("Mail submitted to Store during the previous run never reached SendAsCheck. \r\n                        This may indicate a latency from Store to Submission Service. Investigating. ");
				if (!this.RaiseAlertBasedOnSALatency(immediatePreviousMessageIdPlusLatency))
				{
					this.TraceDebug("Found High or no SA for previous successful run. Skip count towards alert.");
					return false;
				}
				this.TraceDebug("Found lower SA latency. Indicates an issue in Submission service. \r\n                            Investigate.");
				return true;
			}
			else
			{
				if (!flag2)
				{
					this.TraceDebug("Mail submitted during the previous run never reached SMTP Out. \r\n                            Indicates an issue or latency in the Submission service. Verify\r\n                            the stages/traces to identify root cause of the issue");
					return true;
				}
				if (flag4)
				{
					this.TraceDebug("Mail submitted during the previous run attempted to reach Hub via\r\n                            SMTPOut but failed with an unhandled exception. Submission service is not at fault. ");
					return false;
				}
				if (!flag3)
				{
					this.TraceDebug("Mail submitted during the previous run started SMTPOut but didn't finish it, indicating latency\r\n                            on the SMTPOut side. Submission service is not at fault. ");
					return false;
				}
				if (!previousRunCalledDoneWithMessage)
				{
					this.TraceDebug("Mail submitted during the previous run finished SMTPOut (or timed out) but DoneWithMessage was not called. This is \r\n                            called for Success/NoRecipients/NDRGenerated error codes. For other cases like RetrySMTP/PermananentNDRGenerationFailure\r\n                            etc, its not called. Investigate");
					return true;
				}
				this.TraceDebug("None. Previous Run Verification succeeded");
				return false;
			}
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0003E0C0 File Offset: 0x0003C2C0
		private void PerformProbeFinalAction(bool potentialForAlertBasedOnPreviousRun, bool potentialForAlertBasedOnCurrentRun, Exception previousRunVerificationException, Exception currentRunException, DateTime timeMessageSentToStore)
		{
			if (!potentialForAlertBasedOnPreviousRun && !potentialForAlertBasedOnCurrentRun)
			{
				string info = string.Format("MapiSubmitLAMProbe run finished successfully for both CheckPreviousMail and SendCurrentMail at {0}.", timeMessageSentToStore);
				this.TraceDebug(info);
				return;
			}
			if (potentialForAlertBasedOnCurrentRun && potentialForAlertBasedOnPreviousRun)
			{
				ProbeResult probeResult = this.ProbeResultInstance;
				probeResult.FailureContext += "MapiSubmitLAMProbe finished with both CheckPreviousMail and SendMail failure.";
				this.TraceError("MapiSubmitLAMProbe finished with both CheckPreviousMail and SendMail failure.");
				if (currentRunException != null)
				{
					throw currentRunException;
				}
				if (previousRunVerificationException == null)
				{
					throw new ApplicationException("MapiSubmitLAMProbe finished with both CheckPreviousMail and SendMail failure.");
				}
				throw previousRunVerificationException;
			}
			else
			{
				if (potentialForAlertBasedOnCurrentRun)
				{
					ProbeResult probeResult2 = this.ProbeResultInstance;
					probeResult2.FailureContext += "MapiSubmitLAMProbe finished with SendMail failure.";
					this.TraceError("MapiSubmitLAMProbe finished with SendMail failure.");
					if (currentRunException == null)
					{
						currentRunException = new ApplicationException("MapiSubmitLAMProbe finished with SendMail failure.");
					}
					throw currentRunException;
				}
				ProbeResult probeResult3 = this.ProbeResultInstance;
				probeResult3.FailureContext += "MapiSubmitLAMProbe finished with CheckPreviousMail failure.";
				this.TraceError("MapiSubmitLAMProbe finished with CheckPreviousMail failure.");
				if (previousRunVerificationException == null)
				{
					previousRunVerificationException = new ApplicationException("MapiSubmitLAMProbe finished with CheckPreviousMail failure.");
				}
				throw previousRunVerificationException;
			}
		}

		// Token: 0x040009E7 RID: 2535
		internal const string StoreDriverSubmissionLamNotificationIdPrefix = "MBTSubmission/StoreDriverSubmission";

		// Token: 0x040009E8 RID: 2536
		private const string CancellationMessage = "Cancellation requested.";

		// Token: 0x040009E9 RID: 2537
		private const string StoreDriverSubmissionLamNotificationIdFormat = "MBTSubmission/StoreDriverSubmission/{0}";

		// Token: 0x040009EA RID: 2538
		private const string SearchStringInExtensionXml = "<StateAttribute5>EventHandled</StateAttribute5>";

		// Token: 0x040009EB RID: 2539
		private const int NumofMinutesToLookBackToVerifyPreviousRun = 60;

		// Token: 0x040009EC RID: 2540
		private const int NumofMinutesToLookBackToGetPreviousLatencies = 1440;

		// Token: 0x040009ED RID: 2541
		private const int NumofPreviousLatencyResultsToReturn = 5;

		// Token: 0x040009EE RID: 2542
		private const int SaLatencyLimit = 300;

		// Token: 0x040009EF RID: 2543
		private static HashSet<string> wellKnownProperties = new HashSet<string>(new string[]
		{
			"DeleteAfterSubmit",
			"DropMessageInHub",
			"DoNotDeliver",
			"MessageClass",
			"Body",
			"Subject",
			"Message"
		});

		// Token: 0x040009F0 RID: 2544
		private static TracingContext traceContext = new TracingContext();

		// Token: 0x040009F1 RID: 2545
		private readonly bool needsInitalization = true;

		// Token: 0x040009F2 RID: 2546
		private long seqNumber;

		// Token: 0x040009F3 RID: 2547
		private string lamNotificationId;

		// Token: 0x040009F4 RID: 2548
		private string fullLamNotificationId;

		// Token: 0x040009F5 RID: 2549
		private bool firstRun;

		// Token: 0x040009F6 RID: 2550
		private int probeId;

		// Token: 0x040009F7 RID: 2551
		private string previousLamNotificationId;

		// Token: 0x040009F8 RID: 2552
		private MailboxSelectionResult mailboxSelectionResult;

		// Token: 0x040009F9 RID: 2553
		private IMapiMessageSubmitter mapiMessageSubmitterInstance;

		// Token: 0x040009FA RID: 2554
		private ITracer tracerInstance;

		// Token: 0x040009FB RID: 2555
		private IResultsChecker resultsCheckerInstance;

		// Token: 0x040009FC RID: 2556
		private IMailboxProvider mailboxProviderInstance;

		// Token: 0x040009FD RID: 2557
		private ProbeResult probeResultInstance;

		// Token: 0x040009FE RID: 2558
		private SendMapiMailDefinition sendMapiMailDefinitionInstance;
	}
}
