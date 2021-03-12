using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000109 RID: 265
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PerUserDataCollector : BaseObject
	{
		// Token: 0x06000552 RID: 1362 RVA: 0x0000FE38 File Offset: 0x0000E038
		internal PerUserDataCollector(int maxSize)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				if (maxSize < 2)
				{
					throw new BufferTooSmallException();
				}
				this.maxSize = maxSize;
				this.perUserDataEntries = new List<PerUserData>();
				this.writer = new CountWriter();
				this.writer.WriteUInt16(0);
				disposeGuard.Success();
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0000FEAC File Offset: 0x0000E0AC
		internal PerUserDataCollector(int maxSize, PerUserData[] perUserDataEntries) : this(maxSize)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				foreach (PerUserData perUserData in perUserDataEntries)
				{
					if (!this.TryAddPerUserData(perUserData))
					{
						break;
					}
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x0000FF0C File Offset: 0x0000E10C
		internal int MaxSize
		{
			get
			{
				return this.maxSize;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0000FF14 File Offset: 0x0000E114
		internal List<PerUserData> PerUserDataEntries
		{
			get
			{
				return this.perUserDataEntries;
			}
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0000FF1C File Offset: 0x0000E11C
		public bool TryAddPerUserData(PerUserData perUserData)
		{
			base.CheckDisposed();
			if (perUserData == null)
			{
				throw new ArgumentNullException("perUserData");
			}
			perUserData.Serialize(this.writer);
			if (this.writer.Position > (long)this.maxSize)
			{
				return false;
			}
			this.perUserDataEntries.Add(perUserData);
			return true;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0000FF6C File Offset: 0x0000E16C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("PerUserDataCollector Collector:");
			stringBuilder.AppendLine("\nMaxSize: " + this.MaxSize);
			stringBuilder.AppendLine("Number of PerUserData entries: " + this.PerUserDataEntries.Count);
			return stringBuilder.ToString();
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0000FFC7 File Offset: 0x0000E1C7
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<PerUserDataCollector>(this);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0000FFCF File Offset: 0x0000E1CF
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.writer);
			base.InternalDispose();
		}

		// Token: 0x0400030E RID: 782
		private readonly int maxSize;

		// Token: 0x0400030F RID: 783
		private readonly CountWriter writer;

		// Token: 0x04000310 RID: 784
		private List<PerUserData> perUserDataEntries;
	}
}
