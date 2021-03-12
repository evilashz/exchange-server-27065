using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Management.SnapIn;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000AD RID: 173
	public class MonadReadTask : Reader
	{
		// Token: 0x06000568 RID: 1384 RVA: 0x00014988 File Offset: 0x00012B88
		public MonadReadTask()
		{
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00014990 File Offset: 0x00012B90
		public MonadReadTask(Type configObjectType) : this("get-" + configObjectType.Name.ToLowerInvariant())
		{
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x000149AD File Offset: 0x00012BAD
		internal MonadReadTask(string commandText)
		{
			this.CommandText = commandText;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x000149BC File Offset: 0x00012BBC
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x000149C4 File Offset: 0x00012BC4
		[DDIValidCommandText]
		public string CommandText
		{
			get
			{
				return this.commandText;
			}
			set
			{
				this.commandText = value;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x000149CD File Offset: 0x00012BCD
		public override object DataObject
		{
			get
			{
				return this.dataObject;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x000149D5 File Offset: 0x00012BD5
		internal MonadCommand Command
		{
			get
			{
				if (this.command == null)
				{
					this.command = new LoggableMonadCommand(this.commandText);
				}
				return this.command;
			}
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x000149F8 File Offset: 0x00012BF8
		public override void Run(object interactionHandler, DataRow row, DataObjectStore store)
		{
			object[] array = this.Command.Execute();
			switch (array.Length)
			{
			case 0:
				break;
			case 1:
				this.dataObject = array[0];
				break;
			default:
				return;
			}
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00014A2D File Offset: 0x00012C2D
		public override void BuildParameters(DataRow row, DataObjectStore store, IList<ParameterProfile> paramInfos)
		{
			this.Command.Parameters.Clear();
			MonadSaveTask.BuildParametersCore(row, paramInfos, this.Command.Parameters);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00014A59 File Offset: 0x00012C59
		public override bool HasPermission(IList<ParameterProfile> paramInfos)
		{
			return EMCRunspaceConfigurationSingleton.GetInstance().IsCmdletAllowedInScope(this.commandText, (from c in paramInfos
			select c.Name).ToArray<string>());
		}

		// Token: 0x040001C5 RID: 453
		private object dataObject;

		// Token: 0x040001C6 RID: 454
		private MonadCommand command;

		// Token: 0x040001C7 RID: 455
		private string commandText;
	}
}
