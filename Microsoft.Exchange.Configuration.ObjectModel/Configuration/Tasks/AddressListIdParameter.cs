using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000DE RID: 222
	[Serializable]
	public class AddressListIdParameter : ADIdParameter
	{
		// Token: 0x0600081C RID: 2076 RVA: 0x0001D949 File Offset: 0x0001BB49
		public AddressListIdParameter()
		{
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001D951 File Offset: 0x0001BB51
		public AddressListIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001D95A File Offset: 0x0001BB5A
		public AddressListIdParameter(AddressList adList) : base(adList.Id)
		{
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001D968 File Offset: 0x0001BB68
		public AddressListIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0001D971 File Offset: 0x0001BB71
		protected AddressListIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001D97A File Offset: 0x0001BB7A
		public static AddressListIdParameter Parse(string identity)
		{
			return new AddressListIdParameter(identity);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0001D982 File Offset: 0x0001BB82
		internal static ADObjectId GetRootContainerId(IConfigurationSession scSession)
		{
			return AddressListIdParameter.GetRootContainerId(scSession, null);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001D98C File Offset: 0x0001BB8C
		internal static ADObjectId GetRootContainerId(IConfigurationSession scSession, OrganizationId currentOrg)
		{
			if (scSession == null)
			{
				throw new ArgumentNullException("scSession");
			}
			ADObjectId adobjectId;
			if (currentOrg == null || currentOrg.ConfigurationUnit == null)
			{
				adobjectId = scSession.GetOrgContainerId();
			}
			else
			{
				adobjectId = currentOrg.ConfigurationUnit;
			}
			return adobjectId.GetDescendantId(AddressList.RdnAlContainerToOrganization);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0001D9D8 File Offset: 0x0001BBD8
		internal static string[] GetCommonNames(string identityString)
		{
			List<string> list = new List<string>();
			string[] array = Regex.Split(identityString, "(\\\\+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in array)
			{
				if (!string.IsNullOrEmpty(text))
				{
					if ('\\' == text[0])
					{
						if (text.Length % 2 == 0)
						{
							stringBuilder.Append(text.Replace("\\\\", "\\"));
						}
						else
						{
							if (stringBuilder.Length > 0)
							{
								list.Add(stringBuilder.ToString());
							}
							stringBuilder.Length = 0;
							stringBuilder.Append(text.Substring(1).Replace("\\\\", "\\"));
						}
					}
					else
					{
						stringBuilder.Append(text);
					}
				}
			}
			if (stringBuilder.Length > 0)
			{
				list.Add(stringBuilder.ToString());
			}
			return list.ToArray();
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0001DAB2 File Offset: 0x0001BCB2
		internal override IEnumerableFilter<T> GetEnumerableFilter<T>()
		{
			return AddressListIdParameter.AddressListFilter<T>.GetInstance();
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001DABC File Offset: 0x0001BCBC
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
				ADObjectId rootContainerId = AddressListIdParameter.GetRootContainerId(scSession);
				return EnumerableWrapper<T>.GetWrapper(base.GetADObjectIdObjects<T>(rootContainerId, rootId, subTreeSession, optionalData), this.GetEnumerableFilter<T>());
			}
			return base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0001DB64 File Offset: 0x0001BD64
		internal override IEnumerable<T> GetObjectsInOrganization<T>(string identityString, ADObjectId rootId, IDirectorySession session, OptionalIdentityData optionalData)
		{
			IConfigurationSession scSession = session as IConfigurationSession;
			string[] commonNames = AddressListIdParameter.GetCommonNames(identityString);
			ADObjectId identity = this.ResolveAddressListId(AddressListIdParameter.GetRootContainerId(scSession), commonNames);
			EnumerableWrapper<T> wrapper = EnumerableWrapper<T>.GetWrapper(base.GetADObjectIdObjects<T>(identity, rootId, session, optionalData));
			if (wrapper.HasElements())
			{
				return wrapper;
			}
			wrapper = EnumerableWrapper<T>.GetWrapper(base.GetObjectsInOrganization<T>(identityString, rootId, session, optionalData));
			if (wrapper.HasElements() || commonNames.Length == 1 || !identityString.Contains("*"))
			{
				return wrapper;
			}
			Queue<ADObjectId> queue = new Queue<ADObjectId>();
			queue.Enqueue(rootId ?? AddressListIdParameter.GetRootContainerId(scSession));
			for (int i = 0; i < commonNames.Length - 1; i++)
			{
				Queue<ADObjectId> queue2 = new Queue<ADObjectId>();
				string name = commonNames[i];
				foreach (ADObjectId rootId2 in queue)
				{
					QueryFilter filter = base.CreateWildcardOrEqualFilter(ADObjectSchema.Name, name);
					IEnumerable<T> enumerable = this.PerformSearch<T>(filter, rootId2, session, false);
					foreach (T t in enumerable)
					{
						queue2.Enqueue((ADObjectId)t.Identity);
					}
				}
				queue = queue2;
			}
			string name2 = commonNames[commonNames.Length - 1];
			List<IEnumerable<T>> list = new List<IEnumerable<T>>();
			foreach (ADObjectId rootId3 in queue)
			{
				QueryFilter filter2 = base.CreateWildcardOrEqualFilter(ADObjectSchema.Name, name2);
				IEnumerable<T> item = base.PerformPrimarySearch<T>(filter2, rootId3, session, false, optionalData);
				list.Add(item);
			}
			return EnumerableWrapper<T>.GetWrapper(list);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001DD3C File Offset: 0x0001BF3C
		private ADObjectId ResolveAddressListId(ADObjectId rootContainerId, string[] commonName)
		{
			ADObjectId adobjectId = rootContainerId;
			for (int i = 0; i < commonName.Length; i++)
			{
				adobjectId = adobjectId.GetChildId(commonName[i]);
			}
			return adobjectId;
		}

		// Token: 0x020000DF RID: 223
		private class AddressListFilter<T> : IEnumerableFilter<T>
		{
			// Token: 0x06000829 RID: 2089 RVA: 0x0001DD64 File Offset: 0x0001BF64
			public static AddressListIdParameter.AddressListFilter<T> GetInstance()
			{
				return AddressListIdParameter.AddressListFilter<T>.addressListFilter;
			}

			// Token: 0x0600082A RID: 2090 RVA: 0x0001DD6C File Offset: 0x0001BF6C
			public bool AcceptElement(T element)
			{
				AddressBookBase addressBookBase = element as AddressBookBase;
				return addressBookBase != null && !addressBookBase.IsGlobalAddressList && !addressBookBase.IsInSystemAddressListContainer;
			}

			// Token: 0x0600082B RID: 2091 RVA: 0x0001DD9D File Offset: 0x0001BF9D
			public override bool Equals(object obj)
			{
				return obj is AddressListIdParameter.AddressListFilter<T>;
			}

			// Token: 0x0600082C RID: 2092 RVA: 0x0001DDA8 File Offset: 0x0001BFA8
			public override int GetHashCode()
			{
				return typeof(AddressListIdParameter.AddressListFilter<T>).GetHashCode();
			}

			// Token: 0x04000250 RID: 592
			private static AddressListIdParameter.AddressListFilter<T> addressListFilter = new AddressListIdParameter.AddressListFilter<T>();
		}
	}
}
