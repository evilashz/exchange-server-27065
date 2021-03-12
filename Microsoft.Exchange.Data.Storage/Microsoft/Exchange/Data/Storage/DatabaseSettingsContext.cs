using System;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200031A RID: 794
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DatabaseSettingsContext : SimpleDatabaseSettingsContext, IConstraintProvider
	{
		// Token: 0x060023A1 RID: 9121 RVA: 0x00091368 File Offset: 0x0008F568
		public DatabaseSettingsContext(Guid mdbGuid, SettingsContextBase nextContext = null) : base(mdbGuid, nextContext)
		{
			DatabaseLocationInfo databaseLocationInfo = null;
			try
			{
				databaseLocationInfo = ActiveManager.GetCachingActiveManagerInstance().GetServerForDatabase(mdbGuid, GetServerForDatabaseFlags.IgnoreAdSiteBoundary | GetServerForDatabaseFlags.BasicQuery);
			}
			catch (ObjectNotFoundException)
			{
			}
			if (databaseLocationInfo != null)
			{
				this.databaseName = databaseLocationInfo.DatabaseName;
				this.databaseVersion = databaseLocationInfo.AdminDisplayVersion;
				this.serverName = databaseLocationInfo.ServerFqdn;
				this.serverVersion = new ServerVersion(databaseLocationInfo.ServerVersion);
				this.serverGuid = new Guid?(databaseLocationInfo.ServerGuid);
				return;
			}
			this.databaseName = string.Empty;
			this.databaseVersion = new ServerVersion(0);
			this.serverName = string.Empty;
			this.serverVersion = new ServerVersion(0);
			this.serverGuid = new Guid?(Guid.Empty);
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x0009142C File Offset: 0x0008F62C
		public static IConstraintProvider Get(Guid mdbGuid)
		{
			if (DatabaseSettingsContext.TestProvider != null)
			{
				return DatabaseSettingsContext.TestProvider;
			}
			return new DatabaseSettingsContext(mdbGuid, null);
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x060023A3 RID: 9123 RVA: 0x00091442 File Offset: 0x0008F642
		public override string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x060023A4 RID: 9124 RVA: 0x0009144A File Offset: 0x0008F64A
		public override ServerVersion DatabaseVersion
		{
			get
			{
				return this.databaseVersion;
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x060023A5 RID: 9125 RVA: 0x00091452 File Offset: 0x0008F652
		public override string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x060023A6 RID: 9126 RVA: 0x0009145A File Offset: 0x0008F65A
		public override ServerVersion ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x060023A7 RID: 9127 RVA: 0x00091462 File Offset: 0x0008F662
		public override Guid? ServerGuid
		{
			get
			{
				return this.serverGuid;
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x060023A8 RID: 9128 RVA: 0x0009146A File Offset: 0x0008F66A
		// (set) Token: 0x060023A9 RID: 9129 RVA: 0x00091471 File Offset: 0x0008F671
		internal static IConstraintProvider TestProvider { get; set; }

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x060023AA RID: 9130 RVA: 0x0009147C File Offset: 0x0008F67C
		ConstraintCollection IConstraintProvider.Constraints
		{
			get
			{
				ConstraintCollection constraintCollection = ConstraintCollection.CreateGlobal();
				if (!string.IsNullOrEmpty(this.DatabaseName))
				{
					constraintCollection.Add(VariantType.MdbName, this.DatabaseName);
				}
				if (this.DatabaseGuid != null)
				{
					constraintCollection.Add(VariantType.MdbGuid, this.DatabaseGuid.Value);
				}
				if (this.DatabaseVersion != null)
				{
					constraintCollection.Add(VariantType.MdbVersion, this.DatabaseVersion);
				}
				return constraintCollection;
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x060023AB RID: 9131 RVA: 0x000914FC File Offset: 0x0008F6FC
		string IConstraintProvider.RampId
		{
			get
			{
				return ((this.DatabaseGuid != null) ? this.DatabaseGuid.Value : Guid.Empty).ToString();
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x0009153C File Offset: 0x0008F73C
		string IConstraintProvider.RotationId
		{
			get
			{
				return ((this.DatabaseGuid != null) ? this.DatabaseGuid.Value : Guid.Empty).ToString();
			}
		}

		// Token: 0x04001522 RID: 5410
		private readonly string databaseName;

		// Token: 0x04001523 RID: 5411
		private readonly ServerVersion databaseVersion;

		// Token: 0x04001524 RID: 5412
		private readonly string serverName;

		// Token: 0x04001525 RID: 5413
		private readonly Guid? serverGuid;

		// Token: 0x04001526 RID: 5414
		private readonly ServerVersion serverVersion;
	}
}
