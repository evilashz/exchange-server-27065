using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000B4 RID: 180
	[Serializable]
	public abstract class EsentObsoleteException : EsentApiException
	{
		// Token: 0x0600077B RID: 1915 RVA: 0x00011465 File Offset: 0x0000F665
		protected EsentObsoleteException(string description, JET_err err) : base(description, err)
		{
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001146F File Offset: 0x0000F66F
		protected EsentObsoleteException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
