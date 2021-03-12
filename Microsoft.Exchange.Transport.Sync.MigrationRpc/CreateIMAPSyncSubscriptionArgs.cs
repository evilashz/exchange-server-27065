using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Dkm;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Imap;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x02000015 RID: 21
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CreateIMAPSyncSubscriptionArgs : AbstractCreateSyncSubscriptionArgs
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002D80 File Offset: 0x00000F80
		internal CreateIMAPSyncSubscriptionArgs(ADObjectId organizationalUnit, string userLegacyDN, string subscriptionName, string userDisplayName, SmtpAddress imapEmailAddress, string imapLogOnName, string logonPasswordEncrypted, Fqdn imapServer, int imapPort, IEnumerable<string> foldersToExclude, IMAPSecurityMechanism imapSecurityMechanism, IMAPAuthenticationMechanism imapAuthenticationMechanism, string userRootFolder, bool forceNew) : base(AggregationSubscriptionType.IMAP, organizationalUnit, subscriptionName, userLegacyDN, userDisplayName, imapEmailAddress, forceNew)
		{
			this.imapLogOnName = imapLogOnName;
			this.logonPasswordEncrypted = logonPasswordEncrypted;
			this.imapServer = imapServer;
			this.imapPort = imapPort;
			this.imapSecurityMechanism = imapSecurityMechanism;
			this.imapAuthenticationMechanism = imapAuthenticationMechanism;
			this.foldersToExclude = new List<string>(5);
			this.migrationUserRootFolder = userRootFolder;
			if (foldersToExclude != null)
			{
				this.foldersToExclude.AddRange(foldersToExclude);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002DF3 File Offset: 0x00000FF3
		internal string ImapLogOnName
		{
			get
			{
				return this.imapLogOnName;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002DFB File Offset: 0x00000FFB
		internal string LogonPasswordEncrypted
		{
			get
			{
				return this.logonPasswordEncrypted;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002E03 File Offset: 0x00001003
		internal Fqdn ImapServer
		{
			get
			{
				return this.imapServer;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002E0B File Offset: 0x0000100B
		internal int ImapPort
		{
			get
			{
				return this.imapPort;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002E13 File Offset: 0x00001013
		internal IMAPSecurityMechanism ImapSecurityMechanism
		{
			get
			{
				return this.imapSecurityMechanism;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002E1B File Offset: 0x0000101B
		internal IMAPAuthenticationMechanism ImapAuthenticationMechanism
		{
			get
			{
				return this.imapAuthenticationMechanism;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002E23 File Offset: 0x00001023
		internal IEnumerable<string> FoldersToExclude
		{
			get
			{
				return this.foldersToExclude;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002E2B File Offset: 0x0000102B
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002E33 File Offset: 0x00001033
		internal string MigrationUserRootFolder
		{
			get
			{
				return this.migrationUserRootFolder;
			}
			set
			{
				this.migrationUserRootFolder = value;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002E3C File Offset: 0x0000103C
		internal static CreateIMAPSyncSubscriptionArgs Unmarshal(MdbefPropertyCollection inputArgs)
		{
			string[] array = null;
			string text;
			if (MigrationRpcHelper.TryReadValue<string>(inputArgs, 2686058527U, out text) && !string.IsNullOrEmpty(text))
			{
				array = text.Split(new string[]
				{
					AggregationSubscription.FolderExclusionDelimiter
				}, StringSplitOptions.RemoveEmptyEntries);
			}
			string userRootFolder = MigrationRpcHelper.ReadValue<string>(inputArgs, 2686124063U, null);
			bool forceNew = MigrationRpcHelper.ReadValue<bool>(inputArgs, 2686255115U, false);
			return new CreateIMAPSyncSubscriptionArgs(MigrationRpcHelper.ReadADObjectId(inputArgs, 2688811266U), MigrationRpcHelper.ReadValue<string>(inputArgs, 2684485663U), MigrationRpcHelper.ReadValue<string>(inputArgs, 2685403167U), MigrationRpcHelper.ReadValue<string>(inputArgs, 2685927455U), new SmtpAddress(MigrationRpcHelper.ReadValue<string>(inputArgs, 2685599775U)), MigrationRpcHelper.ReadValue<string>(inputArgs, 2685665311U), MigrationRpcHelper.ReadValue<string>(inputArgs, 2685992991U), new Fqdn(MigrationRpcHelper.ReadValue<string>(inputArgs, 2685468703U)), MigrationRpcHelper.ReadValue<int>(inputArgs, 2685534211U), array, MigrationRpcHelper.ReadEnum<IMAPSecurityMechanism>(inputArgs, 2685796355U), MigrationRpcHelper.ReadEnum<IMAPAuthenticationMechanism>(inputArgs, 2685861891U), userRootFolder, forceNew);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002F24 File Offset: 0x00001124
		internal override MdbefPropertyCollection Marshal()
		{
			MdbefPropertyCollection mdbefPropertyCollection = base.Marshal();
			mdbefPropertyCollection[2685665311U] = this.imapLogOnName;
			mdbefPropertyCollection[2685992991U] = this.logonPasswordEncrypted;
			mdbefPropertyCollection[2685468703U] = this.imapServer.ToString();
			mdbefPropertyCollection[2685534211U] = this.imapPort;
			if (this.foldersToExclude.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string value in this.foldersToExclude)
				{
					stringBuilder.Append(value);
					stringBuilder.Append(AggregationSubscription.FolderExclusionDelimiter);
				}
				mdbefPropertyCollection[2686058527U] = stringBuilder.ToString();
			}
			mdbefPropertyCollection[2685796355U] = (int)this.imapSecurityMechanism;
			mdbefPropertyCollection[2685861891U] = (int)this.imapAuthenticationMechanism;
			if (this.MigrationUserRootFolder != null)
			{
				mdbefPropertyCollection[2686124063U] = this.MigrationUserRootFolder;
			}
			return mdbefPropertyCollection;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003044 File Offset: 0x00001244
		internal override AggregationSubscription CreateInMemorySubscription()
		{
			IMAPAggregationSubscription imapaggregationSubscription = new IMAPAggregationSubscription();
			this.FillSubscription(imapaggregationSubscription);
			return imapaggregationSubscription;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003060 File Offset: 0x00001260
		internal override void FillSubscription(AggregationSubscription aggregationSubscription)
		{
			SyncUtilities.ThrowIfArgumentNull("aggregationSubscription", aggregationSubscription);
			IMAPAggregationSubscription imapaggregationSubscription = (IMAPAggregationSubscription)aggregationSubscription;
			base.FillSubscription(aggregationSubscription);
			imapaggregationSubscription.SendAsState = SendAsState.Disabled;
			imapaggregationSubscription.UserDisplayName = base.UserDisplayName;
			imapaggregationSubscription.UserEmailAddress = base.SmtpAddress;
			imapaggregationSubscription.IMAPLogOnName = this.ImapLogOnName;
			imapaggregationSubscription.LogonPasswordSecured = CreateIMAPSyncSubscriptionArgs.exchangeGroupKey.EncryptedStringToSecureString(this.LogonPasswordEncrypted);
			imapaggregationSubscription.IMAPServer = this.ImapServer;
			imapaggregationSubscription.IMAPPort = this.ImapPort;
			imapaggregationSubscription.IMAPSecurity = this.ImapSecurityMechanism;
			imapaggregationSubscription.IMAPAuthentication = this.ImapAuthenticationMechanism;
			imapaggregationSubscription.SetFoldersToExclude(this.FoldersToExclude);
			imapaggregationSubscription.ImapPathPrefix = this.MigrationUserRootFolder;
		}

		// Token: 0x04000076 RID: 118
		private const int DefaultFolderToExcludeCapacity = 5;

		// Token: 0x04000077 RID: 119
		private static readonly ExchangeGroupKey exchangeGroupKey = new ExchangeGroupKey(null, "Microsoft Exchange DKM");

		// Token: 0x04000078 RID: 120
		private readonly string imapLogOnName;

		// Token: 0x04000079 RID: 121
		private readonly string logonPasswordEncrypted;

		// Token: 0x0400007A RID: 122
		private readonly Fqdn imapServer;

		// Token: 0x0400007B RID: 123
		private readonly int imapPort;

		// Token: 0x0400007C RID: 124
		private readonly IMAPSecurityMechanism imapSecurityMechanism;

		// Token: 0x0400007D RID: 125
		private readonly IMAPAuthenticationMechanism imapAuthenticationMechanism;

		// Token: 0x0400007E RID: 126
		private readonly List<string> foldersToExclude;

		// Token: 0x0400007F RID: 127
		private string migrationUserRootFolder;
	}
}
