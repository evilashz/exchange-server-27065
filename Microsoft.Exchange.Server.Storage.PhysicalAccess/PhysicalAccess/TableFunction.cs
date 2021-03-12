using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200008E RID: 142
	public abstract class TableFunction : Table
	{
		// Token: 0x06000623 RID: 1571 RVA: 0x0001C6C0 File Offset: 0x0001A8C0
		protected TableFunction(string name, TableFunction.GetTableContentsDelegate getTableContents, TableFunction.GetColumnFromRowDelegate getColumnFromRow, Visibility visibility, Type[] parameterTypes, Index[] indexes, params PhysicalColumn[] columns) : base(name, TableClass.TableFunction, CultureHelper.DefaultCultureInfo, false, TableAccessHints.None, false, visibility, false, TableFunction.noSpecialColumns, indexes, Table.NoColumns, columns)
		{
			this.getTableContents = getTableContents;
			this.getColumnFromRow = getColumnFromRow;
			this.parameterTypes = parameterTypes;
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x0001C704 File Offset: 0x0001A904
		public Type[] ParameterTypes
		{
			get
			{
				return this.parameterTypes;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0001C70C File Offset: 0x0001A90C
		internal TableFunction.GetTableContentsDelegate GetTableContents
		{
			get
			{
				return this.getTableContents;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x0001C714 File Offset: 0x0001A914
		internal TableFunction.GetColumnFromRowDelegate GetColumnFromRow
		{
			get
			{
				return this.getColumnFromRow;
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001C71C File Offset: 0x0001A91C
		public override void CreateTable(IConnectionProvider connectionProvider, int version)
		{
			throw new NotSupportedException("CreateTable not supported against table function");
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001C728 File Offset: 0x0001A928
		public override void AddColumn(IConnectionProvider connectionProvider, PhysicalColumn column)
		{
			throw new NotSupportedException("AddColumn not supported against table function");
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001C734 File Offset: 0x0001A934
		public override void RemoveColumn(IConnectionProvider connectionProvider, PhysicalColumn column)
		{
			throw new NotSupportedException("RemoveColumn not supported against table function");
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001C740 File Offset: 0x0001A940
		public override void CreateIndex(IConnectionProvider connectionProvider, Index index, IList<object> partitionValues)
		{
			throw new NotSupportedException("CreateIndex not supported against table function");
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001C74C File Offset: 0x0001A94C
		public override void DeleteIndex(IConnectionProvider connectionProvider, Index index, IList<object> partitionValues)
		{
			throw new NotSupportedException("DeleteIndex not supported against table function");
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001C758 File Offset: 0x0001A958
		public override bool IsIndexCreated(IConnectionProvider connectionProvider, Index index, IList<object> partitionValues)
		{
			throw new NotSupportedException("IsIndexCreated not supported against table function");
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001C764 File Offset: 0x0001A964
		public override bool ValidateLocaleVersion(IConnectionProvider connectionProvider, IList<object> partitionValues)
		{
			throw new NotSupportedException("ValidateLocaleVersion not supported against table function");
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001C770 File Offset: 0x0001A970
		public override void GetTableSize(IConnectionProvider connectionProvider, IList<object> partitionValues, out int totalPages, out int availablePages)
		{
			throw new NotSupportedException("GetTableSize not supported against table function");
		}

		// Token: 0x04000247 RID: 583
		private static readonly SpecialColumns noSpecialColumns = default(SpecialColumns);

		// Token: 0x04000248 RID: 584
		private readonly TableFunction.GetTableContentsDelegate getTableContents;

		// Token: 0x04000249 RID: 585
		private readonly TableFunction.GetColumnFromRowDelegate getColumnFromRow;

		// Token: 0x0400024A RID: 586
		private readonly Type[] parameterTypes;

		// Token: 0x0200008F RID: 143
		// (Invoke) Token: 0x06000631 RID: 1585
		public delegate object GetTableContentsDelegate(IConnectionProvider connectionProvider, object[] parameters);

		// Token: 0x02000090 RID: 144
		// (Invoke) Token: 0x06000635 RID: 1589
		public delegate object GetColumnFromRowDelegate(IConnectionProvider connectionProvider, object obj, PhysicalColumn columnToFetch);
	}
}
