using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000FD RID: 253
	[Serializable]
	public sealed class EsentLogSequenceEndDatabasesConsistentException : EsentFragmentationException
	{
		// Token: 0x0600080D RID: 2061 RVA: 0x00011C3E File Offset: 0x0000FE3E
		public EsentLogSequenceEndDatabasesConsistentException() : base("databases have been recovered, but all possible log generations in the current sequence are used; delete all log files and the checkpoint file and backup the databases before continuing", JET_err.LogSequenceEndDatabasesConsistent)
		{
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00011C50 File Offset: 0x0000FE50
		private EsentLogSequenceEndDatabasesConsistentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
