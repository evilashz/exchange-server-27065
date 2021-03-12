using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000A9 RID: 169
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class IMAPJobItemSubscriptionSettings : JobItemSubscriptionSettingsBase
	{
		// Token: 0x0600095A RID: 2394 RVA: 0x000280B8 File Offset: 0x000262B8
		internal IMAPJobItemSubscriptionSettings()
		{
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x000280C0 File Offset: 0x000262C0
		// (set) Token: 0x0600095C RID: 2396 RVA: 0x000280C8 File Offset: 0x000262C8
		public string Username { get; private set; }

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x000280D1 File Offset: 0x000262D1
		// (set) Token: 0x0600095E RID: 2398 RVA: 0x000280D9 File Offset: 0x000262D9
		public string EncryptedPassword { get; private set; }

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x000280E2 File Offset: 0x000262E2
		// (set) Token: 0x06000960 RID: 2400 RVA: 0x000280EA File Offset: 0x000262EA
		public string UserRootFolder { get; private set; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x000280F3 File Offset: 0x000262F3
		public override PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return IMAPJobItemSubscriptionSettings.ImapJobItemSubscriptionSettingsPropertyDefinitions;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x000280FA File Offset: 0x000262FA
		protected override bool IsEmpty
		{
			get
			{
				return base.IsEmpty && string.IsNullOrEmpty(this.Username) && string.IsNullOrEmpty(this.EncryptedPassword) && string.IsNullOrEmpty(this.UserRootFolder);
			}
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0002812C File Offset: 0x0002632C
		public override JobItemSubscriptionSettingsBase Clone()
		{
			return new IMAPJobItemSubscriptionSettings
			{
				Username = this.Username,
				EncryptedPassword = this.EncryptedPassword,
				UserRootFolder = this.UserRootFolder,
				LastModifiedTime = base.LastModifiedTime
			};
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00028170 File Offset: 0x00026370
		public override void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			base.WriteToMessageItem(message, loaded);
			message[MigrationBatchMessageSchema.MigrationJobItemIncomingUsername] = this.Username;
			message[MigrationBatchMessageSchema.MigrationJobItemEncryptedIncomingPassword] = this.EncryptedPassword;
			if (!string.IsNullOrEmpty(this.UserRootFolder))
			{
				message[MigrationBatchMessageSchema.MigrationUserRootFolder] = this.UserRootFolder;
				return;
			}
			message.Delete(MigrationBatchMessageSchema.MigrationUserRootFolder);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x000281D1 File Offset: 0x000263D1
		public override bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			this.Username = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationJobItemIncomingUsername, null);
			this.EncryptedPassword = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationJobItemEncryptedIncomingPassword, null);
			this.UserRootFolder = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationUserRootFolder, null);
			return base.ReadFromMessageItem(message);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00028210 File Offset: 0x00026410
		public override void UpdateFromDataRow(IMigrationDataRow request)
		{
			bool flag = false;
			IMAPMigrationDataRow imapmigrationDataRow = request as IMAPMigrationDataRow;
			if (imapmigrationDataRow == null)
			{
				throw new ArgumentException("expected an IMAPMigrationDataRow", "request");
			}
			if (!object.Equals(this.Username, imapmigrationDataRow.ImapUserId))
			{
				this.Username = imapmigrationDataRow.ImapUserId;
				flag = true;
			}
			if (!object.Equals(this.EncryptedPassword, imapmigrationDataRow.EncryptedPassword))
			{
				this.EncryptedPassword = imapmigrationDataRow.EncryptedPassword;
				flag = true;
			}
			if (!object.Equals(this.UserRootFolder, imapmigrationDataRow.MigrationUserRootFolder))
			{
				this.UserRootFolder = imapmigrationDataRow.MigrationUserRootFolder;
				flag = true;
			}
			if (flag || base.LastModifiedTime == ExDateTime.MinValue)
			{
				base.LastModifiedTime = ExDateTime.UtcNow;
			}
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x000282BC File Offset: 0x000264BC
		protected override void AddDiagnosticInfoToElement(IMigrationDataProvider dataProvider, XElement parent, MigrationDiagnosticArgument argument)
		{
			parent.Add(new object[]
			{
				new XElement("Username", this.Username),
				new XElement("EncryptedPassword", this.EncryptedPassword),
				new XElement("UserRootFolder", this.UserRootFolder)
			});
		}

		// Token: 0x040003AE RID: 942
		public static readonly PropertyDefinition[] ImapJobItemSubscriptionSettingsPropertyDefinitions = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new StorePropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationJobItemIncomingUsername,
				MigrationBatchMessageSchema.MigrationJobItemEncryptedIncomingPassword,
				MigrationBatchMessageSchema.MigrationUserRootFolder
			},
			SubscriptionSettingsBase.SubscriptionSettingsBasePropertyDefinitions
		});
	}
}
