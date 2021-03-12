using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	internal class ComponentFailedPermanentException : ComponentFailedException
	{
		// Token: 0x06000021 RID: 33 RVA: 0x0000234C File Offset: 0x0000054C
		public ComponentFailedPermanentException() : base(Strings.ComponentCriticalFailure)
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002359 File Offset: 0x00000559
		public ComponentFailedPermanentException(Exception innerException) : base(Strings.ComponentCriticalFailure, innerException)
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002367 File Offset: 0x00000567
		public ComponentFailedPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002370 File Offset: 0x00000570
		public ComponentFailedPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000237A File Offset: 0x0000057A
		protected ComponentFailedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002384 File Offset: 0x00000584
		protected ComponentFailedPermanentException(ComponentFailedPermanentException other) : base(other)
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000238D File Offset: 0x0000058D
		internal override void RethrowNewInstance()
		{
			throw new ComponentFailedPermanentException(this);
		}
	}
}
