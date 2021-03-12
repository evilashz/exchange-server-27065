using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.ApplicationLogic.Compliance
{
	// Token: 0x020000CE RID: 206
	internal static class ExPropertyNameMapping
	{
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x00022ABC File Offset: 0x00020CBC
		internal static Dictionary<string, string> Mapping
		{
			get
			{
				if (ExPropertyNameMapping.mapping == null)
				{
					ExPropertyNameMapping.mapping = new Dictionary<string, string>();
					ExPropertyNameMapping.mapping["Item.Extension"] = "Item.Extension";
					ExPropertyNameMapping.mapping["Item.DisplayName"] = "Subject";
					ExPropertyNameMapping.mapping["Item.WhenCreated"] = "Sent";
					ExPropertyNameMapping.mapping["Item.WhenModified"] = "Item.WhenModified";
					ExPropertyNameMapping.mapping["Item.LastModifier"] = "Item.LastModifier";
					ExPropertyNameMapping.mapping["Item.Creator"] = "From";
					ExPropertyNameMapping.mapping["Item.ExpiryTime"] = "Expires";
					ExPropertyNameMapping.mapping["Item.CreationAgeInDays"] = "Received";
					ExPropertyNameMapping.mapping["Item.CreationAgeInMonths"] = "Received";
					ExPropertyNameMapping.mapping["Item.CreationAgeInYears"] = "Received";
					ExPropertyNameMapping.mapping["Item.ModificationAgeInDays"] = "Received";
					ExPropertyNameMapping.mapping["Item.ModificationAgeInMonths"] = "Received";
					ExPropertyNameMapping.mapping["Item.ModificationAgeInYears"] = "Item.Extension";
					ExPropertyNameMapping.mapping["Item.ClassificationDiscovered"] = "Item.ClassificationDiscovered";
					ExPropertyNameMapping.mapping["Item.Extension"] = "Item.Extension";
				}
				return ExPropertyNameMapping.mapping;
			}
		}

		// Token: 0x040003E4 RID: 996
		private const string Extension = "Item.Extension";

		// Token: 0x040003E5 RID: 997
		private const string DisplayName = "Subject";

		// Token: 0x040003E6 RID: 998
		private const string WhenCreated = "Sent";

		// Token: 0x040003E7 RID: 999
		private const string WhenModified = "Item.WhenModified";

		// Token: 0x040003E8 RID: 1000
		private const string Creator = "From";

		// Token: 0x040003E9 RID: 1001
		private const string LastModifier = "Item.LastModifier";

		// Token: 0x040003EA RID: 1002
		private const string ExpiryTime = "Expires";

		// Token: 0x040003EB RID: 1003
		private const string CreationAgeInDays = "Received";

		// Token: 0x040003EC RID: 1004
		private const string CreationAgeInMonths = "Received";

		// Token: 0x040003ED RID: 1005
		private const string CreationAgeInYears = "Received";

		// Token: 0x040003EE RID: 1006
		private const string ModificationAgeInDays = "Received";

		// Token: 0x040003EF RID: 1007
		private const string ModificationAgeInMonths = "Received";

		// Token: 0x040003F0 RID: 1008
		private const string ModificationAgeInYears = "Received";

		// Token: 0x040003F1 RID: 1009
		private const string ClassificationDiscovered = "Item.ClassificationDiscovered";

		// Token: 0x040003F2 RID: 1010
		private static Dictionary<string, string> mapping;
	}
}
