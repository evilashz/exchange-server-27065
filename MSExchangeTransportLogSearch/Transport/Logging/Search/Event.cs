using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200003A RID: 58
	internal class Event
	{
		// Token: 0x06000131 RID: 305 RVA: 0x0000901C File Offset: 0x0000721C
		internal static Event Deserialize(EventDefinition eventDefinition, KeyValuePair<string, object>[] propertyBag)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (KeyValuePair<string, object> keyValuePair in propertyBag)
			{
				if (!dictionary.ContainsKey(keyValuePair.Key))
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return new Event
			{
				TaskName = Event.TryGetProperty<string>(eventDefinition.TaskPropertyName, dictionary, null),
				Id = Event.TryGetProperty<int>(eventDefinition.IdPropertyName, dictionary, 0),
				Operation = Event.TryGetProperty<string>(eventDefinition.OperationPropertyName, dictionary, null),
				Type = Event.TryGetProperty<string>(eventDefinition.OperationTypePropertyName, dictionary, null),
				RelativeTimestamp = Event.TryGetProperty<int>(eventDefinition.RtsPropertyName, dictionary, 0),
				Count = Event.TryGetProperty<int>(eventDefinition.CountPropertyName, dictionary, 0),
				Properties = dictionary
			};
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000090F4 File Offset: 0x000072F4
		private static T TryGetProperty<T>(string propertyName, Dictionary<string, object> propertyDictionary, T defaultValue)
		{
			object obj;
			if (propertyDictionary.TryGetValue(propertyName, out obj) && obj is T)
			{
				return (T)((object)obj);
			}
			return defaultValue;
		}

		// Token: 0x040000D8 RID: 216
		public string TaskName;

		// Token: 0x040000D9 RID: 217
		public string Operation;

		// Token: 0x040000DA RID: 218
		public int Id;

		// Token: 0x040000DB RID: 219
		public string Type;

		// Token: 0x040000DC RID: 220
		public int RelativeTimestamp;

		// Token: 0x040000DD RID: 221
		public int Count;

		// Token: 0x040000DE RID: 222
		public Dictionary<string, object> Properties;
	}
}
