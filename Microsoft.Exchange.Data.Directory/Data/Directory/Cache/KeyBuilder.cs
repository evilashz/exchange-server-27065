﻿using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x020000AD RID: 173
	internal static class KeyBuilder
	{
		// Token: 0x06000956 RID: 2390 RVA: 0x00029690 File Offset: 0x00027890
		internal static Tuple<string, KeyType> LookupKeysFromADObjectId(ADObjectId objectId)
		{
			ArgumentValidator.ThrowIfNull("objectId", objectId);
			if (!string.IsNullOrEmpty(objectId.DistinguishedName))
			{
				return new Tuple<string, KeyType>(objectId.DistinguishedName, KeyType.DistinguishedName);
			}
			if (!Guid.Empty.Equals(objectId.ObjectGuid))
			{
				return new Tuple<string, KeyType>(objectId.ObjectGuid.ToString(), KeyType.Guid);
			}
			throw new ArgumentException("Invalid ObjectId, Guid is empty and DN is null or empty");
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x000296FC File Offset: 0x000278FC
		internal static List<Tuple<string, KeyType>> LookupKeysFromProxyAddress(ProxyAddress proxyAddress)
		{
			ArgumentValidator.ThrowIfNull("proxyAddress", proxyAddress);
			List<Tuple<string, KeyType>> result;
			if (proxyAddress.Prefix == ProxyAddressPrefix.LegacyDN)
			{
				string addressString = proxyAddress.AddressString;
				string item = "x500:" + addressString;
				result = new List<Tuple<string, KeyType>>(2)
				{
					new Tuple<string, KeyType>(item, KeyType.EmailAddresses),
					new Tuple<string, KeyType>(addressString, KeyType.LegacyExchangeDN)
				};
			}
			else
			{
				result = new List<Tuple<string, KeyType>>(1)
				{
					new Tuple<string, KeyType>(proxyAddress.AddressString, KeyType.EmailAddresses)
				};
			}
			return result;
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0002978B File Offset: 0x0002798B
		internal static Tuple<string, KeyType> LookupKeysFromSid(SecurityIdentifier sId)
		{
			ArgumentValidator.ThrowIfNull("sId", sId);
			return new Tuple<string, KeyType>(sId.ToString(), KeyType.Sid | KeyType.MasterAccountSid | KeyType.SidHistory);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x000297A8 File Offset: 0x000279A8
		internal static Tuple<string, KeyType> LookupKeysFromNetId(string netId)
		{
			ArgumentValidator.ThrowIfNull("netId", netId);
			return new Tuple<string, KeyType>(netId, KeyType.NetId);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x000297C0 File Offset: 0x000279C0
		internal static Tuple<string, KeyType> LookupKeysFromExchangeGuid(Guid exchangeGuid, bool includeAggregated, bool includeArchive)
		{
			KeyType keyType = KeyType.ExchangeGuid;
			if (includeAggregated)
			{
				keyType |= KeyType.AggregatedMailboxGuid;
			}
			if (includeArchive)
			{
				keyType |= KeyType.ArchiveGuid;
			}
			return new Tuple<string, KeyType>(exchangeGuid.ToString(), keyType);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x000297FC File Offset: 0x000279FC
		internal static List<Tuple<string, KeyType>> LookupKeysFromLegacyExchangeDNs(string legDN)
		{
			ArgumentValidator.ThrowIfNull("legDN", legDN);
			return new List<Tuple<string, KeyType>>(2)
			{
				new Tuple<string, KeyType>(legDN, KeyType.LegacyExchangeDN),
				new Tuple<string, KeyType>("x500:" + legDN, KeyType.EmailAddresses)
			};
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00029848 File Offset: 0x00027A48
		internal static List<Tuple<string, KeyType>> GetAddKeysFromADRawEntry(ADRawEntry adEntry)
		{
			ArgumentValidator.ThrowIfNull("adEntry", adEntry);
			List<Tuple<string, KeyType>> list = new List<Tuple<string, KeyType>>();
			if (adEntry.Id != null && !string.IsNullOrEmpty(adEntry.Id.DistinguishedName))
			{
				list.Add(new Tuple<string, KeyType>(adEntry.Id.DistinguishedName.ToLower(), KeyType.DistinguishedName));
			}
			if (adEntry.Id != null && !adEntry.Id.ObjectGuid.Equals(Guid.Empty))
			{
				list.Add(new Tuple<string, KeyType>(adEntry.Id.ObjectGuid.ToString(), KeyType.Guid));
			}
			SecurityIdentifier securityIdentifier = (SecurityIdentifier)adEntry[IADSecurityPrincipalSchema.Sid];
			if (securityIdentifier != null)
			{
				list.Add(KeyBuilder.LookupKeysFromSid(securityIdentifier));
			}
			Guid guid = (Guid)adEntry[ADMailboxRecipientSchema.ExchangeGuid];
			if (!Guid.Empty.Equals(guid))
			{
				list.Add(KeyBuilder.LookupKeysFromExchangeGuid(guid, false, false));
			}
			ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)adEntry[ADRecipientSchema.EmailAddresses];
			if (proxyAddressCollection != null)
			{
				foreach (ProxyAddress proxyAddress in proxyAddressCollection)
				{
					list.AddRange(KeyBuilder.LookupKeysFromProxyAddress(proxyAddress));
				}
			}
			NetID netID = (NetID)adEntry[ADUserSchema.NetID];
			if (netID != null)
			{
				list.Add(KeyBuilder.LookupKeysFromNetId(netID.ToString()));
			}
			return list;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x000299C8 File Offset: 0x00027BC8
		internal static List<Tuple<string, KeyType>> GetAddKeysFromObject(ADObject adObject)
		{
			ArgumentValidator.ThrowIfNull("adObject", adObject);
			ObjectType objectTypeFor = CacheUtils.GetObjectTypeFor(adObject.GetType(), true);
			List<Tuple<string, KeyType>> list = new List<Tuple<string, KeyType>>();
			if (!string.IsNullOrEmpty(adObject.DistinguishedName))
			{
				list.Add(new Tuple<string, KeyType>(adObject.DistinguishedName.ToLower(), KeyType.DistinguishedName));
			}
			if (!Guid.Empty.Equals(adObject.Guid))
			{
				list.Add(new Tuple<string, KeyType>(adObject.Guid.ToString(), KeyType.Guid));
			}
			ObjectType objectType = objectTypeFor;
			if (objectType <= ObjectType.OWAMiniRecipient)
			{
				if (objectType <= ObjectType.FederatedOrganizationId)
				{
					switch (objectType)
					{
					case ObjectType.ExchangeConfigurationUnit:
						list.Add(new Tuple<string, KeyType>(adObject.Id.Parent.Name, KeyType.Name));
						list.Add(new Tuple<string, KeyType>(((ExchangeConfigurationUnit)adObject).ExternalDirectoryOrganizationId, KeyType.ExternalDirectoryOrganizationId));
						return list;
					case ObjectType.Recipient:
					{
						list.AddRange(KeyBuilder.GetSidKeys(adObject));
						list.AddRange(KeyBuilder.GetExchangeGuidKeys(adObject));
						string text = (string)adObject[ADRecipientSchema.LegacyExchangeDN];
						if (!string.IsNullOrEmpty(text))
						{
							list.Add(new Tuple<string, KeyType>(text, KeyType.LegacyExchangeDN));
						}
						list.AddRange(KeyBuilder.GetExchangeEmailAddressesKeys(adObject));
						return list;
					}
					case ObjectType.ExchangeConfigurationUnit | ObjectType.Recipient:
						return list;
					case ObjectType.AcceptedDomain:
						list.Add(new Tuple<string, KeyType>(adObject.Name, KeyType.Name));
						return list;
					default:
						if (objectType != ObjectType.FederatedOrganizationId)
						{
							return list;
						}
						list.Add(new Tuple<string, KeyType>(adObject.Id.Parent.DistinguishedName, KeyType.OrgCUDN));
						return list;
					}
				}
				else
				{
					if (objectType == ObjectType.MiniRecipient)
					{
						if ((adObject as MiniRecipient).NetID != null)
						{
							list.Add(KeyBuilder.LookupKeysFromNetId((adObject as MiniRecipient).NetID.ToString()));
						}
						ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)adObject[ADRecipientSchema.EmailAddresses];
						if (proxyAddressCollection != null)
						{
							foreach (ProxyAddress proxyAddress in proxyAddressCollection)
							{
								list.AddRange(KeyBuilder.LookupKeysFromProxyAddress(proxyAddress));
							}
						}
						list.AddRange(KeyBuilder.GetSidKeys(adObject));
						return list;
					}
					if (objectType != ObjectType.TransportMiniRecipient && objectType != ObjectType.OWAMiniRecipient)
					{
						return list;
					}
				}
			}
			else if (objectType <= ObjectType.StorageMiniRecipient)
			{
				if (objectType != ObjectType.ActiveSyncMiniRecipient && objectType != ObjectType.StorageMiniRecipient)
				{
					return list;
				}
			}
			else if (objectType != ObjectType.LoadBalancingMiniRecipient)
			{
				if (objectType == ObjectType.MiniRecipientWithTokenGroups)
				{
					list.AddRange(KeyBuilder.GetSidKeys(adObject));
					return list;
				}
				if (objectType != ObjectType.FrontEndMiniRecipient)
				{
					return list;
				}
				list.AddRange(KeyBuilder.GetExchangeGuidKeys(adObject));
				return list;
			}
			ProxyAddressCollection proxyAddressCollection2 = (ProxyAddressCollection)adObject[ADRecipientSchema.EmailAddresses];
			if (proxyAddressCollection2 != null)
			{
				foreach (ProxyAddress proxyAddress2 in proxyAddressCollection2)
				{
					list.AddRange(KeyBuilder.LookupKeysFromProxyAddress(proxyAddress2));
				}
			}
			list.AddRange(KeyBuilder.GetSidKeys(adObject));
			return list;
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00029CD8 File Offset: 0x00027ED8
		private static List<Tuple<string, KeyType>> GetSidKeys(ADObject adObject)
		{
			ArgumentValidator.ThrowIfNull("adObject", adObject);
			List<Tuple<string, KeyType>> list = new List<Tuple<string, KeyType>>();
			SecurityIdentifier securityIdentifier = (SecurityIdentifier)adObject[ADMailboxRecipientSchema.Sid];
			if (null != securityIdentifier)
			{
				list.Add(new Tuple<string, KeyType>(securityIdentifier.ToString(), KeyType.Sid));
			}
			securityIdentifier = (SecurityIdentifier)adObject[ADRecipientSchema.MasterAccountSid];
			if (null != securityIdentifier)
			{
				list.Add(new Tuple<string, KeyType>(securityIdentifier.ToString(), KeyType.MasterAccountSid));
			}
			MultiValuedProperty<SecurityIdentifier> multiValuedProperty = (MultiValuedProperty<SecurityIdentifier>)adObject[ADMailboxRecipientSchema.SidHistory];
			if (multiValuedProperty != null)
			{
				foreach (SecurityIdentifier securityIdentifier2 in multiValuedProperty)
				{
					list.Add(new Tuple<string, KeyType>(securityIdentifier2.ToString(), KeyType.SidHistory));
				}
			}
			return list;
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00029DBC File Offset: 0x00027FBC
		private static List<Tuple<string, KeyType>> GetExchangeGuidKeys(ADObject adObject)
		{
			ArgumentValidator.ThrowIfNull("adObject", adObject);
			List<Tuple<string, KeyType>> list = new List<Tuple<string, KeyType>>();
			Guid g = (Guid)adObject[ADMailboxRecipientSchema.ExchangeGuid];
			if (!Guid.Empty.Equals(g))
			{
				list.Add(new Tuple<string, KeyType>(g.ToString(), KeyType.ExchangeGuid));
			}
			g = (Guid)adObject[ADUserSchema.ArchiveGuid];
			if (!Guid.Empty.Equals(g))
			{
				list.Add(new Tuple<string, KeyType>(g.ToString(), KeyType.ArchiveGuid));
			}
			MultiValuedProperty<Guid> multiValuedProperty = (MultiValuedProperty<Guid>)adObject[ADUserSchema.AggregatedMailboxGuids];
			if (multiValuedProperty != null && multiValuedProperty.Count > 0)
			{
				foreach (Guid guid in multiValuedProperty)
				{
					list.Add(new Tuple<string, KeyType>(guid.ToString(), KeyType.AggregatedMailboxGuid));
				}
			}
			return list;
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00029ECC File Offset: 0x000280CC
		private static List<Tuple<string, KeyType>> GetExchangeEmailAddressesKeys(ADObject adObject)
		{
			ArgumentValidator.ThrowIfNull("adObject", adObject);
			List<Tuple<string, KeyType>> list = new List<Tuple<string, KeyType>>();
			ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)adObject[ADRecipientSchema.EmailAddresses];
			if (proxyAddressCollection != null && proxyAddressCollection.Count > 0)
			{
				foreach (ProxyAddress proxyAddress in proxyAddressCollection)
				{
					list.Add(new Tuple<string, KeyType>(proxyAddress.AddressString, KeyType.EmailAddresses));
				}
			}
			return list;
		}

		// Token: 0x04000329 RID: 809
		private const string LegDNAddressPrefix = "x500:";
	}
}
