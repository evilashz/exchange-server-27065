using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Storage;
using Microsoft.Exchange.Transport.Storage.Messaging;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x0200022E RID: 558
	public class MailboxDeliveryInstanceAvailabilityProbe : SmtpConnectionProbe
	{
		// Token: 0x06001249 RID: 4681 RVA: 0x00035B8D File Offset: 0x00033D8D
		public MailboxDeliveryInstanceAvailabilityProbe()
		{
			base.UseXmlConfiguration = false;
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x00035B9C File Offset: 0x00033D9C
		protected override bool DisconnectBetweenSessions
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x0600124B RID: 4683 RVA: 0x00035B9F File Offset: 0x00033D9F
		protected StringBuilder ErrorList
		{
			get
			{
				return this.errorList;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x00035BA7 File Offset: 0x00033DA7
		private protected override ISimpleSmtpClient Client
		{
			protected get
			{
				return base.Client;
			}
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00035BB0 File Offset: 0x00033DB0
		public static ProbeDefinition CreateMailboxDeliveryInstanceAvailabilityProbe(MailboxDatabaseInfo mailboxDatabase)
		{
			string text = string.Format("{0}@{1}", mailboxDatabase.MonitoringAccount, mailboxDatabase.MonitoringAccountDomain);
			Guid empty = Guid.Empty;
			if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled && MultiTenantTransport.TryGetExternalOrgId(mailboxDatabase.MonitoringAccountOrganizationId, out empty) != ADOperationResult.Success)
			{
				throw new SmtpConnectionProbeException(string.Format("The external organization id was not found for {0}", text));
			}
			ProbeDefinition probeDefinition = new ProbeDefinition();
			probeDefinition.AssemblyPath = typeof(MailboxDeliveryInstanceAvailabilityProbe).Assembly.Location;
			probeDefinition.TypeName = typeof(MailboxDeliveryInstanceAvailabilityProbe).FullName;
			probeDefinition.Name = "MailboxDeliveryInstanceAvailabilityProbe";
			probeDefinition.ServiceName = ExchangeComponent.MailboxTransport.Name;
			probeDefinition.RecurrenceIntervalSeconds = 120;
			probeDefinition.TimeoutSeconds = 90;
			probeDefinition.MaxRetryAttempts = 3;
			probeDefinition.TargetResource = mailboxDatabase.MailboxDatabaseName;
			probeDefinition.Enabled = true;
			probeDefinition.Attributes["MailboxDatabaseGuid"] = mailboxDatabase.MailboxDatabaseGuid.ToString();
			probeDefinition.Attributes["MonitoringMailboxLegacyExchangeDN"] = mailboxDatabase.MonitoringAccountLegacyDN;
			probeDefinition.Attributes["MonitoringAccountMailboxGuid"] = mailboxDatabase.MonitoringAccountMailboxGuid.ToString();
			probeDefinition.Attributes["ExternalMonitoringAccountOrganizationId"] = empty.ToString();
			probeDefinition.Attributes["RecipientAddress"] = text;
			probeDefinition.Attributes["SmtpServer"] = "127.0.0.1";
			return probeDefinition;
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00035D38 File Offset: 0x00033F38
		public static MonitorDefinition CreateMailboxDeliveryInstanceAvailabilityMonitor(MailboxDatabaseInfo mailboxDatabase, ProbeDefinition probeDefinition)
		{
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition("MailboxDeliveryInstanceAvailabilityMonitor", probeDefinition.ConstructWorkItemResultName(), ExchangeComponent.MailboxTransport.Name, ExchangeComponent.MailboxTransport, 5, true, 120);
			monitorDefinition.TargetResource = mailboxDatabase.MailboxDatabaseName;
			return monitorDefinition;
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00035D78 File Offset: 0x00033F78
		public static ResponderDefinition CreateMailboxDeliveryInstanceAvailabilityEscalateResponder(MailboxDatabaseInfo mailboxDatabase, MonitorDefinition monitorDefinition, out MonitorStateTransition transition)
		{
			transition = new MonitorStateTransition(ServiceHealthStatus.Unhealthy, TimeSpan.FromHours(14400.0));
			return EscalateResponder.CreateDefinition("MailboxDeliveryInstanceAvailabilityEscalateResponder", ExchangeComponent.MailboxTransport.Name, "MailboxDeliveryInstanceAvailabilityMonitor", monitorDefinition.ConstructWorkItemResultName(), mailboxDatabase.MailboxDatabaseName, transition.ToState, ExchangeComponent.MailboxTransport.EscalationTeam, "MailboxDatabaseAvailability for mdb {Probe.TargetResource} unhealthy", "MailboxDatabaseAvailability for mdb {Probe.TargetResource} unhealthy", true, NotificationServiceClass.UrgentInTraining, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00035DEA File Offset: 0x00033FEA
		public static void SetActiveMDBStatus(ProbeResult result, bool active)
		{
			result.StateAttribute1 = active.ToString();
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00035DFC File Offset: 0x00033FFC
		public static bool GetActiveMDBStatus(ProbeResult result)
		{
			bool result2;
			bool.TryParse(result.StateAttribute1, out result2);
			return result2;
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x00035E18 File Offset: 0x00034018
		protected virtual string GetMessageText(string mailboxDatabase)
		{
			return string.Format("X-MS-Exchange-ActiveMonitoringProbeName:{0}\r\nSubject:Delivery probe - Mbx DB - {1} Time {2}\r\n\r\nThis is a mailbox delivery probe", base.Definition.Name, mailboxDatabase, DateTime.UtcNow.ToString());
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00035E50 File Offset: 0x00034050
		protected override void BeforeConnect()
		{
			base.WorkDefinition.MailFrom = null;
			base.WorkDefinition.MailTo = null;
			base.WorkDefinition.Data = null;
			base.TestCount = 1;
			base.WorkDefinition.Port = 475;
			base.WorkDefinition.SmtpServer = "127.0.0.1";
			base.WorkDefinition.HeloDomain = ComputerInformation.DnsPhysicalFullyQualifiedDomainName;
			base.WorkDefinition.AuthenticationType = AuthenticationType.Exchange;
			this.mailboxDatabaseName = base.Definition.TargetResource;
			this.monitoringAccountLegacyDN = base.Definition.Attributes["MonitoringMailboxLegacyExchangeDN"];
			this.recipientAddress = base.Definition.Attributes["RecipientAddress"];
			if (!Guid.TryParse(base.Definition.Attributes["MailboxDatabaseGuid"], out this.mailboxDatabaseGuid))
			{
				throw new Exception(string.Format("Unable to parse MailboxDatabaseGuid: {0}", base.Definition.TargetExtension));
			}
			if (!Guid.TryParse(base.Definition.Attributes["MonitoringAccountMailboxGuid"], out this.monitoringAccountMailboxGuid))
			{
				throw new Exception(string.Format("Unable to parse MonitoringAccountMailboxGuid: {0}", base.Definition.Attributes["MonitoringAccountMailboxGuid"]));
			}
			if (!Guid.TryParse(base.Definition.Attributes["ExternalMonitoringAccountOrganizationId"], out this.externalMonitoringAccountOrganizationId))
			{
				throw new Exception(string.Format("Unable to parse ExternalMonitoringAccountOrganizationId: {0}", base.Definition.Attributes["ExternalMonitoringAccountOrganizationId"]));
			}
			if (!DirectoryAccessor.Instance.IsDatabaseCopyActiveOnLocalServer(this.mailboxDatabaseGuid))
			{
				base.CancelProbeWithSuccess = true;
				MailboxDeliveryInstanceAvailabilityProbe.SetActiveMDBStatus(base.Result, false);
				return;
			}
			MailboxDeliveryInstanceAvailabilityProbe.SetActiveMDBStatus(base.Result, true);
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00036008 File Offset: 0x00034208
		protected override void AfterAuthenticate()
		{
			if (base.ShouldCancelProbe())
			{
				return;
			}
			this.InternalSendMessage();
			if (this.errorList != null)
			{
				throw new SmtpConnectionProbeException(this.errorList.ToString());
			}
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x000360C0 File Offset: 0x000342C0
		protected void InternalSendMessage()
		{
			MailboxDeliveryInstanceAvailabilityProbe.MailDeliveryProbeExtendedPropertyBlob extendedPropertyBlob = new MailboxDeliveryInstanceAvailabilityProbe.MailDeliveryProbeExtendedPropertyBlob(this.recipientAddress, this.monitoringAccountLegacyDN, this.monitoringAccountMailboxGuid);
			MailboxDeliveryInstanceAvailabilityProbe.MailDeliveryProbeSmtpOutSession testSession = new MailboxDeliveryInstanceAvailabilityProbe.MailDeliveryProbeSmtpOutSession();
			string command = string.Format("XSESSIONPARAMS MDBGUID={0}", this.mailboxDatabaseGuid.ToString("N"));
			base.MeasureLatency("XSESSIONPARAMS", delegate()
			{
				this.Client.Send(command);
			});
			command = this.GetMailFromCommand();
			base.MeasureLatency("MAIL FROM", delegate()
			{
				this.Client.Send(command);
			});
			if (!base.VerifyExpectedResponse(SmtpResponse.MailFromOk.ToString()))
			{
				if (MailboxDeliveryInstanceAvailabilityProbe.IsIgnoredError(this.Client.LastResponse))
				{
					this.AddToFailureContext("Response returned was an expected error: " + this.Client.LastResponse);
					return;
				}
				this.AddToErrorList(this.recipientAddress, "MAIL FROM response not as expected. Actual: " + this.Client.LastResponse);
				return;
			}
			else
			{
				command = string.Format("{0}:{1}", "RCPT TO", this.recipientAddress);
				base.MeasureLatency("RCPT TO", delegate()
				{
					this.Client.Send(command);
				});
				if (base.VerifyExpectedResponse(SmtpResponse.RcptToOk.ToString()) || this.Client.LastResponse.Contains("thread limit exceeded"))
				{
					base.MeasureLatency("BDAT EPROP", delegate()
					{
						this.Client.BDat(extendedPropertyBlob.SerializeBlob(testSession), false);
					});
					MemoryStream messageStream = MailboxDeliveryInstanceAvailabilityProbe.GetMessageStream(this.GetMessageText(this.mailboxDatabaseName));
					base.MeasureLatency("BDAT", delegate()
					{
						this.Client.BDat(messageStream, true);
					});
					if (!base.VerifyExpectedResponse(SmtpResponse.NoopOk.ToString()))
					{
						if (MailboxDeliveryInstanceAvailabilityProbe.IsIgnoredError(this.Client.LastResponse))
						{
							this.AddToFailureContext("Response returned was an expected error: " + this.Client.LastResponse);
							return;
						}
						this.AddToErrorList(this.recipientAddress, "BDAT response not as expected. Actual: " + this.Client.LastResponse);
					}
					return;
				}
				if (MailboxDeliveryInstanceAvailabilityProbe.IsIgnoredError(this.Client.LastResponse))
				{
					this.AddToFailureContext("Response returned was an expected error: " + this.Client.LastResponse);
					return;
				}
				this.AddToErrorList(this.recipientAddress, "RCPT TO response not as expected. Actual: " + this.Client.LastResponse);
				return;
			}
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x00036350 File Offset: 0x00034550
		private static bool IsIgnoredError(string lastResponse)
		{
			return MailboxDeliveryAvailabilityProbe.WildCardTransientResponseList.Exists((string wildcard) => TransportProbeCommon.ErrorContains(lastResponse, wildcard)) || MailboxDeliveryAvailabilityProbe.TransientSmtpResponseList.Exists((string transientResponse) => TransportProbeCommon.ErrorMatches(lastResponse, transientResponse));
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0003639C File Offset: 0x0003459C
		private static MemoryStream GetMessageStream(string messageText)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(messageText);
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write(bytes, 0, bytes.Length);
			return memoryStream;
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x000363C8 File Offset: 0x000345C8
		private string GetMailFromCommand()
		{
			string text = string.Empty;
			if (!this.externalMonitoringAccountOrganizationId.Equals(Guid.Empty))
			{
				text = string.Format("MAIL FROM:maildeliveryprobe@maildeliveryprobe.com XATTRDIRECT={0} XATTRORGID=xorgid:{1} XMESSAGECONTEXT={2}", MailDirectionality.Incoming, this.externalMonitoringAccountOrganizationId, ExtendedPropertiesSmtpMessageContextBlob.VersionString);
			}
			else
			{
				text = string.Format("MAIL FROM:maildeliveryprobe@maildeliveryprobe.com XMESSAGECONTEXT={0}", ExtendedPropertiesSmtpMessageContextBlob.VersionString);
			}
			if (this.Client.IsXSysProbeAdvertised)
			{
				text = text + " XSYSPROBEID=" + base.GetProbeId();
			}
			return text;
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x00036440 File Offset: 0x00034640
		private void AddToErrorList(string recipientAddress, string errorMessage)
		{
			string arg = " :";
			if (this.errorList == null)
			{
				this.errorList = new StringBuilder();
				arg = string.Empty;
			}
			this.errorList.AppendFormat("{0} Probe to {1} failed with error {2}", arg, recipientAddress, errorMessage);
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x00036480 File Offset: 0x00034680
		private void AddToFailureContext(string message)
		{
			if (base.Result != null)
			{
				ProbeResult result = base.Result;
				result.FailureContext += message;
			}
		}

		// Token: 0x04000876 RID: 2166
		internal const string MailboxDeliveryInstanceAvailabilityProbeName = "MailboxDeliveryInstanceAvailabilityProbe";

		// Token: 0x04000877 RID: 2167
		internal const string MailboxDeliveryInstanceAvailabilityMonitorName = "MailboxDeliveryInstanceAvailabilityMonitor";

		// Token: 0x04000878 RID: 2168
		internal const string MailboxDeliveryInstanceAvailabilityRestartResponderName = "MailboxDeliveryInstanceAvailabilityRestartResponder";

		// Token: 0x04000879 RID: 2169
		internal const string MailboxDeliveryInstanceAvailabilityEscalateResponderName = "MailboxDeliveryInstanceAvailabilityEscalateResponder";

		// Token: 0x0400087A RID: 2170
		internal const string EscalateResponderSubject = "MailboxDatabaseAvailability for mdb {Probe.TargetResource} unhealthy";

		// Token: 0x0400087B RID: 2171
		internal const string EscalateResponderMessage = "MailboxDatabaseAvailability for mdb {Probe.TargetResource} unhealthy";

		// Token: 0x0400087C RID: 2172
		internal const int ProbeRecurrenceIntervalSeconds = 120;

		// Token: 0x0400087D RID: 2173
		internal const int ProbeTimeoutSeconds = 90;

		// Token: 0x0400087E RID: 2174
		internal const int ProbeRetryAttempts = 3;

		// Token: 0x0400087F RID: 2175
		internal const bool ProbeEnabled = true;

		// Token: 0x04000880 RID: 2176
		internal const int MonitorFailureCount = 5;

		// Token: 0x04000881 RID: 2177
		internal const bool MonitorEnabled = true;

		// Token: 0x04000882 RID: 2178
		internal const int MonitorMonitoringInterval = 120;

		// Token: 0x04000883 RID: 2179
		internal const bool EscalateResponderEnabled = true;

		// Token: 0x04000884 RID: 2180
		internal const int EscalateWaitTimeSeconds = 14400;

		// Token: 0x04000885 RID: 2181
		internal const NotificationServiceClass EscalateLevel = NotificationServiceClass.UrgentInTraining;

		// Token: 0x04000886 RID: 2182
		internal const string MailboxDatabaseGuidAttribute = "MailboxDatabaseGuid";

		// Token: 0x04000887 RID: 2183
		internal const string MonitoringMailboxLegacyExchangeDNAttribute = "MonitoringMailboxLegacyExchangeDN";

		// Token: 0x04000888 RID: 2184
		internal const string RecipientAddressAttribute = "RecipientAddress";

		// Token: 0x04000889 RID: 2185
		internal const string MonitoringAccountMailboxGuidAttribute = "MonitoringAccountMailboxGuid";

		// Token: 0x0400088A RID: 2186
		internal const string ExternalMonitoringAccountOrganizationIdAttribute = "ExternalMonitoringAccountOrganizationId";

		// Token: 0x0400088B RID: 2187
		internal const string SmtpServerAttribute = "SmtpServer";

		// Token: 0x0400088C RID: 2188
		private const string XSesssionParamsCommandFormat = "XSESSIONPARAMS MDBGUID={0}";

		// Token: 0x0400088D RID: 2189
		private const string RcptToCommand = "RCPT TO";

		// Token: 0x0400088E RID: 2190
		private const string MailFromCommand = "MAIL FROM";

		// Token: 0x0400088F RID: 2191
		private const string MailCommandFormat = "MAIL FROM:maildeliveryprobe@maildeliveryprobe.com ";

		// Token: 0x04000890 RID: 2192
		private const string BdatMessageFormat = "X-MS-Exchange-ActiveMonitoringProbeName:{0}\r\nSubject:Delivery probe - Mbx DB - {1} Time {2}\r\n\r\nThis is a mailbox delivery probe";

		// Token: 0x04000891 RID: 2193
		private const string SmtpServer = "127.0.0.1";

		// Token: 0x04000892 RID: 2194
		private StringBuilder errorList;

		// Token: 0x04000893 RID: 2195
		private string mailboxDatabaseName;

		// Token: 0x04000894 RID: 2196
		private string monitoringAccountLegacyDN;

		// Token: 0x04000895 RID: 2197
		private Guid mailboxDatabaseGuid;

		// Token: 0x04000896 RID: 2198
		private Guid monitoringAccountMailboxGuid;

		// Token: 0x04000897 RID: 2199
		private Guid externalMonitoringAccountOrganizationId;

		// Token: 0x04000898 RID: 2200
		private string recipientAddress;

		// Token: 0x0200022F RID: 559
		private class MailDeliveryProbeSmtpOutSession : SmtpOutSession
		{
		}

		// Token: 0x02000230 RID: 560
		private class MailDeliveryProbeExtendedPropertyBlob : ExtendedPropertiesSmtpMessageContextBlob
		{
			// Token: 0x0600125C RID: 4700 RVA: 0x000364AC File Offset: 0x000346AC
			public MailDeliveryProbeExtendedPropertyBlob(string recipientAddress, string legacyExchangeDN, Guid monitoringAccountMailboxGuid) : base(true, true, ProcessTransportRole.Hub)
			{
				this.recipient = MailRecipient.NewMessageRecipient(null, new MailboxDeliveryInstanceAvailabilityProbe.MailDeliveryProbeRecipientStorage
				{
					Email = recipientAddress
				});
				this.recipient.ExtendedProperties.SetValue<bool>("Microsoft.Exchange.Transport.Resolved", true);
				this.recipient.ExtendedProperties.SetValue<Microsoft.Exchange.Data.Directory.Recipient.RecipientType>("Microsoft.Exchange.Transport.DirectoryData.RecipientType", Microsoft.Exchange.Data.Directory.Recipient.RecipientType.UserMailbox);
				this.recipient.ExtendedProperties.SetValue<string>("Microsoft.Exchange.Transport.DirectoryData.LegacyExchangeDN", legacyExchangeDN);
				this.recipient.ExtendedProperties.SetValue<Guid>("Microsoft.Exchange.Transport.DirectoryData.ExchangeGuid", monitoringAccountMailboxGuid);
			}

			// Token: 0x0600125D RID: 4701 RVA: 0x0003660C File Offset: 0x0003480C
			protected override IEnumerable<MailRecipient> GetRecipients(SmtpOutSession smtpOutSession)
			{
				yield return this.recipient;
				yield break;
			}

			// Token: 0x0600125E RID: 4702 RVA: 0x00036629 File Offset: 0x00034829
			protected override IReadOnlyExtendedPropertyCollection GetMailItemExtendedProperties(SmtpOutSession smtpOutSession)
			{
				return this.mailItemExtendedProperty;
			}

			// Token: 0x0600125F RID: 4703 RVA: 0x00036631 File Offset: 0x00034831
			protected override int GetRecipientCount(SmtpOutSession smtpOutSession)
			{
				return 1;
			}

			// Token: 0x04000899 RID: 2201
			private MailRecipient recipient;

			// Token: 0x0400089A RID: 2202
			private ExtendedPropertyDictionary mailItemExtendedProperty = new ExtendedPropertyDictionary();
		}

		// Token: 0x02000231 RID: 561
		private class MailDeliveryProbeRecipientStorage : IMailRecipientStorage
		{
			// Token: 0x1700056F RID: 1391
			// (get) Token: 0x06001260 RID: 4704 RVA: 0x00036634 File Offset: 0x00034834
			public long RecipId
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000570 RID: 1392
			// (get) Token: 0x06001261 RID: 4705 RVA: 0x0003663B File Offset: 0x0003483B
			// (set) Token: 0x06001262 RID: 4706 RVA: 0x00036642 File Offset: 0x00034842
			public long MsgId
			{
				get
				{
					throw new NotImplementedException();
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000571 RID: 1393
			// (get) Token: 0x06001263 RID: 4707 RVA: 0x00036649 File Offset: 0x00034849
			// (set) Token: 0x06001264 RID: 4708 RVA: 0x00036650 File Offset: 0x00034850
			public AdminActionStatus AdminActionStatus
			{
				get
				{
					throw new NotImplementedException();
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000572 RID: 1394
			// (get) Token: 0x06001265 RID: 4709 RVA: 0x00036657 File Offset: 0x00034857
			// (set) Token: 0x06001266 RID: 4710 RVA: 0x0003665E File Offset: 0x0003485E
			public DateTime? DeliveryTime
			{
				get
				{
					throw new NotImplementedException();
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000573 RID: 1395
			// (get) Token: 0x06001267 RID: 4711 RVA: 0x00036665 File Offset: 0x00034865
			// (set) Token: 0x06001268 RID: 4712 RVA: 0x0003666C File Offset: 0x0003486C
			public DsnFlags DsnCompleted
			{
				get
				{
					throw new NotImplementedException();
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000574 RID: 1396
			// (get) Token: 0x06001269 RID: 4713 RVA: 0x00036673 File Offset: 0x00034873
			// (set) Token: 0x0600126A RID: 4714 RVA: 0x0003667A File Offset: 0x0003487A
			public DsnFlags DsnNeeded
			{
				get
				{
					throw new NotImplementedException();
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000575 RID: 1397
			// (get) Token: 0x0600126B RID: 4715 RVA: 0x00036681 File Offset: 0x00034881
			// (set) Token: 0x0600126C RID: 4716 RVA: 0x00036688 File Offset: 0x00034888
			public DsnRequestedFlags DsnRequested
			{
				get
				{
					throw new NotImplementedException();
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000576 RID: 1398
			// (get) Token: 0x0600126D RID: 4717 RVA: 0x0003668F File Offset: 0x0003488F
			// (set) Token: 0x0600126E RID: 4718 RVA: 0x00036696 File Offset: 0x00034896
			public Destination DeliveredDestination
			{
				get
				{
					throw new NotImplementedException();
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000577 RID: 1399
			// (get) Token: 0x0600126F RID: 4719 RVA: 0x0003669D File Offset: 0x0003489D
			// (set) Token: 0x06001270 RID: 4720 RVA: 0x000366A5 File Offset: 0x000348A5
			public string Email
			{
				get
				{
					return this.email;
				}
				set
				{
					this.email = value;
				}
			}

			// Token: 0x17000578 RID: 1400
			// (get) Token: 0x06001271 RID: 4721 RVA: 0x000366AE File Offset: 0x000348AE
			// (set) Token: 0x06001272 RID: 4722 RVA: 0x000366B5 File Offset: 0x000348B5
			public string ORcpt
			{
				get
				{
					throw new NotImplementedException();
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000579 RID: 1401
			// (get) Token: 0x06001273 RID: 4723 RVA: 0x000366BC File Offset: 0x000348BC
			// (set) Token: 0x06001274 RID: 4724 RVA: 0x000366C3 File Offset: 0x000348C3
			public string PrimaryServerFqdnGuid
			{
				get
				{
					throw new NotImplementedException();
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x1700057A RID: 1402
			// (get) Token: 0x06001275 RID: 4725 RVA: 0x000366CA File Offset: 0x000348CA
			// (set) Token: 0x06001276 RID: 4726 RVA: 0x000366D1 File Offset: 0x000348D1
			public int RetryCount
			{
				get
				{
					throw new NotImplementedException();
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x1700057B RID: 1403
			// (get) Token: 0x06001277 RID: 4727 RVA: 0x000366D8 File Offset: 0x000348D8
			// (set) Token: 0x06001278 RID: 4728 RVA: 0x000366DF File Offset: 0x000348DF
			public Status Status
			{
				get
				{
					throw new NotImplementedException();
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x1700057C RID: 1404
			// (get) Token: 0x06001279 RID: 4729 RVA: 0x000366E6 File Offset: 0x000348E6
			// (set) Token: 0x0600127A RID: 4730 RVA: 0x000366ED File Offset: 0x000348ED
			public RequiredTlsAuthLevel? TlsAuthLevel
			{
				get
				{
					throw new NotImplementedException();
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x1700057D RID: 1405
			// (get) Token: 0x0600127B RID: 4731 RVA: 0x000366F4 File Offset: 0x000348F4
			// (set) Token: 0x0600127C RID: 4732 RVA: 0x000366FB File Offset: 0x000348FB
			public int OutboundIPPool
			{
				get
				{
					throw new NotImplementedException();
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x1700057E RID: 1406
			// (get) Token: 0x0600127D RID: 4733 RVA: 0x00036702 File Offset: 0x00034902
			public IExtendedPropertyCollection ExtendedProperties
			{
				get
				{
					return this.extendedProperties;
				}
			}

			// Token: 0x1700057F RID: 1407
			// (get) Token: 0x0600127E RID: 4734 RVA: 0x0003670A File Offset: 0x0003490A
			public bool IsDeleted
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000580 RID: 1408
			// (get) Token: 0x0600127F RID: 4735 RVA: 0x00036711 File Offset: 0x00034911
			public bool IsInSafetyNet
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000581 RID: 1409
			// (get) Token: 0x06001280 RID: 4736 RVA: 0x00036718 File Offset: 0x00034918
			public bool IsActive
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000582 RID: 1410
			// (get) Token: 0x06001281 RID: 4737 RVA: 0x0003671F File Offset: 0x0003491F
			public bool PendingDatabaseUpdates
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x06001282 RID: 4738 RVA: 0x00036726 File Offset: 0x00034926
			public void MarkToDelete()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001283 RID: 4739 RVA: 0x0003672D File Offset: 0x0003492D
			public void Materialize(Transaction transaction)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001284 RID: 4740 RVA: 0x00036734 File Offset: 0x00034934
			public void Commit(TransactionCommitMode commitMode)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001285 RID: 4741 RVA: 0x0003673B File Offset: 0x0003493B
			public IMailRecipientStorage MoveTo(long targetMsgId)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001286 RID: 4742 RVA: 0x00036742 File Offset: 0x00034942
			public IMailRecipientStorage CopyTo(long target)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001287 RID: 4743 RVA: 0x00036749 File Offset: 0x00034949
			public void MinimizeMemory()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001288 RID: 4744 RVA: 0x00036750 File Offset: 0x00034950
			public void ReleaseFromActive()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001289 RID: 4745 RVA: 0x00036757 File Offset: 0x00034957
			public void AddToSafetyNet()
			{
				throw new NotImplementedException();
			}

			// Token: 0x0400089B RID: 2203
			private ExtendedPropertyDictionary extendedProperties = new ExtendedPropertyDictionary();

			// Token: 0x0400089C RID: 2204
			private string email;
		}
	}
}
