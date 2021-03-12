using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000BA RID: 186
	[Serializable]
	public sealed class EsentTaskDroppedException : EsentResourceException
	{
		// Token: 0x06000787 RID: 1927 RVA: 0x000114F6 File Offset: 0x0000F6F6
		public EsentTaskDroppedException() : base("A requested async task could not be executed", JET_err.TaskDropped)
		{
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00011505 File Offset: 0x0000F705
		private EsentTaskDroppedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
