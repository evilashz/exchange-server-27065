using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002E2 RID: 738
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractReferenceAttachment : AbstractAttachment, IReferenceAttachment, IAttachment, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06001F81 RID: 8065 RVA: 0x000865CB File Offset: 0x000847CB
		// (set) Token: 0x06001F82 RID: 8066 RVA: 0x000865D2 File Offset: 0x000847D2
		public virtual string AttachLongPathName
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

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06001F83 RID: 8067 RVA: 0x000865D9 File Offset: 0x000847D9
		// (set) Token: 0x06001F84 RID: 8068 RVA: 0x000865E0 File Offset: 0x000847E0
		public virtual string ProviderEndpointUrl
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

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06001F85 RID: 8069 RVA: 0x000865E7 File Offset: 0x000847E7
		// (set) Token: 0x06001F86 RID: 8070 RVA: 0x000865EE File Offset: 0x000847EE
		public virtual string ProviderType
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
	}
}
