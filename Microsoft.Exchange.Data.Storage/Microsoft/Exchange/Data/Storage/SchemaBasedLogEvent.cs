using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000ADF RID: 2783
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SchemaBasedLogEvent<T> : ILogEvent, IEnumerable
	{
		// Token: 0x060064F7 RID: 25847 RVA: 0x001AC9AA File Offset: 0x001AABAA
		public SchemaBasedLogEvent()
		{
			this.data = new Dictionary<string, object>(StringComparer.Ordinal);
			this.recalculateStringValue = true;
		}

		// Token: 0x17001BE7 RID: 7143
		// (get) Token: 0x060064F8 RID: 25848 RVA: 0x001AC9C9 File Offset: 0x001AABC9
		public string EventId
		{
			get
			{
				return SchemaBasedLogEvent<T>.eventId;
			}
		}

		// Token: 0x060064F9 RID: 25849 RVA: 0x001AC9D0 File Offset: 0x001AABD0
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060064FA RID: 25850 RVA: 0x001AC9D7 File Offset: 0x001AABD7
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			return this.data;
		}

		// Token: 0x060064FB RID: 25851 RVA: 0x001AC9DF File Offset: 0x001AABDF
		public void Add(KeyValuePair<T, object> entry)
		{
			this.Add(entry.Key, entry.Value);
		}

		// Token: 0x060064FC RID: 25852 RVA: 0x001AC9F8 File Offset: 0x001AABF8
		public void Add(T fieldId, object value)
		{
			string valueString = ContactLinkingStrings.GetValueString(value);
			if (valueString != null)
			{
				string fieldIdString = SchemaBasedLogEvent<T>.GetFieldIdString(fieldId);
				this.data[fieldIdString] = valueString;
				this.recalculateStringValue = true;
			}
		}

		// Token: 0x060064FD RID: 25853 RVA: 0x001ACA2C File Offset: 0x001AAC2C
		public override string ToString()
		{
			if (this.recalculateStringValue)
			{
				bool flag;
				string text = LogRowFormatter.FormatCollection(this.GetEventData(), true, out flag);
				this.stringValue = string.Format("S:EventId:{0};{1}", this.EventId, flag ? SchemaBasedLogEvent<T>.GetCsvEscapedString(text) : text);
				this.recalculateStringValue = false;
			}
			return this.stringValue;
		}

		// Token: 0x060064FE RID: 25854 RVA: 0x001ACA80 File Offset: 0x001AAC80
		private static string GetFieldIdString(T fieldId)
		{
			if (SchemaBasedLogEvent<T>.fieldIdStrings == null)
			{
				T[] array = (T[])Enum.GetValues(typeof(T));
				Dictionary<T, string> dictionary = new Dictionary<T, string>(array.Length);
				foreach (T key in array)
				{
					dictionary.Add(key, key.ToString());
				}
				SchemaBasedLogEvent<T>.fieldIdStrings = dictionary;
			}
			return SchemaBasedLogEvent<T>.fieldIdStrings[fieldId];
		}

		// Token: 0x060064FF RID: 25855 RVA: 0x001ACAF4 File Offset: 0x001AACF4
		private static string GetCsvEscapedString(object value)
		{
			return Encoding.UTF8.GetString(Utf8Csv.EncodeAndEscape(value.ToString(), true));
		}

		// Token: 0x04003985 RID: 14725
		private static readonly string eventId = typeof(T).Name;

		// Token: 0x04003986 RID: 14726
		private readonly Dictionary<string, object> data;

		// Token: 0x04003987 RID: 14727
		private static Dictionary<T, string> fieldIdStrings;

		// Token: 0x04003988 RID: 14728
		private string stringValue;

		// Token: 0x04003989 RID: 14729
		private bool recalculateStringValue;
	}
}
