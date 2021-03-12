using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Assistants;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200002C RID: 44
	internal sealed class DatabaseInfo : Base, IDatabaseInfo
	{
		// Token: 0x0600014C RID: 332 RVA: 0x0000661C File Offset: 0x0000481C
		internal DatabaseInfo(Guid guid, string databaseName, string databaseLegacyDN, bool isPublic, EventLogger eventLogger, bool zeroBox = true)
		{
			if (zeroBox)
			{
				this.guid = guid;
				this.systemMailboxName = "SystemMailbox{" + this.guid + "}";
				this.databaseName = databaseName;
				this.isPublic = isPublic;
				this.displayName = string.Concat(new object[]
				{
					this.databaseName,
					" (",
					this.guid,
					")"
				});
				if (!this.isPublic)
				{
					Guid empty = Guid.Empty;
					this.systemAttendantMailboxGuid = Guid.Empty;
					this.systemAttendantMailboxPresent = (this.guid == empty);
				}
			}
			else
			{
				this.Initialize(guid, databaseName, databaseLegacyDN, isPublic);
			}
			SingletonEventLogger.GetSingleton(eventLogger.ServiceName);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000066F6 File Offset: 0x000048F6
		internal DatabaseInfo(Guid guid, string databaseName, string databaseLegacyDN, bool isPublic)
		{
			this.Initialize(guid, databaseName, databaseLegacyDN, isPublic);
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00006714 File Offset: 0x00004914
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000671C File Offset: 0x0000491C
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00006724 File Offset: 0x00004924
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000672C File Offset: 0x0000492C
		public Guid SystemMailboxGuid
		{
			get
			{
				if (this.systemMailboxPrincipal != null)
				{
					return this.systemMailboxPrincipal.MailboxInfo.MailboxGuid;
				}
				return Guid.Empty;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000674C File Offset: 0x0000494C
		public bool IsPublic
		{
			get
			{
				return this.isPublic;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006754 File Offset: 0x00004954
		public List<MailboxInformation> GetMailboxTable(ClientType clientType, PropertyTagPropertyDefinition[] properties)
		{
			List<PropTag> list = new List<PropTag>(MailboxTableQuery.RequiredMailboxTableProperties.Length + properties.Length);
			list.AddRange(MailboxTableQuery.RequiredMailboxTableProperties);
			for (int i = 0; i < properties.Length; i++)
			{
				list.Add((PropTag)properties[i].PropertyTag);
			}
			PropValue[][] mailboxes = MailboxTableQuery.GetMailboxes((clientType == ClientType.EventBased) ? "Client=EBA" : "Client=TBA", this, list.ToArray());
			List<MailboxInformation> list2 = new List<MailboxInformation>(mailboxes.Length);
			foreach (PropValue[] mailboxPropValue in mailboxes)
			{
				MailboxInformation mailboxInformation = this.GetMailboxInformation(mailboxPropValue);
				if (mailboxInformation != null)
				{
					list2.Add(mailboxInformation);
				}
			}
			return list2;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000067F4 File Offset: 0x000049F4
		private void Initialize(Guid guid, string databaseName, string databaseLegacyDN, bool isPublic)
		{
			this.guid = guid;
			this.systemMailboxName = "SystemMailbox{" + this.guid + "}";
			this.databaseName = databaseName;
			this.isPublic = isPublic;
			this.displayName = string.Concat(new object[]
			{
				this.databaseName,
				" (",
				this.guid,
				")"
			});
			Exception ex = null;
			if (!this.isPublic)
			{
				try
				{
					this.systemMailboxPrincipal = ExchangePrincipal.FromADSystemMailbox(ADSessionSettings.FromRootOrgScopeSet(), this.FindSystemMailbox(), LocalServer.GetServer());
				}
				catch (DataValidationException ex2)
				{
					ex = ex2;
				}
				catch (ObjectNotFoundException ex3)
				{
					ex = ex3;
				}
				catch (ADExternalException ex4)
				{
					ex = ex4;
				}
				if (ex != null)
				{
					ExTraceGlobals.DatabaseInfoTracer.TraceError<DatabaseInfo, Exception>((long)this.GetHashCode(), "{0}: Unable to find valid system mailbox. Exception: {1}", this, ex);
					throw new MissingSystemMailboxException(this.DisplayName, ex, base.Logger);
				}
				Guid guid2 = Guid.Empty;
				try
				{
					ADSystemAttendantMailbox systemAttendant = this.GetSystemAttendant();
					if (systemAttendant != null && systemAttendant.Database != null)
					{
						guid2 = systemAttendant.Database.ObjectGuid;
						this.systemAttendantMailboxGuid = ((systemAttendant.ExchangeGuid == Guid.Empty) ? systemAttendant.Guid : systemAttendant.ExchangeGuid);
						ExTraceGlobals.DatabaseInfoTracer.TraceDebug<DatabaseInfo, Guid, Guid>((long)this.GetHashCode(), "{0}: System Attendant Mailbox: Database GUID: {1}, Mailbox GUID: {2}", this, guid2, this.systemAttendantMailboxGuid);
					}
				}
				catch (DataValidationException ex5)
				{
					ex = ex5;
				}
				catch (ObjectNotFoundException ex6)
				{
					ex = ex6;
				}
				if (ex != null)
				{
					base.TracePfd("PFD AIS {0} {1}: System Attendant Mailbox: Database GUID: {2}, Mailbox GUID: {3}", new object[]
					{
						30551,
						this,
						ex
					});
					throw new MissingSystemMailboxException(this.DisplayName, ex, base.Logger);
				}
				this.systemAttendantMailboxPresent = (this.guid == guid2);
				ExTraceGlobals.DatabaseInfoTracer.TraceDebug<DatabaseInfo>((long)this.GetHashCode(), "{0}: Created database info", this);
			}
			base.TracePfd("PFD AIS {0} {1}: Created database info Sucessfully", new object[]
			{
				19287,
				this
			});
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006A38 File Offset: 0x00004C38
		private MailboxInformation GetMailboxInformation(PropValue[] mailboxPropValue)
		{
			PropValue mailboxProperty = MailboxTableQuery.GetMailboxProperty(mailboxPropValue, PropTag.UserGuid);
			PropValue mailboxProperty2 = MailboxTableQuery.GetMailboxProperty(mailboxPropValue, PropTag.DisplayName);
			PropValue mailboxProperty3 = MailboxTableQuery.GetMailboxProperty(mailboxPropValue, PropTag.DateDiscoveredAbsentInDS);
			DateTime lastLogonTime = MailboxInformation.GetLastLogonTime(mailboxPropValue);
			if (mailboxProperty.PropTag != PropTag.UserGuid || mailboxProperty2.PropTag != PropTag.DisplayName)
			{
				return null;
			}
			if (mailboxProperty3.PropTag == PropTag.DateDiscoveredAbsentInDS)
			{
				return null;
			}
			return MailboxInformation.Create(mailboxProperty.GetBytes(), this.Guid, mailboxProperty2.GetString(), null, mailboxPropValue, lastLogonTime, null);
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00006AC0 File Offset: 0x00004CC0
		private string DistinguishedName
		{
			get
			{
				if (this.distinguishedName == null)
				{
					IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 401, "DistinguishedName", "f:\\15.00.1497\\sources\\dev\\assistants\\src\\Assistants\\DatabaseInfo.cs");
					MailboxDatabase mailboxDatabase = tenantOrTopologyConfigurationSession.Read<MailboxDatabase>(new ADObjectId(this.Guid));
					this.distinguishedName = mailboxDatabase.DistinguishedName;
				}
				return this.distinguishedName;
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006B19 File Offset: 0x00004D19
		public override string ToString()
		{
			return this.displayName;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006B24 File Offset: 0x00004D24
		public MailboxSession GetSystemMailbox(ClientType clientType, string actionInfo)
		{
			MailboxSession mailbox;
			try
			{
				mailbox = this.GetMailbox(this.systemMailboxPrincipal, clientType, actionInfo);
			}
			catch (ObjectNotFoundException ex)
			{
				ExTraceGlobals.DatabaseInfoTracer.TraceError<DatabaseInfo, string, ObjectNotFoundException>((long)this.GetHashCode(), "{0}: unable to open system mailbox named '{1}'. Exception: {2}", this, this.systemMailboxName, ex);
				throw new MissingSystemMailboxException(this.DisplayName, ex, base.Logger);
			}
			mailbox.ExTimeZone = ExTimeZone.CurrentTimeZone;
			ExTraceGlobals.DatabaseInfoTracer.TraceDebug<DatabaseInfo, string>((long)this.GetHashCode(), "{0}: Opened system mailbox named '{1}'", this, this.systemMailboxName);
			return mailbox;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006BB0 File Offset: 0x00004DB0
		public MailboxSession GetMailbox(ExchangePrincipal principal, ClientType clientType, string actionInfo)
		{
			string str = (clientType == ClientType.EventBased) ? "Client=EBA;Service=" : "Client=TBA;Service=";
			return MailboxSession.OpenAsAdmin(principal, CultureInfo.InvariantCulture, str + base.Logger.ServiceName + ";Action=" + actionInfo, true);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006BF0 File Offset: 0x00004DF0
		public bool IsUserMailbox(Guid mailboxGuid)
		{
			return this.isPublic || (mailboxGuid != this.SystemMailboxGuid && !this.IsSystemAttendantMailbox(mailboxGuid));
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006C16 File Offset: 0x00004E16
		public bool IsSystemAttendantMailbox(Guid mailboxGuid)
		{
			return this.systemAttendantMailboxPresent && this.systemAttendantMailboxGuid == mailboxGuid;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00006C2E File Offset: 0x00004E2E
		IEnumerable<IMailboxInformation> IDatabaseInfo.GetMailboxTable(ClientType clientType, PropertyTagPropertyDefinition[] properties)
		{
			return this.GetMailboxTable(clientType, properties);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006C38 File Offset: 0x00004E38
		private ADSystemMailbox FindSystemMailbox()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 526, "FindSystemMailbox", "f:\\15.00.1497\\sources\\dev\\assistants\\src\\Assistants\\DatabaseInfo.cs");
			ADRecipient[] array = tenantOrRootOrgRecipientSession.Find(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, this.systemMailboxName), null, 1);
			if (array.Length != 1 || !(array[0] is ADSystemMailbox))
			{
				ExTraceGlobals.DatabaseInfoTracer.TraceError<DatabaseInfo, int, string>((long)this.GetHashCode(), "{0}: Found {1} mailboxes named '{2}' in the AD", this, array.Length, this.systemMailboxName);
				throw new MissingSystemMailboxException(this.DisplayName, base.Logger);
			}
			return (ADSystemMailbox)array[0];
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006CCC File Offset: 0x00004ECC
		private ADSystemAttendantMailbox GetSystemAttendant()
		{
			string text = LocalServer.GetServer().ExchangeLegacyDN + "/cn=Microsoft System Attendant";
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 571, "GetSystemAttendant", "f:\\15.00.1497\\sources\\dev\\assistants\\src\\Assistants\\DatabaseInfo.cs");
			ADRecipient adrecipient = null;
			try
			{
				adrecipient = tenantOrRootOrgRecipientSession.FindByLegacyExchangeDN(text);
			}
			catch (DataValidationException arg)
			{
				ExTraceGlobals.DatabaseInfoTracer.TraceError<DataValidationException>((long)this.GetHashCode(), "{0}: Invalid system attendant mailbox: {1}", arg);
			}
			if (adrecipient == null || !(adrecipient is ADSystemAttendantMailbox))
			{
				ExTraceGlobals.DatabaseInfoTracer.TraceError<DatabaseInfo, string>((long)this.GetHashCode(), "{0}: Unable to find valid SA mailbox with legDN: {1}", this, text);
				return null;
			}
			return (ADSystemAttendantMailbox)adrecipient;
		}

		// Token: 0x04000134 RID: 308
		private const string SystemAttendantRelativeLegDN = "/cn=Microsoft System Attendant";

		// Token: 0x04000135 RID: 309
		private string distinguishedName;

		// Token: 0x04000136 RID: 310
		private Guid guid;

		// Token: 0x04000137 RID: 311
		private string systemMailboxName;

		// Token: 0x04000138 RID: 312
		private string databaseName;

		// Token: 0x04000139 RID: 313
		private ExchangePrincipal systemMailboxPrincipal;

		// Token: 0x0400013A RID: 314
		private bool systemAttendantMailboxPresent;

		// Token: 0x0400013B RID: 315
		private bool isPublic;

		// Token: 0x0400013C RID: 316
		private string displayName;

		// Token: 0x0400013D RID: 317
		private Guid systemAttendantMailboxGuid = Guid.Empty;
	}
}
