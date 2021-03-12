using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.ABProviderFramework
{
	// Token: 0x02000011 RID: 17
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ABProviderCapabilities
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00003F9C File Offset: 0x0000219C
		internal ABProviderCapabilities(ABProviderFlags flags)
		{
			if ((flags & (ABProviderFlags.HasGal | ABProviderFlags.CanBrowse)) != flags)
			{
				throw new ArgumentOutOfRangeException("Got an unknown flag", "flags");
			}
			bool flag = (flags & ABProviderFlags.CanBrowse) != ABProviderFlags.None;
			bool flag2 = (flags & ABProviderFlags.HasGal) != ABProviderFlags.None;
			if (flag && !flag2)
			{
				throw new ArgumentException("Unsupported combination: canBrowse && !hasGal", "flags");
			}
			this.flags = flags;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003FF6 File Offset: 0x000021F6
		public override string ToString()
		{
			return this.flags.ToString();
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00004008 File Offset: 0x00002208
		public ABProviderFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004010 File Offset: 0x00002210
		public bool HasGal
		{
			get
			{
				return this.CheckFlags(ABProviderFlags.HasGal);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004019 File Offset: 0x00002219
		public bool CanBrowse
		{
			get
			{
				return this.CheckFlags(ABProviderFlags.CanBrowse);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004022 File Offset: 0x00002222
		private bool CheckFlags(ABProviderFlags flags)
		{
			return (this.flags & flags) == flags;
		}

		// Token: 0x0400004B RID: 75
		private const int AllKnownFlags = 3;

		// Token: 0x0400004C RID: 76
		private readonly ABProviderFlags flags;
	}
}
