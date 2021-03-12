using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000753 RID: 1875
	[Serializable]
	public class NonUniqueLegacyExchangeDNException : StoragePermanentException
	{
		// Token: 0x0600484B RID: 18507 RVA: 0x00130D7F File Offset: 0x0012EF7F
		public NonUniqueLegacyExchangeDNException(LocalizedString message) : base(message)
		{
		}
	}
}
