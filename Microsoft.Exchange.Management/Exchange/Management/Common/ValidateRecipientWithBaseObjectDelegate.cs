using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x02000134 RID: 308
	// (Invoke) Token: 0x06000ADC RID: 2780
	internal delegate void ValidateRecipientWithBaseObjectDelegate<TDataObject>(TDataObject baseObject, ADRecipient recipient, Task.ErrorLoggerDelegate writeError);
}
