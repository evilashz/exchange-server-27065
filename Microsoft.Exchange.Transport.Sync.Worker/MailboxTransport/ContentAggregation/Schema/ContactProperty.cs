using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Properties.XSO;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema
{
	// Token: 0x0200022F RID: 559
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class ContactProperty<T> : XSOPropertyBase<T>
	{
		// Token: 0x0600141A RID: 5146 RVA: 0x0004976A File Offset: 0x0004796A
		protected ContactProperty(IXSOPropertyManager propertyManager, params PropertyDefinition[] propertyDefinitions) : base(propertyManager, propertyDefinitions)
		{
		}

		// Token: 0x170006C1 RID: 1729
		// (set) Token: 0x0600141B RID: 5147 RVA: 0x00049774 File Offset: 0x00047974
		internal static Func<Item, bool> IsItemNewDelegate
		{
			set
			{
				ContactProperty<T>.isItemNewDelegate = value;
			}
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0004977C File Offset: 0x0004797C
		protected bool IsItemNew(Item item)
		{
			if (ContactProperty<T>.isItemNewDelegate == null)
			{
				return item.Id == null;
			}
			return ContactProperty<T>.isItemNewDelegate(item);
		}

		// Token: 0x04000A89 RID: 2697
		private static Func<Item, bool> isItemNewDelegate;
	}
}
