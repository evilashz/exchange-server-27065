using System;
using System.Globalization;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001EF RID: 495
	internal class IntConverter : BaseConverter
	{
		// Token: 0x06000D0C RID: 3340 RVA: 0x0004294C File Offset: 0x00040B4C
		public static int Parse(string propertyString)
		{
			int result;
			try
			{
				result = int.Parse(propertyString, CultureInfo.InvariantCulture);
			}
			catch (OverflowException ex)
			{
				ex.Data["NeverGenerateWatson"] = null;
				throw;
			}
			return result;
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0004298C File Offset: 0x00040B8C
		public static string ToString(int propertyValue)
		{
			return propertyValue.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0004299A File Offset: 0x00040B9A
		public override object ConvertToObject(string propertyString)
		{
			return IntConverter.Parse(propertyString);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x000429A7 File Offset: 0x00040BA7
		public override string ConvertToString(object propertyValue)
		{
			return IntConverter.ToString((int)propertyValue);
		}
	}
}
