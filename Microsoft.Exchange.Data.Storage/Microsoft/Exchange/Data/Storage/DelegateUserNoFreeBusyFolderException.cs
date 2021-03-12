using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000728 RID: 1832
	[Serializable]
	public class DelegateUserNoFreeBusyFolderException : StoragePermanentException
	{
		// Token: 0x060047C5 RID: 18373 RVA: 0x00130506 File Offset: 0x0012E706
		public DelegateUserNoFreeBusyFolderException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060047C6 RID: 18374 RVA: 0x0013050F File Offset: 0x0012E70F
		protected DelegateUserNoFreeBusyFolderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
