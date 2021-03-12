using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001AB RID: 427
	[Serializable]
	public sealed class EsentObjectNotFoundException : EsentStateException
	{
		// Token: 0x06000969 RID: 2409 RVA: 0x00012F46 File Offset: 0x00011146
		public EsentObjectNotFoundException() : base("No such table or object", JET_err.ObjectNotFound)
		{
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00012F58 File Offset: 0x00011158
		private EsentObjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
