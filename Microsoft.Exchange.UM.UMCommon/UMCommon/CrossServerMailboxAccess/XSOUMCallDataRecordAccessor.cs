using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.UM;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess
{
	// Token: 0x0200008B RID: 139
	internal class XSOUMCallDataRecordAccessor : DisposableBase, IUMCallDataRecordStorage, IDisposeTrackable, IDisposable
	{
		// Token: 0x060004D6 RID: 1238 RVA: 0x00011D0C File Offset: 0x0000FF0C
		public XSOUMCallDataRecordAccessor(ExchangePrincipal mailboxPrincipal)
		{
			ValidateArgument.NotNull(mailboxPrincipal, "mailboxPrincipal");
			this.mailboxPrincipal = mailboxPrincipal;
			this.tracer = new DiagnosticHelper(this, ExTraceGlobals.XsoTracer);
			this.disposeMailboxSession = true;
			this.ExecuteXSOOperation(delegate
			{
				this.Initialize(this.CreateMailboxSession("Client=UM;Action=Manage-CDRMessages"));
			});
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00011D62 File Offset: 0x0000FF62
		public XSOUMCallDataRecordAccessor(MailboxSession mailboxSession)
		{
			ValidateArgument.NotNull(mailboxSession, "mailboxSession");
			this.tracer = new DiagnosticHelper(this, ExTraceGlobals.XsoTracer);
			this.Initialize(mailboxSession);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00011E70 File Offset: 0x00010070
		public void CreateUMCallDataRecord(CDRData cdrData)
		{
			ValidateArgument.NotNull(cdrData, "cdrData");
			this.tracer.Trace("XSOUMCallDataRecordAccessor : CreateUMCallDataRecord, saving cdrData {0}", new object[]
			{
				cdrData.CallIdentity
			});
			this.ExecuteXSOOperation(delegate
			{
				using (Folder folder = UMStagingFolder.OpenOrCreateUMReportingFolder(this.mailboxSession))
				{
					MessageItem messageItem2;
					MessageItem messageItem = messageItem2 = XsoUtil.CreateTemporaryMessage(this.mailboxSession, folder, 90);
					try
					{
						if (!this.TrySetExtendedProperty())
						{
							this.tracer.Trace("Unable to set the extended prop on UMReporting Folder. Not saving the CDR", new object[0]);
							return;
						}
						this.SetMessageProperties(messageItem, cdrData);
						messageItem.Save(SaveMode.NoConflictResolution);
					}
					finally
					{
						if (messageItem2 != null)
						{
							((IDisposable)messageItem2).Dispose();
						}
					}
				}
				this.tracer.Trace("XSOUMCallDataRecordAccessor : CreateUMCallDataRecord, Successfully saved cdrData {0}", new object[]
				{
					cdrData.CallIdentity
				});
			});
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00011F10 File Offset: 0x00010110
		public CDRData[] GetUMCallDataRecordsForUser(string userLegacyExchangeDN)
		{
			ValidateArgument.NotNullOrEmpty(userLegacyExchangeDN, "userLegacyExchangeDN");
			this.tracer.Trace("XSOUMCallDataRecordAccessor : GetUMCallDataRecordsForUser, for user {0}", new object[]
			{
				userLegacyExchangeDN
			});
			CDRData[] cdrs = null;
			this.ExecuteXSOOperation(delegate
			{
				SearchState searchState;
				cdrs = UMReportUtil.PerformCDRSearchUsingCI(this.mailboxSession, userLegacyExchangeDN, out searchState);
			});
			if (cdrs == null)
			{
				cdrs = new CDRData[0];
			}
			this.tracer.Trace("XSOUMCallDataRecordAccessor : GetUMCallDataRecordsForUser, for user {0}. Found {1} CDRData records", new object[]
			{
				userLegacyExchangeDN,
				cdrs.Length
			});
			return cdrs;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00012000 File Offset: 0x00010200
		public CDRData[] GetUMCallDataRecords(ExDateTime startDateTime, ExDateTime endDateTime, int offset, int numberOfRecordsToRead)
		{
			if (ExDateTime.Equals(startDateTime, ExDateTime.MinValue))
			{
				throw new ArgumentException("Start time is not valid");
			}
			if (ExDateTime.Equals(endDateTime, ExDateTime.MinValue))
			{
				throw new ArgumentException("End time is not valid");
			}
			if (offset < 0)
			{
				throw new ArgumentException("Offset should be greater than or equal to 0");
			}
			if (numberOfRecordsToRead < 0)
			{
				throw new ArgumentException("NumberOfRecords should be greater than or equal to 0");
			}
			if (numberOfRecordsToRead > 500000)
			{
				numberOfRecordsToRead = 500000;
			}
			this.tracer.Trace("XSOUMCallDataRecordAccessor : GetUMCallDataRecords with start date {0}, end date {1}, offset {2}, number of records {3}", new object[]
			{
				startDateTime,
				endDateTime,
				offset,
				numberOfRecordsToRead
			});
			CDRData[] cdrs = null;
			this.ExecuteXSOOperation(delegate
			{
				cdrs = UMReportUtil.ReadCDRs(this.mailboxSession, startDateTime, endDateTime, offset, numberOfRecordsToRead);
			});
			if (cdrs == null)
			{
				cdrs = new CDRData[0];
			}
			this.tracer.Trace("XSOUMCallDataRecordAccessor : GetUMCallDataRecords with start date {0}, end date {1}. Found {2} CDRData records", new object[]
			{
				startDateTime,
				endDateTime,
				cdrs.Length
			});
			return cdrs;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x000121AC File Offset: 0x000103AC
		public UMReportRawCounters[] GetUMCallSummary(Guid dialPlanGuid, Guid gatewayGuid, GroupBy groupby)
		{
			this.tracer.Trace("XSOUMCallDataRecordAccessor : GetUMCallSummary with dial plan {0}, Gateway {1} Request.", new object[]
			{
				dialPlanGuid,
				gatewayGuid
			});
			UMReportRawCounters[] counters = null;
			this.ExecuteXSOOperation(delegate
			{
				counters = UMReportUtil.QueryUMReport(this.mailboxSession, dialPlanGuid, gatewayGuid, groupby);
			});
			if (counters == null)
			{
				counters = new UMReportRawCounters[0];
			}
			this.tracer.Trace("XSOUMCallDataRecordAccessor : GetUMCallSummary with dial plan {0}, Gateway {1}. Found {2} UMReportRawCounters records", new object[]
			{
				dialPlanGuid,
				gatewayGuid,
				counters.Length
			});
			return counters;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00012286 File Offset: 0x00010486
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.tracer.Trace("XSOCallDataRecordStorage : InternalDispose", new object[0]);
				if (this.mailboxSession != null && this.disposeMailboxSession)
				{
					this.mailboxSession.Dispose();
				}
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x000122BC File Offset: 0x000104BC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<XSOUMCallDataRecordAccessor>(this);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000122C4 File Offset: 0x000104C4
		private MailboxSession CreateMailboxSession(string clientString)
		{
			ValidateArgument.NotNullOrEmpty(clientString, "clientString");
			return MailboxSessionEstablisher.OpenAsAdmin(this.mailboxPrincipal, CultureInfo.InvariantCulture, clientString);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x000122F0 File Offset: 0x000104F0
		private bool TrySetExtendedProperty()
		{
			bool result = false;
			try
			{
				UMReportUtil.SetMailboxExtendedProperty(this.mailboxSession, true);
				result = true;
			}
			catch (FolderSaveException ex)
			{
				this.tracer.Trace("Cannot set the extended prop on the mailbox. Details {0}", new object[]
				{
					ex
				});
				if (ex.InnerException != null && ex.InnerException is PropertyErrorException)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CannotSetExtendedProp, null, new object[]
					{
						this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress,
						CommonUtil.ToEventLogString(ex)
					});
				}
			}
			return result;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00012394 File Offset: 0x00010594
		private void SetMessageProperties(MessageItem messageToSubmit, CDRData cdrData)
		{
			messageToSubmit.ClassName = "IPM.Note.Microsoft.CDR.UM";
			messageToSubmit[MessageItemSchema.XCDRDataCallStartTime] = new ExDateTime(ExTimeZone.UtcTimeZone, cdrData.CallStartTime);
			messageToSubmit.From = new Participant(cdrData.CallerLegacyExchangeDN, string.Empty, "EX");
			messageToSubmit.Recipients.Add(new Participant(cdrData.CalleeLegacyExchangeDN, string.Empty, "EX"));
			messageToSubmit[MessageItemSchema.XCDRDataCallType] = cdrData.CallType;
			messageToSubmit[MessageItemSchema.XCDRDataCallIdentity] = cdrData.CallIdentity;
			messageToSubmit[MessageItemSchema.XCDRDataParentCallIdentity] = cdrData.ParentCallIdentity;
			messageToSubmit[MessageItemSchema.XCDRDataUMServerName] = cdrData.UMServerName;
			messageToSubmit[MessageItemSchema.XCDRDataDialPlanGuid] = cdrData.DialPlanGuid;
			messageToSubmit[MessageItemSchema.XCDRDataDialPlanName] = cdrData.DialPlanName;
			messageToSubmit[MessageItemSchema.XCDRDataCallDuration] = cdrData.CallDuration;
			messageToSubmit[MessageItemSchema.XCDRDataIPGatewayAddress] = cdrData.IPGatewayAddress;
			messageToSubmit[MessageItemSchema.XCDRDataIPGatewayName] = cdrData.IPGatewayName;
			messageToSubmit[MessageItemSchema.XCDRDataGatewayGuid] = cdrData.GatewayGuid;
			messageToSubmit[MessageItemSchema.XCDRDataCalledPhoneNumber] = cdrData.CalledPhoneNumber;
			messageToSubmit[MessageItemSchema.XCDRDataCallerPhoneNumber] = cdrData.CallerPhoneNumber;
			messageToSubmit[MessageItemSchema.XCDRDataOfferResult] = cdrData.OfferResult;
			messageToSubmit[MessageItemSchema.XCDRDataDropCallReason] = cdrData.DropCallReason;
			messageToSubmit[MessageItemSchema.XCDRDataReasonForCall] = cdrData.ReasonForCall;
			messageToSubmit[MessageItemSchema.XCDRDataTransferredNumber] = cdrData.TransferredNumber;
			messageToSubmit[MessageItemSchema.XCDRDataDialedString] = cdrData.DialedString;
			messageToSubmit[MessageItemSchema.XCDRDataCallerMailboxAlias] = cdrData.CallerMailboxAlias;
			messageToSubmit[MessageItemSchema.XCDRDataCalleeMailboxAlias] = cdrData.CalleeMailboxAlias;
			messageToSubmit[MessageItemSchema.XCDRDataAutoAttendantName] = cdrData.AutoAttendantName;
			messageToSubmit[MessageItemSchema.XCDRDataAudioCodec] = cdrData.AudioQualityMetrics.AudioCodec;
			messageToSubmit[MessageItemSchema.XCDRDataBurstDensity] = cdrData.AudioQualityMetrics.BurstDensity;
			messageToSubmit[MessageItemSchema.XCDRDataBurstDuration] = cdrData.AudioQualityMetrics.BurstDuration;
			messageToSubmit[MessageItemSchema.XCDRDataJitter] = cdrData.AudioQualityMetrics.Jitter;
			messageToSubmit[MessageItemSchema.XCDRDataNMOS] = cdrData.AudioQualityMetrics.NMOS;
			messageToSubmit[MessageItemSchema.XCDRDataNMOSDegradation] = cdrData.AudioQualityMetrics.NMOSDegradation;
			messageToSubmit[MessageItemSchema.XCDRDataNMOSDegradationJitter] = cdrData.AudioQualityMetrics.NMOSDegradationJitter;
			messageToSubmit[MessageItemSchema.XCDRDataNMOSDegradationPacketLoss] = cdrData.AudioQualityMetrics.NMOSDegradationPacketLoss;
			messageToSubmit[MessageItemSchema.XCDRDataPacketLoss] = cdrData.AudioQualityMetrics.PacketLoss;
			messageToSubmit[MessageItemSchema.XCDRDataRoundTrip] = cdrData.AudioQualityMetrics.RoundTrip;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00012674 File Offset: 0x00010874
		private void Initialize(MailboxSession session)
		{
			ExAssert.RetailAssert(session != null, "MailboxSession cannot be null");
			this.mailboxSession = session;
			this.tracer.Trace("XSOUMCallDataRecordAccessor called from WebServices : {1}", new object[]
			{
				!this.disposeMailboxSession
			});
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000126C4 File Offset: 0x000108C4
		private void ExecuteXSOOperation(Action function)
		{
			try
			{
				function();
			}
			catch (Exception ex)
			{
				if (this.mailboxPrincipal != null)
				{
					XsoUtil.LogMailboxConnectionFailureException(ex, this.mailboxPrincipal);
				}
				CallIdTracer.TraceError(ExTraceGlobals.UMReportsTracer, this, ex.ToString(), new object[0]);
				throw;
			}
		}

		// Token: 0x0400030D RID: 781
		private const int MessageRetentionPeriod = 90;

		// Token: 0x0400030E RID: 782
		private readonly bool disposeMailboxSession;

		// Token: 0x0400030F RID: 783
		private ExchangePrincipal mailboxPrincipal;

		// Token: 0x04000310 RID: 784
		private MailboxSession mailboxSession;

		// Token: 0x04000311 RID: 785
		private DiagnosticHelper tracer;
	}
}
