using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200020F RID: 527
	[Serializable]
	public sealed class EsentCallbackFailedException : EsentStateException
	{
		// Token: 0x06000A31 RID: 2609 RVA: 0x00013A36 File Offset: 0x00011C36
		public EsentCallbackFailedException() : base("A callback failed", JET_err.CallbackFailed)
		{
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00013A48 File Offset: 0x00011C48
		private EsentCallbackFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
