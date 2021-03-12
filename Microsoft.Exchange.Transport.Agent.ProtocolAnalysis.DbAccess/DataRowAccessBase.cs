using System;
using System.Globalization;
using System.Reflection;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess
{
	// Token: 0x02000007 RID: 7
	internal abstract class DataRowAccessBase<TTable, TData> : DataRow where TTable : DataTable where TData : DataRow, new()
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000235A File Offset: 0x0000055A
		public DataRowAccessBase() : base(DbAccessServices.GetTableByType(typeof(TTable)))
		{
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002374 File Offset: 0x00000574
		private static void SetPrimaryKeyField(TData data, object value)
		{
			Type typeFromHandle = typeof(TData);
			if (string.IsNullOrEmpty(DataRowAccessBase<TTable, TData>.primaryKey))
			{
				BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public;
				Type typeFromHandle2 = typeof(PrimaryKeyAttribute);
				foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties(bindingAttr))
				{
					PrimaryKeyAttribute[] array = (PrimaryKeyAttribute[])propertyInfo.GetCustomAttributes(typeFromHandle2, false);
					if (array != null && array.Length != 0)
					{
						DataRowAccessBase<TTable, TData>.primaryKey = propertyInfo.Name;
						break;
					}
				}
			}
			PropertyInfo property = typeFromHandle.GetProperty(DataRowAccessBase<TTable, TData>.primaryKey, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);
			property.SetValue(data, value, null);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002410 File Offset: 0x00000610
		public static TData NewData(object value)
		{
			TData tdata = Activator.CreateInstance<TData>();
			DataRowAccessBase<TTable, TData>.SetPrimaryKeyField(tdata, value);
			return tdata;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000242C File Offset: 0x0000062C
		public static TData Find(object value)
		{
			TData data = Activator.CreateInstance<TData>();
			DataRowAccessBase<TTable, TData>.SetPrimaryKeyField(data, value);
			TData result;
			using (DataConnection dataConnection = Database.DataSource.DemandNewConnection())
			{
				DataTable tableByType = DbAccessServices.GetTableByType(typeof(TTable));
				using (DataTableCursor dataTableCursor = tableByType.OpenCursor(dataConnection))
				{
					using (dataTableCursor.BeginTransaction())
					{
						if (data.TrySeekCurrent(dataTableCursor))
						{
							result = DataRowAccessBase<TTable, TData>.LoadCurrentRow(dataTableCursor);
						}
						else
						{
							result = default(TData);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024E4 File Offset: 0x000006E4
		public new void Commit()
		{
			base.Commit();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024EC File Offset: 0x000006EC
		public new void MarkToDelete()
		{
			base.MarkToDelete();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024F4 File Offset: 0x000006F4
		public static TData LoadCurrentRow(DataTableCursor cursor)
		{
			TData tdata = Activator.CreateInstance<TData>();
			typeof(DataRow).InvokeMember("LoadFromCurrentRow", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, tdata, new object[]
			{
				cursor
			}, CultureInfo.InvariantCulture);
			return tdata;
		}

		// Token: 0x04000007 RID: 7
		private static string primaryKey;
	}
}
