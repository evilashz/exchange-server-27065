using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Oab.Probes
{
	// Token: 0x0200023A RID: 570
	public class InvalidOabManifestFileException : Exception
	{
		// Token: 0x06000FD9 RID: 4057 RVA: 0x0006A1AD File Offset: 0x000683AD
		public InvalidOabManifestFileException(string user, string message) : base(string.Format("Invalid manifest file returned by the server for user {0}. {1}", user, message))
		{
		}
	}
}
