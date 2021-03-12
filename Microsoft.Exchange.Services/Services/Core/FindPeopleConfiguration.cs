using System;
using System.Configuration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002F0 RID: 752
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class FindPeopleConfiguration
	{
		// Token: 0x06001531 RID: 5425 RVA: 0x0006CA34 File Offset: 0x0006AC34
		private static int ReadFastSearchTimeoutConfiguration()
		{
			string s = ConfigurationManager.AppSettings["FindPeopleFastSearchTimeoutInMilliseconds"];
			int fastSearchTimeoutConfiguration;
			if (!int.TryParse(s, out fastSearchTimeoutConfiguration))
			{
				fastSearchTimeoutConfiguration = 30000;
			}
			return FindPeopleConfiguration.EnsureFastSearchTimeoutWithinLogicalRange(fastSearchTimeoutConfiguration);
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0006CA67 File Offset: 0x0006AC67
		private static int EnsureFastSearchTimeoutWithinLogicalRange(int fastSearchTimeoutConfiguration)
		{
			if (fastSearchTimeoutConfiguration > Global.SearchTimeoutInMilliseconds)
			{
				return Global.SearchTimeoutInMilliseconds;
			}
			if (fastSearchTimeoutConfiguration < 100)
			{
				return 100;
			}
			return fastSearchTimeoutConfiguration;
		}

		// Token: 0x04000E65 RID: 3685
		private const int DefaultFastSearchTimeoutInMilliseconds = 30000;

		// Token: 0x04000E66 RID: 3686
		private const int GraceTimeout = 100;

		// Token: 0x04000E67 RID: 3687
		public static readonly int MaxRowsDefault = 20;

		// Token: 0x04000E68 RID: 3688
		public static readonly int MaxQueryStringLength = 255;

		// Token: 0x04000E69 RID: 3689
		public static readonly LazyMember<int> FastSearchTimeoutInMilliseconds = new LazyMember<int>(new InitializeLazyMember<int>(FindPeopleConfiguration.ReadFastSearchTimeoutConfiguration));
	}
}
