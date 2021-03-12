using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200072A RID: 1834
	[Serializable]
	public class DuplicateActionException : StoragePermanentException
	{
		// Token: 0x060047CB RID: 18379 RVA: 0x00130586 File Offset: 0x0012E786
		public DuplicateActionException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060047CC RID: 18380 RVA: 0x0013058F File Offset: 0x0012E78F
		public DuplicateActionException(LocalizedString message, Exception e) : base(message, e)
		{
		}

		// Token: 0x060047CD RID: 18381 RVA: 0x00130599 File Offset: 0x0012E799
		protected DuplicateActionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
