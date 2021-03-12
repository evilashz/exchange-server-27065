using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001E8 RID: 488
	internal class GroupId
	{
		// Token: 0x06000CCB RID: 3275 RVA: 0x00036A75 File Offset: 0x00034C75
		public GroupId(GroupType type, Uri uri, int version, string domain = null)
		{
			this.groupType = type;
			this.uri = uri;
			this.serverVersion = version;
			this.Domain = domain;
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00036A9A File Offset: 0x00034C9A
		public GroupId(Exception error)
		{
			Util.ThrowOnNull(error, "error");
			this.groupType = GroupType.SkippedError;
			this.error = error;
			this.serverVersion = Server.E15MinVersion;
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x00036AC6 File Offset: 0x00034CC6
		public GroupType GroupType
		{
			get
			{
				return this.groupType;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x00036ACE File Offset: 0x00034CCE
		public Uri Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x00036AD6 File Offset: 0x00034CD6
		public Exception Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x00036ADE File Offset: 0x00034CDE
		public int ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x00036AE6 File Offset: 0x00034CE6
		// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x00036AEE File Offset: 0x00034CEE
		public string Domain { get; set; }

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00036AF8 File Offset: 0x00034CF8
		public override bool Equals(object other)
		{
			GroupId groupId = (GroupId)other;
			return groupId != null && this.GroupType == groupId.GroupType && (!(this.Uri != null) || this.Uri.Equals(groupId.Uri)) && (!(this.Uri == null) || !(groupId.Uri != null)) && this.ServerVersion == groupId.ServerVersion;
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00036B74 File Offset: 0x00034D74
		public override int GetHashCode()
		{
			int num = (int)this.GroupType;
			if (this.Uri != null)
			{
				num ^= this.Uri.GetHashCode();
			}
			return num ^ this.ServerVersion.GetHashCode();
		}

		// Token: 0x0400091E RID: 2334
		private readonly GroupType groupType;

		// Token: 0x0400091F RID: 2335
		private readonly Uri uri;

		// Token: 0x04000920 RID: 2336
		private readonly Exception error;

		// Token: 0x04000921 RID: 2337
		private readonly int serverVersion;
	}
}
