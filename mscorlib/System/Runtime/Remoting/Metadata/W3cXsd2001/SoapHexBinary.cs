using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007BA RID: 1978
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapHexBinary : ISoapXsd
	{
		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06005652 RID: 22098 RVA: 0x001312C0 File Offset: 0x0012F4C0
		public static string XsdType
		{
			get
			{
				return "hexBinary";
			}
		}

		// Token: 0x06005653 RID: 22099 RVA: 0x001312C7 File Offset: 0x0012F4C7
		public string GetXsdType()
		{
			return SoapHexBinary.XsdType;
		}

		// Token: 0x06005654 RID: 22100 RVA: 0x001312CE File Offset: 0x0012F4CE
		public SoapHexBinary()
		{
		}

		// Token: 0x06005655 RID: 22101 RVA: 0x001312E3 File Offset: 0x0012F4E3
		public SoapHexBinary(byte[] value)
		{
			this._value = value;
		}

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06005656 RID: 22102 RVA: 0x001312FF File Offset: 0x0012F4FF
		// (set) Token: 0x06005657 RID: 22103 RVA: 0x00131307 File Offset: 0x0012F507
		public byte[] Value
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

		// Token: 0x06005658 RID: 22104 RVA: 0x00131310 File Offset: 0x0012F510
		public override string ToString()
		{
			this.sb.Length = 0;
			for (int i = 0; i < this._value.Length; i++)
			{
				string text = this._value[i].ToString("X", CultureInfo.InvariantCulture);
				if (text.Length == 1)
				{
					this.sb.Append('0');
				}
				this.sb.Append(text);
			}
			return this.sb.ToString();
		}

		// Token: 0x06005659 RID: 22105 RVA: 0x00131387 File Offset: 0x0012F587
		public static SoapHexBinary Parse(string value)
		{
			return new SoapHexBinary(SoapHexBinary.ToByteArray(SoapType.FilterBin64(value)));
		}

		// Token: 0x0600565A RID: 22106 RVA: 0x0013139C File Offset: 0x0012F59C
		private static byte[] ToByteArray(string value)
		{
			char[] array = value.ToCharArray();
			if (array.Length % 2 != 0)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:hexBinary", value));
			}
			byte[] array2 = new byte[array.Length / 2];
			for (int i = 0; i < array.Length / 2; i++)
			{
				array2[i] = SoapHexBinary.ToByte(array[i * 2], value) * 16 + SoapHexBinary.ToByte(array[i * 2 + 1], value);
			}
			return array2;
		}

		// Token: 0x0600565B RID: 22107 RVA: 0x00131414 File Offset: 0x0012F614
		private static byte ToByte(char c, string value)
		{
			byte result = 0;
			string s = c.ToString();
			try
			{
				s = c.ToString();
				result = byte.Parse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			}
			catch (Exception)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", new object[]
				{
					"xsd:hexBinary",
					value
				}));
			}
			return result;
		}

		// Token: 0x0400275C RID: 10076
		private byte[] _value;

		// Token: 0x0400275D RID: 10077
		private StringBuilder sb = new StringBuilder(100);
	}
}
