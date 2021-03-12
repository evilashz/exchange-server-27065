using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000968 RID: 2408
	internal class SendCalendarSharingInviteStatistic
	{
		// Token: 0x06004539 RID: 17721 RVA: 0x000F2229 File Offset: 0x000F0429
		public SendCalendarSharingInviteStatistic()
		{
			this.requestedDetailLevelCountMap = new Dictionary<string, int>();
			this.delegateFailedInvites = new List<Tuple<Exception, CalendarSharingRecipient>>();
			this.otherFailedInvites = new List<Tuple<Exception, CalendarSharingRecipient>>();
		}

		// Token: 0x0600453A RID: 17722 RVA: 0x000F2254 File Offset: 0x000F0454
		public void IncreaseSucceededInvite(string detailLevel)
		{
			if (!this.requestedDetailLevelCountMap.ContainsKey(detailLevel))
			{
				this.requestedDetailLevelCountMap.Add(detailLevel, 1);
				return;
			}
			Dictionary<string, int> dictionary;
			(dictionary = this.requestedDetailLevelCountMap)[detailLevel] = dictionary[detailLevel] + 1;
		}

		// Token: 0x0600453B RID: 17723 RVA: 0x000F2298 File Offset: 0x000F0498
		public void LogFailedInvite(Exception ex, CalendarSharingRecipient recipient)
		{
			List<Tuple<Exception, CalendarSharingRecipient>> list;
			if (recipient.DetailLevelType == CalendarSharingDetailLevel.Delegate)
			{
				list = this.delegateFailedInvites;
			}
			else
			{
				list = this.otherFailedInvites;
			}
			list.Add(new Tuple<Exception, CalendarSharingRecipient>(ex, recipient));
		}

		// Token: 0x0600453C RID: 17724 RVA: 0x000F22D0 File Offset: 0x000F04D0
		public string GetSuccessString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.requestedDetailLevelCountMap.Count > 0)
			{
				stringBuilder.Append("Success-[");
				foreach (KeyValuePair<string, int> keyValuePair in this.requestedDetailLevelCountMap)
				{
					stringBuilder.AppendFormat("{0}-{1}|", keyValuePair.Key, keyValuePair.Value);
				}
				stringBuilder.Append("]");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600453D RID: 17725 RVA: 0x000F2370 File Offset: 0x000F0570
		public string GetErrorString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.AppendErrors(stringBuilder, this.delegateFailedInvites, "DelErrs");
			this.AppendErrors(stringBuilder, this.otherFailedInvites, "Errs");
			return stringBuilder.ToString();
		}

		// Token: 0x0600453E RID: 17726 RVA: 0x000F23B0 File Offset: 0x000F05B0
		private void AppendErrors(StringBuilder sb, List<Tuple<Exception, CalendarSharingRecipient>> errorList, string errorTag)
		{
			if (errorList.Count > 0)
			{
				sb.AppendFormat("{0}-[", errorTag);
				HashSet<Type> hashSet = new HashSet<Type>();
				foreach (Tuple<Exception, CalendarSharingRecipient> tuple in this.delegateFailedInvites)
				{
					if (!hashSet.Contains(tuple.Item1.GetType()))
					{
						hashSet.Add(tuple.Item1.GetType());
						sb.AppendFormat("PRIP-{0}|EX-{1}:{2}|", tuple.Item2.EmailAddress.EmailAddress, tuple.Item1.GetType().Name, tuple.Item1.Message);
					}
				}
				sb.Append("]");
			}
		}

		// Token: 0x04002844 RID: 10308
		private const string SuccessString = "Success-[";

		// Token: 0x04002845 RID: 10309
		private const string EndBracket = "]";

		// Token: 0x04002846 RID: 10310
		private const string KeyValuePairFormat = "{0}-{1}|";

		// Token: 0x04002847 RID: 10311
		private const string DelegateErrors = "DelErrs";

		// Token: 0x04002848 RID: 10312
		private const string Errors = "Errs";

		// Token: 0x04002849 RID: 10313
		private const string ErrorKeyValuePairFormat = "PRIP-{0}|EX-{1}:{2}|";

		// Token: 0x0400284A RID: 10314
		private const string ErrorTagFormat = "{0}-[";

		// Token: 0x0400284B RID: 10315
		private Dictionary<string, int> requestedDetailLevelCountMap;

		// Token: 0x0400284C RID: 10316
		private List<Tuple<Exception, CalendarSharingRecipient>> delegateFailedInvites;

		// Token: 0x0400284D RID: 10317
		private List<Tuple<Exception, CalendarSharingRecipient>> otherFailedInvites;
	}
}
