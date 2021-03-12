using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000151 RID: 337
	[Serializable]
	public sealed class EsentLinkNotSupportedException : EsentObsoleteException
	{
		// Token: 0x060008B5 RID: 2229 RVA: 0x0001256E File Offset: 0x0001076E
		public EsentLinkNotSupportedException() : base("Link support unavailable", JET_err.LinkNotSupported)
		{
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00012580 File Offset: 0x00010780
		private EsentLinkNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
