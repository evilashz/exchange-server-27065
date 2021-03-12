using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000A7 RID: 167
	[Serializable]
	public abstract class EsentDataException : EsentErrorException
	{
		// Token: 0x06000761 RID: 1889 RVA: 0x00011361 File Offset: 0x0000F561
		protected EsentDataException(string description, JET_err err) : base(description, err)
		{
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001136B File Offset: 0x0000F56B
		protected EsentDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
