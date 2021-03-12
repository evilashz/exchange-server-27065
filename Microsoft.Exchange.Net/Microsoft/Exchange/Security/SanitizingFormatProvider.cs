using System;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A45 RID: 2629
	internal class SanitizingFormatProvider<SanitizingPolicy> : IFormatProvider, ICustomFormatter where SanitizingPolicy : ISanitizingPolicy, new()
	{
		// Token: 0x0600396D RID: 14701 RVA: 0x00091B23 File Offset: 0x0008FD23
		public SanitizingFormatProvider(IFormatProvider innerFormatProvider)
		{
			this.innerFormatProvider = innerFormatProvider;
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x0600396E RID: 14702 RVA: 0x00091B32 File Offset: 0x0008FD32
		public IFormatProvider InnerFormatProvider
		{
			get
			{
				return this.innerFormatProvider;
			}
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x00091B3A File Offset: 0x0008FD3A
		public object GetFormat(Type formatType)
		{
			if (formatType == typeof(ICustomFormatter))
			{
				return this;
			}
			return null;
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x00091B54 File Offset: 0x0008FD54
		public string Format(string format, object arg, IFormatProvider formatProvider)
		{
			string text = arg as string;
			if (arg == null)
			{
				return string.Empty;
			}
			if (text != null && StringSanitizer<SanitizingPolicy>.IsTrustedString(text))
			{
				return text;
			}
			ISanitizedString<SanitizingPolicy> sanitizedString = arg as ISanitizedString<SanitizingPolicy>;
			if (sanitizedString != null)
			{
				return sanitizedString.ToString();
			}
			if (!(arg is string))
			{
				IFormattable formattable = arg as IFormattable;
				if (formattable == null)
				{
					text = arg.ToString();
				}
				else
				{
					text = formattable.ToString(format, this.InnerFormatProvider);
				}
			}
			return StringSanitizer<SanitizingPolicy>.Sanitize(text);
		}

		// Token: 0x040030E5 RID: 12517
		private readonly IFormatProvider innerFormatProvider;
	}
}
