using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x0200029C RID: 668
	internal class EventTypeParser
	{
		// Token: 0x060012AD RID: 4781 RVA: 0x0005642D File Offset: 0x0005462D
		internal static bool TryParse(string rawString, out MessageTrackingEvent enumValue)
		{
			return EventTypeParser.dictionary.TryGetValue(rawString, out enumValue);
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x0005643C File Offset: 0x0005463C
		internal static Dictionary<string, MessageTrackingEvent> CreateTypeDictionary()
		{
			Dictionary<string, MessageTrackingEvent> dictionary = new Dictionary<string, MessageTrackingEvent>();
			string[] names = Enum.GetNames(typeof(MessageTrackingEvent));
			Array values = Enum.GetValues(typeof(MessageTrackingEvent));
			int num = names.Length;
			for (int i = 0; i < num; i++)
			{
				dictionary.Add(names[i], (MessageTrackingEvent)values.GetValue(i));
			}
			return dictionary;
		}

		// Token: 0x04000C8A RID: 3210
		private static Dictionary<string, MessageTrackingEvent> dictionary = EventTypeParser.CreateTypeDictionary();
	}
}
