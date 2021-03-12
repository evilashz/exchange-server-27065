using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200014F RID: 335
	[Serializable]
	internal class LocalCmdLineServerSettings : RunspaceServerSettings, ICloneable, IEquatable<LocalCmdLineServerSettings>
	{
		// Token: 0x06000E48 RID: 3656 RVA: 0x000428A8 File Offset: 0x00040AA8
		private LocalCmdLineServerSettings()
		{
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x000428B0 File Offset: 0x00040AB0
		internal static LocalCmdLineServerSettings CreateLocalCmdLineServerSettings()
		{
			return new LocalCmdLineServerSettings
			{
				ViewEntireForest = true
			};
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x000428CB File Offset: 0x00040ACB
		internal override bool IsUpdatableByADSession
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x000428CE File Offset: 0x00040ACE
		protected override bool EnforceIsUpdatableByADSession
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x000428D1 File Offset: 0x00040AD1
		internal override bool IsFailoverRequired()
		{
			return false;
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x000428D4 File Offset: 0x00040AD4
		internal override bool TryFailover(out ADServerSettings newServerSettings, out string failToFailOverReason, bool forestWideAffinityRequested = false)
		{
			throw new NotSupportedException("TryFailover is not supported by LocalCmdLineServerSettings");
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x000428E0 File Offset: 0x00040AE0
		internal override void MarkDcDown(Fqdn fqdn)
		{
			throw new NotSupportedException("MarkDcDown is not supported by LocalCmdLineServerSettings");
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x000428EC File Offset: 0x00040AEC
		internal override void MarkDcUp(Fqdn fqdn)
		{
			throw new NotSupportedException("MarkDcUp is not supported by LocalCmdLineServerSettings");
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x000428F8 File Offset: 0x00040AF8
		public override object Clone()
		{
			LocalCmdLineServerSettings localCmdLineServerSettings = new LocalCmdLineServerSettings();
			this.CopyTo(localCmdLineServerSettings);
			return localCmdLineServerSettings;
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00042913 File Offset: 0x00040B13
		public bool Equals(LocalCmdLineServerSettings other)
		{
			return base.Equals(other);
		}
	}
}
