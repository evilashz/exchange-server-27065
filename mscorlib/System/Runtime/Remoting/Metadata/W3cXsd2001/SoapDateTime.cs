using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007B1 RID: 1969
	[ComVisible(true)]
	public sealed class SoapDateTime
	{
		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x06005600 RID: 22016 RVA: 0x001305EB File Offset: 0x0012E7EB
		public static string XsdType
		{
			get
			{
				return "dateTime";
			}
		}

		// Token: 0x06005601 RID: 22017 RVA: 0x001305F2 File Offset: 0x0012E7F2
		public static string ToString(DateTime value)
		{
			return value.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffffzzz", CultureInfo.InvariantCulture);
		}

		// Token: 0x06005602 RID: 22018 RVA: 0x00130608 File Offset: 0x0012E808
		public static DateTime Parse(string value)
		{
			DateTime result;
			try
			{
				if (value == null)
				{
					result = DateTime.MinValue;
				}
				else
				{
					string s = value;
					if (value.EndsWith("Z", StringComparison.Ordinal))
					{
						s = value.Substring(0, value.Length - 1) + "-00:00";
					}
					result = DateTime.ParseExact(s, SoapDateTime.formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
				}
			}
			catch (Exception)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:dateTime", value));
			}
			return result;
		}

		// Token: 0x0400274A RID: 10058
		private static string[] formats = new string[]
		{
			"yyyy-MM-dd'T'HH:mm:ss.fffffffzzz",
			"yyyy-MM-dd'T'HH:mm:ss.ffff",
			"yyyy-MM-dd'T'HH:mm:ss.ffffzzz",
			"yyyy-MM-dd'T'HH:mm:ss.fff",
			"yyyy-MM-dd'T'HH:mm:ss.fffzzz",
			"yyyy-MM-dd'T'HH:mm:ss.ff",
			"yyyy-MM-dd'T'HH:mm:ss.ffzzz",
			"yyyy-MM-dd'T'HH:mm:ss.f",
			"yyyy-MM-dd'T'HH:mm:ss.fzzz",
			"yyyy-MM-dd'T'HH:mm:ss",
			"yyyy-MM-dd'T'HH:mm:sszzz",
			"yyyy-MM-dd'T'HH:mm:ss.fffff",
			"yyyy-MM-dd'T'HH:mm:ss.fffffzzz",
			"yyyy-MM-dd'T'HH:mm:ss.ffffff",
			"yyyy-MM-dd'T'HH:mm:ss.ffffffzzz",
			"yyyy-MM-dd'T'HH:mm:ss.fffffff",
			"yyyy-MM-dd'T'HH:mm:ss.ffffffff",
			"yyyy-MM-dd'T'HH:mm:ss.ffffffffzzz",
			"yyyy-MM-dd'T'HH:mm:ss.fffffffff",
			"yyyy-MM-dd'T'HH:mm:ss.fffffffffzzz",
			"yyyy-MM-dd'T'HH:mm:ss.ffffffffff",
			"yyyy-MM-dd'T'HH:mm:ss.ffffffffffzzz"
		};
	}
}
