using System;
using System.Diagnostics;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000021 RID: 33
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class PreCheckDataHandler : SetupSingleTaskDataHandler
	{
		// Token: 0x0600019B RID: 411 RVA: 0x00006BAC File Offset: 0x00004DAC
		protected PreCheckDataHandler(ISetupContext context, DataHandler topLevelHandler, MonadConnection connection) : base(context, "", connection)
		{
			this.TopLevelHandler = topLevelHandler;
			base.BreakOnError = false;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00006BC9 File Offset: 0x00004DC9
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00006BD1 File Offset: 0x00004DD1
		public DataHandler TopLevelHandler
		{
			get
			{
				return this.topLevelHandler;
			}
			set
			{
				this.topLevelHandler = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600019E RID: 414
		public abstract string ShortDescription { get; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600019F RID: 415
		public abstract string Title { get; }

		// Token: 0x060001A0 RID: 416 RVA: 0x00006BDC File Offset: 0x00004DDC
		public override void UpdateWorkUnits()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.DataHandlers.Clear();
			if (this.IsPreConfigNeeded())
			{
				if (this.preConfigHandler == null)
				{
					this.preConfigHandler = new PreConfigTaskDataHandler(base.SetupContext, base.Connection);
					this.preConfigHandler.SelectedInstallableUnits = ((ModeDataHandler)this.TopLevelHandler).SelectedInstallableUnits;
				}
				if (this.preConfigHandler.Roles.Length != 0)
				{
					base.DataHandlers.Add(this.preConfigHandler);
				}
			}
			this.AddPrecheckHandler(this.TopLevelHandler);
			foreach (DataHandler handler in this.TopLevelHandler.DataHandlers)
			{
				this.AddPrecheckHandler(handler);
			}
			this.AddAnalysisTaskDataHandler();
			if (base.HasDataHandlers)
			{
				base.UpdateWorkUnits();
			}
			else
			{
				base.WorkUnits.Clear();
			}
			SetupLogger.TraceExit();
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006CD8 File Offset: 0x00004ED8
		protected override void OnSaveData()
		{
			UserChoiceState userChoiceState = new UserChoiceState();
			userChoiceState.ReadFromContext(base.SetupContext, this.TopLevelHandler as ModeDataHandler);
			userChoiceState.SaveToFile();
			base.OnSaveData();
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00006D10 File Offset: 0x00004F10
		private bool IsPreConfigNeeded()
		{
			foreach (DataHandler dataHandler in this.TopLevelHandler.DataHandlers)
			{
				IPrecheckEnabled precheckEnabled = dataHandler as IPrecheckEnabled;
				if (precheckEnabled != null)
				{
					if (dataHandler is ConfigurationDataHandler && !(dataHandler is UpgradeCfgDataHandler))
					{
						ConfigurationDataHandler configurationDataHandler = (ConfigurationDataHandler)dataHandler;
						if (base.SetupContext.IsPartiallyConfigured(configurationDataHandler.InstallableUnitName))
						{
							continue;
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006D9C File Offset: 0x00004F9C
		[Conditional("DEBUG")]
		private void AssertAllRequestedRolesArePartiallyConfigured(DataHandler topLevelHandler)
		{
			foreach (string text in (topLevelHandler as ModeDataHandler).SelectedInstallableUnits)
			{
				if (!InstallableUnitConfigurationInfoManager.IsUmLanguagePackInstallableUnit(text) && !InstallableUnitConfigurationInfoManager.IsLanguagePacksInstallableUnit(text))
				{
					bool isDatacenterOnly = RoleManager.GetRoleByName(text).IsDatacenterOnly;
				}
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006E0C File Offset: 0x0000500C
		private void AddPrecheckHandler(DataHandler handler)
		{
			IPrecheckEnabled precheckEnabled = handler as IPrecheckEnabled;
			if (precheckEnabled != null)
			{
				if (handler is ConfigurationDataHandler && !(handler is UpgradeCfgDataHandler))
				{
					ConfigurationDataHandler configurationDataHandler = (ConfigurationDataHandler)handler;
					if (base.SetupContext.IsPartiallyConfigured(configurationDataHandler.InstallableUnitName))
					{
						return;
					}
				}
				precheckEnabled.UpdatePreCheckTaskDataHandler();
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00006E54 File Offset: 0x00005054
		private void AddAnalysisTaskDataHandler()
		{
			PrerequisiteAnalysisTaskDataHandler instance = PrerequisiteAnalysisTaskDataHandler.GetInstance(base.SetupContext, base.Connection);
			if (instance.HasRoles)
			{
				instance.InitializeParameters();
				base.DataHandlers.Add(instance);
			}
		}

		// Token: 0x0400005F RID: 95
		private DataHandler topLevelHandler;

		// Token: 0x04000060 RID: 96
		private PreConfigTaskDataHandler preConfigHandler;
	}
}
