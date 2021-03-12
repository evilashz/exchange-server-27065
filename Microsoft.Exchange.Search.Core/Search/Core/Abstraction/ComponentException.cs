using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	internal abstract class ComponentException : LocalizedException
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002290 File Offset: 0x00000490
		protected ComponentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002299 File Offset: 0x00000499
		protected ComponentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022A3 File Offset: 0x000004A3
		protected ComponentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
