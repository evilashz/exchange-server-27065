using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000F7 RID: 247
	[Serializable]
	public sealed class EsentCheckpointFileNotFoundException : EsentInconsistentException
	{
		// Token: 0x06000801 RID: 2049 RVA: 0x00011B96 File Offset: 0x0000FD96
		public EsentCheckpointFileNotFoundException() : base("Could not locate checkpoint file", JET_err.CheckpointFileNotFound)
		{
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00011BA8 File Offset: 0x0000FDA8
		private EsentCheckpointFileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
