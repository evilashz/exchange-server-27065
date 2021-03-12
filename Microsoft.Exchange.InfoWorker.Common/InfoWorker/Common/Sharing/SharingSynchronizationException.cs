using System;
using System.Collections;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x0200025E RID: 606
	[Serializable]
	public abstract class SharingSynchronizationException : LocalizedException
	{
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x00050A40 File Offset: 0x0004EC40
		public string ErrorDetails
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				this.BuildErrorDetails(stringBuilder);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x00050A65 File Offset: 0x0004EC65
		// (set) Token: 0x06001179 RID: 4473 RVA: 0x00050A6D File Offset: 0x0004EC6D
		public Exception AdditionalException { get; set; }

		// Token: 0x0600117A RID: 4474 RVA: 0x00050A76 File Offset: 0x0004EC76
		public SharingSynchronizationException(LocalizedString localizedString) : base(localizedString)
		{
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00050A7F File Offset: 0x0004EC7F
		public SharingSynchronizationException(LocalizedString localizedString, Exception innerException) : base(localizedString, innerException)
		{
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00050A8C File Offset: 0x0004EC8C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString());
			stringBuilder.AppendLine("=============");
			this.BuildErrorDetails(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00050ABE File Offset: 0x0004ECBE
		private void BuildErrorDetails(StringBuilder errorMessage)
		{
			SharingSynchronizationException.AppendExceptionInformation(errorMessage, this);
			if (this.AdditionalException != null)
			{
				errorMessage.AppendLine("=============");
				SharingSynchronizationException.AppendExceptionInformation(errorMessage, this.AdditionalException);
			}
			errorMessage.AppendLine("=============");
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00050AF4 File Offset: 0x0004ECF4
		private static void AppendExceptionInformation(StringBuilder errorMessage, Exception exception)
		{
			while (exception != null)
			{
				errorMessage.AppendFormat("{0}: {1}; ", exception.GetType(), exception.Message);
				SoapFaultException ex = exception as SoapFaultException;
				if (ex != null && ex.Fault != null)
				{
					errorMessage.AppendFormat("Fault: {0};", ex.Fault.InnerXml);
				}
				errorMessage.AppendLine();
				foreach (object obj in exception.Data)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (dictionaryEntry.Value != null)
					{
						errorMessage.AppendFormat("{0}:{1}{2}{1}", dictionaryEntry.Key, Environment.NewLine, dictionaryEntry.Value);
					}
				}
				exception = exception.InnerException;
				if (exception != null)
				{
					errorMessage.AppendLine("-------------");
				}
			}
		}
	}
}
