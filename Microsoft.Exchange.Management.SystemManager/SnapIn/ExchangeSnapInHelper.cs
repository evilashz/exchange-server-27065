using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementConsole;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.Services;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x0200028A RID: 650
	internal class ExchangeSnapInHelper : IDisposable
	{
		// Token: 0x06001B7A RID: 7034 RVA: 0x00078AFC File Offset: 0x00076CFC
		public ExchangeSnapInHelper(NamespaceSnapInBase snapIn, IExchangeSnapIn exchangeSnapIn)
		{
			this.snapIn = snapIn;
			this.exchangeSnapIn = exchangeSnapIn;
			Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;
			Application.EnableVisualStyles();
			this.snapInName = this.snapIn.GetType().Name;
			try
			{
				Thread.CurrentThread.Name = this.snapInName;
			}
			catch (InvalidOperationException)
			{
			}
			SynchronizationContext synchronizationContext = new SynchronizeInvokeSynchronizationContext(this.snapIn);
			SynchronizationContext.SetSynchronizationContext(synchronizationContext);
			ManagementGUICommon.RegisterAssembly(0, "Microsoft.Exchange.ManagementGUI, Version=15.00.0000.000, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "ObjectPickerSchema.xml");
			ManagementGUICommon.RegisterAssembly(1, "Microsoft.Exchange.ManagementGUI, Version=15.00.0000.000, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "ResultPaneSchema.xml");
			ManagementGUICommon.RegisterAssembly(2, "Microsoft.Exchange.ManagementGUI, Version=15.00.0000.000, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "StrongTypeEditorSchema.xml");
			Assembly assembly = Assembly.Load("Microsoft.Exchange.ManagementGUI, Version=15.00.0000.000, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
			ManagementGUICommon.RegisterResourcesAssembly(ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.ManagementGUI.Resources.Strings", assembly), ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.ManagementGUI.Resources.Icons", assembly));
			this.uiService = new UIService(null);
			this.settingsProvider = new ExchangeSettingsProvider();
			this.services = new ServiceContainer();
			this.services.AddService(typeof(IUIService), this.uiService);
			this.services.AddService(typeof(SynchronizationContext), synchronizationContext);
			this.services.AddService(typeof(ISettingsProviderService), this.settingsProvider);
			this.components = new ServicedContainer(this.services);
			this.LoadTestStub();
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x00078C60 File Offset: 0x00076E60
		public void InitializeSettingProvider()
		{
			this.settingsProvider.Initialize(null, null);
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x00078C6F File Offset: 0x00076E6F
		void IDisposable.Dispose()
		{
			this.components.Dispose();
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x00078C7C File Offset: 0x00076E7C
		internal ServiceContainer Services
		{
			get
			{
				return this.services;
			}
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x00078C84 File Offset: 0x00076E84
		internal void OnInitialize()
		{
			this.services.RemoveService(typeof(IUIService));
			this.uiService = new SnapInUIService(this.snapIn);
			this.services.AddService(typeof(IUIService), this.uiService);
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x00078CD4 File Offset: 0x00076ED4
		public void Initialize(IProgressProvider progressProvider)
		{
			CommandLoggingSession.GetInstance().CommandLoggingEnabled = this.Settings.IsCommandLoggingEnabled;
			CommandLoggingSession.GetInstance().MaximumRecordCount = this.Settings.MaximumRecordCount;
			CommandLoggingDialog.GlobalSettings = this.Settings;
			if (this.Settings.IsCommandLoggingEnabled)
			{
				CommandLoggingDialog.StartDateTime = ((DateTime)ExDateTime.Now).ToString();
			}
			SnapInCallbackService.RegisterSnapInHelpTopicCallback(this.snapIn, new SnapInCallbackService.SnapInHelpTopicCallback(this.HelpCallBack));
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x00078D58 File Offset: 0x00076F58
		private void HelpCallBack(object o)
		{
			ScopeNode scopeNode = o as ScopeNode;
			string helpTopic;
			if (scopeNode != null)
			{
				helpTopic = scopeNode.HelpTopic;
			}
			else
			{
				helpTopic = (o as SelectionData).HelpTopic;
			}
			ExchangeHelpService.ShowHelpFromHelpTopicId(helpTopic);
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x00078D8C File Offset: 0x00076F8C
		internal void Shutdown(AsyncStatus status)
		{
			MonadRemoteRunspaceFactory.ClearAppDomainRemoteRunspaceConnections();
			CommandLoggingDialog.CloseCommandLoggingDialg();
			if (this.settings != null)
			{
				this.settings.PropertyChanged -= this.Settings_PropertyChanged;
			}
			((IDisposable)this).Dispose();
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x00078DBD File Offset: 0x00076FBD
		public IContainer Components
		{
			get
			{
				return this.components;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x00078DC5 File Offset: 0x00076FC5
		public IUIService ShellUI
		{
			get
			{
				return this.uiService;
			}
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x00078DCD File Offset: 0x00076FCD
		public DialogResult ShowDialog(CommonDialog dialog)
		{
			if (this.uiService is SnapInUIService)
			{
				return this.snapIn.Console.ShowDialog(dialog);
			}
			return dialog.ShowDialog(this.uiService.GetDialogOwnerWindow());
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x00078E00 File Offset: 0x00077000
		public int RegisterIcon(string name, Icon icon)
		{
			int num = -1;
			if (icon != null && !string.IsNullOrEmpty(name) && !this.imageListMap.TryGetValue(name, out num))
			{
				Bitmap bitmap = IconLibrary.ToBitmap(icon, SystemInformation.SmallIconSize);
				this.snapIn.SmallImages.Add(bitmap);
				num = this.snapIn.SmallImages.Count - 1;
				this.imageListMap[name] = num;
			}
			return num;
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x00078E68 File Offset: 0x00077068
		public string InstanceDisplayName
		{
			get
			{
				Type type = this.snapIn.GetType();
				object[] customAttributes = type.GetCustomAttributes(typeof(SnapInAboutAttribute), false);
				SnapInAboutAttribute snapInAboutAttribute = customAttributes[0] as SnapInAboutAttribute;
				return WinformsHelper.GetDllResourceString(snapInAboutAttribute.ResourceModule, snapInAboutAttribute.DisplayNameId);
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x00078EB0 File Offset: 0x000770B0
		public ExchangeSettings Settings
		{
			get
			{
				if (this.settings == null)
				{
					this.settings = this.exchangeSnapIn.CreateSettings(this.components);
					this.settings.UpdateProviders(this.settingsProvider);
					this.settings.PropertyChanged += this.Settings_PropertyChanged;
					this.settings.SettingsLoaded += this.Settings_SettingsLoaded;
					this.settings.InstanceDisplayName = this.InstanceDisplayName;
				}
				return this.settings;
			}
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x00078F32 File Offset: 0x00077132
		private void Settings_SettingsLoaded(object sender, SettingsLoadedEventArgs e)
		{
			this.snapIn.IsModified = false;
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x00078F40 File Offset: 0x00077140
		private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			this.snapIn.IsModified = true;
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x00078F4E File Offset: 0x0007714E
		internal ExchangeSettings CreateSettings(IComponent owner)
		{
			return SettingsBase.Synchronized(new ExchangeSettings(owner)) as ExchangeSettings;
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x00078F60 File Offset: 0x00077160
		internal void OnLoadCustomData(AsyncStatus status, byte[] customData)
		{
			try
			{
				this.settingsProvider.ByteData = customData;
			}
			catch (SerializationException)
			{
				this.uiService.ShowMessage(Strings.CannotLoadSettings);
				this.settingsProvider.ByteData = null;
			}
			this.Settings.Reload();
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x00078FBC File Offset: 0x000771BC
		internal byte[] OnSaveCustomData(SyncStatus status)
		{
			byte[] result = null;
			this.Settings.Save();
			try
			{
				result = this.settingsProvider.ByteData;
			}
			catch (Exception innerException)
			{
				throw new LocalizedException(Strings.CannotSaveSettings, innerException);
			}
			return result;
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x00079004 File Offset: 0x00077204
		private void LoadTestStub()
		{
			string text = string.Empty;
			string text2 = string.Empty;
			foreach (string text3 in Environment.GetCommandLineArgs())
			{
				if (text3.StartsWith("/TestStub:", StringComparison.OrdinalIgnoreCase))
				{
					Match match = Regex.Match(text3, ":([^,]+),([^,]+)$");
					if (!match.Success)
					{
						this.uiService.ShowError(Strings.InvalidTestStubArguments);
						break;
					}
					text = match.Groups[1].Value;
					text2 = match.Groups[2].Value;
				}
			}
			Assembly assembly = null;
			ITestStub testStub = null;
			if (text != string.Empty)
			{
				try
				{
					assembly = Assembly.LoadFrom(text);
				}
				catch (FileLoadException ex)
				{
					this.uiService.ShowError(Strings.InvalidTestStubAssembly(ex.Message));
				}
			}
			if (assembly != null)
			{
				Type[] exportedTypes = assembly.GetExportedTypes();
				int j = 0;
				while (j < exportedTypes.Length)
				{
					Type type = exportedTypes[j];
					if (string.Compare(type.ToString(), text2, StringComparison.OrdinalIgnoreCase) == 0)
					{
						if (type.IsSubclassOf(typeof(ITestStub)))
						{
							this.uiService.ShowError(Strings.TestStubNotITestStub);
							break;
						}
						testStub = (assembly.CreateInstance(text2) as ITestStub);
						testStub.InstallStub(this.snapIn);
						break;
					}
					else
					{
						j++;
					}
				}
			}
			if (text != string.Empty && testStub == null)
			{
				this.uiService.ShowError(Strings.TestStubGenericError);
			}
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x00079194 File Offset: 0x00077394
		public override string ToString()
		{
			return this.snapInName;
		}

		// Token: 0x04000A30 RID: 2608
		private NamespaceSnapInBase snapIn;

		// Token: 0x04000A31 RID: 2609
		private IExchangeSnapIn exchangeSnapIn;

		// Token: 0x04000A32 RID: 2610
		private string snapInName;

		// Token: 0x04000A33 RID: 2611
		private IUIService uiService;

		// Token: 0x04000A34 RID: 2612
		private ServiceContainer services;

		// Token: 0x04000A35 RID: 2613
		private ServicedContainer components;

		// Token: 0x04000A36 RID: 2614
		private ExchangeSettingsProvider settingsProvider;

		// Token: 0x04000A37 RID: 2615
		private ExchangeSettings settings;

		// Token: 0x04000A38 RID: 2616
		private Dictionary<string, int> imageListMap = new Dictionary<string, int>();
	}
}
