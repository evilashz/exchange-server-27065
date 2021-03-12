using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000317 RID: 791
	[Serializable]
	public class MailboxComplianceConfiguration : IConfigurable
	{
		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001A98 RID: 6808 RVA: 0x00075D00 File Offset: 0x00073F00
		// (set) Token: 0x06001A99 RID: 6809 RVA: 0x00075D08 File Offset: 0x00073F08
		public ObjectId Identity
		{
			get
			{
				return this.identity;
			}
			set
			{
				this.identity = value;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x00075D11 File Offset: 0x00073F11
		// (set) Token: 0x06001A9B RID: 6811 RVA: 0x00075D19 File Offset: 0x00073F19
		public OrganizationId OrganizationId
		{
			get
			{
				return this.orgId;
			}
			internal set
			{
				this.orgId = value;
			}
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x00075D22 File Offset: 0x00073F22
		ValidationError[] IConfigurable.Validate()
		{
			return new ValidationError[0];
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x00075D2A File Offset: 0x00073F2A
		void IConfigurable.CopyChangesFrom(IConfigurable changedObject)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x00075D31 File Offset: 0x00073F31
		void IConfigurable.ResetChangeTracking()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x00075D38 File Offset: 0x00073F38
		bool IConfigurable.IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x00075D3B File Offset: 0x00073F3B
		ObjectState IConfigurable.ObjectState
		{
			get
			{
				return ObjectState.New;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x00075D3E File Offset: 0x00073F3E
		// (set) Token: 0x06001AA2 RID: 6818 RVA: 0x00075D46 File Offset: 0x00073F46
		internal bool NoPolicy
		{
			get
			{
				return this.noPolicy;
			}
			set
			{
				this.noPolicy = value;
			}
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x00075D4F File Offset: 0x00073F4F
		internal MailboxComplianceConfiguration(MailboxSession mailboxSession)
		{
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x00075D57 File Offset: 0x00073F57
		internal void Save(MailboxSession mailboxSession)
		{
		}

		// Token: 0x04000B8E RID: 2958
		private ObjectId identity;

		// Token: 0x04000B8F RID: 2959
		private OrganizationId orgId;

		// Token: 0x04000B90 RID: 2960
		private bool noPolicy;
	}
}
