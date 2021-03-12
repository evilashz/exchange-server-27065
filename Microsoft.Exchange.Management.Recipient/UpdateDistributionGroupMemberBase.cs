using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000032 RID: 50
	public abstract class UpdateDistributionGroupMemberBase : RecipientObjectActionTask<DistributionGroupIdParameter, ADGroup>
	{
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000C961 File Offset: 0x0000AB61
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageUpdateDistributionGroupMember(this.Identity.ToString());
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000C973 File Offset: 0x0000AB73
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0000C98A File Offset: 0x0000AB8A
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public MultiValuedProperty<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>> Members
		{
			get
			{
				return (MultiValuedProperty<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>>)base.Fields["Members"];
			}
			set
			{
				base.Fields["Members"] = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000C99D File Offset: 0x0000AB9D
		// (set) Token: 0x06000270 RID: 624 RVA: 0x0000C9C3 File Offset: 0x0000ABC3
		[Parameter(Mandatory = false)]
		public SwitchParameter BypassSecurityGroupManagerCheck
		{
			get
			{
				return (SwitchParameter)(base.Fields["BypassSecurityGroupManagerCheck"] ?? false);
			}
			set
			{
				base.Fields["BypassSecurityGroupManagerCheck"] = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000C9DB File Offset: 0x0000ABDB
		internal IRecipientSession GlobalCatalogRBACSession
		{
			get
			{
				return this.globalCatalogRBACSession;
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000C9E3 File Offset: 0x0000ABE3
		protected override IConfigDataProvider CreateSession()
		{
			base.SessionSettings.IncludeSoftDeletedObjectLinks = true;
			this.globalCatalogRBACSession = DistributionGroupMemberTaskBase<RecipientIdParameter>.CreateGlobalCatalogRBACSession(base.DomainController, base.SessionSettings);
			return base.CreateSession();
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000CA10 File Offset: 0x0000AC10
		protected override IConfigurable ResolveDataObject()
		{
			bool skipRangedAttributes = ((IDirectorySession)base.DataSession).SkipRangedAttributes;
			bool skipRangedAttributes2 = base.TenantGlobalCatalogSession.SkipRangedAttributes;
			((IDirectorySession)base.DataSession).SkipRangedAttributes = this.ShouldSkipRangedAttributes();
			base.TenantGlobalCatalogSession.SkipRangedAttributes = this.ShouldSkipRangedAttributes();
			base.TenantGlobalCatalogSession.SessionSettings.IncludeSoftDeletedObjectLinks = true;
			IConfigurable result = base.ResolveDataObject();
			((IDirectorySession)base.DataSession).SkipRangedAttributes = skipRangedAttributes;
			base.TenantGlobalCatalogSession.SkipRangedAttributes = skipRangedAttributes;
			return result;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000CA97 File Offset: 0x0000AC97
		protected virtual bool ShouldSkipRangedAttributes()
		{
			return true;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000CA9C File Offset: 0x0000AC9C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.DataObject.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox || this.DataObject.RecipientTypeDetails == RecipientTypeDetails.RemoteGroupMailbox)
			{
				base.WriteError(new RecipientTaskException(Strings.NotAValidDistributionGroup), ExchangeErrorCategory.Client, this.Identity.ToString());
			}
			if (base.HasErrors)
			{
				return;
			}
			if (base.Fields.IsModified("Members"))
			{
				DistributionGroupMemberTaskBase<DistributionGroupIdParameter>.GetExecutingUserAndCheckGroupOwnership(this, (IDirectorySession)base.DataSession, base.TenantGlobalCatalogSession, this.DataObject, this.BypassSecurityGroupManagerCheck);
				MultiValuedProperty<ADObjectId> multiValuedProperty = new MultiValuedProperty<ADObjectId>();
				foreach (ADObjectId adobjectId in this.DataObject.Members)
				{
					if (-1 != adobjectId.DistinguishedName.IndexOf(",OU=Soft Deleted Objects,", StringComparison.OrdinalIgnoreCase))
					{
						multiValuedProperty.Add(adobjectId);
					}
				}
				this.DataObject.Members = multiValuedProperty;
				this.DataObject.Members.IsCompletelyRead = true;
				if (this.Members != null && this.Members.Count > 0)
				{
					foreach (RecipientWithAdUserGroupIdParameter<RecipientIdParameter> member in this.Members)
					{
						MailboxTaskHelper.ValidateAndAddMember(base.TenantGlobalCatalogSession, this.DataObject, member, false, new Task.ErrorLoggerDelegate(base.WriteError), new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>));
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04000054 RID: 84
		private IRecipientSession globalCatalogRBACSession;
	}
}
