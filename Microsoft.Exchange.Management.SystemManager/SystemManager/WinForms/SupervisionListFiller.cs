using System;
using System.Data;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000EF RID: 239
	internal class SupervisionListFiller : AbstractDataTableFiller
	{
		// Token: 0x06000915 RID: 2325 RVA: 0x0001DBB0 File Offset: 0x0001BDB0
		public SupervisionListFiller(string commandText)
		{
			this.command = new LoggableMonadCommand(commandText);
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0001DBC4 File Offset: 0x0001BDC4
		internal MonadCommand Command
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0001DBCC File Offset: 0x0001BDCC
		protected override void OnFill(DataTable table)
		{
			this.Command.CommandType = CommandType.Text;
			object[] array = this.Command.Execute();
			TransportConfigContainer transportConfigContainer = null;
			int num = array.Length;
			if (num == 1)
			{
				transportConfigContainer = (array[0] as TransportConfigContainer);
			}
			if (transportConfigContainer != null && transportConfigContainer.SupervisionTags != null)
			{
				table.BeginLoadData();
				foreach (string text in transportConfigContainer.SupervisionTags)
				{
					if (this.MatchFilter(text))
					{
						table.Rows.Add(new object[]
						{
							text,
							new Word(text)
						});
					}
				}
				table.EndLoadData();
			}
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0001DC90 File Offset: 0x0001BE90
		private bool MatchFilter(string displayString)
		{
			bool result = true;
			if (!string.IsNullOrEmpty(this.searchText) && (string.IsNullOrEmpty(displayString) || -1 == displayString.IndexOf(this.searchText, 0, StringComparison.InvariantCultureIgnoreCase)))
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0001DCC8 File Offset: 0x0001BEC8
		public override object Clone()
		{
			return new SupervisionListFiller(this.Command.CommandText);
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0001DCDA File Offset: 0x0001BEDA
		public override ICommandBuilder CommandBuilder
		{
			get
			{
				return NullableCommandBuilder.Value;
			}
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0001DCE1 File Offset: 0x0001BEE1
		public override void BuildCommand(string searchText, object[] pipeline, DataRow row)
		{
			this.searchText = searchText;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0001DCEA File Offset: 0x0001BEEA
		public override void BuildCommandWithScope(string searchText, object[] pipeline, DataRow row, object scope)
		{
			this.searchText = searchText;
		}

		// Token: 0x040003FF RID: 1023
		private string searchText;

		// Token: 0x04000400 RID: 1024
		private MonadCommand command;
	}
}
