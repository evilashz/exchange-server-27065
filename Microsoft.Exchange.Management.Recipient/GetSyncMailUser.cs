using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000CE RID: 206
	[Cmdlet("Get", "SyncMailUser", DefaultParameterSetName = "Identity")]
	public sealed class GetSyncMailUser : GetMailUserBase<MailUserIdParameter>
	{
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x0003A682 File Offset: 0x00038882
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<SyncMailUserSchema>();
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x0003A689 File Offset: 0x00038889
		// (set) Token: 0x06000FFB RID: 4091 RVA: 0x0003A6A0 File Offset: 0x000388A0
		[Parameter(ParameterSetName = "CookieSet")]
		public byte[] Cookie
		{
			get
			{
				return (byte[])base.Fields["Cookie"];
			}
			set
			{
				base.Fields["Cookie"] = value;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06000FFC RID: 4092 RVA: 0x0003A6B3 File Offset: 0x000388B3
		// (set) Token: 0x06000FFD RID: 4093 RVA: 0x0003A6D8 File Offset: 0x000388D8
		[Parameter(ParameterSetName = "CookieSet")]
		[ValidateRange(1, 2147483647)]
		public int Pages
		{
			get
			{
				return (int)(base.Fields["Pages"] ?? int.MaxValue);
			}
			set
			{
				base.Fields["Pages"] = value;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x0003A6F0 File Offset: 0x000388F0
		// (set) Token: 0x06000FFF RID: 4095 RVA: 0x0003A6F8 File Offset: 0x000388F8
		[Parameter(ParameterSetName = "Identity")]
		[Parameter(ParameterSetName = "AnrSet")]
		public new Unlimited<uint> ResultSize
		{
			get
			{
				return base.ResultSize;
			}
			set
			{
				base.ResultSize = value;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001000 RID: 4096 RVA: 0x0003A701 File Offset: 0x00038901
		// (set) Token: 0x06001001 RID: 4097 RVA: 0x0003A709 File Offset: 0x00038909
		[Parameter(ParameterSetName = "AnrSet")]
		[Parameter(ParameterSetName = "Identity")]
		public new SwitchParameter ReadFromDomainController
		{
			get
			{
				return base.ReadFromDomainController;
			}
			set
			{
				base.ReadFromDomainController = value;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001002 RID: 4098 RVA: 0x0003A712 File Offset: 0x00038912
		// (set) Token: 0x06001003 RID: 4099 RVA: 0x0003A71A File Offset: 0x0003891A
		[Parameter(ParameterSetName = "Identity")]
		[Parameter(ParameterSetName = "AnrSet")]
		public new SwitchParameter IgnoreDefaultScope
		{
			get
			{
				return base.IgnoreDefaultScope;
			}
			set
			{
				base.IgnoreDefaultScope = value;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x0003A723 File Offset: 0x00038923
		// (set) Token: 0x06001005 RID: 4101 RVA: 0x0003A72B File Offset: 0x0003892B
		[Parameter(ParameterSetName = "Identity")]
		[Parameter(ParameterSetName = "AnrSet")]
		public new string SortBy
		{
			get
			{
				return base.SortBy;
			}
			set
			{
				base.SortBy = value;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x0003A734 File Offset: 0x00038934
		// (set) Token: 0x06001007 RID: 4103 RVA: 0x0003A73C File Offset: 0x0003893C
		[Parameter(Mandatory = false)]
		public SwitchParameter SoftDeletedMailUser
		{
			get
			{
				return base.SoftDeletedObject;
			}
			set
			{
				base.SoftDeletedObject = value;
			}
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0003A748 File Offset: 0x00038948
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession recipientSession = (IRecipientSession)base.CreateSession();
			ADObjectId rootId = recipientSession.SearchRoot;
			if (this.SoftDeletedMailUser.IsPresent && base.CurrentOrganizationId != null && base.CurrentOrganizationId.OrganizationalUnit != null)
			{
				rootId = new ADObjectId("OU=Soft Deleted Objects," + base.CurrentOrganizationId.OrganizationalUnit.DistinguishedName);
			}
			if (this.SoftDeletedMailUser.IsPresent)
			{
				recipientSession = SoftDeletedTaskHelper.GetSessionForSoftDeletedObjects(recipientSession, rootId);
			}
			if (base.ParameterSetName == "CookieSet")
			{
				recipientSession.UseGlobalCatalog = true;
				this.inputCookie = SyncTaskHelper.ResolveSyncCookie(this.Cookie, recipientSession, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			return recipientSession;
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x0003A810 File Offset: 0x00038A10
		protected override QueryFilter InternalFilter
		{
			get
			{
				if (base.ParameterSetName == "CookieSet")
				{
					return new AndFilter(new QueryFilter[]
					{
						base.InternalFilter,
						SyncTaskHelper.GetDeltaFilter(this.inputCookie)
					});
				}
				return base.InternalFilter;
			}
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0003A85C File Offset: 0x00038A5C
		protected override IEnumerable<ADUser> GetPagedData()
		{
			if (base.ParameterSetName == "CookieSet")
			{
				base.WriteVerbose(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(base.DataSession, typeof(ADUser), this.InternalFilter, this.RootId, this.DeepSearch));
				base.InternalResultSize = Unlimited<uint>.UnlimitedValue;
				ADPagedReader<ADUser> adpagedReader = (ADPagedReader<ADUser>)base.DataSession.FindPaged<ADUser>(this.InternalFilter, this.RootId, this.DeepSearch, this.InternalSortBy, this.PageSize);
				adpagedReader.Cookie = this.inputCookie.PageCookie;
				return adpagedReader;
			}
			return base.GetPagedData();
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0003A8FB File Offset: 0x00038AFB
		protected override void WriteResult(IConfigurable dataObject)
		{
			((ADUser)dataObject).BypassModerationCheck = true;
			base.WriteResult(dataObject);
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0003A918 File Offset: 0x00038B18
		protected override void WriteResult<T>(IEnumerable<T> dataObjects)
		{
			if (base.ParameterSetName == "CookieSet")
			{
				SyncTaskHelper.HandleTaskWritePagedResult<ADUser>((IEnumerable<ADUser>)dataObjects, this.inputCookie, ref this.outputCookie, () => base.Stopping, new SyncTaskHelper.OneParameterMethod<bool, IConfigurable>(base.ShouldSkipObject), new SyncTaskHelper.VoidOneParameterMethod<IConfigurable>(this.WriteResult), this.Pages, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError));
				return;
			}
			base.WriteResult<T>(dataObjects);
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0003A9A1 File Offset: 0x00038BA1
		protected override bool ShouldSkipObject(IConfigurable dataObject)
		{
			return !(base.ParameterSetName == "CookieSet") && base.ShouldSkipObject(dataObject);
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0003AA38 File Offset: 0x00038C38
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			ADUser dataObject2 = (ADUser)dataObject;
			SyncMailUser syncMailUser = new SyncMailUser(dataObject2);
			if (syncMailUser.IntendedMailboxPlan != null)
			{
				ADUser aduser = base.ProvisioningCache.TryAddAndGetOrganizationDictionaryValue<ADUser, ADObjectId>(CannedProvisioningCacheKeys.CacheKeyMailboxPlanId, base.CurrentOrganizationId, syncMailUser.IntendedMailboxPlan, () => (ADUser)this.GetDataObject<ADUser>(new MailboxPlanIdParameter(syncMailUser.IntendedMailboxPlan), this.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxPlanNotFound(syncMailUser.IntendedMailboxPlan.ToString())), new LocalizedString?(Strings.ErrorMailboxPlanNotUnique(syncMailUser.IntendedMailboxPlan.ToString()))));
				syncMailUser.propertyBag.SetField(SyncMailUserSchema.IntendedMailboxPlanName, aduser.DisplayName);
			}
			syncMailUser.propertyBag.SetField(ADRecipientSchema.AcceptMessagesOnlyFrom, SyncTaskHelper.RetrieveFullADObjectId(base.TenantGlobalCatalogSession, syncMailUser.AcceptMessagesOnlyFrom));
			syncMailUser.propertyBag.SetField(ADRecipientSchema.AcceptMessagesOnlyFromDLMembers, SyncTaskHelper.RetrieveFullADObjectId(base.TenantGlobalCatalogSession, syncMailUser.AcceptMessagesOnlyFromDLMembers));
			syncMailUser.propertyBag.SetField(ADRecipientSchema.RejectMessagesFrom, SyncTaskHelper.RetrieveFullADObjectId(base.TenantGlobalCatalogSession, syncMailUser.RejectMessagesFrom));
			syncMailUser.propertyBag.SetField(ADRecipientSchema.RejectMessagesFromDLMembers, SyncTaskHelper.RetrieveFullADObjectId(base.TenantGlobalCatalogSession, syncMailUser.RejectMessagesFromDLMembers));
			if (this.outputCookie != null)
			{
				syncMailUser.propertyBag.SetField(SyncMailUserSchema.Cookie, this.outputCookie.ToBytes());
				if (this.outputCookie.HighWatermark == 0L)
				{
					syncMailUser.propertyBag.SetField(SyncMailUserSchema.EndOfList, true);
				}
			}
			syncMailUser.ResetChangeTracking();
			return syncMailUser;
		}

		// Token: 0x040002DF RID: 735
		private SyncCookie inputCookie;

		// Token: 0x040002E0 RID: 736
		private SyncCookie outputCookie;
	}
}
