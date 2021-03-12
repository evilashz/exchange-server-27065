using System;
using System.ComponentModel;
using System.Configuration;
using System.Threading;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Diagnostics.Components.Management.SystemManager;
using Microsoft.Exchange.Management.SystemManager;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x0200026E RID: 622
	[SettingsProvider(typeof(ExchangeSettingsProvider))]
	public class ExchangeADServerSettings : ExchangeSettings
	{
		// Token: 0x06001AA6 RID: 6822 RVA: 0x000759F1 File Offset: 0x00073BF1
		public ExchangeADServerSettings(IComponent owner) : base(owner)
		{
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x00075A11 File Offset: 0x00073C11
		// (set) Token: 0x06001AA8 RID: 6824 RVA: 0x00075A28 File Offset: 0x00073C28
		[DefaultSettingValue("false")]
		public bool ForestViewEnabled
		{
			get
			{
				return this.ADServerSettings == null || this.ADServerSettings.ViewEntireForest;
			}
			set
			{
				if (this.ADServerSettings == null)
				{
					throw new NotSupportedException();
				}
				if (this.ForestViewEnabled != value)
				{
					this.ADServerSettings.ViewEntireForest = value;
					this.OnPropertyChanged(this, new PropertyChangedEventArgs("ForestViewEnabled"));
				}
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x00075A5E File Offset: 0x00073C5E
		// (set) Token: 0x06001AAA RID: 6826 RVA: 0x00075A75 File Offset: 0x00073C75
		[DefaultSettingValue("")]
		public string OrganizationalUnit
		{
			get
			{
				if (this.ADServerSettings == null)
				{
					return null;
				}
				return this.ADServerSettings.RecipientViewRoot;
			}
			set
			{
				if (this.ADServerSettings == null)
				{
					throw new NotSupportedException();
				}
				if (string.Compare(this.OrganizationalUnit, value, StringComparison.OrdinalIgnoreCase) != 0)
				{
					this.ADServerSettings.RecipientViewRoot = value;
					this.OnPropertyChanged(this, new PropertyChangedEventArgs("OrganizationalUnit"));
				}
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x00075AB1 File Offset: 0x00073CB1
		// (set) Token: 0x06001AAC RID: 6828 RVA: 0x00075AEF File Offset: 0x00073CEF
		[DefaultSettingValue("")]
		public Fqdn DomainController
		{
			get
			{
				if (this.ADServerSettings == null)
				{
					return null;
				}
				if (this.ADServerSettings.UserPreferredDomainControllers != null && this.ADServerSettings.UserPreferredDomainControllers.Count != 0)
				{
					return this.ADServerSettings.UserPreferredDomainControllers[0];
				}
				return null;
			}
			set
			{
				if (this.ADServerSettings == null)
				{
					throw new NotSupportedException();
				}
				if (!object.Equals(this.DomainController, value))
				{
					this.ADServerSettings.UserPreferredDomainControllers = new MultiValuedProperty<Fqdn>(value);
					this.OnPropertyChanged(this, new PropertyChangedEventArgs("DomainController"));
				}
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x00075B2F File Offset: 0x00073D2F
		// (set) Token: 0x06001AAE RID: 6830 RVA: 0x00075B46 File Offset: 0x00073D46
		[DefaultSettingValue("")]
		public Fqdn GlobalCatalog
		{
			get
			{
				if (this.ADServerSettings == null)
				{
					return null;
				}
				return this.ADServerSettings.UserPreferredGlobalCatalog;
			}
			set
			{
				if (this.ADServerSettings == null)
				{
					throw new NotSupportedException();
				}
				if (!object.Equals(this.GlobalCatalog, value))
				{
					this.ADServerSettings.UserPreferredGlobalCatalog = value;
					this.OnPropertyChanged(this, new PropertyChangedEventArgs("GlobalCatalog"));
				}
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x00075B81 File Offset: 0x00073D81
		// (set) Token: 0x06001AB0 RID: 6832 RVA: 0x00075B9C File Offset: 0x00073D9C
		[UserScopedSetting]
		[DefaultSettingValue("")]
		public ADObjectId ConfigDomain
		{
			get
			{
				return ((ADObjectId)this["ConfigDomain"]) ?? new ADObjectId();
			}
			set
			{
				this["ConfigDomain"] = value;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x00075BAA File Offset: 0x00073DAA
		// (set) Token: 0x06001AB2 RID: 6834 RVA: 0x00075BC1 File Offset: 0x00073DC1
		[DefaultSettingValue("")]
		public Fqdn ConfigurationDomainController
		{
			get
			{
				if (this.ADServerSettings == null)
				{
					return null;
				}
				return this.ADServerSettings.UserPreferredConfigurationDomainController;
			}
			set
			{
				if (this.ADServerSettings == null)
				{
					throw new NotSupportedException();
				}
				if (!object.Equals(this.ConfigurationDomainController, value))
				{
					this.ADServerSettings.UserPreferredConfigurationDomainController = value;
					this.OnPropertyChanged(this, new PropertyChangedEventArgs("ConfigurationDomainController"));
				}
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x00075BFC File Offset: 0x00073DFC
		// (set) Token: 0x06001AB4 RID: 6836 RVA: 0x00075C14 File Offset: 0x00073E14
		[UserScopedSetting]
		[DefaultSettingValue("")]
		public RunspaceServerSettingsPresentationObject ADServerSettings
		{
			get
			{
				this.EnsureADSettingsEnforced();
				return (RunspaceServerSettingsPresentationObject)this["ADServerSettings"];
			}
			set
			{
				this["ADServerSettings"] = value;
			}
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x00075C22 File Offset: 0x00073E22
		private void EnsureADSettingsEnforced()
		{
			if (this["ADServerSettings"] == null && !EnvironmentAnalyzer.IsWorkGroup())
			{
				this.waitHandle.WaitOne();
			}
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x00075C44 File Offset: 0x00073E44
		internal void EnforceADSettings()
		{
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<ExchangeADServerSettings>(0L, "-->ExchangeSystemManagerSettings.EnforceAdSettings: {0}", this);
			if (this["ADServerSettings"] == null && !EnvironmentAnalyzer.IsWorkGroup() && OrganizationType.Cloud != PSConnectionInfoSingleton.GetInstance().Type)
			{
				try
				{
					try
					{
						using (MonadConnection monadConnection = new MonadConnection("timeout=30", new CommandInteractionHandler(), null, PSConnectionInfoSingleton.GetInstance().GetMonadConnectionInfo()))
						{
							monadConnection.Open();
							LoggableMonadCommand loggableMonadCommand = new LoggableMonadCommand("Get-ADServerSettingsForLogonUser", monadConnection);
							object[] array = loggableMonadCommand.Execute();
							if (array != null && array.Length > 0)
							{
								RunspaceServerSettingsPresentationObject runspaceServerSettingsPresentationObject = array[0] as RunspaceServerSettingsPresentationObject;
								this.ADServerSettings = runspaceServerSettingsPresentationObject;
								this.OrganizationalUnit = runspaceServerSettingsPresentationObject.RecipientViewRoot;
								this.ForestViewEnabled = runspaceServerSettingsPresentationObject.ViewEntireForest;
								this.GlobalCatalog = runspaceServerSettingsPresentationObject.UserPreferredGlobalCatalog;
								this.ConfigurationDomainController = runspaceServerSettingsPresentationObject.UserPreferredConfigurationDomainController;
								if (runspaceServerSettingsPresentationObject.UserPreferredDomainControllers != null && runspaceServerSettingsPresentationObject.UserPreferredDomainControllers.Count != 0)
								{
									this.DomainController = runspaceServerSettingsPresentationObject.UserPreferredDomainControllers[0];
								}
							}
							else
							{
								this.SetDefaultSettings();
							}
						}
					}
					catch (Exception)
					{
						this.SetDefaultSettings();
					}
					goto IL_11A;
				}
				finally
				{
					this.waitHandle.Set();
				}
			}
			this.waitHandle.Set();
			IL_11A:
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<ExchangeADServerSettings>(0L, "<--ExchangeSystemManagerSettings.EnforceAdSettings: {0}", this);
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x00075DA8 File Offset: 0x00073FA8
		internal void SetDefaultSettings()
		{
			base.DoBeginInit();
			this.ADServerSettings = new RunspaceServerSettingsPresentationObject();
			this.ForestViewEnabled = true;
			base.DoEndInit(false);
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x00075DCC File Offset: 0x00073FCC
		public RunspaceServerSettingsPresentationObject CreateRunspaceServerSettingsObject()
		{
			if (EnvironmentAnalyzer.IsWorkGroup() || OrganizationType.Cloud == PSConnectionInfoSingleton.GetInstance().Type || this.ADServerSettings == null)
			{
				return null;
			}
			if (this.ADServerSettings != null)
			{
				lock (this.syncRoot)
				{
					return this.ADServerSettings.Clone() as RunspaceServerSettingsPresentationObject;
				}
			}
			return null;
		}

		// Token: 0x040009E6 RID: 2534
		public const string ForestViewEnabledString = "ForestViewEnabled";

		// Token: 0x040009E7 RID: 2535
		public const string OrganizationalUnitString = "OrganizationalUnit";

		// Token: 0x040009E8 RID: 2536
		public const string DomainControllerString = "DomainController";

		// Token: 0x040009E9 RID: 2537
		public const string GlobalCatalogString = "GlobalCatalog";

		// Token: 0x040009EA RID: 2538
		public const string ConfigDomainString = "ConfigDomain";

		// Token: 0x040009EB RID: 2539
		public const string ConfigurationDomainControllerString = "ConfigurationDomainController";

		// Token: 0x040009EC RID: 2540
		private object syncRoot = new object();

		// Token: 0x040009ED RID: 2541
		private ManualResetEvent waitHandle = new ManualResetEvent(false);
	}
}
