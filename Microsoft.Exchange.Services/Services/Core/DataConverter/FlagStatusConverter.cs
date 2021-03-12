using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001EA RID: 490
	internal class FlagStatusConverter : EnumConverter
	{
		// Token: 0x06000CF6 RID: 3318 RVA: 0x00042750 File Offset: 0x00040950
		public static string ToString(FlagStatus propertyValue)
		{
			string result = "NotFlagged";
			switch (propertyValue)
			{
			case FlagStatus.NotFlagged:
				result = "NotFlagged";
				break;
			case FlagStatus.Complete:
				result = "Complete";
				break;
			case FlagStatus.Flagged:
				result = "Flagged";
				break;
			}
			return result;
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00042790 File Offset: 0x00040990
		public override string ConvertToString(object propertyValue)
		{
			return FlagStatusConverter.ToString((FlagStatus)propertyValue);
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0004279D File Offset: 0x0004099D
		public override object ConvertToObject(string propertyString)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000A7A RID: 2682
		private const string NotFlagged = "NotFlagged";

		// Token: 0x04000A7B RID: 2683
		private const string Flagged = "Flagged";

		// Token: 0x04000A7C RID: 2684
		private const string Complete = "Complete";
	}
}
