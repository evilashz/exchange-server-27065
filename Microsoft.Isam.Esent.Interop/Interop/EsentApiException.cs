using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000A8 RID: 168
	[Serializable]
	public abstract class EsentApiException : EsentErrorException
	{
		// Token: 0x06000763 RID: 1891 RVA: 0x00011375 File Offset: 0x0000F575
		protected EsentApiException(string description, JET_err err) : base(description, err)
		{
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001137F File Offset: 0x0000F57F
		protected EsentApiException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
