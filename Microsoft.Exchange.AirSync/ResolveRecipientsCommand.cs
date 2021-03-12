using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000118 RID: 280
	internal class ResolveRecipientsCommand : Command
	{
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x00054997 File Offset: 0x00052B97
		// (set) Token: 0x06000EE3 RID: 3811 RVA: 0x0005499F File Offset: 0x00052B9F
		internal AirSyncPhotoRetriever PhotoRetriever
		{
			get
			{
				return this.photoRetriever;
			}
			private set
			{
				this.photoRetriever = value;
			}
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x000549A8 File Offset: 0x00052BA8
		internal ResolveRecipientsCommand()
		{
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x000549CD File Offset: 0x00052BCD
		internal override int MinVersion
		{
			get
			{
				return 25;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x000549D1 File Offset: 0x00052BD1
		protected override string RootNodeName
		{
			get
			{
				return "ResolveRecipients";
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x000549D8 File Offset: 0x00052BD8
		private SmimeConfigurationContainer SmimeConfiguration
		{
			get
			{
				if (this.smimeConfiguration == null)
				{
					this.smimeConfiguration = CertificateManager.LoadSmimeConfiguration(base.User.OrganizationId, this.GetHashCode());
				}
				return this.smimeConfiguration;
			}
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00054A58 File Offset: 0x00052C58
		internal override Command.ExecutionState ExecuteCommand()
		{
			this.ReadXmlRequest();
			this.InitializeResponseXmlDocument();
			CertificateManager certificateManager = new CertificateManager(base.User.ExchangePrincipal, base.MailboxSession, base.Context.Request.Culture.LCID, this.maxCertificates, this.SmimeConfiguration, base.User.OrganizationId);
			bool searchADFirst = this.ShouldResolveToADFirst();
			List<ResolvedRecipient> list = null;
			if (this.availabilityOptions != null)
			{
				list = new List<ResolvedRecipient>(Math.Min(this.recipientsList.Count, Configuration.MaximumIdentityArraySize));
			}
			if (this.pictureOptions != null)
			{
				this.photoRetriever = new AirSyncPhotoRetriever(base.Context);
			}
			using (AnrManager anrManager = new AnrManager(base.User, base.MailboxSession, base.Context.Request.Culture.LCID, this.maxAmbiguousRecipients))
			{
				foreach (AmbiguousRecipientToResolve ambiguousRecipientToResolve in this.recipientsList)
				{
					anrManager.ResolveOneRecipient(ambiguousRecipientToResolve.Name, searchADFirst, ambiguousRecipientToResolve);
					if (ambiguousRecipientToResolve.ResolvedTo.Count != 0)
					{
						if (!ambiguousRecipientToResolve.ExactMatchFound)
						{
							goto IL_1C8;
						}
						ambiguousRecipientToResolve.Status = StatusCode.Success;
						if (this.certificateRetrieval != ResolveRecipientsCommand.CertificateRetrievalType.None)
						{
							foreach (ResolvedRecipient resolvedRecipient in ambiguousRecipientToResolve.ResolvedTo)
							{
								resolvedRecipient.CertificateRetrieval = this.certificateRetrieval;
							}
							certificateManager.GetRecipientCerts(ambiguousRecipientToResolve);
						}
						if (this.availabilityOptions != null)
						{
							using (List<ResolvedRecipient>.Enumerator enumerator3 = ambiguousRecipientToResolve.ResolvedTo.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									ResolvedRecipient resolvedRecipient2 = enumerator3.Current;
									if (list.Count < Configuration.MaximumIdentityArraySize)
									{
										list.Add(resolvedRecipient2);
									}
									else
									{
										resolvedRecipient2.AvailabilityStatus = StatusCode.AvailabilityTooManyRecipients;
									}
								}
								goto IL_1DC;
							}
							goto IL_1C8;
						}
						IL_1DC:
						ambiguousRecipientToResolve.PictureOptions = this.pictureOptions;
						continue;
						IL_1C8:
						ambiguousRecipientToResolve.Status = (ambiguousRecipientToResolve.CompleteList ? StatusCode.Sync_ProtocolVersionMismatch : StatusCode.Sync_InvalidSyncKey);
						goto IL_1DC;
					}
					ambiguousRecipientToResolve.Status = StatusCode.Sync_ProtocolError;
				}
			}
			if (this.pictureOptions != null && base.Context.User.Features.IsEnabled(EasFeature.HDPhotos) && base.Context.Request.Version >= 160)
			{
				List<string> recipients = new List<string>();
				this.recipientsList.ForEach(delegate(AmbiguousRecipientToResolve recipient)
				{
					if (recipient.Status == StatusCode.Success)
					{
						recipients.AddRange(from s in recipient.ResolvedTo
						select s.ResolvedTo.SmtpAddress);
					}
				});
				this.photoRetriever.BeginGetThumbnailPhotoFromMailbox(recipients, this.pictureOptions.PhotoSize);
			}
			if (this.availabilityOptions != null && list.Count > 0)
			{
				AvailabilityQuery availabilityQuery = this.CreateAvailabilityQuery(list);
				AvailabilityQueryResult result;
				if (this.QueryAvailability(availabilityQuery, list, out result))
				{
					this.FillInAvailabilityData(list, result);
				}
			}
			this.BuildXmlResponse();
			return Command.ExecutionState.Complete;
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00054D94 File Offset: 0x00052F94
		internal override XmlDocument GetValidationErrorXml()
		{
			if (ResolveRecipientsCommand.validationErrorXml == null)
			{
				XmlDocument commandXmlStub = base.GetCommandXmlStub();
				XmlElement xmlElement = commandXmlStub.CreateElement("Status", this.RootNodeNamespace);
				xmlElement.InnerText = XmlConvert.ToString(5);
				commandXmlStub[this.RootNodeName].AppendChild(xmlElement);
				ResolveRecipientsCommand.validationErrorXml = commandXmlStub;
			}
			return ResolveRecipientsCommand.validationErrorXml;
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00054DEC File Offset: 0x00052FEC
		protected override bool HandleQuarantinedState()
		{
			this.InitializeResponseXmlDocument();
			XmlNode xmlNode = base.XmlResponse.CreateElement("Status", "ResolveRecipients:");
			xmlNode.InnerText = 6.ToString(CultureInfo.InvariantCulture);
			this.responseResolveRecipientsNode.AppendChild(xmlNode);
			return false;
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x00054E38 File Offset: 0x00053038
		private void BuildXmlResponse()
		{
			try
			{
				XmlNode xmlNode = base.XmlResponse.CreateElement("Status", "ResolveRecipients:");
				xmlNode.InnerText = 1.ToString(CultureInfo.InvariantCulture);
				this.responseResolveRecipientsNode.AppendChild(xmlNode);
				foreach (AmbiguousRecipientToResolve ambiguousRecipientToResolve in this.recipientsList)
				{
					ambiguousRecipientToResolve.BuildXmlResponse(base.XmlResponse, this.responseResolveRecipientsNode);
				}
			}
			finally
			{
				if (this.photoRetriever != null)
				{
					this.photoRetriever.Dispose();
					this.photoRetriever = null;
				}
			}
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00054EF8 File Offset: 0x000530F8
		private void InitializeResponseXmlDocument()
		{
			base.XmlResponse = new SafeXmlDocument();
			this.responseResolveRecipientsNode = base.XmlResponse.CreateElement("ResolveRecipients", "ResolveRecipients:");
			base.XmlResponse.AppendChild(this.responseResolveRecipientsNode);
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x00054F34 File Offset: 0x00053134
		private void ParseOptionsNode(XmlNode optionsNode)
		{
			foreach (object obj in optionsNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string localName;
				if ((localName = xmlNode.LocalName) != null)
				{
					if (!(localName == "CertificateRetrieval"))
					{
						if (!(localName == "MaxCertificates"))
						{
							if (!(localName == "MaxAmbiguousRecipients"))
							{
								if (localName == "Availability")
								{
									this.ParseAvailabilityNode(xmlNode);
									continue;
								}
								if (localName == "Picture")
								{
									this.pictureOptions = PictureOptions.Parse(xmlNode, StatusCode.Sync_ServerError);
									continue;
								}
							}
							else
							{
								if (!int.TryParse(xmlNode.InnerText, out this.maxAmbiguousRecipients))
								{
									throw new AirSyncPermanentException(StatusCode.Sync_ServerError, this.GetValidationErrorXml(), null, false)
									{
										ErrorStringForProtocolLogger = "BadMaxAmbigValueInResolveRecipients"
									};
								}
								continue;
							}
						}
						else
						{
							if (!int.TryParse(xmlNode.InnerText, out this.maxCertificates))
							{
								throw new AirSyncPermanentException(StatusCode.Sync_ServerError, this.GetValidationErrorXml(), null, false)
								{
									ErrorStringForProtocolLogger = "BadMaxCertsValueInResolveRecipients"
								};
							}
							continue;
						}
					}
					else
					{
						int num;
						if (!int.TryParse(xmlNode.InnerText, out num))
						{
							throw new AirSyncPermanentException(StatusCode.Sync_ServerError, this.GetValidationErrorXml(), null, false)
							{
								ErrorStringForProtocolLogger = "BadCertRetrievalValueInResolveRecipients"
							};
						}
						this.certificateRetrieval = (ResolveRecipientsCommand.CertificateRetrievalType)num;
						if (this.certificateRetrieval != ResolveRecipientsCommand.CertificateRetrievalType.None && this.maxCertificates == 0)
						{
							this.maxCertificates = 65535;
							continue;
						}
						continue;
					}
				}
				throw new AirSyncPermanentException(StatusCode.Sync_ServerError, this.GetValidationErrorXml(), null, false)
				{
					ErrorStringForProtocolLogger = "BadOptionNode(" + xmlNode.LocalName + ")InResolveRecipients"
				};
			}
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x00055100 File Offset: 0x00053300
		private void ParseAvailabilityNode(XmlNode availabilityNode)
		{
			this.availabilityOptions = new ResolveRecipientsCommand.AvailabilityOptions();
			foreach (object obj in availabilityNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string localName;
				if ((localName = xmlNode.LocalName) != null)
				{
					if (!(localName == "StartTime"))
					{
						if (localName == "EndTime")
						{
							ExDateTime exDateTime;
							if (!ExDateTime.TryParseExact(xmlNode.InnerText, "yyyy-MM-dd\\THH:mm:ss.fff\\Z", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out exDateTime))
							{
								throw new AirSyncPermanentException(StatusCode.Sync_ServerError, this.GetValidationErrorXml(), null, false)
								{
									ErrorStringForProtocolLogger = "BadAvailEndTimeNodeInResolveRecipients2"
								};
							}
							this.availabilityOptions.EndTime = exDateTime;
							continue;
						}
					}
					else
					{
						ExDateTime exDateTime;
						if (!ExDateTime.TryParseExact(xmlNode.InnerText, "yyyy-MM-dd\\THH:mm:ss.fff\\Z", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out exDateTime))
						{
							throw new AirSyncPermanentException(StatusCode.Sync_ServerError, this.GetValidationErrorXml(), null, false)
							{
								ErrorStringForProtocolLogger = "BadAvailStartTimeNodeInResolveRecipients1"
							};
						}
						this.availabilityOptions.StartTime = exDateTime;
						continue;
					}
				}
				throw new AirSyncPermanentException(StatusCode.Sync_ServerError, this.GetValidationErrorXml(), null, false)
				{
					ErrorStringForProtocolLogger = "BadAvailOptionNode(" + xmlNode.LocalName + ")InResolveRecipients"
				};
			}
			if (this.availabilityOptions.EndTime == ExDateTime.MinValue)
			{
				this.availabilityOptions.EndTime = this.availabilityOptions.StartTime.AddDays(7.0);
			}
			if (this.availabilityOptions.EndTime <= this.availabilityOptions.StartTime || this.availabilityOptions.EndTime - this.availabilityOptions.StartTime < ResolveRecipientsCommand.AvailabilityOptions.MinimumQueryInterval || this.availabilityOptions.EndTime - this.availabilityOptions.StartTime > ResolveRecipientsCommand.AvailabilityOptions.MaximumQueryInterval)
			{
				throw new AirSyncPermanentException(StatusCode.Sync_ServerError, this.GetValidationErrorXml(), null, false)
				{
					ErrorStringForProtocolLogger = "BadAvailTimes"
				};
			}
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x00055320 File Offset: 0x00053520
		private void ReadXmlRequest()
		{
			XmlNode xmlRequest = base.XmlRequest;
			XmlNode xmlNode = xmlRequest.FirstChild;
			bool flag = false;
			while (xmlNode != null)
			{
				if (xmlNode.LocalName == "To")
				{
					if (string.IsNullOrEmpty(xmlNode.InnerText))
					{
						throw new AirSyncPermanentException(StatusCode.Sync_ServerError, this.GetValidationErrorXml(), null, false)
						{
							ErrorStringForProtocolLogger = "EmptyToNode"
						};
					}
					AmbiguousRecipientToResolve item = new AmbiguousRecipientToResolve(xmlNode.InnerText);
					this.recipientsList.Add(item);
					if (this.recipientsList.Count >= GlobalSettings.MaxRetrievedItems)
					{
						throw new AirSyncPermanentException(StatusCode.Sync_ServerError, this.GetValidationErrorXml(), null, false)
						{
							ErrorStringForProtocolLogger = "TooManyRecipientsToResolve"
						};
					}
				}
				else
				{
					if (!(xmlNode.LocalName == "Options"))
					{
						throw new AirSyncPermanentException(StatusCode.Sync_ServerError, this.GetValidationErrorXml(), null, false)
						{
							ErrorStringForProtocolLogger = "NoOptionsNodeInResolveRecipients"
						};
					}
					if (flag || xmlNode.ChildNodes == null || xmlNode.ChildNodes.Count == 0)
					{
						throw new AirSyncPermanentException(StatusCode.Sync_ServerError, this.GetValidationErrorXml(), null, false)
						{
							ErrorStringForProtocolLogger = "DupeOrEmptyOptionsNodeInResolveRecipients"
						};
					}
					this.ParseOptionsNode(xmlNode);
					flag = true;
				}
				xmlNode = xmlNode.NextSibling;
			}
			if (this.recipientsList.Count == 0)
			{
				throw new AirSyncPermanentException(StatusCode.Sync_ServerError, this.GetValidationErrorXml(), null, false)
				{
					ErrorStringForProtocolLogger = "NoRecipientsInResolveRecipients"
				};
			}
			base.ProtocolLogger.SetValue(ProtocolLoggerData.NumberOfRecipientsToResolve, this.recipientsList.Count);
			base.ProtocolLogger.SetValue(ProtocolLoggerData.AvailabilityRequested, (this.availabilityOptions != null) ? 1 : 0);
			base.ProtocolLogger.SetValue(ProtocolLoggerData.CertificatesRequested, (this.certificateRetrieval != ResolveRecipientsCommand.CertificateRetrievalType.None) ? 1 : 0);
			base.ProtocolLogger.SetValue(ProtocolLoggerData.PictureRequested, (this.pictureOptions != null) ? 1 : 0);
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x000554DC File Offset: 0x000536DC
		private bool ShouldResolveToADFirst()
		{
			UserConfiguration userConfiguration = null;
			bool result;
			try
			{
				userConfiguration = base.MailboxSession.UserConfigurationManager.GetMailboxConfiguration("OWA.UserOptions", UserConfigurationTypes.Dictionary);
				IDictionary dictionary = userConfiguration.GetDictionary();
				if (!dictionary.Contains("checknameincontactsfirst"))
				{
					result = true;
				}
				else
				{
					object obj = dictionary["checknameincontactsfirst"];
					if (!(obj is bool))
					{
						result = true;
					}
					else
					{
						result = !(bool)obj;
					}
				}
			}
			catch (ObjectNotFoundException)
			{
				result = true;
			}
			catch (CorruptDataException)
			{
				result = true;
			}
			catch (InvalidOperationException)
			{
				result = true;
			}
			finally
			{
				if (userConfiguration != null)
				{
					userConfiguration.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0005558C File Offset: 0x0005378C
		private AvailabilityQuery CreateAvailabilityQuery(List<ResolvedRecipient> availabilityList)
		{
			AvailabilityQuery availabilityQuery = new AvailabilityQuery();
			if (availabilityList.Count > Configuration.MaximumIdentityArraySize)
			{
				throw new InvalidOperationException(string.Format("availabilityList.Count ({0}) > Availability.Configuration.MaximumIdentityArraySize ({1})", availabilityList.Count, Configuration.MaximumIdentityArraySize));
			}
			availabilityQuery.MailboxArray = new MailboxData[availabilityList.Count];
			int num = -1;
			foreach (ResolvedRecipient resolvedRecipient in availabilityList)
			{
				num++;
				availabilityQuery.MailboxArray[num] = new MailboxData(new EmailAddress(resolvedRecipient.ResolvedTo.DisplayName, resolvedRecipient.ResolvedTo.RoutingAddress, resolvedRecipient.ResolvedTo.RoutingType));
				availabilityQuery.MailboxArray[num].AttendeeType = MeetingAttendeeType.Required;
			}
			availabilityQuery.ClientContext = ClientContext.Create(base.User.ClientSecurityContextWrapper.ClientSecurityContext, base.User.OrganizationId, base.Budget, ExTimeZone.UtcTimeZone, base.Request.Culture, AvailabilityQuery.CreateNewMessageId());
			availabilityQuery.DesiredFreeBusyView = new FreeBusyViewOptions
			{
				RequestedView = FreeBusyViewType.FreeBusyMerged,
				MergedFreeBusyIntervalInMinutes = 30,
				TimeWindow = new Duration((DateTime)this.availabilityOptions.StartTime, (DateTime)this.availabilityOptions.EndTime)
			};
			return availabilityQuery;
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x000556F0 File Offset: 0x000538F0
		private bool QueryAvailability(AvailabilityQuery availabilityQuery, List<ResolvedRecipient> availabilityList, out AvailabilityQueryResult result)
		{
			result = null;
			AirSyncCounters.NumberOfAvailabilityRequests.Increment();
			try
			{
				result = availabilityQuery.Execute();
			}
			catch (ClientDisconnectedException)
			{
				AirSyncDiagnostics.TraceError(ExTraceGlobals.RequestsTracer, this, "AvailabilityQuery has thrown ClientDisconnectedException");
				base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "ClientDisconnectedException");
				foreach (ResolvedRecipient resolvedRecipient in availabilityList)
				{
					resolvedRecipient.AvailabilityStatus = StatusCode.AvailabilityTransientFailure;
				}
				return false;
			}
			finally
			{
				AirSyncDiagnostics.TraceDebug<StringBuilder>(ExTraceGlobals.RequestsTracer, this, "AvailabilityQuery log:{0}", availabilityQuery.RequestLogger.LogData);
			}
			if (result == null)
			{
				AirSyncDiagnostics.TraceError(ExTraceGlobals.RequestsTracer, this, "AvailabilityQuery has returned null result");
				base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "EmptyAvailabilityResult");
				foreach (ResolvedRecipient resolvedRecipient2 in availabilityList)
				{
					resolvedRecipient2.AvailabilityStatus = StatusCode.AvailabilityFailure;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x00055824 File Offset: 0x00053A24
		private void FillInAvailabilityData(List<ResolvedRecipient> availabilityList, AvailabilityQueryResult result)
		{
			if (result.FreeBusyResults == null || result.FreeBusyResults.Length != availabilityList.Count)
			{
				if (result.FreeBusyResults == null)
				{
					AirSyncDiagnostics.TraceError(ExTraceGlobals.RequestsTracer, this, "result.FreeBusyResults is null");
				}
				else
				{
					AirSyncDiagnostics.TraceError<int, int>(ExTraceGlobals.RequestsTracer, this, "The number of results {0} doesn't match expected {1}", result.FreeBusyResults.Length, availabilityList.Count);
				}
				base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "WrongLengthOfAvailabilityResult");
				foreach (ResolvedRecipient resolvedRecipient in availabilityList)
				{
					resolvedRecipient.AvailabilityStatus = StatusCode.AvailabilityFailure;
				}
				return;
			}
			int num = -1;
			foreach (ResolvedRecipient resolvedRecipient2 in availabilityList)
			{
				num++;
				if (result.FreeBusyResults[num] == null)
				{
					AirSyncDiagnostics.TraceError<int>(ExTraceGlobals.RequestsTracer, this, "result.FreeBusyResults[{0}] is null", num);
					resolvedRecipient2.AvailabilityStatus = StatusCode.AvailabilityFailure;
				}
				else if (result.FreeBusyResults[num].ExceptionInfo != null)
				{
					AirSyncDiagnostics.TraceError<int, LocalizedException>(ExTraceGlobals.RequestsTracer, this, "result.FreeBusyResults[{0}] returned {1}", num, result.FreeBusyResults[num].ExceptionInfo);
					if (result.FreeBusyResults[num].ExceptionInfo is FreeBusyDLLimitReachedException)
					{
						resolvedRecipient2.AvailabilityStatus = StatusCode.AvailabilityDLLimitReached;
					}
					else if (result.FreeBusyResults[num].ExceptionInfo is TransientException || result.FreeBusyResults[num].ExceptionInfo.InnerException is TransientException)
					{
						resolvedRecipient2.AvailabilityStatus = StatusCode.AvailabilityTransientFailure;
					}
					else
					{
						resolvedRecipient2.AvailabilityStatus = StatusCode.AvailabilityFailure;
					}
				}
				else
				{
					if (string.IsNullOrEmpty(result.FreeBusyResults[num].MergedFreeBusy))
					{
						throw new InvalidOperationException("Empty free busy string received!");
					}
					resolvedRecipient2.AvailabilityStatus = StatusCode.Success;
					resolvedRecipient2.MergedFreeBusy = result.FreeBusyResults[num].MergedFreeBusy;
				}
			}
		}

		// Token: 0x04000A2D RID: 2605
		private const string CheckNameInContactsFirstPropertyName = "checknameincontactsfirst";

		// Token: 0x04000A2E RID: 2606
		private const string ConfigurationName = "OWA.UserOptions";

		// Token: 0x04000A2F RID: 2607
		private static XmlDocument validationErrorXml;

		// Token: 0x04000A30 RID: 2608
		private ResolveRecipientsCommand.CertificateRetrievalType certificateRetrieval = ResolveRecipientsCommand.CertificateRetrievalType.None;

		// Token: 0x04000A31 RID: 2609
		private int maxAmbiguousRecipients = 65535;

		// Token: 0x04000A32 RID: 2610
		private int maxCertificates;

		// Token: 0x04000A33 RID: 2611
		private List<AmbiguousRecipientToResolve> recipientsList = new List<AmbiguousRecipientToResolve>();

		// Token: 0x04000A34 RID: 2612
		private XmlNode responseResolveRecipientsNode;

		// Token: 0x04000A35 RID: 2613
		private ResolveRecipientsCommand.AvailabilityOptions availabilityOptions;

		// Token: 0x04000A36 RID: 2614
		private PictureOptions pictureOptions;

		// Token: 0x04000A37 RID: 2615
		private AirSyncPhotoRetriever photoRetriever;

		// Token: 0x04000A38 RID: 2616
		private SmimeConfigurationContainer smimeConfiguration;

		// Token: 0x02000119 RID: 281
		internal enum CertificateRetrievalType
		{
			// Token: 0x04000A3A RID: 2618
			None = 1,
			// Token: 0x04000A3B RID: 2619
			Full,
			// Token: 0x04000A3C RID: 2620
			Mini
		}

		// Token: 0x0200011A RID: 282
		private enum RecipientType
		{
			// Token: 0x04000A3E RID: 2622
			GAL = 1,
			// Token: 0x04000A3F RID: 2623
			Contact
		}

		// Token: 0x0200011B RID: 283
		private class AvailabilityOptions
		{
			// Token: 0x06000EF4 RID: 3828 RVA: 0x00055A38 File Offset: 0x00053C38
			internal AvailabilityOptions()
			{
				this.EndTime = ExDateTime.MinValue;
			}

			// Token: 0x170005B9 RID: 1465
			// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x00055A4B File Offset: 0x00053C4B
			// (set) Token: 0x06000EF6 RID: 3830 RVA: 0x00055A53 File Offset: 0x00053C53
			internal ExDateTime StartTime { get; set; }

			// Token: 0x170005BA RID: 1466
			// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x00055A5C File Offset: 0x00053C5C
			// (set) Token: 0x06000EF8 RID: 3832 RVA: 0x00055A64 File Offset: 0x00053C64
			internal ExDateTime EndTime { get; set; }

			// Token: 0x04000A40 RID: 2624
			internal const int MergedFreeBusyIntervalInMinutes = 30;

			// Token: 0x04000A41 RID: 2625
			internal const int DefaultQueryInterval = 7;

			// Token: 0x04000A42 RID: 2626
			internal static readonly TimeSpan MaximumQueryInterval = new TimeSpan(Configuration.MaximumQueryIntervalDays, 0, 0, 0);

			// Token: 0x04000A43 RID: 2627
			internal static readonly TimeSpan MinimumQueryInterval = new TimeSpan(0, 0, 30, 0);
		}
	}
}
