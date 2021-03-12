using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000791 RID: 1937
	[Serializable]
	public class VirusMessageDeletedException : VirusException
	{
		// Token: 0x06004909 RID: 18697 RVA: 0x00131B5B File Offset: 0x0012FD5B
		public VirusMessageDeletedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600490A RID: 18698 RVA: 0x00131B64 File Offset: 0x0012FD64
		public VirusMessageDeletedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600490B RID: 18699 RVA: 0x00131B6E File Offset: 0x0012FD6E
		protected VirusMessageDeletedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
