using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x02000014 RID: 20
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ADDatabaseCopyWrapper : ADObjectWrapperBase, IADDatabaseCopy, IADObjectCommon, IComparable<ADDatabaseCopyWrapper>
	{
		// Token: 0x060000ED RID: 237 RVA: 0x00003A78 File Offset: 0x00001C78
		private ADDatabaseCopyWrapper(DatabaseCopy copy) : base(copy)
		{
			this.DatabaseName = copy.DatabaseName;
			this.HostServerName = copy.HostServerName;
			this.HostServer = copy.HostServer;
			this.ReplayLagTime = copy.ReplayLagTime;
			this.TruncationLagTime = copy.TruncationLagTime;
			this.ActivationPreferenceInternal = copy.ActivationPreferenceInternal;
			this.IsValidForRead = copy.IsValidForRead;
			this.IsHostServerPresent = copy.IsHostServerPresent;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003AEC File Offset: 0x00001CEC
		public static ADDatabaseCopyWrapper CreateWrapper(DatabaseCopy databaseCopy)
		{
			if (databaseCopy == null)
			{
				return null;
			}
			return new ADDatabaseCopyWrapper(databaseCopy);
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00003AF9 File Offset: 0x00001CF9
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00003B01 File Offset: 0x00001D01
		public int ActivationPreferenceInternal { get; private set; }

		// Token: 0x060000F1 RID: 241 RVA: 0x00003B0C File Offset: 0x00001D0C
		public int CompareTo(ADDatabaseCopyWrapper other)
		{
			if (other == null)
			{
				return -1;
			}
			return this.ActivationPreferenceInternal.CompareTo(other.ActivationPreferenceInternal);
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00003B32 File Offset: 0x00001D32
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00003B3A File Offset: 0x00001D3A
		public bool IsValidForRead { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00003B43 File Offset: 0x00001D43
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00003B4B File Offset: 0x00001D4B
		public bool IsHostServerPresent { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00003B54 File Offset: 0x00001D54
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00003B5C File Offset: 0x00001D5C
		public string DatabaseName { get; private set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00003B65 File Offset: 0x00001D65
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00003B6D File Offset: 0x00001D6D
		public string HostServerName { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00003B76 File Offset: 0x00001D76
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00003B7E File Offset: 0x00001D7E
		public EnhancedTimeSpan ReplayLagTime { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00003B87 File Offset: 0x00001D87
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00003B8F File Offset: 0x00001D8F
		public EnhancedTimeSpan TruncationLagTime { get; private set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00003B98 File Offset: 0x00001D98
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00003BA0 File Offset: 0x00001DA0
		public int ActivationPreference { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00003BA9 File Offset: 0x00001DA9
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00003BB1 File Offset: 0x00001DB1
		public ADObjectId HostServer { get; set; }
	}
}
