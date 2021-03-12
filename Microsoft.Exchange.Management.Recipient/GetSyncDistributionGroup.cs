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
	// Token: 0x020000BE RID: 190
	[Cmdlet("Get", "SyncDistributionGroup", DefaultParameterSetName = "Identity")]
	[OutputType(new Type[]
	{
		typeof(SyncDistributionGroup)
	})]
	public sealed class GetSyncDistributionGroup : GetDistributionGroupBase
	{
		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x000327B3 File Offset: 0x000309B3
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<SyncDistributionGroupSchema>();
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x000327BA File Offset: 0x000309BA
		// (set) Token: 0x06000C43 RID: 3139 RVA: 0x000327D1 File Offset: 0x000309D1
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

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x000327E4 File Offset: 0x000309E4
		// (set) Token: 0x06000C45 RID: 3141 RVA: 0x00032809 File Offset: 0x00030A09
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

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x00032821 File Offset: 0x00030A21
		// (set) Token: 0x06000C47 RID: 3143 RVA: 0x00032829 File Offset: 0x00030A29
		[Parameter(ParameterSetName = "AnrSet")]
		[Parameter(ParameterSetName = "ManagedBySet")]
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

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x00032832 File Offset: 0x00030A32
		// (set) Token: 0x06000C49 RID: 3145 RVA: 0x0003283A File Offset: 0x00030A3A
		[Parameter(ParameterSetName = "ManagedBySet")]
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

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x00032843 File Offset: 0x00030A43
		// (set) Token: 0x06000C4B RID: 3147 RVA: 0x0003284B File Offset: 0x00030A4B
		[Parameter(ParameterSetName = "ManagedBySet")]
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

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x00032854 File Offset: 0x00030A54
		// (set) Token: 0x06000C4D RID: 3149 RVA: 0x0003285C File Offset: 0x00030A5C
		[Parameter(ParameterSetName = "AnrSet")]
		[Parameter(ParameterSetName = "Identity")]
		[Parameter(ParameterSetName = "ManagedBySet")]
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

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x00032865 File Offset: 0x00030A65
		// (set) Token: 0x06000C4F RID: 3151 RVA: 0x0003288B File Offset: 0x00030A8B
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeSoftDeletedObjects
		{
			get
			{
				return (SwitchParameter)(base.Fields["SoftDeletedObject"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["SoftDeletedObject"] = value;
			}
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x000328A4 File Offset: 0x00030AA4
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession recipientSession = (IRecipientSession)base.CreateSession();
			if (this.IncludeSoftDeletedObjects.IsPresent)
			{
				recipientSession.SessionSettings.IncludeSoftDeletedObjects = true;
			}
			if (base.ParameterSetName == "CookieSet")
			{
				recipientSession.UseGlobalCatalog = true;
				this.inputCookie = SyncTaskHelper.ResolveSyncCookie(this.Cookie, recipientSession, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			return recipientSession;
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x00032920 File Offset: 0x00030B20
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

		// Token: 0x06000C52 RID: 3154 RVA: 0x0003296C File Offset: 0x00030B6C
		protected override IEnumerable<ADGroup> GetPagedData()
		{
			if (base.ParameterSetName == "CookieSet")
			{
				base.WriteVerbose(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(base.DataSession, typeof(ADGroup), this.InternalFilter, this.RootId, this.DeepSearch));
				base.InternalResultSize = Unlimited<uint>.UnlimitedValue;
				ADPagedReader<ADGroup> adpagedReader = (ADPagedReader<ADGroup>)base.DataSession.FindPaged<ADGroup>(this.InternalFilter, this.RootId, this.DeepSearch, this.InternalSortBy, this.PageSize);
				adpagedReader.Cookie = this.inputCookie.PageCookie;
				return adpagedReader;
			}
			return base.GetPagedData();
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00032A0B File Offset: 0x00030C0B
		protected override void WriteResult(IConfigurable dataObject)
		{
			((ADGroup)dataObject).BypassModerationCheck = true;
			base.WriteResult(dataObject);
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00032A28 File Offset: 0x00030C28
		protected override void WriteResult<T>(IEnumerable<T> dataObjects)
		{
			if (base.ParameterSetName == "CookieSet")
			{
				SyncTaskHelper.HandleTaskWritePagedResult<ADGroup>((IEnumerable<ADGroup>)dataObjects, this.inputCookie, ref this.outputCookie, () => base.Stopping, new SyncTaskHelper.OneParameterMethod<bool, IConfigurable>(base.ShouldSkipObject), new SyncTaskHelper.VoidOneParameterMethod<IConfigurable>(this.WriteResult), this.Pages, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError));
				return;
			}
			base.WriteResult<T>(dataObjects);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00032AB1 File Offset: 0x00030CB1
		protected override bool ShouldSkipObject(IConfigurable dataObject)
		{
			return !(base.ParameterSetName == "CookieSet") && base.ShouldSkipObject(dataObject);
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00032AD0 File Offset: 0x00030CD0
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			ADGroup dataObject2 = (ADGroup)dataObject;
			SyncDistributionGroup syncDistributionGroup = new SyncDistributionGroup(dataObject2);
			syncDistributionGroup.propertyBag.SetField(ADRecipientSchema.AcceptMessagesOnlyFrom, SyncTaskHelper.RetrieveFullADObjectId(base.TenantGlobalCatalogSession, syncDistributionGroup.AcceptMessagesOnlyFrom));
			syncDistributionGroup.propertyBag.SetField(ADRecipientSchema.AcceptMessagesOnlyFromDLMembers, SyncTaskHelper.RetrieveFullADObjectId(base.TenantGlobalCatalogSession, syncDistributionGroup.AcceptMessagesOnlyFromDLMembers));
			syncDistributionGroup.propertyBag.SetField(ADRecipientSchema.RejectMessagesFrom, SyncTaskHelper.RetrieveFullADObjectId(base.TenantGlobalCatalogSession, syncDistributionGroup.RejectMessagesFrom));
			syncDistributionGroup.propertyBag.SetField(ADRecipientSchema.RejectMessagesFromDLMembers, SyncTaskHelper.RetrieveFullADObjectId(base.TenantGlobalCatalogSession, syncDistributionGroup.RejectMessagesFromDLMembers));
			if (this.outputCookie != null)
			{
				syncDistributionGroup.propertyBag.SetField(SyncDistributionGroupSchema.Cookie, this.outputCookie.ToBytes());
				if (this.outputCookie.HighWatermark == 0L)
				{
					syncDistributionGroup.propertyBag.SetField(SyncDistributionGroupSchema.EndOfList, true);
				}
			}
			return syncDistributionGroup;
		}

		// Token: 0x04000299 RID: 665
		private SyncCookie inputCookie;

		// Token: 0x0400029A RID: 666
		private SyncCookie outputCookie;
	}
}
