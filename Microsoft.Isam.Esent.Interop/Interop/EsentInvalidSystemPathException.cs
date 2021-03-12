using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200013D RID: 317
	[Serializable]
	public sealed class EsentInvalidSystemPathException : EsentObsoleteException
	{
		// Token: 0x0600088D RID: 2189 RVA: 0x0001233E File Offset: 0x0001053E
		public EsentInvalidSystemPathException() : base("Invalid system path", JET_err.InvalidSystemPath)
		{
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00012350 File Offset: 0x00010550
		private EsentInvalidSystemPathException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
