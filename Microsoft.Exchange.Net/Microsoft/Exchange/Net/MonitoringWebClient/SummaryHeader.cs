using System;
using System.Text;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007EB RID: 2027
	internal class SummaryHeader
	{
		// Token: 0x06002A75 RID: 10869 RVA: 0x0005C971 File Offset: 0x0005AB71
		internal SummaryHeader(string headerTitle, int length, Func<ResponseTrackerItem, string[]> valueExtractionDelegate)
		{
			this.HeaderTitle = headerTitle;
			this.Length = length;
			this.ValueExtractionDelegate = valueExtractionDelegate;
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x0005C990 File Offset: 0x0005AB90
		internal void Append(bool useCsvFormat, StringBuilder stringBuilder, string itemToLog)
		{
			if (itemToLog == null)
			{
				itemToLog = string.Empty;
			}
			if (!useCsvFormat)
			{
				if (itemToLog.Length > this.Length)
				{
					itemToLog = itemToLog.Substring(0, this.Length - 1) + " ";
				}
				else if (itemToLog.Length < this.Length)
				{
					itemToLog = itemToLog.PadRight(this.Length);
				}
			}
			else
			{
				if (itemToLog.Length > this.Length)
				{
					itemToLog = itemToLog.Substring(0, this.Length);
				}
				itemToLog += ",";
			}
			stringBuilder.Append(itemToLog);
		}

		// Token: 0x04002535 RID: 9525
		internal string HeaderTitle;

		// Token: 0x04002536 RID: 9526
		internal int Length;

		// Token: 0x04002537 RID: 9527
		internal Func<ResponseTrackerItem, string[]> ValueExtractionDelegate;
	}
}
