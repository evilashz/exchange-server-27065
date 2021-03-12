using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000015 RID: 21
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ResponseInconsistency : Inconsistency
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00004920 File Offset: 0x00002B20
		private ResponseInconsistency()
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004928 File Offset: 0x00002B28
		private ResponseInconsistency(string description, ResponseType attendeeResponse, ResponseType organizerResponse, ExDateTime attendeeReplyTime, ExDateTime organizerRecordedTime, CalendarValidationContext context) : base(RoleType.Organizer, description, CalendarInconsistencyFlag.Response, context)
		{
			this.ExpectedResponse = attendeeResponse;
			this.ActualResponse = organizerResponse;
			this.AttendeeReplyTime = attendeeReplyTime;
			this.OrganizerRecordedTime = organizerRecordedTime;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004954 File Offset: 0x00002B54
		internal static ResponseInconsistency CreateInstance(ResponseType attendeeResponse, ResponseType organizerResponse, ExDateTime attendeeReplyTime, ExDateTime organizerRecordedTime, CalendarValidationContext context)
		{
			return ResponseInconsistency.CreateInstance(string.Empty, attendeeResponse, organizerResponse, attendeeReplyTime, organizerRecordedTime, context);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004966 File Offset: 0x00002B66
		internal static ResponseInconsistency CreateInstance(string description, ResponseType attendeeResponse, ResponseType organizerResponse, ExDateTime attendeeReplyTime, ExDateTime organizerRecordedTime, CalendarValidationContext context)
		{
			return new ResponseInconsistency(description, attendeeResponse, organizerResponse, attendeeReplyTime, organizerRecordedTime, context);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004978 File Offset: 0x00002B78
		internal override RumInfo CreateRumInfo(CalendarValidationContext context, IList<Attendee> attendees)
		{
			if (this.AttendeeReplyTime.Equals(ExDateTime.MinValue))
			{
				return NullOpRumInfo.CreateInstance();
			}
			switch (this.ExpectedResponse)
			{
			default:
				return NullOpRumInfo.CreateInstance();
			case ResponseType.Tentative:
			case ResponseType.Accept:
			case ResponseType.Decline:
				return ResponseRumInfo.CreateMasterInstance();
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000049D0 File Offset: 0x00002BD0
		// (set) Token: 0x06000092 RID: 146 RVA: 0x000049D8 File Offset: 0x00002BD8
		internal ResponseType ExpectedResponse { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000049E1 File Offset: 0x00002BE1
		// (set) Token: 0x06000094 RID: 148 RVA: 0x000049E9 File Offset: 0x00002BE9
		internal ResponseType ActualResponse { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000049F2 File Offset: 0x00002BF2
		// (set) Token: 0x06000096 RID: 150 RVA: 0x000049FA File Offset: 0x00002BFA
		internal ExDateTime AttendeeReplyTime { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00004A03 File Offset: 0x00002C03
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00004A0B File Offset: 0x00002C0B
		internal ExDateTime OrganizerRecordedTime { get; private set; }
	}
}
