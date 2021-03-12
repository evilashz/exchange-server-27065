using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000A9 RID: 169
	[Serializable]
	public abstract class EsentFatalException : EsentOperationException
	{
		// Token: 0x06000765 RID: 1893 RVA: 0x00011389 File Offset: 0x0000F589
		protected EsentFatalException(string description, JET_err err) : base(description, err)
		{
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00011393 File Offset: 0x0000F593
		protected EsentFatalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
