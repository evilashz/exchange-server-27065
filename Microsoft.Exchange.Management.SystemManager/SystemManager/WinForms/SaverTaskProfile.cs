using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Exchange.Configuration.MonadDataProvider;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000B4 RID: 180
	public class SaverTaskProfile : TaskProfileBase
	{
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x00015CEF File Offset: 0x00013EEF
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x00015CF7 File Offset: 0x00013EF7
		[DDIDictionaryDecorate(UseKeys = false, AttributeType = typeof(DDIDataColumnExistAttribute))]
		[DefaultValue(null)]
		public SavedResultMapping SavedResultMapping
		{
			get
			{
				return this.savedResultMapping;
			}
			set
			{
				this.savedResultMapping = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x00015D00 File Offset: 0x00013F00
		public List<object> SavedResults
		{
			get
			{
				return this.Saver.SavedResults;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00015D0D File Offset: 0x00013F0D
		public bool IsSucceeded
		{
			get
			{
				return this.WorkUnits.AllCompleted;
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00015D1A File Offset: 0x00013F1A
		public void AddSavedResultMapping(string property, string column)
		{
			this.SavedResultMapping[property] = column;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00015D2C File Offset: 0x00013F2C
		internal override void Run(CommandInteractionHandler interactionHandler, DataRow row, DataObjectStore store)
		{
			this.Saver.Run(interactionHandler, row, store);
			if (this.Saver.SavedResults.Count > 0)
			{
				foreach (string text in this.SavedResultMapping.Keys)
				{
					object value = (text != "WholeObjectProperty") ? this.Saver.SavedResults[0].GetType().GetProperty(text).GetValue(this.Saver.SavedResults[0], null) : this.Saver.SavedResults[0];
					row[this.SavedResultMapping[text]] = value;
				}
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00015E08 File Offset: 0x00014008
		private Saver Saver
		{
			get
			{
				return base.Runner as Saver;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00015E15 File Offset: 0x00014015
		public string CommandToRun
		{
			get
			{
				return this.Saver.CommandToRun;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00015E22 File Offset: 0x00014022
		public string ModifiedParametersDescription
		{
			get
			{
				return this.Saver.ModifiedParametersDescription;
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00015E2F File Offset: 0x0001402F
		internal void UpdateWorkUnits(DataRow row)
		{
			this.Saver.UpdateWorkUnits(row);
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x00015E3D File Offset: 0x0001403D
		internal WorkUnitCollection WorkUnits
		{
			get
			{
				return this.Saver.WorkUnits as WorkUnitCollection;
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00015E4F File Offset: 0x0001404F
		public bool HasPermission(string propertyName)
		{
			return (base.Runner as Saver).HasPermission(propertyName, base.GetEffectiveParameters());
		}

		// Token: 0x040001E0 RID: 480
		private SavedResultMapping savedResultMapping = new SavedResultMapping();
	}
}
