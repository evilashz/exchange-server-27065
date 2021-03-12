using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverCommon
{
	// Token: 0x02000002 RID: 2
	[Serializable]
	internal class CrashRepositoryAccessException : LocalizedException
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public CrashRepositoryAccessException(string errorDescription, Exception innerException) : base(LocalizedString.Empty, innerException)
		{
			this.ErrorDescription = errorDescription;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020E5 File Offset: 0x000002E5
		public CrashRepositoryAccessException(Exception innerException) : base(LocalizedString.Empty, innerException)
		{
			this.ErrorDescription = string.Empty;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020FE File Offset: 0x000002FE
		protected CrashRepositoryAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ErrorDescription = string.Empty;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002113 File Offset: 0x00000313
		// (set) Token: 0x06000005 RID: 5 RVA: 0x0000211B File Offset: 0x0000031B
		public string ErrorDescription { get; private set; }
	}
}
