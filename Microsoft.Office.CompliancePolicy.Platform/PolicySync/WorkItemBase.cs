using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x0200013C RID: 316
	[Serializable]
	public abstract class WorkItemBase
	{
		// Token: 0x06000941 RID: 2369 RVA: 0x0001F8DC File Offset: 0x0001DADC
		public WorkItemBase(string externalIdentity, bool processNow, TenantContext tenantContext, bool hasPersistentBackup = false) : this(externalIdentity, default(DateTime), processNow, tenantContext, hasPersistentBackup)
		{
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0001F900 File Offset: 0x0001DB00
		internal WorkItemBase(string externalIdentity, DateTime executeTimeUTC, bool processNow, TenantContext tenantContext, bool hasPersistentBackup = false)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("externalIdentity", externalIdentity);
			ArgumentValidator.ThrowIfNull("tenantContext", tenantContext);
			this.ExternalIdentity = externalIdentity;
			this.ExecuteTimeUTC = executeTimeUTC;
			this.ProcessNow = processNow;
			this.tenantContext = tenantContext;
			this.HasPersistentBackUp = hasPersistentBackup;
			this.Status = WorkItemStatus.NotStarted;
			this.Errors = new List<SyncAgentExceptionBase>();
			this.SerializableErrors = new List<SerializableException>();
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x0001F96C File Offset: 0x0001DB6C
		// (set) Token: 0x06000944 RID: 2372 RVA: 0x0001F974 File Offset: 0x0001DB74
		public DateTime ExecuteTimeUTC { get; set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x0001F97D File Offset: 0x0001DB7D
		// (set) Token: 0x06000946 RID: 2374 RVA: 0x0001F985 File Offset: 0x0001DB85
		public bool ProcessNow { get; set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0001F98E File Offset: 0x0001DB8E
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x0001F996 File Offset: 0x0001DB96
		public string WorkItemId
		{
			get
			{
				return this.workItemId;
			}
			set
			{
				ArgumentValidator.ThrowIfNullOrEmpty("WorkItemId", value);
				this.workItemId = value;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x0001F9AA File Offset: 0x0001DBAA
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x0001F9B2 File Offset: 0x0001DBB2
		public string ExternalIdentity
		{
			get
			{
				return this.externalIdentity;
			}
			set
			{
				ArgumentValidator.ThrowIfNullOrEmpty("ExternalIdentity", value);
				this.externalIdentity = value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x0001F9C6 File Offset: 0x0001DBC6
		// (set) Token: 0x0600094C RID: 2380 RVA: 0x0001F9CE File Offset: 0x0001DBCE
		public TenantContext TenantContext
		{
			get
			{
				return this.tenantContext;
			}
			set
			{
				ArgumentValidator.ThrowIfNull("TenantContext", value);
				this.tenantContext = value;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x0001F9E2 File Offset: 0x0001DBE2
		// (set) Token: 0x0600094E RID: 2382 RVA: 0x0001F9EA File Offset: 0x0001DBEA
		public bool HasPersistentBackUp { get; set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x0001F9F3 File Offset: 0x0001DBF3
		// (set) Token: 0x06000950 RID: 2384 RVA: 0x0001F9FB File Offset: 0x0001DBFB
		public WorkItemStatus Status { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x0001FA04 File Offset: 0x0001DC04
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x0001FA0C File Offset: 0x0001DC0C
		public List<SyncAgentExceptionBase> Errors
		{
			get
			{
				return this.errors;
			}
			set
			{
				this.errors = ((value != null) ? value : new List<SyncAgentExceptionBase>());
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x0001FA1F File Offset: 0x0001DC1F
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x0001FA27 File Offset: 0x0001DC27
		public int TryCount { get; set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x0001FA30 File Offset: 0x0001DC30
		// (set) Token: 0x06000956 RID: 2390 RVA: 0x0001FA38 File Offset: 0x0001DC38
		internal List<SerializableException> SerializableErrors { get; private set; }

		// Token: 0x06000957 RID: 2391 RVA: 0x0001FA44 File Offset: 0x0001DC44
		public static WorkItemBase Deserialize(byte[] binaryData)
		{
			if (binaryData == null || binaryData.Length == 0)
			{
				return null;
			}
			WorkItemBase workItemBase = (WorkItemBase)CommonUtility.BytesToObject(binaryData);
			if (workItemBase != null)
			{
				if (workItemBase.Errors == null)
				{
					workItemBase.Errors = new List<SyncAgentExceptionBase>();
				}
				if (workItemBase.SerializableErrors == null)
				{
					workItemBase.SerializableErrors = new List<SerializableException>();
				}
			}
			return workItemBase;
		}

		// Token: 0x06000958 RID: 2392
		public abstract bool Merge(WorkItemBase newWorkItem);

		// Token: 0x06000959 RID: 2393
		public abstract bool IsEqual(WorkItemBase newWorkItem);

		// Token: 0x0600095A RID: 2394
		public abstract Guid GetPrimaryKey();

		// Token: 0x0600095B RID: 2395 RVA: 0x0001FA94 File Offset: 0x0001DC94
		public byte[] Serialize()
		{
			if (this.Errors.Any<SyncAgentExceptionBase>())
			{
				foreach (SyncAgentExceptionBase ex in this.Errors)
				{
					this.SerializableErrors.Add(new SerializableException(ex));
				}
			}
			return CommonUtility.ObjectToBytes(this);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0001FB04 File Offset: 0x0001DD04
		internal void ResetStatus()
		{
			this.Status = WorkItemStatus.NotStarted;
			this.Errors = new List<SyncAgentExceptionBase>();
			this.SerializableErrors = new List<SerializableException>();
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0001FB23 File Offset: 0x0001DD23
		internal virtual WorkItemBase Split()
		{
			return null;
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0001FB28 File Offset: 0x0001DD28
		internal virtual bool RoughCompare(object other)
		{
			WorkItemBase workItemBase = other as WorkItemBase;
			return workItemBase != null && (this.ProcessNow == workItemBase.ProcessNow && this.ExternalIdentity.Equals(workItemBase.ExternalIdentity, StringComparison.OrdinalIgnoreCase) && this.Status == workItemBase.Status && this.TenantContext.TenantId == workItemBase.TenantContext.TenantId) && this.TryCount == workItemBase.TryCount;
		}

		// Token: 0x040004CB RID: 1227
		private string workItemId;

		// Token: 0x040004CC RID: 1228
		private string externalIdentity;

		// Token: 0x040004CD RID: 1229
		private TenantContext tenantContext;

		// Token: 0x040004CE RID: 1230
		[NonSerialized]
		private List<SyncAgentExceptionBase> errors;
	}
}
