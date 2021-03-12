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
	// Token: 0x02000116 RID: 278
	[Serializable]
	public class GlobalAddressListIdParameter : ADIdParameter
	{
		// Token: 0x060009F4 RID: 2548 RVA: 0x0002169A File Offset: 0x0001F89A
		public GlobalAddressListIdParameter()
		{
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x000216A2 File Offset: 0x0001F8A2
		public GlobalAddressListIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x000216AB File Offset: 0x0001F8AB
		public GlobalAddressListIdParameter(GlobalAddressList globalAddressList) : base(globalAddressList.Id)
		{
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x000216B9 File Offset: 0x0001F8B9
		public GlobalAddressListIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x000216C2 File Offset: 0x0001F8C2
		protected GlobalAddressListIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x000216CB File Offset: 0x0001F8CB
		public static GlobalAddressListIdParameter Parse(string identity)
		{
			return new GlobalAddressListIdParameter(identity);
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x000216D3 File Offset: 0x0001F8D3
		internal static ADObjectId GetRootContainerId(IConfigurationSession scSession)
		{
			return GlobalAddressListIdParameter.GetRootContainerId(scSession, null);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x000216DC File Offset: 0x0001F8DC
		internal static ADObjectId GetRootContainerId(IConfigurationSession scSession, OrganizationId currentOrg)
		{
			ADObjectId adobjectId;
			if (currentOrg == null || currentOrg.ConfigurationUnit == null)
			{
				adobjectId = scSession.GetOrgContainerId();
			}
			else
			{
				adobjectId = currentOrg.ConfigurationUnit;
			}
			return adobjectId.GetDescendantId(GlobalAddressList.RdnGalContainerToOrganization);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00021717 File Offset: 0x0001F917
		internal override IEnumerableFilter<T> GetEnumerableFilter<T>()
		{
			return GlobalAddressListIdParameter.GlobalAddressListFilter<T>.GetInstance();
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00021720 File Offset: 0x0001F920
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (typeof(T) != typeof(AddressBookBase))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			IConfigurationSession scSession = session as IConfigurationSession;
			if ("\\" == base.RawIdentity)
			{
				notFoundReason = null;
				ADObjectId rootContainerId = GlobalAddressListIdParameter.GetRootContainerId(scSession);
				return EnumerableWrapper<T>.GetWrapper(base.GetADObjectIdObjects<T>(rootContainerId, rootId, subTreeSession, optionalData), this.GetEnumerableFilter<T>());
			}
			return base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x000217C8 File Offset: 0x0001F9C8
		internal override IEnumerable<T> GetObjectsInOrganization<T>(string identityString, ADObjectId rootId, IDirectorySession session, OptionalIdentityData optionalData)
		{
			string[] commonNames = AddressListIdParameter.GetCommonNames(identityString);
			if (commonNames.Length == 1)
			{
				ADObjectId rootContainerId = GlobalAddressListIdParameter.GetRootContainerId((IConfigurationSession)session);
				ADObjectId childId = rootContainerId.GetChildId(commonNames[0]);
				EnumerableWrapper<T> wrapper = EnumerableWrapper<T>.GetWrapper(base.GetADObjectIdObjects<T>(childId, rootId, session, optionalData), this.GetEnumerableFilter<T>());
				if (wrapper.HasElements())
				{
					return wrapper;
				}
			}
			return base.GetObjectsInOrganization<T>(identityString, rootId, session, optionalData);
		}

		// Token: 0x02000117 RID: 279
		private class GlobalAddressListFilter<T> : IEnumerableFilter<T>
		{
			// Token: 0x060009FF RID: 2559 RVA: 0x00021824 File Offset: 0x0001FA24
			public static GlobalAddressListIdParameter.GlobalAddressListFilter<T> GetInstance()
			{
				return GlobalAddressListIdParameter.GlobalAddressListFilter<T>.globalAddressListFilter;
			}

			// Token: 0x06000A00 RID: 2560 RVA: 0x0002182C File Offset: 0x0001FA2C
			public bool AcceptElement(T element)
			{
				AddressBookBase addressBookBase = element as AddressBookBase;
				return addressBookBase != null && addressBookBase.IsGlobalAddressList && !addressBookBase.Id.DistinguishedName.StartsWith(GlobalAddressList.RdnGalContainerToOrganization.DistinguishedName, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x06000A01 RID: 2561 RVA: 0x00021872 File Offset: 0x0001FA72
			public override bool Equals(object obj)
			{
				return obj is GlobalAddressListIdParameter.GlobalAddressListFilter<T>;
			}

			// Token: 0x06000A02 RID: 2562 RVA: 0x0002187D File Offset: 0x0001FA7D
			public override int GetHashCode()
			{
				return typeof(GlobalAddressListIdParameter.GlobalAddressListFilter<T>).GetHashCode();
			}

			// Token: 0x04000279 RID: 633
			private static GlobalAddressListIdParameter.GlobalAddressListFilter<T> globalAddressListFilter = new GlobalAddressListIdParameter.GlobalAddressListFilter<T>();
		}
	}
}
