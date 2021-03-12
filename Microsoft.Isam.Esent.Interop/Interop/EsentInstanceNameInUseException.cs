using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200016C RID: 364
	[Serializable]
	public sealed class EsentInstanceNameInUseException : EsentUsageException
	{
		// Token: 0x060008EB RID: 2283 RVA: 0x00012862 File Offset: 0x00010A62
		public EsentInstanceNameInUseException() : base("Instance Name already in use", JET_err.InstanceNameInUse)
		{
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00012874 File Offset: 0x00010A74
		private EsentInstanceNameInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
