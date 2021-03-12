using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Principal;
using System.Text;

namespace Microsoft.Exchange.Extensions
{
	// Token: 0x0200001C RID: 28
	public static class ExceptionExtension
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00003B74 File Offset: 0x00001D74
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

		// Token: 0x0600008D RID: 141 RVA: 0x00003BC4 File Offset: 0x00001DC4
		public static string ToTraceString(this Exception exception)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (exception != null)
			{
				stringBuilder.AppendLine(exception.ToString());
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

		// Token: 0x0600008E RID: 142 RVA: 0x00003C18 File Offset: 0x00001E18
		public static void PreserveExceptionStack(this Exception e)
		{
			if (e != null && ExceptionExtension.preserveStackTraceMethod != null)
			{
				ExceptionExtension.preserveStackTraceMethod.Invoke(e, null);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003C38 File Offset: 0x00001E38
		public static bool ContainsInnerException<T>(this Exception exception)
		{
			if (exception == null)
			{
				return false;
			}
			for (Exception innerException = exception.InnerException; innerException != null; innerException = innerException.InnerException)
			{
				if (innerException is T)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003C68 File Offset: 0x00001E68
		public static string GetCustomMessage(this Exception exception)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (exception is IdentityNotMappedException)
			{
				using (IEnumerator<IdentityReference> enumerator = (exception as IdentityNotMappedException).UnmappedIdentities.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IdentityReference identityReference = enumerator.Current;
						stringBuilder.AppendLine(identityReference.ToString());
					}
					goto IL_90;
				}
			}
			if (exception is SecurityException)
			{
				SecurityException ex = exception as SecurityException;
				stringBuilder.AppendLine("Demand: " + Convert.ToString(ex.Demanded));
				stringBuilder.AppendLine("First Failed Demand: " + Convert.ToString(ex.FirstPermissionThatFailed));
			}
			IL_90:
			return stringBuilder.ToString();
		}

		// Token: 0x0400006E RID: 110
		private static readonly MethodInfo preserveStackTraceMethod = typeof(Exception).GetTypeInfo().DeclaredMethods.FirstOrDefault((MethodInfo m) => string.Equals(m.Name, "InternalPreserveStackTrace", StringComparison.OrdinalIgnoreCase) && !m.IsStatic && !m.IsPublic);
	}
}
