using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002DD RID: 733
	internal class ResponseDetails
	{
		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x00061E11 File Offset: 0x00060011
		// (set) Token: 0x0600150B RID: 5387 RVA: 0x00061E19 File Offset: 0x00060019
		internal bool HasTransientErrors { get; set; }

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x00061E22 File Offset: 0x00060022
		// (set) Token: 0x0600150D RID: 5389 RVA: 0x00061E2A File Offset: 0x0006002A
		internal bool HasFatalErrors { get; set; }

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600150E RID: 5390 RVA: 0x00061E33 File Offset: 0x00060033
		// (set) Token: 0x0600150F RID: 5391 RVA: 0x00061E3B File Offset: 0x0006003B
		internal bool HasConnectionErrors { get; set; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x00061E44 File Offset: 0x00060044
		// (set) Token: 0x06001511 RID: 5393 RVA: 0x00061E4C File Offset: 0x0006004C
		internal bool HasReadStatusErrors { get; set; }

		// Token: 0x06001512 RID: 5394 RVA: 0x00061E55 File Offset: 0x00060055
		internal ResponseDetails()
		{
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x00061E5D File Offset: 0x0006005D
		public void GetErrors(ref bool hasTransientErrors, ref bool hasFatalErrors, ref bool hasConnectionErrors, ref bool hasReadStatusErrors)
		{
			if (this.HasTransientErrors)
			{
				hasTransientErrors = true;
			}
			if (this.HasFatalErrors)
			{
				hasFatalErrors = true;
			}
			if (this.HasConnectionErrors)
			{
				hasConnectionErrors = true;
			}
			if (this.HasReadStatusErrors)
			{
				hasReadStatusErrors = true;
			}
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x00061E8C File Offset: 0x0006008C
		public List<string> GetStrings()
		{
			string text = null;
			if (this.HasTransientErrors || this.HasFatalErrors || this.HasConnectionErrors || this.HasReadStatusErrors)
			{
				if (this.HasConnectionErrors)
				{
					text = "WebServiceError:" + 'C';
				}
				else if (this.HasFatalErrors)
				{
					text = "WebServiceError:" + 'F';
				}
				else if (this.HasTransientErrors)
				{
					text = "WebServiceError:" + 'T';
				}
				else if (this.HasReadStatusErrors)
				{
					text = "WebServiceError:" + 'R';
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return new List<string>
			{
				text
			};
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00061F44 File Offset: 0x00060144
		internal static ResponseDetails GetResponseDetails(string[] data)
		{
			ResponseDetails responseDetails = new ResponseDetails();
			if (data != null)
			{
				int i = 0;
				while (i < data.Length)
				{
					string text = data[i];
					int num = text.IndexOf("WebServiceError:", StringComparison.Ordinal);
					int num2 = num + "WebServiceError:".Length;
					if (num != -1 && num2 < text.Length)
					{
						char c = text[num2];
						if (c == 'C')
						{
							responseDetails.HasConnectionErrors = true;
							break;
						}
						if (c == 'F')
						{
							responseDetails.HasFatalErrors = true;
							break;
						}
						switch (c)
						{
						case 'R':
							responseDetails.HasReadStatusErrors = true;
							return responseDetails;
						case 'S':
							return responseDetails;
						case 'T':
							responseDetails.HasTransientErrors = true;
							return responseDetails;
						default:
							return responseDetails;
						}
					}
					else
					{
						i++;
					}
				}
			}
			return responseDetails;
		}

		// Token: 0x04000DB8 RID: 3512
		private const string ErrrorLabel = "WebServiceError:";

		// Token: 0x04000DB9 RID: 3513
		private const char TrasientErrorValue = 'T';

		// Token: 0x04000DBA RID: 3514
		private const char FatalErrorValue = 'F';

		// Token: 0x04000DBB RID: 3515
		private const char ConnectionErrorValue = 'C';

		// Token: 0x04000DBC RID: 3516
		private const char ReadStatusErrorValue = 'R';
	}
}
