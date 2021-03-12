using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000032 RID: 50
	internal abstract class Processor
	{
		// Token: 0x0600018E RID: 398 RVA: 0x0000CCF4 File Offset: 0x0000AEF4
		protected Processor(IList<Column> arguments)
		{
			this.arguments = Processor.GetDictionary(arguments);
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000CD08 File Offset: 0x0000AF08
		public IDictionary<string, Column> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x06000190 RID: 400
		public abstract IEnumerable<Processor.ColumnDefinition> GetGeneratedColumns();

		// Token: 0x06000191 RID: 401
		public abstract object GetValue(SimpleQueryOperator qop, Reader reader, Column column);

		// Token: 0x06000192 RID: 402 RVA: 0x0000CD10 File Offset: 0x0000AF10
		public virtual void OnBeginRow()
		{
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000CD12 File Offset: 0x0000AF12
		public virtual void OnAfterRow()
		{
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000CD14 File Offset: 0x0000AF14
		private static IDictionary<string, Column> GetDictionary(IList<Column> columns)
		{
			IDictionary<string, Column> dictionary = new Dictionary<string, Column>(columns.Count);
			foreach (Column column in columns)
			{
				dictionary[column.Name] = column;
			}
			return dictionary;
		}

		// Token: 0x0400010D RID: 269
		private readonly IDictionary<string, Column> arguments;

		// Token: 0x02000033 RID: 51
		public struct ColumnDefinition
		{
			// Token: 0x06000195 RID: 405 RVA: 0x0000CD70 File Offset: 0x0000AF70
			public ColumnDefinition(string name, Type type, Visibility visibility)
			{
				this.Name = name;
				this.Type = type;
				this.Visibility = visibility;
			}

			// Token: 0x0400010E RID: 270
			public string Name;

			// Token: 0x0400010F RID: 271
			public Type Type;

			// Token: 0x04000110 RID: 272
			public Visibility Visibility;
		}
	}
}
