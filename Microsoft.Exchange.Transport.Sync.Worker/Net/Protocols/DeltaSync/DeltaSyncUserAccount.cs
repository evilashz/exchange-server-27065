using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.LiveIDAuthentication;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync
{
	// Token: 0x02000078 RID: 120
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DeltaSyncUserAccount
	{
		// Token: 0x06000562 RID: 1378 RVA: 0x000191BC File Offset: 0x000173BC
		private DeltaSyncUserAccount(string username, string password, string puid, bool puidSet, bool passportAuthenticationEnabled)
		{
			this.username = username;
			this.password = password;
			this.puid = puid;
			this.puidSet = puidSet;
			this.authPolicy = "MBI";
			this.emailSyncKey = DeltaSyncCommon.DefaultSyncKey;
			this.folderSyncKey = DeltaSyncCommon.DefaultSyncKey;
			this.passportAuthenticationEnabled = passportAuthenticationEnabled;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00019215 File Offset: 0x00017415
		public static DeltaSyncUserAccount CreateDeltaSyncUserForPassportAuth(string username, string password)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("username", username);
			SyncUtilities.ThrowIfArgumentNull("password", password);
			return new DeltaSyncUserAccount(username, password, null, false, true);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00019237 File Offset: 0x00017437
		public static DeltaSyncUserAccount CreateDeltaSyncUserForTrustedPartnerAuthWithPuid(string username, string puid)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("username", username);
			SyncUtilities.ThrowIfArgumentNull("puid", puid);
			return new DeltaSyncUserAccount(username, null, puid, true, false);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00019259 File Offset: 0x00017459
		public static DeltaSyncUserAccount CreateDeltaSyncUserForTrustedPartnerAuthWithPassword(string username, string password)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("username", username);
			SyncUtilities.ThrowIfArgumentNull("password", password);
			return new DeltaSyncUserAccount(username, password, null, false, false);
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x0001927B File Offset: 0x0001747B
		internal string Username
		{
			get
			{
				return this.username;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00019283 File Offset: 0x00017483
		internal string Password
		{
			get
			{
				return this.password;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x0001928B File Offset: 0x0001748B
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x00019293 File Offset: 0x00017493
		public AuthenticationToken AuthToken
		{
			get
			{
				return this.authToken;
			}
			set
			{
				this.authToken = value;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x0001929C File Offset: 0x0001749C
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x000192A4 File Offset: 0x000174A4
		public string PartnerClientToken
		{
			get
			{
				return this.partnerClientToken;
			}
			set
			{
				this.partnerClientToken = value;
				this.partnerClientTokenSet = true;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x000192B4 File Offset: 0x000174B4
		public bool IsPartnerClientTokenSet
		{
			get
			{
				return this.partnerClientTokenSet;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x000192BC File Offset: 0x000174BC
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x000192C4 File Offset: 0x000174C4
		internal string AuthPolicy
		{
			get
			{
				return this.authPolicy;
			}
			set
			{
				this.authPolicy = value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x000192CD File Offset: 0x000174CD
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x000192D5 File Offset: 0x000174D5
		internal string Puid
		{
			get
			{
				return this.puid;
			}
			set
			{
				this.puid = value;
				this.puidSet = true;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x000192E5 File Offset: 0x000174E5
		internal bool PassportAuthenticationEnabled
		{
			get
			{
				return this.passportAuthenticationEnabled;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x000192ED File Offset: 0x000174ED
		internal bool NeedsAuthentication
		{
			get
			{
				return (this.PassportAuthenticationEnabled && (this.AuthToken == null || this.AuthToken.IsExpired)) || (!this.PassportAuthenticationEnabled && !this.puidSet);
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00019321 File Offset: 0x00017521
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x00019329 File Offset: 0x00017529
		internal string EmailSyncKey
		{
			get
			{
				return this.emailSyncKey;
			}
			set
			{
				this.emailSyncKey = value;
				this.cachedToString = null;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00019339 File Offset: 0x00017539
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x00019341 File Offset: 0x00017541
		internal string FolderSyncKey
		{
			get
			{
				return this.folderSyncKey;
			}
			set
			{
				this.folderSyncKey = value;
				this.cachedToString = null;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00019351 File Offset: 0x00017551
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x00019359 File Offset: 0x00017559
		internal string DeltaSyncServer
		{
			get
			{
				return this.deltaSyncServer;
			}
			set
			{
				this.deltaSyncServer = value;
				this.cachedToString = null;
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0001936C File Offset: 0x0001756C
		public override string ToString()
		{
			if (this.cachedToString == null)
			{
				this.cachedToString = string.Format(CultureInfo.InvariantCulture, "DeltaSyncUsername: {0}, EmailSyncKey: {1}, FolderSyncKey: {2}, DeltaSyncServer: {3}", new object[]
				{
					this.username,
					this.emailSyncKey,
					this.folderSyncKey,
					this.deltaSyncServer
				});
			}
			return this.cachedToString;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000193C8 File Offset: 0x000175C8
		public string GetRequestQueryString()
		{
			if (this.passportAuthenticationEnabled)
			{
				return this.AuthToken.EncodedQueryStringTicket;
			}
			return this.GetPartnerAuthQueryString();
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000193E4 File Offset: 0x000175E4
		private string GetPartnerAuthQueryString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Ln={0}&Pd={1}&dspk={2}", new object[]
			{
				this.username,
				this.puid,
				this.partnerClientToken
			});
		}

		// Token: 0x040002E8 RID: 744
		private const string DefaultAuthPolicy = "MBI";

		// Token: 0x040002E9 RID: 745
		private const string PartnerAuthQueryStringFormat = "Ln={0}&Pd={1}&dspk={2}";

		// Token: 0x040002EA RID: 746
		private string username;

		// Token: 0x040002EB RID: 747
		private string password;

		// Token: 0x040002EC RID: 748
		private AuthenticationToken authToken;

		// Token: 0x040002ED RID: 749
		private string authPolicy;

		// Token: 0x040002EE RID: 750
		private string emailSyncKey;

		// Token: 0x040002EF RID: 751
		private string folderSyncKey;

		// Token: 0x040002F0 RID: 752
		private string deltaSyncServer;

		// Token: 0x040002F1 RID: 753
		private string puid;

		// Token: 0x040002F2 RID: 754
		private bool passportAuthenticationEnabled;

		// Token: 0x040002F3 RID: 755
		private string cachedToString;

		// Token: 0x040002F4 RID: 756
		private string partnerClientToken;

		// Token: 0x040002F5 RID: 757
		private bool puidSet;

		// Token: 0x040002F6 RID: 758
		private bool partnerClientTokenSet;
	}
}
