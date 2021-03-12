using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000AE RID: 174
	[Serializable]
	public abstract class EsentDiskException : EsentResourceException
	{
		// Token: 0x0600076F RID: 1903 RVA: 0x000113ED File Offset: 0x0000F5ED
		protected EsentDiskException(string description, JET_err err) : base(description, err)
		{
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x000113F7 File Offset: 0x0000F5F7
		protected EsentDiskException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
