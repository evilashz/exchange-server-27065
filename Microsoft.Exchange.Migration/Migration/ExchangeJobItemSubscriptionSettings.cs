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
	// Token: 0x02000096 RID: 150
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExchangeJobItemSubscriptionSettings : JobItemSubscriptionSettingsBase
	{
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00025E84 File Offset: 0x00024084
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x00025E8C File Offset: 0x0002408C
		public string MailboxDN { get; private set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00025E95 File Offset: 0x00024095
		// (set) Token: 0x060008B4 RID: 2228 RVA: 0x00025E9D File Offset: 0x0002409D
		public string ExchangeServerDN { get; private set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00025EA6 File Offset: 0x000240A6
		// (set) Token: 0x060008B6 RID: 2230 RVA: 0x00025EAE File Offset: 0x000240AE
		public string ExchangeServer { get; private set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00025EB7 File Offset: 0x000240B7
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x00025EBF File Offset: 0x000240BF
		public string RPCProxyServer { get; private set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x00025EC8 File Offset: 0x000240C8
		public override PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return ExchangeJobItemSubscriptionSettings.ExchangeJobItemSubscriptionSettingsPropertyDefinitions;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x00025ECF File Offset: 0x000240CF
		protected override bool IsEmpty
		{
			get
			{
				return base.IsEmpty && string.IsNullOrEmpty(this.MailboxDN) && string.IsNullOrEmpty(this.ExchangeServerDN) && string.IsNullOrEmpty(this.ExchangeServer) && string.IsNullOrEmpty(this.RPCProxyServer);
			}
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00025F10 File Offset: 0x00024110
		public override JobItemSubscriptionSettingsBase Clone()
		{
			return new ExchangeJobItemSubscriptionSettings
			{
				MailboxDN = this.MailboxDN,
				ExchangeServer = this.ExchangeServer,
				ExchangeServerDN = this.ExchangeServerDN,
				RPCProxyServer = this.RPCProxyServer,
				LastModifiedTime = base.LastModifiedTime
			};
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00025F60 File Offset: 0x00024160
		public override void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			base.WriteToMessageItem(message, loaded);
			MigrationHelper.WriteOrDeleteNullableProperty<string>(message, MigrationBatchMessageSchema.MigrationJobItemExchangeRemoteMailboxLegacyDN, this.MailboxDN);
			MigrationHelper.WriteOrDeleteNullableProperty<string>(message, MigrationBatchMessageSchema.MigrationJobItemExchangeRemoteServerLegacyDN, this.ExchangeServerDN);
			MigrationHelper.WriteOrDeleteNullableProperty<string>(message, MigrationBatchMessageSchema.MigrationJobItemExchangeRemoteServerHostName, this.ExchangeServer);
			MigrationHelper.WriteOrDeleteNullableProperty<string>(message, MigrationBatchMessageSchema.MigrationJobItemExchangeRPCProxyServerHostName, this.RPCProxyServer);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00025FBC File Offset: 0x000241BC
		public override bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			this.MailboxDN = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationJobItemExchangeRemoteMailboxLegacyDN, null);
			this.ExchangeServerDN = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationJobItemExchangeRemoteServerLegacyDN, null);
			this.ExchangeServer = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationJobItemExchangeRemoteServerHostName, null);
			this.RPCProxyServer = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationJobItemExchangeRPCProxyServerHostName, null);
			return base.ReadFromMessageItem(message);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00026018 File Offset: 0x00024218
		public override void UpdateFromDataRow(IMigrationDataRow request)
		{
			if (!(request is ExchangeMigrationDataRow))
			{
				throw new ArgumentException("expected an ExchangeMigrationDataRow", "request");
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00026040 File Offset: 0x00024240
		internal static ExchangeJobItemSubscriptionSettings CreateFromAutodiscoverResponse(AutodiscoverClientResponse autodResponse)
		{
			ExchangeJobItemSubscriptionSettings exchangeJobItemSubscriptionSettings = new ExchangeJobItemSubscriptionSettings();
			if (autodResponse != null)
			{
				exchangeJobItemSubscriptionSettings.MailboxDN = autodResponse.MailboxDN;
				exchangeJobItemSubscriptionSettings.ExchangeServerDN = autodResponse.ExchangeServerDN;
				exchangeJobItemSubscriptionSettings.ExchangeServer = autodResponse.ExchangeServer;
				exchangeJobItemSubscriptionSettings.RPCProxyServer = autodResponse.RPCProxyServer;
			}
			exchangeJobItemSubscriptionSettings.LastModifiedTime = ExDateTime.UtcNow;
			return exchangeJobItemSubscriptionSettings;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00026094 File Offset: 0x00024294
		internal static ExchangeJobItemSubscriptionSettings CreateFromProperties(string mailboxDN, string exchangeServerDN, string exchangeServer, string rpcProxyServer)
		{
			return new ExchangeJobItemSubscriptionSettings
			{
				MailboxDN = mailboxDN,
				ExchangeServerDN = exchangeServerDN,
				ExchangeServer = exchangeServer,
				RPCProxyServer = rpcProxyServer
			};
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x000260C4 File Offset: 0x000242C4
		protected override void AddDiagnosticInfoToElement(IMigrationDataProvider dataProvider, XElement parent, MigrationDiagnosticArgument argument)
		{
			parent.Add(new object[]
			{
				new XElement("MailboxDN", this.MailboxDN),
				new XElement("ExchangeServerDN", this.ExchangeServerDN),
				new XElement("ExchangeServer", this.ExchangeServer),
				new XElement("RPCProxyServer", this.RPCProxyServer)
			});
		}

		// Token: 0x0400035B RID: 859
		public static readonly PropertyDefinition[] ExchangeJobItemSubscriptionSettingsPropertyDefinitions = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new StorePropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationJobItemExchangeRemoteMailboxLegacyDN,
				MigrationBatchMessageSchema.MigrationJobItemExchangeRemoteServerLegacyDN,
				MigrationBatchMessageSchema.MigrationJobItemExchangeRemoteServerHostName,
				MigrationBatchMessageSchema.MigrationJobItemExchangeRPCProxyServerHostName
			},
			SubscriptionSettingsBase.SubscriptionSettingsBasePropertyDefinitions
		});
	}
}
