using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004E1 RID: 1249
	[Serializable]
	public class MiniDatabase : MiniObject, IUsnChanged
	{
		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x060037ED RID: 14317 RVA: 0x000D9620 File Offset: 0x000D7820
		internal override ADObjectSchema Schema
		{
			get
			{
				return MiniDatabase.schema;
			}
		}

		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x060037EE RID: 14318 RVA: 0x000D9627 File Offset: 0x000D7827
		internal override string MostDerivedObjectClass
		{
			get
			{
				return MiniDatabase.mostDerivedClass;
			}
		}

		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x060037EF RID: 14319 RVA: 0x000D9630 File Offset: 0x000D7830
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, MailboxDatabase.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, PublicFolderDatabase.MostDerivedClass)
				});
			}
		}

		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x060037F0 RID: 14320 RVA: 0x000D9670 File Offset: 0x000D7870
		public ADObjectId Server
		{
			get
			{
				return (ADObjectId)this[MiniDatabaseSchema.Server];
			}
		}

		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x060037F1 RID: 14321 RVA: 0x000D9682 File Offset: 0x000D7882
		public string ServerName
		{
			get
			{
				return (string)this[MiniDatabaseSchema.ServerName];
			}
		}

		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x060037F2 RID: 14322 RVA: 0x000D9694 File Offset: 0x000D7894
		public ADObjectId MasterServerOrAvailabilityGroup
		{
			get
			{
				return (ADObjectId)this[MiniDatabaseSchema.MasterServerOrAvailabilityGroup];
			}
		}

		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x060037F3 RID: 14323 RVA: 0x000D96A6 File Offset: 0x000D78A6
		public long UsnChanged
		{
			get
			{
				return (long)this[MiniDatabaseSchema.UsnChanged];
			}
		}

		// Token: 0x040025C7 RID: 9671
		private static MiniDatabaseSchema schema = new MiniDatabaseSchema();

		// Token: 0x040025C8 RID: 9672
		private static string mostDerivedClass = "msExchMDB";
	}
}
