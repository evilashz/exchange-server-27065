using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000219 RID: 537
	[Serializable]
	public sealed class EsentLSCallbackNotSpecifiedException : EsentUsageException
	{
		// Token: 0x06000A45 RID: 2629 RVA: 0x00013B4E File Offset: 0x00011D4E
		public EsentLSCallbackNotSpecifiedException() : base("Attempted to use Local Storage without a callback function being specified", JET_err.LSCallbackNotSpecified)
		{
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00013B60 File Offset: 0x00011D60
		private EsentLSCallbackNotSpecifiedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
