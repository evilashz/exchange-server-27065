using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200012F RID: 303
	[Serializable]
	public class OfflineAddressBookIdParameter : ADIdParameter
	{
		// Token: 0x06000AD9 RID: 2777 RVA: 0x000233A7 File Offset: 0x000215A7
		public OfflineAddressBookIdParameter()
		{
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x000233AF File Offset: 0x000215AF
		public OfflineAddressBookIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x000233B8 File Offset: 0x000215B8
		public OfflineAddressBookIdParameter(OfflineAddressBook offlineAddresss) : base(offlineAddresss.Id)
		{
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x000233C6 File Offset: 0x000215C6
		public OfflineAddressBookIdParameter(OfflineAddressBookPresentationObject offlineAddresss) : base(offlineAddresss.Id)
		{
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x000233D4 File Offset: 0x000215D4
		public OfflineAddressBookIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x000233DD File Offset: 0x000215DD
		protected OfflineAddressBookIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x000233E6 File Offset: 0x000215E6
		public static OfflineAddressBookIdParameter Parse(string identity)
		{
			return new OfflineAddressBookIdParameter(identity);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x000233F0 File Offset: 0x000215F0
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			EnumerableWrapper<T> wrapper = EnumerableWrapper<T>.GetWrapper(base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason));
			if (!wrapper.HasElements() && base.RawIdentity.StartsWith("\\") && base.RawIdentity.Length > 1)
			{
				string identityString = base.RawIdentity.Substring(1);
				wrapper = EnumerableWrapper<T>.GetWrapper(this.GetObjectsInOrganization<T>(identityString, rootId, session, optionalData));
			}
			return wrapper;
		}
	}
}
