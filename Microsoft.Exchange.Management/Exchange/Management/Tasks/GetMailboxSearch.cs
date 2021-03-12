using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Management.Tasks.MailboxSearch;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000753 RID: 1875
	[Cmdlet("Get", "MailboxSearch", DefaultParameterSetName = "Identity")]
	public sealed class GetMailboxSearch : GetTenantADObjectWithIdentityTaskBase<EwsStoreObjectIdParameter, MailboxDiscoverySearch>
	{
		// Token: 0x17001441 RID: 5185
		// (get) Token: 0x060042AA RID: 17066 RVA: 0x00111584 File Offset: 0x0010F784
		// (set) Token: 0x060042AB RID: 17067 RVA: 0x0011159B File Offset: 0x0010F79B
		[Parameter]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x17001442 RID: 5186
		// (get) Token: 0x060042AC RID: 17068 RVA: 0x001115AE File Offset: 0x0010F7AE
		// (set) Token: 0x060042AD RID: 17069 RVA: 0x001115B6 File Offset: 0x0010F7B6
		[Parameter(Mandatory = false)]
		public Unlimited<uint> ResultSize
		{
			get
			{
				return base.InternalResultSize;
			}
			set
			{
				base.InternalResultSize = value;
			}
		}

		// Token: 0x17001443 RID: 5187
		// (get) Token: 0x060042AE RID: 17070 RVA: 0x001115BF File Offset: 0x0010F7BF
		// (set) Token: 0x060042AF RID: 17071 RVA: 0x001115D6 File Offset: 0x0010F7D6
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "InPlaceHoldIdentity")]
		public string InPlaceHoldIdentity
		{
			get
			{
				return (string)base.Fields["InPlaceHoldIdentity"];
			}
			set
			{
				base.Fields["InPlaceHoldIdentity"] = value;
			}
		}

		// Token: 0x17001444 RID: 5188
		// (get) Token: 0x060042B0 RID: 17072 RVA: 0x001115E9 File Offset: 0x0010F7E9
		// (set) Token: 0x060042B1 RID: 17073 RVA: 0x0011160F File Offset: 0x0010F80F
		[Parameter(Mandatory = false)]
		public SwitchParameter ShowDeletionInProgressSearches
		{
			get
			{
				return (SwitchParameter)(base.Fields["ShowDeletionInProgressSearches"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ShowDeletionInProgressSearches"] = value;
			}
		}

		// Token: 0x17001445 RID: 5189
		// (get) Token: 0x060042B2 RID: 17074 RVA: 0x00111627 File Offset: 0x0010F827
		protected override Unlimited<uint> DefaultResultSize
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x060042B3 RID: 17075 RVA: 0x0011162E File Offset: 0x0010F82E
		protected override IConfigDataProvider CreateSession()
		{
			return new DiscoverySearchDataProvider(base.CurrentOrganizationId);
		}

		// Token: 0x060042B4 RID: 17076 RVA: 0x0011163B File Offset: 0x0010F83B
		protected override void InternalValidate()
		{
			if (this.Identity != null && this.ShowDeletionInProgressSearches == true)
			{
				base.WriteError(new MailboxSearchTaskException(Strings.InvalidOperationIdentityWithShowDeletion), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x060042B5 RID: 17077 RVA: 0x0011166C File Offset: 0x0010F86C
		protected override void InternalProcessRecord()
		{
			if (this.InPlaceHoldIdentity != null)
			{
				MailboxDiscoverySearch mailboxDiscoverySearch = ((DiscoverySearchDataProvider)base.DataSession).FindByInPlaceHoldIdentity(this.InPlaceHoldIdentity);
				if (mailboxDiscoverySearch == null)
				{
					base.WriteError(new ManagementObjectNotFoundException(Strings.MailboxSearchObjectWithHoldIdentityNotFound(this.InPlaceHoldIdentity)), ExchangeErrorCategory.Context, null);
				}
				this.WriteResult(mailboxDiscoverySearch);
				return;
			}
			if (this.Identity != null)
			{
				string text = this.Identity.ToString();
				MailboxDataProvider mailboxDataProvider = Utils.GetMailboxDataProvider(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, new Task.TaskErrorLoggingDelegate(base.WriteError));
				if (Utils.IsLegacySearchObjectIdentity(text))
				{
					MailboxDiscoverySearch mailboxDiscoverySearch2 = ((DiscoverySearchDataProvider)base.DataSession).FindByLegacySearchObjectIdentity(text);
					if (mailboxDiscoverySearch2 != null)
					{
						this.WriteResult(mailboxDiscoverySearch2);
						return;
					}
					LocalizedString? localizedString;
					IEnumerable<SearchObject> dataObjects = base.GetDataObjects<SearchObject>(new SearchObjectIdParameter(text), mailboxDataProvider, this.RootId, base.OptionalIdentityData, out localizedString);
					foreach (SearchObject searchObject in dataObjects)
					{
						base.WriteResult(new MailboxSearchObject(searchObject, searchObject.SearchStatus ?? new SearchStatus()));
					}
					if (!base.HasErrors && base.WriteObjectCount == 0U)
					{
						base.WriteError(new ManagementObjectNotFoundException(localizedString ?? base.GetErrorMessageObjectNotFound(text, null, null)), (ErrorCategory)1003, null);
						return;
					}
				}
				else
				{
					SearchObject e14SearchObjectByName = Utils.GetE14SearchObjectByName(this.Identity.ToString(), mailboxDataProvider);
					if (e14SearchObjectByName == null)
					{
						base.InternalProcessRecord();
						return;
					}
					base.WriteResult(new MailboxSearchObject(e14SearchObjectByName, e14SearchObjectByName.SearchStatus ?? new SearchStatus()));
					return;
				}
			}
			else
			{
				base.InternalProcessRecord();
				MailboxDataProvider mailboxDataProvider2 = Utils.GetMailboxDataProvider(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, new Task.TaskErrorLoggingDelegate(base.WriteError));
				try
				{
					foreach (SearchObject searchObject2 in mailboxDataProvider2.FindPaged<SearchObject>(null, null, true, null, (int)(this.ResultSize.IsUnlimited ? 0U : (this.ResultSize.Value - base.WriteObjectCount))))
					{
						base.WriteResult(new MailboxSearchObject(searchObject2, searchObject2.SearchStatus ?? new SearchStatus()));
					}
				}
				catch (TenantAccessBlockedException exception)
				{
					base.WriteError(exception, (ErrorCategory)1003, null);
				}
			}
		}

		// Token: 0x060042B6 RID: 17078 RVA: 0x001118EC File Offset: 0x0010FAEC
		protected override void WriteResult(IConfigurable dataObject)
		{
			MailboxDiscoverySearch mailboxDiscoverySearch = dataObject as MailboxDiscoverySearch;
			if (MailboxDiscoverySearch.IsInProgressState(mailboxDiscoverySearch.Status) || MailboxDiscoverySearch.IsInDeletionState(mailboxDiscoverySearch.Status))
			{
				Utils.CreateMailboxDiscoverySearchRequest((DiscoverySearchDataProvider)base.DataSession, mailboxDiscoverySearch.Name, ActionRequestType.UpdateStatus, base.ExchangeRunspaceConfig.GetRbacContext().ToString());
			}
			if (!MailboxDiscoverySearch.IsInDeletionState(mailboxDiscoverySearch.Status) || this.Identity != null || true == this.ShowDeletionInProgressSearches)
			{
				base.WriteResult(new MailboxSearchObject(mailboxDiscoverySearch, ((DiscoverySearchDataProvider)base.DataSession).OrganizationId));
			}
		}

		// Token: 0x060042B7 RID: 17079 RVA: 0x00111980 File Offset: 0x0010FB80
		protected override OrganizationId ResolveCurrentOrganization()
		{
			if (this.Organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.RescopeToSubtree(sessionSettings), 257, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Search\\GetMailboxSearch.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
				return adorganizationalUnit.OrganizationId;
			}
			return base.CurrentOrganizationId ?? base.ExecutingUserOrganizationId;
		}
	}
}
