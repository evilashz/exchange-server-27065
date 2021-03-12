using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Microsoft.Exchange.Data.Common
{
	// Token: 0x02000003 RID: 3
	public class ExFormatProvider : IFormatProvider, ICustomFormatter
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020FF File Offset: 0x000002FF
		public ExFormatProvider()
		{
			this.defaultFormatProvider = CultureInfo.InvariantCulture;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002114 File Offset: 0x00000314
		public static string FormatX509Certificate(X509Certificate2 x509Certificate, string format, IFormatProvider formatProvider)
		{
			StringBuilder stringBuilder = new StringBuilder(500);
			stringBuilder.Append("[Subject]" + Environment.NewLine + "  ");
			stringBuilder.Append(x509Certificate.Subject.ToString());
			stringBuilder.Append(string.Concat(new string[]
			{
				Environment.NewLine,
				Environment.NewLine,
				"[Issuer]",
				Environment.NewLine,
				"  "
			}));
			stringBuilder.Append(x509Certificate.Issuer);
			stringBuilder.Append(string.Concat(new string[]
			{
				Environment.NewLine,
				Environment.NewLine,
				"[Serial Number]",
				Environment.NewLine,
				"  "
			}));
			stringBuilder.Append(x509Certificate.SerialNumber);
			stringBuilder.Append(string.Concat(new string[]
			{
				Environment.NewLine,
				Environment.NewLine,
				"[Not Before]",
				Environment.NewLine,
				"  "
			}));
			stringBuilder.Append(x509Certificate.NotBefore.ToString(formatProvider));
			stringBuilder.Append(string.Concat(new string[]
			{
				Environment.NewLine,
				Environment.NewLine,
				"[Not After]",
				Environment.NewLine,
				"  "
			}));
			stringBuilder.Append(x509Certificate.NotAfter.ToString(formatProvider));
			stringBuilder.Append(string.Concat(new string[]
			{
				Environment.NewLine,
				Environment.NewLine,
				"[Thumbprint]",
				Environment.NewLine,
				"  "
			}));
			stringBuilder.Append(x509Certificate.GetCertHashString());
			stringBuilder.Append(Environment.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000022FA File Offset: 0x000004FA
		public object GetFormat(Type formatType)
		{
			if (formatType == typeof(ICustomFormatter))
			{
				return this;
			}
			return this.defaultFormatProvider.GetFormat(formatType);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000231C File Offset: 0x0000051C
		public string Format(string format, object arg, IFormatProvider formatProvider)
		{
			string result = string.Empty;
			X509Certificate2 x509Certificate = null;
			bool flag = false;
			if (arg is X509Certificate2)
			{
				x509Certificate = (arg as X509Certificate2);
			}
			else if (arg is X509Certificate)
			{
				try
				{
					x509Certificate = new X509Certificate2((X509Certificate)arg);
				}
				catch (CryptographicException ex)
				{
					flag = true;
					result = string.Format("Error formatting certificate: {0}", ex.Message);
				}
			}
			if (x509Certificate != null)
			{
				result = ExFormatProvider.FormatX509Certificate(x509Certificate, format, this.defaultFormatProvider);
			}
			else if (!flag)
			{
				if (arg is IFormattable)
				{
					result = ((IFormattable)arg).ToString(format, this.defaultFormatProvider);
				}
				else if (arg != null)
				{
					result = arg.ToString();
				}
				else
				{
					result = string.Empty;
				}
			}
			return result;
		}

		// Token: 0x04000005 RID: 5
		private IFormatProvider defaultFormatProvider;
	}
}
