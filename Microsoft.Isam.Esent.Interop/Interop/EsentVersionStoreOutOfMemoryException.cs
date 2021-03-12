using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200015C RID: 348
	[Serializable]
	public sealed class EsentVersionStoreOutOfMemoryException : EsentQuotaException
	{
		// Token: 0x060008CB RID: 2251 RVA: 0x000126A2 File Offset: 0x000108A2
		public EsentVersionStoreOutOfMemoryException() : base("Version store out of memory (cleanup already attempted)", JET_err.VersionStoreOutOfMemory)
		{
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x000126B4 File Offset: 0x000108B4
		private EsentVersionStoreOutOfMemoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
