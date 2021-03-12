using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020008CB RID: 2251
	internal static class AffectedTaskOccurrences
	{
		// Token: 0x06003FB0 RID: 16304 RVA: 0x000DC290 File Offset: 0x000DA490
		public static AffectedTaskOccurrencesType ConvertToEnum(string affectedTaskOccurencesValue)
		{
			AffectedTaskOccurrencesType result = AffectedTaskOccurrencesType.AllOccurrences;
			if (affectedTaskOccurencesValue != null)
			{
				if (!(affectedTaskOccurencesValue == "AllOccurrences"))
				{
					if (affectedTaskOccurencesValue == "SpecifiedOccurrenceOnly")
					{
						result = AffectedTaskOccurrencesType.SpecifiedOccurrenceOnly;
					}
				}
				else
				{
					result = AffectedTaskOccurrencesType.AllOccurrences;
				}
			}
			return result;
		}

		// Token: 0x0400246B RID: 9323
		public const string AllOccurrences = "AllOccurrences";

		// Token: 0x0400246C RID: 9324
		public const string SpecifiedOccurrenceOnly = "SpecifiedOccurrenceOnly";
	}
}
