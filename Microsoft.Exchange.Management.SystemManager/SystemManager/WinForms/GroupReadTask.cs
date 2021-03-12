using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Management.SnapIn;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000EE RID: 238
	public class GroupReadTask : Reader
	{
		// Token: 0x0600090B RID: 2315 RVA: 0x0001DAE1 File Offset: 0x0001BCE1
		public GroupReadTask()
		{
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0001DAE9 File Offset: 0x0001BCE9
		public GroupReadTask(string commandText)
		{
			this.CommandText = commandText;
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x0001DAF8 File Offset: 0x0001BCF8
		public override object DataObject
		{
			get
			{
				return this.dataObject;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x0001DB00 File Offset: 0x0001BD00
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

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x0001DB21 File Offset: 0x0001BD21
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x0001DB29 File Offset: 0x0001BD29
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

		// Token: 0x06000911 RID: 2321 RVA: 0x0001DB32 File Offset: 0x0001BD32
		public override void Run(object interactionHandler, DataRow row, DataObjectStore store)
		{
			this.dataObject = new List<object>(this.Command.Execute());
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0001DB52 File Offset: 0x0001BD52
		public override bool HasPermission(IList<ParameterProfile> paramInfos)
		{
			return EMCRunspaceConfigurationSingleton.GetInstance().IsCmdletAllowedInScope(this.commandText, (from c in paramInfos
			select c.Name).ToArray<string>());
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0001DB8C File Offset: 0x0001BD8C
		public override void BuildParameters(DataRow row, DataObjectStore store, IList<ParameterProfile> paramInfos)
		{
			this.Command.Parameters.Clear();
			MonadSaveTask.BuildParametersCore(row, paramInfos, this.Command.Parameters);
		}

		// Token: 0x040003FB RID: 1019
		private object dataObject;

		// Token: 0x040003FC RID: 1020
		private LoggableMonadCommand command;

		// Token: 0x040003FD RID: 1021
		private string commandText;
	}
}
