using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000A6 RID: 166
	[Serializable]
	public abstract class EsentOperationException : EsentErrorException
	{
		// Token: 0x0600075F RID: 1887 RVA: 0x0001134D File Offset: 0x0000F54D
		protected EsentOperationException(string description, JET_err err) : base(description, err)
		{
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00011357 File Offset: 0x0000F557
		protected EsentOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
