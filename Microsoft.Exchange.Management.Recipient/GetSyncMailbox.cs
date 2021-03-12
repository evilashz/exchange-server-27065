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
	// Token: 0x020000C5 RID: 197
	[OutputType(new Type[]
	{
		typeof(SyncMailbox)
	})]
	[Cmdlet("Get", "SyncMailbox", DefaultParameterSetName = "Identity")]
	public sealed class GetSyncMailbox : GetMailboxOrSyncMailbox
	{
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x00035746 File Offset: 0x00033946
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<SyncMailboxSchema>();
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x0003574D File Offset: 0x0003394D
		// (set) Token: 0x06000D8D RID: 3469 RVA: 0x00035764 File Offset: 0x00033964
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

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x00035777 File Offset: 0x00033977
		// (set) Token: 0x06000D8F RID: 3471 RVA: 0x0003579C File Offset: 0x0003399C
		[ValidateRange(1, 2147483647)]
		[Parameter(ParameterSetName = "CookieSet")]
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

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x000357B4 File Offset: 0x000339B4
		// (set) Token: 0x06000D91 RID: 3473 RVA: 0x000357BC File Offset: 0x000339BC
		[Parameter(ParameterSetName = "Identity")]
		[Parameter(ParameterSetName = "MailboxPlanSet")]
		[Parameter(ParameterSetName = "ServerSet")]
		[Parameter(ParameterSetName = "AnrSet")]
		[Parameter(ParameterSetName = "DatabaseSet")]
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

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x000357C5 File Offset: 0x000339C5
		// (set) Token: 0x06000D93 RID: 3475 RVA: 0x000357CD File Offset: 0x000339CD
		[Parameter(ParameterSetName = "DatabaseSet")]
		[Parameter(ParameterSetName = "ServerSet")]
		[Parameter(ParameterSetName = "MailboxPlanSet")]
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

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x000357D6 File Offset: 0x000339D6
		// (set) Token: 0x06000D95 RID: 3477 RVA: 0x000357DE File Offset: 0x000339DE
		[Parameter(ParameterSetName = "AnrSet")]
		[Parameter(ParameterSetName = "MailboxPlanSet")]
		[Parameter(ParameterSetName = "DatabaseSet")]
		[Parameter(ParameterSetName = "Identity")]
		[Parameter(ParameterSetName = "ServerSet")]
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

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x000357E7 File Offset: 0x000339E7
		// (set) Token: 0x06000D97 RID: 3479 RVA: 0x000357EF File Offset: 0x000339EF
		[Parameter(ParameterSetName = "AnrSet")]
		[Parameter(ParameterSetName = "DatabaseSet")]
		[Parameter(ParameterSetName = "Identity")]
		[Parameter(ParameterSetName = "MailboxPlanSet")]
		[Parameter(ParameterSetName = "ServerSet")]
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

		// Token: 0x06000D98 RID: 3480 RVA: 0x000357F8 File Offset: 0x000339F8
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession recipientSession = (IRecipientSession)base.CreateSession();
			if (base.ParameterSetName == "CookieSet")
			{
				recipientSession.UseGlobalCatalog = true;
				this.inputCookie = SyncTaskHelper.ResolveSyncCookie(this.Cookie, recipientSession, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			return recipientSession;
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x00035858 File Offset: 0x00033A58
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

		// Token: 0x06000D9A RID: 3482 RVA: 0x000358A4 File Offset: 0x00033AA4
		protected override IEnumerable<ADUser> GetPagedData()
		{
			base.WriteVerbose(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(base.DataSession, typeof(ADUser), this.InternalFilter, this.RootId, this.DeepSearch));
			if (base.ParameterSetName == "CookieSet")
			{
				base.InternalResultSize = Unlimited<uint>.UnlimitedValue;
				ADPagedReader<ADUser> adpagedReader = (ADPagedReader<ADUser>)base.DataSession.FindPaged<ADUser>(this.InternalFilter, this.RootId, this.DeepSearch, this.InternalSortBy, this.PageSize);
				adpagedReader.Cookie = this.inputCookie.PageCookie;
				return adpagedReader;
			}
			return base.GetPagedData();
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00035943 File Offset: 0x00033B43
		protected override void WriteResult(IConfigurable dataObject)
		{
			((ADUser)dataObject).BypassModerationCheck = true;
			base.WriteResult(dataObject);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00035960 File Offset: 0x00033B60
		protected override void WriteResult<T>(IEnumerable<T> dataObjects)
		{
			if (base.ParameterSetName == "CookieSet")
			{
				SyncTaskHelper.HandleTaskWritePagedResult<ADUser>((IEnumerable<ADUser>)dataObjects, this.inputCookie, ref this.outputCookie, () => base.Stopping, new SyncTaskHelper.OneParameterMethod<bool, IConfigurable>(base.ShouldSkipObject), new SyncTaskHelper.VoidOneParameterMethod<IConfigurable>(this.WriteResult), this.Pages, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError));
				return;
			}
			base.WriteResult<T>(dataObjects);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x000359E9 File Offset: 0x00033BE9
		protected override bool ShouldSkipObject(IConfigurable dataObject)
		{
			return !(base.ParameterSetName == "CookieSet") && base.ShouldSkipObject(dataObject);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00035A80 File Offset: 0x00033C80
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			ADUser dataObject2 = (ADUser)dataObject;
			SyncMailbox syncMailbox = new SyncMailbox(dataObject2);
			syncMailbox.propertyBag.SetField(ADRecipientSchema.AcceptMessagesOnlyFrom, SyncTaskHelper.RetrieveFullADObjectId(base.TenantGlobalCatalogSession, syncMailbox.AcceptMessagesOnlyFrom));
			syncMailbox.propertyBag.SetField(ADRecipientSchema.AcceptMessagesOnlyFromDLMembers, SyncTaskHelper.RetrieveFullADObjectId(base.TenantGlobalCatalogSession, syncMailbox.AcceptMessagesOnlyFromDLMembers));
			syncMailbox.propertyBag.SetField(ADRecipientSchema.RejectMessagesFrom, SyncTaskHelper.RetrieveFullADObjectId(base.TenantGlobalCatalogSession, syncMailbox.RejectMessagesFrom));
			syncMailbox.propertyBag.SetField(ADRecipientSchema.RejectMessagesFromDLMembers, SyncTaskHelper.RetrieveFullADObjectId(base.TenantGlobalCatalogSession, syncMailbox.RejectMessagesFromDLMembers));
			if (syncMailbox.MailboxPlan != null)
			{
				ADUser aduser = base.ProvisioningCache.TryAddAndGetOrganizationDictionaryValue<ADUser, ADObjectId>(CannedProvisioningCacheKeys.CacheKeyMailboxPlanId, base.CurrentOrganizationId, syncMailbox.MailboxPlan, () => (ADUser)this.GetDataObject<ADUser>(new MailboxPlanIdParameter(syncMailbox.MailboxPlan), this.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxPlanNotFound(syncMailbox.MailboxPlan.ToString())), new LocalizedString?(Strings.ErrorMailboxPlanNotUnique(syncMailbox.MailboxPlan.ToString()))));
				syncMailbox.propertyBag.SetField(SyncMailboxSchema.MailboxPlanName, aduser.DisplayName);
			}
			if (this.outputCookie != null)
			{
				syncMailbox.propertyBag.SetField(SyncMailboxSchema.Cookie, this.outputCookie.ToBytes());
				if (this.outputCookie.HighWatermark == 0L)
				{
					syncMailbox.propertyBag.SetField(SyncMailboxSchema.EndOfList, true);
				}
			}
			syncMailbox.ResetChangeTracking();
			return syncMailbox;
		}

		// Token: 0x040002B6 RID: 694
		private SyncCookie inputCookie;

		// Token: 0x040002B7 RID: 695
		private SyncCookie outputCookie;
	}
}
