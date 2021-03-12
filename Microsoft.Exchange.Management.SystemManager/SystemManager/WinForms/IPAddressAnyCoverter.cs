using System;
using System.Net;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200017F RID: 383
	internal class IPAddressAnyCoverter : TextConverter
	{
		// Token: 0x06000F35 RID: 3893 RVA: 0x0003AEFD File Offset: 0x000390FD
		protected override object ParseObject(string s, IFormatProvider provider)
		{
			if (IPAddress.Any.ToString().Equals(s))
			{
				return IPAddress.Any;
			}
			return base.ParseObject(s, provider);
		}
	}
}
