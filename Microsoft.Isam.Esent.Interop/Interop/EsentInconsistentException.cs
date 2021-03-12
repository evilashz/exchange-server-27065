using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000B0 RID: 176
	[Serializable]
	public abstract class EsentInconsistentException : EsentDataException
	{
		// Token: 0x06000773 RID: 1907 RVA: 0x00011415 File Offset: 0x0000F615
		protected EsentInconsistentException(string description, JET_err err) : base(description, err)
		{
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001141F File Offset: 0x0000F61F
		protected EsentInconsistentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
