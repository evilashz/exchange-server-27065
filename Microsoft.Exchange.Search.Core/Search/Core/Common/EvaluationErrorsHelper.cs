using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000067 RID: 103
	internal static class EvaluationErrorsHelper
	{
		// Token: 0x06000253 RID: 595 RVA: 0x00005E48 File Offset: 0x00004048
		public static int MakeRetriableError(int evaluationError)
		{
			return (int)EvaluationErrorsHelper.GetErrorCode(evaluationError);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00005E50 File Offset: 0x00004050
		public static int MakeRetriableError(EvaluationErrors evaluationError)
		{
			return (int)EvaluationErrorsHelper.GetErrorCode(evaluationError);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00005E58 File Offset: 0x00004058
		public static int MakePermanentError(int evaluationError)
		{
			return (int)(-(int)EvaluationErrorsHelper.GetErrorCode(evaluationError));
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00005E61 File Offset: 0x00004061
		public static int MakePermanentError(EvaluationErrors evaluationError)
		{
			return (int)(-(int)EvaluationErrorsHelper.GetErrorCode(evaluationError));
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00005E6A File Offset: 0x0000406A
		public static bool IsRetriableError(EvaluationErrors evaluationError)
		{
			return EvaluationErrorsHelper.IsRetriableError((int)evaluationError);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00005E72 File Offset: 0x00004072
		public static bool IsRetriableError(int evaluationError)
		{
			return evaluationError > 0;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00005E78 File Offset: 0x00004078
		public static bool IsPermanentError(EvaluationErrors evaluationError)
		{
			return EvaluationErrorsHelper.IsPermanentError((int)evaluationError);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00005E80 File Offset: 0x00004080
		public static bool IsPermanentError(int evaluationError)
		{
			return evaluationError < 0;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00005E86 File Offset: 0x00004086
		public static EvaluationErrors GetErrorCode(EvaluationErrors evaluationError)
		{
			return EvaluationErrorsHelper.GetErrorCode((int)evaluationError);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00005E8E File Offset: 0x0000408E
		public static EvaluationErrors GetErrorCode(int evaluationError)
		{
			return (EvaluationErrors)Math.Abs(evaluationError);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00005E96 File Offset: 0x00004096
		public static LocalizedString GetErrorDescription(EvaluationErrors evaluationError)
		{
			evaluationError = EvaluationErrorsHelper.GetErrorCode(evaluationError);
			if (evaluationError == EvaluationErrors.None)
			{
				return LocalizedString.Empty;
			}
			return new LocalizedString(LocalizedDescriptionAttribute.FromEnum(typeof(EvaluationErrors), evaluationError));
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00005EC4 File Offset: 0x000040C4
		public static bool ShouldMakePermanent(int attemptCount, int errorCode)
		{
			EvaluationErrors errorCode2 = EvaluationErrorsHelper.GetErrorCode(errorCode);
			switch (errorCode2)
			{
			case EvaluationErrors.AttachmentLimitReached:
			case EvaluationErrors.DocumentParserFailure:
				break;
			case EvaluationErrors.MarsWriterTruncation:
			case EvaluationErrors.AnnotationTokenError:
				goto IL_5A;
			case EvaluationErrors.PoisonDocument:
				return attemptCount >= EvaluationErrorsHelper.searchConfig.MaxAttemptCountPoisonBeforePermanent || EvaluationErrorsHelper.IsPermanentError(errorCode);
			default:
				switch (errorCode2)
				{
				case EvaluationErrors.MailboxQuarantined:
				case EvaluationErrors.LoginFailed:
				case EvaluationErrors.TextConversionFailure:
					break;
				case EvaluationErrors.MailboxLocked:
				case EvaluationErrors.MapiNoSupport:
					goto IL_5A;
				default:
					goto IL_5A;
				}
				break;
			}
			return true;
			IL_5A:
			return attemptCount >= EvaluationErrorsHelper.searchConfig.MaxAttemptCountBeforePermanent || EvaluationErrorsHelper.IsPermanentError(errorCode);
		}

		// Token: 0x04000102 RID: 258
		public const int MaxAttemptCountBeforePermanent = 3;

		// Token: 0x04000103 RID: 259
		private static SearchConfig searchConfig = SearchConfig.Instance;
	}
}
