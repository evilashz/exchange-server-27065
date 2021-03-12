using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200010E RID: 270
	[Serializable]
	public abstract class OwaTransientException : LocalizedException
	{
		// Token: 0x060009A5 RID: 2469 RVA: 0x00022ACE File Offset: 0x00020CCE
		public OwaTransientException(string message) : this(message, null, null)
		{
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00022AD9 File Offset: 0x00020CD9
		public OwaTransientException(string message, Exception innerException) : this(message, innerException, null)
		{
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00022AE4 File Offset: 0x00020CE4
		protected OwaTransientException(string message, Exception innerException, object thisObject) : base(new LocalizedString(message), innerException)
		{
			ExTraceGlobals.ExceptionTracer.TraceDebug((long)((thisObject != null) ? thisObject.GetHashCode() : 0), (message != null) ? message : "<Exception has no message associated>");
		}
	}
}
