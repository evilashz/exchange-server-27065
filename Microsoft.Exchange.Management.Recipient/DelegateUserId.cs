using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200009E RID: 158
	[Serializable]
	public sealed class DelegateUserId : ObjectId
	{
		// Token: 0x06000A76 RID: 2678 RVA: 0x0002CE67 File Offset: 0x0002B067
		public DelegateUserId(string id)
		{
			this.identity = id;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0002CE76 File Offset: 0x0002B076
		public override byte[] GetBytes()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0002CE7D File Offset: 0x0002B07D
		public override string ToString()
		{
			return this.identity;
		}

		// Token: 0x0400022E RID: 558
		private readonly string identity;
	}
}
