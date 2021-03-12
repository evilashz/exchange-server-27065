using System;
using System.Globalization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200028B RID: 651
	public static class ByteQuantifiedSizeExtensions
	{
		// Token: 0x06002A62 RID: 10850 RVA: 0x00084F97 File Offset: 0x00083197
		public static string ToAppropriateUnitFormatString(this ByteQuantifiedSize size)
		{
			return size.ToAppropriateUnitFormatString("{0:0}");
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x00084FA4 File Offset: 0x000831A4
		public static string ToAppropriateUnitFormatString(this Unlimited<ByteQuantifiedSize> size)
		{
			if (size.IsUnlimited)
			{
				return OwaOptionStrings.Unlimited;
			}
			return size.Value.ToAppropriateUnitFormatString("{0:0.##}");
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x00084FCC File Offset: 0x000831CC
		public static string ToAppropriateUnitFormatString(this ByteQuantifiedSize size, string valueFormat)
		{
			if (size.ToTB() > 0UL)
			{
				return string.Format(OwaOptionStrings.MailboxUsageUnitTB, string.Format(CultureInfo.InvariantCulture, valueFormat, new object[]
				{
					size.ToBytes() / 1099511627776.0
				}));
			}
			if (size.ToGB() > 0UL)
			{
				return string.Format(OwaOptionStrings.MailboxUsageUnitGB, string.Format(CultureInfo.InvariantCulture, valueFormat, new object[]
				{
					size.ToBytes() / 1073741824.0
				}));
			}
			if (size.ToMB() > 0UL)
			{
				return string.Format(OwaOptionStrings.MailboxUsageUnitMB, string.Format(CultureInfo.InvariantCulture, valueFormat, new object[]
				{
					size.ToBytes() / 1048576.0
				}));
			}
			if (size.ToKB() > 0UL)
			{
				return string.Format(OwaOptionStrings.MailboxUsageUnitKB, string.Format(CultureInfo.InvariantCulture, valueFormat, new object[]
				{
					size.ToBytes() / 1024.0
				}));
			}
			return string.Format(CultureInfo.InvariantCulture, OwaOptionStrings.MailboxUsageUnitB, new object[]
			{
				size.ToBytes()
			});
		}
	}
}
