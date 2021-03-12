using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x020000CD RID: 205
	public static class VisibilityHelper
	{
		// Token: 0x0600096A RID: 2410 RVA: 0x0001D8E4 File Offset: 0x0001BAE4
		public static Visibility Select(IEnumerable<Visibility> visibilities)
		{
			Visibility visibility = Visibility.Public;
			foreach (Visibility v in visibilities)
			{
				visibility = VisibilityHelper.Select(visibility, v);
				if (visibility == Visibility.Private)
				{
					return visibility;
				}
			}
			return visibility;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0001D93C File Offset: 0x0001BB3C
		public static Visibility Select(Visibility v1, Visibility v2)
		{
			if (v1 == Visibility.Private || v2 == Visibility.Private)
			{
				return Visibility.Private;
			}
			if (v1 == Visibility.Redacted || v2 == Visibility.Redacted)
			{
				return Visibility.Redacted;
			}
			if (v1 == Visibility.Public || v2 == Visibility.Public)
			{
				return Visibility.Public;
			}
			return Visibility.Redacted;
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0001D95C File Offset: 0x0001BB5C
		public static string GetPrefix(Visibility v)
		{
			switch (v)
			{
			case Visibility.Public:
				return "PUBLIC";
			case Visibility.Redacted:
				return "REDACTED";
			case Visibility.Private:
				return "PRIVATE";
			default:
				return "UNKNOWN";
			}
		}
	}
}
