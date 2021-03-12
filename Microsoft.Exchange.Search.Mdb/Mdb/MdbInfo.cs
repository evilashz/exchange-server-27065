using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x0200001E RID: 30
	internal class MdbInfo : IEquatable<MdbInfo>
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00006716 File Offset: 0x00004916
		internal MdbInfo(MailboxDatabase mailboxDatabase) : this(mailboxDatabase.Guid, mailboxDatabase.Name, mailboxDatabase.Server.Name, Path.GetDirectoryName(mailboxDatabase.EdbFilePath.ToString()), mailboxDatabase.DatabaseCopies.Length)
		{
			this.EnableOwningServerUpdate = true;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00006754 File Offset: 0x00004954
		internal MdbInfo(Guid mdbGuid) : this(mdbGuid, string.Empty, string.Empty, string.Empty, 1)
		{
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006770 File Offset: 0x00004970
		internal MdbInfo(MdbInfo mdbInfo) : this(mdbInfo.Guid, mdbInfo.Name, mdbInfo.OwningServer, mdbInfo.DatabasePath, mdbInfo.NumberOfCopies)
		{
			this.ActivationPreference = mdbInfo.ActivationPreference;
			this.MountedOnLocalServer = mdbInfo.MountedOnLocalServer;
			this.IsSuspended = mdbInfo.IsSuspended;
			this.NumberOfItems = mdbInfo.NumberOfItems;
			this.IsInstantSearchEnabled = mdbInfo.IsInstantSearchEnabled;
			this.IsRefinersEnabled = mdbInfo.IsRefinersEnabled;
			this.IsCatalogSuspended = mdbInfo.IsCatalogSuspended;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000067F8 File Offset: 0x000049F8
		internal MdbInfo(Guid mdbGuid, string mdbName, string owningServer, string databasePath, int numberOfCopies = 1)
		{
			this.guid = mdbGuid;
			this.name = mdbName;
			this.OwningServer = owningServer;
			this.DatabasePath = databasePath;
			this.NumberOfCopies = numberOfCopies;
			this.cachedToStringValue = string.Format("{0} ({1})", this.Guid, this.Name);
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00006851 File Offset: 0x00004A51
		internal Guid Guid
		{
			[DebuggerStepThrough]
			get
			{
				return this.guid;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00006859 File Offset: 0x00004A59
		internal string Name
		{
			[DebuggerStepThrough]
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00006861 File Offset: 0x00004A61
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00006869 File Offset: 0x00004A69
		internal string OwningServer { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00006872 File Offset: 0x00004A72
		// (set) Token: 0x0600009E RID: 158 RVA: 0x0000687A File Offset: 0x00004A7A
		internal string DatabasePath { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00006883 File Offset: 0x00004A83
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x0000688B File Offset: 0x00004A8B
		internal int NumberOfCopies { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00006894 File Offset: 0x00004A94
		internal string IndexSystemName
		{
			get
			{
				return FastIndexVersion.GetIndexSystemName(this.Guid);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000068A1 File Offset: 0x00004AA1
		internal virtual Guid? SystemAttendantGuid
		{
			get
			{
				this.InitializeSystemMailboxAndSystemAttendantGuids(false);
				return this.systemAttendantGuid;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000068B0 File Offset: 0x00004AB0
		internal virtual Guid SystemMailboxGuid
		{
			get
			{
				this.InitializeSystemMailboxAndSystemAttendantGuids(false);
				return this.systemMailboxGuid;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000068BF File Offset: 0x00004ABF
		internal ADSystemMailbox SystemMailbox
		{
			get
			{
				this.InitializeSystemMailboxAndSystemAttendantGuids(false);
				return this.systemMailbox;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000068CE File Offset: 0x00004ACE
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x000068D6 File Offset: 0x00004AD6
		internal VersionInfo CatalogVersion { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000068DF File Offset: 0x00004ADF
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x000068E7 File Offset: 0x00004AE7
		internal bool PreferredActiveCopy { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000068F0 File Offset: 0x00004AF0
		// (set) Token: 0x060000AA RID: 170 RVA: 0x000068F8 File Offset: 0x00004AF8
		internal int ActivationPreference { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00006901 File Offset: 0x00004B01
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00006909 File Offset: 0x00004B09
		internal ICollection<MdbCopy> DatabaseCopies { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00006912 File Offset: 0x00004B12
		// (set) Token: 0x060000AE RID: 174 RVA: 0x0000691A File Offset: 0x00004B1A
		internal int MaxSupportedVersion { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00006923 File Offset: 0x00004B23
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000692B File Offset: 0x00004B2B
		internal IndexStatusErrorCode NotIndexed { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00006934 File Offset: 0x00004B34
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x0000693C File Offset: 0x00004B3C
		internal bool EnableOwningServerUpdate { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00006945 File Offset: 0x00004B45
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x0000694D File Offset: 0x00004B4D
		internal bool MountedOnLocalServer { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00006958 File Offset: 0x00004B58
		internal string DesiredCatalogFolder
		{
			get
			{
				string text = this.DatabasePath;
				if (ExEnvironment.IsTest && !string.IsNullOrEmpty(text))
				{
					text += "_Catalog";
					if (Directory.Exists(this.DatabasePath) && !Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					return text;
				}
				return Path.GetFullPath(text);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000069B3 File Offset: 0x00004BB3
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x000069BB File Offset: 0x00004BBB
		internal bool IsSuspended { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000069C4 File Offset: 0x00004BC4
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x000069CC File Offset: 0x00004BCC
		internal bool ShouldAutomaticallySuspendCatalog { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000069D5 File Offset: 0x00004BD5
		// (set) Token: 0x060000BB RID: 187 RVA: 0x000069DD File Offset: 0x00004BDD
		internal bool IsLagCopy { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000069E6 File Offset: 0x00004BE6
		// (set) Token: 0x060000BD RID: 189 RVA: 0x000069EE File Offset: 0x00004BEE
		internal long NumberOfItems { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000069F7 File Offset: 0x00004BF7
		// (set) Token: 0x060000BF RID: 191 RVA: 0x000069FF File Offset: 0x00004BFF
		internal DateTime NumberOfItemsTimeStamp { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00006A08 File Offset: 0x00004C08
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00006A10 File Offset: 0x00004C10
		internal bool IsInstantSearchEnabled { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00006A19 File Offset: 0x00004C19
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00006A21 File Offset: 0x00004C21
		internal bool IsRefinersEnabled { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00006A2A File Offset: 0x00004C2A
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00006A32 File Offset: 0x00004C32
		internal bool IsCatalogSuspended { get; set; }

		// Token: 0x060000C6 RID: 198 RVA: 0x00006A3B File Offset: 0x00004C3B
		public override string ToString()
		{
			return this.cachedToStringValue;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00006A44 File Offset: 0x00004C44
		public override int GetHashCode()
		{
			return this.Guid.GetHashCode();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00006A68 File Offset: 0x00004C68
		public bool Equals(MdbInfo other)
		{
			return this.Guid.Equals(other.Guid);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006A89 File Offset: 0x00004C89
		public void ResetSystemMailboxGuidCache()
		{
			this.InitializeSystemMailboxAndSystemAttendantGuids(true);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00006A94 File Offset: 0x00004C94
		public void UpdateDatabaseLocationInfo()
		{
			if (!this.EnableOwningServerUpdate)
			{
				return;
			}
			LocalizedString message = Strings.FailedToGetActiveServer(this.Guid);
			try
			{
				ActiveManager noncachingActiveManagerInstance = ActiveManager.GetNoncachingActiveManagerInstance();
				DatabaseLocationInfo serverForDatabase = noncachingActiveManagerInstance.GetServerForDatabase(this.Guid, GetServerForDatabaseFlags.IgnoreAdSiteBoundary | GetServerForDatabaseFlags.BasicQuery);
				if (serverForDatabase.RequestResult != DatabaseLocationInfoResult.Success)
				{
					throw new ComponentFailedTransientException(message);
				}
				this.OwningServer = serverForDatabase.ServerFqdn;
				this.MountedOnLocalServer = (serverForDatabase.ServerGuid == LocalServer.GetServer().Guid);
			}
			catch (ServerForDatabaseNotFoundException innerException)
			{
				throw new ComponentFailedTransientException(message, innerException);
			}
			catch (StorageTransientException innerException2)
			{
				throw new ComponentFailedTransientException(message, innerException2);
			}
			catch (ADTransientException innerException3)
			{
				throw new ComponentFailedTransientException(message, innerException3);
			}
			catch (StoragePermanentException innerException4)
			{
				throw new ComponentFailedPermanentException(message, innerException4);
			}
			catch (ADExternalException innerException5)
			{
				throw new ComponentFailedPermanentException(message, innerException5);
			}
			catch (ADOperationException innerException6)
			{
				throw new ComponentFailedPermanentException(message, innerException6);
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00006CDC File Offset: 0x00004EDC
		private void InitializeSystemMailboxAndSystemAttendantGuids(bool forceInitialize)
		{
			if (!this.guidInitializationAttempted || forceInitialize)
			{
				this.guidInitializationAttempted = true;
				ADNotificationAdapter.RunADOperation(delegate()
				{
					string legacyExchangeDN = LocalServer.GetServer().ExchangeLegacyDN + "/cn=Microsoft System Attendant";
					IRootOrganizationRecipientSession rootOrganizationRecipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 509, "InitializeSystemMailboxAndSystemAttendantGuids", "f:\\15.00.1497\\sources\\dev\\Search\\src\\Mdb\\MdbInfo.cs");
					rootOrganizationRecipientSession.ServerTimeout = new TimeSpan?(MdbInfo.ADTimeout);
					try
					{
						ADRecipient adrecipient = rootOrganizationRecipientSession.FindByLegacyExchangeDN(legacyExchangeDN);
						ADSystemAttendantMailbox adsystemAttendantMailbox = adrecipient as ADSystemAttendantMailbox;
						if (adsystemAttendantMailbox != null && adsystemAttendantMailbox.Database != null && adsystemAttendantMailbox.Database.ObjectGuid == this.Guid)
						{
							this.systemAttendantGuid = new Guid?((adsystemAttendantMailbox.ExchangeGuid == Guid.Empty) ? adsystemAttendantMailbox.Guid : adsystemAttendantMailbox.ExchangeGuid);
						}
					}
					catch (DataValidationException)
					{
					}
					ADRecipient[] array = rootOrganizationRecipientSession.Find(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, "SystemMailbox{" + this.Guid + "}"), null, 2);
					if (array.Length != 1)
					{
						throw new ComponentFailedPermanentException(Strings.FailedToFindSystemMailbox(this.Guid));
					}
					this.systemMailbox = (array[0] as ADSystemMailbox);
					if (this.systemMailbox != null)
					{
						this.systemMailboxGuid = this.systemMailbox.ExchangeGuid;
						return;
					}
					throw new ComponentFailedPermanentException(Strings.FailedToFindSystemMailbox(this.Guid));
				});
			}
		}

		// Token: 0x04000069 RID: 105
		private const string SystemMailboxNamePrefix = "SystemMailbox";

		// Token: 0x0400006A RID: 106
		private const string SystemAttendantLegacyDNSuffix = "/cn=Microsoft System Attendant";

		// Token: 0x0400006B RID: 107
		private static readonly TimeSpan ADTimeout = TimeSpan.FromSeconds(30.0);

		// Token: 0x0400006C RID: 108
		private readonly Guid guid;

		// Token: 0x0400006D RID: 109
		private readonly string name;

		// Token: 0x0400006E RID: 110
		private readonly string cachedToStringValue;

		// Token: 0x0400006F RID: 111
		private Guid? systemAttendantGuid;

		// Token: 0x04000070 RID: 112
		private Guid systemMailboxGuid;

		// Token: 0x04000071 RID: 113
		private ADSystemMailbox systemMailbox;

		// Token: 0x04000072 RID: 114
		private bool guidInitializationAttempted;
	}
}
