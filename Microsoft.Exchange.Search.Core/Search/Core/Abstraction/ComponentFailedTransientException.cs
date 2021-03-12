using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	internal class ComponentFailedTransientException : ComponentFailedException
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002395 File Offset: 0x00000595
		public ComponentFailedTransientException() : base(Strings.ComponentFailure)
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000023A2 File Offset: 0x000005A2
		public ComponentFailedTransientException(Exception innerException) : base(Strings.ComponentFailure, innerException)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000023B0 File Offset: 0x000005B0
		public ComponentFailedTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000023B9 File Offset: 0x000005B9
		public ComponentFailedTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000023C3 File Offset: 0x000005C3
		protected ComponentFailedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000023CD File Offset: 0x000005CD
		protected ComponentFailedTransientException(ComponentFailedTransientException other) : base(other)
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000023D6 File Offset: 0x000005D6
		internal override void RethrowNewInstance()
		{
			throw new ComponentFailedTransientException(this);
		}
	}
}
