using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200005D RID: 93
	internal class SubscriptionDispatchEventHandler : SyncHealthLogEventHandler
	{
		// Token: 0x060001DF RID: 479 RVA: 0x0000C270 File Offset: 0x0000A470
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
				string text6 = (string)eventData[5].Value;
				string text7 = (string)eventData[6].Value;
				string text8 = (string)eventData[7].Value;
				bool.Parse((string)eventData[8].Value);
				bool.Parse((string)eventData[9].Value);
				bool.Parse((string)eventData[10].Value);
				string text9 = (string)eventData[11].Value;
				bool.Parse((string)eventData[12].Value);
				int num = (int)eventData[13].Value;
				string text10 = (string)eventData[14].Value;
				string text11 = (string)eventData[15].Value;
				string text12 = (string)eventData[16].Value;
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

		// Token: 0x04000168 RID: 360
		internal const string EventName = "subscriptiondispatch";
	}
}
