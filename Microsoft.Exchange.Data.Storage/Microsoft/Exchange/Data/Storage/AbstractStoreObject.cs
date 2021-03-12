using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002C0 RID: 704
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractStoreObject : AbstractStorePropertyBag, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06001D79 RID: 7545 RVA: 0x00085C83 File Offset: 0x00083E83
		public virtual StoreObjectId StoreObjectId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06001D7A RID: 7546 RVA: 0x00085C8A File Offset: 0x00083E8A
		// (set) Token: 0x06001D7B RID: 7547 RVA: 0x00085C91 File Offset: 0x00083E91
		public virtual PersistablePropertyBag PropertyBag
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

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x00085C98 File Offset: 0x00083E98
		// (set) Token: 0x06001D7D RID: 7549 RVA: 0x00085C9F File Offset: 0x00083E9F
		public virtual string ClassName
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

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x00085CA6 File Offset: 0x00083EA6
		public virtual bool IsNew
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06001D7F RID: 7551 RVA: 0x00085CAD File Offset: 0x00083EAD
		public virtual IStoreSession Session
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x00085CB4 File Offset: 0x00083EB4
		public virtual StoreObjectId ParentId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06001D81 RID: 7553 RVA: 0x00085CBB File Offset: 0x00083EBB
		// (set) Token: 0x06001D82 RID: 7554 RVA: 0x00085CC2 File Offset: 0x00083EC2
		public virtual VersionedId Id
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

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06001D83 RID: 7555 RVA: 0x00085CC9 File Offset: 0x00083EC9
		public virtual byte[] RecordKey
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06001D84 RID: 7556 RVA: 0x00085CD0 File Offset: 0x00083ED0
		public virtual LocationIdentifierHelper LocationIdentifierHelperInstance
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x00085CD7 File Offset: 0x00083ED7
		public virtual void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
