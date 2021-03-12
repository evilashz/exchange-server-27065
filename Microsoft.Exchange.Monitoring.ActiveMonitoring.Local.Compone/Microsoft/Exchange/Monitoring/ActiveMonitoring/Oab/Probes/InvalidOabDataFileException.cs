using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Oab.Probes
{
	// Token: 0x02000239 RID: 569
	public class InvalidOabDataFileException : Exception
	{
		// Token: 0x06000FD8 RID: 4056 RVA: 0x0006A199 File Offset: 0x00068399
		public InvalidOabDataFileException(string user, string message) : base(string.Format("Invalid server response for user {0}. {1}", user, message))
		{
		}
	}
}
