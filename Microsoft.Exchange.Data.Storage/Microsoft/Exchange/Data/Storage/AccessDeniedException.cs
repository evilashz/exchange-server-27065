using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000711 RID: 1809
	[Serializable]
	public class AccessDeniedException : StoragePermanentException
	{
		// Token: 0x06004783 RID: 18307 RVA: 0x0013017D File Offset: 0x0012E37D
		public AccessDeniedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06004784 RID: 18308 RVA: 0x00130186 File Offset: 0x0012E386
		public AccessDeniedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004785 RID: 18309 RVA: 0x00130190 File Offset: 0x0012E390
		protected AccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
