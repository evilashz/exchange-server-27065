using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	internal class ADContactInfo : ContactInfo
	{
		// Token: 0x0600003D RID: 61 RVA: 0x0000256C File Offset: 0x0000076C
		internal ADContactInfo(IADOrgPerson orgPerson)
		{
			this.orgPerson = orgPerson;
			this.mobilePhone = Utils.TrimSpaces(orgPerson.MobilePhone);
			this.homePhone = Utils.TrimSpaces(orgPerson.HomePhone);
			this.title = Utils.TrimSpaces(orgPerson.Title);
			this.companyName = Utils.TrimSpaces(orgPerson.Company);
			this.displayName = (Utils.TrimSpaces(orgPerson.DisplayName) ?? (Utils.TrimSpaces(orgPerson.FirstName) + " " + Utils.TrimSpaces(orgPerson.LastName)));
			ExAssert.RetailAssert(orgPerson is ADRecipient, "Class {0} implements IADOrgPerson doesn't inherit from ADRecipient", new object[]
			{
				orgPerson.GetType().FullName
			});
			using (UMSubscriber umsubscriber = UMRecipient.Factory.FromADRecipient<UMSubscriber>(orgPerson as ADRecipient))
			{
				if (umsubscriber != null)
				{
					this.dialPlan = umsubscriber.DialPlan;
					this.extension = umsubscriber.Extension;
					if (string.IsNullOrEmpty(this.businessPhone))
					{
						this.businessPhone = umsubscriber.Extension;
					}
				}
			}
			this.businessPhone = (Utils.TrimSpaces(this.orgPerson.Phone) ?? this.extension);
			foreach (string text in orgPerson.EmailAddresses.ToStringArray())
			{
				if (text.StartsWith("SIP:", true, CultureInfo.InvariantCulture))
				{
					this.imaddress = text;
					break;
				}
			}
			this.exchangeLegacyDN = orgPerson.LegacyExchangeDN;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000026F0 File Offset: 0x000008F0
		internal ADContactInfo(IADOrgPerson orgPerson, FoundByType foundBy) : this(orgPerson)
		{
			this.foundBy = foundBy;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002700 File Offset: 0x00000900
		internal ADContactInfo(IADOrgPerson orgPerson, UMDialPlan dialPlan, PhoneNumber callerId) : this(orgPerson)
		{
			this.SetFoundbyType(dialPlan, callerId);
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002711 File Offset: 0x00000911
		internal override IADOrgPerson ADOrgPerson
		{
			get
			{
				return this.orgPerson;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002719 File Offset: 0x00000919
		internal override UMDialPlan DialPlan
		{
			get
			{
				return this.dialPlan;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002721 File Offset: 0x00000921
		internal override string Title
		{
			get
			{
				return this.title;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002729 File Offset: 0x00000929
		internal override string Company
		{
			get
			{
				return this.companyName;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002731 File Offset: 0x00000931
		internal override string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002739 File Offset: 0x00000939
		internal override string FirstName
		{
			get
			{
				return this.orgPerson.FirstName;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002746 File Offset: 0x00000946
		internal override string LastName
		{
			get
			{
				return this.orgPerson.LastName;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002753 File Offset: 0x00000953
		// (set) Token: 0x06000048 RID: 72 RVA: 0x0000275B File Offset: 0x0000095B
		internal override string BusinessPhone
		{
			get
			{
				return this.businessPhone;
			}
			set
			{
				this.businessPhone = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002764 File Offset: 0x00000964
		// (set) Token: 0x0600004A RID: 74 RVA: 0x0000276C File Offset: 0x0000096C
		internal override string MobilePhone
		{
			get
			{
				return this.mobilePhone;
			}
			set
			{
				this.mobilePhone = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002775 File Offset: 0x00000975
		internal override string FaxNumber
		{
			get
			{
				return this.orgPerson.Fax;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002782 File Offset: 0x00000982
		// (set) Token: 0x0600004D RID: 77 RVA: 0x0000278A File Offset: 0x0000098A
		internal override string HomePhone
		{
			get
			{
				return this.homePhone;
			}
			set
			{
				this.homePhone = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002793 File Offset: 0x00000993
		internal override string Extension
		{
			get
			{
				return this.extension;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600004F RID: 79 RVA: 0x0000279B File Offset: 0x0000099B
		internal override string SipLine
		{
			get
			{
				return this.ADOrgPerson.RtcSipLine;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000027A8 File Offset: 0x000009A8
		internal override string IMAddress
		{
			get
			{
				return this.imaddress;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000027B0 File Offset: 0x000009B0
		internal override string EMailAddress
		{
			get
			{
				return this.orgPerson.PrimarySmtpAddress.ToString();
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000027D6 File Offset: 0x000009D6
		internal override FoundByType FoundBy
		{
			get
			{
				return this.foundBy;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000027DE File Offset: 0x000009DE
		internal string LegacyExchangeDN
		{
			get
			{
				return this.exchangeLegacyDN;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000027E8 File Offset: 0x000009E8
		internal override string Id
		{
			get
			{
				return this.orgPerson.Guid.ToString();
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002810 File Offset: 0x00000A10
		internal override string EwsId
		{
			get
			{
				return this.orgPerson.PrimarySmtpAddress.ToString();
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002836 File Offset: 0x00000A36
		internal override string EwsType
		{
			get
			{
				return "Mailbox";
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000283D File Offset: 0x00000A3D
		internal override string City
		{
			get
			{
				return this.orgPerson.City;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000284A File Offset: 0x00000A4A
		internal override string Country
		{
			get
			{
				if (!(this.orgPerson.CountryOrRegion != null))
				{
					return null;
				}
				return this.orgPerson.CountryOrRegion.Name;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002871 File Offset: 0x00000A71
		internal override ICollection<string> SanitizedPhoneNumbers
		{
			get
			{
				return this.orgPerson.SanitizedPhoneNumbers;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002880 File Offset: 0x00000A80
		internal static bool TryFindUmSubscriberByCallerId(UMDialPlan dialPlan, PhoneNumber callerId, out ADContactInfo contactInfo)
		{
			contactInfo = null;
			if (dialPlan == null)
			{
				return true;
			}
			try
			{
				contactInfo = ADContactInfo.FindUmSubscriberByCallerId(dialPlan, callerId);
			}
			catch (LocalizedException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000028B8 File Offset: 0x00000AB8
		internal static ADContactInfo FindUmSubscriberByCallerId(UMDialPlan dialPlan, PhoneNumber callerId)
		{
			if (dialPlan == null)
			{
				return null;
			}
			IADRecipientLookup lookup = ADRecipientLookupFactory.CreateUmProxyAddressLookup(dialPlan);
			return ADContactInfo.FindContactByCallerId(dialPlan, callerId, lookup);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000028DC File Offset: 0x00000ADC
		internal static bool TryFindCallerByCallerId(UMSubscriber umuser, PhoneNumber callerId, out ADContactInfo contactInfo)
		{
			contactInfo = null;
			try
			{
				contactInfo = ADContactInfo.FindCallerByCallerId(umuser, callerId);
				return true;
			}
			catch (LocalizedException)
			{
			}
			return false;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002910 File Offset: 0x00000B10
		internal static ADContactInfo FindCallerByCallerId(UMSubscriber umuser, PhoneNumber callerId)
		{
			if (umuser == null || umuser.DialPlan == null)
			{
				return null;
			}
			IADRecipientLookup lookup = ADRecipientLookupFactory.CreateUmProxyAddressLookup(umuser.DialPlan);
			return ADContactInfo.FindContactByCallerId(umuser.DialPlan, callerId, lookup);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002943 File Offset: 0x00000B43
		internal override Participant CreateParticipant(PhoneNumber callerId, CultureInfo cultureInfo)
		{
			return new Participant(this.ADOrgPerson as ADRecipient);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002958 File Offset: 0x00000B58
		private static ADContactInfo FindContactByCallerId(UMDialPlan dialPlan, PhoneNumber callerId, IADRecipientLookup lookup)
		{
			if (callerId == null || PhoneNumber.IsNullOrEmpty(callerId) || dialPlan == null)
			{
				return null;
			}
			ADContactInfo result = null;
			try
			{
				if ((result = ADContactInfo.FindContactByDialPlan(dialPlan, callerId, lookup)) != null)
				{
					return result;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "ADContactInfo::FindContactByCallerId attempting match against additional AD attributes", new object[0]);
				if ((result = ADContactInfo.FindContactBySipProxy(dialPlan, callerId, lookup)) != null)
				{
					return result;
				}
				callerId = callerId.Extend(dialPlan);
				if ((result = ADContactInfo.FindContactByUMCallingLineId(dialPlan, callerId, lookup)) != null)
				{
					return result;
				}
				if ((result = ADContactInfo.FindContactByRtcSipLine(dialPlan, callerId, lookup)) != null)
				{
					return result;
				}
				if ((result = ADContactInfo.FindContactByADHeuristics(dialPlan, callerId, lookup)) != null)
				{
					return result;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "ADContactInfo::FindByUMCallingLineId did not find any matches", new object[0]);
			}
			catch (LocalizedException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "Ignoring exception while resolving caller e={0}", new object[]
				{
					ex
				});
				result = null;
				throw;
			}
			return result;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002A44 File Offset: 0x00000C44
		private static ADContactInfo FindContactByDialPlan(UMDialPlan dialPlan, PhoneNumber callerId, IADRecipientLookup lookup)
		{
			ADContactInfo adcontactInfo = null;
			IADOrgPerson iadorgPerson = lookup.LookupByExtensionAndEquivalentDialPlan(callerId.ToDial, dialPlan) as IADOrgPerson;
			if (iadorgPerson != null)
			{
				adcontactInfo = new ADContactInfo(iadorgPerson, FoundByType.BusinessPhone);
			}
			PIIMessage data = PIIMessage.Create(PIIType._UserDisplayName, (adcontactInfo == null) ? "<null>" : adcontactInfo.DisplayName);
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, data, "ADContactInfo::FindContactInDialPlan returning _UserDisplayName", new object[0]);
			return adcontactInfo;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002AA8 File Offset: 0x00000CA8
		private static ADContactInfo FindContactBySipProxy(UMDialPlan dialPlan, PhoneNumber callerId, IADRecipientLookup lookup)
		{
			ADContactInfo adcontactInfo = null;
			if (callerId.UriType == UMUriType.SipName)
			{
				IADOrgPerson iadorgPerson = lookup.LookupBySipExtension(callerId.Number) as IADOrgPerson;
				if (iadorgPerson != null)
				{
					adcontactInfo = new ADContactInfo(iadorgPerson, FoundByType.BusinessPhone);
				}
			}
			PIIMessage data = PIIMessage.Create(PIIType._UserDisplayName, (adcontactInfo == null) ? "<null>" : adcontactInfo.DisplayName);
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, data, "ADContactInfo::FindContactBySipProxy returning contact = _UserDisplayName", new object[0]);
			return adcontactInfo;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002B14 File Offset: 0x00000D14
		private static ADContactInfo FindContactByUMCallingLineId(UMDialPlan dialPlan, PhoneNumber callerId, IADRecipientLookup lookup)
		{
			ADContactInfo result = null;
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADOrgPersonSchema.UMCallingLineIds, callerId.ToDial);
			IADOrgPerson iadorgPerson = ADContactInfo.SearchForUniquePerson(filter, lookup);
			if (iadorgPerson != null)
			{
				result = new ADContactInfo(iadorgPerson, FoundByType.BusinessPhone);
			}
			return result;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002B4C File Offset: 0x00000D4C
		private static ADContactInfo FindContactByRtcSipLine(UMDialPlan dialPlan, PhoneNumber callerId, IADRecipientLookup lookup)
		{
			ADContactInfo result = null;
			if (callerId.UriType != UMUriType.E164 && callerId.UriType != UMUriType.TelExtn)
			{
				return null;
			}
			QueryFilter filter = ADContactInfo.BuildRtcSipLineFilter(dialPlan, callerId);
			IADOrgPerson iadorgPerson = ADContactInfo.SearchForUniquePerson(filter, lookup);
			if (iadorgPerson != null)
			{
				result = new ADContactInfo(iadorgPerson, FoundByType.BusinessPhone);
			}
			return result;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002B8C File Offset: 0x00000D8C
		private static ADContactInfo FindContactByADHeuristics(UMDialPlan dialPlan, PhoneNumber callerId, IADRecipientLookup lookup)
		{
			ADContactInfo result = null;
			if (!dialPlan.AllowHeuristicADCallingLineIdResolution)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "Not considering clid for heuristic match because its not allowed on the dialplan", new object[0]);
				return null;
			}
			if (callerId.UriType != UMUriType.E164 && callerId.UriType != UMUriType.TelExtn)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "Not considering clid for heuristic match because its not an E164 or tel extension", new object[0]);
				return null;
			}
			string text = callerId.ToDial;
			int num = text.Length - (text.StartsWith("+", StringComparison.OrdinalIgnoreCase) ? 1 : 0);
			if (num < 3)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "Not considering clid for heuristic match because it has {0} significant digits.", new object[]
				{
					num
				});
				return null;
			}
			List<string> optionalPrefixes = callerId.GetOptionalPrefixes(dialPlan);
			foreach (string text2 in optionalPrefixes)
			{
				if (text.StartsWith(text2, StringComparison.OrdinalIgnoreCase) && text.Length > text2.Length)
				{
					text = text.Substring(text2.Length);
					break;
				}
			}
			if (UMUriType.TelExtn == dialPlan.URIType && text.Length < dialPlan.NumberOfDigitsInExtension)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "Not performing heuristic search because the searchstring length '{0}' is less than extension length '{1}'", new object[]
				{
					text.Length,
					dialPlan.NumberOfDigitsInExtension
				});
				return null;
			}
			QueryFilter filter = new TextFilter(ADRecipientSchema.IndexedPhoneNumbers, text, MatchOptions.Suffix, MatchFlags.Default);
			ADRecipient[] matches = lookup.LookupByQueryFilter(filter);
			IADOrgPerson iadorgPerson = null;
			if (ADContactInfo.TryFindBestHeuristicPhoneMatch(matches, optionalPrefixes, text, dialPlan, out iadorgPerson))
			{
				result = new ADContactInfo(iadorgPerson, dialPlan, callerId);
			}
			return result;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002D38 File Offset: 0x00000F38
		private static bool TryFindBestHeuristicPhoneMatch(ADRecipient[] matches, List<string> optionalPrefixes, string searchString, UMDialPlan dialPlan, out IADOrgPerson bestPerson)
		{
			bestPerson = null;
			string value = null;
			int num = int.MaxValue;
			if (matches == null || matches.Length == 0)
			{
				return false;
			}
			foreach (ADRecipient adrecipient in matches)
			{
				IADOrgPerson iadorgPerson = (IADOrgPerson)adrecipient;
				foreach (string text in iadorgPerson.SanitizedPhoneNumbers)
				{
					if (text.EndsWith(searchString, StringComparison.OrdinalIgnoreCase))
					{
						string b = string.Empty;
						if (text.Length > searchString.Length)
						{
							b = text.Substring(0, text.Length - searchString.Length);
						}
						int j = 0;
						while (j < optionalPrefixes.Count)
						{
							if (string.Equals(optionalPrefixes[j], b, StringComparison.OrdinalIgnoreCase))
							{
								if (j < num)
								{
									bestPerson = iadorgPerson;
									value = text;
									num = j;
									break;
								}
								if (j == num && bestPerson != null && bestPerson.Guid != iadorgPerson.Guid)
								{
									CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "ADContactInfo found more than one match.  Discarding matches.", new object[0]);
									bestPerson = null;
									value = string.Empty;
									break;
								}
								break;
							}
							else
							{
								j++;
							}
						}
					}
				}
			}
			if (bestPerson == null)
			{
				return false;
			}
			PIIMessage[] data = new PIIMessage[]
			{
				PIIMessage.Create(PIIType._PII, bestPerson.ToString()),
				PIIMessage.Create(PIIType._PII, value)
			};
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, data, "ADContactInfo choosing person=PII1, phone=PII2", new object[0]);
			return true;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002ED4 File Offset: 0x000010D4
		private static IADOrgPerson SearchForUniquePerson(QueryFilter filter, IADRecipientLookup lookup)
		{
			IADOrgPerson iadorgPerson = null;
			ADRecipient[] array = lookup.LookupByQueryFilter(filter);
			if (array == null || array.Length == 0)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "No recipient found for filter'{0}'", new object[]
				{
					filter
				});
			}
			else if (array.Length == 1)
			{
				iadorgPerson = (array[0] as IADOrgPerson);
			}
			else
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "Non-unique recipient for filter '{0}'", new object[]
				{
					filter
				});
			}
			PIIMessage data = PIIMessage.Create(PIIType._UserDisplayName, (iadorgPerson == null) ? "<null>" : iadorgPerson.DisplayName);
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, data, "ADContactInfo::SearchForUniquePerson returning person = _UserDisplayName", new object[0]);
			return iadorgPerson;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002F80 File Offset: 0x00001180
		private static QueryFilter BuildRtcSipLineFilter(UMDialPlan dialPlan, PhoneNumber callerId)
		{
			string text = "tel:";
			if (UMUriType.E164 == callerId.UriType || callerId.ToDial.StartsWith("+"))
			{
				text += callerId.ToDial;
			}
			else if (!string.IsNullOrEmpty(dialPlan.CountryOrRegionCode) && !callerId.ToDial.StartsWith(dialPlan.CountryOrRegionCode, StringComparison.OrdinalIgnoreCase))
			{
				text = text + "+" + dialPlan.CountryOrRegionCode + callerId.Number;
			}
			else
			{
				text = text + "+" + callerId.Number;
			}
			QueryFilter queryFilter = new TextFilter(ADOrgPersonSchema.RtcSipLine, text + ";", MatchOptions.Prefix, MatchFlags.IgnoreCase);
			QueryFilter queryFilter2 = new ComparisonFilter(ComparisonOperator.Equal, ADOrgPersonSchema.RtcSipLine, text);
			return new OrFilter(new QueryFilter[]
			{
				queryFilter,
				queryFilter2
			});
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003044 File Offset: 0x00001244
		private void SetFoundbyType(UMDialPlan dialPlan, PhoneNumber callerId)
		{
			if (callerId.IsMatch(this.BusinessPhone ?? string.Empty, dialPlan))
			{
				this.foundBy = FoundByType.BusinessPhone;
				return;
			}
			if (callerId.IsMatch(this.HomePhone ?? string.Empty, dialPlan))
			{
				this.foundBy = FoundByType.HomePhone;
				return;
			}
			if (callerId.IsMatch(this.MobilePhone ?? string.Empty, dialPlan))
			{
				this.foundBy = FoundByType.MobilePhone;
			}
		}

		// Token: 0x0400000B RID: 11
		private const int MinimumSignificantDigitsForAdSearch = 3;

		// Token: 0x0400000C RID: 12
		private IADOrgPerson orgPerson;

		// Token: 0x0400000D RID: 13
		private UMDialPlan dialPlan;

		// Token: 0x0400000E RID: 14
		private string businessPhone;

		// Token: 0x0400000F RID: 15
		private string mobilePhone;

		// Token: 0x04000010 RID: 16
		private string homePhone;

		// Token: 0x04000011 RID: 17
		private string extension;

		// Token: 0x04000012 RID: 18
		private string imaddress;

		// Token: 0x04000013 RID: 19
		private string title;

		// Token: 0x04000014 RID: 20
		private string displayName;

		// Token: 0x04000015 RID: 21
		private string companyName;

		// Token: 0x04000016 RID: 22
		private string exchangeLegacyDN;

		// Token: 0x04000017 RID: 23
		private FoundByType foundBy;
	}
}
