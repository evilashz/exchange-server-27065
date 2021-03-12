using System;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A47 RID: 2631
	internal class SanitizingStringWriter<SanitizingPolicy> : StringWriter where SanitizingPolicy : ISanitizingPolicy, new()
	{
		// Token: 0x06003986 RID: 14726 RVA: 0x00091E4C File Offset: 0x0009004C
		public SanitizingStringWriter()
		{
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x00091E54 File Offset: 0x00090054
		public SanitizingStringWriter(IFormatProvider formatProvider) : base(formatProvider)
		{
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x00091E5D File Offset: 0x0009005D
		public SanitizingStringWriter(StringBuilder builder) : base(builder)
		{
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x00091E66 File Offset: 0x00090066
		public SanitizingStringWriter(StringBuilder builder, IFormatProvider formatProvider) : base(builder, formatProvider)
		{
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x00091E70 File Offset: 0x00090070
		public T ToSanitizedString<T>() where T : ISanitizedString<SanitizingPolicy>, new()
		{
			T result = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
			result.UntrustedValue = base.ToString();
			result.DecreeToBeTrusted();
			return result;
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x00091EBF File Offset: 0x000900BF
		public override void Write(string value)
		{
			if (value == null)
			{
				value = string.Empty;
			}
			if (StringSanitizer<SanitizingPolicy>.IsTrustedString(value))
			{
				base.Write(value);
				return;
			}
			base.Write(StringSanitizer<SanitizingPolicy>.Sanitize(value));
		}

		// Token: 0x0600398C RID: 14732 RVA: 0x00091EE8 File Offset: 0x000900E8
		public override void Write(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			ISanitizedString<SanitizingPolicy> sanitizedString = value as ISanitizedString<SanitizingPolicy>;
			if (sanitizedString != null)
			{
				base.Write(sanitizedString.ToString());
				return;
			}
			this.Write(value.ToString());
		}

		// Token: 0x0600398D RID: 14733 RVA: 0x00091F26 File Offset: 0x00090126
		public override void Write(string format, params object[] args)
		{
			this.WriteCommon(format, args);
		}

		// Token: 0x0600398E RID: 14734 RVA: 0x00091F30 File Offset: 0x00090130
		public override void Write(string format, object arg0)
		{
			this.WriteCommon(format, new object[]
			{
				arg0
			});
		}

		// Token: 0x0600398F RID: 14735 RVA: 0x00091F50 File Offset: 0x00090150
		public override void Write(string format, object arg0, object arg1)
		{
			this.WriteCommon(format, new object[]
			{
				arg0,
				arg1
			});
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x00091F74 File Offset: 0x00090174
		public override void Write(string format, object arg0, object arg1, object arg2)
		{
			this.WriteCommon(format, new object[]
			{
				arg0,
				arg1,
				arg2
			});
		}

		// Token: 0x06003991 RID: 14737 RVA: 0x00091F9D File Offset: 0x0009019D
		public override void WriteLine(string value)
		{
			this.Write(value);
			base.WriteLine();
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x00091FAC File Offset: 0x000901AC
		public override void WriteLine(object value)
		{
			this.Write(value);
			base.WriteLine();
		}

		// Token: 0x06003993 RID: 14739 RVA: 0x00091FBB File Offset: 0x000901BB
		public override void WriteLine(string format, params object[] args)
		{
			this.WriteLineCommon(format, args);
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x00091FC8 File Offset: 0x000901C8
		public override void WriteLine(string format, object arg0)
		{
			this.WriteLineCommon(format, new object[]
			{
				arg0
			});
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x00091FE8 File Offset: 0x000901E8
		public override void WriteLine(string format, object arg0, object arg1)
		{
			this.WriteLineCommon(format, new object[]
			{
				arg0,
				arg1
			});
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x0009200C File Offset: 0x0009020C
		public override void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			this.WriteLineCommon(format, new object[]
			{
				arg0,
				arg1,
				arg2
			});
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x00092035 File Offset: 0x00090235
		private void WriteCommon(string format, params object[] args)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			base.Write(StringSanitizer<SanitizingPolicy>.SanitizeFormat(base.FormatProvider, format, args));
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x00092058 File Offset: 0x00090258
		private void WriteLineCommon(string format, params object[] args)
		{
			this.WriteCommon(format, args);
			base.WriteLine();
		}
	}
}
