using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Win32;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000042 RID: 66
	public abstract class SchemaUpgrader
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x000171C9 File Offset: 0x000153C9
		public static SchemaUpgrader Null(ComponentVersion from, ComponentVersion to)
		{
			return new SchemaUpgrader.NullUpgrader(from, to);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000171D2 File Offset: 0x000153D2
		public static SchemaUpgrader Null(short fromMajor, ushort fromMinor, short toMajor, ushort toMinor)
		{
			return SchemaUpgrader.Null(new ComponentVersion(fromMajor, fromMinor), new ComponentVersion(toMajor, toMinor));
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x000171E7 File Offset: 0x000153E7
		public bool IsNullUpgrader
		{
			get
			{
				return this is SchemaUpgrader.NullUpgrader;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x000171F2 File Offset: 0x000153F2
		public Hookable<ComponentVersion> FromHook
		{
			get
			{
				return this.from;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x000171FA File Offset: 0x000153FA
		public Hookable<ComponentVersion> ToHook
		{
			get
			{
				return this.to;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00017202 File Offset: 0x00015402
		public ComponentVersion From
		{
			get
			{
				return this.from.Value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0001720F File Offset: 0x0001540F
		public ComponentVersion To
		{
			get
			{
				return this.to.Value;
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0001721C File Offset: 0x0001541C
		internal long GetQuarantinedFileTime()
		{
			return RegistryReader.Instance.GetValue<long>(Registry.LocalMachine, this.quarantineSubKey, "QuarantinedTime", 0L);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0001723A File Offset: 0x0001543A
		internal void SetQuarantinedFileTime(long value)
		{
			RegistryWriter.Instance.CreateSubKey(Registry.LocalMachine, this.quarantineSubKey);
			RegistryWriter.Instance.SetValue(Registry.LocalMachine, this.quarantineSubKey, "QuarantinedTime", value, RegistryValueKind.QWord);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00017273 File Offset: 0x00015473
		internal int GetCrashCount()
		{
			return RegistryReader.Instance.GetValue<int>(Registry.LocalMachine, this.quarantineSubKey, "CrashCount", 0);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00017290 File Offset: 0x00015490
		internal void SetCrashCount(int value)
		{
			RegistryWriter.Instance.CreateSubKey(Registry.LocalMachine, this.quarantineSubKey);
			RegistryWriter.Instance.SetValue(Registry.LocalMachine, this.quarantineSubKey, "CrashCount", value, RegistryValueKind.DWord);
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x000172C8 File Offset: 0x000154C8
		public bool IsQuarantined
		{
			get
			{
				long quarantinedFileTime = this.GetQuarantinedFileTime();
				DateTime d = DateTime.FromFileTimeUtc(quarantinedFileTime);
				if (DateTime.UtcNow - d > TimeSpan.FromDays(1.0))
				{
					try
					{
						if (quarantinedFileTime != 0L)
						{
							RegistryWriter.Instance.DeleteSubKeyTree(Registry.LocalMachine, this.quarantineSubKey);
						}
					}
					catch (ArgumentException exception)
					{
						NullExecutionDiagnostics.Instance.OnExceptionCatch(exception);
					}
					return false;
				}
				return true;
			}
		}

		// Token: 0x060002D1 RID: 721
		public abstract void InitInMemoryDatabaseSchema(Context context, StoreDatabase database);

		// Token: 0x060002D2 RID: 722
		public abstract void PerformUpgrade(Context context, ISchemaVersion container);

		// Token: 0x060002D3 RID: 723 RVA: 0x00017340 File Offset: 0x00015540
		public virtual void TransactionAborted(Context context, ISchemaVersion container)
		{
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00017344 File Offset: 0x00015544
		public SchemaUpgrader(ComponentVersion from, ComponentVersion to)
		{
			if (from.Major == to.Major)
			{
				if (to.Minor != from.Minor + 1)
				{
					throw new ArgumentException("target Minor version of the upgrade must be one greater than the source of the upgrade");
				}
			}
			else
			{
				if (to.Major != from.Major + 1)
				{
					throw new ArgumentException("Major schema upgrades must incremental.");
				}
				if (to.Minor != 0)
				{
					throw new ArgumentException("Major schema upgrades must reset the Minor version to zero.");
				}
			}
			this.from = Hookable<ComponentVersion>.Create(true, from);
			this.to = Hookable<ComponentVersion>.Create(true, to);
			this.quarantineSubKey = "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem\\SchemaUpgraderQuarantines\\" + base.GetType().ToString();
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000173EC File Offset: 0x000155EC
		protected bool TestVersionIsReady(Context context, ISchemaVersion container)
		{
			StoreDatabase storeDatabase = container as StoreDatabase;
			if (storeDatabase == null)
			{
				storeDatabase = context.Database;
			}
			return storeDatabase.PhysicalDatabase.DatabaseType != DatabaseType.Jet || container.GetCurrentSchemaVersion(context).Value >= this.To.Value;
		}

		// Token: 0x04000274 RID: 628
		private const string QuarantinedTimeKey = "QuarantinedTime";

		// Token: 0x04000275 RID: 629
		private const string CrashCountKey = "CrashCount";

		// Token: 0x04000276 RID: 630
		private readonly string quarantineSubKey;

		// Token: 0x04000277 RID: 631
		private Hookable<ComponentVersion> from;

		// Token: 0x04000278 RID: 632
		private Hookable<ComponentVersion> to;

		// Token: 0x02000043 RID: 67
		private sealed class NullUpgrader : SchemaUpgrader
		{
			// Token: 0x060002D6 RID: 726 RVA: 0x0001743B File Offset: 0x0001563B
			public NullUpgrader(ComponentVersion from, ComponentVersion to) : base(from, to)
			{
			}

			// Token: 0x060002D7 RID: 727 RVA: 0x00017445 File Offset: 0x00015645
			public override void InitInMemoryDatabaseSchema(Context context, StoreDatabase database)
			{
			}

			// Token: 0x060002D8 RID: 728 RVA: 0x00017447 File Offset: 0x00015647
			public override void PerformUpgrade(Context context, ISchemaVersion container)
			{
			}
		}
	}
}
