using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002A1 RID: 673
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class StoreEntryIdInformation
	{
		// Token: 0x06001BF4 RID: 7156 RVA: 0x0008113C File Offset: 0x0007F33C
		internal StoreEntryIdInformation()
		{
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x00081144 File Offset: 0x0007F344
		// (set) Token: 0x06001BF6 RID: 7158 RVA: 0x0008114C File Offset: 0x0007F34C
		public string MailboxLegacyDN { get; internal set; }

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x00081155 File Offset: 0x0007F355
		// (set) Token: 0x06001BF8 RID: 7160 RVA: 0x0008115D File Offset: 0x0007F35D
		public string ServerName { get; internal set; }

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x00081166 File Offset: 0x0007F366
		// (set) Token: 0x06001BFA RID: 7162 RVA: 0x0008116E File Offset: 0x0007F36E
		public string ProviderFileName { get; internal set; }

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x00081177 File Offset: 0x0007F377
		// (set) Token: 0x06001BFC RID: 7164 RVA: 0x0008117F File Offset: 0x0007F37F
		public byte[] ProviderFileNameBytes { get; internal set; }

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06001BFD RID: 7165 RVA: 0x00081188 File Offset: 0x0007F388
		// (set) Token: 0x06001BFE RID: 7166 RVA: 0x00081190 File Offset: 0x0007F390
		public byte Version { get; internal set; }

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06001BFF RID: 7167 RVA: 0x00081199 File Offset: 0x0007F399
		// (set) Token: 0x06001C00 RID: 7168 RVA: 0x000811A1 File Offset: 0x0007F3A1
		public byte[] WrappedStoreGuid { get; internal set; }

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x000811AA File Offset: 0x0007F3AA
		// (set) Token: 0x06001C02 RID: 7170 RVA: 0x000811B2 File Offset: 0x0007F3B2
		public byte[] StoreGuid { get; internal set; }

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x000811BB File Offset: 0x0007F3BB
		// (set) Token: 0x06001C04 RID: 7172 RVA: 0x000811C3 File Offset: 0x0007F3C3
		public byte[] Flags { get; internal set; }

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x000811CC File Offset: 0x0007F3CC
		// (set) Token: 0x06001C06 RID: 7174 RVA: 0x000811D4 File Offset: 0x0007F3D4
		public uint FlagsInt { get; internal set; }

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06001C07 RID: 7175 RVA: 0x000811DD File Offset: 0x0007F3DD
		// (set) Token: 0x06001C08 RID: 7176 RVA: 0x000811E5 File Offset: 0x0007F3E5
		public byte[] StoreEntryIdBytes { get; internal set; }

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06001C09 RID: 7177 RVA: 0x000811EE File Offset: 0x0007F3EE
		// (set) Token: 0x06001C0A RID: 7178 RVA: 0x000811F6 File Offset: 0x0007F3F6
		public bool IsPublic { get; internal set; }
	}
}
