using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000037 RID: 55
	internal abstract class GetTopSizesProcessor : Processor
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x0000CF50 File Offset: 0x0000B150
		protected GetTopSizesProcessor(string prefix, int pairs, IList<Column> arguments) : base(arguments)
		{
			string arg = prefix ?? "Item";
			int num = Math.Max(pairs, 1);
			this.generated = new List<Processor.ColumnDefinition>(num * 2);
			this.getters = new Dictionary<string, Tuple<Func<int, object>, int>>(num * 2);
			for (int i = 0; i < num; i++)
			{
				string text = string.Format("{0}Name{1}", arg, i + 1);
				string text2 = string.Format("{0}Size{1}", arg, i + 1);
				this.generated.Add(new Processor.ColumnDefinition(text, typeof(string), Visibility.Public));
				this.generated.Add(new Processor.ColumnDefinition(text2, typeof(int), Visibility.Public));
				this.getters[text] = new Tuple<Func<int, object>, int>(new Func<int, object>(this.GetIdentifier), i);
				this.getters[text2] = new Tuple<Func<int, object>, int>(new Func<int, object>(this.GetSize), i);
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000D044 File Offset: 0x0000B244
		public override IEnumerable<Processor.ColumnDefinition> GetGeneratedColumns()
		{
			return this.generated;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000D04C File Offset: 0x0000B24C
		public override void OnBeginRow()
		{
			this.data = null;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000D058 File Offset: 0x0000B258
		public override object GetValue(SimpleQueryOperator qop, Reader reader, Column column)
		{
			if (this.data == null)
			{
				this.data = this.GetData(qop, reader);
				this.data.Sort(GetTopSizesProcessor.sizeComparer);
			}
			Tuple<Func<int, object>, int> tuple;
			if (column != null && this.getters.TryGetValue(column.Name, out tuple))
			{
				return tuple.Item1(tuple.Item2);
			}
			return null;
		}

		// Token: 0x060001A4 RID: 420
		public abstract List<Tuple<string, int>> GetData(SimpleQueryOperator qop, Reader reader);

		// Token: 0x060001A5 RID: 421 RVA: 0x0000D0BC File Offset: 0x0000B2BC
		private object GetIdentifier(int index)
		{
			if (this.data != null && this.data.Count > index)
			{
				return this.data[index].Item1;
			}
			return null;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000D0E7 File Offset: 0x0000B2E7
		private object GetSize(int index)
		{
			if (this.data != null && this.data.Count > index)
			{
				return this.data[index].Item2;
			}
			return null;
		}

		// Token: 0x04000112 RID: 274
		private static Comparison<Tuple<string, int>> sizeComparer = (Tuple<string, int> x, Tuple<string, int> y) => y.Item2.CompareTo(x.Item2);

		// Token: 0x04000113 RID: 275
		private readonly IList<Processor.ColumnDefinition> generated;

		// Token: 0x04000114 RID: 276
		private readonly IDictionary<string, Tuple<Func<int, object>, int>> getters;

		// Token: 0x04000115 RID: 277
		private List<Tuple<string, int>> data;
	}
}
