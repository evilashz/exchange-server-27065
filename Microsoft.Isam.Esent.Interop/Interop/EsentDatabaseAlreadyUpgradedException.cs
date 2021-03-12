using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000109 RID: 265
	[Serializable]
	public sealed class EsentDatabaseAlreadyUpgradedException : EsentStateException
	{
		// Token: 0x06000825 RID: 2085 RVA: 0x00011D8E File Offset: 0x0000FF8E
		public EsentDatabaseAlreadyUpgradedException() : base("Attempted to upgrade a database that is already current", JET_err.DatabaseAlreadyUpgraded)
		{
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00011DA0 File Offset: 0x0000FFA0
		private EsentDatabaseAlreadyUpgradedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
