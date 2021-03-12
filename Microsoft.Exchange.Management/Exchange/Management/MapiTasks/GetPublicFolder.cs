using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.MapiTasks
{
	// Token: 0x0200048C RID: 1164
	[Cmdlet("Get", "PublicFolder", DefaultParameterSetName = "Identity")]
	public sealed class GetPublicFolder : GetTenantADObjectWithIdentityTaskBase<PublicFolderIdParameter, PublicFolder>
	{
		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06002928 RID: 10536 RVA: 0x000A2D6C File Offset: 0x000A0F6C
		// (set) Token: 0x06002929 RID: 10537 RVA: 0x000A2D74 File Offset: 0x000A0F74
		[Parameter(ParameterSetName = "Recurse", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[Parameter(ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[Parameter(ParameterSetName = "GetChildren", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public override PublicFolderIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x0600292A RID: 10538 RVA: 0x000A2D7D File Offset: 0x000A0F7D
		// (set) Token: 0x0600292B RID: 10539 RVA: 0x000A2DA3 File Offset: 0x000A0FA3
		[Parameter(Mandatory = true, ParameterSetName = "Recurse")]
		public SwitchParameter Recurse
		{
			get
			{
				return (SwitchParameter)(base.Fields["Recurse"] ?? false);
			}
			set
			{
				base.Fields["Recurse"] = value;
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x0600292C RID: 10540 RVA: 0x000A2DBB File Offset: 0x000A0FBB
		// (set) Token: 0x0600292D RID: 10541 RVA: 0x000A2DE1 File Offset: 0x000A0FE1
		[Parameter(Mandatory = true, ParameterSetName = "GetChildren")]
		public SwitchParameter GetChildren
		{
			get
			{
				return (SwitchParameter)(base.Fields["GetChildren"] ?? false);
			}
			set
			{
				base.Fields["GetChildren"] = value;
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x0600292E RID: 10542 RVA: 0x000A2DF9 File Offset: 0x000A0FF9
		// (set) Token: 0x0600292F RID: 10543 RVA: 0x000A2E01 File Offset: 0x000A1001
		[Parameter(ParameterSetName = "Recurse")]
		[Parameter(ParameterSetName = "GetChildren")]
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

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06002930 RID: 10544 RVA: 0x000A2E0A File Offset: 0x000A100A
		// (set) Token: 0x06002931 RID: 10545 RVA: 0x000A2E30 File Offset: 0x000A1030
		[Parameter(Mandatory = false)]
		public SwitchParameter ResidentFolders
		{
			get
			{
				return (SwitchParameter)(base.Fields["ResidentFolders"] ?? false);
			}
			set
			{
				base.Fields["ResidentFolders"] = value;
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06002932 RID: 10546 RVA: 0x000A2E48 File Offset: 0x000A1048
		// (set) Token: 0x06002933 RID: 10547 RVA: 0x000A2E5F File Offset: 0x000A105F
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

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06002934 RID: 10548 RVA: 0x000A2E72 File Offset: 0x000A1072
		// (set) Token: 0x06002935 RID: 10549 RVA: 0x000A2E89 File Offset: 0x000A1089
		[ValidateNotNull]
		[Parameter(Mandatory = false)]
		public MailboxIdParameter Mailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Mailbox"];
			}
			set
			{
				base.Fields["Mailbox"] = value;
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06002936 RID: 10550 RVA: 0x000A2E9C File Offset: 0x000A109C
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter internalFilter = base.InternalFilter;
				if (this.inputFilter == null)
				{
					return internalFilter;
				}
				if (internalFilter != null)
				{
					return new AndFilter(new QueryFilter[]
					{
						internalFilter,
						this.inputFilter
					});
				}
				return this.inputFilter;
			}
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x000A2EE0 File Offset: 0x000A10E0
		protected sealed override IConfigDataProvider CreateSession()
		{
			OrganizationIdParameter organization = null;
			if (MapiTaskHelper.IsDatacenter)
			{
				organization = MapiTaskHelper.ResolveTargetOrganizationIdParameter(this.Organization, this.Identity, base.CurrentOrganizationId, new Task.ErrorLoggerDelegate(base.ThrowTerminatingError), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			}
			base.CurrentOrganizationId = MapiTaskHelper.ResolveTargetOrganization(base.DomainController, organization, base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId);
			this.rootId = null;
			if ((this.Recurse.IsPresent || this.GetChildren.IsPresent) && this.Identity != null)
			{
				this.rootId = this.Identity.PublicFolderId;
				this.Identity = null;
			}
			if (this.publicFolderDataProvider == null || base.CurrentOrganizationId != this.publicFolderDataProvider.CurrentOrganizationId)
			{
				if (this.publicFolderDataProvider != null)
				{
					this.publicFolderDataProvider.Dispose();
					this.publicFolderDataProvider = null;
				}
				Guid mailboxGuid = Guid.Empty;
				if (base.Fields.IsModified("Mailbox"))
				{
					ADUser aduser = (ADUser)base.GetDataObject<ADUser>(this.Mailbox, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.Mailbox.ToString())), new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.Mailbox.ToString())), ExchangeErrorCategory.Client);
					this.VerifyIsPublicFolderMailbox(aduser);
					mailboxGuid = aduser.ExchangeGuid;
				}
				try
				{
					this.publicFolderDataProvider = new PublicFolderDataProvider(this.ConfigurationSession, "Get-PublicFolder", mailboxGuid);
				}
				catch (AccessDeniedException exception)
				{
					base.WriteError(exception, ErrorCategory.PermissionDenied, this.Identity);
				}
			}
			if (this.ResidentFolders.IsPresent)
			{
				this.contentMailboxGuid = this.publicFolderDataProvider.PublicFolderSession.MailboxGuid;
				this.inputFilter = new ComparisonFilter(ComparisonOperator.Equal, FolderSchema.ReplicaListBinary, this.contentMailboxGuid.ToByteArray());
			}
			return this.publicFolderDataProvider;
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x000A30C4 File Offset: 0x000A12C4
		protected override void InternalProcessRecord()
		{
			if (this.rootId != null)
			{
				base.GetDataObject<PublicFolder>(new PublicFolderIdParameter(this.rootId), base.DataSession, null, new LocalizedString?(Strings.ErrorPublicFolderNotFound(this.rootId.ToString())), new LocalizedString?(Strings.ErrorPublicFolderNotUnique(this.rootId.ToString())));
			}
			base.InternalProcessRecord();
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06002939 RID: 10553 RVA: 0x000A3122 File Offset: 0x000A1322
		protected override Unlimited<uint> DefaultResultSize
		{
			get
			{
				return new Unlimited<uint>(10000U);
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x0600293A RID: 10554 RVA: 0x000A312E File Offset: 0x000A132E
		protected override ObjectId RootId
		{
			get
			{
				return this.rootId;
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x0600293B RID: 10555 RVA: 0x000A3138 File Offset: 0x000A1338
		protected override bool DeepSearch
		{
			get
			{
				return this.Recurse.IsPresent;
			}
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x000A3153 File Offset: 0x000A1353
		protected override void InternalStateReset()
		{
			if (this.Identity == null)
			{
				this.Identity = new PublicFolderIdParameter(new PublicFolderId(MapiFolderPath.IpmSubtreeRoot));
			}
			this.contentMailboxGuid = Guid.Empty;
			this.inputFilter = null;
			base.InternalStateReset();
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x000A318A File Offset: 0x000A138A
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x000A319D File Offset: 0x000A139D
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && this.publicFolderDataProvider != null)
			{
				this.publicFolderDataProvider.Dispose();
				this.publicFolderDataProvider = null;
			}
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x000A31C3 File Offset: 0x000A13C3
		protected override void WriteResult<T>(IEnumerable<T> dataObjects)
		{
			if (dataObjects != null && this.contentMailboxGuid != Guid.Empty)
			{
				base.WriteResult<T>(this.FilterRootPublicFolder<T>(dataObjects));
				return;
			}
			base.WriteResult<T>(dataObjects);
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x000A33E0 File Offset: 0x000A15E0
		private IEnumerable<T> FilterRootPublicFolder<T>(IEnumerable<T> dataObjects)
		{
			using (IEnumerator<T> enumerator = dataObjects.GetEnumerator())
			{
				if (enumerator.MoveNext() && this.contentMailboxGuid.Equals((enumerator.Current as PublicFolder).ContentMailboxGuid))
				{
					yield return enumerator.Current;
				}
				while (enumerator.MoveNext())
				{
					!0 ! = enumerator.Current;
					yield return !;
				}
			}
			yield break;
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x000A3404 File Offset: 0x000A1604
		protected override void WriteResult(IConfigurable dataObject)
		{
			PublicFolder publicFolder = (PublicFolder)dataObject;
			TenantPublicFolderConfiguration value = TenantPublicFolderConfigurationCache.Instance.GetValue(base.CurrentOrganizationId);
			if (value != null)
			{
				if (value.GetLocalMailboxRecipient(publicFolder.ContentMailboxGuid) == null)
				{
					TenantPublicFolderConfigurationCache.Instance.RemoveValue(base.CurrentOrganizationId);
					value = TenantPublicFolderConfigurationCache.Instance.GetValue(base.CurrentOrganizationId);
				}
				PublicFolderRecipient localMailboxRecipient = value.GetLocalMailboxRecipient(publicFolder.ContentMailboxGuid);
				publicFolder.ContentMailboxName = ((localMailboxRecipient != null) ? localMailboxRecipient.MailboxName : string.Empty);
				if (base.NeedSuppressingPiiData)
				{
					publicFolder.ContentMailboxName = SuppressingPiiData.Redact(publicFolder.ContentMailboxName);
				}
				publicFolder.ResetChangeTracking();
			}
			base.WriteResult(publicFolder);
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x000A34A4 File Offset: 0x000A16A4
		private void VerifyIsPublicFolderMailbox(ADUser adUser)
		{
			if (adUser == null || adUser.RecipientTypeDetails != RecipientTypeDetails.PublicFolderMailbox)
			{
				base.WriteError(new ObjectNotFoundException(Strings.PublicFolderMailboxNotFound), ExchangeErrorCategory.Client, adUser);
			}
		}

		// Token: 0x04001E44 RID: 7748
		private const string ParameterRecurse = "Recurse";

		// Token: 0x04001E45 RID: 7749
		private const string ParameterGetChildren = "GetChildren";

		// Token: 0x04001E46 RID: 7750
		private const string ParameterSetRecurse = "Recurse";

		// Token: 0x04001E47 RID: 7751
		private const string ParameterSetGetChildren = "GetChildren";

		// Token: 0x04001E48 RID: 7752
		private const string MailboxFieldName = "Mailbox";

		// Token: 0x04001E49 RID: 7753
		private const string ParameterResidentFolders = "ResidentFolders";

		// Token: 0x04001E4A RID: 7754
		private QueryFilter inputFilter;

		// Token: 0x04001E4B RID: 7755
		private Guid contentMailboxGuid = Guid.Empty;

		// Token: 0x04001E4C RID: 7756
		private PublicFolderId rootId;

		// Token: 0x04001E4D RID: 7757
		private PublicFolderDataProvider publicFolderDataProvider;
	}
}
