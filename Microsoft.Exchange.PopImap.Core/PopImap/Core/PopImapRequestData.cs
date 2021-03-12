using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x0200000F RID: 15
	public class PopImapRequestData
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00004E96 File Offset: 0x00003096
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00004E9E File Offset: 0x0000309E
		public Guid Id { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00004EA7 File Offset: 0x000030A7
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00004EAF File Offset: 0x000030AF
		public string ServerName { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004EB8 File Offset: 0x000030B8
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00004EC0 File Offset: 0x000030C0
		public string UserEmail { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004EC9 File Offset: 0x000030C9
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00004ED1 File Offset: 0x000030D1
		public string CommandName { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00004EDA File Offset: 0x000030DA
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00004EE2 File Offset: 0x000030E2
		public string Parameters { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00004EEB File Offset: 0x000030EB
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00004EF3 File Offset: 0x000030F3
		public string Response { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00004EFC File Offset: 0x000030FC
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00004F04 File Offset: 0x00003104
		public string ResponseType { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004F0D File Offset: 0x0000310D
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00004F15 File Offset: 0x00003115
		public string LightLogContext { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00004F1E File Offset: 0x0000311E
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00004F26 File Offset: 0x00003126
		public string Message { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00004F2F File Offset: 0x0000312F
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00004F37 File Offset: 0x00003137
		public double RequestTime { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00004F40 File Offset: 0x00003140
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00004F48 File Offset: 0x00003148
		public double RpcLatency { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00004F51 File Offset: 0x00003151
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00004F59 File Offset: 0x00003159
		public double LdapLatency { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00004F62 File Offset: 0x00003162
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00004F6A File Offset: 0x0000316A
		public bool HasErrors { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00004F73 File Offset: 0x00003173
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00004F7B File Offset: 0x0000317B
		public List<ErrorDetail> ErrorDetails { get; set; }

		// Token: 0x06000111 RID: 273 RVA: 0x00004F84 File Offset: 0x00003184
		internal PopImapRequestData(Guid id)
		{
			this.Id = id;
		}
	}
}
