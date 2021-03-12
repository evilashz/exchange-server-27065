using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200010D RID: 269
	[Serializable]
	public sealed class OwaException : LocalizedException
	{
		// Token: 0x060009A2 RID: 2466 RVA: 0x00022A87 File Offset: 0x00020C87
		public OwaException(string message, Exception innerException, object thisObject) : base(new LocalizedString(message), innerException)
		{
			ExTraceGlobals.ExceptionTracer.TraceDebug((long)((thisObject != null) ? thisObject.GetHashCode() : 0), (message != null) ? message : "<Exception has no message associated>");
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00022AB8 File Offset: 0x00020CB8
		public OwaException(string message, Exception innerException) : this(message, innerException, null)
		{
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00022AC3 File Offset: 0x00020CC3
		public OwaException(string message) : this(message, null, null)
		{
		}
	}
}
