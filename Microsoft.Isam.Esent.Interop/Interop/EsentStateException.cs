using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000B3 RID: 179
	[Serializable]
	public abstract class EsentStateException : EsentApiException
	{
		// Token: 0x06000779 RID: 1913 RVA: 0x00011451 File Offset: 0x0000F651
		protected EsentStateException(string description, JET_err err) : base(description, err)
		{
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001145B File Offset: 0x0000F65B
		protected EsentStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
