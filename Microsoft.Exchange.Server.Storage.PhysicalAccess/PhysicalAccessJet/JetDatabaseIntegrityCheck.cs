using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000A8 RID: 168
	internal class JetDatabaseIntegrityCheck : DisposableBase
	{
		// Token: 0x06000740 RID: 1856 RVA: 0x000228F0 File Offset: 0x00020AF0
		public JetDatabaseIntegrityCheck(JetDatabase database, TextWriter output)
		{
			this.output = output;
			this.database = database;
			Api.JetBeginSession(this.database.JetInstance, out this.sesid, null, null);
			Api.JetAttachDatabase(this.sesid, this.database.DatabaseFile, AttachDatabaseGrbit.None);
			Api.JetOpenDatabase(this.sesid, this.database.DatabaseFile, null, out this.dbid, OpenDatabaseGrbit.None);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00022960 File Offset: 0x00020B60
		public bool DoIntegrityCheck(Microsoft.Exchange.Server.Storage.PhysicalAccess.Table table)
		{
			this.failureCount = 0;
			if (table.IsPartitioned)
			{
				this.CheckTemplateTable(table);
				if (this.failureCount != 0)
				{
					return false;
				}
				string prefix = table.Name + "_";
				using (IEnumerator<string> enumerator = this.GetTableNamesStartingWith(prefix).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						this.Check(this.TableIsDerivedTable(text), "Table {0} should be derived from {1}", new object[]
						{
							text,
							table.Name
						});
					}
					goto IL_B0;
				}
			}
			this.Check(!this.TableIsTemplateTable(table.Name), "Table {0} isn't partitioned so it should not be a template table", new object[]
			{
				table.Name
			});
			IL_B0:
			return 0 == this.failureCount;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00022A38 File Offset: 0x00020C38
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<JetDatabaseIntegrityCheck>(this);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00022A40 File Offset: 0x00020C40
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && !this.sesid.Equals(default(JET_SESID)))
			{
				Api.JetEndSession(this.sesid, EndSessionGrbit.None);
			}
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00022A75 File Offset: 0x00020C75
		private void Check(bool condition, string message, params object[] args)
		{
			if (!condition)
			{
				this.output.WriteLine(message, args);
				this.failureCount++;
			}
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00022A98 File Offset: 0x00020C98
		private void CheckTemplateTable(Microsoft.Exchange.Server.Storage.PhysicalAccess.Table table)
		{
			this.Check(this.TableIsTemplateTable(table.Name), "Table {0} is partitioned so it should be a template table", new object[]
			{
				table.Name
			});
			this.Check(this.TableIsEmpty(table), "Table {0} is a template table. It should be empty", new object[]
			{
				table.Name
			});
			this.Check(table.SpecialCols.NumberOfPartioningColumns != 0, "Table {0} is partitioned so it should have a partition keys", new object[]
			{
				table.Name
			});
			for (int i = 0; i < table.SpecialCols.NumberOfPartioningColumns; i++)
			{
				this.Check(table.Columns[i].Type == typeof(int), "Partition column {0} in table {1} should be of type int", new object[]
				{
					table.Columns[i],
					table.Name
				});
			}
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00022B82 File Offset: 0x00020D82
		private bool TableIsTemplateTable(string tableName)
		{
			return this.ObjectInfoFlagIsSet(tableName, ObjectInfoFlags.TableTemplate);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00022B90 File Offset: 0x00020D90
		private bool TableIsDerivedTable(string tableName)
		{
			return this.ObjectInfoFlagIsSet(tableName, ObjectInfoFlags.TableDerived);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00022BA0 File Offset: 0x00020DA0
		private bool ObjectInfoFlagIsSet(string tableName, ObjectInfoFlags flag)
		{
			JET_OBJECTINFO jet_OBJECTINFO;
			Api.JetGetObjectInfo(this.sesid, this.dbid, JET_objtyp.Table, tableName, out jet_OBJECTINFO);
			return ObjectInfoFlags.None != (jet_OBJECTINFO.flags & flag);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00022BD0 File Offset: 0x00020DD0
		private bool TableIsEmpty(Microsoft.Exchange.Server.Storage.PhysicalAccess.Table table)
		{
			OpenTableGrbit openTableGrbit = JetConnection.GetOpenTableGrbit(table, false);
			openTableGrbit |= OpenTableGrbit.ReadOnly;
			JET_TABLEID tableid;
			Api.JetOpenTable(this.sesid, this.dbid, table.Name, null, 0, openTableGrbit, out tableid);
			bool result;
			try
			{
				result = !Api.TryMoveFirst(this.sesid, tableid);
			}
			finally
			{
				Api.JetCloseTable(this.sesid, tableid);
			}
			return result;
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00022DF4 File Offset: 0x00020FF4
		private IEnumerable<string> GetTableNamesStartingWith(string prefix)
		{
			foreach (string tablename in Api.GetTableNames(this.sesid, this.dbid))
			{
				if (tablename.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
				{
					yield return tablename;
				}
			}
			yield break;
		}

		// Token: 0x040002A6 RID: 678
		private readonly JetDatabase database;

		// Token: 0x040002A7 RID: 679
		private readonly TextWriter output;

		// Token: 0x040002A8 RID: 680
		private readonly JET_SESID sesid;

		// Token: 0x040002A9 RID: 681
		private readonly JET_DBID dbid;

		// Token: 0x040002AA RID: 682
		private int failureCount;
	}
}
