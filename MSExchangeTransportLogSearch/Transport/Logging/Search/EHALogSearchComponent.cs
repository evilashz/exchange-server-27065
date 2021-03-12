using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.LogSearch;
using Microsoft.Exchange.HostedServices.Archive.MetaReplication;
using Microsoft.Exchange.Net.ExSmtpClient;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000041 RID: 65
	public class EHALogSearchComponent
	{
		// Token: 0x06000169 RID: 361 RVA: 0x00009C88 File Offset: 0x00007E88
		public static void Start(string newServer)
		{
			try
			{
				if (!string.IsNullOrEmpty(newServer))
				{
					EHALogSearchComponent.serverName = newServer;
				}
				if (EHALogSearchComponent.agentLogMonitor == null)
				{
					EHALogSearchComponent.agentLogMonitor = new LogMonitor(EHALogSearchComponent.Configuration.LogDirectory, "EHAMigrationLog", EHALogSearchComponent.serverName, "Log", EHALogSearchComponent.Configuration.TrackingEvent, new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
					{
						{
							"AgentLogs",
							0.00800000037997961
						}
					});
					LogFileInfo.LogFileOpened += EHALogSearchComponent.OnLogFileOpened;
					EHALogSearchComponent.contentDescriptions = new MimeContentDescriptions();
					EHALogSearchComponent.contentDescriptions.LoadMimeContentTypes(Assembly.GetExecutingAssembly());
				}
				if (EHALogSearchComponent.agentLogMonitor != null && !EHALogSearchComponent.agentLogMonitorStarted)
				{
					EHALogSearchComponent.journalResponseColumnIndex = EHALogSearchComponent.Configuration.TrackingEvent.NameToIndex("reason-data");
					EHALogSearchComponent.agentLogMonitor.Start();
					EHALogSearchComponent.agentLogMonitorStarted = true;
				}
			}
			catch (MimeContentSerializerLoadException ex)
			{
				EHALogSearchComponent.logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_LogSearchServiceStartFailureInit, DateTime.UtcNow.Hour.ToString(), new object[]
				{
					ex
				});
			}
			if (EHALogSearchComponent.agentLogMonitor != null)
			{
				ExTraceGlobals.ServiceTracer.TraceDebug((long)EHALogSearchComponent.agentLogMonitor.GetHashCode(), "EHALogSearchComponent LogMonitor Started as " + EHALogSearchComponent.serverName);
			}
			if (EHALogSearchComponent.agentLogMonitorStarted)
			{
				EHALogSearchComponent.refreshTimer = new Timer(delegate(object state)
				{
					EHALogSearchComponent.FlushAfterTimeout();
				});
				EHALogSearchComponent.refreshTimer.Change(TimeSpan.FromSeconds(1.0), EHALogSearchComponent.TimerFlushInterval);
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00009E0C File Offset: 0x0000800C
		public static void Start()
		{
			EHALogSearchComponent.Start(EHALogSearchComponent.serverName);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00009E18 File Offset: 0x00008018
		public static void Stop()
		{
			if (EHALogSearchComponent.agentLogMonitor != null)
			{
				ExTraceGlobals.ServiceTracer.TraceDebug((long)EHALogSearchComponent.agentLogMonitor.GetHashCode(), "EHALogSearchComponent LogMonitor stopping:" + EHALogSearchComponent.serverName);
				EHALogSearchComponent.agentLogMonitor.Stop();
				LogFileInfo.LogFileOpened -= EHALogSearchComponent.OnLogFileOpened;
				EHALogSearchComponent.agentLogMonitor = null;
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00009E74 File Offset: 0x00008074
		public static void OnLogFileOpened(object target, EventArgs args)
		{
			LogFile logFile = target as LogFile;
			if (logFile != null)
			{
				string text = logFile.Prefix.ToLower();
				if (text.Equals("ehamigrationlog", StringComparison.InvariantCultureIgnoreCase))
				{
					ExTraceGlobals.ServiceTracer.TraceDebug((long)EHALogSearchComponent.agentLogMonitor.GetHashCode(), "EHALogSearchComponent LogMonitor OnLogFileOpened adding handler to " + logFile.ToString());
					logFile.ProcessRow += EHALogSearchComponent.ProcessRow;
				}
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00009EDC File Offset: 0x000080DC
		private static string RegexMatch(Regex rx, string data)
		{
			string result = string.Empty;
			if (rx != null && !string.IsNullOrEmpty(data) && !string.IsNullOrEmpty(data = data.Trim()))
			{
				Match match = rx.Match(data.Trim());
				if (match != null && match.Groups != null && 2 <= match.Groups.Count)
				{
					CaptureCollection captures = match.Groups[1].Captures;
					if (captures != null && 1 <= captures.Count)
					{
						result = captures[0].Value;
					}
				}
			}
			return result;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00009F5C File Offset: 0x0000815C
		private static uint Convert(string number)
		{
			uint result = 0U;
			if (!string.IsNullOrEmpty(number) && !uint.TryParse(number.Trim(), out result))
			{
				result = 0U;
			}
			return result;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00009F88 File Offset: 0x00008188
		private static void ProcessRow(object sender, ProcessRowEventArgs args)
		{
			if (EHALogSearchComponent.agentLogMonitorStarted)
			{
				LogFileInfo logFileInfo = sender as LogFileInfo;
				if (logFileInfo != null)
				{
					try
					{
						string field = args.Row.GetField<string>(EHALogSearchComponent.journalResponseColumnIndex);
						string strB = EHALogSearchComponent.RegexMatch(EHALogSearchComponent.JournalStatusParser.RxSTATUS, field).ToLower();
						string senderAddress = EHALogSearchComponent.RegexMatch(EHALogSearchComponent.JournalStatusParser.RxSND, field);
						if (string.Compare(EHALogSearchComponent.Configuration.UnwrapProcessSuccess, strB, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(EHALogSearchComponent.Configuration.NDRProcessSuccess, strB, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(EHALogSearchComponent.Configuration.AlreadyProcessed, strB, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(EHALogSearchComponent.Configuration.PermanentError, strB, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(EHALogSearchComponent.Configuration.NoUsersResolved, strB, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(EHALogSearchComponent.Configuration.DropJournalReportWithoutNdr, strB, StringComparison.OrdinalIgnoreCase) == 0)
						{
							uint num = EHALogSearchComponent.Convert(EHALogSearchComponent.RegexMatch(EHALogSearchComponent.JournalStatusParser.RxRBS, field));
							uint num2 = EHALogSearchComponent.Convert(EHALogSearchComponent.RegexMatch(EHALogSearchComponent.JournalStatusParser.RxRTO, field));
							string text = EHALogSearchComponent.RegexMatch(EHALogSearchComponent.JournalStatusParser.RxRID, field);
							string recipientAddress = EHALogSearchComponent.RegexMatch(EHALogSearchComponent.JournalStatusParser.RxRCP, field);
							string text2 = EHALogSearchComponent.RegexMatch(EHALogSearchComponent.JournalStatusParser.RxExtOrgId, field);
							Guid empty = Guid.Empty;
							if (!Guid.TryParse(text2, out empty))
							{
								throw new FormatException(string.Format(CultureInfo.InvariantCulture, "The value '{0}' of ExtOrgId segment is not in a recognized Guid format", new object[]
								{
									text2
								}));
							}
							if (empty == Guid.Empty)
							{
								throw new FormatException(string.Format(CultureInfo.InvariantCulture, "The value '{0}' of ExtOrgId segment is Empty and it cannot be used for tenant attribution.", new object[]
								{
									text2
								}));
							}
							Microsoft.Exchange.HostedServices.Archive.MetaReplication.ReplicationStatus defectiveStatus = EHALogSearchComponent.GetDefectiveStatus(field);
							if (0 < text.Length && 0U < num && 0U < num2)
							{
								EHALogSearchComponent.AddMessageCheckSend(senderAddress, recipientAddress, text, empty, num, num2, defectiveStatus);
							}
						}
					}
					catch (FormatException ex)
					{
						EHALogSearchComponent.logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_TransportSyncLogEntryProcessingFailure, DateTime.UtcNow.Hour.ToString(), new object[]
						{
							ex
						});
					}
					catch (ArgumentException ex2)
					{
						EHALogSearchComponent.logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_TransportSyncLogEntryProcessingFailure, DateTime.UtcNow.Hour.ToString(), new object[]
						{
							ex2
						});
					}
					catch (NullReferenceException ex3)
					{
						EHALogSearchComponent.logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_TransportSyncLogEntryProcessingFailure, DateTime.UtcNow.Hour.ToString(), new object[]
						{
							ex3
						});
					}
				}
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000A218 File Offset: 0x00008418
		private static Microsoft.Exchange.HostedServices.Archive.MetaReplication.ReplicationStatus GetDefectiveStatus(string unjournalResponse)
		{
			if (unjournalResponse.ToUpperInvariant().Contains(EHALogSearchComponent.JournalStatusParser.DefectiveStatusUnprovisionedUsersAndDistributionGroups.ToUpperInvariant()))
			{
				return Microsoft.Exchange.HostedServices.Archive.MetaReplication.ReplicationStatus.FailedUnprovisionedUserAndDistributionLists;
			}
			if (unjournalResponse.ToUpperInvariant().Contains(EHALogSearchComponent.JournalStatusParser.DefectiveStatusUnprovisionedUsers.ToUpperInvariant()))
			{
				return Microsoft.Exchange.HostedServices.Archive.MetaReplication.ReplicationStatus.FailedUnprovisionedUsers;
			}
			if (unjournalResponse.ToUpperInvariant().Contains(EHALogSearchComponent.JournalStatusParser.DefectiveStatusPermanentError.ToUpperInvariant()))
			{
				return Microsoft.Exchange.HostedServices.Archive.MetaReplication.ReplicationStatus.FailedPermanentError;
			}
			if (unjournalResponse.ToUpperInvariant().Contains(EHALogSearchComponent.JournalStatusParser.DefectiveStatusNoUserResolved.ToUpperInvariant()))
			{
				return Microsoft.Exchange.HostedServices.Archive.MetaReplication.ReplicationStatus.FailedNoRecipientsResolved;
			}
			if (unjournalResponse.ToUpperInvariant().Contains(EHALogSearchComponent.JournalStatusParser.DefectiveStatusDistributionGroup.ToUpperInvariant()))
			{
				return Microsoft.Exchange.HostedServices.Archive.MetaReplication.ReplicationStatus.FailedDistributionGroups;
			}
			return Microsoft.Exchange.HostedServices.Archive.MetaReplication.ReplicationStatus.Succeeded;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000A2B0 File Offset: 0x000084B0
		private static void AddMessageCheckSend(string senderAddress, string recipientAddress, string messageId, Guid externalOrganizationId, uint batchSize, uint timeoutSeconds, Microsoft.Exchange.HostedServices.Archive.MetaReplication.ReplicationStatus status)
		{
			EHALogSearchComponent.JournalMessageSource journalMessageSource;
			if (!EHALogSearchComponent.journalMigrationJobs.TryGetValue(senderAddress, out journalMessageSource))
			{
				journalMessageSource = new EHALogSearchComponent.JournalMessageSource(recipientAddress, batchSize, timeoutSeconds, DateTime.UtcNow);
				EHALogSearchComponent.journalMigrationJobs.Add(senderAddress, journalMessageSource);
			}
			else
			{
				if (string.Compare(journalMessageSource.ReplyFromAddress, recipientAddress, StringComparison.OrdinalIgnoreCase) != 0)
				{
					journalMessageSource.ReplyFromAddress = recipientAddress;
				}
				if (journalMessageSource.MaxResponses != batchSize)
				{
					journalMessageSource.MaxResponses = batchSize;
				}
				if (journalMessageSource.ResponseTimeout != timeoutSeconds)
				{
					journalMessageSource.ResponseTimeout = timeoutSeconds;
				}
				if (journalMessageSource.ExternalOrganizationId != externalOrganizationId)
				{
					journalMessageSource.ExternalOrganizationId = externalOrganizationId;
				}
			}
			if (!journalMessageSource.ReceivedMessageIds.ContainsKey(messageId))
			{
				MessageInsertedKey messageInsertedKey = new MessageInsertedKey();
				byte[] array = (from b in messageId.Split(new char[]
				{
					'-'
				})
				select byte.Parse(b, NumberStyles.HexNumber)).ToArray<byte>();
				messageInsertedKey.MessageId = array;
				EHALogSearchComponent.ConfirmationWrapper value = new EHALogSearchComponent.ConfirmationWrapper(new MessageInsertedConfirmation
				{
					Key = messageInsertedKey,
					CustomerId = ((((int)array[0] << 8 | (int)array[1]) << 8 | (int)array[2]) << 8 | (int)array[3]),
					Status = status,
					DatacenterName = "EOA"
				}, messageId, status.ToString());
				journalMessageSource.ReceivedMessageIds.Add(messageId, value);
			}
			Hashtable hashtable = null;
			lock (EHALogSearchComponent.agentLogMonitor)
			{
				if ((long)journalMessageSource.ReceivedMessageIds.Count >= (long)((ulong)journalMessageSource.MaxResponses))
				{
					hashtable = journalMessageSource.ReceivedMessageIds;
					journalMessageSource.ReceivedMessageIds = new Hashtable();
					journalMessageSource.BatchStartTime = DateTime.UtcNow;
				}
			}
			if (hashtable != null)
			{
				EHALogSearchComponent.PrepareEmailMessage(senderAddress, recipientAddress, externalOrganizationId, hashtable);
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000A468 File Offset: 0x00008668
		private static void FlushAfterTimeout()
		{
			ExTraceGlobals.ServiceTracer.TraceError((long)EHALogSearchComponent.agentLogMonitor.GetHashCode(), "EHALogSearchComponent FlushAfterTimeout() invoked");
			if (EHALogSearchComponent.journalMigrationJobs == null || EHALogSearchComponent.journalMigrationJobs.Count <= 0)
			{
				ExTraceGlobals.ServiceTracer.TraceError((long)EHALogSearchComponent.agentLogMonitor.GetHashCode(), "EHALogSearchComponent FlushAfterTimeout() journal migration jobs is empty.");
				return;
			}
			ExTraceGlobals.ServiceTracer.TraceError((long)EHALogSearchComponent.agentLogMonitor.GetHashCode(), "EHALogSearchComponent FlushAfterTimeout() journal migration jobs exist.");
			List<EHALogSearchComponent.DCSiteJob> list = new List<EHALogSearchComponent.DCSiteJob>();
			lock (EHALogSearchComponent.agentLogMonitor)
			{
				foreach (KeyValuePair<string, EHALogSearchComponent.JournalMessageSource> keyValuePair in EHALogSearchComponent.journalMigrationJobs)
				{
					string key = keyValuePair.Key;
					EHALogSearchComponent.JournalMessageSource value = keyValuePair.Value;
					string replyFromAddress = value.ReplyFromAddress;
					Guid externalOrganizationId = value.ExternalOrganizationId;
					Hashtable hashtable = null;
					ExTraceGlobals.ServiceTracer.TraceError<string>((long)EHALogSearchComponent.agentLogMonitor.GetHashCode(), "EHALogSearchComponent FlushAfterTimeout() Analyzing EhaJournalSite {0}.", value.ToString());
					DateTime t = value.BatchStartTime.AddSeconds(value.ResponseTimeout);
					DateTime utcNow = DateTime.UtcNow;
					if (t < utcNow)
					{
						hashtable = value.ReceivedMessageIds;
						value.ReceivedMessageIds = new Hashtable();
						value.BatchStartTime = DateTime.UtcNow;
						ExTraceGlobals.ServiceTracer.TraceError<string, string, DateTime>((long)EHALogSearchComponent.agentLogMonitor.GetHashCode(), "EHALogSearchComponent FlushAfterTimeout() Timeout is hit , flush for EhaJournalSite {0}, ExpiryTime {1}, TimeNow {2}", value.ToString(), t.ToString(), utcNow);
					}
					else
					{
						ExTraceGlobals.ServiceTracer.TraceError<string, string, DateTime>((long)EHALogSearchComponent.agentLogMonitor.GetHashCode(), "EHALogSearchComponent FlushAfterTimeout() Timeout not hit, dont flush yet for EhaJournalSite {0}, ExpiryTime {1}, TimeNow {2}", value.ToString(), t.ToString(), utcNow);
					}
					if (hashtable != null)
					{
						EHALogSearchComponent.DCSiteJob item = new EHALogSearchComponent.DCSiteJob(key, replyFromAddress, externalOrganizationId, hashtable);
						list.Add(item);
						ExTraceGlobals.ServiceTracer.TraceError<string, string, DateTime>((long)EHALogSearchComponent.agentLogMonitor.GetHashCode(), "EHALogSearchComponent FlushAfterTimeout() Timeout is hit, flushing mimemessages for EhaJournalSite {0}, ExpiryTime {1}, TimeNow {2}", value.ToString(), t.ToString(), utcNow);
					}
				}
			}
			foreach (EHALogSearchComponent.DCSiteJob dcsiteJob in list)
			{
				if (dcsiteJob.MimeMessages.Count > 0)
				{
					EHALogSearchComponent.PrepareEmailMessage(dcsiteJob.SenderAddress, dcsiteJob.RecipientAddress, dcsiteJob.ExternalOrganizationId, dcsiteJob.MimeMessages);
				}
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000A70C File Offset: 0x0000890C
		private static void PrepareEmailMessage(string senderAddress, string recipientAddress, Guid externalOrganizationId, Hashtable mimeMessages)
		{
			int num = EHALogSearchComponent.WriteMimePartsToEmailMessage(mimeMessages.Values, senderAddress, recipientAddress, externalOrganizationId);
			if (num != mimeMessages.Count)
			{
				ExTraceGlobals.ServiceTracer.TraceError<int, int>((long)EHALogSearchComponent.agentLogMonitor.GetHashCode(), "EHALogSearchComponent AddMessageCheckSend emailed {0} of {1} confirmations", num, mimeMessages.Count);
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000A754 File Offset: 0x00008954
		private static int WriteMimePartsToEmailMessage(IEnumerable mimeParts, string toAddress, string fromAddress, Guid externalOrganizationId)
		{
			int num = 0;
			if (string.IsNullOrEmpty(fromAddress))
			{
				fromAddress = "EHAJournalConfirmation@" + NativeHelpers.GetLocalComputerFqdn(false);
			}
			using (EmailMessage emailMessage = EmailMessage.Create(BodyFormat.Text))
			{
				emailMessage.Subject = DateTime.UtcNow.ToString();
				emailMessage.To.Add(new EmailRecipient(toAddress, toAddress));
				emailMessage.Sender = new EmailRecipient(fromAddress, fromAddress);
				Header header = Header.Create("X-Sender");
				header.Value = fromAddress;
				emailMessage.MimeDocument.RootPart.Headers.AppendChild(header);
				header = Header.Create("X-Receiver");
				header.Value = toAddress;
				emailMessage.MimeDocument.RootPart.Headers.AppendChild(header);
				header = Header.Create("X-CreatedBy");
				header.Value = "MSExchange12";
				emailMessage.MimeDocument.RootPart.Headers.AppendChild(header);
				header = Header.Create("X-EndOfInjectedXHeaders");
				header.Value = " ";
				emailMessage.MimeDocument.RootPart.Headers.AppendChild(header);
				Header header2 = Header.Create("X-MS-Exchange-Organization-ArchiveReplication-DataType");
				header2.Value = " ";
				emailMessage.MimeDocument.RootPart.Headers.AppendChild(header2);
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (MetaExtractMime metaExtractMime = new MetaExtractMime(fromAddress, memoryStream))
					{
						metaExtractMime.SetContentDescriptionsProvider(EHALogSearchComponent.contentDescriptions);
						foreach (object obj in mimeParts)
						{
							EHALogSearchComponent.ConfirmationWrapper confirmationWrapper = (EHALogSearchComponent.ConfirmationWrapper)obj;
							try
							{
								if (metaExtractMime.AddBlobPart(confirmationWrapper.Confirmation, false, confirmationWrapper.EhaMessageId, confirmationWrapper.EoaMessageStatus))
								{
									num++;
								}
							}
							catch (XmlException ex)
							{
								EHALogSearchComponent.logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_TransportSyncLogEntryProcessingFailure, DateTime.UtcNow.Hour.ToString(), new object[]
								{
									ex
								});
								ExTraceGlobals.ServiceTracer.TraceError<string, XmlException>(0L, "Failed to add confirmation part to message body. MessageId={0}; XmlException={1}", confirmationWrapper.EhaMessageId, ex);
							}
						}
						metaExtractMime.InsertLastPartEndingBoundary();
						using (Stream contentWriteStream = emailMessage.Body.GetContentWriteStream())
						{
							using (StreamWriter streamWriter = new StreamWriter(contentWriteStream))
							{
								memoryStream.Seek(0L, SeekOrigin.Begin);
								streamWriter.Write(new StreamReader(memoryStream).ReadToEnd());
							}
						}
					}
				}
				if (!EHALogSearchComponent.SubmitMessage(emailMessage, toAddress, fromAddress, externalOrganizationId))
				{
					num = 0;
				}
			}
			return num;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000AAA4 File Offset: 0x00008CA4
		private static bool SubmitMessage(EmailMessage emailMessage, string toAddress, string fromAddress, Guid externalOrganizationId)
		{
			bool result = false;
			int num = 3;
			for (int i = 0; i < num; i++)
			{
				try
				{
					using (SmtpClient smtpClient = new SmtpClient(Environment.MachineName, 25, new EHALogSearchComponent.SmtpClientDebugOutput()))
					{
						smtpClient.AuthCredentials(CredentialCache.DefaultNetworkCredentials);
						smtpClient.To = new string[]
						{
							toAddress
						};
						smtpClient.From = fromAddress;
						if (externalOrganizationId != Guid.Empty)
						{
							smtpClient.FromParameters.Add(new KeyValuePair<string, string>("XATTRDIRECT", "Originating"));
							smtpClient.FromParameters.Add(new KeyValuePair<string, string>("XATTRORGID", "xorgid:" + externalOrganizationId));
						}
						using (MemoryStream memoryStream = new MemoryStream())
						{
							emailMessage.MimeDocument.WriteTo(memoryStream);
							smtpClient.DataStream = memoryStream;
							smtpClient.Submit();
						}
						result = true;
						break;
					}
				}
				catch (Exception ex)
				{
					if (!EHALogSearchComponent.CheckTransientSmtpFailure(ex))
					{
						EHALogSearchComponent.logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_TransportSyncLogEntryProcessingFailure, DateTime.UtcNow.Hour.ToString(), new object[]
						{
							ex
						});
						break;
					}
					if (i + 1 < num)
					{
						ExTraceGlobals.ServiceTracer.TraceError<Exception>((long)EHALogSearchComponent.agentLogMonitor.GetHashCode(), "EHALogSearchComponent The confirmation message submission failed with the following exception {0}. The message submission will be retried.", ex);
					}
					else
					{
						EHALogSearchComponent.logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_TransportSyncLogEntryProcessingFailure, DateTime.UtcNow.Hour.ToString(), new object[]
						{
							ex
						});
					}
				}
			}
			return result;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000AC5C File Offset: 0x00008E5C
		private static bool CheckTransientSmtpFailure(Exception e)
		{
			return e is AlreadyConnectedToSMTPServerException || e is FailedToConnectToSMTPServerException || e is MustBeTlsForAuthException || e is AuthFailureException || e is AuthApiFailureException || e is InvalidSmtpServerResponseException || e is UnexpectedSmtpServerResponseException || e is NotConnectedToSMTPServerException || e is SocketException || (e is IOException && e.InnerException != null && e.InnerException is SocketException);
		}

		// Token: 0x04000109 RID: 265
		private static readonly TimeSpan TimerFlushInterval = new TimeSpan(0, 30, 0);

		// Token: 0x0400010A RID: 266
		private static ExEventLog logger = new ExEventLog(ExTraceGlobals.ServiceTracer.Category, "MSExchangeEHALogSearchComponent");

		// Token: 0x0400010B RID: 267
		private static int journalResponseColumnIndex;

		// Token: 0x0400010C RID: 268
		private static LogMonitor agentLogMonitor = null;

		// Token: 0x0400010D RID: 269
		private static bool agentLogMonitorStarted = false;

		// Token: 0x0400010E RID: 270
		private static string serverName = string.Empty;

		// Token: 0x0400010F RID: 271
		private static Dictionary<string, EHALogSearchComponent.JournalMessageSource> journalMigrationJobs = new Dictionary<string, EHALogSearchComponent.JournalMessageSource>();

		// Token: 0x04000110 RID: 272
		private static MimeContentDescriptions contentDescriptions = null;

		// Token: 0x04000111 RID: 273
		private static Timer refreshTimer;

		// Token: 0x02000042 RID: 66
		private class Configuration
		{
			// Token: 0x1700004F RID: 79
			// (get) Token: 0x0600017B RID: 379 RVA: 0x0000AD3A File Offset: 0x00008F3A
			internal static CsvTable TrackingEvent
			{
				get
				{
					return EHALogSearchComponent.Configuration.trackingEvent;
				}
			}

			// Token: 0x04000114 RID: 276
			internal const bool ComputeContentMd5 = false;

			// Token: 0x04000115 RID: 277
			internal const string LogFileNamePrefix = "EHAMigrationLog";

			// Token: 0x04000116 RID: 278
			internal const string LogFileNamePrefixLC = "ehamigrationlog";

			// Token: 0x04000117 RID: 279
			internal const string LogFileNameExtension = "Log";

			// Token: 0x04000118 RID: 280
			internal const string LogMonitorClass = "AgentLogs";

			// Token: 0x04000119 RID: 281
			internal const string LogFileStatusColumn = "reason-data";

			// Token: 0x0400011A RID: 282
			internal const string SenderExtendedHeader = "X-Sender";

			// Token: 0x0400011B RID: 283
			internal const string ReceiverExtendedHeader = "X-Receiver";

			// Token: 0x0400011C RID: 284
			internal const string CreatedByExtendedHeader = "X-CreatedBy";

			// Token: 0x0400011D RID: 285
			internal const string CreatedByMSExchangeHeaderValue = "MSExchange12";

			// Token: 0x0400011E RID: 286
			internal const string EndOfInjectedXHeadersHeader = "X-EndOfInjectedXHeaders";

			// Token: 0x0400011F RID: 287
			internal const string ReplicationDataTypeExtendedHeader = "X-MS-Exchange-Organization-ArchiveReplication-DataType";

			// Token: 0x04000120 RID: 288
			internal const string EHADataCenterName = "EOA";

			// Token: 0x04000121 RID: 289
			internal const float IndexPercentageByPrefix = 0.008f;

			// Token: 0x04000122 RID: 290
			internal const string EHALogSearchComponentEventSource = "MSExchangeEHALogSearchComponent";

			// Token: 0x04000123 RID: 291
			internal const string DirectionalityParam = "XATTRDIRECT";

			// Token: 0x04000124 RID: 292
			internal const string DirectionalityOriginating = "Originating";

			// Token: 0x04000125 RID: 293
			internal const string OrganizationIdParam = "XATTRORGID";

			// Token: 0x04000126 RID: 294
			internal const string OrganizationIdPrefix = "xorgid:";

			// Token: 0x04000127 RID: 295
			internal static readonly string LogDirectory = Path.Combine(ConfigurationContext.Setup.InstallPath, "TransportRoles\\Logs\\EHAConfirmations\\");

			// Token: 0x04000128 RID: 296
			internal static readonly string UnwrapProcessSuccess = "unwrapprocesssuccess";

			// Token: 0x04000129 RID: 297
			internal static readonly string NDRProcessSuccess = "ndrprocesssuccess";

			// Token: 0x0400012A RID: 298
			internal static readonly string AlreadyProcessed = "alreadyprocessed";

			// Token: 0x0400012B RID: 299
			internal static readonly string NoUsersResolved = "nousersresolved";

			// Token: 0x0400012C RID: 300
			internal static readonly string DropJournalReportWithoutNdr = "dropjournalreportwithoutndr";

			// Token: 0x0400012D RID: 301
			internal static readonly string PermanentError = "permanenterror";

			// Token: 0x0400012E RID: 302
			internal static readonly Version E12Version = new Version(12, 0, 0, 0);

			// Token: 0x0400012F RID: 303
			internal static readonly Version E14InterfaceUpdateVersion = new Version(14, 0, 533);

			// Token: 0x04000130 RID: 304
			private static readonly CsvTable trackingEvent = new CsvTable(new CsvField[]
			{
				new CsvField("date-time", typeof(DateTime), EHALogSearchComponent.Configuration.E12Version),
				new CsvField("reason-data", typeof(string), EHALogSearchComponent.Configuration.E14InterfaceUpdateVersion)
			});
		}

		// Token: 0x02000043 RID: 67
		private class JournalStatusParser
		{
			// Token: 0x04000131 RID: 305
			internal static readonly Regex RxSND = new Regex("SND=<([^>]*)>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

			// Token: 0x04000132 RID: 306
			internal static readonly Regex RxRID = new Regex("RID=<([^>]*)>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

			// Token: 0x04000133 RID: 307
			internal static readonly Regex RxRCP = new Regex("RCP=<([^>]*)>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

			// Token: 0x04000134 RID: 308
			internal static readonly Regex RxRBS = new Regex("RBS=<([^>]*)>", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

			// Token: 0x04000135 RID: 309
			internal static readonly Regex RxRTO = new Regex("RTO=<([^>]*)>", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

			// Token: 0x04000136 RID: 310
			internal static readonly Regex RxExtOrgId = new Regex("ExtOrgId=<([^>]*)>", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

			// Token: 0x04000137 RID: 311
			internal static readonly Regex RxSTATUS = new Regex("status=<([^>]*)>", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

			// Token: 0x04000138 RID: 312
			internal static readonly string DefectiveStatusPermanentError = "<permanenterror>";

			// Token: 0x04000139 RID: 313
			internal static readonly string DefectiveStatusUnprovisionedUsers = "<unprovisionedusers>";

			// Token: 0x0400013A RID: 314
			internal static readonly string DefectiveStatusNoUserResolved = "<nousersresolved>";

			// Token: 0x0400013B RID: 315
			internal static readonly string DefectiveStatusDistributionGroup = "<distributiongroup>";

			// Token: 0x0400013C RID: 316
			internal static readonly string DefectiveStatusUnprovisionedUsersAndDistributionGroups = "<unprovisionedanddistributionlistusers>";
		}

		// Token: 0x02000044 RID: 68
		private class JournalMessageSource
		{
			// Token: 0x06000180 RID: 384 RVA: 0x0000AEE0 File Offset: 0x000090E0
			internal JournalMessageSource(string replyFrom, uint max, uint timeout, DateTime date)
			{
				this.ReplyFromAddress = replyFrom;
				this.MaxResponses = max;
				this.ResponseTimeout = timeout;
				this.BatchStartTime = date;
			}

			// Token: 0x06000181 RID: 385 RVA: 0x0000AF3C File Offset: 0x0000913C
			public override string ToString()
			{
				if (string.Concat(new object[]
				{
					"ReplyFromAddress = ",
					this.ReplyFromAddress,
					"MaxResponse",
					this.MaxResponses,
					"BatchStartTime",
					this.BatchStartTime,
					"ReceivedMessageIds.Count",
					this.ReceivedMessageIds
				}) != null)
				{
					return this.ReceivedMessageIds.Count.ToString();
				}
				return "0";
			}

			// Token: 0x0400013D RID: 317
			internal uint MaxResponses;

			// Token: 0x0400013E RID: 318
			internal uint ResponseTimeout;

			// Token: 0x0400013F RID: 319
			internal string ReplyFromAddress = string.Empty;

			// Token: 0x04000140 RID: 320
			internal Hashtable ReceivedMessageIds = new Hashtable();

			// Token: 0x04000141 RID: 321
			internal DateTime BatchStartTime = DateTime.UtcNow;

			// Token: 0x04000142 RID: 322
			internal Guid ExternalOrganizationId = Guid.Empty;
		}

		// Token: 0x02000045 RID: 69
		internal class DCSiteJob
		{
			// Token: 0x06000182 RID: 386 RVA: 0x0000AFC0 File Offset: 0x000091C0
			internal DCSiteJob(string senderAddress, string recipientAddress, Guid externalOrganizationId, Hashtable messages)
			{
				this.senderAddress = senderAddress;
				this.recipientAddress = recipientAddress;
				this.externalOrganizationId = externalOrganizationId;
				this.mimeMessages = messages;
			}

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x06000183 RID: 387 RVA: 0x0000B01C File Offset: 0x0000921C
			public string SenderAddress
			{
				get
				{
					return this.senderAddress;
				}
			}

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x06000184 RID: 388 RVA: 0x0000B024 File Offset: 0x00009224
			public string RecipientAddress
			{
				get
				{
					return this.recipientAddress;
				}
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x06000185 RID: 389 RVA: 0x0000B02C File Offset: 0x0000922C
			public Guid ExternalOrganizationId
			{
				get
				{
					return this.externalOrganizationId;
				}
			}

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x06000186 RID: 390 RVA: 0x0000B034 File Offset: 0x00009234
			public Hashtable MimeMessages
			{
				get
				{
					return this.mimeMessages;
				}
			}

			// Token: 0x04000143 RID: 323
			private readonly string senderAddress = string.Empty;

			// Token: 0x04000144 RID: 324
			private readonly string recipientAddress = string.Empty;

			// Token: 0x04000145 RID: 325
			private readonly Guid externalOrganizationId = Guid.Empty;

			// Token: 0x04000146 RID: 326
			private Hashtable mimeMessages = new Hashtable();
		}

		// Token: 0x02000046 RID: 70
		internal class ConfirmationWrapper
		{
			// Token: 0x06000187 RID: 391 RVA: 0x0000B03C File Offset: 0x0000923C
			internal ConfirmationWrapper(MessageInsertedConfirmation confirmData, string ehaId, string eoaStatus)
			{
				this.confirmation = confirmData;
				this.ehaMessageId = ehaId;
				this.eoaMessageStatus = eoaStatus;
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x06000188 RID: 392 RVA: 0x0000B06F File Offset: 0x0000926F
			internal MessageInsertedConfirmation Confirmation
			{
				get
				{
					return this.confirmation;
				}
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x06000189 RID: 393 RVA: 0x0000B077 File Offset: 0x00009277
			internal string EhaMessageId
			{
				get
				{
					return this.ehaMessageId;
				}
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x0600018A RID: 394 RVA: 0x0000B07F File Offset: 0x0000927F
			internal string EoaMessageStatus
			{
				get
				{
					return this.eoaMessageStatus;
				}
			}

			// Token: 0x04000147 RID: 327
			private readonly string ehaMessageId = string.Empty;

			// Token: 0x04000148 RID: 328
			private readonly string eoaMessageStatus = string.Empty;

			// Token: 0x04000149 RID: 329
			private MessageInsertedConfirmation confirmation;
		}

		// Token: 0x02000047 RID: 71
		internal class SmtpClientDebugOutput : ISmtpClientDebugOutput
		{
			// Token: 0x0600018B RID: 395 RVA: 0x0000B088 File Offset: 0x00009288
			public void Output(Trace tracer, object context, string message, params object[] args)
			{
				try
				{
					if (message != null)
					{
						if (args != null)
						{
							message = string.Format(CultureInfo.InvariantCulture, message, args);
						}
					}
					else
					{
						message = "Null message";
					}
				}
				catch (FormatException)
				{
					message = "Badly formatted string.";
				}
				tracer.TraceDebug((long)((context != null) ? context.GetHashCode() : 0), message);
			}
		}
	}
}
