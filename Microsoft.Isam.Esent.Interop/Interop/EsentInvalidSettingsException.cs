using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001BC RID: 444
	[Serializable]
	public sealed class EsentInvalidSettingsException : EsentUsageException
	{
		// Token: 0x0600098B RID: 2443 RVA: 0x00013122 File Offset: 0x00011322
		public EsentInvalidSettingsException() : base("System parameters were set improperly", JET_err.InvalidSettings)
		{
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00013134 File Offset: 0x00011334
		private EsentInvalidSettingsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
