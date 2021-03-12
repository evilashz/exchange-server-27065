using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Clutter
{
	// Token: 0x0200043D RID: 1085
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class IServerModelConfigurationExtensions
	{
		// Token: 0x06003077 RID: 12407 RVA: 0x000C7225 File Offset: 0x000C5425
		public static IEnumerable<int> GetAllModelVersions(this IServerModelConfiguration serverModelConfig)
		{
			return new List<int>(Enumerable.Range(serverModelConfig.MinModelVersion, serverModelConfig.MaxModelVersion - serverModelConfig.MinModelVersion + 1));
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x000C7246 File Offset: 0x000C5446
		public static IEnumerable<int> GetSupportedModelVersions(this IServerModelConfiguration serverModelConfig)
		{
			if (serverModelConfig.BlockedModelVersions == null)
			{
				return serverModelConfig.GetAllModelVersions();
			}
			return serverModelConfig.GetAllModelVersions().Except(serverModelConfig.BlockedModelVersions);
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x000C7268 File Offset: 0x000C5468
		public static int GetLatestSupportedModelVersion(this IServerModelConfiguration serverModelConfig)
		{
			return serverModelConfig.GetSupportedModelVersions().Max();
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x000C7278 File Offset: 0x000C5478
		public static IEnumerable<int> GetSupportedClassificationModelVersions(this IServerModelConfiguration serverModelConfig)
		{
			IEnumerable<int> second = serverModelConfig.ClassificationModelVersions ?? Enumerable.Empty<int>();
			return serverModelConfig.GetSupportedModelVersions().Intersect(second);
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x000C72A1 File Offset: 0x000C54A1
		public static int GetLatestSupportedClassificationModelVersion(this IServerModelConfiguration serverModelConfig)
		{
			return serverModelConfig.GetSupportedClassificationModelVersions().Max();
		}
	}
}
