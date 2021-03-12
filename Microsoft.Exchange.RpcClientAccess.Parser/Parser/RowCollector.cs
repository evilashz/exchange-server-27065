using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200011B RID: 283
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RowCollector : BaseObject
	{
		// Token: 0x060005A3 RID: 1443 RVA: 0x00010808 File Offset: 0x0000EA08
		internal RowCollector(int maxSize, bool throwOnFirstAddFailure, Encoding string8Encoding)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				if (maxSize < 2)
				{
					throw new BufferTooSmallException();
				}
				this.maxSize = maxSize;
				this.propertyRows = new List<PropertyRow>();
				this.throwOnFirstAddFailure = throwOnFirstAddFailure;
				this.string8Encoding = string8Encoding;
				this.writer = new CountWriter();
				this.writer.WriteUInt16(0);
				disposeGuard.Success();
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0001088C File Offset: 0x0000EA8C
		internal RowCollector(int maxSize, PropertyTag[] columns, PropertyValue[][] rows) : this(maxSize, columns, rows, CodePageMap.GetCodePage(CTSGlobals.AsciiEncoding))
		{
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x000108A4 File Offset: 0x0000EAA4
		internal RowCollector(int maxSize, PropertyTag[] columns, PropertyValue[][] rows, int codePageId) : this(maxSize, false, CodePageMap.GetEncoding(codePageId))
		{
			this.SetColumns(columns);
			foreach (PropertyValue[] rowValues in rows)
			{
				if (!this.TryAddRow(rowValues))
				{
					return;
				}
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x000108E5 File Offset: 0x0000EAE5
		internal int MaxSize
		{
			get
			{
				return this.maxSize;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x000108ED File Offset: 0x0000EAED
		internal ushort RowCount
		{
			get
			{
				return (ushort)this.propertyRows.Count;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x000108FB File Offset: 0x0000EAFB
		internal PropertyTag[] Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00010903 File Offset: 0x0000EB03
		internal List<PropertyRow> Rows
		{
			get
			{
				return this.propertyRows;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0001090B File Offset: 0x0000EB0B
		internal Encoding String8Encoding
		{
			get
			{
				return this.string8Encoding;
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00010913 File Offset: 0x0000EB13
		public void SetColumns(PropertyTag[] columns)
		{
			base.CheckDisposed();
			if (this.columns == null || ArrayComparer<PropertyTag>.Comparer.Equals(this.columns, columns))
			{
				this.columns = columns;
				return;
			}
			throw new InvalidOperationException("Can't assign a different list of columns");
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00010948 File Offset: 0x0000EB48
		public bool TryAddRow(PropertyValue[] rowValues)
		{
			base.CheckDisposed();
			if (rowValues == null)
			{
				throw new ArgumentNullException("rowValues");
			}
			if (this.columns == null)
			{
				throw new InvalidOperationException("No columns were set before adding rows");
			}
			if (this.columns.Length != rowValues.Length)
			{
				string paramName = string.Format("rowValues does not contain the correct number of columns.  Expected: {0}  Found: {1}", this.columns.Length, rowValues.Length);
				throw new ArgumentException("rowValues", paramName);
			}
			if (ExTraceGlobals.FaultInjectionTracer.IsTraceEnabled(TraceType.FaultInjection) && RowCollector.FaultInjectRowsOverflow(this.RowCount))
			{
				return false;
			}
			for (int i = 0; i < this.columns.Length; i++)
			{
				if (this.columns[i].PropertyId != rowValues[i].PropertyTag.PropertyId)
				{
					throw new ArgumentException(string.Format("Column {0} contains an incorrect ID.  Expected: {1}  Found: {2}", i, this.columns[i].PropertyId, rowValues[i].PropertyTag.PropertyId));
				}
				if (this.columns[i].PropertyType != PropertyType.Unspecified && rowValues[i].PropertyTag.PropertyType != PropertyType.Error && PropertyTag.RemoveMviWithMvIfNeeded(this.columns[i]).PropertyType != rowValues[i].PropertyTag.PropertyType)
				{
					throw new ArgumentException(string.Format("Column {0} contains an incorrect Type.  Expected: {1}  Found: {2}", i, this.columns[i].PropertyType, rowValues[i].PropertyTag.PropertyType));
				}
			}
			PropertyRow item = new PropertyRow(this.columns, rowValues);
			item.Serialize(this.writer, this.string8Encoding, WireFormatStyle.Rop);
			if (this.writer.Position <= (long)this.maxSize)
			{
				this.propertyRows.Add(item);
				return true;
			}
			if (this.RowCount == 0 && this.throwOnFirstAddFailure)
			{
				throw new BufferTooSmallException();
			}
			return false;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00010B60 File Offset: 0x0000ED60
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("Row Collector:" + Environment.NewLine);
			stringBuilder.AppendLine("MaxSize: " + this.MaxSize);
			if (this.columns != null)
			{
				stringBuilder.AppendLine("Number of Columns: " + this.columns.Length);
			}
			stringBuilder.AppendLine("Number of Rows: " + this.RowCount);
			return stringBuilder.ToString();
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00010BE6 File Offset: 0x0000EDE6
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RowCollector>(this);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00010BF0 File Offset: 0x0000EDF0
		private static bool FaultInjectRowsOverflow(ushort rowCount)
		{
			int num = 0;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<int>(2955291965U, ref num);
			return num > 0 && (int)rowCount >= num;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00010C1D File Offset: 0x0000EE1D
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.writer);
			base.InternalDispose();
		}

		// Token: 0x0400031C RID: 796
		private readonly bool throwOnFirstAddFailure;

		// Token: 0x0400031D RID: 797
		private readonly int maxSize;

		// Token: 0x0400031E RID: 798
		private readonly Encoding string8Encoding;

		// Token: 0x0400031F RID: 799
		private readonly CountWriter writer;

		// Token: 0x04000320 RID: 800
		private PropertyTag[] columns;

		// Token: 0x04000321 RID: 801
		private List<PropertyRow> propertyRows;
	}
}
