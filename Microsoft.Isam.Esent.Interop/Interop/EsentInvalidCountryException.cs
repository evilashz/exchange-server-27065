using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000156 RID: 342
	[Serializable]
	public sealed class EsentInvalidCountryException : EsentObsoleteException
	{
		// Token: 0x060008BF RID: 2239 RVA: 0x000125FA File Offset: 0x000107FA
		public EsentInvalidCountryException() : base("Invalid or unknown country/region code", JET_err.InvalidCountry)
		{
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001260C File Offset: 0x0001080C
		private EsentInvalidCountryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
