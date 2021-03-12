using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002CB RID: 715
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractContact : AbstractContactBase, IContact, IContactBase, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06001EA4 RID: 7844 RVA: 0x00086105 File Offset: 0x00084305
		public virtual IDictionary<EmailAddressIndex, Participant> EmailAddresses
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x0008610C File Offset: 0x0008430C
		// (set) Token: 0x06001EA6 RID: 7846 RVA: 0x00086113 File Offset: 0x00084313
		public virtual string ImAddress
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

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06001EA7 RID: 7847 RVA: 0x0008611A File Offset: 0x0008431A
		// (set) Token: 0x06001EA8 RID: 7848 RVA: 0x00086121 File Offset: 0x00084321
		public bool IsFavorite
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

		// Token: 0x06001EA9 RID: 7849 RVA: 0x00086128 File Offset: 0x00084328
		public Stream GetPhotoStream()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x0008612F File Offset: 0x0008432F
		public override void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
