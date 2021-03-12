using System;
using System.Net;
using System.Security.Authentication;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x02000090 RID: 144
	internal class AuditLogExceptionFormatter : IExceptionLogFormatter
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x00017669 File Offset: 0x00015869
		public static IExceptionLogFormatter Instance
		{
			get
			{
				return AuditLogExceptionFormatter.instance;
			}
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00017670 File Offset: 0x00015870
		public string FormatException(Exception exception)
		{
			AuditLogServiceException ex = exception as AuditLogServiceException;
			if (ex != null)
			{
				return string.Format("{0}[{1}]", AuditingOpticsLogger.DefaultExceptionFormatter.FormatException(exception), ex.Code);
			}
			if (exception is AuthenticationException || exception is WebException)
			{
				return string.Format("{0}[{1}]", AuditingOpticsLogger.DefaultExceptionFormatter.FormatException(exception), exception.Message);
			}
			return AuditingOpticsLogger.DefaultExceptionFormatter.FormatException(exception);
		}

		// Token: 0x040002BC RID: 700
		private static readonly IExceptionLogFormatter instance = new AuditLogExceptionFormatter();
	}
}
