using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200005E RID: 94
	internal class SubscriptionInfoEventHandler : SyncHealthLogEventHandler
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x0000C49C File Offset: 0x0000A69C
		internal override bool ProcessEntry(DateTime entryTimeStamp, KeyValuePair<string, object>[] eventData, out Exception exception)
		{
			exception = null;
			try
			{
				string text = (string)eventData[0].Value;
				string text2 = (string)eventData[1].Value;
				string text3 = (string)eventData[2].Value;
				string text4 = (string)eventData[3].Value;
				string text5 = (string)eventData[4].Value;
				int num = (int)eventData[5].Value;
				int num2 = (int)eventData[6].Value;
				int num3 = (int)eventData[7].Value;
				int num4 = (int)eventData[8].Value;
			}
			catch (ArgumentNullException ex)
			{
				exception = ex;
			}
			catch (ArgumentException ex2)
			{
				exception = ex2;
			}
			catch (FormatException ex3)
			{
				exception = ex3;
			}
			catch (OverflowException ex4)
			{
				exception = ex4;
			}
			catch (IndexOutOfRangeException ex5)
			{
				exception = ex5;
			}
			catch (InvalidCastException ex6)
			{
				exception = ex6;
			}
			return exception == null;
		}

		// Token: 0x04000169 RID: 361
		internal const string EventName = "subscriptioninfo";
	}
}
