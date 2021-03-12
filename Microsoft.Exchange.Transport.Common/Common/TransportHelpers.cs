using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Common
{
	// Token: 0x0200000E RID: 14
	internal static class TransportHelpers
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002F60 File Offset: 0x00001160
		public static bool AttemptAddToDictionary<TKey, TValue>(IDictionary<TKey, TValue> currentEntries, TKey keyToAdd, TValue valueToAdd, TransportHelpers.DiagnosticsHandler<TKey, TValue> diagnosticsHandler = null)
		{
			ArgumentValidator.ThrowIfNull("currentEntries", currentEntries);
			ArgumentValidator.ThrowIfNull("keyToAdd", keyToAdd);
			try
			{
				currentEntries.Add(keyToAdd, valueToAdd);
			}
			catch (ArgumentException ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("Attempt made to add a duplicate entry to the dictionary");
				stringBuilder.AppendLine("Current entries in the dictionary");
				foreach (KeyValuePair<TKey, TValue> keyValuePair in currentEntries)
				{
					stringBuilder.AppendLine(string.Format("Key={0}, Value={1}", keyValuePair.Key, keyValuePair.Value));
					if (stringBuilder.Length > 4096)
					{
						stringBuilder.AppendLine("Truncating current contents of dictionary");
						break;
					}
				}
				stringBuilder.AppendLine("Entry to be added to the dictionary");
				stringBuilder.AppendLine(string.Format("Key={0}, Value={1}", keyToAdd, valueToAdd));
				string text = stringBuilder.ToString();
				bool flag;
				ExWatson.SendThrottledGenericWatsonReport("E12", ExWatson.ApplicationVersion.ToString(), ExWatson.AppName, "15.00.1497.010", Assembly.GetExecutingAssembly().GetName().Name, ex.GetType().Name, ex.StackTrace, ex.GetHashCode().ToString(CultureInfo.InvariantCulture), ex.TargetSite.Name, text, TimeSpan.FromHours(1.0), out flag);
				if (diagnosticsHandler != null)
				{
					diagnosticsHandler(keyToAdd, valueToAdd, text);
				}
				return false;
			}
			return true;
		}

		// Token: 0x04000032 RID: 50
		private const int SizeToStopListingCurrentDictionaryContents = 4096;

		// Token: 0x0200000F RID: 15
		// (Invoke) Token: 0x0600004B RID: 75
		public delegate void DiagnosticsHandler<in TKey, in TValue>(TKey key, TValue value, string message);
	}
}
