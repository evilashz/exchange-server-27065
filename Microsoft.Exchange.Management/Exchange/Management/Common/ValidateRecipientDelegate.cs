using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x02000133 RID: 307
	// (Invoke) Token: 0x06000AD8 RID: 2776
	internal delegate void ValidateRecipientDelegate(ADRecipient recipient, string recipientId, Task.ErrorLoggerDelegate writeError);
}
