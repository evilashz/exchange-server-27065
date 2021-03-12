using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001F8 RID: 504
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class HeavyBlockingOperationException : LocalizedException
	{
		// Token: 0x060010A0 RID: 4256 RVA: 0x00039161 File Offset: 0x00037361
		public HeavyBlockingOperationException() : base(Strings.HeavyBlockingOperationException)
		{
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0003916E File Offset: 0x0003736E
		public HeavyBlockingOperationException(Exception innerException) : base(Strings.HeavyBlockingOperationException, innerException)
		{
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0003917C File Offset: 0x0003737C
		protected HeavyBlockingOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x00039186 File Offset: 0x00037386
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
