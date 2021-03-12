using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E0A RID: 3594
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SyncStateInfo
	{
		// Token: 0x17002124 RID: 8484
		// (get) Token: 0x06007BF3 RID: 31731 RVA: 0x0022383F File Offset: 0x00221A3F
		public virtual SaveMode SaveModeForSyncState
		{
			get
			{
				return SaveMode.NoConflictResolutionForceSave;
			}
		}

		// Token: 0x17002125 RID: 8485
		// (get) Token: 0x06007BF4 RID: 31732
		// (set) Token: 0x06007BF5 RID: 31733
		public abstract string UniqueName { get; set; }

		// Token: 0x17002126 RID: 8486
		// (get) Token: 0x06007BF6 RID: 31734
		public abstract int Version { get; }

		// Token: 0x17002127 RID: 8487
		// (get) Token: 0x06007BF7 RID: 31735 RVA: 0x00223842 File Offset: 0x00221A42
		// (set) Token: 0x06007BF8 RID: 31736 RVA: 0x0022384A File Offset: 0x00221A4A
		public bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				this.readOnly = value;
			}
		}

		// Token: 0x06007BF9 RID: 31737 RVA: 0x00223854 File Offset: 0x00221A54
		public virtual void HandleSyncStateVersioning(SyncState syncState)
		{
			if (syncState == null)
			{
				throw new ArgumentNullException("syncState");
			}
			if (syncState.BackendVersion.Value != syncState.Version)
			{
				syncState.HandleCorruptSyncState();
			}
		}

		// Token: 0x040054FA RID: 21754
		public static readonly PropertyDefinition StorageLocation = InternalSchema.SyncCustomState;

		// Token: 0x040054FB RID: 21755
		private bool readOnly;
	}
}
