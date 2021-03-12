using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200004F RID: 79
	public class DataHandler : IValidator
	{
		// Token: 0x060002F9 RID: 761 RVA: 0x0000AC62 File Offset: 0x00008E62
		public DataHandler()
		{
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000AC7C File Offset: 0x00008E7C
		public DataHandler(ICloneable dataSource) : this()
		{
			this.DataSource = dataSource.Clone();
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000AC90 File Offset: 0x00008E90
		public DataHandler(bool breakOnError) : this()
		{
			this.BreakOnError = breakOnError;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000AC9F File Offset: 0x00008E9F
		// (set) Token: 0x060002FD RID: 765 RVA: 0x0000ACA7 File Offset: 0x00008EA7
		public bool IsObjectReadOnly
		{
			get
			{
				return this.isObjectReadOnly;
			}
			set
			{
				this.isObjectReadOnly = value;
				if (!this.isObjectReadOnly)
				{
					this.objectReadOnlyReason = string.Empty;
				}
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000ACC3 File Offset: 0x00008EC3
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000ACCB File Offset: 0x00008ECB
		public string ObjectReadOnlyReason
		{
			get
			{
				return this.objectReadOnlyReason;
			}
			set
			{
				this.objectReadOnlyReason = value;
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000ACD4 File Offset: 0x00008ED4
		protected virtual void CheckObjectReadOnly()
		{
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000ACD6 File Offset: 0x00008ED6
		public bool HasDataHandlers
		{
			get
			{
				return this.dataHandlers != null && this.dataHandlers.Count > 0;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000ACF0 File Offset: 0x00008EF0
		public IList<DataHandler> DataHandlers
		{
			get
			{
				if (this.dataHandlers == null)
				{
					this.dataHandlers = new List<DataHandler>();
				}
				return this.dataHandlers;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000AD0B File Offset: 0x00008F0B
		// (set) Token: 0x06000304 RID: 772 RVA: 0x0000AD13 File Offset: 0x00008F13
		public bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				this.readOnly = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000AD1C File Offset: 0x00008F1C
		public List<object> SavedResults
		{
			get
			{
				if (this.savedResults == null)
				{
					this.savedResults = new List<object>();
				}
				return this.savedResults;
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000AD38 File Offset: 0x00008F38
		internal void Read(CommandInteractionHandler interactionHandler, string pageName)
		{
			if (!this.Cancelled)
			{
				CancelEventArgs cancelEventArgs = new CancelEventArgs();
				this.OnReadingData(cancelEventArgs);
				if (!cancelEventArgs.Cancel)
				{
					this.OnReadData(interactionHandler, pageName);
					this.CheckObjectReadOnly();
				}
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000AD70 File Offset: 0x00008F70
		protected virtual void OnReadingData(CancelEventArgs e)
		{
			if (this.ReadingData != null)
			{
				this.ReadingData(this, e);
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000308 RID: 776 RVA: 0x0000AD88 File Offset: 0x00008F88
		// (remove) Token: 0x06000309 RID: 777 RVA: 0x0000ADC0 File Offset: 0x00008FC0
		public event CancelEventHandler ReadingData;

		// Token: 0x0600030A RID: 778 RVA: 0x0000ADF8 File Offset: 0x00008FF8
		internal virtual void OnReadData(CommandInteractionHandler interactionHandler, string pageName)
		{
			if (this.HasDataHandlers)
			{
				foreach (DataHandler dataHandler in this.DataHandlers)
				{
					if (this.Cancelled)
					{
						break;
					}
					dataHandler.Read(interactionHandler, pageName);
				}
				this.DataSource = this.DataHandlers[0].DataSource;
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000AE70 File Offset: 0x00009070
		internal void Save(CommandInteractionHandler interactionHandler)
		{
			if (!this.ReadOnly)
			{
				if (this.HasWorkUnits)
				{
					foreach (WorkUnit workUnit in this.WorkUnits)
					{
						workUnit.ResetStatus();
					}
				}
				if (!this.HasWorkUnits)
				{
					this.UpdateWorkUnits();
				}
				if (!this.Cancelled)
				{
					CancelEventArgs cancelEventArgs = new CancelEventArgs();
					this.OnSavingData(cancelEventArgs);
					if (!cancelEventArgs.Cancel)
					{
						this.OnSaveData(interactionHandler);
						if (!this.HasWorkUnits || !this.WorkUnits.HasFailures)
						{
							this.ClearParameterNames();
						}
					}
				}
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000AF1C File Offset: 0x0000911C
		protected virtual void OnSavingData(CancelEventArgs e)
		{
			if (this.SavingData != null)
			{
				this.SavingData(this, e);
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x0600030D RID: 781 RVA: 0x0000AF34 File Offset: 0x00009134
		// (remove) Token: 0x0600030E RID: 782 RVA: 0x0000AF6C File Offset: 0x0000916C
		public event CancelEventHandler SavingData;

		// Token: 0x0600030F RID: 783 RVA: 0x0000AFA4 File Offset: 0x000091A4
		internal virtual void OnSaveData(CommandInteractionHandler interactionHandler)
		{
			if (this.HasDataHandlers)
			{
				this.SavedResults.Clear();
				foreach (DataHandler dataHandler in this.DataHandlers)
				{
					if (dataHandler.IsModified())
					{
						if (this.Cancelled)
						{
							break;
						}
						dataHandler.ProgressReport += this.OnProgressReport;
						try
						{
							dataHandler.Save(interactionHandler);
						}
						finally
						{
							dataHandler.ProgressReport -= this.OnProgressReport;
						}
						this.SavedResults.AddRange(dataHandler.SavedResults);
						if (this.BreakOnError && !dataHandler.IsSucceeded)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000B06C File Offset: 0x0000926C
		internal virtual string CommandToRun
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (DataHandler dataHandler in this.DataHandlers)
				{
					if (dataHandler.IsModified())
					{
						string commandToRun = dataHandler.CommandToRun;
						if (!string.IsNullOrEmpty(commandToRun))
						{
							stringBuilder.Append(commandToRun);
						}
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000B0E0 File Offset: 0x000092E0
		// (set) Token: 0x06000312 RID: 786 RVA: 0x0000B0E8 File Offset: 0x000092E8
		protected bool BreakOnError
		{
			get
			{
				return this.breakOnError;
			}
			set
			{
				this.breakOnError = value;
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000B0F1 File Offset: 0x000092F1
		public virtual ValidationError[] Validate()
		{
			return this.ValidateOnly(null);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000B0FC File Offset: 0x000092FC
		public virtual ValidationError[] ValidateOnly(object objectToBeValidated)
		{
			List<ValidationError> list = new List<ValidationError>();
			IConfigurable configurable = this.DataSource as IConfigurable;
			if (configurable != null && (objectToBeValidated == null || configurable == objectToBeValidated))
			{
				ValidationError[] array = configurable.Validate();
				if (array != null)
				{
					list.AddRange(array);
				}
			}
			if (this.HasDataHandlers)
			{
				foreach (DataHandler dataHandler in this.DataHandlers)
				{
					if (dataHandler.DataSource != this.DataSource && (objectToBeValidated == null || dataHandler.DataSource == objectToBeValidated))
					{
						ValidationError[] array2 = dataHandler.Validate();
						if (array2 != null)
						{
							list.AddRange(array2);
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000B1B0 File Offset: 0x000093B0
		// (set) Token: 0x06000316 RID: 790 RVA: 0x0000B1B8 File Offset: 0x000093B8
		public object DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				this.dataSource = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000B1C1 File Offset: 0x000093C1
		public bool HasWorkUnits
		{
			get
			{
				return this.workUnits != null && this.workUnits.Count > 0;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000B1DB File Offset: 0x000093DB
		internal WorkUnitCollection WorkUnits
		{
			get
			{
				if (this.workUnits == null)
				{
					this.workUnits = new WorkUnitCollection();
				}
				return this.workUnits;
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000B1F8 File Offset: 0x000093F8
		public virtual void UpdateWorkUnits()
		{
			if (this.HasDataHandlers)
			{
				if (this.HasWorkUnits)
				{
					this.WorkUnits.Clear();
				}
				foreach (DataHandler dataHandler in this.DataHandlers)
				{
					if (dataHandler.IsModified())
					{
						dataHandler.UpdateWorkUnits();
						if (dataHandler.HasWorkUnits)
						{
							foreach (WorkUnit item in dataHandler.WorkUnits)
							{
								this.WorkUnits.Add(item);
							}
						}
					}
				}
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000B2B4 File Offset: 0x000094B4
		public virtual string CompletionStatus
		{
			get
			{
				if (this.HasWorkUnits)
				{
					return this.WorkUnits.Description;
				}
				return "";
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000B2D4 File Offset: 0x000094D4
		public virtual string CompletionDescription
		{
			get
			{
				return " ";
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000B2DB File Offset: 0x000094DB
		public virtual string InProgressDescription
		{
			get
			{
				return " ";
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000B2E2 File Offset: 0x000094E2
		public virtual bool IsSucceeded
		{
			get
			{
				return this.WorkUnits.AllCompleted;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000B2EF File Offset: 0x000094EF
		public bool Cancelled
		{
			get
			{
				return this.cancelled;
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000B2F8 File Offset: 0x000094F8
		public virtual void Cancel()
		{
			this.cancelled = true;
			if (this.HasDataHandlers)
			{
				foreach (DataHandler dataHandler in this.DataHandlers)
				{
					dataHandler.Cancel();
				}
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000B354 File Offset: 0x00009554
		public void ResetCancel()
		{
			this.cancelled = false;
			if (this.HasDataHandlers)
			{
				foreach (DataHandler dataHandler in this.DataHandlers)
				{
					dataHandler.ResetCancel();
				}
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000B3B0 File Offset: 0x000095B0
		public virtual bool CanCancel
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000B3B4 File Offset: 0x000095B4
		public virtual bool OverrideCorruptedValuesWithDefault()
		{
			bool flag = false;
			if (this.DataSource is ADObject)
			{
				flag |= WinformsHelper.OverrideCorruptedValuesWithDefault(this.DataSource as ADObject);
			}
			if (this.HasDataHandlers)
			{
				foreach (DataHandler dataHandler in this.DataHandlers)
				{
					if (dataHandler.DataSource != this.DataSource && dataHandler.DataSource is ADObject)
					{
						flag |= dataHandler.OverrideCorruptedValuesWithDefault();
					}
				}
			}
			return flag;
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000B44C File Offset: 0x0000964C
		public virtual bool IsCorrupted
		{
			get
			{
				bool flag = false;
				IConfigurable configurable = this.DataSource as IConfigurable;
				if (configurable != null)
				{
					flag = !configurable.IsValid;
				}
				if (this.HasDataHandlers)
				{
					foreach (DataHandler dataHandler in this.DataHandlers)
					{
						flag |= dataHandler.IsCorrupted;
					}
				}
				return flag;
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000B4C0 File Offset: 0x000096C0
		protected virtual bool IsModified()
		{
			if (this.HasDataHandlers)
			{
				foreach (DataHandler dataHandler in this.DataHandlers)
				{
					if (dataHandler.IsModified())
					{
						return true;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000B520 File Offset: 0x00009720
		protected HashSet<string> ParameterNames
		{
			get
			{
				if (this.parameterNames == null)
				{
					this.parameterNames = new HashSet<string>();
				}
				return this.parameterNames;
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000B53B File Offset: 0x0000973B
		internal void ClearParameterNames()
		{
			this.ParameterNames.Clear();
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000B548 File Offset: 0x00009748
		internal virtual void SpecifyParameterNames(Dictionary<object, List<string>> bindingMembers)
		{
			if (this.HasDataHandlers)
			{
				using (IEnumerator<DataHandler> enumerator = this.DataHandlers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DataHandler dataHandler = enumerator.Current;
						dataHandler.SpecifyParameterNames(bindingMembers);
					}
					return;
				}
			}
			if (this.DataSource != null && bindingMembers.ContainsKey(this.DataSource))
			{
				this.ParameterNames.UnionWith(bindingMembers[this.DataSource]);
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000B5CC File Offset: 0x000097CC
		internal void SpecifyParameterNames(string parameterName)
		{
			this.SpecifyParameterNames(new List<string>
			{
				parameterName
			});
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000B5F0 File Offset: 0x000097F0
		internal void SpecifyParameterNames(List<string> parameterNames)
		{
			if (this.DataSource != null)
			{
				this.SpecifyParameterNames(new Dictionary<object, List<string>>
				{
					{
						this.DataSource,
						parameterNames
					}
				});
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x0600032A RID: 810 RVA: 0x0000B620 File Offset: 0x00009820
		// (remove) Token: 0x0600032B RID: 811 RVA: 0x0000B658 File Offset: 0x00009858
		internal event EventHandler<ProgressReportEventArgs> ProgressReport;

		// Token: 0x0600032C RID: 812 RVA: 0x0000B68D File Offset: 0x0000988D
		internal void OnProgressReport(object sender, ProgressReportEventArgs e)
		{
			if (this.ProgressReport != null)
			{
				this.ProgressReport(sender, e);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000B6A4 File Offset: 0x000098A4
		internal virtual bool TimeConsuming
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000B6A7 File Offset: 0x000098A7
		public virtual string ModifiedParametersDescription
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000CE RID: 206
		private bool cancelled;

		// Token: 0x040000CF RID: 207
		private bool readOnly;

		// Token: 0x040000D0 RID: 208
		private object dataSource;

		// Token: 0x040000D1 RID: 209
		private List<object> savedResults;

		// Token: 0x040000D2 RID: 210
		private List<DataHandler> dataHandlers;

		// Token: 0x040000D3 RID: 211
		private bool isObjectReadOnly;

		// Token: 0x040000D4 RID: 212
		private string objectReadOnlyReason = string.Empty;

		// Token: 0x040000D7 RID: 215
		private bool breakOnError = true;

		// Token: 0x040000D8 RID: 216
		private WorkUnitCollection workUnits;

		// Token: 0x040000D9 RID: 217
		private HashSet<string> parameterNames;
	}
}
