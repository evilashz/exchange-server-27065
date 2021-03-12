using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200015A RID: 346
	[Serializable]
	public sealed class EsentVersionStoreEntryTooBigException : EsentErrorException
	{
		// Token: 0x060008C7 RID: 2247 RVA: 0x0001266A File Offset: 0x0001086A
		public EsentVersionStoreEntryTooBigException() : base("Attempted to create a version store entry (RCE) larger than a version bucket", JET_err.VersionStoreEntryTooBig)
		{
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0001267C File Offset: 0x0001087C
		private EsentVersionStoreEntryTooBigException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
