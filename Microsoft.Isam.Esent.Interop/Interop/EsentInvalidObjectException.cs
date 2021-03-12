using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001B3 RID: 435
	[Serializable]
	public sealed class EsentInvalidObjectException : EsentObsoleteException
	{
		// Token: 0x06000979 RID: 2425 RVA: 0x00013026 File Offset: 0x00011226
		public EsentInvalidObjectException() : base("Object is invalid for operation", JET_err.InvalidObject)
		{
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00013038 File Offset: 0x00011238
		private EsentInvalidObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
