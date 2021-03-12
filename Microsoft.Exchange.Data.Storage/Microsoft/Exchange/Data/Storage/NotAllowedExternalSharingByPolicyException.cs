using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000756 RID: 1878
	[Serializable]
	public class NotAllowedExternalSharingByPolicyException : StoragePermanentException
	{
		// Token: 0x06004852 RID: 18514 RVA: 0x00130DC2 File Offset: 0x0012EFC2
		public NotAllowedExternalSharingByPolicyException() : base(ServerStrings.NotAllowedExternalSharingByPolicy)
		{
		}

		// Token: 0x06004853 RID: 18515 RVA: 0x00130DCF File Offset: 0x0012EFCF
		protected NotAllowedExternalSharingByPolicyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
