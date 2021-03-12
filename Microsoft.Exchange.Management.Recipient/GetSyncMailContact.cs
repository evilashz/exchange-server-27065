using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000CA RID: 202
	[OutputType(new Type[]
	{
		typeof(SyncMailContact)
	})]
	[Cmdlet("Get", "SyncMailContact", DefaultParameterSetName = "Identity")]
	public sealed class GetSyncMailContact : GetMailContactBase
	{
		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x00037926 File Offset: 0x00035B26
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<SyncMailContactSchema>();
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x0003792D File Offset: 0x00035B2D
		// (set) Token: 0x06000EBB RID: 3771 RVA: 0x00037944 File Offset: 0x00035B44
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

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x00037957 File Offset: 0x00035B57
		// (set) Token: 0x06000EBD RID: 3773 RVA: 0x0003797C File Offset: 0x00035B7C
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

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x00037994 File Offset: 0x00035B94
		// (set) Token: 0x06000EBF RID: 3775 RVA: 0x0003799C File Offset: 0x00035B9C
		[Parameter(ParameterSetName = "AnrSet")]
		[Parameter(ParameterSetName = "Identity")]
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

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x000379A5 File Offset: 0x00035BA5
		// (set) Token: 0x06000EC1 RID: 3777 RVA: 0x000379AD File Offset: 0x00035BAD
		[Parameter(ParameterSetName = "Identity")]
		[Parameter(ParameterSetName = "AnrSet")]
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

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x000379B6 File Offset: 0x00035BB6
		// (set) Token: 0x06000EC3 RID: 3779 RVA: 0x000379BE File Offset: 0x00035BBE
		[Parameter(ParameterSetName = "AnrSet")]
		[Parameter(ParameterSetName = "Identity")]
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

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x000379C7 File Offset: 0x00035BC7
		// (set) Token: 0x06000EC5 RID: 3781 RVA: 0x000379CF File Offset: 0x00035BCF
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

		// Token: 0x06000EC6 RID: 3782 RVA: 0x000379D8 File Offset: 0x00035BD8
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

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x00037A38 File Offset: 0x00035C38
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

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00037A84 File Offset: 0x00035C84
		protected override IEnumerable<ADContact> GetPagedData()
		{
			if (base.ParameterSetName == "CookieSet")
			{
				base.WriteVerbose(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(base.DataSession, typeof(ADContact), this.InternalFilter, this.RootId, this.DeepSearch));
				base.InternalResultSize = Unlimited<uint>.UnlimitedValue;
				ADPagedReader<ADContact> adpagedReader = (ADPagedReader<ADContact>)base.DataSession.FindPaged<ADContact>(this.InternalFilter, this.RootId, this.DeepSearch, this.InternalSortBy, this.PageSize);
				adpagedReader.Cookie = this.inputCookie.PageCookie;
				return adpagedReader;
			}
			return base.GetPagedData();
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00037B23 File Offset: 0x00035D23
		protected override void WriteResult(IConfigurable dataObject)
		{
			((ADContact)dataObject).BypassModerationCheck = true;
			base.WriteResult(dataObject);
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x00037B40 File Offset: 0x00035D40
		protected override void WriteResult<T>(IEnumerable<T> dataObjects)
		{
			if (base.ParameterSetName == "CookieSet")
			{
				SyncTaskHelper.HandleTaskWritePagedResult<ADContact>((IEnumerable<ADContact>)dataObjects, this.inputCookie, ref this.outputCookie, () => base.Stopping, new SyncTaskHelper.OneParameterMethod<bool, IConfigurable>(base.ShouldSkipObject), new SyncTaskHelper.VoidOneParameterMethod<IConfigurable>(this.WriteResult), this.Pages, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError));
				return;
			}
			base.WriteResult<T>(dataObjects);
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00037BC9 File Offset: 0x00035DC9
		protected override bool ShouldSkipObject(IConfigurable dataObject)
		{
			return !(base.ParameterSetName == "CookieSet") && base.ShouldSkipObject(dataObject);
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x00037BE8 File Offset: 0x00035DE8
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			ADContact dataObject2 = (ADContact)dataObject;
			SyncMailContact syncMailContact = new SyncMailContact(dataObject2);
			syncMailContact.propertyBag.SetField(ADRecipientSchema.AcceptMessagesOnlyFrom, SyncTaskHelper.RetrieveFullADObjectId(base.TenantGlobalCatalogSession, syncMailContact.AcceptMessagesOnlyFrom));
			syncMailContact.propertyBag.SetField(ADRecipientSchema.AcceptMessagesOnlyFromDLMembers, SyncTaskHelper.RetrieveFullADObjectId(base.TenantGlobalCatalogSession, syncMailContact.AcceptMessagesOnlyFromDLMembers));
			syncMailContact.propertyBag.SetField(ADRecipientSchema.RejectMessagesFrom, SyncTaskHelper.RetrieveFullADObjectId(base.TenantGlobalCatalogSession, syncMailContact.RejectMessagesFrom));
			syncMailContact.propertyBag.SetField(ADRecipientSchema.RejectMessagesFromDLMembers, SyncTaskHelper.RetrieveFullADObjectId(base.TenantGlobalCatalogSession, syncMailContact.RejectMessagesFromDLMembers));
			if (this.outputCookie != null)
			{
				syncMailContact.propertyBag.SetField(SyncMailContactSchema.Cookie, this.outputCookie.ToBytes());
				if (this.outputCookie.HighWatermark == 0L)
				{
					syncMailContact.propertyBag.SetField(SyncMailContactSchema.EndOfList, true);
				}
			}
			return syncMailContact;
		}

		// Token: 0x040002C9 RID: 713
		private SyncCookie inputCookie;

		// Token: 0x040002CA RID: 714
		private SyncCookie outputCookie;
	}
}
