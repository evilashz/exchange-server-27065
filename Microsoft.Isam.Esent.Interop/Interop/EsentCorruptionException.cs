using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000AF RID: 175
	[Serializable]
	public abstract class EsentCorruptionException : EsentDataException
	{
		// Token: 0x06000771 RID: 1905 RVA: 0x00011401 File Offset: 0x0000F601
		protected EsentCorruptionException(string description, JET_err err) : base(description, err)
		{
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001140B File Offset: 0x0000F60B
		protected EsentCorruptionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
