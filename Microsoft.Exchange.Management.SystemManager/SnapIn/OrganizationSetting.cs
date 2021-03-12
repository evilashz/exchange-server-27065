using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x02000295 RID: 661
	[Serializable]
	public class OrganizationSetting
	{
		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x0007A1AB File Offset: 0x000783AB
		// (set) Token: 0x06001C01 RID: 7169 RVA: 0x0007A1B3 File Offset: 0x000783B3
		public string DisplayName { get; set; }

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x0007A1BC File Offset: 0x000783BC
		// (set) Token: 0x06001C03 RID: 7171 RVA: 0x0007A1C4 File Offset: 0x000783C4
		public Uri Uri { get; set; }

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001C04 RID: 7172 RVA: 0x0007A1CD File Offset: 0x000783CD
		// (set) Token: 0x06001C05 RID: 7173 RVA: 0x0007A1D5 File Offset: 0x000783D5
		public bool LogonWithDefaultCredential { get; set; }

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x0007A1DE File Offset: 0x000783DE
		// (set) Token: 0x06001C07 RID: 7175 RVA: 0x0007A1E6 File Offset: 0x000783E6
		public string CredentialKey { get; set; }

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x0007A1EF File Offset: 0x000783EF
		// (set) Token: 0x06001C09 RID: 7177 RVA: 0x0007A1F7 File Offset: 0x000783F7
		public string Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
				this.supportedVersionList = null;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001C0A RID: 7178 RVA: 0x0007A207 File Offset: 0x00078407
		// (set) Token: 0x06001C0B RID: 7179 RVA: 0x0007A20F File Offset: 0x0007840F
		public OrganizationType Type { get; set; }

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001C0C RID: 7180 RVA: 0x0007A218 File Offset: 0x00078418
		// (set) Token: 0x06001C0D RID: 7181 RVA: 0x0007A220 File Offset: 0x00078420
		public SupportedVersionList SupportedVersionList
		{
			get
			{
				return this.supportedVersionList;
			}
			set
			{
				this.supportedVersionList = value;
			}
		}

		// Token: 0x04000A62 RID: 2658
		private string key;

		// Token: 0x04000A63 RID: 2659
		private SupportedVersionList supportedVersionList;
	}
}
