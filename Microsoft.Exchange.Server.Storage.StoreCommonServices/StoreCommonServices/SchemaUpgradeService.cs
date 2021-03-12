﻿using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200012B RID: 299
	public static class SchemaUpgradeService
	{
		// Token: 0x06000B88 RID: 2952 RVA: 0x0003A4CC File Offset: 0x000386CC
		internal static IDisposable HookUpgraders(Dictionary<SchemaUpgradeService.SchemaCategory, List<SchemaUpgrader>> value)
		{
			return SchemaUpgradeService.schemaUpgradersTestHook.SetTestHook(value);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0003A4D9 File Offset: 0x000386D9
		internal static IDisposable SetFaultHook(Action action)
		{
			return SchemaUpgradeService.upgradeFaultHook.SetTestHook(action);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0003A4E8 File Offset: 0x000386E8
		internal static IDisposable ConfigureForUpgraderTest(SchemaUpgradeService.SchemaCategory schemaCategory, SchemaUpgrader testSchemaUpgrader)
		{
			switch (schemaCategory)
			{
			case SchemaUpgradeService.SchemaCategory.Database:
				return SchemaUpgradeService.ConfigureForUpgraderTest(testSchemaUpgrader, SchemaUpgrader.Null(0, 0, 0, 1));
			case SchemaUpgradeService.SchemaCategory.Mailbox:
				return SchemaUpgradeService.ConfigureForUpgraderTest(SchemaUpgrader.Null(0, 0, 0, 1), testSchemaUpgrader);
			default:
				throw new ArgumentException("schemaCategory");
			}
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0003A534 File Offset: 0x00038734
		internal static IDisposable ConfigureForUpgraderTest(SchemaUpgrader databaseSchemaUpgrader, SchemaUpgrader mailboxSchemaUpgrader)
		{
			DisposeGuard disposeGuard = default(DisposeGuard);
			if (!databaseSchemaUpgrader.IsNullUpgrader)
			{
				using (List<SchemaUpgrader>.Enumerator enumerator = SchemaUpgradeService.schemaUpgradersTestHook.Value[SchemaUpgradeService.SchemaCategory.Database].GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SchemaUpgrader schemaUpgrader = enumerator.Current;
						if (schemaUpgrader.GetType() == databaseSchemaUpgrader.GetType())
						{
							return null;
						}
					}
					goto IL_C2;
				}
			}
			if (!mailboxSchemaUpgrader.IsNullUpgrader)
			{
				foreach (SchemaUpgrader schemaUpgrader2 in SchemaUpgradeService.schemaUpgradersTestHook.Value[SchemaUpgradeService.SchemaCategory.Mailbox])
				{
					if (schemaUpgrader2.GetType() == mailboxSchemaUpgrader.GetType())
					{
						return null;
					}
				}
			}
			IL_C2:
			List<SchemaUpgrader> list = new List<SchemaUpgrader>(SchemaUpgradeService.schemaUpgradersTestHook.Value[SchemaUpgradeService.SchemaCategory.Database]);
			List<SchemaUpgrader> list2 = new List<SchemaUpgrader>(SchemaUpgradeService.schemaUpgradersTestHook.Value[SchemaUpgradeService.SchemaCategory.Mailbox]);
			Dictionary<SchemaUpgradeService.SchemaCategory, List<SchemaUpgrader>> dictionary = new Dictionary<SchemaUpgradeService.SchemaCategory, List<SchemaUpgrader>>();
			ComponentVersion to = list[list.Count - 1].To;
			ComponentVersion testHook = new ComponentVersion(to.Major, to.Minor + 1);
			disposeGuard.Add<IDisposable>(databaseSchemaUpgrader.FromHook.SetTestHook(to));
			disposeGuard.Add<IDisposable>(databaseSchemaUpgrader.ToHook.SetTestHook(testHook));
			list.Add(databaseSchemaUpgrader);
			disposeGuard.Add<IDisposable>(mailboxSchemaUpgrader.FromHook.SetTestHook(to));
			disposeGuard.Add<IDisposable>(mailboxSchemaUpgrader.ToHook.SetTestHook(testHook));
			list2.Add(mailboxSchemaUpgrader);
			dictionary[SchemaUpgradeService.SchemaCategory.Database] = list;
			dictionary[SchemaUpgradeService.SchemaCategory.Mailbox] = list2;
			disposeGuard.Add<IDisposable>(SchemaUpgradeService.HookUpgraders(dictionary));
			return disposeGuard;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0003A710 File Offset: 0x00038910
		public static void ClearRegistrations()
		{
			SchemaUpgradeService.schemaUpgradersTestHook.Value.Clear();
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0003A748 File Offset: 0x00038948
		public static void Register(SchemaUpgradeService.SchemaCategory category, IEnumerable<SchemaUpgrader> upgraders)
		{
			List<SchemaUpgrader> list = new List<SchemaUpgrader>(upgraders);
			if (SchemaUpgradeService.schemaUpgradersTestHook.Value.ContainsKey(category))
			{
				throw new ArgumentException("Re-registering the same upgrader category");
			}
			list.Sort((SchemaUpgrader a, SchemaUpgrader b) => a.From.CompareTo(b.From));
			for (int i = 1; i < list.Count; i++)
			{
				SchemaUpgrader schemaUpgrader = list[i - 1];
				SchemaUpgrader schemaUpgrader2 = list[i];
				if (schemaUpgrader.To.Value != schemaUpgrader2.From.Value)
				{
					throw new ArgumentException("Upgraders must be sequential");
				}
			}
			foreach (List<SchemaUpgrader> list2 in SchemaUpgradeService.schemaUpgradersTestHook.Value.Values)
			{
				if (list2.Count != list.Count)
				{
					throw new ArgumentException("mismatched count of upgraders amongst the various upgrader types. Make sure each set of upgraders accounts for the same set of version upgrades");
				}
				if (list2[0].From.Value != list[0].From.Value)
				{
					throw new ArgumentException("mismatched starting version 'from' for upgraders. Make sure each set of upgraders accounts for the same set of version upgrades");
				}
				for (int j = 0; j < list.Count; j++)
				{
					if (list2[j].To.Value != list[j].To.Value)
					{
						throw new ArgumentException("mismatched intermediate version 'to' for upgraders. Make sure each set of upgraders accounts for the same set of version upgrades");
					}
				}
			}
			SchemaUpgradeService.schemaUpgradersTestHook.Value[category] = list;
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0003A8EC File Offset: 0x00038AEC
		public static void InitInMemoryDatabaseSchema(Context context, StoreDatabase database)
		{
			foreach (List<SchemaUpgrader> list in SchemaUpgradeService.schemaUpgradersTestHook.Value.Values)
			{
				foreach (SchemaUpgrader schemaUpgrader in list)
				{
					schemaUpgrader.InitInMemoryDatabaseSchema(context, database);
				}
			}
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0003AEC4 File Offset: 0x000390C4
		public static ComponentVersion Upgrade(Context context, ISchemaVersion container, SchemaUpgradeService.SchemaCategory category, ComponentVersion requestedVersion)
		{
			SchemaUpgradeService.<>c__DisplayClass4 CS$<>8__locals1 = new SchemaUpgradeService.<>c__DisplayClass4();
			CS$<>8__locals1.context = context;
			CS$<>8__locals1.container = container;
			CS$<>8__locals1.category = category;
			CS$<>8__locals1.upgraders = SchemaUpgradeService.schemaUpgradersTestHook.Value[CS$<>8__locals1.category];
			CS$<>8__locals1.currentVersion = CS$<>8__locals1.container.GetCurrentSchemaVersion(CS$<>8__locals1.context);
			ComponentVersion currentVersion = CS$<>8__locals1.currentVersion;
			CS$<>8__locals1.didUpgrade = false;
			if (CS$<>8__locals1.context.Database.PhysicalDatabase.DatabaseType != DatabaseType.Jet)
			{
				return CS$<>8__locals1.currentVersion;
			}
			if (ConfigurationSchema.DisableSchemaUpgraders.Value)
			{
				if (ExTraceGlobals.SchemaUpgradeServiceTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.SchemaUpgradeServiceTracer.TraceDebug<SchemaUpgradeService.SchemaCategory, string>(46364L, "Skipping all {0} upgraders for {1} because upgraders have been disabled", CS$<>8__locals1.category, CS$<>8__locals1.container.Identifier);
				}
				return CS$<>8__locals1.currentVersion;
			}
			ComponentVersion value = ConfigurationSchema.MaximumRequestableSchemaVersion.Value;
			if (requestedVersion.CompareTo(value) > 0)
			{
				if (ExTraceGlobals.SchemaUpgradeServiceTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.SchemaUpgradeServiceTracer.TraceDebug(50460L, "Requested upgrade of {0} {1} to version {2} will be capped at version {3} instead", new object[]
					{
						CS$<>8__locals1.category,
						CS$<>8__locals1.container.Identifier,
						requestedVersion,
						value
					});
				}
				requestedVersion = value;
			}
			CS$<>8__locals1.i = 0;
			while (CS$<>8__locals1.i < CS$<>8__locals1.upgraders.Count && CS$<>8__locals1.upgraders[CS$<>8__locals1.i].From.Value < CS$<>8__locals1.currentVersion.Value)
			{
				CS$<>8__locals1.i++;
			}
			if (CS$<>8__locals1.i < CS$<>8__locals1.upgraders.Count && CS$<>8__locals1.currentVersion.Value != CS$<>8__locals1.upgraders[CS$<>8__locals1.i].From.Value)
			{
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_SchemaNoUpgradeFromCurrentVersionFailure, new object[]
				{
					CS$<>8__locals1.category,
					CS$<>8__locals1.container.Identifier,
					CS$<>8__locals1.currentVersion.ToString(),
					CS$<>8__locals1.upgraders[CS$<>8__locals1.i].From.ToString()
				});
				return CS$<>8__locals1.currentVersion;
			}
			CS$<>8__locals1.startTransactionStamp = -1L;
			CS$<>8__locals1.endTransactionStamp = -1L;
			CS$<>8__locals1.commencingTuple = ((SchemaUpgradeService.SchemaCategory.Mailbox == CS$<>8__locals1.category) ? SchemaUpgradeService.mailboxCommencingTuple : SchemaUpgradeService.generalCommencingTuple);
			ExEventLog.EventTuple tuple = (SchemaUpgradeService.SchemaCategory.Mailbox == CS$<>8__locals1.category) ? SchemaUpgradeService.mailboxCompleteTuple : SchemaUpgradeService.generalCompleteTuple;
			ExEventLog.EventTuple tuple2 = (SchemaUpgradeService.SchemaCategory.Mailbox == CS$<>8__locals1.category) ? SchemaUpgradeService.mailboxQuarantineTuple : SchemaUpgradeService.generalQuarantineTuple;
			CS$<>8__locals1.shouldContinue = true;
			while (CS$<>8__locals1.i < CS$<>8__locals1.upgraders.Count && CS$<>8__locals1.currentVersion.Value < requestedVersion.Value && CS$<>8__locals1.shouldContinue)
			{
				if (CS$<>8__locals1.upgraders[CS$<>8__locals1.i].IsQuarantined)
				{
					Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(tuple2, new object[]
					{
						CS$<>8__locals1.category,
						CS$<>8__locals1.container.Identifier,
						CS$<>8__locals1.upgraders[CS$<>8__locals1.i].From.ToString(),
						CS$<>8__locals1.upgraders[CS$<>8__locals1.i].To.ToString()
					});
					break;
				}
				ILUtil.DoTryFilterCatch<SchemaUpgrader>(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<Upgrade>b__2)), new GenericFilterDelegate<SchemaUpgrader>(null, (UIntPtr)ldftn(ExceptionFilter)), null, CS$<>8__locals1.upgraders[CS$<>8__locals1.i]);
				CS$<>8__locals1.i++;
			}
			if (CS$<>8__locals1.didUpgrade)
			{
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(tuple, new object[]
				{
					CS$<>8__locals1.category,
					CS$<>8__locals1.container.Identifier,
					currentVersion.ToString(),
					CS$<>8__locals1.currentVersion.ToString()
				});
			}
			return CS$<>8__locals1.currentVersion;
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0003B334 File Offset: 0x00039534
		private static bool ExceptionFilter(object exception, SchemaUpgrader upgrader)
		{
			int num = upgrader.GetCrashCount() + 1;
			upgrader.SetCrashCount(num);
			if (num > 2)
			{
				upgrader.SetQuarantinedFileTime(DateTime.UtcNow.ToFileTimeUtc());
			}
			return false;
		}

		// Token: 0x04000668 RID: 1640
		private static Hookable<Dictionary<SchemaUpgradeService.SchemaCategory, List<SchemaUpgrader>>> schemaUpgradersTestHook = Hookable<Dictionary<SchemaUpgradeService.SchemaCategory, List<SchemaUpgrader>>>.Create(true, new Dictionary<SchemaUpgradeService.SchemaCategory, List<SchemaUpgrader>>());

		// Token: 0x04000669 RID: 1641
		private static Hookable<Action> upgradeFaultHook = Hookable<Action>.Create(true, null);

		// Token: 0x0400066A RID: 1642
		private static DeterministicTime deterministicTime = new DeterministicTime();

		// Token: 0x0400066B RID: 1643
		private static readonly ExEventLog.EventTuple generalCommencingTuple = MSExchangeISEventLogConstants.Tuple_SchemaUpgradeCommencing;

		// Token: 0x0400066C RID: 1644
		private static readonly ExEventLog.EventTuple generalCompleteTuple = MSExchangeISEventLogConstants.Tuple_SchemaUpgradeComplete;

		// Token: 0x0400066D RID: 1645
		private static readonly ExEventLog.EventTuple generalQuarantineTuple = MSExchangeISEventLogConstants.Tuple_SchemaUpgradeQuarantined;

		// Token: 0x0400066E RID: 1646
		private static readonly ExEventLog.EventTuple mailboxCommencingTuple = new ExEventLog.EventTuple(SchemaUpgradeService.generalCommencingTuple.EventId, SchemaUpgradeService.generalCommencingTuple.CategoryId, SchemaUpgradeService.generalCommencingTuple.EntryType, SchemaUpgradeService.generalCommencingTuple.Level + 1, SchemaUpgradeService.generalCommencingTuple.Period);

		// Token: 0x0400066F RID: 1647
		private static readonly ExEventLog.EventTuple mailboxCompleteTuple = new ExEventLog.EventTuple(SchemaUpgradeService.generalCompleteTuple.EventId, SchemaUpgradeService.generalCompleteTuple.CategoryId, SchemaUpgradeService.generalCompleteTuple.EntryType, SchemaUpgradeService.generalCompleteTuple.Level + 1, SchemaUpgradeService.generalCompleteTuple.Period);

		// Token: 0x04000670 RID: 1648
		private static readonly ExEventLog.EventTuple mailboxQuarantineTuple = new ExEventLog.EventTuple(SchemaUpgradeService.generalQuarantineTuple.EventId, SchemaUpgradeService.generalQuarantineTuple.CategoryId, SchemaUpgradeService.generalQuarantineTuple.EntryType, SchemaUpgradeService.generalQuarantineTuple.Level + 1, SchemaUpgradeService.generalQuarantineTuple.Period);

		// Token: 0x0200012C RID: 300
		public enum SchemaCategory
		{
			// Token: 0x04000673 RID: 1651
			Database,
			// Token: 0x04000674 RID: 1652
			Mailbox,
			// Token: 0x04000675 RID: 1653
			Test
		}
	}
}
