using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007BB RID: 1979
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapBase64Binary : ISoapXsd
	{
		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x0600565C RID: 22108 RVA: 0x0013147C File Offset: 0x0012F67C
		public static string XsdType
		{
			get
			{
				return "base64Binary";
			}
		}

		// Token: 0x0600565D RID: 22109 RVA: 0x00131483 File Offset: 0x0012F683
		public string GetXsdType()
		{
			return SoapBase64Binary.XsdType;
		}

		// Token: 0x0600565E RID: 22110 RVA: 0x0013148A File Offset: 0x0012F68A
		public SoapBase64Binary()
		{
		}

		// Token: 0x0600565F RID: 22111 RVA: 0x00131492 File Offset: 0x0012F692
		public SoapBase64Binary(byte[] value)
		{
			this._value = value;
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06005660 RID: 22112 RVA: 0x001314A1 File Offset: 0x0012F6A1
		// (set) Token: 0x06005661 RID: 22113 RVA: 0x001314A9 File Offset: 0x0012F6A9
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

		// Token: 0x06005662 RID: 22114 RVA: 0x001314B2 File Offset: 0x0012F6B2
		public override string ToString()
		{
			if (this._value == null)
			{
				return null;
			}
			return SoapType.LineFeedsBin64(Convert.ToBase64String(this._value));
		}

		// Token: 0x06005663 RID: 22115 RVA: 0x001314D0 File Offset: 0x0012F6D0
		public static SoapBase64Binary Parse(string value)
		{
			if (value == null || value.Length == 0)
			{
				return new SoapBase64Binary(new byte[0]);
			}
			byte[] value2;
			try
			{
				value2 = Convert.FromBase64String(SoapType.FilterBin64(value));
			}
			catch (Exception)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "base64Binary", value));
			}
			return new SoapBase64Binary(value2);
		}

		// Token: 0x0400275E RID: 10078
		private byte[] _value;
	}
}
