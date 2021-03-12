using System;
using System.Collections;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.LogUploaderProxy
{
	// Token: 0x02000019 RID: 25
	public class LogRowFormatter
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00002DFE File Offset: 0x00000FFE
		public LogRowFormatter(LogSchema schema) : this(schema, LogRowFormatter.DefaultEscapeLineBreaks)
		{
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00002E0C File Offset: 0x0000100C
		public LogRowFormatter(LogSchema schema, bool escapeLineBreaks) : this(schema, escapeLineBreaks, true)
		{
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00002E17 File Offset: 0x00001017
		public LogRowFormatter(LogSchema schema, bool escapeLineBreaks, bool escapeRawData)
		{
			this.logRowFormatterImpl = new LogRowFormatter(schema.LogSchemaImpl, escapeLineBreaks, escapeRawData);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00002E32 File Offset: 0x00001032
		public LogRowFormatter(LogRowFormatter copy)
		{
			this.logRowFormatterImpl = new LogRowFormatter(copy.logRowFormatterImpl);
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00002E4B File Offset: 0x0000104B
		internal LogRowFormatter LogRowFormatterImpl
		{
			get
			{
				return this.logRowFormatterImpl;
			}
		}

		// Token: 0x17000020 RID: 32
		public object this[int index]
		{
			get
			{
				return this.logRowFormatterImpl[index];
			}
			set
			{
				this.logRowFormatterImpl[index] = value;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00002E70 File Offset: 0x00001070
		public static string FormatCollection(IEnumerable data)
		{
			return LogRowFormatter.FormatCollection(data);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00002E78 File Offset: 0x00001078
		public static string FormatCollection(IEnumerable data, out bool needsEscaping)
		{
			return LogRowFormatter.FormatCollection(data, LogRowFormatter.DefaultEscapeLineBreaks, out needsEscaping);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00002E86 File Offset: 0x00001086
		public static string FormatCollection(IEnumerable data, bool escapeLineBreaks, out bool needsEscaping)
		{
			return LogRowFormatter.FormatCollection(data, escapeLineBreaks, out needsEscaping);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00002E90 File Offset: 0x00001090
		public static string Format(object data)
		{
			return LogRowFormatter.Format(data);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00002E98 File Offset: 0x00001098
		internal void Write(Stream output)
		{
			this.logRowFormatterImpl.Write(output);
		}

		// Token: 0x04000040 RID: 64
		public static readonly bool DefaultEscapeLineBreaks = LogRowFormatter.DefaultEscapeLineBreaks;

		// Token: 0x04000041 RID: 65
		private LogRowFormatter logRowFormatterImpl;
	}
}
