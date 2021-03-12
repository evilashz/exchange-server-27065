using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicySync;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000E4 RID: 228
	[Serializable]
	public sealed class PsUnifiedPolicyNotification : IConfigurable
	{
		// Token: 0x06000915 RID: 2325 RVA: 0x0002603C File Offset: 0x0002423C
		public PsUnifiedPolicyNotification(Workload workload, string identity, IEnumerable<SyncChangeInfo> syncChangeInfos, bool fullSync)
		{
			this.Workload = workload;
			this.Identity = new ConfigObjectId(identity);
			this.SyncChangeInfos = new MultiValuedProperty<string>(from x in syncChangeInfos
			select x.ToString());
			this.FullSync = fullSync;
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x00026098 File Offset: 0x00024298
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x000260A0 File Offset: 0x000242A0
		public Workload Workload { get; private set; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x000260A9 File Offset: 0x000242A9
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x000260B1 File Offset: 0x000242B1
		public ObjectId Identity { get; private set; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x000260BA File Offset: 0x000242BA
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x000260C2 File Offset: 0x000242C2
		public bool FullSync { get; private set; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x000260CB File Offset: 0x000242CB
		// (set) Token: 0x0600091D RID: 2333 RVA: 0x000260D3 File Offset: 0x000242D3
		public MultiValuedProperty<string> SyncChangeInfos { get; private set; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x000260DC File Offset: 0x000242DC
		public ObjectState ObjectState
		{
			get
			{
				return ObjectState.New;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x000260DF File Offset: 0x000242DF
		public bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x000260E2 File Offset: 0x000242E2
		public ValidationError[] Validate()
		{
			return new ValidationError[0];
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x000260EA File Offset: 0x000242EA
		public void CopyChangesFrom(IConfigurable changedObject)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x000260F1 File Offset: 0x000242F1
		public void ResetChangeTracking()
		{
			throw new NotImplementedException();
		}
	}
}
