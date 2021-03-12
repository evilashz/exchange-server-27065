using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x02000132 RID: 306
	// (Invoke) Token: 0x06000AD4 RID: 2772
	internal delegate ADRecipient GetRecipientDelegate<TIdentityParameter>(TIdentityParameter identityParameter, Task.ErrorLoggerDelegate writeError) where TIdentityParameter : IIdentityParameter;
}
