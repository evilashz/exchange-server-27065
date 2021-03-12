using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200005C RID: 92
	internal class SubscriptionCreationEventHandler : SyncHealthLogEventHandler
	{
		// Token: 0x060001DD RID: 477 RVA: 0x0000C0D4 File Offset: 0x0000A2D4
		internal override bool ProcessEntry(DateTime entryTimeStamp, KeyValuePair<string, object>[] eventData, out Exception exception)
		{
			exception = null;
			string text = null;
			try
			{
				string text2 = (string)eventData[0].Value;
				string text3 = (string)eventData[1].Value;
				string text4 = (string)eventData[2].Value;
				string text5 = (string)eventData[3].Value;
				string text6 = (string)eventData[4].Value;
				string text7 = (string)eventData[5].Value;
				string text8 = (string)eventData[6].Value;
				string text9 = (string)eventData[7].Value;
				int num = (int)eventData[8].Value;
				string text10 = (string)eventData[9].Value;
				string text11 = (string)eventData[10].Value;
				DateTime dateTime = (DateTime)eventData[11].Value;
				text = (string)eventData[12].Value;
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
			return exception == null && (!string.Equals(text.ToLowerInvariant(), "migration") || true);
		}

		// Token: 0x04000165 RID: 357
		internal const string EventName = "subscriptioncreation";

		// Token: 0x04000166 RID: 358
		private const string AutoprovisionedString = "auto";

		// Token: 0x04000167 RID: 359
		private const string MigrationString = "migration";
	}
}
