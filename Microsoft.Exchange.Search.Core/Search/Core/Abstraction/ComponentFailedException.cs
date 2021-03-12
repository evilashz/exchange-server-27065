using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	internal abstract class ComponentFailedException : ComponentException
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000022AD File Offset: 0x000004AD
		protected ComponentFailedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022B6 File Offset: 0x000004B6
		protected ComponentFailedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022C0 File Offset: 0x000004C0
		protected ComponentFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022CA File Offset: 0x000004CA
		protected ComponentFailedException(ComponentFailedException other) : base(other.LocalizedString, other.InnerException)
		{
		}

		// Token: 0x06000019 RID: 25
		internal abstract void RethrowNewInstance();
	}
}
