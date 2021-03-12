using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000AD RID: 173
	[Serializable]
	public abstract class EsentQuotaException : EsentResourceException
	{
		// Token: 0x0600076D RID: 1901 RVA: 0x000113D9 File Offset: 0x0000F5D9
		protected EsentQuotaException(string description, JET_err err) : base(description, err)
		{
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x000113E3 File Offset: 0x0000F5E3
		protected EsentQuotaException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
