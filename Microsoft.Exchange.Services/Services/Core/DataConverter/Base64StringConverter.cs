using System;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001DA RID: 474
	internal class Base64StringConverter : BaseConverter
	{
		// Token: 0x06000CB2 RID: 3250 RVA: 0x00041AF8 File Offset: 0x0003FCF8
		public static byte[] Parse(string propertyString)
		{
			return Convert.FromBase64String(propertyString);
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00041B00 File Offset: 0x0003FD00
		public static string ToString(byte[] propertyValue)
		{
			return Convert.ToBase64String(propertyValue);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00041B08 File Offset: 0x0003FD08
		public override object ConvertToObject(string propertyString)
		{
			return Base64StringConverter.Parse(propertyString);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00041B10 File Offset: 0x0003FD10
		public override string ConvertToString(object propertyValue)
		{
			return Base64StringConverter.ToString((byte[])propertyValue);
		}
	}
}
