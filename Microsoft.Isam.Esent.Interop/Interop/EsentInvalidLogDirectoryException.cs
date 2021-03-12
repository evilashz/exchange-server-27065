using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200013E RID: 318
	[Serializable]
	public sealed class EsentInvalidLogDirectoryException : EsentObsoleteException
	{
		// Token: 0x0600088F RID: 2191 RVA: 0x0001235A File Offset: 0x0001055A
		public EsentInvalidLogDirectoryException() : base("Invalid log directory", JET_err.InvalidLogDirectory)
		{
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001236C File Offset: 0x0001056C
		private EsentInvalidLogDirectoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
