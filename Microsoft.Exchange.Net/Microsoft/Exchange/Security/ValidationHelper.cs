using System;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C96 RID: 3222
	internal static class ValidationHelper
	{
		// Token: 0x060046F5 RID: 18165 RVA: 0x000BEDB2 File Offset: 0x000BCFB2
		public static string ExceptionMessage(Exception exception)
		{
			if (exception == null)
			{
				return string.Empty;
			}
			if (exception.InnerException == null)
			{
				return exception.Message;
			}
			return exception.Message + " (" + ValidationHelper.ExceptionMessage(exception.InnerException) + ")";
		}

		// Token: 0x060046F6 RID: 18166 RVA: 0x000BEDEC File Offset: 0x000BCFEC
		public static string ToString(object objectValue)
		{
			if (objectValue == null)
			{
				return "(null)";
			}
			if (objectValue is string && (objectValue as string).Length == 0)
			{
				return "(string.empty)";
			}
			if (objectValue is Exception)
			{
				return ValidationHelper.ExceptionMessage(objectValue as Exception);
			}
			if (objectValue is IntPtr)
			{
				return "0x" + ((IntPtr)objectValue).ToString("x");
			}
			return objectValue.ToString();
		}
	}
}
