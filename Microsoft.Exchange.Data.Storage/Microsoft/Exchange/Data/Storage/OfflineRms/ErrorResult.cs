using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000AD1 RID: 2769
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class ErrorResult
	{
		// Token: 0x0600649E RID: 25758 RVA: 0x001AB154 File Offset: 0x001A9354
		private ErrorResult(Exception e)
		{
			this.ExceptionMessage = e.Message;
			this.ExceptionType = e.GetType().ToString();
			this.ExceptionStack = (string.IsNullOrEmpty(e.StackTrace) ? string.Empty : e.StackTrace.Replace("\r\n", string.Empty));
		}

		// Token: 0x0600649F RID: 25759 RVA: 0x001AB1B4 File Offset: 0x001A93B4
		public static List<ErrorResult> GetErrorResultListFromException(Exception e)
		{
			List<ErrorResult> list = new List<ErrorResult>();
			for (Exception ex = e; ex != null; ex = ex.InnerException)
			{
				list.Add(new ErrorResult(ex));
			}
			return list;
		}

		// Token: 0x060064A0 RID: 25760 RVA: 0x001AB1E4 File Offset: 0x001A93E4
		public static string GetSerializedString(IList<ErrorResult> errorResults)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ErrorResult errorResult in errorResults)
			{
				stringBuilder.Append("[Type:");
				stringBuilder.Append(errorResult.ExceptionType);
				stringBuilder.Append("]");
				stringBuilder.Append("[Message:");
				stringBuilder.Append(errorResult.ExceptionMessage);
				stringBuilder.Append("]");
				stringBuilder.Append("[ExceptionStack:");
				stringBuilder.Append(errorResult.ExceptionStack);
				stringBuilder.Append("]");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400394A RID: 14666
		public string ExceptionMessage;

		// Token: 0x0400394B RID: 14667
		public string ExceptionType;

		// Token: 0x0400394C RID: 14668
		public string ExceptionStack;
	}
}
