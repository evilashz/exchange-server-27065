using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x020001AB RID: 427
	internal class UnifiedPolicyTraceBatch : IConfigurable, IEnumerable<UnifiedPolicyTrace>, IEnumerable
	{
		// Token: 0x060011F7 RID: 4599 RVA: 0x0003726C File Offset: 0x0003546C
		public UnifiedPolicyTraceBatch()
		{
			this.batch = new List<UnifiedPolicyTrace>();
			this.identity = new ConfigObjectId(CombGuidGenerator.NewGuid().ToString());
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x000372A8 File Offset: 0x000354A8
		// (set) Token: 0x060011F9 RID: 4601 RVA: 0x000372B0 File Offset: 0x000354B0
		public int? PartitionId
		{
			get
			{
				return this.fssPartitionId;
			}
			set
			{
				this.fssPartitionId = value;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x000372B9 File Offset: 0x000354B9
		public ObjectId Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x000372C1 File Offset: 0x000354C1
		public bool IsValid
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x000372C8 File Offset: 0x000354C8
		public ObjectState ObjectState
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x000372CF File Offset: 0x000354CF
		public int ObjectCount
		{
			get
			{
				return this.batch.Count;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x000372DC File Offset: 0x000354DC
		// (set) Token: 0x060011FF RID: 4607 RVA: 0x000372E4 File Offset: 0x000354E4
		public int? PersistentStoreCopyId
		{
			get
			{
				return this.fssCopyId;
			}
			set
			{
				this.fssCopyId = value;
			}
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x000372ED File Offset: 0x000354ED
		public ValidationError[] Validate()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x000372F4 File Offset: 0x000354F4
		public void CopyChangesFrom(IConfigurable source)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x000372FB File Offset: 0x000354FB
		public void ResetChangeTracking()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00037302 File Offset: 0x00035502
		public IEnumerator<UnifiedPolicyTrace> GetEnumerator()
		{
			return this.batch.GetEnumerator();
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00037314 File Offset: 0x00035514
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0003731C File Offset: 0x0003551C
		public void Add(UnifiedPolicyTrace dataTrace)
		{
			if (dataTrace == null)
			{
				throw new ArgumentNullException("dataTrace");
			}
			this.batch.Add(dataTrace);
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00037338 File Offset: 0x00035538
		public void Add(IEnumerable<UnifiedPolicyTrace> dataObjects)
		{
			if (dataObjects == null)
			{
				throw new ArgumentNullException("dataObjects");
			}
			this.batch.AddRange(dataObjects);
		}

		// Token: 0x04000892 RID: 2194
		private int? fssCopyId;

		// Token: 0x04000893 RID: 2195
		private int? fssPartitionId;

		// Token: 0x04000894 RID: 2196
		private List<UnifiedPolicyTrace> batch;

		// Token: 0x04000895 RID: 2197
		private ObjectId identity;
	}
}
