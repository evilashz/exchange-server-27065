using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002EE RID: 750
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FindPeople : SingleStepServiceCommand<FindPeopleRequest, FindPeopleResult>
	{
		// Token: 0x06001520 RID: 5408 RVA: 0x0006C21C File Offset: 0x0006A41C
		public FindPeople(CallContext callContext, FindPeopleRequest request) : base(callContext, request)
		{
			this.request = request;
			this.callContext = callContext;
			this.InitializeTracers();
			OwsLogRegistry.Register(FindPeople.FindPeopleActionName, typeof(FindPeopleMetadata), new Type[0]);
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x0006C278 File Offset: 0x0006A478
		private static ADObjectId GetAddressListId(AddressListId addressListId)
		{
			Guid guid;
			if (Guid.TryParse(addressListId.Id, out guid))
			{
				return new ADObjectId(guid);
			}
			throw new ServiceArgumentException((CoreResources.IDs)3784063568U);
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0006C2AA File Offset: 0x0006A4AA
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new FindPeopleResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0006C2D4 File Offset: 0x0006A4D4
		internal override ServiceResult<FindPeopleResult> Execute()
		{
			DateTime utcNow = DateTime.UtcNow;
			this.findPeopleImplementation = this.CreateFindPeopleImplementation();
			this.findPeopleImplementation.Validate();
			FindPeopleResult findPeopleResult = this.findPeopleImplementation.Execute();
			if (this.request.ShouldResolveOneOffEmailAddress)
			{
				if (this.request.ParentFolderId != null)
				{
					this.tracer.TraceError((long)this.GetHashCode(), "ShouldResolveOneOffEmailAddress is set to true but scope of the search is not set to both mailbox and directory.");
					throw new ServiceArgumentException((CoreResources.IDs)3784063568U);
				}
				if (findPeopleResult.PersonaList.Length == 0)
				{
					this.tracer.TraceDebug((long)this.GetHashCode(), "FindPeople:Execute method calling CreatePersonaIfQueryStringIsValidAddress.");
					findPeopleResult = this.CreatePersonaIfQueryStringIsValidAddress();
				}
			}
			ServiceResult<FindPeopleResult> result = new ServiceResult<FindPeopleResult>(findPeopleResult);
			DateTime utcNow2 = DateTime.UtcNow;
			this.findPeopleImplementation.Logger.Set(FindPeopleMetadata.CommandExecutionStart, utcNow.ToUniversalTime().ToString("o"));
			this.findPeopleImplementation.Logger.Set(FindPeopleMetadata.CommandExecutionEnd, utcNow2.ToUniversalTime().ToString("o"));
			return result;
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0006C3DB File Offset: 0x0006A5DB
		protected override void LogTracesForCurrentRequest()
		{
			ServiceCommandBase.TraceLoggerFactory.Create(base.CallContext.HttpContext.Response.Headers).LogTraces(this.requestTracer);
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0006C408 File Offset: 0x0006A608
		private void InitializeTracers()
		{
			ITracer tracer;
			if (!base.IsRequestTracingEnabled)
			{
				ITracer instance = NullTracer.Instance;
				tracer = instance;
			}
			else
			{
				tracer = new InMemoryTracer(ExTraceGlobals.FindPeopleCallTracer.Category, ExTraceGlobals.FindPeopleCallTracer.TraceTag);
			}
			this.requestTracer = tracer;
			this.tracer = ExTraceGlobals.FindPeopleCallTracer.Compose(this.requestTracer);
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0006C45C File Offset: 0x0006A65C
		private ADObjectId GetGlobalAddressListId()
		{
			IExchangePrincipal mailboxOwner = base.MailboxIdentityMailboxSession.MailboxOwner;
			return this.GetGlobalAddressListId(mailboxOwner.MailboxInfo.Configuration.AddressBookPolicy, this.callContext.EffectiveCaller.ClientSecurityContext, base.MailboxIdentityMailboxSession.GetADConfigurationSession(true, ConsistencyMode.IgnoreInvalid), base.MailboxIdentityMailboxSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid));
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0006C4B5 File Offset: 0x0006A6B5
		private ADObjectId GetGlobalAddressListId(ADObjectId addressBookPolicyId, ClientSecurityContext clientSecurityContext, IConfigurationSession configurationSession, IRecipientSession recipientSession)
		{
			if (!addressBookPolicyId.IsNullOrEmpty())
			{
				return DirectoryHelper.GetGlobalAddressListFromAddressBookPolicy(addressBookPolicyId, configurationSession);
			}
			return this.GetGlobalAddressListInAbsenceOfABP(clientSecurityContext, configurationSession, recipientSession);
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0006C4D4 File Offset: 0x0006A6D4
		private ADObjectId GetGlobalAddressListInAbsenceOfABP(ClientSecurityContext clientSecurityContext, IConfigurationSession configurationSession, IRecipientSession recipientSession)
		{
			AddressBookBase globalAddressList = AddressBookBase.GetGlobalAddressList(clientSecurityContext, configurationSession, recipientSession, null);
			if (globalAddressList != null)
			{
				return globalAddressList.Id;
			}
			return null;
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0006C4F8 File Offset: 0x0006A6F8
		private IdAndSession GetFolderIdAndSession()
		{
			ServiceCommandBase.ThrowIfNull(this.request.ParentFolderId, "ParentFolderId", "FindPeople::GetFolderIdAndSession");
			ServiceCommandBase.ThrowIfNull(this.request.ParentFolderId.BaseFolderId, "ParentFolderId", "FindPeople::GetFolderIdAndSession");
			return base.IdConverter.ConvertFolderIdToIdAndSession(this.request.ParentFolderId.BaseFolderId, IdConverter.ConvertOption.IgnoreChangeKey);
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0006C55C File Offset: 0x0006A75C
		private FindPeopleImplementation CreateFindPeopleImplementation()
		{
			FindPeopleParameters parameters = new FindPeopleParameters
			{
				QueryString = this.request.QueryString,
				SortResults = this.request.SortOrder,
				Paging = this.request.Paging,
				Restriction = this.request.Restriction,
				AggregationRestriction = this.request.AggregationRestriction,
				PersonaShape = this.request.PersonaShape,
				CultureInfo = this.callContext.ClientCulture,
				Logger = this.callContext.ProtocolLog
			};
			if (this.request.QueryString == null)
			{
				return this.CreateFindPeopleBrowseImplementation(parameters);
			}
			return this.CreateFindPeopleSearchImplemenation(parameters);
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0006C618 File Offset: 0x0006A818
		private FindPeopleImplementation CreateFindPeopleBrowseImplementation(FindPeopleParameters parameters)
		{
			ServiceCommandBase.ThrowIfNull(this.request.ParentFolderId, "ParentFolderId", "FindPeople::CreateFindPeopleImplementation");
			AddressListId addressListId = this.request.ParentFolderId.BaseFolderId as AddressListId;
			if (addressListId != null)
			{
				MailboxSession mailboxSessionOrFail = this.GetMailboxSessionOrFail();
				return new BrowsePeopleInDirectory(parameters, this.callContext.ADRecipientSessionContext.OrganizationId, FindPeople.GetAddressListId(addressListId), mailboxSessionOrFail);
			}
			IdAndSession folderIdAndSession = this.GetFolderIdAndSession();
			if (folderIdAndSession.Session.IsPublicFolderSession)
			{
				if (this.request.ParentFolderId.BaseFolderId is FolderId || this.request.ParentFolderId.BaseFolderId is DistinguishedFolderId)
				{
					return new BrowsePeopleInPublicFolder(parameters, folderIdAndSession);
				}
				throw new ServiceArgumentException((CoreResources.IDs)3784063568U);
			}
			else
			{
				if (ClientInfo.OWA.IsMatch(folderIdAndSession.Session.ClientInfoString))
				{
					MailboxSession mailboxSessionOrFail2 = this.GetMailboxSessionOrFail();
					StoreId defaultFolderId = mailboxSessionOrFail2.GetDefaultFolderId(DefaultFolderType.FromFavoriteSenders);
					if (defaultFolderId != null && defaultFolderId.Equals(folderIdAndSession.Id))
					{
						this.tracer.TraceDebug((long)this.GetHashCode(), "FindPeople.CreateFindPeopleImplementation. Calling BrowsePeopleInMailFolder.");
						return new BrowsePeopleInMailFolder(parameters, mailboxSessionOrFail2, folderIdAndSession.Id, this.tracer);
					}
				}
				if (this.request.ParentFolderId.BaseFolderId is FolderId || this.request.ParentFolderId.BaseFolderId is DistinguishedFolderId)
				{
					MailboxSession mailboxSessionOrFail3 = this.GetMailboxSessionOrFail();
					return new BrowsePeopleInMailbox(parameters, mailboxSessionOrFail3, folderIdAndSession);
				}
				throw new ServiceArgumentException((CoreResources.IDs)3784063568U);
			}
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0006C78C File Offset: 0x0006A98C
		private FindPeopleImplementation CreateFindPeopleSearchImplemenation(FindPeopleParameters parameters)
		{
			if (this.request.ParentFolderId == null)
			{
				MailboxSession mailboxSessionOrFail = this.GetMailboxSessionOrFail();
				return new SearchPeopleInMailboxAndDirectory(parameters, mailboxSessionOrFail, this.callContext.ADRecipientSessionContext.OrganizationId, mailboxSessionOrFail.GetDefaultFolderId(DefaultFolderType.Contacts), this.GetGlobalAddressListIdOrFail());
			}
			DistinguishedFolderId distinguishedFolderId = this.request.ParentFolderId.BaseFolderId as DistinguishedFolderId;
			if (distinguishedFolderId != null && distinguishedFolderId.Id == DistinguishedFolderIdName.directory)
			{
				MailboxSession mailboxSessionOrFail2 = this.GetMailboxSessionOrFail();
				return new SearchPeopleInDirectory(parameters, this.callContext.ADRecipientSessionContext.OrganizationId, this.GetGlobalAddressListIdOrFail(), mailboxSessionOrFail2, null);
			}
			AddressListId addressListId = this.request.ParentFolderId.BaseFolderId as AddressListId;
			if (addressListId != null)
			{
				MailboxSession mailboxSessionOrFail3 = this.GetMailboxSessionOrFail();
				return new SearchPeopleInDirectory(parameters, this.callContext.ADRecipientSessionContext.OrganizationId, FindPeople.GetAddressListId(addressListId), mailboxSessionOrFail3, null);
			}
			if (!(this.request.ParentFolderId.BaseFolderId is FolderId) && !(this.request.ParentFolderId.BaseFolderId is DistinguishedFolderId))
			{
				throw new ServiceArgumentException((CoreResources.IDs)3784063568U);
			}
			IdAndSession folderIdAndSession = this.GetFolderIdAndSession();
			if (folderIdAndSession.Session.IsPublicFolderSession)
			{
				return new SearchPeopleInPublicFolder(parameters, folderIdAndSession);
			}
			MailboxSession mailboxSessionOrFail4 = this.GetMailboxSessionOrFail();
			return new SearchPeopleInMailbox(parameters, folderIdAndSession, new PeopleAggregationExtension(mailboxSessionOrFail4));
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0006C8D0 File Offset: 0x0006AAD0
		private MailboxSession GetMailboxSessionOrFail()
		{
			MailboxSession mailboxIdentityMailboxSession = base.MailboxIdentityMailboxSession;
			if (mailboxIdentityMailboxSession == null || mailboxIdentityMailboxSession.MailboxOwner == null)
			{
				throw new ServiceArgumentException((CoreResources.IDs)3784063568U);
			}
			return mailboxIdentityMailboxSession;
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x0006C900 File Offset: 0x0006AB00
		private ADObjectId GetGlobalAddressListIdOrFail()
		{
			ADObjectId globalAddressListId = this.GetGlobalAddressListId();
			if (globalAddressListId == null)
			{
				throw new ServiceArgumentException((CoreResources.IDs)3784063568U);
			}
			return globalAddressListId;
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0006C928 File Offset: 0x0006AB28
		private FindPeopleResult CreatePersonaIfQueryStringIsValidAddress()
		{
			string queryString = this.request.QueryString;
			if (string.IsNullOrWhiteSpace(queryString) || !SmtpAddress.IsValidSmtpAddress(queryString))
			{
				return FindPeopleResult.CreateSearchResult(Array<Persona>.Empty);
			}
			this.tracer.TraceDebug<string>((long)this.GetHashCode(), "FindPeople:CreatePersonaIfQueryStringIsValidAddress - QueryString is a valid smtp address {0}", queryString);
			EmailAddressWrapper emailAddressWrapper = new EmailAddressWrapper
			{
				Name = queryString,
				EmailAddress = queryString,
				RoutingType = "SMTP",
				MailboxType = MailboxHelper.MailboxTypeType.OneOff.ToString()
			};
			Persona persona = new Persona
			{
				PersonaId = IdConverter.PersonaIdFromPersonId(base.MailboxIdentityMailboxSession.MailboxGuid, PersonId.CreateNew()),
				DisplayName = queryString,
				EmailAddress = emailAddressWrapper,
				EmailAddresses = new EmailAddressWrapper[]
				{
					emailAddressWrapper
				},
				PersonaType = FindPeople.PersonaTypePerson
			};
			return FindPeopleResult.CreateSearchResult(new Persona[]
			{
				persona
			});
		}

		// Token: 0x04000E5B RID: 3675
		private static readonly string FindPeopleActionName = typeof(FindPeople).Name;

		// Token: 0x04000E5C RID: 3676
		private static readonly string PersonaTypePerson = PersonaTypeConverter.ToString(PersonType.Person);

		// Token: 0x04000E5D RID: 3677
		private readonly CallContext callContext;

		// Token: 0x04000E5E RID: 3678
		private readonly FindPeopleRequest request;

		// Token: 0x04000E5F RID: 3679
		private ITracer tracer = ExTraceGlobals.FindPeopleCallTracer;

		// Token: 0x04000E60 RID: 3680
		private ITracer requestTracer = NullTracer.Instance;

		// Token: 0x04000E61 RID: 3681
		private FindPeopleImplementation findPeopleImplementation;
	}
}
