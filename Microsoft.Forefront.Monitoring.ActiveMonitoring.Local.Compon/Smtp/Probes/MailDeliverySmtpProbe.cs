using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Storage;
using Microsoft.Exchange.Transport.Storage.Messaging;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000223 RID: 547
	public class MailDeliverySmtpProbe : SmtpConnectionProbe
	{
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x00033DAC File Offset: 0x00031FAC
		protected override bool DisconnectBetweenSessions
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x00033DAF File Offset: 0x00031FAF
		protected StringBuilder ErrorList
		{
			get
			{
				return this.errorList;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x00033DB7 File Offset: 0x00031FB7
		private protected override ISimpleSmtpClient Client
		{
			protected get
			{
				return base.Client;
			}
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00033DC0 File Offset: 0x00031FC0
		protected virtual string GetMessageText(string mailboxDatabase)
		{
			return string.Format("X-MS-Exchange-ActiveMonitoringProbeName:{0}\r\nSubject:Delivery probe - Mbx DB - {1} Time {2}\r\n\r\nThis is a mailbox delivery probe", base.Definition.Name, mailboxDatabase, DateTime.UtcNow.ToString());
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00033DF8 File Offset: 0x00031FF8
		protected override void BeforeConnect()
		{
			if (!base.Broker.IsLocal())
			{
				throw new SmtpConnectionProbeException("MailDeliverySmtpProbe is a local-only probe and should not be used outside in");
			}
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			if (instance == null || instance.MailboxDatabaseEndpoint == null)
			{
				throw new SmtpConnectionProbeException("No MailboxDatabaseEndpoint for Backend found on this server");
			}
			if (instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend.Count == 0)
			{
				base.Result.StateAttribute2 = "No mailboxes found, proceeding as success";
				this.cancelProbe = true;
			}
			base.WorkDefinition.MailFrom = null;
			base.WorkDefinition.MailTo = null;
			base.WorkDefinition.Data = null;
			this.mailboxCollectionForBackend = instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend;
			base.TestCount = this.mailboxCollectionForBackend.Count;
			base.WorkDefinition.Port = 475;
			base.WorkDefinition.SmtpServer = "127.0.0.1";
			base.WorkDefinition.HeloDomain = ComputerInformation.DnsPhysicalFullyQualifiedDomainName;
			base.WorkDefinition.AuthenticationType = AuthenticationType.Exchange;
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x00033EE4 File Offset: 0x000320E4
		protected override void AfterAuthenticate()
		{
			if (this.cancelProbe)
			{
				return;
			}
			this.SendMessage();
			this.currentIndex++;
			if (this.currentIndex == base.TestCount && this.errorList != null)
			{
				throw new SmtpConnectionProbeException(this.errorList.ToString());
			}
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x00033FC4 File Offset: 0x000321C4
		protected void InternalSendMessage(string recipientAddress, string mailboxDatabaseName, Guid mailboxDatabaseGuid, string mailboxLegacyExchangeDN, Guid mailboxGuid, OrganizationId monitoringAccountOrganizationId)
		{
			MailDeliverySmtpProbe.MailDeliveryProbeExtendedPropertyBlob extendedPropertyBlob = new MailDeliverySmtpProbe.MailDeliveryProbeExtendedPropertyBlob(recipientAddress, mailboxLegacyExchangeDN, mailboxGuid);
			MailDeliverySmtpProbe.MailDeliveryProbeSmtpOutSession testSession = new MailDeliverySmtpProbe.MailDeliveryProbeSmtpOutSession();
			string command = string.Format("XSESSIONPARAMS MDBGUID={0}", mailboxDatabaseGuid.ToString("N"));
			base.MeasureLatency("XSESSIONPARAMS", delegate()
			{
				this.Client.Send(command);
			});
			command = this.GetMailFromCommand(recipientAddress, mailboxDatabaseName, mailboxDatabaseGuid, mailboxLegacyExchangeDN, mailboxGuid, monitoringAccountOrganizationId);
			base.MeasureLatency("MAILFROM", delegate()
			{
				this.Client.Send(command);
			});
			if (!base.VerifyExpectedResponse(SmtpResponse.MailFromOk.ToString()))
			{
				if (MailDeliverySmtpProbe.IsIgnoredError(this.Client.LastResponse))
				{
					this.AddToFailureContext("Response returned was an expected error: " + this.Client.LastResponse);
					return;
				}
				this.AddToErrorList(recipientAddress, "MAIL FROM response not as expected. Actual: " + this.Client.LastResponse);
				return;
			}
			else
			{
				command = "RCPT TO:" + recipientAddress;
				base.MeasureLatency("RCPTTO", delegate()
				{
					this.Client.Send(command);
				});
				if (base.VerifyExpectedResponse(SmtpResponse.RcptToOk.ToString()) || this.Client.LastResponse.Contains("thread limit exceeded"))
				{
					base.MeasureLatency("BDAT EPROP", delegate()
					{
						this.Client.BDat(extendedPropertyBlob.SerializeBlob(testSession), false);
					});
					MemoryStream messageStream = MailDeliverySmtpProbe.GetMessageStream(this.GetMessageText(mailboxDatabaseName));
					base.MeasureLatency("BDAT", delegate()
					{
						this.Client.BDat(messageStream, true);
					});
					if (!base.VerifyExpectedResponse(SmtpResponse.NoopOk.ToString()))
					{
						if (MailDeliverySmtpProbe.IsIgnoredError(this.Client.LastResponse))
						{
							this.AddToFailureContext("Response returned was an expected error: " + this.Client.LastResponse);
							return;
						}
						this.AddToErrorList(recipientAddress, "BDAT response not as expected. Actual: " + this.Client.LastResponse);
					}
					return;
				}
				if (MailDeliverySmtpProbe.IsIgnoredError(this.Client.LastResponse))
				{
					this.AddToFailureContext("Response returned was an expected error: " + this.Client.LastResponse);
					return;
				}
				this.AddToErrorList(recipientAddress, "RCPT TO response not as expected. Actual: " + this.Client.LastResponse);
				return;
			}
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00034220 File Offset: 0x00032420
		private static bool IsIgnoredError(string lastResponse)
		{
			return lastResponse.Contains("MailboxOfflineException") || MailDeliverySmtpProbe.transientSmtpResponseList.Exists((string transientResponse) => TransportProbeCommon.ErrorMatches(lastResponse, transientResponse));
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00034264 File Offset: 0x00032464
		private static MemoryStream GetMessageStream(string messageText)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(messageText);
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write(bytes, 0, bytes.Length);
			return memoryStream;
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x00034290 File Offset: 0x00032490
		private string GetMailFromCommand(string recipientAddress, string mailboxDatabaseName, Guid mailboxDatabaseGuid, string mailboxLegacyExchangeDN, Guid mailboxGuid, OrganizationId monitoringAccountOrganizationId)
		{
			string text = string.Empty;
			if (Datacenter.IsMicrosoftHostedOnly(true))
			{
				Guid empty = Guid.Empty;
				if (monitoringAccountOrganizationId == null)
				{
					throw new SmtpConnectionProbeException(string.Format("The organization id was not specified for {0}", recipientAddress));
				}
				if (MultiTenantTransport.TryGetExternalOrgId(monitoringAccountOrganizationId, out empty) != ADOperationResult.Success)
				{
					throw new SmtpConnectionProbeException(string.Format("The external organization id was not found for {0}", recipientAddress));
				}
				text = string.Format("MAIL FROM:maildeliveryprobe@maildeliveryprobe.com XATTRDIRECT={0} XATTRORGID=xorgid:{1} XMESSAGECONTEXT={2}", MailDirectionality.Incoming, empty, ExtendedPropertiesSmtpMessageContextBlob.VersionString);
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

		// Token: 0x060011E2 RID: 4578 RVA: 0x0003433C File Offset: 0x0003253C
		private void SendMessage()
		{
			MailboxDatabaseInfo mailboxDatabaseInfo = this.mailboxCollectionForBackend.ElementAt(this.currentIndex);
			bool flag = DirectoryAccessor.Instance.IsDatabaseCopyActiveOnLocalServer(mailboxDatabaseInfo.MailboxDatabaseGuid);
			if (flag)
			{
				string recipientAddress = string.Format("{0}@{1}", mailboxDatabaseInfo.MonitoringAccount, mailboxDatabaseInfo.MonitoringAccountDomain);
				this.InternalSendMessage(recipientAddress, mailboxDatabaseInfo.MailboxDatabaseName, mailboxDatabaseInfo.MailboxDatabaseGuid, mailboxDatabaseInfo.MonitoringMailboxLegacyExchangeDN, mailboxDatabaseInfo.MonitoringAccountMailboxGuid, mailboxDatabaseInfo.MonitoringAccountOrganizationId);
				return;
			}
			WTFDiagnostics.TraceInformation(ExTraceGlobals.SMTPConnectionTracer, new TracingContext(), string.Format("MailDeliverySmtpProbe skipped because Mailbox Database {0} copy status was not active.", mailboxDatabaseInfo.MailboxDatabaseGuid), null, "SendMessage", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\MailDeliverySmtpProbe.cs", 401);
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x000343E0 File Offset: 0x000325E0
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

		// Token: 0x060011E4 RID: 4580 RVA: 0x00034420 File Offset: 0x00032620
		private void AddToFailureContext(string message)
		{
			if (base.Result != null)
			{
				ProbeResult result = base.Result;
				result.FailureContext += message;
			}
		}

		// Token: 0x04000838 RID: 2104
		private const string XSesssionParamsCommandFormat = "XSESSIONPARAMS MDBGUID={0}";

		// Token: 0x04000839 RID: 2105
		private const string MailCommandFormat = "MAIL FROM:maildeliveryprobe@maildeliveryprobe.com ";

		// Token: 0x0400083A RID: 2106
		private const string BdatMessageFormat = "X-MS-Exchange-ActiveMonitoringProbeName:{0}\r\nSubject:Delivery probe - Mbx DB - {1} Time {2}\r\n\r\nThis is a mailbox delivery probe";

		// Token: 0x0400083B RID: 2107
		private const string SmtpServer = "127.0.0.1";

		// Token: 0x0400083C RID: 2108
		private static readonly List<string> transientSmtpResponseList = new List<string>
		{
			AckReason.MessageDelayedDeleteByAdmin.ToString(),
			AckReason.MessageDeletedByAdmin.ToString(),
			AckReason.MessageDeletedByTransportAgent.ToString(),
			AckReason.PoisonMessageDeletedByAdmin.ToString(),
			AckReason.MessageDelayedDeleteByAdmin.ToString(),
			AckReason.MessageDeletedByAdmin.ToString(),
			AckReason.MessageDeletedByTransportAgent.ToString(),
			AckReason.PoisonMessageDeletedByAdmin.ToString(),
			AckReason.MessageDelayedDeleteByAdmin.ToString(),
			AckReason.MessageDeletedByAdmin.ToString(),
			AckReason.MessageDeletedByTransportAgent.ToString(),
			AckReason.PoisonMessageDeletedByAdmin.ToString(),
			AckReason.MailboxServerOffline.ToString(),
			AckReason.MDBOffline.ToString(),
			AckReason.MapiNoAccessFailure.ToString(),
			AckReason.MailboxServerTooBusy.ToString(),
			AckReason.MailboxMapiSessionLimit.ToString(),
			AckReason.MailboxServerMaxThreadsPerMdbExceeded.ToString(),
			AckReason.MapiExceptionMaxThreadsPerSCTExceeded.ToString(),
			AckReason.MailboxDatabaseThreadLimitExceeded.ToString(),
			AckReason.RecipientThreadLimitExceeded.ToString(),
			AckReason.DeliverySourceThreadLimitExceeded.ToString(),
			AckReason.DynamicMailboxDatabaseThrottlingLimitExceeded.ToString(),
			AckReason.MailboxIOError.ToString(),
			AckReason.MailboxServerNotEnoughMemory.ToString(),
			AckReason.MissingMdbProperties.ToString(),
			AckReason.RecipientMailboxQuarantined.ToString()
		};

		// Token: 0x0400083D RID: 2109
		private ICollection<MailboxDatabaseInfo> mailboxCollectionForBackend;

		// Token: 0x0400083E RID: 2110
		private int currentIndex;

		// Token: 0x0400083F RID: 2111
		private StringBuilder errorList;

		// Token: 0x04000840 RID: 2112
		private bool cancelProbe;

		// Token: 0x02000224 RID: 548
		private class MailDeliveryProbeSmtpOutSession : SmtpOutSession
		{
		}

		// Token: 0x02000225 RID: 549
		private class MailDeliveryProbeExtendedPropertyBlob : ExtendedPropertiesSmtpMessageContextBlob
		{
			// Token: 0x060011E8 RID: 4584 RVA: 0x00034728 File Offset: 0x00032928
			public MailDeliveryProbeExtendedPropertyBlob(string recipientAddress, string legacyExchangeDN, Guid mailboxGuid) : base(true, true, ProcessTransportRole.Hub)
			{
				this.recipient = MailRecipient.NewMessageRecipient(null, new MailDeliverySmtpProbe.MailDeliveryProbeRecipientStorage
				{
					Email = recipientAddress
				});
				this.recipient.ExtendedProperties.SetValue<bool>("Microsoft.Exchange.Transport.Resolved", true);
				this.recipient.ExtendedProperties.SetValue<Microsoft.Exchange.Data.Directory.Recipient.RecipientType>("Microsoft.Exchange.Transport.DirectoryData.RecipientType", Microsoft.Exchange.Data.Directory.Recipient.RecipientType.UserMailbox);
				this.recipient.ExtendedProperties.SetValue<string>("Microsoft.Exchange.Transport.DirectoryData.LegacyExchangeDN", legacyExchangeDN);
				this.recipient.ExtendedProperties.SetValue<Guid>("Microsoft.Exchange.Transport.DirectoryData.ExchangeGuid", mailboxGuid);
			}

			// Token: 0x060011E9 RID: 4585 RVA: 0x00034888 File Offset: 0x00032A88
			protected override IEnumerable<MailRecipient> GetRecipients(SmtpOutSession smtpOutSession)
			{
				yield return this.recipient;
				yield break;
			}

			// Token: 0x060011EA RID: 4586 RVA: 0x000348A5 File Offset: 0x00032AA5
			protected override IReadOnlyExtendedPropertyCollection GetMailItemExtendedProperties(SmtpOutSession smtpOutSession)
			{
				return this.mailItemExtendedProperty;
			}

			// Token: 0x060011EB RID: 4587 RVA: 0x000348AD File Offset: 0x00032AAD
			protected override int GetRecipientCount(SmtpOutSession smtpOutSession)
			{
				return 1;
			}

			// Token: 0x04000841 RID: 2113
			private MailRecipient recipient;

			// Token: 0x04000842 RID: 2114
			private ExtendedPropertyDictionary mailItemExtendedProperty = new ExtendedPropertyDictionary();
		}

		// Token: 0x02000226 RID: 550
		private class MailDeliveryProbeRecipientStorage : IMailRecipientStorage
		{
			// Token: 0x17000555 RID: 1365
			// (get) Token: 0x060011EC RID: 4588 RVA: 0x000348B0 File Offset: 0x00032AB0
			public long RecipId
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000556 RID: 1366
			// (get) Token: 0x060011ED RID: 4589 RVA: 0x000348B7 File Offset: 0x00032AB7
			// (set) Token: 0x060011EE RID: 4590 RVA: 0x000348BE File Offset: 0x00032ABE
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

			// Token: 0x17000557 RID: 1367
			// (get) Token: 0x060011EF RID: 4591 RVA: 0x000348C5 File Offset: 0x00032AC5
			// (set) Token: 0x060011F0 RID: 4592 RVA: 0x000348CC File Offset: 0x00032ACC
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

			// Token: 0x17000558 RID: 1368
			// (get) Token: 0x060011F1 RID: 4593 RVA: 0x000348D3 File Offset: 0x00032AD3
			// (set) Token: 0x060011F2 RID: 4594 RVA: 0x000348DA File Offset: 0x00032ADA
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

			// Token: 0x17000559 RID: 1369
			// (get) Token: 0x060011F3 RID: 4595 RVA: 0x000348E1 File Offset: 0x00032AE1
			// (set) Token: 0x060011F4 RID: 4596 RVA: 0x000348E8 File Offset: 0x00032AE8
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

			// Token: 0x1700055A RID: 1370
			// (get) Token: 0x060011F5 RID: 4597 RVA: 0x000348EF File Offset: 0x00032AEF
			// (set) Token: 0x060011F6 RID: 4598 RVA: 0x000348F6 File Offset: 0x00032AF6
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

			// Token: 0x1700055B RID: 1371
			// (get) Token: 0x060011F7 RID: 4599 RVA: 0x000348FD File Offset: 0x00032AFD
			// (set) Token: 0x060011F8 RID: 4600 RVA: 0x00034904 File Offset: 0x00032B04
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

			// Token: 0x1700055C RID: 1372
			// (get) Token: 0x060011F9 RID: 4601 RVA: 0x0003490B File Offset: 0x00032B0B
			// (set) Token: 0x060011FA RID: 4602 RVA: 0x00034912 File Offset: 0x00032B12
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

			// Token: 0x1700055D RID: 1373
			// (get) Token: 0x060011FB RID: 4603 RVA: 0x00034919 File Offset: 0x00032B19
			// (set) Token: 0x060011FC RID: 4604 RVA: 0x00034921 File Offset: 0x00032B21
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

			// Token: 0x1700055E RID: 1374
			// (get) Token: 0x060011FD RID: 4605 RVA: 0x0003492A File Offset: 0x00032B2A
			// (set) Token: 0x060011FE RID: 4606 RVA: 0x00034931 File Offset: 0x00032B31
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

			// Token: 0x1700055F RID: 1375
			// (get) Token: 0x060011FF RID: 4607 RVA: 0x00034938 File Offset: 0x00032B38
			// (set) Token: 0x06001200 RID: 4608 RVA: 0x0003493F File Offset: 0x00032B3F
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

			// Token: 0x17000560 RID: 1376
			// (get) Token: 0x06001201 RID: 4609 RVA: 0x00034946 File Offset: 0x00032B46
			// (set) Token: 0x06001202 RID: 4610 RVA: 0x0003494D File Offset: 0x00032B4D
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

			// Token: 0x17000561 RID: 1377
			// (get) Token: 0x06001203 RID: 4611 RVA: 0x00034954 File Offset: 0x00032B54
			// (set) Token: 0x06001204 RID: 4612 RVA: 0x0003495B File Offset: 0x00032B5B
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

			// Token: 0x17000562 RID: 1378
			// (get) Token: 0x06001205 RID: 4613 RVA: 0x00034962 File Offset: 0x00032B62
			// (set) Token: 0x06001206 RID: 4614 RVA: 0x00034969 File Offset: 0x00032B69
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

			// Token: 0x17000563 RID: 1379
			// (get) Token: 0x06001207 RID: 4615 RVA: 0x00034970 File Offset: 0x00032B70
			// (set) Token: 0x06001208 RID: 4616 RVA: 0x00034977 File Offset: 0x00032B77
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

			// Token: 0x17000564 RID: 1380
			// (get) Token: 0x06001209 RID: 4617 RVA: 0x0003497E File Offset: 0x00032B7E
			public IExtendedPropertyCollection ExtendedProperties
			{
				get
				{
					return this.extendedProperties;
				}
			}

			// Token: 0x17000565 RID: 1381
			// (get) Token: 0x0600120A RID: 4618 RVA: 0x00034986 File Offset: 0x00032B86
			public bool IsDeleted
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000566 RID: 1382
			// (get) Token: 0x0600120B RID: 4619 RVA: 0x0003498D File Offset: 0x00032B8D
			public bool IsInSafetyNet
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000567 RID: 1383
			// (get) Token: 0x0600120C RID: 4620 RVA: 0x00034994 File Offset: 0x00032B94
			public bool IsActive
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000568 RID: 1384
			// (get) Token: 0x0600120D RID: 4621 RVA: 0x0003499B File Offset: 0x00032B9B
			public bool PendingDatabaseUpdates
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x0600120E RID: 4622 RVA: 0x000349A2 File Offset: 0x00032BA2
			public void MarkToDelete()
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600120F RID: 4623 RVA: 0x000349A9 File Offset: 0x00032BA9
			public void Materialize(Transaction transaction)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001210 RID: 4624 RVA: 0x000349B0 File Offset: 0x00032BB0
			public void Commit(TransactionCommitMode commitMode)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001211 RID: 4625 RVA: 0x000349B7 File Offset: 0x00032BB7
			public IMailRecipientStorage MoveTo(long targetMsgId)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001212 RID: 4626 RVA: 0x000349BE File Offset: 0x00032BBE
			public IMailRecipientStorage CopyTo(long target)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001213 RID: 4627 RVA: 0x000349C5 File Offset: 0x00032BC5
			public void MinimizeMemory()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001214 RID: 4628 RVA: 0x000349CC File Offset: 0x00032BCC
			public void ReleaseFromActive()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001215 RID: 4629 RVA: 0x000349D3 File Offset: 0x00032BD3
			public void AddToSafetyNet()
			{
				throw new NotImplementedException();
			}

			// Token: 0x04000843 RID: 2115
			private ExtendedPropertyDictionary extendedProperties = new ExtendedPropertyDictionary();

			// Token: 0x04000844 RID: 2116
			private string email;
		}
	}
}
