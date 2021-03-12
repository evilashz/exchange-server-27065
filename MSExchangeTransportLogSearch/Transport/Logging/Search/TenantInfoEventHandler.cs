using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000061 RID: 97
	internal class TenantInfoEventHandler : SyncHealthLogEventHandler
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x0000CD14 File Offset: 0x0000AF14
		internal override bool ProcessEntry(DateTime entryTimeStamp, KeyValuePair<string, object>[] eventData, out Exception exception)
		{
			exception = null;
			try
			{
				string text = (string)eventData[0].Value;
				string text2 = (string)eventData[1].Value;
				string text3 = (string)eventData[2].Value;
				int num = (int)eventData[3].Value;
				int num2 = (int)eventData[4].Value;
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

		// Token: 0x0400017C RID: 380
		internal const string EventName = "tenantinfo";
	}
}
