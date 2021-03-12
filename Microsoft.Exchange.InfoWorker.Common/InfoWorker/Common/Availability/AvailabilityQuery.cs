using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000E9 RID: 233
	internal sealed class AvailabilityQuery : Query<AvailabilityQueryResult>
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x0001A3B0 File Offset: 0x000185B0
		public AvailabilityQuery() : base(CasTraceEventType.Availability, FreeBusyApplication.FreeBusyIOCompletion, PerformanceCounters.CurrentRequests)
		{
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x0001A3C3 File Offset: 0x000185C3
		// (set) Token: 0x06000608 RID: 1544 RVA: 0x0001A3CB File Offset: 0x000185CB
		public MailboxData[] MailboxArray
		{
			get
			{
				return this.mailboxArray;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					throw new IdentityArrayEmptyException();
				}
				if (value.Length > Configuration.MaximumIdentityArraySize)
				{
					throw new IdentityArrayTooBigException(Configuration.MaximumIdentityArraySize, value.Length);
				}
				this.mailboxArray = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x0001A3FA File Offset: 0x000185FA
		// (set) Token: 0x0600060A RID: 1546 RVA: 0x0001A402 File Offset: 0x00018602
		public FreeBusyViewOptions DesiredFreeBusyView
		{
			get
			{
				return this.desiredFreeBusyView;
			}
			set
			{
				if (value != null)
				{
					value.Validate();
				}
				this.desiredFreeBusyView = value;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0001A414 File Offset: 0x00018614
		// (set) Token: 0x0600060C RID: 1548 RVA: 0x0001A41C File Offset: 0x0001861C
		public SuggestionsViewOptions DesiredSuggestionsView
		{
			get
			{
				return this.desiredSuggestionsView;
			}
			set
			{
				if (value != null)
				{
					value.Validate();
				}
				this.desiredSuggestionsView = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0001A42E File Offset: 0x0001862E
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x0001A436 File Offset: 0x00018636
		public bool DefaultFreeBusyOnly
		{
			get
			{
				return this.defaultFreeBusyOnly;
			}
			set
			{
				this.defaultFreeBusyOnly = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0001A43F File Offset: 0x0001863F
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x0001A447 File Offset: 0x00018647
		public bool IsCrossForestRequest { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001A450 File Offset: 0x00018650
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x0001A458 File Offset: 0x00018658
		public double PreExecutionTimeTaken { get; set; }

		// Token: 0x06000613 RID: 1555 RVA: 0x0001A464 File Offset: 0x00018664
		internal void UpdateClientReportedPerfCounters(string messageId, DateTime requestTime, int responseTime, int responseSize, int responseCode, int[] recipientErrorCodes)
		{
			TimeSpan t = DateTime.UtcNow.Subtract(requestTime);
			if (t.Ticks < 0L)
			{
				t = DateTime.UtcNow.AddHours(24.0).Subtract(requestTime);
				if (t.Ticks < 0L)
				{
					return;
				}
			}
			if (t < TimeSpan.FromHours(24.0))
			{
				int num = responseCode;
				int num2 = 200;
				if (num == num2 && recipientErrorCodes != null)
				{
					for (int i = 0; i < recipientErrorCodes.Length; i++)
					{
						if (recipientErrorCodes[i] > 0 && AvailabilityQuery.IsServerError(recipientErrorCodes[i]))
						{
							num = recipientErrorCodes[i];
							break;
						}
					}
				}
				if (num != num2)
				{
					PerformanceCounters.FailedClientReportedRequestsTotal.Increment();
					ErrorConstants errorConstants = (ErrorConstants)num;
					if (errorConstants == ErrorConstants.ServiceDiscoveryFailed)
					{
						PerformanceCounters.FailedClientRequestsNoASUrl.Increment();
						return;
					}
					if (errorConstants == ErrorConstants.TimeoutExpired)
					{
						PerformanceCounters.FailedClientRequestsTimeouts.Increment();
						return;
					}
					PerformanceCounters.FailedClientRequestsPartialOrOther.Increment();
					return;
				}
				else
				{
					PerformanceCounters.PastTotalClientSuccessRequests.Increment();
					int num3 = (int)Math.Round((double)(responseTime / 1000));
					if (num3 <= 5)
					{
						PerformanceCounters.PastClientRequestsUnder5.Increment();
					}
					if (num3 <= 10)
					{
						PerformanceCounters.PastClientRequestsUnder10.Increment();
					}
					if (num3 <= 20)
					{
						PerformanceCounters.PastClientRequestsUnder20.Increment();
					}
					if (num3 > 20)
					{
						PerformanceCounters.PastClientRequestsOver20.Increment();
					}
				}
			}
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001A5B8 File Offset: 0x000187B8
		protected override void ValidateSpecificInputData()
		{
			if (base.ClientContext.TimeZone == null)
			{
				throw new MissingArgumentException(Strings.descMissingArgument("TimeZone"));
			}
			if (this.mailboxArray == null || this.mailboxArray.Length == 0)
			{
				throw new MissingArgumentException(Strings.descMissingArgument("MailboxDataArray"));
			}
			for (int i = 0; i < this.mailboxArray.Length; i++)
			{
				if (this.mailboxArray[i] == null)
				{
					throw new MissingArgumentException(Strings.descMissingMailboxArrayElement(i.ToString()));
				}
			}
			if (this.desiredFreeBusyView == null && this.desiredSuggestionsView == null)
			{
				throw new MissingArgumentException(Strings.descFreeBusyAndSuggestionsNull);
			}
			if (this.desiredFreeBusyView != null && this.desiredSuggestionsView != null)
			{
				Duration detailedSuggestionsWindow = this.desiredSuggestionsView.DetailedSuggestionsWindow;
				Duration timeWindow = this.DesiredFreeBusyView.TimeWindow;
				if (detailedSuggestionsWindow.StartTime < timeWindow.StartTime || detailedSuggestionsWindow.EndTime > timeWindow.EndTime)
				{
					throw new InvalidParameterException(Strings.descInvalidSuggestionsTimeRange);
				}
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001A6A8 File Offset: 0x000188A8
		protected override AvailabilityQueryResult ExecuteInternal()
		{
			AvailabilityQueryResult availabilityQueryResult = AvailabilityQueryResult.Create();
			this.internalFreeBusyView = (this.desiredFreeBusyView ?? FreeBusyViewOptions.CreateDefaultForMeetingSuggestions(this.desiredSuggestionsView.DetailedSuggestionsWindow));
			this.ExecuteFreeBusyQuery(availabilityQueryResult);
			if (this.desiredSuggestionsView != null)
			{
				this.ExecuteMeetingSuggestionsQuery(availabilityQueryResult);
			}
			if (this.desiredFreeBusyView == null)
			{
				availabilityQueryResult.FreeBusyResults = null;
			}
			return availabilityQueryResult;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0001A704 File Offset: 0x00018904
		protected override void UpdateCountersAtExecuteEnd(Stopwatch responseTimer)
		{
			if (this.desiredSuggestionsView == null)
			{
				PerformanceCounters.AverageFreeBusyRequestProcessingTime.IncrementBy(responseTimer.ElapsedTicks);
				PerformanceCounters.AverageFreeBusyRequestProcessingTimeBase.Increment();
			}
			else
			{
				PerformanceCounters.AverageSuggestionsRequestProcessingTime.IncrementBy(responseTimer.ElapsedTicks);
				PerformanceCounters.AverageSuggestionsRequestProcessingTimeBase.Increment();
				PerformanceCounters.SuggestionsRequestsPerSecond.Increment();
			}
			PerformanceCounters.RequestsPerSecond.Increment();
			PerformanceCounters.AverageMailboxCountPerRequest.IncrementBy((long)this.individualMailboxesProcessed);
			PerformanceCounters.AverageMailboxCountPerRequestBase.Increment();
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001A788 File Offset: 0x00018988
		protected override void AppendSpecificSpExecuteOperationData(StringBuilder spOperationData)
		{
			if (this.desiredSuggestionsView != null)
			{
				spOperationData.AppendFormat(", Good Threshold: {0}, MaximumResultsByDay: {1}, MaximumNonWorkHourResultsByDay: {2}", this.desiredSuggestionsView.GoodThreshold, this.desiredSuggestionsView.MaximumResultsByDay, this.desiredSuggestionsView.MaximumNonWorkHourResultsByDay);
				spOperationData.AppendFormat(", MeetingDuration: {0} minutes, MinimumSuggestionQuality: {1}", this.desiredSuggestionsView.MeetingDurationInMinutes, this.desiredSuggestionsView.MinimumSuggestionQuality);
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001A808 File Offset: 0x00018A08
		public static string CreateNewMessageId()
		{
			return "urn:uuid:" + Guid.NewGuid().ToString();
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001A834 File Offset: 0x00018A34
		private static bool IsServerError(int errorCode)
		{
			if (errorCode <= 5012)
			{
				switch (errorCode)
				{
				case 5001:
				case 5002:
				case 5003:
					break;
				default:
					if (errorCode != 5009 && errorCode != 5012)
					{
						goto IL_7C;
					}
					break;
				}
			}
			else
			{
				switch (errorCode)
				{
				case 5026:
				case 5028:
				case 5029:
				case 5032:
					break;
				case 5027:
				case 5030:
				case 5031:
					goto IL_7C;
				default:
					switch (errorCode)
					{
					case 5036:
					case 5037:
						break;
					default:
						if (errorCode != 5043)
						{
							goto IL_7C;
						}
						break;
					}
					break;
				}
			}
			return false;
			IL_7C:
			return true;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001A8C0 File Offset: 0x00018AC0
		private void ExecuteFreeBusyQuery(AvailabilityQueryResult availabilityQueryResult)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			AvailabilityQuery.CalendarViewTracer.TraceDebug((long)this.GetHashCode(), "{0}: Entering AvailabilityQuery.ExecuteFreeBusyQuery()", new object[]
			{
				TraceContext.Get()
			});
			base.RequestLogger.AppendToLog<int>("AddressCount", this.MailboxArray.Length);
			base.RequestLogger.AppendToLog<string>("MessageId", base.ClientContext.MessageId);
			base.RequestLogger.AppendToLog<string>("Requester", base.ClientContext.IdentityForFilteredTracing);
			try
			{
				Guid serverRequestId = Microsoft.Exchange.Diagnostics.Trace.TraceCasStart(this.casTraceEventType);
				QueryType queryType = ((this.desiredFreeBusyView != null) ? QueryType.FreeBusy : ((QueryType)0)) | ((this.desiredSuggestionsView != null) ? QueryType.MeetingSuggestions : ((QueryType)0));
				ADObjectId adobjectId = null;
				OrganizationId organizationId = OrganizationId.ForestWideOrgId;
				if (base.ClientContext is InternalClientContext && !this.IsCrossForestRequest)
				{
					adobjectId = base.ClientContext.QueryBaseDN;
					organizationId = base.ClientContext.OrganizationId;
				}
				if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled && (organizationId == null || organizationId == OrganizationId.ForestWideOrgId))
				{
					AvailabilityQuery.CalendarViewTracer.TraceDebug<object, EmailAddress>((long)this.GetHashCode(), "{0}: Looking up the OrgId for address {1}", TraceContext.Get(), this.mailboxArray[0].Email);
					organizationId = this.GetTargetOrganizationIdFromCache(this.mailboxArray[0].Email);
				}
				if (organizationId != null && organizationId.OrganizationalUnit != null)
				{
					base.RequestLogger.AppendToLog<string>("ORG", organizationId.OrganizationalUnit.Name);
				}
				AvailabilityQuery.CalendarViewTracer.TraceDebug((long)this.GetHashCode(), "{0}: Ad scoping for the requester {1}: QueryBaseDN: {2}, OrganizationId: {3}", new object[]
				{
					TraceContext.Get(),
					base.ClientContext.ToString(),
					(adobjectId == null) ? "<NULL>" : adobjectId.ToString(),
					organizationId
				});
				FreeBusyApplication freeBusyApplication = new FreeBusyApplication(base.ClientContext, this.internalFreeBusyView, this.defaultFreeBusyOnly, queryType);
				try
				{
					this.queryPrepareDeadline = this.queryPrepareDeadline.AddMilliseconds(-this.PreExecutionTimeTaken);
					this.requestProcessingDeadline = this.requestProcessingDeadline.AddMilliseconds(-this.PreExecutionTimeTaken);
					using (RequestDispatcher requestDispatcher = new RequestDispatcher(base.RequestLogger))
					{
						FreeBusyRecipientQuery freeBusyRecipientQuery = new FreeBusyRecipientQuery(base.ClientContext, adobjectId, organizationId, this.queryPrepareDeadline);
						RecipientQueryResults recipientQueryResults = freeBusyRecipientQuery.Query(this.mailboxArray);
						QueryGenerator queryGenerator = new QueryGenerator(freeBusyApplication, base.ClientContext, base.RequestLogger, requestDispatcher, this.queryPrepareDeadline, this.requestProcessingDeadline, recipientQueryResults);
						bool flag = false;
						try
						{
							BaseQuery[] queries = queryGenerator.GetQueries();
							this.freeBusyQueryArray = FreeBusyApplication.ConvertBaseToFreeBusyQuery(queries);
							if (base.ClientContext.Budget != null)
							{
								base.ClientContext.Budget.EndLocal();
								flag = true;
							}
							requestDispatcher.Execute(this.requestProcessingDeadline, base.HttpResponse);
							this.individualMailboxesProcessed = queryGenerator.UniqueQueriesCount;
						}
						finally
						{
							if (flag)
							{
								base.ClientContext.Budget.StartLocal("AvailabilityQuery.ExecuteFreeBusyQuery", default(TimeSpan));
							}
							requestDispatcher.LogStatistics(base.RequestLogger);
						}
					}
				}
				finally
				{
					freeBusyApplication.LogThreadsUsage(base.RequestLogger);
				}
				foreach (FreeBusyQuery freeBusyQuery in this.freeBusyQueryArray)
				{
					if (freeBusyQuery.AttendeeKind == AttendeeKind.Group)
					{
						FreeBusyQueryResult resultOnFirstCall;
						if (freeBusyQuery.GroupMembersForFreeBusy != null)
						{
							resultOnFirstCall = MergedFreeBusy.MergeGroupMemberResults(base.ClientContext.TimeZone, this.internalFreeBusyView.MergedFreeBusyIntervalInMinutes, new ExDateTime(base.ClientContext.TimeZone, this.internalFreeBusyView.TimeWindow.StartTime), new ExDateTime(base.ClientContext.TimeZone, this.internalFreeBusyView.TimeWindow.EndTime), freeBusyQuery.GroupMembersForFreeBusy, base.ClientContext.RequestSchemaVersion);
						}
						else
						{
							resultOnFirstCall = new FreeBusyQueryResult(FreeBusyViewType.None, null, null, null);
						}
						freeBusyQuery.SetResultOnFirstCall(resultOnFirstCall);
					}
				}
				availabilityQueryResult.FreeBusyResults = new FreeBusyQueryResult[this.freeBusyQueryArray.Length];
				for (int j = 0; j < this.freeBusyQueryArray.Length; j++)
				{
					availabilityQueryResult.FreeBusyResults[j] = this.freeBusyQueryArray[j].Result;
				}
				this.TraceExecuteFreeBusyQueryStop(serverRequestId);
				AvailabilityQuery.CalendarViewTracer.TraceDebug((long)this.GetHashCode(), "{0}: Leaving AvailabilityQuery.ExecuteFreeBusyQuery()", new object[]
				{
					TraceContext.Get()
				});
			}
			finally
			{
				base.RequestLogger.CalculateQueryStatistics(this.freeBusyQueryArray);
				base.LogFailures(availabilityQueryResult, base.RequestLogger.ExceptionData);
				stopwatch.Stop();
				base.RequestLogger.AppendToLog<long>("TAQ", stopwatch.ElapsedMilliseconds);
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001ADC8 File Offset: 0x00018FC8
		private void TraceExecuteFreeBusyQueryStop(Guid serverRequestId)
		{
			if (ETWTrace.ShouldTraceCasStop(serverRequestId))
			{
				base.ServerName = Query<AvailabilityQueryResult>.GetCurrentHttpRequestServerName();
				StringBuilder stringBuilder = new StringBuilder();
				if (this.desiredFreeBusyView != null)
				{
					stringBuilder.AppendFormat("Time Window {0} to {1}; MergedFreeBusyIntervalInMinutes: {2}; RequestedView: {3}", new object[]
					{
						this.desiredFreeBusyView.TimeWindow.StartTime,
						this.desiredFreeBusyView.TimeWindow.EndTime,
						this.desiredFreeBusyView.MergedFreeBusyIntervalInMinutes,
						this.desiredFreeBusyView.RequestedView
					});
				}
				Microsoft.Exchange.Diagnostics.Trace.TraceCasStop(this.casTraceEventType, serverRequestId, 0, 0, base.ServerName, TraceContext.Get(), "AvailabilityQuery::ExecuteFreeBusyQuery", stringBuilder, string.Empty);
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001AE88 File Offset: 0x00019088
		private void ExecuteMeetingSuggestionsQuery(AvailabilityQueryResult result)
		{
			AvailabilityQuery.MeetingSuggestionsTracer.TraceDebug((long)this.GetHashCode(), "{0}: Entering AvailabilityQuery.ExecuteMeetingSuggestionsQuery()", new object[]
			{
				TraceContext.Get()
			});
			base.RequestLogger.AppendToLog<int>("suggest", 1);
			try
			{
				MeetingSuggester meetingSuggester = new MeetingSuggester();
				meetingSuggester.SetOptionsFromSuggestionsViewOptions(this.desiredSuggestionsView, base.ClientContext.TimeZone);
				List<AttendeeData> list = new List<AttendeeData>(this.mailboxArray.Length);
				List<int>[] array = new List<int>[this.mailboxArray.Length];
				int num = 0;
				for (int i = 0; i < this.freeBusyQueryArray.Length; i++)
				{
					MailboxData mailboxData = this.mailboxArray[i];
					FreeBusyQuery freeBusyQuery = this.freeBusyQueryArray[i];
					List<int> list2 = new List<int>();
					switch (freeBusyQuery.AttendeeKind)
					{
					case AttendeeKind.Individual:
					case AttendeeKind.Unknown:
						this.AddAttendeeDataToList(mailboxData, freeBusyQuery, list);
						list2.Add(num);
						num++;
						break;
					case AttendeeKind.Group:
						if (freeBusyQuery.GroupMembersForSuggestions != null)
						{
							foreach (FreeBusyQuery freeBusyQuery2 in freeBusyQuery.GroupMembersForSuggestions)
							{
								this.AddAttendeeDataToList(mailboxData, freeBusyQuery2, list);
								list2.Add(num);
								num++;
							}
						}
						else
						{
							this.AddAttendeeDataToList(mailboxData, freeBusyQuery, list);
							list2.Add(num);
							num++;
						}
						break;
					}
					array[i] = list2;
				}
				AttendeeData[] attendees = list.ToArray();
				Duration detailedSuggestionsWindow = this.desiredSuggestionsView.DetailedSuggestionsWindow;
				result.DailyMeetingSuggestions = meetingSuggester.GetSuggestionsByDateRange(new ExDateTime(base.ClientContext.TimeZone, detailedSuggestionsWindow.StartTime), new ExDateTime(base.ClientContext.TimeZone, detailedSuggestionsWindow.EndTime), base.ClientContext.TimeZone, this.desiredSuggestionsView.MeetingDurationInMinutes, attendees);
				foreach (SuggestionDayResult suggestionDayResult in result.DailyMeetingSuggestions)
				{
					foreach (Suggestion suggestion in suggestionDayResult.SuggestionArray)
					{
						AttendeeConflictData[] array2 = new AttendeeConflictData[this.mailboxArray.Length];
						for (int m = 0; m < this.freeBusyQueryArray.Length; m++)
						{
							FreeBusyQuery freeBusyQuery3 = this.freeBusyQueryArray[m];
							List<int> list3 = array[m];
							switch (freeBusyQuery3.AttendeeKind)
							{
							case AttendeeKind.Individual:
							{
								int num2 = list3[0];
								array2[m] = suggestion.AttendeeConflictDataArray[num2];
								break;
							}
							case AttendeeKind.Group:
								if (freeBusyQuery3.GroupMembersForSuggestions != null)
								{
									int num3 = 0;
									int num4 = 0;
									int num5 = 0;
									foreach (int num6 in list3)
									{
										IndividualAttendeeConflictData individualAttendeeConflictData = (IndividualAttendeeConflictData)suggestion.AttendeeConflictDataArray[num6];
										if (individualAttendeeConflictData.IsMissingFreeBusyData)
										{
											num4++;
										}
										else if (individualAttendeeConflictData.BusyType == BusyType.Free)
										{
											num5++;
										}
										else
										{
											num3++;
										}
									}
									array2[m] = GroupAttendeeConflictData.Create(list3.Count, num3, num5, num4);
								}
								else
								{
									array2[m] = TooBigGroupAttendeeConflictData.Create();
								}
								break;
							case AttendeeKind.Unknown:
								array2[m] = UnknownAttendeeConflictData.Create();
								break;
							}
						}
						suggestion.AttendeeConflictDataArray = array2;
					}
				}
			}
			catch (LocalizedException ex)
			{
				ErrorHandler.SetErrorCodeIfNecessary(ex, ErrorConstants.MeetingSuggestionGenerationFailed);
				result.MeetingSuggestionsException = ex;
			}
			AvailabilityQuery.MeetingSuggestionsTracer.TraceDebug((long)this.GetHashCode(), "{0}: Leaving AvailabilityQuery.ExecuteMeetingSuggestionsQuery()", new object[]
			{
				TraceContext.Get()
			});
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001B240 File Offset: 0x00019440
		private void AddAttendeeDataToList(MailboxData mailboxData, FreeBusyQuery freeBusyQuery, List<AttendeeData> attendeeDataList)
		{
			if (freeBusyQuery.Result == null)
			{
				AvailabilityQuery.MeetingSuggestionsTracer.TraceError<object, EmailAddress>((long)this.GetHashCode(), "{0}: Null freebusyquery result for attendee {1}, treating as if we got no data for this attendee", TraceContext.Get(), freeBusyQuery.Email);
			}
			string text = (freeBusyQuery.Result != null) ? freeBusyQuery.Result.MergedFreeBusy : null;
			if (string.IsNullOrEmpty(text) || (this.desiredSuggestionsView.GlobalObjectId != null && mailboxData.AttendeeType == MeetingAttendeeType.Organizer))
			{
				text = MergedFreeBusy.GenerateMergedFreeBusyString(base.ClientContext.TimeZone, this.internalFreeBusyView.MergedFreeBusyIntervalInMinutes, new ExDateTime(base.ClientContext.TimeZone, this.internalFreeBusyView.TimeWindow.StartTime), new ExDateTime(base.ClientContext.TimeZone, this.internalFreeBusyView.TimeWindow.EndTime), (freeBusyQuery.Result != null) ? freeBusyQuery.Result.CalendarEventArrayInternal : null, freeBusyQuery.Result == null || freeBusyQuery.Result.ExceptionInfo != null, this.desiredSuggestionsView.GlobalObjectIdByteArray, base.ClientContext.RequestSchemaVersion);
			}
			AttendeeData item = new AttendeeData(mailboxData.AttendeeType, freeBusyQuery.Email.Address, mailboxData.ExcludeConflicts, new ExDateTime(base.ClientContext.TimeZone, this.internalFreeBusyView.TimeWindow.StartTime), new ExDateTime(base.ClientContext.TimeZone, this.internalFreeBusyView.TimeWindow.EndTime), text, base.ClientContext.RequestSchemaVersion, this.GetAttendeeWorkHours(freeBusyQuery));
			attendeeDataList.Add(item);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001B3CC File Offset: 0x000195CC
		private AttendeeWorkHours GetAttendeeWorkHours(FreeBusyQuery freeBusyQuery)
		{
			AttendeeWorkHours attendeeWorkHours = null;
			try
			{
				if (freeBusyQuery != null && freeBusyQuery.Result != null)
				{
					attendeeWorkHours = new AttendeeWorkHours(freeBusyQuery.Result.WorkingHours);
				}
			}
			catch (ArgumentException arg)
			{
				AvailabilityQuery.MeetingSuggestionsTracer.TraceError<object, string, ArgumentException>((long)this.GetHashCode(), "{0}: Using default working hours for {1} since we got an exception while constructing AttendeeWorkHours. The Exception is: {2}", TraceContext.Get(), freeBusyQuery.Email.Address, arg);
			}
			if (attendeeWorkHours == null)
			{
				attendeeWorkHours = new AttendeeWorkHours(null);
			}
			return attendeeWorkHours;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001B440 File Offset: 0x00019640
		private OrganizationId GetTargetOrganizationIdFromCache(EmailAddress targetAddress)
		{
			return DomainToOrganizationIdCache.Singleton.Get(new SmtpDomain(targetAddress.Domain));
		}

		// Token: 0x0400037F RID: 895
		private static readonly Microsoft.Exchange.Diagnostics.Trace CalendarViewTracer = ExTraceGlobals.CalendarViewTracer;

		// Token: 0x04000380 RID: 896
		private static readonly Microsoft.Exchange.Diagnostics.Trace MeetingSuggestionsTracer = ExTraceGlobals.MeetingSuggestionsTracer;

		// Token: 0x04000381 RID: 897
		private FreeBusyQuery[] freeBusyQueryArray;

		// Token: 0x04000382 RID: 898
		private MailboxData[] mailboxArray;

		// Token: 0x04000383 RID: 899
		private FreeBusyViewOptions desiredFreeBusyView;

		// Token: 0x04000384 RID: 900
		private SuggestionsViewOptions desiredSuggestionsView;

		// Token: 0x04000385 RID: 901
		private FreeBusyViewOptions internalFreeBusyView;

		// Token: 0x04000386 RID: 902
		private bool defaultFreeBusyOnly;
	}
}
