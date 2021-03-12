using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000197 RID: 407
	[Serializable]
	public abstract class OwaPermanentException : LocalizedException
	{
		// Token: 0x06000EC9 RID: 3785 RVA: 0x0005E321 File Offset: 0x0005C521
		public OwaPermanentException(string message) : this(message, null, null)
		{
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0005E32C File Offset: 0x0005C52C
		public OwaPermanentException(string message, Exception innerException) : this(message, innerException, null)
		{
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0005E337 File Offset: 0x0005C537
		protected OwaPermanentException(string message, Exception innerException, object thisObject) : base(new LocalizedString(message), innerException)
		{
			ExTraceGlobals.ExceptionTracer.TraceDebug((long)((thisObject != null) ? thisObject.GetHashCode() : 0), (message != null) ? message : "<Exception has no message associated>");
		}
	}
}
