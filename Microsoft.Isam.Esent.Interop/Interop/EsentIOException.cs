using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000AA RID: 170
	[Serializable]
	public abstract class EsentIOException : EsentOperationException
	{
		// Token: 0x06000767 RID: 1895 RVA: 0x0001139D File Offset: 0x0000F59D
		protected EsentIOException(string description, JET_err err) : base(description, err)
		{
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000113A7 File Offset: 0x0000F5A7
		protected EsentIOException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
