using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002C1 RID: 705
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractItem : AbstractStoreObject, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06001D87 RID: 7559 RVA: 0x00085CE6 File Offset: 0x00083EE6
		// (set) Token: 0x06001D88 RID: 7560 RVA: 0x00085CED File Offset: 0x00083EED
		public virtual AttachmentCollection AttachmentCollection
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

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06001D89 RID: 7561 RVA: 0x00085CF4 File Offset: 0x00083EF4
		public virtual Body Body
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06001D8A RID: 7562 RVA: 0x00085CFB File Offset: 0x00083EFB
		public virtual IBody IBody
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06001D8B RID: 7563 RVA: 0x00085D02 File Offset: 0x00083F02
		public virtual ItemCategoryList Categories
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06001D8C RID: 7564 RVA: 0x00085D09 File Offset: 0x00083F09
		public virtual ICoreItem CoreItem
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06001D8D RID: 7565 RVA: 0x00085D10 File Offset: 0x00083F10
		public ItemCharsetDetector CharsetDetector
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x00085D17 File Offset: 0x00083F17
		// (set) Token: 0x06001D8F RID: 7567 RVA: 0x00085D1E File Offset: 0x00083F1E
		public virtual IAttachmentCollection IAttachmentCollection
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

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06001D90 RID: 7568 RVA: 0x00085D25 File Offset: 0x00083F25
		public virtual MapiMessage MapiMessage
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06001D91 RID: 7569 RVA: 0x00085D2C File Offset: 0x00083F2C
		// (set) Token: 0x06001D92 RID: 7570 RVA: 0x00085D33 File Offset: 0x00083F33
		public PropertyBagSaveFlags SaveFlags
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

		// Token: 0x06001D93 RID: 7571 RVA: 0x00085D3A File Offset: 0x00083F3A
		public virtual void OpenAsReadWrite()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x00085D41 File Offset: 0x00083F41
		public virtual ConflictResolutionResult Save(SaveMode saveMode)
		{
			throw new NotImplementedException();
		}
	}
}
