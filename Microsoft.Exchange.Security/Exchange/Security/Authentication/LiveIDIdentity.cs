using System;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000074 RID: 116
	[Serializable]
	public class LiveIDIdentity : SidBasedIdentity
	{
		// Token: 0x060003F9 RID: 1017 RVA: 0x0002030E File Offset: 0x0001E50E
		public LiveIDIdentity(string userPrincipal, string userSid, string memberName, string partitionId = null, LiveIdLoginAttributes loginAttributes = null, string userNetId = null) : base(userPrincipal, userSid, memberName, "OrgId", partitionId)
		{
			this.LoginAttributes = loginAttributes;
			this.UserNetId = userNetId;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00020330 File Offset: 0x0001E530
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x00020338 File Offset: 0x0001E538
		public bool HasAcceptedAccruals
		{
			get
			{
				return this.hasAcceptedAccruals;
			}
			protected internal set
			{
				this.hasAcceptedAccruals = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00020341 File Offset: 0x0001E541
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x00020349 File Offset: 0x0001E549
		public LiveIdLoginAttributes LoginAttributes { get; private set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00020352 File Offset: 0x0001E552
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x0002035A File Offset: 0x0001E55A
		internal string UserNetId { get; private set; }

		// Token: 0x04000441 RID: 1089
		internal const string LiveIdAuthType = "OrgId";

		// Token: 0x04000442 RID: 1090
		private bool hasAcceptedAccruals;
	}
}
