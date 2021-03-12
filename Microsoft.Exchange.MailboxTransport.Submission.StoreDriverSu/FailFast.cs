using System;
using System.Threading;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x0200003A RID: 58
	internal static class FailFast
	{
		// Token: 0x0600023F RID: 575 RVA: 0x0000CE5C File Offset: 0x0000B05C
		public static void Fail(Exception exception)
		{
			ParameterizedThreadStart start = new ParameterizedThreadStart(FailFast.ThrowException);
			Thread thread = new Thread(start);
			FailFast.UnexpectedSubmissionException parameter = new FailFast.UnexpectedSubmissionException(exception);
			thread.Start(parameter);
			thread.Join();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000CE91 File Offset: 0x0000B091
		private static void ThrowException(object exception)
		{
			throw (Exception)exception;
		}

		// Token: 0x0200003B RID: 59
		[Serializable]
		internal class UnexpectedSubmissionException : LocalizedException
		{
			// Token: 0x06000241 RID: 577 RVA: 0x0000CE99 File Offset: 0x0000B099
			public UnexpectedSubmissionException(Exception innerException) : base(new LocalizedString(Strings.UnexpectedException), innerException)
			{
			}
		}
	}
}
