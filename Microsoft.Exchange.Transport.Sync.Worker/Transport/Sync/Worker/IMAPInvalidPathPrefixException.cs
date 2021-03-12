using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Worker
{
	// Token: 0x02000027 RID: 39
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class IMAPInvalidPathPrefixException : Exception
	{
		// Token: 0x0600020E RID: 526 RVA: 0x000097CB File Offset: 0x000079CB
		public IMAPInvalidPathPrefixException(string message, string pathPrefix) : this(message, null, pathPrefix)
		{
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000097D6 File Offset: 0x000079D6
		public IMAPInvalidPathPrefixException(string message, Exception innerException, string pathPrefix) : base(message, innerException)
		{
			this.pathPrefix = pathPrefix;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000097E7 File Offset: 0x000079E7
		public IMAPInvalidPathPrefixException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000211 RID: 529 RVA: 0x000097F1 File Offset: 0x000079F1
		public string PathPrefix
		{
			get
			{
				return this.pathPrefix;
			}
		}

		// Token: 0x04000117 RID: 279
		private readonly string pathPrefix;
	}
}
