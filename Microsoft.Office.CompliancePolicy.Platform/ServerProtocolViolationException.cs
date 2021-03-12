using System;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x02000047 RID: 71
	[Serializable]
	public sealed class ServerProtocolViolationException : Exception
	{
		// Token: 0x0600018C RID: 396 RVA: 0x00005DA3 File Offset: 0x00003FA3
		public ServerProtocolViolationException(long size) : base(string.Format("A server protocol violation occurred. The server is sending more data then it actually committed ({0})", size))
		{
		}
	}
}
