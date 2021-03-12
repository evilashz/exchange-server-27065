using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000E0 RID: 224
	[Serializable]
	public class AddressBookBaseIdParameter : IIdentityParameter
	{
		// Token: 0x0600082F RID: 2095 RVA: 0x0001DDCD File Offset: 0x0001BFCD
		public AddressBookBaseIdParameter()
		{
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001DDD5 File Offset: 0x0001BFD5
		public AddressBookBaseIdParameter(ADObjectId adObjectId)
		{
			this.addressListIdParameter = new AddressListIdParameter(adObjectId);
			this.globalAddressListIdParameter = new GlobalAddressListIdParameter(adObjectId);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001DDF5 File Offset: 0x0001BFF5
		public AddressBookBaseIdParameter(AddressList addressList)
		{
			this.addressListIdParameter = new AddressListIdParameter(addressList);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001DE09 File Offset: 0x0001C009
		public AddressBookBaseIdParameter(GlobalAddressList globalAddressList)
		{
			this.globalAddressListIdParameter = new GlobalAddressListIdParameter(globalAddressList);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001DE1D File Offset: 0x0001C01D
		public AddressBookBaseIdParameter(INamedIdentity namedIdentity) : this(namedIdentity.Identity)
		{
			this.rawIdentity = namedIdentity.DisplayName;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001DE37 File Offset: 0x0001C037
		protected AddressBookBaseIdParameter(string identity)
		{
			this.rawIdentity = identity;
			this.addressListIdParameter = AddressListIdParameter.Parse(identity);
			this.globalAddressListIdParameter = GlobalAddressListIdParameter.Parse(identity);
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x0001DE5E File Offset: 0x0001C05E
		string IIdentityParameter.RawIdentity
		{
			get
			{
				return this.RawIdentity;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x0001DE66 File Offset: 0x0001C066
		internal string RawIdentity
		{
			get
			{
				return this.rawIdentity ?? this.ToString();
			}
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001DE78 File Offset: 0x0001C078
		public static AddressBookBaseIdParameter Parse(string identity)
		{
			return new AddressBookBaseIdParameter(identity);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001DE80 File Offset: 0x0001C080
		void IIdentityParameter.Initialize(ObjectId objectId)
		{
			this.Initialize(objectId);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001DE89 File Offset: 0x0001C089
		public override string ToString()
		{
			if (this.addressListIdParameter != null)
			{
				return this.addressListIdParameter.ToString();
			}
			if (this.globalAddressListIdParameter != null)
			{
				return this.globalAddressListIdParameter.ToString();
			}
			return string.Empty;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001DEB8 File Offset: 0x0001C0B8
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			return this.GetObjects<T>(rootId, session, optionalData, out notFoundReason);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001DEC8 File Offset: 0x0001C0C8
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session)
		{
			LocalizedString? localizedString;
			return this.GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001DEE0 File Offset: 0x0001C0E0
		internal virtual void Initialize(ObjectId objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			ADObjectId adobjectId = objectId as ADObjectId;
			if (adobjectId == null)
			{
				throw new ArgumentException(Strings.ErrorInvalidParameterType("objectId", typeof(ADObjectId).Name), "objectId");
			}
			if (this.addressListIdParameter != null || this.globalAddressListIdParameter != null)
			{
				throw new InvalidOperationException(Strings.ErrorChangeImmutableType);
			}
			this.addressListIdParameter = new AddressListIdParameter();
			this.addressListIdParameter.Initialize(adobjectId);
			this.globalAddressListIdParameter = new GlobalAddressListIdParameter();
			this.globalAddressListIdParameter.Initialize(adobjectId);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001DF7C File Offset: 0x0001C17C
		internal virtual IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			TaskLogger.LogEnter();
			notFoundReason = null;
			IEnumerable<AddressBookBase> enumerable = new List<AddressBookBase>();
			try
			{
				if (typeof(T) != typeof(AddressBookBase))
				{
					throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
				}
				if (session == null)
				{
					throw new ArgumentNullException("session");
				}
				IList<IEnumerable<AddressBookBase>> list = new List<IEnumerable<AddressBookBase>>();
				if (this.addressListIdParameter != null)
				{
					list.Add(this.addressListIdParameter.GetObjects<AddressBookBase>(rootId, session));
				}
				if (this.globalAddressListIdParameter != null)
				{
					list.Add(this.globalAddressListIdParameter.GetObjects<AddressBookBase>(rootId, session));
				}
				enumerable = EnumerableWrapper<AddressBookBase>.GetWrapper(list);
			}
			finally
			{
				TaskLogger.LogExit();
			}
			return (IEnumerable<T>)enumerable;
		}

		// Token: 0x04000251 RID: 593
		private AddressListIdParameter addressListIdParameter;

		// Token: 0x04000252 RID: 594
		private GlobalAddressListIdParameter globalAddressListIdParameter;

		// Token: 0x04000253 RID: 595
		private string rawIdentity;
	}
}
