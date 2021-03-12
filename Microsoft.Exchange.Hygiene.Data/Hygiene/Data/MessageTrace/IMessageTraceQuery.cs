using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000147 RID: 327
	internal interface IMessageTraceQuery
	{
		// Token: 0x06000C8E RID: 3214
		MessageTrace[] FindPagedTrace(Guid organizationalUnitRoot, DateTime start, DateTime end, string fromEmailPrefix = null, string fromEmailDomain = null, string toEmailPrefix = null, string toEmailDomain = null, string clientMessageId = null, int rowIndex = 0, int rowCount = -1);

		// Token: 0x06000C8F RID: 3215
		MessageTrace Read(Guid organizationalUnitRoot, Guid messageId);
	}
}
