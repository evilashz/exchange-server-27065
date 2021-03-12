using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000E4 RID: 228
	// (Invoke) Token: 0x060011E1 RID: 4577
	public delegate IConfigurable TeamMailboxGetDataObject<TObject>(IIdentityParameter id, IConfigDataProvider session, ObjectId rootId, LocalizedString? notFoundError, LocalizedString? multipleFoundError, ExchangeErrorCategory errorCategory) where TObject : IConfigurable, new();
}
