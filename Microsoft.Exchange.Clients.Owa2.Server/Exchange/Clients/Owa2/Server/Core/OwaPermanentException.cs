using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000AB RID: 171
	[Serializable]
	public abstract class OwaPermanentException : LocalizedException
	{
		// Token: 0x060006EB RID: 1771 RVA: 0x00014B9D File Offset: 0x00012D9D
		public OwaPermanentException(string message) : this(message, null, null)
		{
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00014BA8 File Offset: 0x00012DA8
		public OwaPermanentException(string message, Exception innerException) : this(message, innerException, null)
		{
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00014BB3 File Offset: 0x00012DB3
		protected OwaPermanentException(string message, Exception innerException, object thisObject) : base(new LocalizedString(message), innerException)
		{
			ExTraceGlobals.ExceptionTracer.TraceDebug((long)((thisObject != null) ? thisObject.GetHashCode() : 0), (message != null) ? message : "<Exception has no message associated>");
		}
	}
}
