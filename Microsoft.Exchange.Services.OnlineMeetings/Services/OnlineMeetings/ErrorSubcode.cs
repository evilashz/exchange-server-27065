using System;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x0200000F RID: 15
	internal enum ErrorSubcode
	{
		// Token: 0x0400004B RID: 75
		None,
		// Token: 0x0400004C RID: 76
		ServiceFailure,
		// Token: 0x0400004D RID: 77
		ServiceUnavailable,
		// Token: 0x0400004E RID: 78
		Timeout,
		// Token: 0x0400004F RID: 79
		RequestCancelled,
		// Token: 0x04000050 RID: 80
		Forbidden,
		// Token: 0x04000051 RID: 81
		NotFound,
		// Token: 0x04000052 RID: 82
		MethodNotAllowed,
		// Token: 0x04000053 RID: 83
		ClientTimeout,
		// Token: 0x04000054 RID: 84
		Conflict,
		// Token: 0x04000055 RID: 85
		Gone,
		// Token: 0x04000056 RID: 86
		PreconditionFailed,
		// Token: 0x04000057 RID: 87
		PreconditionRequired,
		// Token: 0x04000058 RID: 88
		EntityTooLarge,
		// Token: 0x04000059 RID: 89
		TooManyRequests,
		// Token: 0x0400005A RID: 90
		RequestTooLarge,
		// Token: 0x0400005B RID: 91
		UnsupportedMediaType,
		// Token: 0x0400005C RID: 92
		LocalFailure,
		// Token: 0x0400005D RID: 93
		RemoteFailure,
		// Token: 0x0400005E RID: 94
		ResourceTerminating,
		// Token: 0x0400005F RID: 95
		ResourceExists,
		// Token: 0x04000060 RID: 96
		InvalidRequest,
		// Token: 0x04000061 RID: 97
		UnhandledException,
		// Token: 0x04000062 RID: 98
		Unknown,
		// Token: 0x04000063 RID: 99
		AnonymousNotAllowed,
		// Token: 0x04000064 RID: 100
		InviteesOnly,
		// Token: 0x04000065 RID: 101
		SignInForCommunicationRequired,
		// Token: 0x04000066 RID: 102
		NotEnterpriseVoiceEnabled,
		// Token: 0x04000067 RID: 103
		UserLookupFailed,
		// Token: 0x04000068 RID: 104
		AuthenticatedJoinNotSupported,
		// Token: 0x04000069 RID: 105
		AlreadyExists,
		// Token: 0x0400006A RID: 106
		ApplicationNotFound = 1001,
		// Token: 0x0400006B RID: 107
		ApplicationTerminating,
		// Token: 0x0400006C RID: 108
		NoteSizeTooBig = 2001,
		// Token: 0x0400006D RID: 109
		UnknownPhoneType,
		// Token: 0x0400006E RID: 110
		ReadOnlyPhoneType,
		// Token: 0x0400006F RID: 111
		DuplicatePhoneType,
		// Token: 0x04000070 RID: 112
		InvalidTeamDelegateRingTime,
		// Token: 0x04000071 RID: 113
		DelegateRingDisabled,
		// Token: 0x04000072 RID: 114
		ForwardImmediateCustomDisabled,
		// Token: 0x04000073 RID: 115
		SimulRingCustomDisabled,
		// Token: 0x04000074 RID: 116
		TeamRingDisabled,
		// Token: 0x04000075 RID: 117
		VoicemailNotEnabled,
		// Token: 0x04000076 RID: 118
		NoDelegatesConfigured,
		// Token: 0x04000077 RID: 119
		NoTeamMembersConfigured,
		// Token: 0x04000078 RID: 120
		InvalidUnansweredRingTime,
		// Token: 0x04000079 RID: 121
		UnknownAvailability,
		// Token: 0x0400007A RID: 122
		PhotoDisabled,
		// Token: 0x0400007B RID: 123
		UnauthorizedPhotoDisplay,
		// Token: 0x0400007C RID: 124
		ResultExceeededLimits = 3001,
		// Token: 0x0400007D RID: 125
		MembershipChangesNotSupported,
		// Token: 0x0400007E RID: 126
		TooManyOnlineMeetings,
		// Token: 0x0400007F RID: 127
		ThreadIdAlreadyExists,
		// Token: 0x04000080 RID: 128
		DoNotDisturb,
		// Token: 0x04000081 RID: 129
		ConnectedElsewhere,
		// Token: 0x04000082 RID: 130
		SessionNotFound,
		// Token: 0x04000083 RID: 131
		SessionContextNotChangable,
		// Token: 0x04000084 RID: 132
		CallNotAnswered,
		// Token: 0x04000085 RID: 133
		FederationRequired,
		// Token: 0x04000086 RID: 134
		Canceled,
		// Token: 0x04000087 RID: 135
		Declined,
		// Token: 0x04000088 RID: 136
		CallNotAcceptable,
		// Token: 0x04000089 RID: 137
		Transferred,
		// Token: 0x0400008A RID: 138
		CallReplaced,
		// Token: 0x0400008B RID: 139
		EscalationFailed,
		// Token: 0x0400008C RID: 140
		InvalidSDP,
		// Token: 0x0400008D RID: 141
		OfferAnswerFailure,
		// Token: 0x0400008E RID: 142
		AudioUnavailable,
		// Token: 0x0400008F RID: 143
		UserNotEnabledForOutsideVoice,
		// Token: 0x04000090 RID: 144
		InsufficientBandwidth,
		// Token: 0x04000091 RID: 145
		RepliedWithOtherModality,
		// Token: 0x04000092 RID: 146
		DestinationNotFound,
		// Token: 0x04000093 RID: 147
		DialoutNotAllowed,
		// Token: 0x04000094 RID: 148
		Unreachable,
		// Token: 0x04000095 RID: 149
		MediaEncryptionNotSupported,
		// Token: 0x04000096 RID: 150
		MediaEncryptionRequired,
		// Token: 0x04000097 RID: 151
		Unavailable,
		// Token: 0x04000098 RID: 152
		TooManyParticipants,
		// Token: 0x04000099 RID: 153
		TooManyLobbyParticipants,
		// Token: 0x0400009A RID: 154
		Busy,
		// Token: 0x0400009B RID: 155
		AttendeeNotAllowed,
		// Token: 0x0400009C RID: 156
		Demoted,
		// Token: 0x0400009D RID: 157
		MediaFailure,
		// Token: 0x0400009E RID: 158
		Removed,
		// Token: 0x0400009F RID: 159
		TemporarilyUnavailable,
		// Token: 0x040000A0 RID: 160
		ModalityNotSupported,
		// Token: 0x040000A1 RID: 161
		NotAllowed,
		// Token: 0x040000A2 RID: 162
		Ejected,
		// Token: 0x040000A3 RID: 163
		Denied,
		// Token: 0x040000A4 RID: 164
		Ended,
		// Token: 0x040000A5 RID: 165
		ConversationTerminatedNoConnectedModality,
		// Token: 0x040000A6 RID: 166
		ParameterValidationFailure,
		// Token: 0x040000A7 RID: 167
		UserNotEnabledForPushNotifications,
		// Token: 0x040000A8 RID: 168
		PushNotificationSubscriptionFailure,
		// Token: 0x040000A9 RID: 169
		PushNotificationSubscriptionAlreadyExists,
		// Token: 0x040000AA RID: 170
		ConferenceRoleChanged,
		// Token: 0x040000AB RID: 171
		MissingAnonymousDisplayName,
		// Token: 0x040000AC RID: 172
		SessionSwitched,
		// Token: 0x040000AD RID: 173
		EventChannelOutOfSync = 6001
	}
}
