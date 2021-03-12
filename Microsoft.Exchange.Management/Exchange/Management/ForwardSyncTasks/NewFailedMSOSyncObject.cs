using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x0200036D RID: 877
	[Cmdlet("New", "FailedMSOSyncObject", SupportsShouldProcess = true)]
	public sealed class NewFailedMSOSyncObject : Task
	{
		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06001EA7 RID: 7847 RVA: 0x00084FDB File Offset: 0x000831DB
		// (set) Token: 0x06001EA8 RID: 7848 RVA: 0x00084FE3 File Offset: 0x000831E3
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public SyncObjectId ObjectId { get; set; }

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06001EA9 RID: 7849 RVA: 0x00084FEC File Offset: 0x000831EC
		// (set) Token: 0x06001EAA RID: 7850 RVA: 0x00084FF4 File Offset: 0x000831F4
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public ServiceInstanceId ServiceInstanceId { get; set; }

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06001EAB RID: 7851 RVA: 0x00084FFD File Offset: 0x000831FD
		// (set) Token: 0x06001EAC RID: 7852 RVA: 0x00085005 File Offset: 0x00083205
		[Parameter(Mandatory = false)]
		public bool IsTemporary { get; set; }

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06001EAD RID: 7853 RVA: 0x0008500E File Offset: 0x0008320E
		// (set) Token: 0x06001EAE RID: 7854 RVA: 0x00085016 File Offset: 0x00083216
		[Parameter(Mandatory = false)]
		public bool IsIncrementalOnly { get; set; }

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06001EAF RID: 7855 RVA: 0x0008501F File Offset: 0x0008321F
		// (set) Token: 0x06001EB0 RID: 7856 RVA: 0x00085027 File Offset: 0x00083227
		[Parameter(Mandatory = false)]
		public bool IsLinkRelated { get; set; }

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06001EB1 RID: 7857 RVA: 0x00085030 File Offset: 0x00083230
		// (set) Token: 0x06001EB2 RID: 7858 RVA: 0x00085038 File Offset: 0x00083238
		[Parameter(Mandatory = false)]
		public bool IsTenantWideDivergence { get; set; }

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06001EB3 RID: 7859 RVA: 0x00085041 File Offset: 0x00083241
		// (set) Token: 0x06001EB4 RID: 7860 RVA: 0x00085049 File Offset: 0x00083249
		[Parameter(Mandatory = false)]
		public bool IsValidationDivergence { get; set; }

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x00085052 File Offset: 0x00083252
		// (set) Token: 0x06001EB6 RID: 7862 RVA: 0x0008505A File Offset: 0x0008325A
		[Parameter(Mandatory = false)]
		public bool IsRetriable { get; set; }

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x00085063 File Offset: 0x00083263
		public ObjectId Identity
		{
			get
			{
				return new CompoundSyncObjectId(this.ObjectId, this.ServiceInstanceId);
			}
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x00085078 File Offset: 0x00083278
		protected override void InternalProcessRecord()
		{
			if (this.forwardSyncDataAccessHelper == null)
			{
				this.forwardSyncDataAccessHelper = new ForwardSyncDataAccessHelper(this.ServiceInstanceId.InstanceId);
			}
			FailedMSOSyncObject existingDivergence = this.forwardSyncDataAccessHelper.GetExistingDivergence(this.ObjectId);
			if (existingDivergence != null)
			{
				this.forwardSyncDataAccessHelper.UpdateExistingDivergence(existingDivergence, 1, false, false, false, true, new string[]
				{
					"TenantWideDivergenceConvertedFromCompanyDivergence"
				}, 1, false, true, null);
			}
			else
			{
				this.forwardSyncDataAccessHelper.PersistNewDivergence(this.ObjectId, DateTime.UtcNow, this.IsIncrementalOnly, this.IsLinkRelated, this.IsTemporary, this.IsTenantWideDivergence, new string[]
				{
					"DivergenceInjectedFromOutside"
				}, this.IsValidationDivergence, this.IsRetriable, null);
			}
			if (this.IsTenantWideDivergence)
			{
				this.MarkDivergenceUnhaltingForContext(this.ObjectId.ContextId);
			}
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x00085144 File Offset: 0x00083344
		protected override void InternalValidate()
		{
			Exception innerException;
			if (!NewFailedMSOSyncObject.IsValidGuid(this.ObjectId.ContextId, out innerException) || !NewFailedMSOSyncObject.IsValidGuid(this.ObjectId.ObjectId, out innerException))
			{
				base.WriteError(new LocalizedException(DirectoryStrings.ExArgumentException("ObjectId", this.ObjectId), innerException), ExchangeErrorCategory.Client, null);
			}
			if (this.IsTenantWideDivergence)
			{
				if (this.ObjectId.ObjectClass != DirectoryObjectClass.Company || string.CompareOrdinal(this.ObjectId.ContextId, this.ObjectId.ObjectId) != 0)
				{
					base.WriteError(new InvalidObjectIdForTenantWideDivergenceException(this.ObjectId.ToString()), ExchangeErrorCategory.Client, null);
				}
			}
			else if ((this.ObjectId.ObjectClass != DirectoryObjectClass.Company && string.CompareOrdinal(this.ObjectId.ContextId, this.ObjectId.ObjectId) == 0) || (this.ObjectId.ObjectClass == DirectoryObjectClass.Company && string.CompareOrdinal(this.ObjectId.ContextId, this.ObjectId.ObjectId) != 0))
			{
				base.WriteError(new LocalizedException(DirectoryStrings.ExArgumentException("ObjectId", this.ObjectId), innerException), ExchangeErrorCategory.Client, null);
			}
			if (this.forwardSyncDataAccessHelper == null)
			{
				this.forwardSyncDataAccessHelper = new ForwardSyncDataAccessHelper(this.ServiceInstanceId.InstanceId);
			}
			FailedMSOSyncObject existingDivergence = this.forwardSyncDataAccessHelper.GetExistingDivergence(this.ObjectId);
			if (existingDivergence != null && (!this.IsTenantWideDivergence || existingDivergence.IsTenantWideDivergence))
			{
				base.WriteError(new DivergenceAlreadyExistsException(this.ObjectId.ToString()), ExchangeErrorCategory.Client, existingDivergence);
			}
			if (this.IsValidationDivergence && this.IsTenantWideDivergence)
			{
				base.WriteError(new CannotBeBothValidationDivergenceAndTenantWideDivergenceException(this.ObjectId.ToString()), ExchangeErrorCategory.Client, null);
			}
			if (!this.IsValidationDivergence && !this.IsRetriable)
			{
				base.WriteError(new NoneValidationDivergenceMustBeRetriableException(this.ObjectId.ToString()), ExchangeErrorCategory.Client, null);
			}
			base.InternalValidate();
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x00085324 File Offset: 0x00083524
		private static bool IsValidGuid(string guidString, out Exception parseException)
		{
			parseException = null;
			try
			{
				new Guid(guidString);
			}
			catch (FormatException ex)
			{
				parseException = ex;
				return false;
			}
			catch (OverflowException ex2)
			{
				parseException = ex2;
				return false;
			}
			return true;
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x0008536C File Offset: 0x0008356C
		private void MarkDivergenceUnhaltingForContext(string contextId)
		{
			lock (this)
			{
				QueryFilter filter = new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, FailedMSOSyncObjectSchema.ContextId, contextId),
					new ComparisonFilter(ComparisonOperator.Equal, FailedMSOSyncObjectSchema.IsTenantWideDivergence, false),
					new ComparisonFilter(ComparisonOperator.NotEqual, FailedMSOSyncObjectSchema.ExternalDirectoryObjectClass, DirectoryObjectClass.Company)
				});
				this.MarkDivergencesUnhalting(filter);
			}
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x000853F0 File Offset: 0x000835F0
		private void MarkDivergencesUnhalting(QueryFilter filter)
		{
			IEnumerable<FailedMSOSyncObject> enumerable = this.forwardSyncDataAccessHelper.FindDivergence(filter);
			IConfigurationSession configurationSession = ForwardSyncDataAccessHelper.CreateSession(false);
			foreach (FailedMSOSyncObject failedMSOSyncObject in enumerable)
			{
				failedMSOSyncObject.IsIgnoredInHaltCondition = true;
				configurationSession.Save(failedMSOSyncObject);
			}
		}

		// Token: 0x0400193D RID: 6461
		private ForwardSyncDataAccessHelper forwardSyncDataAccessHelper;
	}
}
