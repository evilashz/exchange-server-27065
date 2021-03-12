using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007B9 RID: 1977
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapMonth : ISoapXsd
	{
		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06005649 RID: 22089 RVA: 0x00131228 File Offset: 0x0012F428
		public static string XsdType
		{
			get
			{
				return "gMonth";
			}
		}

		// Token: 0x0600564A RID: 22090 RVA: 0x0013122F File Offset: 0x0012F42F
		public string GetXsdType()
		{
			return SoapMonth.XsdType;
		}

		// Token: 0x0600564B RID: 22091 RVA: 0x00131236 File Offset: 0x0012F436
		public SoapMonth()
		{
		}

		// Token: 0x0600564C RID: 22092 RVA: 0x00131249 File Offset: 0x0012F449
		public SoapMonth(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x0600564D RID: 22093 RVA: 0x00131263 File Offset: 0x0012F463
		// (set) Token: 0x0600564E RID: 22094 RVA: 0x0013126B File Offset: 0x0012F46B
		public DateTime Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x0600564F RID: 22095 RVA: 0x00131274 File Offset: 0x0012F474
		public override string ToString()
		{
			return this._value.ToString("--MM--", CultureInfo.InvariantCulture);
		}

		// Token: 0x06005650 RID: 22096 RVA: 0x0013128B File Offset: 0x0012F48B
		public static SoapMonth Parse(string value)
		{
			return new SoapMonth(DateTime.ParseExact(value, SoapMonth.formats, CultureInfo.InvariantCulture, DateTimeStyles.None));
		}

		// Token: 0x0400275A RID: 10074
		private DateTime _value = DateTime.MinValue;

		// Token: 0x0400275B RID: 10075
		private static string[] formats = new string[]
		{
			"--MM--",
			"--MM--zzz"
		};
	}
}
