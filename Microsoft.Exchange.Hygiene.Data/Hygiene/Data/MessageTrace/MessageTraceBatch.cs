using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200016A RID: 362
	internal class MessageTraceBatch : IConfigurable, IEnumerable<MessageTrace>, IEnumerable
	{
		// Token: 0x06000E96 RID: 3734 RVA: 0x0002AF58 File Offset: 0x00029158
		public MessageTraceBatch()
		{
			this.batch = new List<MessageTrace>();
			this.identity = new ConfigObjectId(Guid.NewGuid().ToString());
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0002AF94 File Offset: 0x00029194
		public MessageTraceBatch(Guid tenantId) : this()
		{
			if (tenantId.Equals(Guid.Empty))
			{
				throw new ArgumentException("tenantId == Guid.Empty");
			}
			this.organizationalUnitRoot = new Guid?(tenantId);
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x0002AFC1 File Offset: 0x000291C1
		// (set) Token: 0x06000E99 RID: 3737 RVA: 0x0002AFC9 File Offset: 0x000291C9
		public int? PartionId
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

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public ObjectId Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x0002AFDA File Offset: 0x000291DA
		public bool IsValid
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x0002AFE1 File Offset: 0x000291E1
		public ObjectState ObjectState
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x0002AFE8 File Offset: 0x000291E8
		public int MessageCount
		{
			get
			{
				return this.batch.Count;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x0002AFF5 File Offset: 0x000291F5
		// (set) Token: 0x06000E9F RID: 3743 RVA: 0x0002AFFD File Offset: 0x000291FD
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

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x0002B006 File Offset: 0x00029206
		public Guid? OrganizationalUnitRoot
		{
			get
			{
				return this.organizationalUnitRoot;
			}
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0002B00E File Offset: 0x0002920E
		public ValidationError[] Validate()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0002B015 File Offset: 0x00029215
		public void CopyChangesFrom(IConfigurable source)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0002B01C File Offset: 0x0002921C
		public void ResetChangeTracking()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0002B023 File Offset: 0x00029223
		public IEnumerator<MessageTrace> GetEnumerator()
		{
			return this.batch.GetEnumerator();
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0002B035 File Offset: 0x00029235
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0002B040 File Offset: 0x00029240
		public void Add(MessageTrace messageTrace)
		{
			if (this.OrganizationalUnitRoot != null && !messageTrace.OrganizationalUnitRoot.Equals(this.OrganizationalUnitRoot))
			{
				throw new ArgumentException("Unable to add a message to the batch because it is for a different tenant.", "messageTrace");
			}
			this.batch.Add(messageTrace);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0002B0CC File Offset: 0x000292CC
		public void Add(IEnumerable<MessageTrace> messageTraces)
		{
			if (this.OrganizationalUnitRoot != null)
			{
				MessageTrace messageTrace2 = messageTraces.FirstOrDefault((MessageTrace messageTrace) => !messageTrace.OrganizationalUnitRoot.Equals(this.OrganizationalUnitRoot));
				if (messageTrace2 != null)
				{
					string message = string.Format("Unable to add a message with ExMessageId {1} to the batch because it is for a different tenant {0}", messageTrace2.OrganizationalUnitRoot, messageTrace2.ExMessageId);
					throw new ArgumentException(message, "messageTraces");
				}
			}
			this.batch.AddRange(messageTraces);
		}

		// Token: 0x040006BC RID: 1724
		private readonly Guid? organizationalUnitRoot;

		// Token: 0x040006BD RID: 1725
		private int? fssCopyId;

		// Token: 0x040006BE RID: 1726
		private int? fssPartitionId;

		// Token: 0x040006BF RID: 1727
		private List<MessageTrace> batch;

		// Token: 0x040006C0 RID: 1728
		private ObjectId identity;
	}
}
