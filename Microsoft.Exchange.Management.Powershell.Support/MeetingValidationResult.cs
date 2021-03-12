using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.CalendarDiagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Infoworker.MeetingValidator;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000002 RID: 2
	[Serializable]
	public class MeetingValidationResult : ConfigurableObject, IConfigurable, IVersionable, ICloneable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public MeetingValidationResult() : base(new SimplePropertyBag())
		{
			this.MeetingType = string.Empty;
			this.ValidatingRole = string.Empty;
			this.PrimarySmtpAddress = SmtpAddress.Empty;
			this.IntervalStartDate = ExDateTime.MinValue;
			this.IntervalEndDate = ExDateTime.MinValue;
			this.StartTime = ExDateTime.MinValue;
			this.EndTime = ExDateTime.MinValue;
			this.ErrorDescription = string.Empty;
			this.MeetingId = string.Empty;
			this.GlobalObjectId = string.Empty;
			this.CleanGlobalObjectId = string.Empty;
			this.CreationTime = ExDateTime.MinValue;
			this.LastModifiedTime = ExDateTime.MinValue;
			this.Location = string.Empty;
			this.Organizer = string.Empty;
			this.IsConsistent = true;
			this.WasValidationSuccessful = true;
			this.DuplicatesDetected = false;
			this.HasConflicts = false;
			this.ExtractVersion = 0L;
			this.ExtractTime = ExDateTime.MinValue;
			this.NumDelegates = 0;
			this.InternetMessageId = string.Empty;
			this.SequenceNumber = 0;
			this.OwnerApptId = 0;
			this.OwnerCriticalChangeTime = ExDateTime.MinValue;
			this.AttendeeCriticalChangeTime = ExDateTime.MinValue;
			this.WasValidationSuccessful = true;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000021F9 File Offset: 0x000003F9
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MeetingValidationResult.schema;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002200 File Offset: 0x00000400
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002208 File Offset: 0x00000408
		public static MeetingValidationResult CreateOutputObject(MeetingValidationResult result)
		{
			MeetingValidationResult meetingValidationResult = new MeetingValidationResult();
			meetingValidationResult.PrimarySmtpAddress = new SmtpAddress(result.MailboxUserPrimarySmtpAddress);
			meetingValidationResult.ValidatingRole = (result.IsOrganizer ? "Organizer" : "Attendee");
			meetingValidationResult.WasValidationSuccessful = result.WasValidationSuccessful;
			meetingValidationResult.StartTime = result.StartTime;
			meetingValidationResult.EndTime = result.EndTime;
			meetingValidationResult.IntervalStartDate = result.IntervalStartDate;
			meetingValidationResult.IntervalEndDate = result.IntervalEndDate;
			meetingValidationResult.Location = result.Location;
			meetingValidationResult.Subject = result.Subject;
			if (result.MeetingId != null)
			{
				meetingValidationResult.MeetingId = result.MeetingId.ToBase64String();
			}
			meetingValidationResult.MeetingType = result.ItemType.ToString();
			if (result.GlobalObjectId != null)
			{
				meetingValidationResult.GlobalObjectId = result.GlobalObjectId.ToString();
			}
			if (result.CleanGlobalObjectId != null)
			{
				meetingValidationResult.CleanGlobalObjectId = result.CleanGlobalObjectId.ToString();
			}
			meetingValidationResult.CreationTime = result.CreationTime;
			meetingValidationResult.LastModifiedTime = result.LastModifiedTime;
			meetingValidationResult.DuplicatesDetected = result.DuplicatesDetected;
			meetingValidationResult.IsConsistent = result.IsConsistent;
			if (!string.IsNullOrEmpty(result.OrganizerPrimarySmtpAddress))
			{
				meetingValidationResult.Organizer = result.OrganizerPrimarySmtpAddress;
			}
			if (result.MeetingData != null)
			{
				meetingValidationResult.InternetMessageId = result.MeetingData.InternetMessageId;
				meetingValidationResult.ExtractVersion = result.MeetingData.ExtractVersion;
				meetingValidationResult.ExtractTime = result.MeetingData.ExtractTime;
				meetingValidationResult.HasConflicts = result.MeetingData.HasConflicts;
				meetingValidationResult.NumDelegates = result.NumberOfDelegates;
				meetingValidationResult.SequenceNumber = result.MeetingData.SequenceNumber;
				meetingValidationResult.OwnerApptId = (result.MeetingData.OwnerAppointmentId ?? -2);
				meetingValidationResult.OwnerCriticalChangeTime = result.MeetingData.OwnerCriticalChangeTime;
				meetingValidationResult.AttendeeCriticalChangeTime = result.MeetingData.AttendeeCriticalChangeTime;
			}
			if (!string.IsNullOrEmpty(result.ErrorDescription))
			{
				meetingValidationResult.ErrorDescription = result.ErrorDescription;
			}
			return meetingValidationResult;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002414 File Offset: 0x00000614
		public static void FilterResultsLists(MeetingValidationResult cmdLetValidationResult, MeetingValidationResult result, FailureCategory failureCategory, bool onlyReportErrors)
		{
			List<ResultsPerAttendee> list = new List<ResultsPerAttendee>();
			foreach (KeyValuePair<string, MeetingComparisonResult> keyValuePair in result.ResultsPerAttendee)
			{
				ResultsPerAttendee resultsPerAttendee = new ResultsPerAttendee();
				resultsPerAttendee.PrimarySMTPAddress = new SmtpAddress(keyValuePair.Key);
				foreach (ConsistencyCheckResult consistencyCheckResult in keyValuePair.Value)
				{
					if (!onlyReportErrors || consistencyCheckResult.Status != CheckStatusType.Passed)
					{
						foreach (Inconsistency inconsistency in consistencyCheckResult)
						{
							switch (consistencyCheckResult.CheckType)
							{
							case ConsistencyCheckType.CanValidateOwnerCheck:
								if (inconsistency.Flag == CalendarInconsistencyFlag.Organizer)
								{
									resultsPerAttendee.CantOpen = "CanValidateCheck failed " + inconsistency.Description;
								}
								else
								{
									resultsPerAttendee.MailboxUnavailable = "CanValidateCheck failed " + consistencyCheckResult.ErrorString;
								}
								break;
							case ConsistencyCheckType.MeetingExistenceCheck:
								if (inconsistency.Flag == CalendarInconsistencyFlag.Response)
								{
									resultsPerAttendee.IntentionalWrongTrackingInfo = string.Format("MeetingExistenceCheck failed (intent: {0}) {1}", inconsistency.Intent, consistencyCheckResult.ErrorString);
								}
								else if (inconsistency.Flag == CalendarInconsistencyFlag.OrphanedMeeting && ClientIntentQuery.CheckDesiredClientIntent(inconsistency.Intent, new ClientIntentFlags[]
								{
									ClientIntentFlags.MeetingCanceled,
									ClientIntentFlags.MeetingExceptionCanceled
								}))
								{
									resultsPerAttendee.IntentionalMissingMeetings = string.Format("MeetingExistenceCheck failed (intent: {0}) {1}", inconsistency.Intent, consistencyCheckResult.ErrorString);
								}
								else
								{
									resultsPerAttendee.MissingMeetings = string.Format("MeetingExistenceCheck failed (intent: {0}) {1}", inconsistency.Intent, consistencyCheckResult.ErrorString);
								}
								break;
							case ConsistencyCheckType.ValidateStoreObjectCheck:
								resultsPerAttendee.CantOpen = "ValidateStoreObjectCheck failed " + inconsistency.Description;
								break;
							case ConsistencyCheckType.MeetingCancellationCheck:
								resultsPerAttendee.WrongTrackingInfo = "MeetingCancellationCheck failed " + consistencyCheckResult.ErrorString;
								break;
							case ConsistencyCheckType.AttendeeOnListCheck:
								resultsPerAttendee.WrongTrackingInfo = "AttendeeOnListCheck failed " + consistencyCheckResult.ErrorString;
								break;
							case ConsistencyCheckType.CorrectResponseCheck:
							{
								ResponseInconsistency responseInconsistency = inconsistency as ResponseInconsistency;
								if (responseInconsistency != null)
								{
									resultsPerAttendee.WrongTrackingInfo = "CorrectResponseCheck: Attendee has a response that is different from what was sent to the organizer.";
									ResultsPerAttendee resultsPerAttendee2 = resultsPerAttendee;
									resultsPerAttendee2.WrongTrackingInfo += string.Format("User {0} at {1}, the organizer expected {2} at {3}. ", new object[]
									{
										responseInconsistency.ExpectedResponse,
										responseInconsistency.AttendeeReplyTime,
										responseInconsistency.ActualResponse,
										responseInconsistency.OrganizerRecordedTime
									});
									ResultsPerAttendee resultsPerAttendee3 = resultsPerAttendee;
									resultsPerAttendee3.WrongTrackingInfo += responseInconsistency.Description;
								}
								break;
							}
							case ConsistencyCheckType.MeetingPropertiesMatchCheck:
							{
								PropertyInconsistency propertyInconsistency = inconsistency as PropertyInconsistency;
								if (propertyInconsistency != null)
								{
									string text;
									if (result.IsOrganizer)
									{
										text = propertyInconsistency.ActualValue;
									}
									else
									{
										text = propertyInconsistency.ExpectedValue;
									}
									if (propertyInconsistency.PropertyName.Contains("StartTime"))
									{
										Match match = Regex.Match(text, "Start\\[(?<value>.+)\\]");
										resultsPerAttendee.WrongStartTime = (match.Success ? match.Groups["value"].Value : text);
									}
									else if (propertyInconsistency.PropertyName.Contains("EndTime"))
									{
										Match match2 = Regex.Match(text, "End\\[(?<value>.+)\\]");
										resultsPerAttendee.WrongEndTime = (match2.Success ? match2.Groups["value"].Value : text);
									}
									else if (propertyInconsistency.PropertyName.Contains("Location"))
									{
										resultsPerAttendee.WrongLocation = text;
									}
									else if (propertyInconsistency.PropertyName.Contains("MeetingOverlap"))
									{
										resultsPerAttendee.WrongOverlap = text;
									}
									else if (propertyInconsistency.PropertyName.Contains("SequenceNumber"))
									{
										ResultsPerAttendee resultsPerAttendee4 = resultsPerAttendee;
										resultsPerAttendee4.DelayedUpdatesWrongVersion = resultsPerAttendee4.DelayedUpdatesWrongVersion + text + ", ";
									}
									else if (propertyInconsistency.PropertyName.Contains("OwnerCriticalChangeTime"))
									{
										ResultsPerAttendee resultsPerAttendee5 = resultsPerAttendee;
										resultsPerAttendee5.DelayedUpdatesWrongVersion = resultsPerAttendee5.DelayedUpdatesWrongVersion + text + ", ";
									}
									else if (propertyInconsistency.PropertyName.Contains("AttendeeCriticalChangeTime"))
									{
										ResultsPerAttendee resultsPerAttendee6 = resultsPerAttendee;
										resultsPerAttendee6.DelayedUpdatesWrongVersion = resultsPerAttendee6.DelayedUpdatesWrongVersion + text + ", ";
									}
								}
								break;
							}
							case ConsistencyCheckType.RecurrenceBlobsConsistentCheck:
								resultsPerAttendee.RecurrenceProblems = "RecurrenceBlobsConsistentCheck failed " + consistencyCheckResult.ErrorString;
								break;
							case ConsistencyCheckType.RecurrencesMatchCheck:
								resultsPerAttendee.RecurrenceProblems = "RecurrencesMatchCheck failed " + consistencyCheckResult.ErrorString;
								break;
							case ConsistencyCheckType.TimeZoneMatchCheck:
							{
								PropertyInconsistency propertyInconsistency2 = inconsistency as PropertyInconsistency;
								if (propertyInconsistency2 != null)
								{
									resultsPerAttendee.WrongTimeZone = propertyInconsistency2.ActualValue + "(Expected: " + propertyInconsistency2.ExpectedValue + ")";
								}
								break;
							}
							}
						}
					}
				}
				bool flag = false;
				if (failureCategory != FailureCategory.All)
				{
					if ((failureCategory & FailureCategory.DuplicateMeetings) == FailureCategory.DuplicateMeetings && !string.IsNullOrEmpty(resultsPerAttendee.Duplicates))
					{
						flag = true;
					}
					else if ((failureCategory & FailureCategory.WrongLocation) == FailureCategory.WrongLocation && !string.IsNullOrEmpty(resultsPerAttendee.WrongLocation))
					{
						flag = true;
					}
					else if ((failureCategory & FailureCategory.WrongTime) == FailureCategory.WrongTime && (!string.IsNullOrEmpty(resultsPerAttendee.WrongStartTime) || !string.IsNullOrEmpty(resultsPerAttendee.WrongEndTime)))
					{
						flag = true;
					}
					else if ((failureCategory & FailureCategory.WrongTrackingStatus) == FailureCategory.WrongTrackingStatus && !string.IsNullOrEmpty(resultsPerAttendee.WrongTrackingInfo))
					{
						flag = true;
					}
					else if ((failureCategory & FailureCategory.CorruptMeetings) == FailureCategory.CorruptMeetings && (!string.IsNullOrEmpty(resultsPerAttendee.CantOpen) || !string.IsNullOrEmpty(cmdLetValidationResult.ErrorDescription)))
					{
						flag = true;
					}
					else if ((failureCategory & FailureCategory.MissingMeetings) == FailureCategory.MissingMeetings && !string.IsNullOrEmpty(resultsPerAttendee.MissingMeetings))
					{
						flag = true;
					}
					else if ((failureCategory & FailureCategory.RecurrenceProblems) == FailureCategory.RecurrenceProblems && !string.IsNullOrEmpty(resultsPerAttendee.RecurrenceProblems))
					{
						flag = true;
					}
					else if ((failureCategory & FailureCategory.MailboxUnavailable) == FailureCategory.MailboxUnavailable && !string.IsNullOrEmpty(resultsPerAttendee.MailboxUnavailable))
					{
						flag = true;
					}
				}
				else if (!onlyReportErrors || resultsPerAttendee.HasErrors())
				{
					flag = true;
				}
				if (flag)
				{
					list.Add(resultsPerAttendee);
				}
			}
			cmdLetValidationResult.ResultsPerAttendee = list.ToArray();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002A78 File Offset: 0x00000C78
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002A8A File Offset: 0x00000C8A
		public string MeetingType
		{
			get
			{
				return (string)this[MeetingValidationResultSchema.MeetingType];
			}
			internal set
			{
				this[MeetingValidationResultSchema.MeetingType] = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002A98 File Offset: 0x00000C98
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002AAA File Offset: 0x00000CAA
		public string ValidatingRole
		{
			get
			{
				return (string)this[MeetingValidationResultSchema.ValidatingRole];
			}
			internal set
			{
				this[MeetingValidationResultSchema.ValidatingRole] = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002AB8 File Offset: 0x00000CB8
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002ACA File Offset: 0x00000CCA
		public SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return (SmtpAddress)this[MeetingValidationResultSchema.PrimarySmtpAddress];
			}
			internal set
			{
				this[MeetingValidationResultSchema.PrimarySmtpAddress] = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002ADD File Offset: 0x00000CDD
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002AEF File Offset: 0x00000CEF
		public ExDateTime IntervalStartDate
		{
			get
			{
				return (ExDateTime)this[MeetingValidationResultSchema.IntervalStartDate];
			}
			internal set
			{
				this[MeetingValidationResultSchema.IntervalStartDate] = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002B02 File Offset: 0x00000D02
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002B14 File Offset: 0x00000D14
		public ExDateTime IntervalEndDate
		{
			get
			{
				return (ExDateTime)this[MeetingValidationResultSchema.IntervalEndDate];
			}
			internal set
			{
				this[MeetingValidationResultSchema.IntervalEndDate] = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002B27 File Offset: 0x00000D27
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002B39 File Offset: 0x00000D39
		public ExDateTime StartTime
		{
			get
			{
				return (ExDateTime)this[MeetingValidationResultSchema.StartTime];
			}
			internal set
			{
				this[MeetingValidationResultSchema.StartTime] = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002B4C File Offset: 0x00000D4C
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002B5E File Offset: 0x00000D5E
		public ExDateTime EndTime
		{
			get
			{
				return (ExDateTime)this[MeetingValidationResultSchema.EndTime];
			}
			internal set
			{
				this[MeetingValidationResultSchema.EndTime] = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002B71 File Offset: 0x00000D71
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002B83 File Offset: 0x00000D83
		public string ErrorDescription
		{
			get
			{
				return (string)this[MeetingValidationResultSchema.ErrorDescription];
			}
			internal set
			{
				this[MeetingValidationResultSchema.ErrorDescription] = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002B91 File Offset: 0x00000D91
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002BA3 File Offset: 0x00000DA3
		public string MeetingId
		{
			get
			{
				return (string)this[MeetingValidationResultSchema.MeetingId];
			}
			internal set
			{
				this[MeetingValidationResultSchema.MeetingId] = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002BB1 File Offset: 0x00000DB1
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002BC3 File Offset: 0x00000DC3
		public string GlobalObjectId
		{
			get
			{
				return (string)this[MeetingValidationResultSchema.GlobalObjectId];
			}
			internal set
			{
				this[MeetingValidationResultSchema.GlobalObjectId] = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002BD1 File Offset: 0x00000DD1
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002BE3 File Offset: 0x00000DE3
		public string CleanGlobalObjectId
		{
			get
			{
				return (string)this[MeetingValidationResultSchema.CleanGlobalObjectId];
			}
			internal set
			{
				this[MeetingValidationResultSchema.CleanGlobalObjectId] = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002BF1 File Offset: 0x00000DF1
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002C03 File Offset: 0x00000E03
		public ExDateTime CreationTime
		{
			get
			{
				return (ExDateTime)this[MeetingValidationResultSchema.CreationTime];
			}
			internal set
			{
				this[MeetingValidationResultSchema.CreationTime] = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002C16 File Offset: 0x00000E16
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002C28 File Offset: 0x00000E28
		public ExDateTime LastModifiedTime
		{
			get
			{
				return (ExDateTime)this[MeetingValidationResultSchema.LastModifiedTime];
			}
			internal set
			{
				this[MeetingValidationResultSchema.LastModifiedTime] = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002C3B File Offset: 0x00000E3B
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002C4D File Offset: 0x00000E4D
		public string Location
		{
			get
			{
				return (string)this[MeetingValidationResultSchema.Location];
			}
			internal set
			{
				this[MeetingValidationResultSchema.Location] = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002C5B File Offset: 0x00000E5B
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002C6D File Offset: 0x00000E6D
		public string Subject
		{
			get
			{
				return (string)this[MeetingValidationResultSchema.Subject];
			}
			internal set
			{
				this[MeetingValidationResultSchema.Subject] = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002C7B File Offset: 0x00000E7B
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002C8D File Offset: 0x00000E8D
		public string Organizer
		{
			get
			{
				return (string)this[MeetingValidationResultSchema.Organizer];
			}
			internal set
			{
				this[MeetingValidationResultSchema.Organizer] = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002C9B File Offset: 0x00000E9B
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002CAD File Offset: 0x00000EAD
		public bool IsConsistent
		{
			get
			{
				return (bool)this[MeetingValidationResultSchema.IsConsistent];
			}
			internal set
			{
				this[MeetingValidationResultSchema.IsConsistent] = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002CC0 File Offset: 0x00000EC0
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002CD2 File Offset: 0x00000ED2
		public bool DuplicatesDetected
		{
			get
			{
				return (bool)this[MeetingValidationResultSchema.DuplicatesDetected];
			}
			internal set
			{
				this[MeetingValidationResultSchema.DuplicatesDetected] = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002CE5 File Offset: 0x00000EE5
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002CF7 File Offset: 0x00000EF7
		public bool HasConflicts
		{
			get
			{
				return (bool)this[MeetingValidationResultSchema.HasConflicts];
			}
			internal set
			{
				this[MeetingValidationResultSchema.HasConflicts] = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002D0A File Offset: 0x00000F0A
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002D1C File Offset: 0x00000F1C
		public long ExtractVersion
		{
			get
			{
				return (long)this[MeetingValidationResultSchema.ExtractVersion];
			}
			internal set
			{
				this[MeetingValidationResultSchema.ExtractVersion] = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002D2F File Offset: 0x00000F2F
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002D41 File Offset: 0x00000F41
		public ExDateTime ExtractTime
		{
			get
			{
				return (ExDateTime)this[MeetingValidationResultSchema.ExtractTime];
			}
			internal set
			{
				this[MeetingValidationResultSchema.ExtractTime] = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002D54 File Offset: 0x00000F54
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002D66 File Offset: 0x00000F66
		public int NumDelegates
		{
			get
			{
				return (int)this[MeetingValidationResultSchema.NumDelegates];
			}
			internal set
			{
				this[MeetingValidationResultSchema.NumDelegates] = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002D79 File Offset: 0x00000F79
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002D8B File Offset: 0x00000F8B
		public string InternetMessageId
		{
			get
			{
				return (string)this[MeetingValidationResultSchema.InternetMessageId];
			}
			internal set
			{
				this[MeetingValidationResultSchema.InternetMessageId] = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002D99 File Offset: 0x00000F99
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002DAB File Offset: 0x00000FAB
		public int SequenceNumber
		{
			get
			{
				return (int)this[MeetingValidationResultSchema.SequenceNumber];
			}
			internal set
			{
				this[MeetingValidationResultSchema.SequenceNumber] = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002DBE File Offset: 0x00000FBE
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002DD0 File Offset: 0x00000FD0
		public int OwnerApptId
		{
			get
			{
				return (int)this[MeetingValidationResultSchema.OwnerApptId];
			}
			internal set
			{
				this[MeetingValidationResultSchema.OwnerApptId] = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002DE3 File Offset: 0x00000FE3
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002DF5 File Offset: 0x00000FF5
		public ExDateTime OwnerCriticalChangeTime
		{
			get
			{
				return (ExDateTime)this[MeetingValidationResultSchema.OwnerCriticalChangeTime];
			}
			internal set
			{
				this[MeetingValidationResultSchema.OwnerCriticalChangeTime] = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002E08 File Offset: 0x00001008
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002E1A File Offset: 0x0000101A
		public ExDateTime AttendeeCriticalChangeTime
		{
			get
			{
				return (ExDateTime)this[MeetingValidationResultSchema.AttendeeCriticalChangeTime];
			}
			internal set
			{
				this[MeetingValidationResultSchema.AttendeeCriticalChangeTime] = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002E2D File Offset: 0x0000102D
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002E3F File Offset: 0x0000103F
		public bool WasValidationSuccessful
		{
			get
			{
				return (bool)this[MeetingValidationResultSchema.WasValidationSuccessful];
			}
			internal set
			{
				this[MeetingValidationResultSchema.WasValidationSuccessful] = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002E52 File Offset: 0x00001052
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002E5A File Offset: 0x0000105A
		public ResultsPerAttendee[] ResultsPerAttendee
		{
			get
			{
				return this.resultsPerAttendee;
			}
			internal set
			{
				this.resultsPerAttendee = value;
			}
		}

		// Token: 0x04000001 RID: 1
		private ResultsPerAttendee[] resultsPerAttendee;

		// Token: 0x04000002 RID: 2
		private static InMemoryObjectSchema schema = ObjectSchema.GetInstance<MeetingValidationResultSchema>();
	}
}
