using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Web;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006A4 RID: 1700
	public static class ExceptionExtension
	{
		// Token: 0x060048B2 RID: 18610 RVA: 0x000DE678 File Offset: 0x000DC878
		public static string GetFullMessage(this Exception exception)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			while (exception != null)
			{
				string message = exception.Message;
				if (message != text)
				{
					text = message;
					stringBuilder.AppendLine(text);
				}
				exception = exception.InnerException;
				if (exception != null)
				{
					stringBuilder.AppendLine();
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060048B3 RID: 18611 RVA: 0x000DE6C8 File Offset: 0x000DC8C8
		public static string ToTraceString(this Exception exception)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (exception != null)
			{
				stringBuilder.AppendLine(exception.ToString());
				stringBuilder.AppendLine(exception.StackTrace);
				string customMessage = exception.GetCustomMessage();
				if (!string.IsNullOrEmpty(customMessage))
				{
					stringBuilder.AppendLine(customMessage);
				}
				exception = exception.InnerException;
				if (exception != null)
				{
					stringBuilder.AppendLine();
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060048B4 RID: 18612 RVA: 0x000DE72C File Offset: 0x000DC92C
		public static string GetCustomMessage(this Exception exception)
		{
			StringBuilder stringBuilder = new StringBuilder();
			IdentityNotMappedException ex;
			if (exception.TryCastTo(out ex))
			{
				using (IEnumerator<IdentityReference> enumerator = ex.UnmappedIdentities.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IdentityReference identityReference = enumerator.Current;
						stringBuilder.AppendLine(identityReference.ToString());
					}
					goto IL_14C;
				}
			}
			SecurityException ex2;
			WebException ex3;
			if (exception.TryCastTo(out ex2))
			{
				stringBuilder.Append("Demand: ");
				stringBuilder.AppendLine((ex2.Demanded != null) ? ex2.Demanded.ToString() : string.Empty);
				stringBuilder.Append("First Failed Demand: ");
				stringBuilder.AppendLine((ex2.FirstPermissionThatFailed != null) ? ex2.FirstPermissionThatFailed.ToString() : string.Empty);
				if (HttpContext.Current != null && HttpContext.Current.Request != null)
				{
					stringBuilder.Append("Url: ");
					stringBuilder.AppendLine(HttpContext.Current.GetRequestUrlForLog());
				}
			}
			else if (exception.TryCastTo(out ex3) && ex3.Response != null)
			{
				Stream responseStream = ex3.Response.GetResponseStream();
				if (responseStream.CanRead)
				{
					using (StreamReader streamReader = new StreamReader(responseStream))
					{
						stringBuilder.Append("Response Stream: ");
						stringBuilder.AppendLine(streamReader.ReadToEnd());
						goto IL_14C;
					}
				}
				responseStream.Close();
			}
			IL_14C:
			return stringBuilder.ToString();
		}

		// Token: 0x060048B5 RID: 18613 RVA: 0x000DE8A8 File Offset: 0x000DCAA8
		public static bool TryCastTo<T>(this Exception exception, out T target) where T : Exception
		{
			target = (exception as T);
			return target != null;
		}

		// Token: 0x060048B6 RID: 18614 RVA: 0x000DE8CC File Offset: 0x000DCACC
		public static bool IsInterestingHttpException(this Exception exception)
		{
			return exception is HttpException && ((HttpException)exception).GetHttpCode() == 404;
		}

		// Token: 0x060048B7 RID: 18615 RVA: 0x000DE8EC File Offset: 0x000DCAEC
		public static bool IsMaxRequestLengthExceededException(this Exception exception)
		{
			if (exception is HttpException)
			{
				Exception ex = exception;
				if (ex.InnerException is HttpException)
				{
					ex = ex.InnerException;
				}
				if (((HttpException)ex).GetHttpCode() == 500 && ((HttpException)ex).ErrorCode == -2147467259)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060048B8 RID: 18616 RVA: 0x000DE940 File Offset: 0x000DCB40
		public static bool IsUICriticalException(this Exception ex)
		{
			return ex is NullReferenceException || ex is StackOverflowException || ex is OutOfMemoryException || ex is ThreadAbortException || ex is IndexOutOfRangeException || ex is ArgumentException || ex is AccessViolationException || ex is NotSupportedException || ex is NotImplementedException || ex is InvalidOperationException || ex is InvalidCastException;
		}

		// Token: 0x060048B9 RID: 18617 RVA: 0x000DE9AB File Offset: 0x000DCBAB
		public static bool IsControlPanelException(this Exception ex)
		{
			return ex.StackTrace.Contains("Microsoft.Exchange.Management.ControlPanel");
		}

		// Token: 0x060048BA RID: 18618 RVA: 0x000DE9BD File Offset: 0x000DCBBD
		public static ErrorInformationBase ToErrorInformationBase(this Exception exception)
		{
			return new ErrorInformationBase(exception);
		}

		// Token: 0x060048BB RID: 18619 RVA: 0x000DE9CD File Offset: 0x000DCBCD
		public static ErrorInformationBase[] ToErrorInformationBase(this Exception[] exceptions)
		{
			return Array.ConvertAll<Exception, ErrorInformationBase>(exceptions, (Exception x) => x.ToErrorInformationBase());
		}

		// Token: 0x060048BC RID: 18620 RVA: 0x000DE9F2 File Offset: 0x000DCBF2
		public static void ThrowFaultException(this Exception exception)
		{
			throw new FaultException<ExceptionDetail>(new ExceptionDetail(exception), exception.Message);
		}
	}
}
