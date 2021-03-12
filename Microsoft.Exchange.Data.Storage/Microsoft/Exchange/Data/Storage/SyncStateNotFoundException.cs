using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000780 RID: 1920
	[Serializable]
	public class SyncStateNotFoundException : ObjectNotFoundException
	{
		// Token: 0x060048DB RID: 18651 RVA: 0x0013190A File Offset: 0x0012FB0A
		public SyncStateNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060048DC RID: 18652 RVA: 0x00131913 File Offset: 0x0012FB13
		public SyncStateNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060048DD RID: 18653 RVA: 0x0013191D File Offset: 0x0012FB1D
		protected SyncStateNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
