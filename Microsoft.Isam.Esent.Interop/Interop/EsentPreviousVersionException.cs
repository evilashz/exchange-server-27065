using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000BF RID: 191
	[Serializable]
	public sealed class EsentPreviousVersionException : EsentErrorException
	{
		// Token: 0x06000791 RID: 1937 RVA: 0x00011576 File Offset: 0x0000F776
		public EsentPreviousVersionException() : base("Version already existed. Recovery failure", JET_err.PreviousVersion)
		{
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00011588 File Offset: 0x0000F788
		private EsentPreviousVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
