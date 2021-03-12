using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200010A RID: 266
	[Serializable]
	public sealed class EsentDatabaseIncompleteUpgradeException : EsentStateException
	{
		// Token: 0x06000827 RID: 2087 RVA: 0x00011DAA File Offset: 0x0000FFAA
		public EsentDatabaseIncompleteUpgradeException() : base("Attempted to use a database which was only partially converted to the current format -- must restore from backup", JET_err.DatabaseIncompleteUpgrade)
		{
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00011DBC File Offset: 0x0000FFBC
		private EsentDatabaseIncompleteUpgradeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
