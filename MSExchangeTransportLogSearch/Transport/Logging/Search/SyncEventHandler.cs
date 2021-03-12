using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200005F RID: 95
	internal class SyncEventHandler : SyncHealthLogEventHandler
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x0000C5D4 File Offset: 0x0000A7D4
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
				Convert.ToDouble(eventData[6].Value);
				((string)eventData[7].Value).ToLowerInvariant();
				((string)eventData[8].Value).ToLowerInvariant();
				int num = (int)eventData[9].Value;
				int num2 = (int)eventData[10].Value;
				int num3 = (int)eventData[11].Value;
				int num4 = (int)eventData[12].Value;
				int num5 = (int)eventData[13].Value;
				int num6 = (int)eventData[14].Value;
				int num7 = (int)eventData[15].Value;
				int num8 = (int)eventData[16].Value;
				int num9 = (int)eventData[17].Value;
				int num10 = (int)eventData[18].Value;
				int num11 = (int)eventData[19].Value;
				int num12 = (int)eventData[20].Value;
				Convert.ToDouble(eventData[21].Value);
				bool.Parse((string)eventData[22].Value);
				bool.Parse((string)eventData[23].Value);
				int num13 = (int)eventData[24].Value;
				int num14 = (int)eventData[25].Value;
				int num15 = (int)eventData[26].Value;
				int num16 = (int)eventData[27].Value;
				int num17 = (int)eventData[28].Value;
				int num18 = (int)eventData[29].Value;
				int num19 = (int)eventData[30].Value;
				int num20 = (int)eventData[31].Value;
				int num21 = (int)eventData[32].Value;
				int num22 = (int)eventData[33].Value;
				int num23 = (int)eventData[34].Value;
				int num24 = (int)eventData[35].Value;
				int num25 = (int)eventData[36].Value;
				int num26 = (int)eventData[37].Value;
				int num27 = (int)eventData[38].Value;
				int num28 = (int)eventData[39].Value;
				int num29 = (int)eventData[40].Value;
				int num30 = (int)eventData[41].Value;
				Convert.ToDouble(eventData[42].Value);
				int num31 = (int)eventData[43].Value;
				Convert.ToDouble(eventData[44].Value);
				int num32 = (int)eventData[45].Value;
				Convert.ToDouble(eventData[46].Value);
				int num33 = (int)eventData[47].Value;
				Convert.ToDouble(eventData[48].Value);
				Convert.ToDouble(eventData[49].Value);
				int num34 = (int)eventData[50].Value;
				Convert.ToDouble(eventData[51].Value);
				int num35 = (int)eventData[52].Value;
				Convert.ToDouble(eventData[53].Value);
				Convert.ToDouble(eventData[54].Value);
				long.Parse(eventData[55].Value.ToString());
				long.Parse(eventData[56].Value.ToString());
				Convert.ToDouble(eventData[57].Value);
				string text7 = (string)eventData[58].Value;
				string text8 = (string)eventData[59].Value;
				int num36 = (int)eventData[60].Value;
				int num37 = (int)eventData[61].Value;
				string text9 = (string)eventData[62].Value;
				string text10 = (string)eventData[63].Value;
				int num38 = (int)eventData[64].Value;
				bool.Parse((string)eventData[65].Value);
				string text11 = (string)eventData[66].Value;
				bool.Parse((string)eventData[67].Value);
				string text12 = (string)eventData[68].Value;
				string text13 = (string)eventData[69].Value;
				string text14 = (string)eventData[70].Value;
				string text15 = (string)eventData[71].Value;
				string text16 = (string)eventData[72].Value;
				string text17 = (string)eventData[73].Value;
				string text18 = (string)eventData[74].Value;
				string text19 = (string)eventData[75].Value;
				string text20 = (string)eventData[76].Value;
				string text21 = (string)eventData[77].Value;
				string text22 = (string)eventData[78].Value;
				string text23 = (string)eventData[79].Value;
				string text24 = (string)eventData[80].Value;
				string text25 = (string)eventData[81].Value;
				long.Parse(eventData[82].Value.ToString());
				string text26 = (string)eventData[83].Value;
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

		// Token: 0x0400016A RID: 362
		internal const string EventName = "sync";

		// Token: 0x0400016B RID: 363
		private const string PoisonSubscriptionStatusString = "poisonous";

		// Token: 0x0400016C RID: 364
		private const string DisabledSubscriptionStatusString = "disabled";

		// Token: 0x0400016D RID: 365
		private const string DelayedSubscriptionStatusString = "delayed";

		// Token: 0x0400016E RID: 366
		private const string AuthenticationErrorSubscriptionDetailedStatusString = "authenticationerror";

		// Token: 0x0400016F RID: 367
		private const string ConnectionErrorSubscriptionDetailedStatusString = "connectionerror";

		// Token: 0x04000170 RID: 368
		private const string CommunicationErrorSubscriptionDetailedStatusString = "communicationerror";

		// Token: 0x04000171 RID: 369
		private const string RemoteMailboxQuotaWarningSubscriptionDetailedStatusString = "remotemailboxquotawarning";

		// Token: 0x04000172 RID: 370
		private const string LabsMailboxQuotaWarningSubscriptionDetailedStatusString = "labsmailboxquotawarning";

		// Token: 0x04000173 RID: 371
		private const string MaxedOutSyncRelationshipsErrorSubscriptionDetailedStatusString = "maxedoutsyncrelationshipserror";
	}
}
