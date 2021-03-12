using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000B2 RID: 178
	[Serializable]
	public abstract class EsentUsageException : EsentApiException
	{
		// Token: 0x06000777 RID: 1911 RVA: 0x0001143D File Offset: 0x0000F63D
		protected EsentUsageException(string description, JET_err err) : base(description, err)
		{
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00011447 File Offset: 0x0000F647
		protected EsentUsageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
