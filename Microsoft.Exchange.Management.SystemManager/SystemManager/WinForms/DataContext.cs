using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200011F RID: 287
	public sealed class DataContext
	{
		// Token: 0x06000B06 RID: 2822 RVA: 0x00027B74 File Offset: 0x00025D74
		public DataContext(DataHandler dataHandler, bool bypassCorruptObjectVerification)
		{
			if (dataHandler == null)
			{
				throw new ArgumentNullException("dataHandler");
			}
			this.dataHandler = dataHandler;
			this.bypassCorruptObjectVerification = bypassCorruptObjectVerification;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00027BCB File Offset: 0x00025DCB
		public DataContext(DataHandler dataHandler) : this(dataHandler, false)
		{
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x00027BD5 File Offset: 0x00025DD5
		internal List<ExchangePage> Pages
		{
			get
			{
				return this.pages;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x00027BDD File Offset: 0x00025DDD
		public DataHandler DataHandler
		{
			get
			{
				return this.dataHandler;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x00027BE5 File Offset: 0x00025DE5
		private EventHandlerList Events
		{
			get
			{
				return this.events;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x00027BED File Offset: 0x00025DED
		// (set) Token: 0x06000B0C RID: 2828 RVA: 0x00027BF5 File Offset: 0x00025DF5
		[DefaultValue(false)]
		public bool IsDirty
		{
			get
			{
				return this.isDirty;
			}
			set
			{
				if (this.IsDirty != value)
				{
					this.isDirty = value;
					this.OnIsDirtyChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x00027C12 File Offset: 0x00025E12
		public bool IsSaving
		{
			get
			{
				return this.isAccessingData;
			}
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00027C1C File Offset: 0x00025E1C
		private void OnIsDirtyChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)this.Events[DataContext.EventIsDirtyChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06000B0F RID: 2831 RVA: 0x00027C4A File Offset: 0x00025E4A
		// (remove) Token: 0x06000B10 RID: 2832 RVA: 0x00027C5D File Offset: 0x00025E5D
		public event EventHandler IsDirtyChanged
		{
			add
			{
				SynchronizedDelegate.Combine(this.Events, DataContext.EventIsDirtyChanged, value);
			}
			remove
			{
				SynchronizedDelegate.Remove(this.Events, DataContext.EventIsDirtyChanged, value);
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x00027C70 File Offset: 0x00025E70
		// (set) Token: 0x06000B12 RID: 2834 RVA: 0x00027C78 File Offset: 0x00025E78
		public DataContextFlags Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00027C84 File Offset: 0x00025E84
		internal object ReadData(CommandInteractionHandler interactionHandler, string pageName)
		{
			if (!this.needToReadData)
			{
				if (!(this.DataHandler is AutomatedDataHandler))
				{
					goto IL_53;
				}
			}
			try
			{
				this.isAccessingData = true;
				this.DataHandler.Read(interactionHandler, pageName);
				this.needToReadData = false;
				if (!this.bypassCorruptObjectVerification)
				{
					this.IsDataSourceCorrupted = this.dataHandler.IsCorrupted;
				}
			}
			finally
			{
				this.isAccessingData = false;
			}
			IL_53:
			return this.DataHandler.DataSource;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00027D00 File Offset: 0x00025F00
		internal void SaveData(CommandInteractionHandler interactionHandler)
		{
			if (this.IsDirty)
			{
				try
				{
					this.isAccessingData = true;
					this.DataHandler.Save(interactionHandler);
					bool flag = this.DataHandler.HasWorkUnits && this.DataHandler.WorkUnits.HasFailures;
					if (!flag)
					{
						this.IsDirty = false;
						this.needToReadData = true;
					}
					if (!flag || this.DataHandler.WorkUnits.IsDataChanged)
					{
						this.OnDataSaved(EventArgs.Empty);
					}
				}
				finally
				{
					this.isAccessingData = false;
				}
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x00027D94 File Offset: 0x00025F94
		// (set) Token: 0x06000B16 RID: 2838 RVA: 0x00027D9C File Offset: 0x00025F9C
		[DefaultValue(null)]
		public IRefreshable RefreshOnSave
		{
			get
			{
				return this.refreshOnSave;
			}
			set
			{
				this.refreshOnSave = value;
			}
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00027DA8 File Offset: 0x00025FA8
		private void OnDataSaved(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)this.Events[DataContext.EventDataSaved];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06000B18 RID: 2840 RVA: 0x00027DDA File Offset: 0x00025FDA
		// (remove) Token: 0x06000B19 RID: 2841 RVA: 0x00027DED File Offset: 0x00025FED
		public event EventHandler DataSaved
		{
			add
			{
				SynchronizedDelegate.Combine(this.Events, DataContext.EventDataSaved, value);
			}
			remove
			{
				SynchronizedDelegate.Remove(this.Events, DataContext.EventDataSaved, value);
			}
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00027E00 File Offset: 0x00026000
		public ValidationError[] Validate()
		{
			return this.DataHandler.Validate();
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00027E0D File Offset: 0x0002600D
		public ValidationError[] ValidateOnly(object objectToBeValidated)
		{
			return this.DataHandler.ValidateOnly(objectToBeValidated);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00027E1B File Offset: 0x0002601B
		public void OverrideCorruptedValuesWithDefault()
		{
			this.IsDirty = this.DataHandler.OverrideCorruptedValuesWithDefault();
			this.IsDataSourceCorrupted = false;
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00027E35 File Offset: 0x00026035
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x00027E3D File Offset: 0x0002603D
		public bool IsDataSourceCorrupted
		{
			get
			{
				return this.isDataSourceCorrupted;
			}
			private set
			{
				this.isDataSourceCorrupted = value;
			}
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00027E46 File Offset: 0x00026046
		internal void AllowNextRead()
		{
			this.needToReadData = true;
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00027E50 File Offset: 0x00026050
		public string ModifiedParametersDescription
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.DataHandler.ModifiedParametersDescription);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00027E7B File Offset: 0x0002607B
		public int SelectedObjectsCount
		{
			get
			{
				return this.Flags.SelectedObjectsCount;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x00027E88 File Offset: 0x00026088
		public string SelectedObjectDetailsType
		{
			get
			{
				return this.Flags.SelectedObjectDetailsType;
			}
		}

		// Token: 0x0400049D RID: 1181
		private DataHandler dataHandler;

		// Token: 0x0400049E RID: 1182
		private bool isDirty;

		// Token: 0x0400049F RID: 1183
		private bool isAccessingData;

		// Token: 0x040004A0 RID: 1184
		private EventHandlerList events = new EventHandlerList();

		// Token: 0x040004A1 RID: 1185
		private static readonly object EventIsDirtyChanged = new object();

		// Token: 0x040004A2 RID: 1186
		private static readonly object EventDataSaved = new object();

		// Token: 0x040004A3 RID: 1187
		private List<ExchangePage> pages = new List<ExchangePage>();

		// Token: 0x040004A4 RID: 1188
		private DataContextFlags flags = new DataContextFlags();

		// Token: 0x040004A5 RID: 1189
		private bool isDataSourceCorrupted;

		// Token: 0x040004A6 RID: 1190
		private bool bypassCorruptObjectVerification;

		// Token: 0x040004A7 RID: 1191
		private bool needToReadData = true;

		// Token: 0x040004A8 RID: 1192
		private IRefreshable refreshOnSave;
	}
}
