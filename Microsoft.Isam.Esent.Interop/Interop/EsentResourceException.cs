using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000AB RID: 171
	[Serializable]
	public abstract class EsentResourceException : EsentOperationException
	{
		// Token: 0x06000769 RID: 1897 RVA: 0x000113B1 File Offset: 0x0000F5B1
		protected EsentResourceException(string description, JET_err err) : base(description, err)
		{
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x000113BB File Offset: 0x0000F5BB
		protected EsentResourceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
