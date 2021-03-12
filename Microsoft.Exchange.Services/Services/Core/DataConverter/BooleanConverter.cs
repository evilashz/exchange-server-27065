using System;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001DB RID: 475
	internal class BooleanConverter : BaseConverter
	{
		// Token: 0x06000CB7 RID: 3255 RVA: 0x00041B28 File Offset: 0x0003FD28
		public static bool Parse(string booleanString)
		{
			string a;
			if ((a = booleanString.ToLowerInvariant()) != null)
			{
				if (a == "false" || a == "0")
				{
					return false;
				}
				if (a == "true" || a == "1")
				{
					return true;
				}
			}
			throw new FormatException("Invalid property value for boolean parsing: " + booleanString);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00041B89 File Offset: 0x0003FD89
		public static string ToString(bool propertyValue)
		{
			if (!propertyValue)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00041B99 File Offset: 0x0003FD99
		public override object ConvertToObject(string propertyString)
		{
			return BooleanConverter.Parse(propertyString);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00041BA6 File Offset: 0x0003FDA6
		public override string ConvertToString(object propertyValue)
		{
			return BooleanConverter.ToString((bool)propertyValue);
		}
	}
}
