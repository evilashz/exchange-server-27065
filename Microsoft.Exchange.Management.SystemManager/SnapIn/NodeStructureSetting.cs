using System;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x02000297 RID: 663
	[Serializable]
	public class NodeStructureSetting
	{
		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001C0F RID: 7183 RVA: 0x0007A231 File Offset: 0x00078431
		// (set) Token: 0x06001C10 RID: 7184 RVA: 0x0007A239 File Offset: 0x00078439
		public string DisplayName { get; set; }

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x0007A242 File Offset: 0x00078442
		// (set) Token: 0x06001C12 RID: 7186 RVA: 0x0007A24A File Offset: 0x0007844A
		public Uri Uri { get; set; }

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001C13 RID: 7187 RVA: 0x0007A253 File Offset: 0x00078453
		// (set) Token: 0x06001C14 RID: 7188 RVA: 0x0007A25B File Offset: 0x0007845B
		public bool LogonWithDefaultCredential { get; set; }

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001C15 RID: 7189 RVA: 0x0007A264 File Offset: 0x00078464
		// (set) Token: 0x06001C16 RID: 7190 RVA: 0x0007A26C File Offset: 0x0007846C
		public string CredentialKey { get; set; }

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001C17 RID: 7191 RVA: 0x0007A275 File Offset: 0x00078475
		// (set) Token: 0x06001C18 RID: 7192 RVA: 0x0007A27D File Offset: 0x0007847D
		public NodeStructureSettingState State { get; set; }

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001C19 RID: 7193 RVA: 0x0007A286 File Offset: 0x00078486
		// (set) Token: 0x06001C1A RID: 7194 RVA: 0x0007A28E File Offset: 0x0007848E
		public string Key { get; set; }

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001C1B RID: 7195 RVA: 0x0007A297 File Offset: 0x00078497
		// (set) Token: 0x06001C1C RID: 7196 RVA: 0x0007A29F File Offset: 0x0007849F
		public OrganizationType Type { get; set; }
	}
}
