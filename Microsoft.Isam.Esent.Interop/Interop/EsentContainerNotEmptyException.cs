using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200014A RID: 330
	[Serializable]
	public sealed class EsentContainerNotEmptyException : EsentObsoleteException
	{
		// Token: 0x060008A7 RID: 2215 RVA: 0x000124AA File Offset: 0x000106AA
		public EsentContainerNotEmptyException() : base("Container is not empty", JET_err.ContainerNotEmpty)
		{
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x000124BC File Offset: 0x000106BC
		private EsentContainerNotEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
