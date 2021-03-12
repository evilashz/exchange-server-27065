using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200029C RID: 668
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SessionCapabilities
	{
		// Token: 0x06001B9F RID: 7071 RVA: 0x0007FFC0 File Offset: 0x0007E1C0
		internal SessionCapabilities(SessionCapabilitiesFlags flags)
		{
			this.Flags = flags;
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x0007FFCF File Offset: 0x0007E1CF
		public SessionCapabilitiesFlags CapabilitiesFlags
		{
			get
			{
				return this.Flags;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x0007FFD7 File Offset: 0x0007E1D7
		public bool CanSend
		{
			get
			{
				return this.CheckFlags(SessionCapabilitiesFlags.CanSend);
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x0007FFE0 File Offset: 0x0007E1E0
		public bool CanDeliver
		{
			get
			{
				return this.CheckFlags(SessionCapabilitiesFlags.CanDeliver);
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06001BA3 RID: 7075 RVA: 0x0007FFE9 File Offset: 0x0007E1E9
		public bool CanCreateDefaultFolders
		{
			get
			{
				return this.CheckFlags(SessionCapabilitiesFlags.CanCreateDefaultFolders);
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x0007FFF2 File Offset: 0x0007E1F2
		public bool MustHideDefaultFolders
		{
			get
			{
				return this.CheckFlags(SessionCapabilitiesFlags.MustHideDefaultFolders);
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x0007FFFB File Offset: 0x0007E1FB
		public bool CanHaveDelegateUsers
		{
			get
			{
				return this.CheckFlags(SessionCapabilitiesFlags.CanHaveDelegateUsers);
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x00080005 File Offset: 0x0007E205
		public bool CanHaveExternalUsers
		{
			get
			{
				return this.CheckFlags(SessionCapabilitiesFlags.CanHaveExternalUsers);
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x0008000F File Offset: 0x0007E20F
		public bool CanHaveRules
		{
			get
			{
				return this.CheckFlags(SessionCapabilitiesFlags.CanHaveRules);
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x00080019 File Offset: 0x0007E219
		public bool CanHaveJunkEmailRule
		{
			get
			{
				return this.CheckFlags(SessionCapabilitiesFlags.CanHaveJunkEmailRule);
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x00080026 File Offset: 0x0007E226
		public bool CanHaveMasterCategoryList
		{
			get
			{
				return this.CheckFlags(SessionCapabilitiesFlags.CanHaveMasterCategoryList);
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x00080033 File Offset: 0x0007E233
		public bool CanHaveOof
		{
			get
			{
				return this.CheckFlags(SessionCapabilitiesFlags.CanHaveOof);
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06001BAB RID: 7083 RVA: 0x00080040 File Offset: 0x0007E240
		public bool CanHaveUserConfigurationManager
		{
			get
			{
				return this.CheckFlags(SessionCapabilitiesFlags.CanHaveUserConfigurationManager);
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x0008004D File Offset: 0x0007E24D
		public bool MustCreateFolderHierarchy
		{
			get
			{
				return this.CheckFlags(SessionCapabilitiesFlags.MustCreateFolderHierarchy);
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06001BAD RID: 7085 RVA: 0x0008005A File Offset: 0x0007E25A
		public bool CanHaveCulture
		{
			get
			{
				return this.CheckFlags(SessionCapabilitiesFlags.CanHaveCulture);
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x00080067 File Offset: 0x0007E267
		public bool Default
		{
			get
			{
				return this.CheckFlags(SessionCapabilitiesFlags.Default);
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x00080074 File Offset: 0x0007E274
		public bool CanSetCalendarAPIProperties
		{
			get
			{
				return this.CheckFlags(SessionCapabilitiesFlags.CanSetCalendarAPIProperties);
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x00080081 File Offset: 0x0007E281
		public bool IsReadOnly
		{
			get
			{
				return this.CheckFlags(SessionCapabilitiesFlags.ReadOnly);
			}
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x0008008E File Offset: 0x0007E28E
		internal SessionCapabilities CloneAndExtendCapabilities(SessionCapabilitiesFlags sessionCapabilitiesFlags)
		{
			return new SessionCapabilities(this.Flags | sessionCapabilitiesFlags);
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x0008009D File Offset: 0x0007E29D
		private bool CheckFlags(SessionCapabilitiesFlags flags)
		{
			return (this.Flags & flags) == flags;
		}

		// Token: 0x04001339 RID: 4921
		internal static SessionCapabilities PrimarySessionCapabilities = new SessionCapabilities(SessionCapabilitiesFlags.Default);

		// Token: 0x0400133A RID: 4922
		internal static SessionCapabilities ArchiveSessionCapabilities = new SessionCapabilities(SessionCapabilitiesFlags.CanCreateDefaultFolders | SessionCapabilitiesFlags.MustHideDefaultFolders | SessionCapabilitiesFlags.CanHaveUserConfigurationManager);

		// Token: 0x0400133B RID: 4923
		internal static SessionCapabilities MirrorSessionCapabilities = new SessionCapabilities(SessionCapabilitiesFlags.CanSend | SessionCapabilitiesFlags.CanCreateDefaultFolders | SessionCapabilitiesFlags.CanHaveRules | SessionCapabilitiesFlags.CanHaveJunkEmailRule | SessionCapabilitiesFlags.CanHaveMasterCategoryList | SessionCapabilitiesFlags.CanHaveOof | SessionCapabilitiesFlags.CanHaveUserConfigurationManager | SessionCapabilitiesFlags.CanHaveCulture);

		// Token: 0x0400133C RID: 4924
		private readonly SessionCapabilitiesFlags Flags;
	}
}
