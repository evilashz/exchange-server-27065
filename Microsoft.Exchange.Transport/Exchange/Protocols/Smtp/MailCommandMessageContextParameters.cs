using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000405 RID: 1029
	internal class MailCommandMessageContextParameters
	{
		// Token: 0x06002F78 RID: 12152 RVA: 0x000BE3CE File Offset: 0x000BC5CE
		public MailCommandMessageContextParameters(string messageContextMailParameterString, Version adrc, Version eprop, Version fastIndex, List<IInboundMessageContextBlob> blobs)
		{
			this.verbatimParameters = messageContextMailParameterString;
			this.AdrcVersion = adrc;
			this.EpropVersion = eprop;
			this.FastIndexVersion = fastIndex;
			this.OrderedListOfBlobs = blobs;
		}

		// Token: 0x04001757 RID: 5975
		public readonly string verbatimParameters;

		// Token: 0x04001758 RID: 5976
		public readonly Version AdrcVersion;

		// Token: 0x04001759 RID: 5977
		public readonly Version EpropVersion;

		// Token: 0x0400175A RID: 5978
		public readonly Version FastIndexVersion;

		// Token: 0x0400175B RID: 5979
		public readonly List<IInboundMessageContextBlob> OrderedListOfBlobs = new List<IInboundMessageContextBlob>();
	}
}
