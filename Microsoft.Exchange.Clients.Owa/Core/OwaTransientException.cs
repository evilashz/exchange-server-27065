using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000198 RID: 408
	[Serializable]
	public abstract class OwaTransientException : LocalizedException
	{
		// Token: 0x06000ECC RID: 3788 RVA: 0x0005E368 File Offset: 0x0005C568
		public OwaTransientException(string message) : this(message, null, null)
		{
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0005E373 File Offset: 0x0005C573
		public OwaTransientException(string message, Exception innerException) : this(message, innerException, null)
		{
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0005E37E File Offset: 0x0005C57E
		protected OwaTransientException(string message, Exception innerException, object thisObject) : base(new LocalizedString(message), innerException)
		{
			ExTraceGlobals.ExceptionTracer.TraceDebug((long)((thisObject != null) ? thisObject.GetHashCode() : 0), (message != null) ? message : "<Exception has no message associated>");
		}
	}
}
