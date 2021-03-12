using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Infoworker.MeetingValidator;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000004 RID: 4
	[Cmdlet("Get", "CalendarValidationResult", DefaultParameterSetName = "Identity")]
	public sealed class GetCalendarValidationResult : GetRecipientObjectTask<MailboxIdParameter, ADUser>
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002E70 File Offset: 0x00001070
		public GetCalendarValidationResult()
		{
			int num;
			ThreadPool.GetMaxThreads(out this.maxThreadPoolThreads, out num);
			this.validatorObjectCount = -1;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002E97 File Offset: 0x00001097
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002EAE File Offset: 0x000010AE
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, Position = 0)]
		public new MailboxIdParameter Identity
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002EC1 File Offset: 0x000010C1
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002EE6 File Offset: 0x000010E6
		[Parameter(Mandatory = false)]
		public ExDateTime IntervalStartDate
		{
			get
			{
				return (ExDateTime)(base.Fields["IntervalStartDate"] ?? ExDateTime.Today);
			}
			set
			{
				base.Fields["IntervalStartDate"] = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002F00 File Offset: 0x00001100
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002F41 File Offset: 0x00001141
		[Parameter(Mandatory = false)]
		public ExDateTime IntervalEndDate
		{
			get
			{
				return (ExDateTime)(base.Fields["IntervalEndDate"] ?? ExDateTime.Today.AddDays(1.0));
			}
			set
			{
				base.Fields["IntervalEndDate"] = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002F59 File Offset: 0x00001159
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002F7A File Offset: 0x0000117A
		[Parameter(Mandatory = false)]
		public bool OnlyReportErrors
		{
			get
			{
				return (bool)(base.Fields["OnlyReportErrors"] ?? true);
			}
			set
			{
				base.Fields["OnlyReportErrors"] = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002F92 File Offset: 0x00001192
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002FB3 File Offset: 0x000011B3
		[Parameter(Mandatory = false)]
		public bool IncludeAnalysis
		{
			get
			{
				return (bool)(base.Fields["IncludeAnalysis"] ?? false);
			}
			set
			{
				base.Fields["IncludeAnalysis"] = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002FCB File Offset: 0x000011CB
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002FEB File Offset: 0x000011EB
		[Parameter(Mandatory = false)]
		public string Location
		{
			get
			{
				return (string)(base.Fields["Location"] ?? string.Empty);
			}
			set
			{
				base.Fields["Location"] = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002FFE File Offset: 0x000011FE
		// (set) Token: 0x0600004F RID: 79 RVA: 0x0000301E File Offset: 0x0000121E
		[Parameter(Mandatory = false)]
		public string Subject
		{
			get
			{
				return (string)(base.Fields["Subject"] ?? string.Empty);
			}
			set
			{
				base.Fields["Subject"] = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003031 File Offset: 0x00001231
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00003051 File Offset: 0x00001251
		[Parameter(Mandatory = false, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true)]
		public string MeetingID
		{
			get
			{
				return (string)(base.Fields["MeetingID"] ?? string.Empty);
			}
			set
			{
				base.Fields["MeetingID"] = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003064 File Offset: 0x00001264
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00003089 File Offset: 0x00001289
		[Parameter(Mandatory = false)]
		public FailureCategory FailureCategoryType
		{
			get
			{
				return (FailureCategory)(base.Fields["FailureCategoryType"] ?? FailureCategory.All);
			}
			set
			{
				base.Fields["FailureCategoryType"] = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000030A1 File Offset: 0x000012A1
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000030B8 File Offset: 0x000012B8
		[ValidateRange(1, 100)]
		[Parameter(Mandatory = false)]
		public int MaxThreads
		{
			get
			{
				return (int)base.Fields["MaxThreads"];
			}
			set
			{
				base.Fields["MaxThreads"] = value;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000030D0 File Offset: 0x000012D0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.Fields["MaxThreads"] != null && this.MaxThreads > this.maxThreadPoolThreads)
			{
				base.WriteError(new ArgumentException(Strings.ErrorMaxThreadPoolThreads(this.maxThreadPoolThreads)), ErrorCategory.InvalidOperation, this);
			}
			if (this.IntervalStartDate >= this.IntervalEndDate)
			{
				base.WriteError(new ArgumentException(Strings.ErrorStartDateEqualGreaterThanEndDate(this.IntervalStartDate.ToString(), this.IntervalEndDate.ToString())), ErrorCategory.InvalidOperation, this);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000317C File Offset: 0x0000137C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			if (this.Identity != null)
			{
				LocalizedString? localizedString;
				IEnumerable<ADUser> dataObjects = base.GetDataObjects(this.Identity, base.OptionalIdentityData, out localizedString);
				this.WriteResult<ADUser>(dataObjects);
				if (!base.HasErrors && base.WriteObjectCount == 0U && this.validatorObjectCount == -1)
				{
					string source = (base.DataSession != null) ? base.DataSession.Source : null;
					Exception exception = new ManagementObjectNotFoundException(localizedString ?? base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(ADUser).ToString(), source));
					base.WriteError(exception, ErrorCategory.InvalidData, null);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003244 File Offset: 0x00001444
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			if (dataObject == null)
			{
				return;
			}
			try
			{
				ADUser user = (ADUser)dataObject;
				this.DoCalendarValidation(user);
			}
			catch (StorageTransientException exception)
			{
				base.WriteError(exception, ErrorCategory.ReadError, null);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000329C File Offset: 0x0000149C
		private void DoCalendarValidation(ADUser user)
		{
			List<MeetingValidationResult> list = null;
			base.WriteProgress(Strings.CalendarValidationTask, Strings.ValidatingCalendar(this.Identity.ToString()), 0);
			try
			{
				TopologyProvider.SetProcessTopologyMode(false, false);
				string mailboxUserAddress = string.Format("SMTP:{0}", user.PrimarySmtpAddress);
				CalendarValidator calendarValidator;
				if (!string.IsNullOrEmpty(this.MeetingID))
				{
					VersionedId meetingId = new VersionedId(this.MeetingID);
					calendarValidator = CalendarValidator.CreateMeetingSpecificValidatingInstance(mailboxUserAddress, user.OrganizationId, base.RootOrgContainerId, meetingId);
				}
				else
				{
					calendarValidator = CalendarValidator.CreateRangeValidatingInstance(mailboxUserAddress, user.OrganizationId, base.RootOrgContainerId, this.IntervalStartDate, this.IntervalEndDate, this.Location, this.Subject);
				}
				list = calendarValidator.Run();
			}
			catch (WrongServerException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, this);
			}
			catch (MailboxUserNotFoundException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidData, this);
			}
			catch (CorruptDataException exception3)
			{
				base.WriteError(exception3, ErrorCategory.InvalidData, this);
			}
			finally
			{
				TopologyProvider.SetProcessTopologyMode(true, false);
			}
			this.validatorObjectCount = 0;
			int num = (list.Count > 0) ? list.Count : 1;
			foreach (MeetingValidationResult meetingValidationResult in list)
			{
				if (!this.OnlyReportErrors || !meetingValidationResult.IsConsistent || !meetingValidationResult.WasValidationSuccessful || meetingValidationResult.DuplicatesDetected)
				{
					MeetingValidationResult meetingValidationResult2 = MeetingValidationResult.CreateOutputObject(meetingValidationResult);
					MeetingValidationResult.FilterResultsLists(meetingValidationResult2, meetingValidationResult, this.FailureCategoryType, this.OnlyReportErrors);
					this.validatorObjectCount++;
					bool flag = true;
					if (meetingValidationResult2.ResultsPerAttendee.Length < 1)
					{
						flag = false;
					}
					if ((this.FailureCategoryType & FailureCategory.CorruptMeetings) == FailureCategory.CorruptMeetings && !meetingValidationResult2.WasValidationSuccessful)
					{
						flag = true;
					}
					else if ((this.FailureCategoryType & FailureCategory.DuplicateMeetings) == FailureCategory.DuplicateMeetings && meetingValidationResult2.DuplicatesDetected)
					{
						flag = true;
					}
					if (flag)
					{
						meetingValidationResult2.SetIsReadOnly(true);
						base.WriteResult(meetingValidationResult2);
					}
					base.WriteProgress(Strings.CalendarValidationTask, Strings.ValidatingCalendar(this.Identity.ToString()), this.validatorObjectCount / num * 100);
				}
			}
			base.WriteProgress(Strings.CalendarValidationTask, Strings.ValidatingCalendar(this.Identity.ToString()), 100);
		}

		// Token: 0x0400000E RID: 14
		private const int DefaultInitialObjectCount = -1;

		// Token: 0x0400000F RID: 15
		private int maxThreadPoolThreads;

		// Token: 0x04000010 RID: 16
		private int validatorObjectCount;
	}
}
