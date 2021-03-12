using System;
using System.ComponentModel;
using System.Data;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000043 RID: 67
	public class MonadDataTableLoader : DataTableLoader
	{
		// Token: 0x060002A5 RID: 677 RVA: 0x0000A1EC File Offset: 0x000083EC
		public MonadDataTableLoader(DataTable dataTable, string noun)
		{
			if (dataTable == null)
			{
				throw new ArgumentNullException("dataTable");
			}
			this.defaultTable = dataTable;
			base.Table = this.defaultTable;
			base.Table.TableName = noun;
			this.selectCommand = new LoggableMonadCommand();
			base.RefreshArgument = this.selectCommand;
			this.Noun = noun;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000A255 File Offset: 0x00008455
		internal MonadParameterCollection Parameters
		{
			get
			{
				return this.selectCommand.Parameters;
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000A262 File Offset: 0x00008462
		public MonadDataTableLoader(string noun) : this(new DataTable(), noun)
		{
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000A270 File Offset: 0x00008470
		public MonadDataTableLoader(Type type) : this(new DataTable(), type.Name.ToLowerInvariant())
		{
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000A288 File Offset: 0x00008488
		public MonadDataTableLoader() : this("")
		{
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000A295 File Offset: 0x00008495
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.defaultTable.Dispose();
				this.selectCommand.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000A2B7 File Offset: 0x000084B7
		protected sealed override DataTable DefaultTable
		{
			get
			{
				return this.defaultTable;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000A2BF File Offset: 0x000084BF
		protected sealed override ICloneable DefaultRefreshArgument
		{
			get
			{
				return this.selectCommand;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000A2C7 File Offset: 0x000084C7
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000A2D0 File Offset: 0x000084D0
		[DefaultValue("")]
		public virtual string Noun
		{
			get
			{
				return this.noun;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				this.noun = value;
				this.selectCommand.CommandText = "get-" + value;
				this.toString = "MonadDataTableLoader(" + this.selectCommand.CommandText + ")";
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000A324 File Offset: 0x00008524
		public override string ToString()
		{
			return this.toString;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000A32C File Offset: 0x0000852C
		protected override bool TryToGetPartialRefreshArgument(object[] ids, out object partialRefreshArgument)
		{
			if (1 != ids.Length)
			{
				throw new InvalidOperationException();
			}
			MonadCommand monadCommand = this.selectCommand.Clone();
			monadCommand.Parameters.AddWithValue("Identity", ids[0]);
			partialRefreshArgument = monadCommand;
			return true;
		}

		// Token: 0x040000B3 RID: 179
		private DataTable defaultTable;

		// Token: 0x040000B4 RID: 180
		private MonadCommand selectCommand;

		// Token: 0x040000B5 RID: 181
		private string noun = "";

		// Token: 0x040000B6 RID: 182
		private string toString;
	}
}
