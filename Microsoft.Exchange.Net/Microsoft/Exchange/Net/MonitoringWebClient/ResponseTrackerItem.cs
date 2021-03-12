using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007EA RID: 2026
	[Serializable]
	public class ResponseTrackerItem
	{
		// Token: 0x06002A72 RID: 10866 RVA: 0x0005C7C8 File Offset: 0x0005A9C8
		internal bool Matches(TestId? testId, RequestTarget? requestTarget, HttpWebRequestWrapper request)
		{
			return (testId == null || this.StepId == testId.ToString()) && (requestTarget == null || this.TargetType == requestTarget.ToString()) && this.TargetHost == request.RequestUri.Host && this.PathAndQuery == request.RequestUri.PathAndQuery;
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x0005C850 File Offset: 0x0005AA50
		internal void AppendSummary(bool useCsvFormat, SummaryHeader[] headers, StringBuilder stringBuilder)
		{
			if (stringBuilder.Length == 0)
			{
				foreach (SummaryHeader summaryHeader in headers)
				{
					summaryHeader.Append(useCsvFormat, stringBuilder, summaryHeader.HeaderTitle);
				}
			}
			stringBuilder.Append(Environment.NewLine);
			int num = 1;
			Dictionary<SummaryHeader, string[]> dictionary = new Dictionary<SummaryHeader, string[]>();
			foreach (SummaryHeader summaryHeader2 in headers)
			{
				string[] array = summaryHeader2.ValueExtractionDelegate(this);
				if (array != null && array.Length > 0)
				{
					if (array.Length > 1)
					{
						num = ((array.Length > num) ? array.Length : num);
						dictionary.Add(summaryHeader2, array);
					}
					summaryHeader2.Append(useCsvFormat, stringBuilder, array[0]);
				}
			}
			for (int k = 1; k < num; k++)
			{
				stringBuilder.Append(Environment.NewLine);
				foreach (SummaryHeader summaryHeader3 in headers)
				{
					string itemToLog = string.Empty;
					string[] array2;
					if (dictionary.TryGetValue(summaryHeader3, out array2) && array2.Length > k)
					{
						itemToLog = array2[k];
					}
					summaryHeader3.Append(useCsvFormat, stringBuilder, itemToLog);
				}
			}
		}

		// Token: 0x04002519 RID: 9497
		public int Index;

		// Token: 0x0400251A RID: 9498
		public string StepId;

		// Token: 0x0400251B RID: 9499
		public string TargetHost;

		// Token: 0x0400251C RID: 9500
		public string TargetIpAddress;

		// Token: 0x0400251D RID: 9501
		public string TargetType;

		// Token: 0x0400251E RID: 9502
		public string PathAndQuery;

		// Token: 0x0400251F RID: 9503
		public string RespondingServer;

		// Token: 0x04002520 RID: 9504
		public string MailboxServer;

		// Token: 0x04002521 RID: 9505
		public string DomainController;

		// Token: 0x04002522 RID: 9506
		public string ARRServer;

		// Token: 0x04002523 RID: 9507
		public string FailingServer;

		// Token: 0x04002524 RID: 9508
		public string FailingTargetHostname;

		// Token: 0x04002525 RID: 9509
		public string FailingTargetIPAddress;

		// Token: 0x04002526 RID: 9510
		public int? FailureHttpResponseCode;

		// Token: 0x04002527 RID: 9511
		public TimeSpan ResponseLatency;

		// Token: 0x04002528 RID: 9512
		public TimeSpan TotalLatency;

		// Token: 0x04002529 RID: 9513
		public TimeSpan? CasLatency;

		// Token: 0x0400252A RID: 9514
		public TimeSpan? RpcLatency;

		// Token: 0x0400252B RID: 9515
		public TimeSpan? LdapLatency;

		// Token: 0x0400252C RID: 9516
		public TimeSpan? MservLatency;

		// Token: 0x0400252D RID: 9517
		public TimeSpan DnsLatency;

		// Token: 0x0400252E RID: 9518
		public string TargetVipName;

		// Token: 0x0400252F RID: 9519
		public string TargetVipForestName;

		// Token: 0x04002530 RID: 9520
		public long ContentLength;

		// Token: 0x04002531 RID: 9521
		public string FailedIpAddresses;

		// Token: 0x04002532 RID: 9522
		public bool? IsE14CasServer;

		// Token: 0x04002533 RID: 9523
		[XmlIgnore]
		[NonSerialized]
		internal HttpWebResponseWrapper Response;

		// Token: 0x04002534 RID: 9524
		[XmlIgnore]
		[NonSerialized]
		internal ConcurrentDictionary<NamedVip, Exception> IpAddressListFailureList;
	}
}
