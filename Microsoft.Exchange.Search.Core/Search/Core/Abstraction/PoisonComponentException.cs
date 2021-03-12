using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x0200000E RID: 14
	[Serializable]
	internal class PoisonComponentException : ComponentFailedException
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002508 File Offset: 0x00000708
		public PoisonComponentException() : base(Strings.OperationFailure)
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002515 File Offset: 0x00000715
		public PoisonComponentException(Exception innerException) : base(Strings.OperationFailure, innerException)
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002523 File Offset: 0x00000723
		public PoisonComponentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000252C File Offset: 0x0000072C
		public PoisonComponentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002536 File Offset: 0x00000736
		protected PoisonComponentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002540 File Offset: 0x00000740
		internal override void RethrowNewInstance()
		{
			throw new ComponentFailedPermanentException(this);
		}
	}
}
