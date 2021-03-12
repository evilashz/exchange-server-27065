using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002C9 RID: 713
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractContactBase : AbstractItem, IContactBase, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06001E9A RID: 7834 RVA: 0x000860E8 File Offset: 0x000842E8
		// (set) Token: 0x06001E9B RID: 7835 RVA: 0x000860EF File Offset: 0x000842EF
		public virtual string DisplayName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x000860F6 File Offset: 0x000842F6
		public override void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
