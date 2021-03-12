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
	// Token: 0x020000C2 RID: 194
	[Cmdlet("Get", "SyncDynamicDistributionGroup", DefaultParameterSetName = "Identity")]
	[OutputType(new Type[]
	{
		typeof(SyncDynamicDistributionGroup)
	})]
	public sealed class GetSyncDynamicDistributionGroup : GetDynamicDistributionGroupBase
	{
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x000331C4 File Offset: 0x000313C4
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<SyncDynamicDistributionGroupSchema>();
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x000331CB File Offset: 0x000313CB
		// (set) Token: 0x06000C71 RID: 3185 RVA: 0x000331E2 File Offset: 0x000313E2
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

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x000331F5 File Offset: 0x000313F5
		// (set) Token: 0x06000C73 RID: 3187 RVA: 0x0003321A File Offset: 0x0003141A
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

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x00033232 File Offset: 0x00031432
		// (set) Token: 0x06000C75 RID: 3189 RVA: 0x0003323A File Offset: 0x0003143A
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

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x00033243 File Offset: 0x00031443
		// (set) Token: 0x06000C77 RID: 3191 RVA: 0x0003324B File Offset: 0x0003144B
		[Parameter(ParameterSetName = "Identity")]
		[Parameter(ParameterSetName = "AnrSet")]
		[Parameter(ParameterSetName = "ManagedBySet")]
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

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x00033254 File Offset: 0x00031454
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x0003325C File Offset: 0x0003145C
		[Parameter(ParameterSetName = "AnrSet")]
		[Parameter(ParameterSetName = "ManagedBySet")]
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

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00033265 File Offset: 0x00031465
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x0003326D File Offset: 0x0003146D
		[Parameter(ParameterSetName = "AnrSet")]
		[Parameter(ParameterSetName = "ManagedBySet")]
		[Parameter(ParameterSetName = "Identity")]
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

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00033276 File Offset: 0x00031476
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x0003329C File Offset: 0x0003149C
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

		// Token: 0x06000C7E RID: 3198 RVA: 0x000332B4 File Offset: 0x000314B4
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

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x00033330 File Offset: 0x00031530
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

		// Token: 0x06000C80 RID: 3200 RVA: 0x0003337C File Offset: 0x0003157C
		protected override IEnumerable<ADDynamicGroup> GetPagedData()
		{
			if (base.ParameterSetName == "CookieSet")
			{
				base.WriteVerbose(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(base.DataSession, typeof(ADDynamicGroup), this.InternalFilter, this.RootId, this.DeepSearch));
				base.InternalResultSize = Unlimited<uint>.UnlimitedValue;
				ADPagedReader<ADDynamicGroup> adpagedReader = (ADPagedReader<ADDynamicGroup>)base.DataSession.FindPaged<ADDynamicGroup>(this.InternalFilter, this.RootId, this.DeepSearch, this.InternalSortBy, this.PageSize);
				adpagedReader.Cookie = this.inputCookie.PageCookie;
				return adpagedReader;
			}
			return base.GetPagedData();
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00033424 File Offset: 0x00031624
		protected override void WriteResult<T>(IEnumerable<T> dataObjects)
		{
			if (base.ParameterSetName == "CookieSet")
			{
				SyncTaskHelper.HandleTaskWritePagedResult<ADDynamicGroup>((IEnumerable<ADDynamicGroup>)dataObjects, this.inputCookie, ref this.outputCookie, () => base.Stopping, new SyncTaskHelper.OneParameterMethod<bool, IConfigurable>(base.ShouldSkipObject), new SyncTaskHelper.VoidOneParameterMethod<IConfigurable>(this.WriteResult), this.Pages, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError));
				return;
			}
			base.WriteResult<T>(dataObjects);
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x000334AD File Offset: 0x000316AD
		protected override bool ShouldSkipObject(IConfigurable dataObject)
		{
			return !(base.ParameterSetName == "CookieSet") && base.ShouldSkipObject(dataObject);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x000334CC File Offset: 0x000316CC
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			ADDynamicGroup dataObject2 = (ADDynamicGroup)dataObject;
			SyncDynamicDistributionGroup syncDynamicDistributionGroup = new SyncDynamicDistributionGroup(dataObject2);
			if (this.outputCookie != null)
			{
				syncDynamicDistributionGroup.propertyBag.SetField(SyncDynamicDistributionGroupSchema.Cookie, this.outputCookie.ToBytes());
				if (this.outputCookie.HighWatermark == 0L)
				{
					syncDynamicDistributionGroup.propertyBag.SetField(SyncDynamicDistributionGroupSchema.EndOfList, true);
				}
			}
			return syncDynamicDistributionGroup;
		}

		// Token: 0x040002A4 RID: 676
		private SyncCookie inputCookie;

		// Token: 0x040002A5 RID: 677
		private SyncCookie outputCookie;
	}
}
