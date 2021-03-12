using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200022F RID: 559
	public enum LastChangeAction
	{
		// Token: 0x04001050 RID: 4176
		None,
		// Token: 0x04001051 RID: 4177
		AttachmentChange,
		// Token: 0x04001052 RID: 4178
		AttachmentRemoved,
		// Token: 0x04001053 RID: 4179
		CancelMeeting,
		// Token: 0x04001054 RID: 4180
		Create,
		// Token: 0x04001055 RID: 4181
		CreateForward,
		// Token: 0x04001056 RID: 4182
		CreateForwardNotification,
		// Token: 0x04001057 RID: 4183
		ModifyReminder,
		// Token: 0x04001058 RID: 4184
		CreateReply,
		// Token: 0x04001059 RID: 4185
		DeleteOccurrence,
		// Token: 0x0400105A RID: 4186
		DismissReminder,
		// Token: 0x0400105B RID: 4187
		MakeModifiedOccurrence,
		// Token: 0x0400105C RID: 4188
		RecipientAdded,
		// Token: 0x0400105D RID: 4189
		RespondToMeetingRequest,
		// Token: 0x0400105E RID: 4190
		SendMeetingCancellations,
		// Token: 0x0400105F RID: 4191
		SendMeetingMessages,
		// Token: 0x04001060 RID: 4192
		SetBody,
		// Token: 0x04001061 RID: 4193
		SnoozeReminder,
		// Token: 0x04001062 RID: 4194
		RecipientRemoved,
		// Token: 0x04001063 RID: 4195
		UpdateCalendarItem,
		// Token: 0x04001064 RID: 4196
		UpdateSequenceNumber,
		// Token: 0x04001065 RID: 4197
		AttachmentAdded,
		// Token: 0x04001066 RID: 4198
		AcrPerformed,
		// Token: 0x04001067 RID: 4199
		UnDeleteException,
		// Token: 0x04001068 RID: 4200
		PreProcessMeetingMessage,
		// Token: 0x04001069 RID: 4201
		RecoverDeletedOccurance,
		// Token: 0x0400106A RID: 4202
		SetFlag,
		// Token: 0x0400106B RID: 4203
		SetVersion,
		// Token: 0x0400106C RID: 4204
		SetCalendarProcessingSteps,
		// Token: 0x0400106D RID: 4205
		CopyParticipantsToCalendarItem,
		// Token: 0x0400106E RID: 4206
		SetForwardedAttendees,
		// Token: 0x0400106F RID: 4207
		CopyMeetingRequestProperties,
		// Token: 0x04001070 RID: 4208
		ProcessParticipants,
		// Token: 0x04001071 RID: 4209
		SetDefaultMeetingRequestType,
		// Token: 0x04001072 RID: 4210
		SetUnsendableRecipients,
		// Token: 0x04001073 RID: 4211
		ProcessCounterProposal,
		// Token: 0x04001074 RID: 4212
		ReplayInboundContent,
		// Token: 0x04001075 RID: 4213
		ProcessMeetingRequest,
		// Token: 0x04001076 RID: 4214
		ProcessMeetingResponse,
		// Token: 0x04001077 RID: 4215
		ProcessMeetingCancellation,
		// Token: 0x04001078 RID: 4216
		ProcessMeetingForwardNotification,
		// Token: 0x04001079 RID: 4217
		UpdateRumSent,
		// Token: 0x0400107A RID: 4218
		ForwardRumSent,
		// Token: 0x0400107B RID: 4219
		CancellationRumSent,
		// Token: 0x0400107C RID: 4220
		ResponseRumSent,
		// Token: 0x0400107D RID: 4221
		CompareToCalendarItem,
		// Token: 0x0400107E RID: 4222
		MeetingMessageOutdated,
		// Token: 0x0400107F RID: 4223
		SmartPropertyFixup,
		// Token: 0x04001080 RID: 4224
		Reload,
		// Token: 0x04001081 RID: 4225
		DefaultIsAllDayEventRuleApplied,
		// Token: 0x04001082 RID: 4226
		DefaultRecurrencePatternRuleApplied,
		// Token: 0x04001083 RID: 4227
		DefaultOwnerAppointmentIdRuleApplied,
		// Token: 0x04001084 RID: 4228
		DefaultInvitedForCalendarItemRuleApplied,
		// Token: 0x04001085 RID: 4229
		DefaultInvitedForMeetingMessageRuleApplied,
		// Token: 0x04001086 RID: 4230
		NativeStartTimeToReminderTimeRuleApplied,
		// Token: 0x04001087 RID: 4231
		NativeStartTimeForCalendarRuleApplied,
		// Token: 0x04001088 RID: 4232
		NativeEndTimeForCalendarRuleApplied,
		// Token: 0x04001089 RID: 4233
		NativeStartTimeForMessageRuleApplied,
		// Token: 0x0400108A RID: 4234
		NativeEndTimeForMessageRuleApplied,
		// Token: 0x0400108B RID: 4235
		DefaultCleanGlobalObjectIdFromGlobalObjectIdRuleApplied,
		// Token: 0x0400108C RID: 4236
		DefaultIsExceptionFromItemClassRuleApplied,
		// Token: 0x0400108D RID: 4237
		GlobalObjectIdOnRecurringMasterRuleApplied,
		// Token: 0x0400108E RID: 4238
		ResponseAndReplyRequestedRuleApplied,
		// Token: 0x0400108F RID: 4239
		DefaultAppointmentStateFromItemClassRuleApplied,
		// Token: 0x04001090 RID: 4240
		RecurringTimeZoneRuleApplied,
		// Token: 0x04001091 RID: 4241
		LocationLidWhereRuleApplied,
		// Token: 0x04001092 RID: 4242
		DefaultClientIntentRuleApplied,
		// Token: 0x04001093 RID: 4243
		TruncateSubjectRuleApplied,
		// Token: 0x04001094 RID: 4244
		StartTimeEndTimeToDurationRuleApplied,
		// Token: 0x04001095 RID: 4245
		DefaultReminderNextTimeFromStartTimeAndOffsetRuleApplied,
		// Token: 0x04001096 RID: 4246
		RecurrenceBlobToFlagsRuleApplied,
		// Token: 0x04001097 RID: 4247
		SchedulePlusPropertiesToRecurrenceBlobRuleApplied,
		// Token: 0x04001098 RID: 4248
		SequenceCompositePropertyRuleApplied,
		// Token: 0x04001099 RID: 4249
		NoEmptyTaskRecurrenceBlobRuleApplied,
		// Token: 0x0400109A RID: 4250
		ExceptionalBodyFromBodyRuleApplied,
		// Token: 0x0400109B RID: 4251
		MoveAppointmentTime,
		// Token: 0x0400109C RID: 4252
		SendMeetingUpdate,
		// Token: 0x0400109D RID: 4253
		ExecuteEWSCalendarOperation,
		// Token: 0x0400109E RID: 4254
		Bind,
		// Token: 0x0400109F RID: 4255
		Resurrect,
		// Token: 0x040010A0 RID: 4256
		EnhancedLocationRuleApplied = 82,
		// Token: 0x040010A1 RID: 4257
		EventLocationRuleApplied,
		// Token: 0x040010A2 RID: 4258
		ClipStartTimeForSingleMeetingRuleApplied,
		// Token: 0x040010A3 RID: 4259
		ClipEndTimeForSingleMeetingRuleApplied,
		// Token: 0x040010A4 RID: 4260
		MasterPropertyOverrideProtectionInvoked,
		// Token: 0x040010A5 RID: 4261
		PropertyChangeMetadataTrackingUpdated,
		// Token: 0x040010A6 RID: 4262
		SeriesOperationVirtualPropertyCleanedUp,
		// Token: 0x040010A7 RID: 4263
		CalendarOriginatorIdRuleApplied,
		// Token: 0x040010A8 RID: 4264
		AppointmentsMadeRecurrentRemovedFromSeries = 96,
		// Token: 0x040010A9 RID: 4265
		DefaultOrganizerRuleApplied,
		// Token: 0x040010AA RID: 4266
		CalendarViewPropertiesApplied,
		// Token: 0x040010AB RID: 4267
		HasAttendeesRuleApplied
	}
}
