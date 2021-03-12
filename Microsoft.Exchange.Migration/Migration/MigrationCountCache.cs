using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000055 RID: 85
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MigrationCountCache : MigrationPersistableDictionary
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000F198 File Offset: 0x0000D398
		internal bool IsEmpty
		{
			get
			{
				return base.PropertyBag.Count == 0;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000F1A8 File Offset: 0x0000D3A8
		internal bool IsValid
		{
			get
			{
				foreach (object obj in ((IDictionary)base.PropertyBag).Keys)
				{
					string text = obj as string;
					if (string.IsNullOrEmpty(text))
					{
						return false;
					}
					object obj2 = base.Get<object>(text);
					if (obj2 is int && (int)obj2 < 0)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000F234 File Offset: 0x0000D434
		public static MigrationCountCache Deserialize(string serializedData)
		{
			MigrationCountCache migrationCountCache = new MigrationCountCache();
			if (!string.IsNullOrEmpty(serializedData))
			{
				migrationCountCache.DeserializeData(serializedData);
			}
			return migrationCountCache;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000F258 File Offset: 0x0000D458
		public MigrationCountCache Clone()
		{
			MigrationCountCache migrationCountCache = new MigrationCountCache();
			foreach (object obj in ((IDictionary)base.PropertyBag).Keys)
			{
				string text = obj as string;
				if (!string.IsNullOrEmpty(text))
				{
					migrationCountCache.Set<object>(text, base.Get<object>(text));
				}
			}
			return migrationCountCache;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000F305 File Offset: 0x0000D505
		public int GetCachedStatusCount(params MigrationUserStatus[] statuses)
		{
			if (statuses == null)
			{
				return 0;
			}
			return statuses.Sum(delegate(MigrationUserStatus status)
			{
				int? nullable = base.GetNullable<int>(MigrationCountCache.MapFromStatusToKey[status]);
				if (nullable == null)
				{
					return 0;
				}
				return nullable.GetValueOrDefault();
			});
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000F31E File Offset: 0x0000D51E
		public void SetCachedStatusCount(MigrationUserStatus status, int value)
		{
			base.Set<int>(MigrationCountCache.MapFromStatusToKey[status], value);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000F334 File Offset: 0x0000D534
		public int GetCachedOtherCount(string key)
		{
			int? nullable = base.GetNullable<int>(key);
			if (nullable == null)
			{
				return 0;
			}
			return nullable.GetValueOrDefault();
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000F35B File Offset: 0x0000D55B
		public void SetCachedOtherCount(string key, int value)
		{
			base.Set<int>(key, value);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000F368 File Offset: 0x0000D568
		public void IncrementCachedOtherCount(string key, int amount)
		{
			int num = base.GetNullable<int>(key) ?? 0;
			num += amount;
			base.Set<int>(key, num);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000F39D File Offset: 0x0000D59D
		public ExDateTime? GetCachedTimestamp(string key)
		{
			return base.GetNullable<ExDateTime>(key);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000F3A6 File Offset: 0x0000D5A6
		public void SetCachedTimestamp(string key, ExDateTime? value)
		{
			base.SetNullable<ExDateTime>(key, value);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000F3B0 File Offset: 0x0000D5B0
		public XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument)
		{
			XElement xelement = new XElement("MigrationCountCache");
			foreach (object obj in ((IDictionary)base.PropertyBag).Keys)
			{
				string text = obj as string;
				if (!string.IsNullOrEmpty(text))
				{
					xelement.Add(new XElement(text, base.Get<object>(text)));
				}
			}
			return xelement;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000F440 File Offset: 0x0000D640
		public void ApplyStatusChange(MigrationCountCache.MigrationStatusChange change)
		{
			foreach (string key in change.Keys)
			{
				this.IncrementCachedOtherCount(key, change[key]);
			}
		}

		// Token: 0x0400012B RID: 299
		public const string LastSync = "LastSync";

		// Token: 0x0400012C RID: 300
		public const string RemovedItems = "Removed";

		// Token: 0x0400012D RID: 301
		public const string ProvisionedItems = "Provisioned";

		// Token: 0x0400012E RID: 302
		private const string StatusQueued = "StatusQueued";

		// Token: 0x0400012F RID: 303
		private const string StatusSyncing = "StatusSyncing";

		// Token: 0x04000130 RID: 304
		private const string StatusFailed = "StatusFailed";

		// Token: 0x04000131 RID: 305
		private const string StatusSynced = "StatusSynced";

		// Token: 0x04000132 RID: 306
		private const string StatusIncrementalFailed = "StatusIncrementalFailed";

		// Token: 0x04000133 RID: 307
		private const string StatusCompleting = "StatusCompleting";

		// Token: 0x04000134 RID: 308
		private const string StatusCompleted = "StatusCompleted";

		// Token: 0x04000135 RID: 309
		private const string StatusCompletionFailed = "StatusCompletionFailed";

		// Token: 0x04000136 RID: 310
		private const string StatusCompletedWithWarnings = "StatusCompletedWithWarnings";

		// Token: 0x04000137 RID: 311
		private const string StatusCorrupted = "StatusCorrupted";

		// Token: 0x04000138 RID: 312
		private const string StatusProvisioning = "StatusProvisioning";

		// Token: 0x04000139 RID: 313
		private const string StatusProvisionUpdating = "StatusProvisionUpdating";

		// Token: 0x0400013A RID: 314
		private const string StatusCompletionSynced = "StatusCompletionSynced";

		// Token: 0x0400013B RID: 315
		private const string StatusValidating = "StatusValidating";

		// Token: 0x0400013C RID: 316
		private const string StatusIncrementalSyncing = "StatusIncrementalSyncing";

		// Token: 0x0400013D RID: 317
		private const string StatusIncrementalSynced = "StatusIncrementalSynced";

		// Token: 0x0400013E RID: 318
		private const string StatusStopped = "StatusStopped";

		// Token: 0x0400013F RID: 319
		private const string StatusIncrementalStopped = "StatusIncrementalStopped";

		// Token: 0x04000140 RID: 320
		private const string StatusStarting = "StatusStarting";

		// Token: 0x04000141 RID: 321
		private const string StatusStopping = "StatusStopping";

		// Token: 0x04000142 RID: 322
		private const string StatusRemoving = "StatusRemoving";

		// Token: 0x04000143 RID: 323
		private static readonly Dictionary<MigrationUserStatus, string> MapFromStatusToKey = new Dictionary<MigrationUserStatus, string>
		{
			{
				MigrationUserStatus.Queued,
				"StatusQueued"
			},
			{
				MigrationUserStatus.Syncing,
				"StatusSyncing"
			},
			{
				MigrationUserStatus.Failed,
				"StatusFailed"
			},
			{
				MigrationUserStatus.Synced,
				"StatusSynced"
			},
			{
				MigrationUserStatus.IncrementalFailed,
				"StatusIncrementalFailed"
			},
			{
				MigrationUserStatus.Completing,
				"StatusCompleting"
			},
			{
				MigrationUserStatus.Completed,
				"StatusCompleted"
			},
			{
				MigrationUserStatus.CompletionFailed,
				"StatusCompletionFailed"
			},
			{
				MigrationUserStatus.CompletedWithWarnings,
				"StatusCompletedWithWarnings"
			},
			{
				MigrationUserStatus.Corrupted,
				"StatusCorrupted"
			},
			{
				MigrationUserStatus.Provisioning,
				"StatusProvisioning"
			},
			{
				MigrationUserStatus.ProvisionUpdating,
				"StatusProvisionUpdating"
			},
			{
				MigrationUserStatus.CompletionSynced,
				"StatusCompletionSynced"
			},
			{
				MigrationUserStatus.Validating,
				"StatusValidating"
			},
			{
				MigrationUserStatus.IncrementalSyncing,
				"StatusIncrementalSyncing"
			},
			{
				MigrationUserStatus.IncrementalSynced,
				"StatusIncrementalSynced"
			},
			{
				MigrationUserStatus.Stopped,
				"StatusStopped"
			},
			{
				MigrationUserStatus.IncrementalStopped,
				"StatusIncrementalStopped"
			},
			{
				MigrationUserStatus.Starting,
				"StatusStarting"
			},
			{
				MigrationUserStatus.Stopping,
				"StatusStopping"
			},
			{
				MigrationUserStatus.Removing,
				"StatusRemoving"
			}
		};

		// Token: 0x02000056 RID: 86
		internal class MigrationStatusChange : Dictionary<string, int>
		{
			// Token: 0x06000420 RID: 1056 RVA: 0x0000F5C5 File Offset: 0x0000D7C5
			private MigrationStatusChange()
			{
			}

			// Token: 0x06000421 RID: 1057 RVA: 0x0000F5D0 File Offset: 0x0000D7D0
			public static MigrationCountCache.MigrationStatusChange CreateStatusChange(MigrationUserStatus oldStatus, MigrationUserStatus newStatus)
			{
				if (oldStatus == newStatus)
				{
					return null;
				}
				return new MigrationCountCache.MigrationStatusChange
				{
					{
						MigrationCountCache.MapFromStatusToKey[oldStatus],
						-1
					},
					{
						MigrationCountCache.MapFromStatusToKey[newStatus],
						1
					}
				};
			}

			// Token: 0x06000422 RID: 1058 RVA: 0x0000F610 File Offset: 0x0000D810
			public static MigrationCountCache.MigrationStatusChange CreateInject(MigrationUserStatus status)
			{
				return new MigrationCountCache.MigrationStatusChange
				{
					{
						MigrationCountCache.MapFromStatusToKey[status],
						1
					}
				};
			}

			// Token: 0x06000423 RID: 1059 RVA: 0x0000F638 File Offset: 0x0000D838
			public static MigrationCountCache.MigrationStatusChange CreateRemoval(MigrationUserStatus status)
			{
				return new MigrationCountCache.MigrationStatusChange
				{
					{
						MigrationCountCache.MapFromStatusToKey[status],
						-1
					}
				};
			}

			// Token: 0x06000424 RID: 1060 RVA: 0x0000F660 File Offset: 0x0000D860
			public static MigrationCountCache.MigrationStatusChange operator +(MigrationCountCache.MigrationStatusChange statusChange1, MigrationCountCache.MigrationStatusChange statusChange2)
			{
				MigrationCountCache.MigrationStatusChange migrationStatusChange = new MigrationCountCache.MigrationStatusChange();
				foreach (string key in statusChange1.Keys.Union(statusChange2.Keys))
				{
					int num = 0;
					if (statusChange1.ContainsKey(key))
					{
						num += statusChange1[key];
					}
					if (statusChange2.ContainsKey(key))
					{
						num += statusChange2[key];
					}
					migrationStatusChange[key] = num;
				}
				return migrationStatusChange;
			}

			// Token: 0x04000144 RID: 324
			public static MigrationCountCache.MigrationStatusChange None = new MigrationCountCache.MigrationStatusChange();
		}
	}
}
