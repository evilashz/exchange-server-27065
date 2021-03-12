using System;
using System.Globalization;
using System.Security.Principal;
using Microsoft.Exchange.Data.Mapi.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Mapi;
using Microsoft.Mapi.Security;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000037 RID: 55
	[Serializable]
	internal class MapiMessageStoreSession : MapiAdministrationSession
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x0000C7C9 File Offset: 0x0000A9C9
		public MapiMessageStoreSession(string serverExchangeLegacyDn, string userConnectAsExchangeLegacyDn, Fqdn serverFqdn) : base(serverExchangeLegacyDn, serverFqdn)
		{
			this.Reconfigure(userConnectAsExchangeLegacyDn, OpenStoreFlag.UseAdminPrivilege | OpenStoreFlag.IgnoreHomeMdb, null, null);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000C7E2 File Offset: 0x0000A9E2
		public MapiMessageStoreSession(string serverExchangeLegacyDn, string userConnectAsExchangeLegacyDn, Fqdn serverFqdn, Guid databaseGuid) : this(serverExchangeLegacyDn, userConnectAsExchangeLegacyDn, serverFqdn)
		{
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000C7ED File Offset: 0x0000A9ED
		public MapiMessageStoreSession(string serverExchangeLegacyDn, string userConnectAsExchangeLegacyDn, Fqdn serverFqdn, ConsistencyMode consistencyMode) : this(serverExchangeLegacyDn, userConnectAsExchangeLegacyDn, serverFqdn)
		{
			base.ConsistencyMode = consistencyMode;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000C800 File Offset: 0x0000AA00
		public MapiMessageStoreSession(string serverExchangeLegacyDn, string userConnectAsExchangeLegacyDn, Fqdn serverFqdn, OpenStoreFlag openStoreFlags, CultureInfo cultureInformation, ClientIdentityInfo clientIdentityInformation, ConsistencyMode consistencyMode) : this(serverExchangeLegacyDn, userConnectAsExchangeLegacyDn, serverFqdn, consistencyMode)
		{
			this.Reconfigure(userConnectAsExchangeLegacyDn, openStoreFlags, cultureInformation, clientIdentityInformation);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000C81A File Offset: 0x0000AA1A
		public MapiMessageStoreSession(string serverExchangeLegacyDn, string userConnectAsExchangeLegacyDn, Fqdn serverFqdn, OpenStoreFlag openStoreFlags, CultureInfo cultureInformation, WindowsIdentity windowsIdentity, ConsistencyMode consistencyMode) : this(serverExchangeLegacyDn, userConnectAsExchangeLegacyDn, serverFqdn, consistencyMode)
		{
			this.Reconfigure(userConnectAsExchangeLegacyDn, openStoreFlags, cultureInformation, windowsIdentity);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000C834 File Offset: 0x0000AA34
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.clientSecurityContext != null)
			{
				this.clientSecurityContext.Dispose();
				this.clientSecurityContext = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000C85A File Offset: 0x0000AA5A
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MapiMessageStoreSession>(this);
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000C862 File Offset: 0x0000AA62
		public ClientIdentityInfo ClientIdentityInformation
		{
			get
			{
				return this.clientIdentityInformation;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000C86A File Offset: 0x0000AA6A
		public WindowsIdentity WindowsIdentity
		{
			get
			{
				return this.windowsIdentity;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000C872 File Offset: 0x0000AA72
		public string UserConnectAsExchangeLegacyDn
		{
			get
			{
				return this.userConnectAsExchangeLegacyDn;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000C87A File Offset: 0x0000AA7A
		public OpenStoreFlag OpenStoreFlags
		{
			get
			{
				return this.openStoreFlags;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000C882 File Offset: 0x0000AA82
		public CultureInfo CultureInformation
		{
			get
			{
				return this.cultureInformation;
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000C88A File Offset: 0x0000AA8A
		public void SetSecurityAccessToken(ISecurityAccessToken securityAccessToken)
		{
			if (this.clientSecurityContext != null)
			{
				this.clientSecurityContext.Dispose();
				this.clientSecurityContext = null;
			}
			this.clientSecurityContext = new ClientSecurityContext(securityAccessToken);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000C8B2 File Offset: 0x0000AAB2
		public void Reconfigure(string userConnectAsExchangeLegacyDn, OpenStoreFlag openStoreFlags, CultureInfo cultureInformation, ClientIdentityInfo clientIdentityInformation)
		{
			if (userConnectAsExchangeLegacyDn == null)
			{
				throw new ArgumentNullException("userConnectAsExchangeLegacyDn");
			}
			this.clientIdentityInformation = clientIdentityInformation;
			this.userConnectAsExchangeLegacyDn = userConnectAsExchangeLegacyDn;
			this.openStoreFlags = openStoreFlags;
			this.cultureInformation = cultureInformation;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000C8DF File Offset: 0x0000AADF
		public void Reconfigure(string userConnectAsExchangeLegacyDn, OpenStoreFlag openStoreFlags, CultureInfo cultureInformation, WindowsIdentity windowsIdentity)
		{
			if (userConnectAsExchangeLegacyDn == null)
			{
				throw new ArgumentNullException("userConnectAsExchangeLegacyDn");
			}
			this.windowsIdentity = windowsIdentity;
			this.userConnectAsExchangeLegacyDn = userConnectAsExchangeLegacyDn;
			this.openStoreFlags = openStoreFlags;
			this.cultureInformation = cultureInformation;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000C90C File Offset: 0x0000AB0C
		internal static MapiEntryId GetAddressBookEntryIdFromLegacyDN(string userLegacyDN)
		{
			byte[] binaryEntryId = null;
			try
			{
				binaryEntryId = MapiStore.GetAddressBookEntryIdFromLegacyDN(userLegacyDN);
			}
			catch (MapiRetryableException exception)
			{
				MapiSession.ThrowWrappedException(exception, Strings.ErrorGetAddressBookEntryIdFromLegacyDN(userLegacyDN), null, null);
			}
			catch (MapiPermanentException exception2)
			{
				MapiSession.ThrowWrappedException(exception2, Strings.ErrorGetAddressBookEntryIdFromLegacyDN(userLegacyDN), null, null);
			}
			catch (MapiInvalidOperationException exception3)
			{
				MapiSession.ThrowWrappedException(exception3, Strings.ErrorGetAddressBookEntryIdFromLegacyDN(userLegacyDN), null, null);
			}
			return new MapiEntryId(binaryEntryId);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000C988 File Offset: 0x0000AB88
		internal static string GetLegacyDNFromAddressBookEntryId(MapiEntryId abbEntryId)
		{
			string result = null;
			try
			{
				result = MapiStore.GetLegacyDNFromAddressBookEntryId((byte[])abbEntryId);
			}
			catch (MapiRetryableException exception)
			{
				MapiSession.ThrowWrappedException(exception, Strings.ErrorGetGetLegacyDNFromAddressBookEntryId(abbEntryId.ToString()), null, null);
			}
			catch (MapiPermanentException exception2)
			{
				MapiSession.ThrowWrappedException(exception2, Strings.ErrorGetGetLegacyDNFromAddressBookEntryId(abbEntryId.ToString()), null, null);
			}
			catch (MapiInvalidOperationException exception3)
			{
				MapiSession.ThrowWrappedException(exception3, Strings.ErrorGetGetLegacyDNFromAddressBookEntryId(abbEntryId.ToString()), null, null);
			}
			return result;
		}

		// Token: 0x0400012A RID: 298
		private ClientIdentityInfo clientIdentityInformation;

		// Token: 0x0400012B RID: 299
		private ClientSecurityContext clientSecurityContext;

		// Token: 0x0400012C RID: 300
		private string userConnectAsExchangeLegacyDn;

		// Token: 0x0400012D RID: 301
		private OpenStoreFlag openStoreFlags;

		// Token: 0x0400012E RID: 302
		private CultureInfo cultureInformation;

		// Token: 0x0400012F RID: 303
		private WindowsIdentity windowsIdentity;
	}
}
