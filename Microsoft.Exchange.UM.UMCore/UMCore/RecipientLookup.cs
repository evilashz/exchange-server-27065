using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200016F RID: 367
	internal class RecipientLookup
	{
		// Token: 0x06000ABE RID: 2750 RVA: 0x0002D33C File Offset: 0x0002B53C
		private RecipientLookup(BaseUMCallSession vo)
		{
			this.partialMatches = new List<ContactSearchItem>();
			this.exactMatches = new List<ContactSearchItem>();
			this.rawMatches = new List<ADRecipient>();
			DialPermissionWrapper dialPermissionWrapper = DialPermissionWrapperFactory.Create(vo);
			if (dialPermissionWrapper.ContactScope == DialScopeEnum.AddressList && dialPermissionWrapper.SearchRoot != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, "RecipientLookup::ctor(): SearchRoot: {0}.", new object[]
				{
					dialPermissionWrapper.SearchRoot
				});
				this.lookup = ADRecipientLookupFactory.CreateFromOrganizationId(dialPermissionWrapper.OrganizationId, dialPermissionWrapper.SearchRoot);
				return;
			}
			this.lookup = ADRecipientLookupFactory.CreateFromOrganizationId(dialPermissionWrapper.OrganizationId, null);
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0002D3D3 File Offset: 0x0002B5D3
		internal int QueryResults
		{
			get
			{
				return this.queryResults;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0002D3DB File Offset: 0x0002B5DB
		internal int TotalMatches
		{
			get
			{
				return this.totalMatches;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0002D3E3 File Offset: 0x0002B5E3
		internal List<ContactSearchItem> PartialMatches
		{
			get
			{
				return this.partialMatches;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0002D3EB File Offset: 0x0002B5EB
		internal List<ContactSearchItem> ExactMatches
		{
			get
			{
				return this.exactMatches;
			}
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002D3F3 File Offset: 0x0002B5F3
		internal static RecipientLookup Create(BaseUMCallSession vo)
		{
			return new RecipientLookup(vo);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0002D3FC File Offset: 0x0002B5FC
		internal void SetSearchInExistingResults()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, "RecipientLookup::SetSearchInExistingResults().", new object[0]);
			this.searchInExistingResults = true;
			this.searchDomain = new List<ContactSearchItem>();
			this.searchDomain.AddRange(this.exactMatches);
			this.searchDomain.AddRange(this.partialMatches);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002D454 File Offset: 0x0002B654
		internal int Lookup(string dtmf, SearchMethod mode, bool wildcardsearch, bool filterOptOutUsers, bool anonymousCaller, UMDialPlan targetDialPlan)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, "RecipientLookup::Lookup({0},{1},{2},{3},{4},{5}).", new object[]
			{
				dtmf,
				mode,
				wildcardsearch,
				filterOptOutUsers,
				anonymousCaller,
				(targetDialPlan != null) ? targetDialPlan.DistinguishedName : "<null>"
			});
			this.exactMatches.Clear();
			this.partialMatches.Clear();
			this.rawMatches.Clear();
			this.totalMatches = 0;
			if (!this.searchInExistingResults)
			{
				return this.SearchActiveDirectory(dtmf, mode, wildcardsearch, filterOptOutUsers, anonymousCaller, targetDialPlan);
			}
			return this.SearchInExistingResults(dtmf, wildcardsearch, filterOptOutUsers);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0002D504 File Offset: 0x0002B704
		internal int SearchInExistingResults(string dtmf, bool wildcardsearch, bool filterOptOutUsers)
		{
			string value = "emailAddress:";
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(value);
			stringBuilder.Append(dtmf);
			string value2 = stringBuilder.ToString();
			foreach (ContactSearchItem contactSearchItem in this.searchDomain)
			{
				if (contactSearchItem.DtmfEmailAlias.StartsWith(value2, StringComparison.OrdinalIgnoreCase))
				{
					if (this.IsEqualDtmf(new MultiValuedProperty<string>(contactSearchItem.DtmfEmailAlias), SearchMethod.EmailAlias, dtmf))
					{
						this.exactMatches.Add(contactSearchItem);
					}
					else
					{
						this.partialMatches.Add(contactSearchItem);
					}
				}
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, "RecipientLookup::SearchInExistingResults() - returning Exact={0} Partial={1}.", new object[]
			{
				this.exactMatches.Count,
				this.partialMatches.Count
			});
			return this.exactMatches.Count + this.partialMatches.Count;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0002D60C File Offset: 0x0002B80C
		internal int SearchActiveDirectory(string dtmf, SearchMethod mode, bool wildcardsearch, bool filterOptOutUsers, bool anonymousCaller, UMDialPlan targetDialPlan)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, "RecipientLookup::SearchActiveDirectory({0},{1},{2},{3},{4},{5}).", new object[]
			{
				dtmf,
				mode,
				wildcardsearch,
				filterOptOutUsers,
				anonymousCaller,
				(targetDialPlan != null) ? targetDialPlan.DistinguishedName : "<null>"
			});
			if (wildcardsearch)
			{
				dtmf += "*";
			}
			string mode2 = string.Empty;
			switch (mode)
			{
			case SearchMethod.FirstNameLastName:
				mode2 = "firstNameLastName:";
				break;
			case SearchMethod.LastNameFirstName:
				mode2 = "lastNameFirstName:";
				break;
			case SearchMethod.EmailAlias:
				mode2 = "emailAddress:";
				break;
			default:
				ExAssert.RetailAssert(false, "Unsupported search mode {0}", new object[]
				{
					mode
				});
				break;
			}
			ADRecipient[] array = this.lookup.LookupByDtmfMap(mode2, dtmf, filterOptOutUsers, anonymousCaller, targetDialPlan, Constants.DirectorySearch.MaxResultsToPreprocess + 1);
			CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, "RecipientLookup::SearchActiveDirectory({0}) rawMatches = {1}.", new object[]
			{
				dtmf,
				array.Length
			});
			if (array != null)
			{
				this.rawMatches.AddRange(array);
			}
			this.queryResults = this.rawMatches.Count;
			CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, "RecipientLookup::SearchActiveDirectory returning #results = {0}.", new object[]
			{
				this.queryResults
			});
			return this.rawMatches.Count;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0002D770 File Offset: 0x0002B970
		internal ADRecipient LookupByExtension(string extension, BaseUMCallSession vo, DirectorySearchPurpose purpose, DialScopeEnum scope)
		{
			extension = extension.TrimEnd(RecipientLookup.nonNumerics);
			PIIMessage data = PIIMessage.Create(PIIType._PhoneNumber, extension);
			CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, data, "RecipientLookup::LookupByExtension(_PhoneNumber,{0},{1}).", new object[]
			{
				purpose,
				scope
			});
			ADRecipient adrecipient = this.lookup.LookupByExtensionAndDialPlan(extension, vo.CurrentCallContext.DialPlan);
			PIIMessage data2 = PIIMessage.Create(PIIType._SmtpAddress, (adrecipient != null) ? adrecipient.PrimarySmtpAddress.ToString() : "<null>");
			if (adrecipient != null && this.SatisfiesPurpose(adrecipient, purpose) && this.IsInScope(adrecipient, vo.CurrentCallContext.DialPlan, scope))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, data2, "LookupByExtension: Found user=_SmtpAddress in scope={0} for purpose={1}.", new object[]
				{
					scope,
					purpose
				});
			}
			else
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, data2, "Did not find user=_SmtpAddress in scope={0} for purpose={1}.", new object[]
				{
					scope,
					purpose
				});
				adrecipient = null;
			}
			PIIMessage data3 = PIIMessage.Create(PIIType._User, adrecipient);
			CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, data3, "LookupByExtension returning recipient=_User.", new object[0]);
			return adrecipient;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0002D8A8 File Offset: 0x0002BAA8
		internal void PostProcess(string dtmf, SearchMethod mode, DirectorySearchPurpose purpose)
		{
			if (this.searchInExistingResults)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, "RecipientLookup::PostProcess - SearchInExistingResults - returning without doing anything.", new object[0]);
				return;
			}
			List<ContactSearchItem> list = this.FilterByPurpose(this.rawMatches, purpose);
			this.totalMatches = list.Count;
			foreach (ContactSearchItem contactSearchItem in list)
			{
				ADRecipient recipient = contactSearchItem.Recipient;
				PIIMessage data = PIIMessage.Create(PIIType._User, recipient.DisplayName);
				if (this.IsEqualDtmf(recipient.UMDtmfMap, mode, dtmf))
				{
					this.exactMatches.Add(contactSearchItem);
					CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, data, "ExactMatch: [_User] For Dtmf {0} SearchMode: {1}.", new object[]
					{
						dtmf,
						mode
					});
				}
				else
				{
					this.partialMatches.Add(contactSearchItem);
					CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, data, "PartialMatch: [_User] For Dtmf {0} SearchMode: {1}.", new object[]
					{
						dtmf,
						mode
					});
				}
			}
			Dictionary<string, ContactSearchItem> dictionary = new Dictionary<string, ContactSearchItem>();
			foreach (ContactSearchItem contactSearchItem2 in list)
			{
				IADOrgPerson iadorgPerson = contactSearchItem2.Recipient as IADOrgPerson;
				if (iadorgPerson != null)
				{
					if (dictionary.ContainsKey(iadorgPerson.DisplayName))
					{
						ContactSearchItem contactSearchItem3 = dictionary[iadorgPerson.DisplayName];
						contactSearchItem3.NeedDisambiguation = true;
						contactSearchItem2.NeedDisambiguation = true;
					}
					else
					{
						dictionary[iadorgPerson.DisplayName] = contactSearchItem2;
					}
				}
			}
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0002DA54 File Offset: 0x0002BC54
		private static bool MapDtmfAttribute(string s, ref SearchMethod mode)
		{
			bool result = false;
			if (string.Compare(s, "firstNameLastName:", true, CultureInfo.InvariantCulture) == 0)
			{
				mode = SearchMethod.FirstNameLastName;
				result = true;
			}
			else if (string.Compare(s, "lastNameFirstName:", true, CultureInfo.InvariantCulture) == 0)
			{
				mode = SearchMethod.LastNameFirstName;
				result = true;
			}
			else if (string.Compare(s, "emailAddress:", true, CultureInfo.InvariantCulture) == 0)
			{
				mode = SearchMethod.EmailAlias;
				result = true;
			}
			return result;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0002DAB0 File Offset: 0x0002BCB0
		private static bool AllowedForAnonymousAccess(ADRecipient r)
		{
			bool flag = false;
			switch (r.RecipientType)
			{
			case RecipientType.User:
			case RecipientType.UserMailbox:
			case RecipientType.MailUser:
			case RecipientType.Contact:
			case RecipientType.MailContact:
				flag = true;
				break;
			}
			PIIMessage data = PIIMessage.Create(PIIType._User, r.DisplayName);
			CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, null, data, "AllowedForAnonymousAccess(_User[{0}] returning {1}.", new object[]
			{
				r.RecipientType,
				flag
			});
			return flag;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0002DB24 File Offset: 0x0002BD24
		private List<ContactSearchItem> FilterByPurpose(List<ADRecipient> results, DirectorySearchPurpose purpose)
		{
			List<ContactSearchItem> list = new List<ContactSearchItem>();
			foreach (ADRecipient r in results)
			{
				if (this.SatisfiesPurpose(r, purpose))
				{
					list.Add(ContactSearchItem.CreateFromRecipient(r));
				}
			}
			return list;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0002DB88 File Offset: 0x0002BD88
		private bool SatisfiesPurpose(ADRecipient r, DirectorySearchPurpose purpose)
		{
			bool flag = false;
			switch (purpose)
			{
			case DirectorySearchPurpose.Call:
			{
				IADOrgPerson iadorgPerson = r as IADOrgPerson;
				if (iadorgPerson != null)
				{
					if (!string.IsNullOrEmpty(r.UMExtension) || !string.IsNullOrEmpty(iadorgPerson.Phone) || !string.IsNullOrEmpty(iadorgPerson.HomePhone) || !string.IsNullOrEmpty(iadorgPerson.MobilePhone))
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, "SatisfiesPurpose::Either HomePhone,BusinessPhone or MobilePhone is present on ADOrgPerson.", new object[0]);
						flag = true;
					}
				}
				else
				{
					PIIMessage data = PIIMessage.Create(PIIType._User, r.DisplayName);
					CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, data, "SatisfiesPurpose::Recipient _User is not of type ADOrgPerson.", new object[0]);
				}
				break;
			}
			case DirectorySearchPurpose.SendMessage:
				flag = (SmtpAddress.Empty != r.PrimarySmtpAddress);
				break;
			case DirectorySearchPurpose.Both:
				flag = true;
				break;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, "SatisfiesPurpose returning: {0}", new object[]
			{
				flag
			});
			return flag;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0002DC68 File Offset: 0x0002BE68
		private bool IsInScope(ADRecipient r, UMDialPlan targetDP, DialScopeEnum scope)
		{
			bool flag;
			if (scope == DialScopeEnum.DialPlan)
			{
				IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromADRecipient(r);
				UMDialPlan dialPlanFromRecipient = iadsystemConfigurationLookup.GetDialPlanFromRecipient(r);
				PIIMessage data = PIIMessage.Create(PIIType._SmtpAddress, r.PrimarySmtpAddress);
				if (dialPlanFromRecipient == null)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, data, "IsInScope: User _SmtpAddress has no dialplan info, and Scope is Dialplan, returning false.", new object[0]);
					flag = false;
				}
				else
				{
					flag = Utils.IsIdenticalDialPlan(dialPlanFromRecipient, targetDP);
					CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, data, "IsInScope: Dialplan of User _SmtpAddress {0} originating dialplan", new object[]
					{
						flag ? "matches" : "does not match"
					});
				}
			}
			else
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, "IsInScope: DialScope is GlobalAddressList, returning TRUE.", new object[0]);
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0002DD10 File Offset: 0x0002BF10
		private bool IsEqualDtmf(MultiValuedProperty<string> dtmfMap, SearchMethod mode, string dtmf)
		{
			foreach (string text in dtmfMap)
			{
				int num = text.IndexOf(':');
				if (num != -1)
				{
					string s = text.Substring(0, num + 1);
					string strA = text.Substring(num + 1);
					SearchMethod searchMethod = SearchMethod.None;
					if (RecipientLookup.MapDtmfAttribute(s, ref searchMethod) && searchMethod == mode)
					{
						return string.Compare(strA, dtmf, true, CultureInfo.InvariantCulture) == 0;
					}
				}
			}
			return false;
		}

		// Token: 0x04000983 RID: 2435
		private static char[] nonNumerics = "*#".ToCharArray();

		// Token: 0x04000984 RID: 2436
		private IADRecipientLookup lookup;

		// Token: 0x04000985 RID: 2437
		private List<ContactSearchItem> partialMatches;

		// Token: 0x04000986 RID: 2438
		private List<ContactSearchItem> exactMatches;

		// Token: 0x04000987 RID: 2439
		private List<ADRecipient> rawMatches;

		// Token: 0x04000988 RID: 2440
		private bool searchInExistingResults;

		// Token: 0x04000989 RID: 2441
		private List<ContactSearchItem> searchDomain;

		// Token: 0x0400098A RID: 2442
		private int queryResults;

		// Token: 0x0400098B RID: 2443
		private int totalMatches;
	}
}
