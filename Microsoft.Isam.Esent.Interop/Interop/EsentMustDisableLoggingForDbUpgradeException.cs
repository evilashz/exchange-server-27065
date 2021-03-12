using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000113 RID: 275
	[Serializable]
	public sealed class EsentMustDisableLoggingForDbUpgradeException : EsentObsoleteException
	{
		// Token: 0x06000839 RID: 2105 RVA: 0x00011EA6 File Offset: 0x000100A6
		public EsentMustDisableLoggingForDbUpgradeException() : base("Cannot have logging enabled while attempting to upgrade db", JET_err.MustDisableLoggingForDbUpgrade)
		{
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00011EB8 File Offset: 0x000100B8
		private EsentMustDisableLoggingForDbUpgradeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
