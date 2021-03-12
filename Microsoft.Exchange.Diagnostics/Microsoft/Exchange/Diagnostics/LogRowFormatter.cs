using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001C4 RID: 452
	public class LogRowFormatter
	{
		// Token: 0x06000C95 RID: 3221 RVA: 0x0002E9C6 File Offset: 0x0002CBC6
		public LogRowFormatter(LogSchema schema) : this(schema, LogRowFormatter.DefaultEscapeLineBreaks)
		{
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0002E9D4 File Offset: 0x0002CBD4
		public LogRowFormatter(LogSchema schema, bool escapeLineBreaks) : this(schema, escapeLineBreaks, true)
		{
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0002E9DF File Offset: 0x0002CBDF
		public LogRowFormatter(LogSchema schema, bool escapeLineBreaks, bool escapeRawData)
		{
			this.fields = new object[schema.Fields.Length];
			this.encodedFields = new byte[schema.Fields.Length][];
			this.escapeLineBreaks = escapeLineBreaks;
			this.escapeRawData = escapeRawData;
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0002EA1C File Offset: 0x0002CC1C
		public LogRowFormatter(LogRowFormatter copy)
		{
			this.fields = new object[copy.fields.Length];
			this.encodedFields = new byte[copy.fields.Length][];
			this.escapeLineBreaks = copy.escapeLineBreaks;
			this.escapeRawData = copy.escapeRawData;
			for (int i = 0; i < this.fields.Length; i++)
			{
				this.fields[i] = copy.fields[i];
				this.encodedFields[i] = copy.encodedFields[i];
			}
		}

		// Token: 0x17000294 RID: 660
		public object this[int index]
		{
			get
			{
				return this.fields[index];
			}
			set
			{
				this.fields[index] = value;
				this.encodedFields[index] = this.Encode(value);
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0002EAC4 File Offset: 0x0002CCC4
		public virtual bool ShouldConvertEncoding
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0002EAC8 File Offset: 0x0002CCC8
		public static string FormatCollection(IEnumerable data)
		{
			bool flag;
			return LogRowFormatter.FormatCollection(data, out flag);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0002EADD File Offset: 0x0002CCDD
		public static string FormatCollection(IEnumerable data, out bool needsEscaping)
		{
			return LogRowFormatter.FormatCollection(data, LogRowFormatter.DefaultEscapeLineBreaks, out needsEscaping);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0002EAEC File Offset: 0x0002CCEC
		public static string FormatCollection(IEnumerable data, bool escapeLineBreaks, out bool needsEscaping)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			needsEscaping = false;
			foreach (object data2 in data)
			{
				bool flag;
				string text = LogRowFormatter.Format(data2, out flag);
				if (flag)
				{
					needsEscaping = true;
					Utf8Csv.EscapeAndAppendCollectionMember(stringBuilder, text, escapeLineBreaks);
				}
				else
				{
					Utf8Csv.AppendCollectionMember(stringBuilder, text);
				}
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0002EB88 File Offset: 0x0002CD88
		public static string Format(object data)
		{
			bool flag;
			return LogRowFormatter.Format(data, out flag);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0002EB9D File Offset: 0x0002CD9D
		internal void Write(Stream output)
		{
			Utf8Csv.WriteRawRow(output, this.encodedFields);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0002EBAC File Offset: 0x0002CDAC
		protected virtual byte[] Encode(object data)
		{
			if (data == null)
			{
				return null;
			}
			if (data is byte[])
			{
				return this.EncodeBytes((byte[])data);
			}
			if (!(data is string) && data is IEnumerable)
			{
				return this.EncodeCollection((IEnumerable)data);
			}
			bool flag;
			string s = LogRowFormatter.Format(data, out flag);
			if (!flag)
			{
				return Utf8Csv.Encode(s);
			}
			return Utf8Csv.EncodeAndEscape(s, this.escapeLineBreaks);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0002EC10 File Offset: 0x0002CE10
		protected virtual byte[] EncodeBytes(byte[] data)
		{
			if (this.escapeRawData)
			{
				byte[] data2 = data;
				if (this.ShouldConvertEncoding)
				{
					data2 = Encoding.Convert(Encoding.GetEncoding("iso-8859-1"), Encoding.UTF8, data);
				}
				return Utf8Csv.Escape(data2, true);
			}
			return data;
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0002EC50 File Offset: 0x0002CE50
		protected virtual byte[] EncodeCollection(IEnumerable data)
		{
			bool flag;
			string s = LogRowFormatter.FormatCollection(data, this.escapeLineBreaks, out flag);
			if (!flag)
			{
				return Utf8Csv.Encode(s);
			}
			return Utf8Csv.EncodeAndEscape(s, this.escapeLineBreaks);
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0002EC84 File Offset: 0x0002CE84
		private static string Format(object data, out bool needsEscaping)
		{
			if (data == null)
			{
				needsEscaping = false;
				return string.Empty;
			}
			needsEscaping = true;
			string result;
			if (data is Guid)
			{
				needsEscaping = false;
				result = ((Guid)data).ToString();
			}
			else if (data is DateTime)
			{
				needsEscaping = false;
				result = ((DateTime)data).ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffZ", DateTimeFormatInfo.InvariantInfo);
			}
			else if (data is int)
			{
				result = ((int)data).ToString(NumberFormatInfo.InvariantInfo);
			}
			else if (data is KeyValuePair<string, object>)
			{
				result = LogRowFormatter.EncodeKeyValuePair((KeyValuePair<string, object>)data);
			}
			else if (data is float)
			{
				result = ((float)data).ToString(NumberFormatInfo.InvariantInfo);
			}
			else if (data is double)
			{
				result = ((double)data).ToString(NumberFormatInfo.InvariantInfo);
			}
			else
			{
				result = data.ToString();
			}
			return result;
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0002ED68 File Offset: 0x0002CF68
		private static string EncodeKeyValuePair(KeyValuePair<string, object> keyValuePair)
		{
			string key = keyValuePair.Key;
			object value = keyValuePair.Value;
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("keyValuePair", "Key cannot be null/empty");
			}
			if (!SpecialCharacters.IsValidKey(key))
			{
				throw new ArgumentException("Invalid key in KeyValuePair", key);
			}
			string arg;
			string arg2;
			if (value is DateTime)
			{
				arg = "Dt";
				arg2 = ((DateTime)value).ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffZ", DateTimeFormatInfo.InvariantInfo);
			}
			else if (value is int)
			{
				arg = "I32";
				arg2 = ((int)value).ToString(NumberFormatInfo.InvariantInfo);
			}
			else if (value is string)
			{
				arg = "S";
				arg2 = (string)value;
			}
			else if (value is Guid)
			{
				arg = "S";
				arg2 = ((Guid)value).ToString();
			}
			else if (value is float)
			{
				arg = "F";
				arg2 = ((float)value).ToString(NumberFormatInfo.InvariantInfo);
			}
			else if (value is double)
			{
				arg = "Dbl";
				arg2 = ((double)value).ToString(NumberFormatInfo.InvariantInfo);
			}
			else
			{
				arg = "S";
				arg2 = "[LogRowFormatter:EncodeKeyValuePair Invalid Data Type for value]";
			}
			return string.Format("{0}:{1}={2}", arg, keyValuePair.Key, arg2);
		}

		// Token: 0x04000968 RID: 2408
		public static readonly bool DefaultEscapeLineBreaks = true;

		// Token: 0x04000969 RID: 2409
		private readonly object[] fields;

		// Token: 0x0400096A RID: 2410
		private readonly byte[][] encodedFields;

		// Token: 0x0400096B RID: 2411
		private readonly bool escapeLineBreaks;

		// Token: 0x0400096C RID: 2412
		private readonly bool escapeRawData;
	}
}
