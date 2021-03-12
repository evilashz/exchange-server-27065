using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007B4 RID: 1972
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapDate : ISoapXsd
	{
		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06005613 RID: 22035 RVA: 0x00130D46 File Offset: 0x0012EF46
		public static string XsdType
		{
			get
			{
				return "date";
			}
		}

		// Token: 0x06005614 RID: 22036 RVA: 0x00130D4D File Offset: 0x0012EF4D
		public string GetXsdType()
		{
			return SoapDate.XsdType;
		}

		// Token: 0x06005615 RID: 22037 RVA: 0x00130D54 File Offset: 0x0012EF54
		public SoapDate()
		{
		}

		// Token: 0x06005616 RID: 22038 RVA: 0x00130D7C File Offset: 0x0012EF7C
		public SoapDate(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x06005617 RID: 22039 RVA: 0x00130DAC File Offset: 0x0012EFAC
		public SoapDate(DateTime value, int sign)
		{
			this._value = value;
			this._sign = sign;
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06005618 RID: 22040 RVA: 0x00130DE0 File Offset: 0x0012EFE0
		// (set) Token: 0x06005619 RID: 22041 RVA: 0x00130DE8 File Offset: 0x0012EFE8
		public DateTime Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value.Date;
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x0600561A RID: 22042 RVA: 0x00130DF7 File Offset: 0x0012EFF7
		// (set) Token: 0x0600561B RID: 22043 RVA: 0x00130DFF File Offset: 0x0012EFFF
		public int Sign
		{
			get
			{
				return this._sign;
			}
			set
			{
				this._sign = value;
			}
		}

		// Token: 0x0600561C RID: 22044 RVA: 0x00130E08 File Offset: 0x0012F008
		public override string ToString()
		{
			if (this._sign < 0)
			{
				return this._value.ToString("'-'yyyy-MM-dd", CultureInfo.InvariantCulture);
			}
			return this._value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x0600561D RID: 22045 RVA: 0x00130E40 File Offset: 0x0012F040
		public static SoapDate Parse(string value)
		{
			int sign = 0;
			if (value[0] == '-')
			{
				sign = -1;
			}
			return new SoapDate(DateTime.ParseExact(value, SoapDate.formats, CultureInfo.InvariantCulture, DateTimeStyles.None), sign);
		}

		// Token: 0x0400274D RID: 10061
		private DateTime _value = DateTime.MinValue.Date;

		// Token: 0x0400274E RID: 10062
		private int _sign;

		// Token: 0x0400274F RID: 10063
		private static string[] formats = new string[]
		{
			"yyyy-MM-dd",
			"'+'yyyy-MM-dd",
			"'-'yyyy-MM-dd",
			"yyyy-MM-ddzzz",
			"'+'yyyy-MM-ddzzz",
			"'-'yyyy-MM-ddzzz"
		};
	}
}
