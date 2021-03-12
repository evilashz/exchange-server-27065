using System;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A48 RID: 2632
	internal sealed class SanitizingTextWriter<SanitizingPolicy> : TextWriter where SanitizingPolicy : ISanitizingPolicy, new()
	{
		// Token: 0x06003999 RID: 14745 RVA: 0x00092068 File Offset: 0x00090268
		public SanitizingTextWriter(TextWriter writer, bool takeOwnership)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.writer = writer;
			this.ownWriter = takeOwnership;
		}

		// Token: 0x0600399A RID: 14746 RVA: 0x0009208C File Offset: 0x0009028C
		public SanitizingTextWriter(TextWriter writer) : this(writer, false)
		{
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x0600399B RID: 14747 RVA: 0x00092096 File Offset: 0x00090296
		public override IFormatProvider FormatProvider
		{
			get
			{
				return this.writer.FormatProvider;
			}
		}

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x0600399C RID: 14748 RVA: 0x000920A3 File Offset: 0x000902A3
		public override Encoding Encoding
		{
			get
			{
				return this.writer.Encoding;
			}
		}

		// Token: 0x0600399D RID: 14749 RVA: 0x000920B0 File Offset: 0x000902B0
		public override void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x0600399E RID: 14750 RVA: 0x000920BD File Offset: 0x000902BD
		public override void Write(char value)
		{
			this.writer.Write(value);
		}

		// Token: 0x0600399F RID: 14751 RVA: 0x000920CB File Offset: 0x000902CB
		public override void Write(string value)
		{
			if (value == null)
			{
				value = string.Empty;
			}
			if (StringSanitizer<SanitizingPolicy>.IsTrustedString(value))
			{
				this.writer.Write(value);
				return;
			}
			StringSanitizer<SanitizingPolicy>.Sanitize(this.writer, value);
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x000920F8 File Offset: 0x000902F8
		public override void Write(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value is SanitizingStringBuilder<SanitizingPolicy>)
			{
				this.writer.Write(value.ToString());
				return;
			}
			ISanitizedString<SanitizingPolicy> sanitizedString = value as ISanitizedString<SanitizingPolicy>;
			if (sanitizedString != null)
			{
				this.writer.Write(sanitizedString.ToString());
				return;
			}
			this.Write(value.ToString());
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x00092155 File Offset: 0x00090355
		public override void WriteLine(string value)
		{
			this.Write(value);
			this.writer.WriteLine();
		}

		// Token: 0x060039A2 RID: 14754 RVA: 0x00092169 File Offset: 0x00090369
		public override void WriteLine(object value)
		{
			this.Write(value);
			this.writer.WriteLine();
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x0009217D File Offset: 0x0009037D
		public override void Write(string format, params object[] args)
		{
			this.WriteCommon(format, args);
		}

		// Token: 0x060039A4 RID: 14756 RVA: 0x00092188 File Offset: 0x00090388
		public override void Write(string format, object arg0)
		{
			this.WriteCommon(format, new object[]
			{
				arg0
			});
		}

		// Token: 0x060039A5 RID: 14757 RVA: 0x000921A8 File Offset: 0x000903A8
		public override void Write(string format, object arg0, object arg1)
		{
			this.WriteCommon(format, new object[]
			{
				arg0,
				arg1
			});
		}

		// Token: 0x060039A6 RID: 14758 RVA: 0x000921CC File Offset: 0x000903CC
		public override void Write(string format, object arg0, object arg1, object arg2)
		{
			this.WriteCommon(format, new object[]
			{
				arg0,
				arg1,
				arg2
			});
		}

		// Token: 0x060039A7 RID: 14759 RVA: 0x000921F5 File Offset: 0x000903F5
		public override void WriteLine(string format, params object[] args)
		{
			this.WriteLineCommon(format, args);
		}

		// Token: 0x060039A8 RID: 14760 RVA: 0x00092200 File Offset: 0x00090400
		public override void WriteLine(string format, object arg0)
		{
			this.WriteLineCommon(format, new object[]
			{
				arg0
			});
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x00092220 File Offset: 0x00090420
		public override void WriteLine(string format, object arg0, object arg1)
		{
			this.WriteLineCommon(format, new object[]
			{
				arg0,
				arg1
			});
		}

		// Token: 0x060039AA RID: 14762 RVA: 0x00092244 File Offset: 0x00090444
		public override void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			this.WriteLineCommon(format, new object[]
			{
				arg0,
				arg1,
				arg2
			});
		}

		// Token: 0x060039AB RID: 14763 RVA: 0x0009226D File Offset: 0x0009046D
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && this.ownWriter)
			{
				this.writer.Dispose();
			}
		}

		// Token: 0x060039AC RID: 14764 RVA: 0x0009228C File Offset: 0x0009048C
		private void WriteCommon(string format, params object[] args)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			this.writer.Write(StringSanitizer<SanitizingPolicy>.SanitizeFormat(this.writer.FormatProvider, format, args));
		}

		// Token: 0x060039AD RID: 14765 RVA: 0x000922B9 File Offset: 0x000904B9
		private void WriteLineCommon(string format, params object[] args)
		{
			this.WriteCommon(format, args);
			this.writer.WriteLine();
		}

		// Token: 0x040030E7 RID: 12519
		private TextWriter writer;

		// Token: 0x040030E8 RID: 12520
		private bool ownWriter;
	}
}
