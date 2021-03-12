using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent
{
	// Token: 0x020000A4 RID: 164
	[Serializable]
	public abstract class EsentException : Exception
	{
		// Token: 0x06000758 RID: 1880 RVA: 0x000112D2 File Offset: 0x0000F4D2
		protected EsentException()
		{
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x000112DA File Offset: 0x0000F4DA
		protected EsentException(string message) : base(message)
		{
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x000112E3 File Offset: 0x0000F4E3
		protected EsentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
