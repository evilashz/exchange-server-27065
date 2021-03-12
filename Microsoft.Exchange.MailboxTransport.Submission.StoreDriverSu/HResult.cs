using System;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000035 RID: 53
	internal static class HResult
	{
		// Token: 0x06000211 RID: 529 RVA: 0x0000C56F File Offset: 0x0000A76F
		public static bool IsHandled(uint errorCode)
		{
			return (errorCode & 3221225472U) == 0U;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000C57B File Offset: 0x0000A77B
		public static bool IsRetryableAtCurrentHub(uint errorCode)
		{
			return (errorCode & 3221225472U) == 1073741824U;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000C58B File Offset: 0x0000A78B
		public static bool IsRetryableAtOtherHub(uint errorCode)
		{
			return (errorCode & 3221225472U) == 2147483648U;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000C59B File Offset: 0x0000A79B
		public static bool IsUnexpectedError(uint errorCode)
		{
			return (errorCode & 3221225472U) == 3221225472U;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000C5AB File Offset: 0x0000A7AB
		public static bool IsRetryMailbox(uint errorCode)
		{
			return (errorCode & 117440512U) == 16777216U;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000C5BB File Offset: 0x0000A7BB
		public static bool IsRetryMailboxDatabase(uint errorCode)
		{
			return (errorCode & 117440512U) == 33554432U;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000C5CB File Offset: 0x0000A7CB
		public static bool IsRetryMailboxServer(uint errorCode)
		{
			return (errorCode & 117440512U) == 67108864U;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000C5DB File Offset: 0x0000A7DB
		public static bool IsNonActionable(uint errorCode)
		{
			return errorCode == 6U || errorCode == 16U || errorCode == 8U || errorCode == 9U;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000C5F1 File Offset: 0x0000A7F1
		public static bool IsMessageSubmittedOrHasNoRcpts(uint errorCode)
		{
			return errorCode == 0U || errorCode == 1U || errorCode == 15U;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000C604 File Offset: 0x0000A804
		public static string GetStringForErrorCode(uint errorCode)
		{
			uint num = errorCode;
			if (num <= 1107296262U)
			{
				switch (num)
				{
				case 0U:
					return "Success";
				case 1U:
					return "NDRGenerated";
				case 2U:
					return "NoMessageForNDR";
				case 3U:
					return "PoisonMessage";
				case 4U:
				case 13U:
				case 14U:
					break;
				case 5U:
					return "PermanentNDRGenerationFailure";
				case 6U:
					return "NoMessageItem";
				case 7U:
					return "InvalidSender";
				case 8U:
					return "VirusMessage";
				case 9U:
					return "EventWithoutSubmitFlag";
				case 10U:
					return "InvalidQuotaWarningMessage";
				case 11U:
					return "MessageThrottled";
				case 12U:
					return "ResubmissionAbortedDueToContentChange";
				case 15U:
					return "NoRecipients";
				case 16U:
					return "Skip";
				case 17U:
					return "InvalidSenderNullProxy";
				case 18U:
					return "InvalidSenderInvalidProxy";
				case 19U:
					return "InvalidSenderLookupFailed";
				case 20U:
					return "InvalidSenderCouldNotResolveRecipient";
				case 21U:
					return "InvalidSenderCouldNotResolveUser";
				case 22U:
					return "PoisonMessageHandledAndPoisonNdrSuccess";
				case 23U:
					return "PoisonMessageHandledButPoisonNdrPermanentFailure";
				case 24U:
					return "PoisonMessageHandledButPoisonNdrRetry";
				default:
					switch (num)
					{
					case 1090519040U:
						return "RetryMailboxError";
					case 1090519041U:
						return "TemporaryNDRGenerationFailureMailbox";
					case 1090519042U:
						return "TooManySubmissions";
					case 1090519043U:
						return "RetryableRpcError";
					default:
						switch (num)
						{
						case 1107296260U:
							return "RetryMailboxDatabaseError";
						case 1107296262U:
							return "TemporaryNDRGenerationFailureMailboxDatabase";
						}
						break;
					}
					break;
				}
			}
			else if (num <= 2214592517U)
			{
				switch (num)
				{
				case 1140850693U:
					return "RetryMailboxServerError";
				case 1140850694U:
					break;
				case 1140850695U:
					return "TemporaryNDRGenerationFailureMailboxServer";
				case 1140850696U:
					return "ResourceQuarantined";
				default:
					switch (num)
					{
					case 2214592513U:
						return "SubmissionPaused";
					case 2214592514U:
						return "ServerRetired";
					case 2214592515U:
						return "ServerNotAvailable";
					case 2214592516U:
						return "RetryOtherHubError";
					case 2214592517U:
						return "TemporaryNDRGenerationFailureOtherHub";
					}
					break;
				}
			}
			else
			{
				if (num == 2684354560U)
				{
					return "RetrySmtp";
				}
				if (num == 3221225472U)
				{
					return "UnexpectedError";
				}
			}
			return errorCode.ToString("X");
		}

		// Token: 0x040000F2 RID: 242
		public const uint EventHandledBit = 0U;

		// Token: 0x040000F3 RID: 243
		public const uint RetryCurrentHubBit = 1073741824U;

		// Token: 0x040000F4 RID: 244
		public const uint RetryOtherHubBit = 2147483648U;

		// Token: 0x040000F5 RID: 245
		public const uint RetrySmtp = 2684354560U;

		// Token: 0x040000F6 RID: 246
		public const uint UnexpectedErrorBit = 3221225472U;

		// Token: 0x040000F7 RID: 247
		public const uint CategoryBit = 3221225472U;

		// Token: 0x040000F8 RID: 248
		public const uint RetryMailboxBit = 16777216U;

		// Token: 0x040000F9 RID: 249
		public const uint RetryMailboxDatabaseBit = 33554432U;

		// Token: 0x040000FA RID: 250
		public const uint RetryMailboxServerBit = 67108864U;

		// Token: 0x040000FB RID: 251
		public const uint ScopeBit = 117440512U;

		// Token: 0x040000FC RID: 252
		public const uint Success = 0U;

		// Token: 0x040000FD RID: 253
		public const uint NDRGenerated = 1U;

		// Token: 0x040000FE RID: 254
		public const uint NoMessageForNDR = 2U;

		// Token: 0x040000FF RID: 255
		public const uint PoisonMessage = 3U;

		// Token: 0x04000100 RID: 256
		public const uint PermanentNDRGenerationFailure = 5U;

		// Token: 0x04000101 RID: 257
		public const uint NoMessageItem = 6U;

		// Token: 0x04000102 RID: 258
		public const uint InvalidSender = 7U;

		// Token: 0x04000103 RID: 259
		public const uint VirusMessage = 8U;

		// Token: 0x04000104 RID: 260
		public const uint EventWithoutSubmitFlag = 9U;

		// Token: 0x04000105 RID: 261
		public const uint InvalidQuotaWarningMessage = 10U;

		// Token: 0x04000106 RID: 262
		public const uint MessageThrottled = 11U;

		// Token: 0x04000107 RID: 263
		public const uint ResubmissionAbortedDueToContentChange = 12U;

		// Token: 0x04000108 RID: 264
		public const uint NoRecipients = 15U;

		// Token: 0x04000109 RID: 265
		public const uint Skip = 16U;

		// Token: 0x0400010A RID: 266
		public const uint InvalidSenderNullProxy = 17U;

		// Token: 0x0400010B RID: 267
		public const uint InvalidSenderInvalidProxy = 18U;

		// Token: 0x0400010C RID: 268
		public const uint InvalidSenderLookupFailed = 19U;

		// Token: 0x0400010D RID: 269
		public const uint InvalidSenderCouldNotResolveRecipient = 20U;

		// Token: 0x0400010E RID: 270
		public const uint InvalidSenderCouldNotResolveUser = 21U;

		// Token: 0x0400010F RID: 271
		public const uint PoisonMessageHandledAndPoisonNdrSuccess = 22U;

		// Token: 0x04000110 RID: 272
		public const uint PoisonMessageHandledButPoisonNdrPermanentFailure = 23U;

		// Token: 0x04000111 RID: 273
		public const uint PoisonMessageHandledButPoisonNdrRetry = 24U;

		// Token: 0x04000112 RID: 274
		public const uint RetryMailboxError = 1090519040U;

		// Token: 0x04000113 RID: 275
		public const uint TemporaryNDRGenerationFailureMailbox = 1090519041U;

		// Token: 0x04000114 RID: 276
		public const uint TooManySubmissions = 1090519042U;

		// Token: 0x04000115 RID: 277
		public const uint RetryableRpcError = 1090519043U;

		// Token: 0x04000116 RID: 278
		public const uint RetryMailboxDatabaseError = 1107296260U;

		// Token: 0x04000117 RID: 279
		public const uint RetryMailboxServerError = 1140850693U;

		// Token: 0x04000118 RID: 280
		public const uint TemporaryNDRGenerationFailureMailboxDatabase = 1107296262U;

		// Token: 0x04000119 RID: 281
		public const uint TemporaryNDRGenerationFailureMailboxServer = 1140850695U;

		// Token: 0x0400011A RID: 282
		public const uint ResourceQuarantined = 1140850696U;

		// Token: 0x0400011B RID: 283
		public const uint SubmissionPaused = 2214592513U;

		// Token: 0x0400011C RID: 284
		public const uint ServerRetired = 2214592514U;

		// Token: 0x0400011D RID: 285
		public const uint ServerNotAvailable = 2214592515U;

		// Token: 0x0400011E RID: 286
		public const uint RetryOtherHubError = 2214592516U;

		// Token: 0x0400011F RID: 287
		public const uint TemporaryNDRGenerationFailureOtherHub = 2214592517U;

		// Token: 0x04000120 RID: 288
		public const uint UnexpectedError = 3221225472U;
	}
}
