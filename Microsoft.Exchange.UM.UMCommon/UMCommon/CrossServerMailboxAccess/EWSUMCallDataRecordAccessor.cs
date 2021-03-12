﻿using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess
{
	// Token: 0x02000083 RID: 131
	internal class EWSUMCallDataRecordAccessor : DisposableBase, IUMCallDataRecordStorage, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000475 RID: 1141 RVA: 0x0000F398 File Offset: 0x0000D598
		public EWSUMCallDataRecordAccessor(ExchangePrincipal user)
		{
			EWSUMCallDataRecordAccessor <>4__this = this;
			ValidateArgument.NotNull(user, "user");
			this.user = user;
			this.tracer = new DiagnosticHelper(this, ExTraceGlobals.XsoTracer);
			PIIMessage pii = PIIMessage.Create(PIIType._SmtpAddress, user.MailboxInfo.PrimarySmtpAddress.ToString());
			this.tracer.Trace(pii, "EWSUMCallDataRecordAccessor for user: _SmtpAddress", new object[0]);
			Exception e = UMMailboxAccessorEwsBinding.ExecuteEWSOperation(delegate
			{
				<>4__this.ewsBinding = new UMMailboxAccessorEwsBinding(user, <>4__this.tracer);
				<>4__this.tracer.Trace("EWSUMCallDataRecordAccessor, EWS Url = {0}", new object[]
				{
					<>4__this.ewsBinding.Url
				});
			}, this.tracer);
			this.CheckForErrors(e);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000F478 File Offset: 0x0000D678
		public void CreateUMCallDataRecord(CDRData cdrData)
		{
			this.tracer.Trace("EWSUMCallDataRecordAccessor : CreateUMCallDataRecord, for call {0}", new object[]
			{
				cdrData.CallIdentity
			});
			ValidateArgument.NotNull(cdrData, "CDRData");
			CreateUMCallDataRecordType request = new CreateUMCallDataRecordType();
			request.CDRData = cdrData.ToCDRDataType();
			CreateUMCallDataRecordResponseMessageType response = null;
			Exception e = UMMailboxAccessorEwsBinding.ExecuteEWSOperation(delegate
			{
				response = this.ewsBinding.CreateUMCallDataRecord(request);
			}, this.tracer);
			this.CheckResponse(e, response);
			this.tracer.Trace("EWSUMCallDataRecordAccessor :Sucessfully saved CallDataRecord, for call {0} ", new object[]
			{
				cdrData.CallIdentity
			});
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000F558 File Offset: 0x0000D758
		public CDRData[] GetUMCallDataRecordsForUser(string userLegacyExchangeDN)
		{
			this.tracer.Trace("EWSUMCallDataRecordAccessor : GetUMCallDataRecordsForUser, for user {0}.", new object[]
			{
				userLegacyExchangeDN
			});
			ValidateArgument.NotNullOrEmpty(userLegacyExchangeDN, "userLegacyExchangeDN");
			GetUMCallDataRecordsType request = new GetUMCallDataRecordsType();
			request.UserLegacyExchangeDN = userLegacyExchangeDN;
			request.FilterBy = UMCDRFilterByType.FilterByUser;
			GetUMCallDataRecordsResponseMessageType response = null;
			Exception e = UMMailboxAccessorEwsBinding.ExecuteEWSOperation(delegate
			{
				response = this.ewsBinding.GetUMCallDataRecords(request);
			}, this.tracer);
			this.CheckResponse(e, response);
			if (response.CallDataRecords != null && response.CallDataRecords.Length > 0)
			{
				List<CDRData> list = response.CallDataRecords.ConvertAll((CDRDataType ewsCdrData) => new CDRData(ewsCdrData));
				this.tracer.Trace("EWSUMCallDataRecordAccessor : GetUMCallDataRecordsForUser, for user {0}. Found {1} CDRData records", new object[]
				{
					userLegacyExchangeDN,
					list.Count
				});
				return list.ToArray();
			}
			this.tracer.Trace("EWSUMCallDataRecordAccessor : GetUMCallDataRecordsForUser, for user {0}. Found no CDRData records for the user.", new object[]
			{
				userLegacyExchangeDN
			});
			return new CDRData[0];
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000F6BC File Offset: 0x0000D8BC
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
			if (numberOfRecordsToRead > 5000)
			{
				numberOfRecordsToRead = 5000;
			}
			this.tracer.Trace("EWSUMCallDataRecordAccessor : GetUMCallDataRecords with start date {0}, end date {1}, offset {2}, number of records {3}", new object[]
			{
				startDateTime,
				endDateTime,
				offset,
				numberOfRecordsToRead
			});
			GetUMCallDataRecordsType request = new GetUMCallDataRecordsType();
			request.StartDateTime = new DateTime(startDateTime.Year, startDateTime.Month, startDateTime.Day, startDateTime.Hour, startDateTime.Minute, startDateTime.Second);
			request.StartDateTimeSpecified = true;
			request.EndDateTime = new DateTime(endDateTime.Year, endDateTime.Month, endDateTime.Day, endDateTime.Hour, endDateTime.Minute, endDateTime.Second);
			request.EndDateTimeSpecified = true;
			request.Offset = offset;
			request.OffsetSpecified = true;
			request.NumberOfRecords = numberOfRecordsToRead;
			request.NumberOfRecordsSpecified = true;
			request.FilterBy = UMCDRFilterByType.FilterByDate;
			GetUMCallDataRecordsResponseMessageType response = null;
			Exception e = UMMailboxAccessorEwsBinding.ExecuteEWSOperation(delegate
			{
				response = this.ewsBinding.GetUMCallDataRecords(request);
			}, this.tracer);
			this.CheckResponse(e, response);
			if (response.CallDataRecords != null && response.CallDataRecords.Length > 0)
			{
				List<CDRData> list = response.CallDataRecords.ConvertAll((CDRDataType ewsCdrData) => new CDRData(ewsCdrData));
				this.tracer.Trace("EWSUMCallDataRecordAccessor : GetUMCallDataRecords with start date {0}, end date {1}. Found {2} CDRData records", new object[]
				{
					startDateTime,
					endDateTime,
					list.Count
				});
				return list.ToArray();
			}
			this.tracer.Trace("EWSUMCallDataRecordAccessor : GetUMCallDataRecords with start date {0}, end date {1}. Found no CDRData records", new object[]
			{
				startDateTime,
				endDateTime
			});
			return new CDRData[0];
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000F968 File Offset: 0x0000DB68
		public UMReportRawCounters[] GetUMCallSummary(Guid dialPlanGuid, Guid gatewayGuid, GroupBy groupby)
		{
			this.tracer.Trace("EWSUMCallDataRecordAccessor : GetUMCallSummary with dial plan {0}, Gateway {1} Request.", new object[]
			{
				dialPlanGuid,
				gatewayGuid
			});
			GetUMCallSummaryType request = new GetUMCallSummaryType();
			request.DailPlanGuid = dialPlanGuid.ToString();
			request.GatewayGuid = gatewayGuid.ToString();
			request.GroupRecordsBy = (UMCDRGroupByType)Enum.Parse(typeof(UMCDRGroupByType), groupby.ToString());
			GetUMCallSummaryResponseMessageType response = null;
			Exception e = UMMailboxAccessorEwsBinding.ExecuteEWSOperation(delegate
			{
				response = this.ewsBinding.GetUMCallSummary(request);
			}, this.tracer);
			this.CheckResponse(e, response);
			if (response.UMReportRawCountersCollection != null && response.UMReportRawCountersCollection.Length > 0)
			{
				List<UMReportRawCounters> list = response.UMReportRawCountersCollection.ConvertAll((UMReportRawCountersType ewsReportRawCounters) => new UMReportRawCounters(ewsReportRawCounters));
				this.tracer.Trace("EWSUMCallDataRecordAccessor : GetUMCallSummary with dial plan {0}, Gateway {1}. Found {2} UMReportRawCounters records", new object[]
				{
					dialPlanGuid,
					gatewayGuid,
					list.Count
				});
				return list.ToArray();
			}
			this.tracer.Trace("EWSUMCallDataRecordAccessor : GetUMCallSummary with dial plan {0}, Gateway {1}. Found no UMReportRawCounters records", new object[]
			{
				dialPlanGuid,
				gatewayGuid
			});
			return new UMReportRawCounters[0];
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000FB03 File Offset: 0x0000DD03
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.tracer.Trace("EWSUMCallDataRecordAccessor : InternalDispose", new object[0]);
				if (this.ewsBinding != null)
				{
					this.ewsBinding.Dispose();
				}
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000FB31 File Offset: 0x0000DD31
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<EWSUMCallDataRecordAccessor>(this);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000FB3C File Offset: 0x0000DD3C
		private void CheckForErrors(Exception e)
		{
			if (e != null)
			{
				this.tracer.Trace("EWSUMCallDataRecordAccessor : CheckForErrors, Exception: {0}", new object[]
				{
					e
				});
				throw new CDROperationException(e.Message, e);
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000FB78 File Offset: 0x0000DD78
		private void CheckResponse(Exception e, ResponseMessageType response)
		{
			this.CheckForErrors(e);
			if (response == null)
			{
				this.tracer.Trace("EWSUMCallDataRecordAccessor : CheckResponse, response == null", new object[0]);
				throw new EWSUMMailboxAccessException(Strings.EWSNoResponseReceived);
			}
			this.tracer.Trace("EWSUMCallDataRecordAccessor : CheckResponse, ResponseCode = {0}, ResponseClass = {1}, MessageText = {2}", new object[]
			{
				response.ResponseCode,
				response.ResponseClass,
				response.MessageText
			});
			if (response.ResponseClass != ResponseClassType.Success || response.ResponseCode != ResponseCodeType.NoError)
			{
				ResponseCodeType responseCode = response.ResponseCode;
				if (responseCode <= ResponseCodeType.ErrorInternalServerTransientError)
				{
					if (responseCode <= ResponseCodeType.ErrorContentIndexingNotEnabled)
					{
						if (responseCode != ResponseCodeType.ErrorConnectionFailed)
						{
							if (responseCode != ResponseCodeType.ErrorContentIndexingNotEnabled)
							{
								goto IL_185;
							}
							throw new ContentIndexingNotEnabledException();
						}
					}
					else if (responseCode != ResponseCodeType.ErrorExceededConnectionCount)
					{
						switch (responseCode)
						{
						case ResponseCodeType.ErrorInsufficientResources:
						case ResponseCodeType.ErrorInternalServerTransientError:
							break;
						case ResponseCodeType.ErrorInternalServerError:
							goto IL_185;
						default:
							goto IL_185;
						}
					}
				}
				else if (responseCode <= ResponseCodeType.ErrorMailboxStoreUnavailable)
				{
					if (responseCode == ResponseCodeType.ErrorItemNotFound)
					{
						throw new ObjectNotFoundException(Strings.CDROperationObjectNotFound(this.user.MailboxInfo.PrimarySmtpAddress.ToString()));
					}
					switch (responseCode)
					{
					case ResponseCodeType.ErrorMailboxMoveInProgress:
					case ResponseCodeType.ErrorMailboxStoreUnavailable:
						break;
					default:
						goto IL_185;
					}
				}
				else
				{
					if (responseCode == ResponseCodeType.ErrorQuotaExceeded)
					{
						throw new QuotaExceededException(Strings.CDROperationQuotaExceededError(response.MessageText));
					}
					if (responseCode != ResponseCodeType.ErrorServerBusy)
					{
						if (responseCode != ResponseCodeType.ErrorUnifiedMessagingReportDataNotFound)
						{
							goto IL_185;
						}
						throw new UnableToFindUMReportDataException(this.user.MailboxInfo.PrimarySmtpAddress.ToString());
					}
				}
				throw new CDROperationException(Strings.CDROperationTransientError(response.MessageText));
				IL_185:
				throw new EWSUMMailboxAccessException(Strings.EWSOperationFailed(response.ResponseCode.ToString(), response.MessageText));
			}
		}

		// Token: 0x040002F3 RID: 755
		private const int MaxCDRBatchSize = 5000;

		// Token: 0x040002F4 RID: 756
		private UMMailboxAccessorEwsBinding ewsBinding;

		// Token: 0x040002F5 RID: 757
		private DiagnosticHelper tracer;

		// Token: 0x040002F6 RID: 758
		private ExchangePrincipal user;
	}
}
