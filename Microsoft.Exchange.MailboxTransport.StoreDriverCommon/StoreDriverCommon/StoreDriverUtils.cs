using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverCommon
{
	// Token: 0x02000003 RID: 3
	internal static class StoreDriverUtils
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000213C File Offset: 0x0000033C
		public static string GetOldestPoisonEntryValueName(SynchronizedDictionary<string, CrashProperties> poisonEntryDictionary)
		{
			ArgumentValidator.ThrowIfNull("poisonEntryDictionary", poisonEntryDictionary);
			return (from keyValuePair in poisonEntryDictionary
			orderby keyValuePair.Value.LastCrashTime
			select keyValuePair.Key).FirstOrDefault<string>();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021A0 File Offset: 0x000003A0
		public static void SendInformationalWatson(Exception exception, string detailedExceptionInformation)
		{
			ArgumentValidator.ThrowIfNull("exception", exception);
			ArgumentValidator.ThrowIfNullOrEmpty("detailedExceptionInformation", detailedExceptionInformation);
			ExWatson.SendGenericWatsonReport("E12", ExWatson.ApplicationVersion.ToString(), ExWatson.AppName, "15.00.1497.010", Assembly.GetExecutingAssembly().GetName().Name, exception.GetType().Name, exception.StackTrace, exception.GetHashCode().ToString(), exception.TargetSite.Name, detailedExceptionInformation);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000221B File Offset: 0x0000041B
		public static bool CheckIfDateTimeExceedsThreshold(DateTime dateTime, DateTime dateTimeReference, TimeSpan timeSpan)
		{
			return dateTimeReference > dateTime + timeSpan;
		}
	}
}
