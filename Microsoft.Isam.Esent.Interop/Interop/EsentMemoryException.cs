using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000AC RID: 172
	[Serializable]
	public abstract class EsentMemoryException : EsentResourceException
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x000113C5 File Offset: 0x0000F5C5
		protected EsentMemoryException(string description, JET_err err) : base(description, err)
		{
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000113CF File Offset: 0x0000F5CF
		protected EsentMemoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
