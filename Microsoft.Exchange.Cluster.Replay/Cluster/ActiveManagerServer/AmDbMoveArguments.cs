using System;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000005 RID: 5
	internal class AmDbMoveArguments
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002148 File Offset: 0x00000348
		public AmDbMoveArguments(AmDbActionCode actionCode)
		{
			this.SourceServer = AmServerName.Empty;
			this.TargetServer = AmServerName.Empty;
			this.MountFlags = MountFlags.None;
			this.DismountFlags = UnmountFlags.SkipCacheFlush;
			this.MoveComment = ReplayStrings.AmBcsNoneSpecified;
			this.MountDialOverride = DatabaseMountDialOverride.Lossless;
			this.TryOtherHealthyServers = true;
			this.SkipValidationChecks = AmBcsSkipFlags.None;
			this.ActionCode = actionCode;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021AC File Offset: 0x000003AC
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000021B4 File Offset: 0x000003B4
		internal AmServerName SourceServer { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021BD File Offset: 0x000003BD
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000021C5 File Offset: 0x000003C5
		internal AmServerName TargetServer { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021CE File Offset: 0x000003CE
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000021D6 File Offset: 0x000003D6
		internal MountFlags MountFlags { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021DF File Offset: 0x000003DF
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000021E7 File Offset: 0x000003E7
		internal UnmountFlags DismountFlags { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000021F0 File Offset: 0x000003F0
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000021F8 File Offset: 0x000003F8
		internal DatabaseMountDialOverride MountDialOverride { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002201 File Offset: 0x00000401
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002209 File Offset: 0x00000409
		internal bool TryOtherHealthyServers { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002212 File Offset: 0x00000412
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000221A File Offset: 0x0000041A
		internal AmBcsSkipFlags SkipValidationChecks { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002223 File Offset: 0x00000423
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000222B File Offset: 0x0000042B
		internal AmDbActionCode ActionCode { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002234 File Offset: 0x00000434
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000223C File Offset: 0x0000043C
		internal string MoveComment
		{
			get
			{
				return this.m_moveComment;
			}
			set
			{
				this.m_moveComment = value;
				if (string.IsNullOrEmpty(this.m_moveComment))
				{
					this.m_moveComment = ReplayStrings.AmBcsNoneSpecified;
				}
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002262 File Offset: 0x00000462
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000226A File Offset: 0x0000046A
		internal string ComponentName { get; set; }

		// Token: 0x04000006 RID: 6
		private string m_moveComment;
	}
}
