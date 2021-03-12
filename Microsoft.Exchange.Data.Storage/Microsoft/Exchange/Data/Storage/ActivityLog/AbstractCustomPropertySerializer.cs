using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ActivityLog
{
	// Token: 0x02000345 RID: 837
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractCustomPropertySerializer
	{
		// Token: 0x06002517 RID: 9495 RVA: 0x000958A6 File Offset: 0x00093AA6
		protected AbstractCustomPropertySerializer(int version, int minSupportedDeserializerVersion)
		{
			this.Version = version;
			this.MinSupportedDeserializerVersion = minSupportedDeserializerVersion;
		}

		// Token: 0x06002518 RID: 9496
		public abstract byte[] Serialize(IDictionary<string, string> data, out bool isTruncated);

		// Token: 0x06002519 RID: 9497
		public abstract Dictionary<string, string> Deserialize(byte[] array, out bool isTruncated);

		// Token: 0x0400168F RID: 5775
		public const int ByteLimit = 1024;

		// Token: 0x04001690 RID: 5776
		public const int HeaderLength = 3;

		// Token: 0x04001691 RID: 5777
		public const int HeaderVersionIndex = 0;

		// Token: 0x04001692 RID: 5778
		public const int HeaderMinSupportedVersionIndex = 1;

		// Token: 0x04001693 RID: 5779
		public const int HeaderFlagsIndex = 2;

		// Token: 0x04001694 RID: 5780
		public readonly int Version;

		// Token: 0x04001695 RID: 5781
		public readonly int MinSupportedDeserializerVersion;
	}
}
