using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000327 RID: 807
	internal sealed class GetSearchableMailboxes : SingleStepServiceCommand<GetSearchableMailboxesRequest, SearchableMailbox[]>, IDisposeTrackable, IDisposable
	{
		// Token: 0x060016CC RID: 5836 RVA: 0x00078E2C File Offset: 0x0007702C
		public GetSearchableMailboxes(CallContext callContext, GetSearchableMailboxesRequest request) : base(callContext, request)
		{
			if (MailboxSearchFlighting.IsFlighted(callContext, "GetSearchableMailboxes", out this.policy))
			{
				this.isFlighted = true;
				return;
			}
			this.disposeTracker = this.GetDisposeTracker();
			this.SaveRequestData(request);
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x00078E7B File Offset: 0x0007707B
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<GetSearchableMailboxes>(this);
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x00078E83 File Offset: 0x00077083
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x00078E98 File Offset: 0x00077098
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x00078EBA File Offset: 0x000770BA
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetSearchableMailboxesResponse(base.Result.Code, base.Result.Error, base.Result.Value, this.excludedMailboxes.ToArray());
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x00078EF0 File Offset: 0x000770F0
		internal override ServiceResult<SearchableMailbox[]> Execute()
		{
			if (this.isFlighted)
			{
				return MailboxSearchFlighting.GetSearchableMailboxes(this.policy, base.Request);
			}
			this.ValidateRequestData();
			MailboxSearchHelper.PerformCommonAuthorization(base.CallContext.IsExternalUser, out this.runspaceConfig, out this.recipientSession);
			SearchableMailbox[] value = this.QuerySearchableMailboxes();
			return new ServiceResult<SearchableMailbox[]>(value);
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x00078F46 File Offset: 0x00077146
		private void Dispose(bool fromDispose)
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			if (!this.disposed)
			{
				this.disposed = true;
			}
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x00078F6A File Offset: 0x0007716A
		private void SaveRequestData(GetSearchableMailboxesRequest request)
		{
			this.searchFilter = ((request.SearchFilter == null) ? request.SearchFilter : request.SearchFilter.Trim());
			this.expandGroupMembership = request.ExpandGroupMembership;
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x00078F9C File Offset: 0x0007719C
		private void ValidateRequestData()
		{
			if (this.expandGroupMembership && (string.IsNullOrEmpty(this.searchFilter) || this.IsWildcard(this.searchFilter)))
			{
				throw new ServiceArgumentException((CoreResources.IDs)4083587704U);
			}
			if (this.IsSuffixSearchWildcard(this.searchFilter))
			{
				throw new ServiceArgumentException(CoreResources.IDs.ErrorSuffixSearchNotAllowed);
			}
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00078FFC File Offset: 0x000771FC
		private SearchableMailbox[] QuerySearchableMailboxes()
		{
			List<QueryFilter> additionalSearchFilters = this.GetAdditionalSearchFilters();
			SortBy sortBy = new SortBy(MiniRecipientSchema.DisplayName, SortOrder.Ascending);
			ADPagedReader<MiniRecipient> adpagedReader = MailboxSearchHelper.QueryADObject(this.recipientSession, additionalSearchFilters, sortBy);
			List<SearchableMailbox> list = new List<SearchableMailbox>();
			foreach (MiniRecipient miniRecipient in adpagedReader)
			{
				bool flag = MailboxSearchHelper.IsMembershipGroup(miniRecipient);
				if (flag && this.expandGroupMembership)
				{
					using (Dictionary<ADObjectId, ADRawEntry>.Enumerator enumerator2 = MailboxSearchHelper.ProcessGroupExpansion(this.recipientSession, miniRecipient, this.recipientSession.SessionSettings.CurrentOrganizationId).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							KeyValuePair<ADObjectId, ADRawEntry> keyValuePair = enumerator2.Current;
							if (keyValuePair.Value != null)
							{
								MailboxSearchHelper.VerifyAndAddSearchableMailboxToCollection(list, keyValuePair.Value, false, this.runspaceConfig, this.excludedMailboxes, true);
								if (list.Count > 1500)
								{
									ExTraceGlobals.SearchTracer.TraceDebug<int>((long)this.GetHashCode(), "Reach max limit ({0}) of total recipients to return", 1500);
									throw new ServiceArgumentException(CoreResources.IDs.ErrorADSessionFilter, CoreResources.ErrorReturnTooManyMailboxesFromDG(miniRecipient.DisplayName, 1500));
								}
							}
							else
							{
								ExTraceGlobals.SearchTracer.TraceError<string>((long)this.GetHashCode(), "Unable to find mailbox {0}", keyValuePair.Key.Name);
							}
						}
						continue;
					}
				}
				MailboxSearchHelper.VerifyAndAddSearchableMailboxToCollection(list, miniRecipient, flag, this.runspaceConfig, this.excludedMailboxes, true);
				if (list.Count >= 1500)
				{
					ExTraceGlobals.SearchTracer.TraceDebug<int>((long)this.GetHashCode(), "Reach max limit ({0}) of total recipients to return", 1500);
					break;
				}
			}
			list.Sort();
			return list.ToArray();
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x000791DC File Offset: 0x000773DC
		private List<QueryFilter> GetAdditionalSearchFilters()
		{
			List<QueryFilter> list = new List<QueryFilter>();
			if (!string.IsNullOrEmpty(this.searchFilter))
			{
				if (this.searchFilter.StartsWith("*") && this.searchFilter.EndsWith("*") && this.searchFilter.Length <= 2)
				{
					return list;
				}
				Guid empty = Guid.Empty;
				if (Guid.TryParse(this.searchFilter, out empty))
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, empty));
					return list;
				}
				SmtpAddress smtpAddress = new SmtpAddress(this.searchFilter);
				if (smtpAddress.IsValidAddress)
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.EmailAddresses, "SMTP:" + smtpAddress.ToString()));
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.ExternalEmailAddress, "SMTP:" + smtpAddress.ToString()));
					return list;
				}
				list.Add(this.CreateWildcardOrEqualFilter(ADUserSchema.UserPrincipalName, this.searchFilter));
				list.Add(this.CreateWildcardOrEqualFilter(ADRecipientSchema.Alias, this.searchFilter));
				list.Add(this.CreateWildcardOrEqualFilter(ADUserSchema.FirstName, this.searchFilter));
				list.Add(this.CreateWildcardOrEqualFilter(ADUserSchema.LastName, this.searchFilter));
				list.Add(this.CreateWildcardOrEqualFilter(ADRecipientSchema.DisplayName, this.searchFilter));
			}
			return list;
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x00079340 File Offset: 0x00077540
		private QueryFilter CreateWildcardOrEqualFilter(ADPropertyDefinition schemaProperty, string searchFilter)
		{
			if (this.IsWildcard(searchFilter))
			{
				string text = searchFilter.Substring(0, searchFilter.Length - 1);
				return new TextFilter(schemaProperty, text, MatchOptions.Prefix, MatchFlags.IgnoreCase);
			}
			return new ComparisonFilter(ComparisonOperator.Equal, schemaProperty, searchFilter);
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x0007937A File Offset: 0x0007757A
		private bool IsWildcard(string s)
		{
			return !string.IsNullOrEmpty(s) && (s.StartsWith("*") || s.EndsWith("*"));
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x000793A1 File Offset: 0x000775A1
		private bool IsSuffixSearchWildcard(string s)
		{
			return !string.IsNullOrEmpty(s) && s.StartsWith("*") && s.Length > 1;
		}

		// Token: 0x04000F61 RID: 3937
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04000F62 RID: 3938
		private bool disposed;

		// Token: 0x04000F63 RID: 3939
		private string searchFilter;

		// Token: 0x04000F64 RID: 3940
		private bool expandGroupMembership;

		// Token: 0x04000F65 RID: 3941
		private ExchangeRunspaceConfiguration runspaceConfig;

		// Token: 0x04000F66 RID: 3942
		private IRecipientSession recipientSession;

		// Token: 0x04000F67 RID: 3943
		private List<FailedSearchMailbox> excludedMailboxes = new List<FailedSearchMailbox>(1);

		// Token: 0x04000F68 RID: 3944
		private readonly bool isFlighted;

		// Token: 0x04000F69 RID: 3945
		private readonly ISearchPolicy policy;
	}
}
