using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000D1 RID: 209
	[Cmdlet("Get", "SyncDeletedRecipient", DefaultParameterSetName = "CookieSet")]
	public sealed class GetSyncDeletedRecipient : GetTaskBase<ADRawEntry>
	{
		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x0003B1E1 File Offset: 0x000393E1
		// (set) Token: 0x0600104F RID: 4175 RVA: 0x0003B1F8 File Offset: 0x000393F8
		[Parameter]
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

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x0003B20B File Offset: 0x0003940B
		// (set) Token: 0x06001051 RID: 4177 RVA: 0x0003B230 File Offset: 0x00039430
		[Parameter]
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

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x0003B248 File Offset: 0x00039448
		// (set) Token: 0x06001053 RID: 4179 RVA: 0x0003B25F File Offset: 0x0003945F
		[Parameter]
		[ValidateNotNullOrEmpty]
		public SyncRecipientType RecipientType
		{
			get
			{
				return (SyncRecipientType)base.Fields["RecipientType"];
			}
			set
			{
				base.Fields["RecipientType"] = value;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x0003B277 File Offset: 0x00039477
		// (set) Token: 0x06001055 RID: 4181 RVA: 0x0003B27F File Offset: 0x0003947F
		[Parameter]
		public new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x0003B288 File Offset: 0x00039488
		protected override QueryFilter InternalFilter
		{
			get
			{
				if (!base.Fields.IsChanged("RecipientType"))
				{
					this.RecipientType = SyncRecipientType.All;
				}
				QueryFilter queryFilter = SyncDeleteRecipientFilters.GetFilter(this.RecipientType);
				if (base.CurrentOrganizationId != null && base.CurrentOrganizationId.OrganizationalUnit != null)
				{
					queryFilter = new AndFilter(new QueryFilter[]
					{
						queryFilter,
						new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.OrganizationalUnitRoot, base.CurrentOrganizationId.OrganizationalUnit)
					});
				}
				else
				{
					queryFilter = new AndFilter(new QueryFilter[]
					{
						queryFilter,
						new NotFilter(new ExistsFilter(ADObjectSchema.OrganizationalUnitRoot))
					});
				}
				return new AndFilter(new QueryFilter[]
				{
					queryFilter,
					SyncTaskHelper.GetDeltaFilter(this.inputCookie)
				});
			}
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0003B348 File Offset: 0x00039548
		protected override IEnumerable<ADRawEntry> GetPagedData()
		{
			if (this.Cookie == null)
			{
				DeletedRecipient item = new DeletedRecipient();
				return new List<ADRawEntry>
				{
					item
				};
			}
			PropertyDefinition[] properties = new PropertyDefinition[]
			{
				ADObjectSchema.Id,
				ADObjectSchema.Name,
				ADObjectSchema.DistinguishedName,
				ADObjectSchema.Guid,
				ADObjectSchema.OrganizationalUnitRoot,
				ADObjectSchema.ObjectClass,
				ADObjectSchema.WhenChanged,
				ADObjectSchema.WhenChangedUTC,
				ADObjectSchema.WhenCreated,
				ADObjectSchema.WhenCreatedUTC
			};
			if (this.requireTwoQueries)
			{
				IRecipientSession sessionForSoftDeletedObjects = SoftDeletedTaskHelper.GetSessionForSoftDeletedObjects(base.CurrentOrganizationId, this.DomainController);
				ADObjectId childId = base.CurrentOrganizationId.OrganizationalUnit.GetChildId("OU", "Soft Deleted Objects");
				QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.IsSoftDeletedByRemove, true);
				queryFilter = new AndFilter(new QueryFilter[]
				{
					queryFilter,
					SyncTaskHelper.GetDeltaFilter(this.inputCookie)
				});
				this.reader2 = sessionForSoftDeletedObjects.FindPagedADRawEntry(childId, QueryScope.OneLevel, queryFilter, null, this.PageSize, properties);
				this.reader2.Cookie = this.inputCookie.PageCookie2;
				base.WriteVerbose(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(sessionForSoftDeletedObjects, typeof(ADRawEntry), queryFilter, null, false));
			}
			IRecipientSession recipientSession = (IRecipientSession)base.DataSession;
			ADObjectId deletedObjectsContainer = ADSession.GetDeletedObjectsContainer(recipientSession.GetDomainNamingContext());
			QueryFilter internalFilter = this.InternalFilter;
			recipientSession.SessionSettings.SkipCheckVirtualIndex = true;
			ADPagedReader<ADRawEntry> adpagedReader = recipientSession.FindPagedADRawEntry(deletedObjectsContainer, QueryScope.OneLevel, internalFilter, null, this.PageSize, properties);
			adpagedReader.IncludeDeletedObjects = true;
			adpagedReader.Cookie = this.inputCookie.PageCookie;
			base.WriteVerbose(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(base.DataSession, typeof(ADRawEntry), this.InternalFilter, null, false));
			return adpagedReader;
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0003B51C File Offset: 0x0003971C
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession sessionForDeletedObjects = MailboxTaskHelper.GetSessionForDeletedObjects(this.DomainController, base.CurrentOrganizationId, base.ExecutingUserOrganizationId);
			this.inputCookie = SyncTaskHelper.ResolveSyncCookie(this.Cookie, sessionForDeletedObjects, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError));
			return sessionForDeletedObjects;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0003B584 File Offset: 0x00039784
		protected override void WriteResult<T>(IEnumerable<T> dataObjects)
		{
			SyncTaskHelper.OneParameterMethod<bool, IConfigurable> oneParameterMethod = null;
			SyncTaskHelper.OneParameterMethod<bool, IConfigurable> oneParameterMethod2 = null;
			ADRawEntry adrawEntry = null;
			if (dataObjects is ADPagedReader<ADRawEntry>)
			{
				bool flag;
				if (this.requireTwoQueries)
				{
					IEnumerable<ADRawEntry> dataObjects2 = (IEnumerable<ADRawEntry>)dataObjects;
					IEnumerable<ADRawEntry> dataObjects3 = this.reader2;
					SyncCookie syncCookie = this.inputCookie;
					SyncTaskHelper.ParameterlessMethod<bool> isStopping = () => base.Stopping;
					if (oneParameterMethod == null)
					{
						oneParameterMethod = ((IConfigurable dataObject) => false);
					}
					this.searchNeedsRetry = !SyncTaskHelper.HandleTaskWritePagedResult<ADRawEntry>(dataObjects2, dataObjects3, syncCookie, ref this.outputCookie, isStopping, oneParameterMethod, new SyncTaskHelper.VoidOneParameterMethod<IConfigurable>(this.WriteResult), this.Pages, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError), out flag);
				}
				else
				{
					IEnumerable<ADRawEntry> dataObjects4 = (IEnumerable<ADRawEntry>)dataObjects;
					SyncCookie syncCookie2 = this.inputCookie;
					SyncTaskHelper.ParameterlessMethod<bool> isStopping2 = () => base.Stopping;
					if (oneParameterMethod2 == null)
					{
						oneParameterMethod2 = ((IConfigurable dataObject) => false);
					}
					this.searchNeedsRetry = !SyncTaskHelper.HandleTaskWritePagedResult<ADRawEntry>(dataObjects4, syncCookie2, ref this.outputCookie, isStopping2, oneParameterMethod2, new SyncTaskHelper.VoidOneParameterMethod<IConfigurable>(this.WriteResult), this.Pages, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError), out flag);
				}
				if (!this.searchNeedsRetry && !flag)
				{
					adrawEntry = new DeletedRecipient();
				}
			}
			else
			{
				adrawEntry = ((List<ADRawEntry>)dataObjects)[0];
			}
			if (adrawEntry != null)
			{
				this.outputCookie = new SyncCookie(this.inputCookie.DomainController, this.inputCookie.HighWatermarks, WatermarkMap.Empty, null);
				this.WriteResult(adrawEntry);
			}
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0003B6F0 File Offset: 0x000398F0
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			DeletedRecipient deleteRecipient;
			if (dataObject is DeletedRecipient)
			{
				deleteRecipient = (DeletedRecipient)dataObject;
			}
			else
			{
				ADRawEntry adrawEntry = (ADRawEntry)dataObject;
				this.PackDeletedRecipientInfo(adrawEntry);
				deleteRecipient = new DeletedRecipient(null, adrawEntry.propertyBag);
			}
			SyncDeletedRecipient syncDeletedRecipient = new SyncDeletedRecipient(deleteRecipient);
			if (this.outputCookie != null)
			{
				syncDeletedRecipient.propertyBag.SetField(SyncDeletedObjectSchema.Cookie, this.outputCookie.ToBytes());
				if (this.outputCookie.HighWatermark == 0L)
				{
					syncDeletedRecipient.propertyBag.SetField(SyncDeletedObjectSchema.EndOfList, true);
				}
			}
			return syncDeletedRecipient;
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0003B780 File Offset: 0x00039980
		protected override void WriteResult(IConfigurable dataObject)
		{
			base.WriteResult(this.ConvertDataObjectToPresentationObject(dataObject));
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0003B790 File Offset: 0x00039990
		protected override void InternalProcessRecord()
		{
			this.requireTwoQueries = (this.Cookie != null && base.CurrentOrganizationId != null && base.CurrentOrganizationId.OrganizationalUnit != null);
			base.InternalProcessRecord();
			int num = 0;
			while (this.searchNeedsRetry)
			{
				num++;
				if (num <= 5)
				{
					base.WriteVerbose(Strings.VerboseGetSyncDeletedRecipientNeedsRetry(num));
					base.InternalProcessRecord();
				}
				else
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorGetSyncDeletedRecipientRetryFailed), ErrorCategory.InvalidOperation, null);
				}
			}
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0003B810 File Offset: 0x00039A10
		private void PackDeletedRecipientInfo(ADRawEntry entry)
		{
			string text = (string)entry.propertyBag[ADObjectSchema.DistinguishedName];
			int num = text.IndexOf(",OU=Soft Deleted Objects,", StringComparison.OrdinalIgnoreCase);
			if (-1 != num)
			{
				string text2 = (string)entry.propertyBag[ADObjectSchema.Name];
				string arg = ((Guid)entry.propertyBag[ADObjectSchema.Guid]).ToString();
				string value = string.Format("{0}\nDEL:{1}", text2, arg);
				entry.propertyBag.SetField(ADObjectSchema.Name, value);
				ADObjectId deletedObjectsContainer = ADSession.GetDeletedObjectsContainer(((IRecipientSession)base.DataSession).GetDomainNamingContext());
				string arg2 = AdName.Escape(text2);
				string text3 = string.Format("CN={0}\\0ADEL:{1},{2}", arg2, arg, deletedObjectsContainer.DistinguishedName);
				entry.propertyBag.SetField(ADObjectSchema.DistinguishedName, text3);
				ADObjectId value2 = new ADObjectId(text3);
				entry.propertyBag.SetField(ADObjectSchema.Id, value2);
			}
			entry.propertyBag.SetField(DeletedObjectSchema.IsDeleted, true);
			entry.propertyBag.SetField(DeletedObjectSchema.LastKnownParent, entry.propertyBag[ADObjectSchema.OrganizationalUnitRoot]);
			entry.propertyBag.SetField(ADObjectSchema.OrganizationId, base.CurrentOrganizationId);
		}

		// Token: 0x040002E3 RID: 739
		private const int InvalidPagedSearchMaxRetryCount = 5;

		// Token: 0x040002E4 RID: 740
		private bool searchNeedsRetry;

		// Token: 0x040002E5 RID: 741
		private SyncCookie inputCookie;

		// Token: 0x040002E6 RID: 742
		private SyncCookie outputCookie;

		// Token: 0x040002E7 RID: 743
		private bool requireTwoQueries;

		// Token: 0x040002E8 RID: 744
		private ADPagedReader<ADRawEntry> reader2;
	}
}
