using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200002D RID: 45
	internal static class CXStrings
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00002B2C File Offset: 0x00000D2C
		static CXStrings()
		{
			CXStrings.stringIDs.Add(3102706246U, "Pop3DisabledResponseMsg");
			CXStrings.stringIDs.Add(89833565U, "ImapSent");
			CXStrings.stringIDs.Add(118956886U, "ImapDeletedItems");
			CXStrings.stringIDs.Add(3142549709U, "ImapTrash");
			CXStrings.stringIDs.Add(3395599741U, "ImapAllMail");
			CXStrings.stringIDs.Add(3870026763U, "ImapJunkEmail");
			CXStrings.stringIDs.Add(3095449466U, "ImapDeletedMessages");
			CXStrings.stringIDs.Add(1506270660U, "Pop3MirroredAccountNotPossibleMsg");
			CXStrings.stringIDs.Add(4069538910U, "ImapMaxBytesReceivedExceeded");
			CXStrings.stringIDs.Add(1496473782U, "ImapNoExistsData");
			CXStrings.stringIDs.Add(3295210032U, "ImapServerShutdown");
			CXStrings.stringIDs.Add(451010195U, "DownloadedLimitExceededError");
			CXStrings.stringIDs.Add(932388783U, "ImapSocketException");
			CXStrings.stringIDs.Add(3366710326U, "ImapServerNetworkError");
			CXStrings.stringIDs.Add(3719135992U, "ImapSpam");
			CXStrings.stringIDs.Add(3818046208U, "Pop3LeaveOnServerNotPossibleMsg");
			CXStrings.stringIDs.Add(3777833512U, "ConnectionAlreadyOpenError");
			CXStrings.stringIDs.Add(625985317U, "Pop3AuthErrorMsg");
			CXStrings.stringIDs.Add(1286062087U, "ImapSecurityStatusError");
			CXStrings.stringIDs.Add(1699298936U, "ConnectionClosedError");
			CXStrings.stringIDs.Add(2300253185U, "ImapSentMessages");
			CXStrings.stringIDs.Add(941137818U, "ImapSelectMailboxFailed");
			CXStrings.stringIDs.Add(2876093320U, "ImapSentMail");
			CXStrings.stringIDs.Add(2828988683U, "ImapServerTimeout");
			CXStrings.stringIDs.Add(2125287154U, "Pop3NonCompliantServerMsg");
			CXStrings.stringIDs.Add(2277965524U, "EasMissingOrBadUrlOnRedirectMsg");
			CXStrings.stringIDs.Add(2645419332U, "Pop3TransientLoginDelayedAuthErrorMsg");
			CXStrings.stringIDs.Add(2621146879U, "ImapSentItems");
			CXStrings.stringIDs.Add(913261964U, "Pop3TransientSystemAuthErrorMsg");
			CXStrings.stringIDs.Add(1010454791U, "ImapJunk");
			CXStrings.stringIDs.Add(4258277560U, "ImapDraft");
			CXStrings.stringIDs.Add(2506606049U, "ImapServerDisconnected");
			CXStrings.stringIDs.Add(726012886U, "ImapServerConnectionClosed");
			CXStrings.stringIDs.Add(2965122554U, "Pop3CapabilitiesNotSupportedMsg");
			CXStrings.stringIDs.Add(169570007U, "ImapDrafts");
			CXStrings.stringIDs.Add(3286312367U, "ImapMaxBytesSentExceeded");
			CXStrings.stringIDs.Add(2004202859U, "TlsRemoteCertificateInvalidError");
			CXStrings.stringIDs.Add(3137388257U, "Pop3TransientInUseAuthErrorMsg");
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00002E60 File Offset: 0x00001060
		public static LocalizedString EasWebExceptionMsg(string msg)
		{
			return new LocalizedString("EasWebExceptionMsg", CXStrings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00002E88 File Offset: 0x00001088
		public static LocalizedString ImapInvalidResponseErrorMsg(string failureReason)
		{
			return new LocalizedString("ImapInvalidResponseErrorMsg", CXStrings.ResourceManager, new object[]
			{
				failureReason
			});
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002EB0 File Offset: 0x000010B0
		public static LocalizedString Pop3DisabledResponseMsg
		{
			get
			{
				return new LocalizedString("Pop3DisabledResponseMsg", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00002EC7 File Offset: 0x000010C7
		public static LocalizedString ImapSent
		{
			get
			{
				return new LocalizedString("ImapSent", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00002EDE File Offset: 0x000010DE
		public static LocalizedString ImapDeletedItems
		{
			get
			{
				return new LocalizedString("ImapDeletedItems", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00002EF5 File Offset: 0x000010F5
		public static LocalizedString ImapTrash
		{
			get
			{
				return new LocalizedString("ImapTrash", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00002F0C File Offset: 0x0000110C
		public static LocalizedString ImapAllMail
		{
			get
			{
				return new LocalizedString("ImapAllMail", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00002F23 File Offset: 0x00001123
		public static LocalizedString ImapJunkEmail
		{
			get
			{
				return new LocalizedString("ImapJunkEmail", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00002F3A File Offset: 0x0000113A
		public static LocalizedString ImapDeletedMessages
		{
			get
			{
				return new LocalizedString("ImapDeletedMessages", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00002F51 File Offset: 0x00001151
		public static LocalizedString Pop3MirroredAccountNotPossibleMsg
		{
			get
			{
				return new LocalizedString("Pop3MirroredAccountNotPossibleMsg", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00002F68 File Offset: 0x00001168
		public static LocalizedString ImapMaxBytesReceivedExceeded
		{
			get
			{
				return new LocalizedString("ImapMaxBytesReceivedExceeded", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00002F80 File Offset: 0x00001180
		public static LocalizedString ItemLevelPermanentExceptionMsg(string msg)
		{
			return new LocalizedString("ItemLevelPermanentExceptionMsg", CXStrings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00002FA8 File Offset: 0x000011A8
		public static LocalizedString ImapNoExistsData
		{
			get
			{
				return new LocalizedString("ImapNoExistsData", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00002FBF File Offset: 0x000011BF
		public static LocalizedString ImapServerShutdown
		{
			get
			{
				return new LocalizedString("ImapServerShutdown", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00002FD8 File Offset: 0x000011D8
		public static LocalizedString Pop3ErrorResponseMsg(string command, string response)
		{
			return new LocalizedString("Pop3ErrorResponseMsg", CXStrings.ResourceManager, new object[]
			{
				command,
				response
			});
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003004 File Offset: 0x00001204
		public static LocalizedString DownloadedLimitExceededError
		{
			get
			{
				return new LocalizedString("DownloadedLimitExceededError", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000301C File Offset: 0x0000121C
		public static LocalizedString EasCommandFailed(string responseStatus, string httpStatus)
		{
			return new LocalizedString("EasCommandFailed", CXStrings.ResourceManager, new object[]
			{
				responseStatus,
				httpStatus
			});
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003048 File Offset: 0x00001248
		public static LocalizedString EasWBXmlPermanentExceptionMsg(string msg)
		{
			return new LocalizedString("EasWBXmlPermanentExceptionMsg", CXStrings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003070 File Offset: 0x00001270
		public static LocalizedString UnexpectedCapabilitiesError(string unexpectedCapabilitiesMsg)
		{
			return new LocalizedString("UnexpectedCapabilitiesError", CXStrings.ResourceManager, new object[]
			{
				unexpectedCapabilitiesMsg
			});
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003098 File Offset: 0x00001298
		public static LocalizedString ImapSocketException
		{
			get
			{
				return new LocalizedString("ImapSocketException", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000030AF File Offset: 0x000012AF
		public static LocalizedString ImapServerNetworkError
		{
			get
			{
				return new LocalizedString("ImapServerNetworkError", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000030C8 File Offset: 0x000012C8
		public static LocalizedString TlsError(string msg)
		{
			return new LocalizedString("TlsError", CXStrings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000030F0 File Offset: 0x000012F0
		public static LocalizedString ImapCommunicationErrorMsg(string imapCommunicationErrorMsg, RetryPolicy retryPolicy)
		{
			return new LocalizedString("ImapCommunicationErrorMsg", CXStrings.ResourceManager, new object[]
			{
				imapCommunicationErrorMsg,
				retryPolicy
			});
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003121 File Offset: 0x00001321
		public static LocalizedString ImapSpam
		{
			get
			{
				return new LocalizedString("ImapSpam", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003138 File Offset: 0x00001338
		public static LocalizedString ImapConnectionErrorMsg(string imapConnectionErrorMsg, RetryPolicy retryPolicy)
		{
			return new LocalizedString("ImapConnectionErrorMsg", CXStrings.ResourceManager, new object[]
			{
				imapConnectionErrorMsg,
				retryPolicy
			});
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003169 File Offset: 0x00001369
		public static LocalizedString Pop3LeaveOnServerNotPossibleMsg
		{
			get
			{
				return new LocalizedString("Pop3LeaveOnServerNotPossibleMsg", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00003180 File Offset: 0x00001380
		public static LocalizedString ConnectionAlreadyOpenError
		{
			get
			{
				return new LocalizedString("ConnectionAlreadyOpenError", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003198 File Offset: 0x00001398
		public static LocalizedString ImapUnsupportedAuthenticationErrorMsg(string authErrorMsg, string authMechanismName, RetryPolicy retryPolicy)
		{
			return new LocalizedString("ImapUnsupportedAuthenticationErrorMsg", CXStrings.ResourceManager, new object[]
			{
				authErrorMsg,
				authMechanismName,
				retryPolicy
			});
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000031D0 File Offset: 0x000013D0
		public static LocalizedString EasUnexpectedHttpStatusMsg(string msg)
		{
			return new LocalizedString("EasUnexpectedHttpStatusMsg", CXStrings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000031F8 File Offset: 0x000013F8
		public static LocalizedString EasRetryAfterExceptionMsg(TimeSpan delay, string msg)
		{
			return new LocalizedString("EasRetryAfterExceptionMsg", CXStrings.ResourceManager, new object[]
			{
				delay,
				msg
			});
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003229 File Offset: 0x00001429
		public static LocalizedString Pop3AuthErrorMsg
		{
			get
			{
				return new LocalizedString("Pop3AuthErrorMsg", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003240 File Offset: 0x00001440
		public static LocalizedString ImapSecurityStatusError
		{
			get
			{
				return new LocalizedString("ImapSecurityStatusError", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003258 File Offset: 0x00001458
		public static LocalizedString OperationLevelPermanentExceptionMsg(string msg)
		{
			return new LocalizedString("OperationLevelPermanentExceptionMsg", CXStrings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003280 File Offset: 0x00001480
		public static LocalizedString ConnectionClosedError
		{
			get
			{
				return new LocalizedString("ConnectionClosedError", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003297 File Offset: 0x00001497
		public static LocalizedString ImapSentMessages
		{
			get
			{
				return new LocalizedString("ImapSentMessages", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000032B0 File Offset: 0x000014B0
		public static LocalizedString ImapErrorMsg(string failureReason)
		{
			return new LocalizedString("ImapErrorMsg", CXStrings.ResourceManager, new object[]
			{
				failureReason
			});
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000032D8 File Offset: 0x000014D8
		public static LocalizedString ImapSelectMailboxFailed
		{
			get
			{
				return new LocalizedString("ImapSelectMailboxFailed", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000032F0 File Offset: 0x000014F0
		public static LocalizedString ItemLevelTransientExceptionMsg(string msg)
		{
			return new LocalizedString("ItemLevelTransientExceptionMsg", CXStrings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003318 File Offset: 0x00001518
		public static LocalizedString ImapSentMail
		{
			get
			{
				return new LocalizedString("ImapSentMail", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003330 File Offset: 0x00001530
		public static LocalizedString NonPromotableTransientExceptionMsg(string msg)
		{
			return new LocalizedString("NonPromotableTransientExceptionMsg", CXStrings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003358 File Offset: 0x00001558
		public static LocalizedString ItemLimitExceededExceptionMsg(string limitExceededMsg)
		{
			return new LocalizedString("ItemLimitExceededExceptionMsg", CXStrings.ResourceManager, new object[]
			{
				limitExceededMsg
			});
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003380 File Offset: 0x00001580
		public static LocalizedString ImapServerTimeout
		{
			get
			{
				return new LocalizedString("ImapServerTimeout", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003398 File Offset: 0x00001598
		public static LocalizedString TlsFailureOccurredError(string securityStatus)
		{
			return new LocalizedString("TlsFailureOccurredError", CXStrings.ResourceManager, new object[]
			{
				securityStatus
			});
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000033C0 File Offset: 0x000015C0
		public static LocalizedString EasRequiresFolderSyncExceptionMsg(string msg)
		{
			return new LocalizedString("EasRequiresFolderSyncExceptionMsg", CXStrings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000033E8 File Offset: 0x000015E8
		public static LocalizedString MessageSizeLimitExceededError(string limitExceededMsg)
		{
			return new LocalizedString("MessageSizeLimitExceededError", CXStrings.ResourceManager, new object[]
			{
				limitExceededMsg
			});
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003410 File Offset: 0x00001610
		public static LocalizedString EasRequiresSyncKeyResetExceptionMsg(string msg)
		{
			return new LocalizedString("EasRequiresSyncKeyResetExceptionMsg", CXStrings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003438 File Offset: 0x00001638
		public static LocalizedString Pop3NonCompliantServerMsg
		{
			get
			{
				return new LocalizedString("Pop3NonCompliantServerMsg", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000344F File Offset: 0x0000164F
		public static LocalizedString EasMissingOrBadUrlOnRedirectMsg
		{
			get
			{
				return new LocalizedString("EasMissingOrBadUrlOnRedirectMsg", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003466 File Offset: 0x00001666
		public static LocalizedString Pop3TransientLoginDelayedAuthErrorMsg
		{
			get
			{
				return new LocalizedString("Pop3TransientLoginDelayedAuthErrorMsg", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003480 File Offset: 0x00001680
		public static LocalizedString ImapAuthenticationErrorMsg(string imapAuthenticationErrorMsg, string authMechanismName, RetryPolicy retryPolicy)
		{
			return new LocalizedString("ImapAuthenticationErrorMsg", CXStrings.ResourceManager, new object[]
			{
				imapAuthenticationErrorMsg,
				authMechanismName,
				retryPolicy
			});
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000034B8 File Offset: 0x000016B8
		public static LocalizedString MissingCapabilitiesError(string missingCapabilitiesMsg)
		{
			return new LocalizedString("MissingCapabilitiesError", CXStrings.ResourceManager, new object[]
			{
				missingCapabilitiesMsg
			});
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000034E0 File Offset: 0x000016E0
		public static LocalizedString ImapSentItems
		{
			get
			{
				return new LocalizedString("ImapSentItems", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000034F7 File Offset: 0x000016F7
		public static LocalizedString Pop3TransientSystemAuthErrorMsg
		{
			get
			{
				return new LocalizedString("Pop3TransientSystemAuthErrorMsg", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003510 File Offset: 0x00001710
		public static LocalizedString UnhandledError(string typeName)
		{
			return new LocalizedString("UnhandledError", CXStrings.ResourceManager, new object[]
			{
				typeName
			});
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003538 File Offset: 0x00001738
		public static LocalizedString ImapBadResponseErrorMsg(string failureReason)
		{
			return new LocalizedString("ImapBadResponseErrorMsg", CXStrings.ResourceManager, new object[]
			{
				failureReason
			});
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003560 File Offset: 0x00001760
		public static LocalizedString Pop3PermErrorResponseMsg(string command, string response)
		{
			return new LocalizedString("Pop3PermErrorResponseMsg", CXStrings.ResourceManager, new object[]
			{
				command,
				response
			});
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000358C File Offset: 0x0000178C
		public static LocalizedString ImapJunk
		{
			get
			{
				return new LocalizedString("ImapJunk", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000035A3 File Offset: 0x000017A3
		public static LocalizedString ImapDraft
		{
			get
			{
				return new LocalizedString("ImapDraft", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000035BA File Offset: 0x000017BA
		public static LocalizedString ImapServerDisconnected
		{
			get
			{
				return new LocalizedString("ImapServerDisconnected", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000035D1 File Offset: 0x000017D1
		public static LocalizedString ImapServerConnectionClosed
		{
			get
			{
				return new LocalizedString("ImapServerConnectionClosed", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000035E8 File Offset: 0x000017E8
		public static LocalizedString Pop3CapabilitiesNotSupportedMsg
		{
			get
			{
				return new LocalizedString("Pop3CapabilitiesNotSupportedMsg", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003600 File Offset: 0x00001800
		public static LocalizedString OperationLevelTransientExceptionMsg(string msg)
		{
			return new LocalizedString("OperationLevelTransientExceptionMsg", CXStrings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00003628 File Offset: 0x00001828
		public static LocalizedString ImapDrafts
		{
			get
			{
				return new LocalizedString("ImapDrafts", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x0000363F File Offset: 0x0000183F
		public static LocalizedString ImapMaxBytesSentExceeded
		{
			get
			{
				return new LocalizedString("ImapMaxBytesSentExceeded", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003658 File Offset: 0x00001858
		public static LocalizedString EasWBXmlExceptionMsg(string msg)
		{
			return new LocalizedString("EasWBXmlExceptionMsg", CXStrings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003680 File Offset: 0x00001880
		public static LocalizedString TlsRemoteCertificateInvalidError
		{
			get
			{
				return new LocalizedString("TlsRemoteCertificateInvalidError", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003698 File Offset: 0x00001898
		public static LocalizedString ImapConnectionClosedErrorMsg(string imapConnectionClosedErrMsg)
		{
			return new LocalizedString("ImapConnectionClosedErrorMsg", CXStrings.ResourceManager, new object[]
			{
				imapConnectionClosedErrMsg
			});
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000036C0 File Offset: 0x000018C0
		public static LocalizedString Pop3TransientInUseAuthErrorMsg
		{
			get
			{
				return new LocalizedString("Pop3TransientInUseAuthErrorMsg", CXStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000036D8 File Offset: 0x000018D8
		public static LocalizedString Pop3BrokenResponseMsg(string command, string response)
		{
			return new LocalizedString("Pop3BrokenResponseMsg", CXStrings.ResourceManager, new object[]
			{
				command,
				response
			});
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003704 File Offset: 0x00001904
		public static LocalizedString Pop3UnknownResponseMsg(string command, string response)
		{
			return new LocalizedString("Pop3UnknownResponseMsg", CXStrings.ResourceManager, new object[]
			{
				command,
				response
			});
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003730 File Offset: 0x00001930
		public static LocalizedString GetLocalizedString(CXStrings.IDs key)
		{
			return new LocalizedString(CXStrings.stringIDs[(uint)key], CXStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000084 RID: 132
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(38);

		// Token: 0x04000085 RID: 133
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Connections.Common.CXStrings", typeof(CXStrings).GetTypeInfo().Assembly);

		// Token: 0x0200002E RID: 46
		public enum IDs : uint
		{
			// Token: 0x04000087 RID: 135
			Pop3DisabledResponseMsg = 3102706246U,
			// Token: 0x04000088 RID: 136
			ImapSent = 89833565U,
			// Token: 0x04000089 RID: 137
			ImapDeletedItems = 118956886U,
			// Token: 0x0400008A RID: 138
			ImapTrash = 3142549709U,
			// Token: 0x0400008B RID: 139
			ImapAllMail = 3395599741U,
			// Token: 0x0400008C RID: 140
			ImapJunkEmail = 3870026763U,
			// Token: 0x0400008D RID: 141
			ImapDeletedMessages = 3095449466U,
			// Token: 0x0400008E RID: 142
			Pop3MirroredAccountNotPossibleMsg = 1506270660U,
			// Token: 0x0400008F RID: 143
			ImapMaxBytesReceivedExceeded = 4069538910U,
			// Token: 0x04000090 RID: 144
			ImapNoExistsData = 1496473782U,
			// Token: 0x04000091 RID: 145
			ImapServerShutdown = 3295210032U,
			// Token: 0x04000092 RID: 146
			DownloadedLimitExceededError = 451010195U,
			// Token: 0x04000093 RID: 147
			ImapSocketException = 932388783U,
			// Token: 0x04000094 RID: 148
			ImapServerNetworkError = 3366710326U,
			// Token: 0x04000095 RID: 149
			ImapSpam = 3719135992U,
			// Token: 0x04000096 RID: 150
			Pop3LeaveOnServerNotPossibleMsg = 3818046208U,
			// Token: 0x04000097 RID: 151
			ConnectionAlreadyOpenError = 3777833512U,
			// Token: 0x04000098 RID: 152
			Pop3AuthErrorMsg = 625985317U,
			// Token: 0x04000099 RID: 153
			ImapSecurityStatusError = 1286062087U,
			// Token: 0x0400009A RID: 154
			ConnectionClosedError = 1699298936U,
			// Token: 0x0400009B RID: 155
			ImapSentMessages = 2300253185U,
			// Token: 0x0400009C RID: 156
			ImapSelectMailboxFailed = 941137818U,
			// Token: 0x0400009D RID: 157
			ImapSentMail = 2876093320U,
			// Token: 0x0400009E RID: 158
			ImapServerTimeout = 2828988683U,
			// Token: 0x0400009F RID: 159
			Pop3NonCompliantServerMsg = 2125287154U,
			// Token: 0x040000A0 RID: 160
			EasMissingOrBadUrlOnRedirectMsg = 2277965524U,
			// Token: 0x040000A1 RID: 161
			Pop3TransientLoginDelayedAuthErrorMsg = 2645419332U,
			// Token: 0x040000A2 RID: 162
			ImapSentItems = 2621146879U,
			// Token: 0x040000A3 RID: 163
			Pop3TransientSystemAuthErrorMsg = 913261964U,
			// Token: 0x040000A4 RID: 164
			ImapJunk = 1010454791U,
			// Token: 0x040000A5 RID: 165
			ImapDraft = 4258277560U,
			// Token: 0x040000A6 RID: 166
			ImapServerDisconnected = 2506606049U,
			// Token: 0x040000A7 RID: 167
			ImapServerConnectionClosed = 726012886U,
			// Token: 0x040000A8 RID: 168
			Pop3CapabilitiesNotSupportedMsg = 2965122554U,
			// Token: 0x040000A9 RID: 169
			ImapDrafts = 169570007U,
			// Token: 0x040000AA RID: 170
			ImapMaxBytesSentExceeded = 3286312367U,
			// Token: 0x040000AB RID: 171
			TlsRemoteCertificateInvalidError = 2004202859U,
			// Token: 0x040000AC RID: 172
			Pop3TransientInUseAuthErrorMsg = 3137388257U
		}

		// Token: 0x0200002F RID: 47
		private enum ParamIDs
		{
			// Token: 0x040000AE RID: 174
			EasWebExceptionMsg,
			// Token: 0x040000AF RID: 175
			ImapInvalidResponseErrorMsg,
			// Token: 0x040000B0 RID: 176
			ItemLevelPermanentExceptionMsg,
			// Token: 0x040000B1 RID: 177
			Pop3ErrorResponseMsg,
			// Token: 0x040000B2 RID: 178
			EasCommandFailed,
			// Token: 0x040000B3 RID: 179
			EasWBXmlPermanentExceptionMsg,
			// Token: 0x040000B4 RID: 180
			UnexpectedCapabilitiesError,
			// Token: 0x040000B5 RID: 181
			TlsError,
			// Token: 0x040000B6 RID: 182
			ImapCommunicationErrorMsg,
			// Token: 0x040000B7 RID: 183
			ImapConnectionErrorMsg,
			// Token: 0x040000B8 RID: 184
			ImapUnsupportedAuthenticationErrorMsg,
			// Token: 0x040000B9 RID: 185
			EasUnexpectedHttpStatusMsg,
			// Token: 0x040000BA RID: 186
			EasRetryAfterExceptionMsg,
			// Token: 0x040000BB RID: 187
			OperationLevelPermanentExceptionMsg,
			// Token: 0x040000BC RID: 188
			ImapErrorMsg,
			// Token: 0x040000BD RID: 189
			ItemLevelTransientExceptionMsg,
			// Token: 0x040000BE RID: 190
			NonPromotableTransientExceptionMsg,
			// Token: 0x040000BF RID: 191
			ItemLimitExceededExceptionMsg,
			// Token: 0x040000C0 RID: 192
			TlsFailureOccurredError,
			// Token: 0x040000C1 RID: 193
			EasRequiresFolderSyncExceptionMsg,
			// Token: 0x040000C2 RID: 194
			MessageSizeLimitExceededError,
			// Token: 0x040000C3 RID: 195
			EasRequiresSyncKeyResetExceptionMsg,
			// Token: 0x040000C4 RID: 196
			ImapAuthenticationErrorMsg,
			// Token: 0x040000C5 RID: 197
			MissingCapabilitiesError,
			// Token: 0x040000C6 RID: 198
			UnhandledError,
			// Token: 0x040000C7 RID: 199
			ImapBadResponseErrorMsg,
			// Token: 0x040000C8 RID: 200
			Pop3PermErrorResponseMsg,
			// Token: 0x040000C9 RID: 201
			OperationLevelTransientExceptionMsg,
			// Token: 0x040000CA RID: 202
			EasWBXmlExceptionMsg,
			// Token: 0x040000CB RID: 203
			ImapConnectionClosedErrorMsg,
			// Token: 0x040000CC RID: 204
			Pop3BrokenResponseMsg,
			// Token: 0x040000CD RID: 205
			Pop3UnknownResponseMsg
		}
	}
}
