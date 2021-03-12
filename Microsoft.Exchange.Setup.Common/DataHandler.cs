using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000003 RID: 3
	public class DataHandler
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000021E2 File Offset: 0x000003E2
		public DataHandler()
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021FC File Offset: 0x000003FC
		public DataHandler(ICloneable dataSource) : this()
		{
			this.DataSource = dataSource.Clone();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002210 File Offset: 0x00000410
		public DataHandler(bool breakOnError) : this()
		{
			this.BreakOnError = breakOnError;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000221F File Offset: 0x0000041F
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002227 File Offset: 0x00000427
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

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002243 File Offset: 0x00000443
		// (set) Token: 0x0600000E RID: 14 RVA: 0x0000224B File Offset: 0x0000044B
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

		// Token: 0x0600000F RID: 15 RVA: 0x00002254 File Offset: 0x00000454
		protected virtual void CheckObjectReadOnly()
		{
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002256 File Offset: 0x00000456
		public bool HasDataHandlers
		{
			get
			{
				return this.dataHandlers != null && this.dataHandlers.Count > 0;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002270 File Offset: 0x00000470
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

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000228B File Offset: 0x0000048B
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002293 File Offset: 0x00000493
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

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000229C File Offset: 0x0000049C
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

		// Token: 0x06000015 RID: 21 RVA: 0x000022B8 File Offset: 0x000004B8
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

		// Token: 0x06000016 RID: 22 RVA: 0x000022F0 File Offset: 0x000004F0
		protected virtual void OnReadingData(CancelEventArgs e)
		{
			if (this.ReadingData != null)
			{
				this.ReadingData(this, e);
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000017 RID: 23 RVA: 0x00002308 File Offset: 0x00000508
		// (remove) Token: 0x06000018 RID: 24 RVA: 0x00002340 File Offset: 0x00000540
		public event CancelEventHandler ReadingData;

		// Token: 0x06000019 RID: 25 RVA: 0x00002378 File Offset: 0x00000578
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

		// Token: 0x0600001A RID: 26 RVA: 0x000023F0 File Offset: 0x000005F0
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

		// Token: 0x0600001B RID: 27 RVA: 0x0000249C File Offset: 0x0000069C
		protected virtual void OnSavingData(CancelEventArgs e)
		{
			if (this.SavingData != null)
			{
				this.SavingData(this, e);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600001C RID: 28 RVA: 0x000024B4 File Offset: 0x000006B4
		// (remove) Token: 0x0600001D RID: 29 RVA: 0x000024EC File Offset: 0x000006EC
		public event CancelEventHandler SavingData;

		// Token: 0x0600001E RID: 30 RVA: 0x00002524 File Offset: 0x00000724
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

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000025EC File Offset: 0x000007EC
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

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002660 File Offset: 0x00000860
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002668 File Offset: 0x00000868
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

		// Token: 0x06000022 RID: 34 RVA: 0x00002671 File Offset: 0x00000871
		public virtual ValidationError[] Validate()
		{
			return this.ValidateOnly(null);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000267C File Offset: 0x0000087C
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002730 File Offset: 0x00000930
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002738 File Offset: 0x00000938
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002741 File Offset: 0x00000941
		public bool HasWorkUnits
		{
			get
			{
				return this.workUnits != null && this.workUnits.Count > 0;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000275B File Offset: 0x0000095B
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

		// Token: 0x06000028 RID: 40 RVA: 0x00002778 File Offset: 0x00000978
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

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002834 File Offset: 0x00000A34
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

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002854 File Offset: 0x00000A54
		public virtual string CompletionDescription
		{
			get
			{
				return " ";
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000285B File Offset: 0x00000A5B
		public virtual string InProgressDescription
		{
			get
			{
				return " ";
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002862 File Offset: 0x00000A62
		public virtual bool IsSucceeded
		{
			get
			{
				return this.WorkUnits.AllCompleted;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000286F File Offset: 0x00000A6F
		public bool Cancelled
		{
			get
			{
				return this.cancelled;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002878 File Offset: 0x00000A78
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

		// Token: 0x0600002F RID: 47 RVA: 0x000028D4 File Offset: 0x00000AD4
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002930 File Offset: 0x00000B30
		public virtual bool CanCancel
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002934 File Offset: 0x00000B34
		public virtual bool OverrideCorruptedValuesWithDefault()
		{
			bool flag = false;
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000029AC File Offset: 0x00000BAC
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

		// Token: 0x06000033 RID: 51 RVA: 0x00002A20 File Offset: 0x00000C20
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002A80 File Offset: 0x00000C80
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

		// Token: 0x06000035 RID: 53 RVA: 0x00002A9B File Offset: 0x00000C9B
		internal void ClearParameterNames()
		{
			this.ParameterNames.Clear();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002AA8 File Offset: 0x00000CA8
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

		// Token: 0x06000037 RID: 55 RVA: 0x00002B2C File Offset: 0x00000D2C
		internal void SpecifyParameterNames(string parameterName)
		{
			this.SpecifyParameterNames(new List<string>
			{
				parameterName
			});
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002B50 File Offset: 0x00000D50
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

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000039 RID: 57 RVA: 0x00002B80 File Offset: 0x00000D80
		// (remove) Token: 0x0600003A RID: 58 RVA: 0x00002BB8 File Offset: 0x00000DB8
		internal event EventHandler<ProgressReportEventArgs> ProgressReport;

		// Token: 0x0600003B RID: 59 RVA: 0x00002BED File Offset: 0x00000DED
		internal void OnProgressReport(object sender, ProgressReportEventArgs e)
		{
			if (this.ProgressReport != null)
			{
				this.ProgressReport(sender, e);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002C04 File Offset: 0x00000E04
		internal virtual bool TimeConsuming
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002C07 File Offset: 0x00000E07
		public virtual string ModifiedParametersDescription
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000003 RID: 3
		private bool cancelled;

		// Token: 0x04000004 RID: 4
		private bool readOnly;

		// Token: 0x04000005 RID: 5
		private object dataSource;

		// Token: 0x04000006 RID: 6
		private List<object> savedResults;

		// Token: 0x04000007 RID: 7
		private List<DataHandler> dataHandlers;

		// Token: 0x04000008 RID: 8
		private bool isObjectReadOnly;

		// Token: 0x04000009 RID: 9
		private string objectReadOnlyReason = string.Empty;

		// Token: 0x0400000C RID: 12
		private bool breakOnError = true;

		// Token: 0x0400000D RID: 13
		private WorkUnitCollection workUnits;

		// Token: 0x0400000E RID: 14
		private HashSet<string> parameterNames;
	}
}
