using System;
using System.Collections.Generic;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000046 RID: 70
	public class QueryableInTransitInfo
	{
		// Token: 0x060001FE RID: 510 RVA: 0x0000DD10 File Offset: 0x0000BF10
		public QueryableInTransitInfo(MailboxState state, InTransitStatus inTransitStatus, List<object> logons)
		{
			this.DatabaseGuid = state.DatabaseGuid;
			this.MailboxGuid = state.MailboxGuid;
			this.MailboxNumber = state.MailboxNumber;
			this.MailboxType = state.MailboxType;
			this.Quarantined = state.Quarantined;
			this.Status = state.Status;
			this.TenantHint = state.TenantHint;
			this.InTransitStatus = inTransitStatus.ToString();
			if (logons != null)
			{
				this.ClientInfos = new ClientInfo[logons.Count];
				int num = 0;
				foreach (object obj in logons)
				{
					MapiLogon mapiLogon = obj as MapiLogon;
					if (mapiLogon != null && mapiLogon.Session != null)
					{
						ClientInfo clientInfo = new ClientInfo();
						clientInfo.ApplicationId = mapiLogon.Session.ApplicationId;
						clientInfo.ClientVersion = QueryableInTransitInfo.CreateClientVersion(mapiLogon.Session.ClientVersion);
						clientInfo.ClientMachineName = mapiLogon.Session.ClientMachineName;
						clientInfo.ClientProcessName = mapiLogon.Session.ClientProcessName;
						clientInfo.ConnectTime = mapiLogon.Session.ConnectTime;
						this.ClientInfos[num++] = clientInfo;
					}
				}
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000DE64 File Offset: 0x0000C064
		// (set) Token: 0x06000200 RID: 512 RVA: 0x0000DE6C File Offset: 0x0000C06C
		public Guid DatabaseGuid { get; private set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000DE75 File Offset: 0x0000C075
		// (set) Token: 0x06000202 RID: 514 RVA: 0x0000DE7D File Offset: 0x0000C07D
		public Guid MailboxGuid { get; private set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000DE86 File Offset: 0x0000C086
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0000DE8E File Offset: 0x0000C08E
		public int MailboxNumber { get; private set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000DE97 File Offset: 0x0000C097
		// (set) Token: 0x06000206 RID: 518 RVA: 0x0000DE9F File Offset: 0x0000C09F
		public MailboxInfo.MailboxType MailboxType { get; private set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000DEA8 File Offset: 0x0000C0A8
		// (set) Token: 0x06000208 RID: 520 RVA: 0x0000DEB0 File Offset: 0x0000C0B0
		public bool Quarantined { get; private set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000DEB9 File Offset: 0x0000C0B9
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000DEC1 File Offset: 0x0000C0C1
		public MailboxStatus Status { get; private set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000DECA File Offset: 0x0000C0CA
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000DED2 File Offset: 0x0000C0D2
		public TenantHint TenantHint { get; private set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000DEDB File Offset: 0x0000C0DB
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000DEE3 File Offset: 0x0000C0E3
		public string InTransitStatus { get; private set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000DEEC File Offset: 0x0000C0EC
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000DEF4 File Offset: 0x0000C0F4
		public ClientInfo[] ClientInfos { get; private set; }

		// Token: 0x06000211 RID: 529 RVA: 0x0000DF00 File Offset: 0x0000C100
		public static string CreateClientVersion(Microsoft.Exchange.Protocols.MAPI.Version version)
		{
			return string.Format("{0}.{1:00}.{2:0000}.{3:000}", new object[]
			{
				version.ProductMajor,
				version.ProductMinor,
				version.BuildMajor,
				version.BuildMinor
			});
		}
	}
}
