using System;
using Microsoft.Exchange.Data.Directory.DirSync;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000830 RID: 2096
	internal class SyncProperty<T> : ADDirSyncProperty<T>, ISyncProperty
	{
		// Token: 0x06006804 RID: 26628 RVA: 0x0016EA00 File Offset: 0x0016CC00
		internal SyncProperty(T value) : base(value)
		{
		}

		// Token: 0x06006805 RID: 26629 RVA: 0x0016EA09 File Offset: 0x0016CC09
		private SyncProperty(T value, ADDirSyncPropertyState state) : base(value, state)
		{
		}

		// Token: 0x170024CE RID: 9422
		// (get) Token: 0x06006806 RID: 26630 RVA: 0x0016EA14 File Offset: 0x0016CC14
		public new static SyncProperty<T> NoChange
		{
			get
			{
				return new SyncProperty<T>(default(T), ADDirSyncPropertyState.NoChange);
			}
		}

		// Token: 0x06006807 RID: 26631 RVA: 0x0016EA30 File Offset: 0x0016CC30
		public static implicit operator SyncProperty<T>(T value)
		{
			return new SyncProperty<T>(value);
		}

		// Token: 0x170024CF RID: 9423
		// (get) Token: 0x06006808 RID: 26632 RVA: 0x0016EA38 File Offset: 0x0016CC38
		public bool HasValue
		{
			get
			{
				return base.State != ADDirSyncPropertyState.NoChange;
			}
		}

		// Token: 0x06006809 RID: 26633 RVA: 0x0016EA46 File Offset: 0x0016CC46
		public object GetValue()
		{
			return base.Value;
		}

		// Token: 0x0600680A RID: 26634 RVA: 0x0016EA54 File Offset: 0x0016CC54
		public override string ToString()
		{
			if (!this.HasValue)
			{
				return "<Not Present>";
			}
			if (base.Value != null && base.Value == null)
			{
				return "<null>";
			}
			T value = base.Value;
			return value.ToString();
		}
	}
}
