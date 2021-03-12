using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.MessagingPolicies.AddressRewrite
{
	// Token: 0x0200000A RID: 10
	internal static class Utils
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000031E8 File Offset: 0x000013E8
		public static ADObjectId RootId
		{
			get
			{
				return new ADObjectId("OU=MSExchangeGateway");
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000031F4 File Offset: 0x000013F4
		internal static ADObjectId GetRelativeDnForEntryType(Utils.EntryType entryType, IConfigurationSession session)
		{
			string unescapedCommonName = (entryType == Utils.EntryType.SmtpAddress) ? Utils.EmailEntriesRDn : Utils.DomainEntriesRDn;
			ADObjectId childId = Utils.RootId.GetChildId(Utils.AddressRewriteConfigRDn);
			return childId.GetChildId(unescapedCommonName);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000322C File Offset: 0x0000142C
		internal static Utils.EntryType ValidateAddressEntrySyntax(string address)
		{
			try
			{
				RoutingAddress.Parse(address);
				return Utils.EntryType.SmtpAddress;
			}
			catch (FormatException)
			{
			}
			try
			{
				SmtpDomain.Parse(address);
				return Utils.EntryType.Domain;
			}
			catch (FormatException)
			{
			}
			if (address == "*")
			{
				return Utils.EntryType.WildCardedDomain;
			}
			if (address.StartsWith("*.") && address.Length > 2)
			{
				SmtpDomain.Parse(address.Substring(2, address.Length - 2));
				return Utils.EntryType.WildCardedDomain;
			}
			throw new FormatException(Strings.AddressRewriteUnrecognizedAddress);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000032C0 File Offset: 0x000014C0
		internal static void CheckConflicts(ADObjectId baseContainer, bool outboundOnly, string internalAddress, string externalAddress, IConfigurationSession session, Guid? skipGuid)
		{
			QueryFilter filter;
			if (outboundOnly)
			{
				filter = new ComparisonFilter(ComparisonOperator.Equal, AddressRewriteEntrySchema.InternalAddress, internalAddress);
			}
			else
			{
				QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, AddressRewriteEntrySchema.InternalAddress, internalAddress);
				QueryFilter queryFilter2 = new ComparisonFilter(ComparisonOperator.Equal, AddressRewriteEntrySchema.ExternalAddress, externalAddress);
				filter = new OrFilter(new QueryFilter[]
				{
					queryFilter,
					queryFilter2
				});
			}
			AddressRewriteEntry[] array = session.Find<AddressRewriteEntry>(baseContainer, QueryScope.OneLevel, filter, null, 2);
			if (array == null)
			{
				return;
			}
			AddressRewriteEntry[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				AddressRewriteEntry addressRewriteEntry = array2[i];
				if (skipGuid == null || !addressRewriteEntry.Guid.Equals(skipGuid))
				{
					if (addressRewriteEntry.InternalAddress.Equals(internalAddress, StringComparison.OrdinalIgnoreCase))
					{
						throw new ArgumentException(Strings.AddressRewriteInternalAddressExists, null);
					}
					throw new ArgumentException(Strings.AddressRewriteExternalAddressExists, null);
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000033A0 File Offset: 0x000015A0
		internal static ADObjectId ValidateEntryAddresses(string internalAddress, string externalAddress, bool outboundOnly, MultiValuedProperty<string> exceptionList, IConfigurationSession session, Guid? skipGuid)
		{
			if (!outboundOnly && exceptionList != null && exceptionList.Count != 0)
			{
				throw new ArgumentException(Strings.AddressRewriteExceptionListDisallowed, null);
			}
			Utils.EntryType entryType = Utils.ValidateSyntax(internalAddress, externalAddress, outboundOnly);
			outboundOnly = (outboundOnly || entryType == Utils.EntryType.WildCardedDomain);
			ADObjectId relativeDnForEntryType = Utils.GetRelativeDnForEntryType(entryType, session);
			Utils.CheckConflicts(relativeDnForEntryType, outboundOnly, internalAddress, externalAddress, session, skipGuid);
			return relativeDnForEntryType;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000033F8 File Offset: 0x000015F8
		internal static Utils.EntryType ValidateSyntax(string internalAddress, string externalAddress, bool outboundOnly)
		{
			Utils.EntryType entryType = Utils.ValidateAddressEntrySyntax(internalAddress);
			Utils.EntryType entryType2 = Utils.ValidateAddressEntrySyntax(externalAddress);
			if (entryType == Utils.EntryType.SmtpAddress && entryType2 == Utils.EntryType.SmtpAddress)
			{
				return Utils.EntryType.SmtpAddress;
			}
			if ((entryType != Utils.EntryType.Domain && entryType != Utils.EntryType.WildCardedDomain) || entryType2 != Utils.EntryType.Domain)
			{
				throw new ArgumentException(Strings.AddressRewriteInvalidMapping, null);
			}
			if (entryType == Utils.EntryType.WildCardedDomain && !outboundOnly)
			{
				throw new ArgumentException(Strings.AddressRewriteWildcardWarning, null);
			}
			return entryType;
		}

		// Token: 0x0400001F RID: 31
		private static string DomainEntriesRDn = "Domain Entries";

		// Token: 0x04000020 RID: 32
		private static string EmailEntriesRDn = "Email Entries";

		// Token: 0x04000021 RID: 33
		private static string AddressRewriteConfigRDn = "Address Rewrite Configuration";

		// Token: 0x0200000B RID: 11
		internal enum EntryType
		{
			// Token: 0x04000023 RID: 35
			SmtpAddress,
			// Token: 0x04000024 RID: 36
			Domain,
			// Token: 0x04000025 RID: 37
			WildCardedDomain
		}
	}
}
