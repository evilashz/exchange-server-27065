using System;
using System.Linq;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002CD RID: 717
	public static class HelperExtensions
	{
		// Token: 0x06002C75 RID: 11381 RVA: 0x00089460 File Offset: 0x00087660
		public static SmtpAddress[] ToSmtpAddressArray(this string smtpAddresses)
		{
			if (!string.IsNullOrEmpty(smtpAddresses))
			{
				string[] source = smtpAddresses.ToArrayOfStrings();
				return (from address in source
				select new SmtpAddress(address)).ToArray<SmtpAddress>();
			}
			return null;
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x000894B8 File Offset: 0x000876B8
		public static string ToSmtpAddressesString(this SmtpAddress[] addresses)
		{
			if (addresses != null)
			{
				return (from address in addresses
				where true
				select address.ToString()).ToArray<string>().StringArrayJoin(",");
			}
			return null;
		}
	}
}
