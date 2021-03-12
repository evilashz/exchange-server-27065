using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.SystemManager;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementConsole;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.Commands;
using Microsoft.ManagementGUI.Services;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x02000289 RID: 649
	public abstract class ExchangeScopeNode : ScopeNode
	{
		// Token: 0x06001B5D RID: 7005 RVA: 0x000783F6 File Offset: 0x000765F6
		public ExchangeScopeNode() : this(false)
		{
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x00078400 File Offset: 0x00076600
		public ExchangeScopeNode(bool hideExpandIcon) : base(hideExpandIcon)
		{
			Type type = base.GetType();
			base.LanguageIndependentName = type.Name;
			base.HelpTopic = base.GetType().FullName;
			if (Guid.Empty == base.NodeType)
			{
				throw new ApplicationException(type.FullName + " is missing the NodeTypeAttribute.");
			}
			if (!type.IsSealed)
			{
				throw new ApplicationException(type.FullName + " is a scope node of a non-sealed class.");
			}
			FieldInfo field = type.GetField("NodeGuid");
			if (null == field)
			{
				throw new ApplicationException(type.FullName + " is missing the public const string NodeGuid field.");
			}
			string strB = field.GetValue(null) as string;
			if (string.Compare(base.NodeType.ToString(), strB, true, CultureInfo.InvariantCulture) != 0)
			{
				throw new ApplicationException(type.FullName + " NodeGuid field does not correspond to the NodeType property. Check that the NodeType attribute is using the correct constant.");
			}
			ServiceContainer serviceContainer = new ServiceContainer(this.SnapIn);
			this.progressProvider = new ScopeNodeProgressProvider(this);
			serviceContainer.AddService(typeof(IProgressProvider), this.progressProvider);
			this.components = new ServicedContainer(serviceContainer);
			base.EnabledStandardVerbs |= 64;
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x00078538 File Offset: 0x00076738
		protected void RegisterConnectionToPSServerAction()
		{
			this.propertiesCommand = new Command();
			this.propertiesCommand.Name = "Properties";
			this.propertiesCommand.Icon = Icons.Properties;
			this.propertiesCommand.Text = Strings.ShowPropertiesCommand;
			this.propertiesCommand.Execute += this.connectToServerCommand_Execute;
			this.Commands.Add(this.propertiesCommand);
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x000785AD File Offset: 0x000767AD
		// (set) Token: 0x06001B61 RID: 7009 RVA: 0x000785B8 File Offset: 0x000767B8
		public Icon Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("icon");
				}
				if (this.Icon != value)
				{
					this.icon = value;
					base.SelectedImageIndex = (base.ImageIndex = this.SnapIn.RegisterIcon(base.LanguageIndependentName, this.Icon));
					this.OnIconChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x00078614 File Offset: 0x00076814
		protected virtual void OnIconChanged(EventArgs e)
		{
			EventHandler iconChanged = this.IconChanged;
			if (iconChanged != null)
			{
				iconChanged(this, e);
			}
		}

		// Token: 0x140000B1 RID: 177
		// (add) Token: 0x06001B63 RID: 7011 RVA: 0x00078634 File Offset: 0x00076834
		// (remove) Token: 0x06001B64 RID: 7012 RVA: 0x0007866C File Offset: 0x0007686C
		public event EventHandler IconChanged;

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001B65 RID: 7013 RVA: 0x000786A1 File Offset: 0x000768A1
		public IExchangeSnapIn SnapIn
		{
			get
			{
				return (IExchangeSnapIn)base.SnapIn;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x000786AE File Offset: 0x000768AE
		public IUIService ShellUI
		{
			get
			{
				return this.SnapIn.ShellUI;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001B67 RID: 7015 RVA: 0x000786BB File Offset: 0x000768BB
		public IProgressProvider ProgressProvider
		{
			get
			{
				return this.progressProvider;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x000786C3 File Offset: 0x000768C3
		public CommandCollection Commands
		{
			get
			{
				if (this.commands == null)
				{
					this.commands = new CommandCollection();
					new CommandsActionsAdapter(this.SnapIn, base.ActionsPaneItems, this.commands, false, this.SnapIn, null);
				}
				return this.commands;
			}
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x000786FE File Offset: 0x000768FE
		public virtual void InitializeView(Control control, IProgress status)
		{
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x00078700 File Offset: 0x00076900
		protected sealed override void OnRefresh(AsyncStatus status)
		{
			try
			{
				SynchronizationContext.SetSynchronizationContext(new SynchronizeInvokeSynchronizationContext(base.SnapIn));
				if (this.DataSource != null)
				{
					status.EnableManualCompletion();
					this.DataSource.Refresh(new StatusProgress(status, base.SnapIn));
				}
				if (this.Refreshing != null)
				{
					this.Refreshing(this, EventArgs.Empty);
				}
			}
			catch (Exception ex)
			{
				if (ExceptionHelper.IsUICriticalException(ex))
				{
					throw;
				}
				this.ShellUI.ShowError(ex);
			}
		}

		// Token: 0x140000B2 RID: 178
		// (add) Token: 0x06001B6B RID: 7019 RVA: 0x00078788 File Offset: 0x00076988
		// (remove) Token: 0x06001B6C RID: 7020 RVA: 0x000787C0 File Offset: 0x000769C0
		public event EventHandler Refreshing;

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x000787F5 File Offset: 0x000769F5
		// (set) Token: 0x06001B6E RID: 7022 RVA: 0x00078800 File Offset: 0x00076A00
		public IRefreshableNotification DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				if (this.DataSource != value)
				{
					IComponent component = this.DataSource as IComponent;
					if (component != null)
					{
						this.components.Remove(component);
					}
					this.dataSource = value;
					component = (this.DataSource as IComponent);
					if (component != null && component.Site == null)
					{
						this.components.Add(component, this.DataSource.GetHashCode().ToString());
					}
					if (this.DataSource != null)
					{
						base.EnabledStandardVerbs |= 64;
						return;
					}
					base.EnabledStandardVerbs &= -65;
				}
			}
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x00078895 File Offset: 0x00076A95
		protected sealed override void OnExpand(AsyncStatus status)
		{
			base.OnExpand(status);
			if (this.IsSnapInRootNode)
			{
				this.InitializeSnapIn();
			}
			this.OnExpand();
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x000788B2 File Offset: 0x00076AB2
		protected virtual void OnExpand()
		{
			if (this.IsSnapInRootNode)
			{
				ExchangeHelpService.Initialize();
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x000788C1 File Offset: 0x00076AC1
		protected IContainer Components
		{
			get
			{
				return this.components;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001B72 RID: 7026 RVA: 0x000788C9 File Offset: 0x00076AC9
		private bool IsSnapInRootNode
		{
			get
			{
				return 1 == this.SnapIn.ScopeNodeCollection.Count && this.SnapIn.ScopeNodeCollection[0] == this;
			}
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x000788F4 File Offset: 0x00076AF4
		protected void InitializeSnapIn()
		{
			CmdletAssemblyHelper.LoadingAllCmdletAssembliesAndReference(AppDomain.CurrentDomain.BaseDirectory, new string[0]);
			this.SnapIn.Initialize(this.ProgressProvider);
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x00078920 File Offset: 0x00076B20
		public virtual DialogResult ShowDialog(Form form)
		{
			DialogResult result = DialogResult.Cancel;
			try
			{
				this.Components.Add(form, form.Name + form.GetHashCode());
				result = (this.SnapIn as NamespaceSnapInBase).Console.ShowDialog(form);
			}
			finally
			{
				this.Components.Remove(form);
			}
			return result;
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x00078988 File Offset: 0x00076B88
		public DialogResult ShowDialog(ExchangePropertyPageControl propertyPage)
		{
			propertyPage.AutoScaleDimensions = ExchangeUserControl.DefaultAutoScaleDimension;
			propertyPage.AutoScaleMode = AutoScaleMode.Font;
			DialogResult result;
			using (PropertyPageDialog propertyPageDialog = new PropertyPageDialog(propertyPage))
			{
				result = this.ShowDialog(propertyPageDialog);
			}
			return result;
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x000789D4 File Offset: 0x00076BD4
		public DialogResult ShowDialog(string caption, string dialogHelpTopic, ExchangePropertyPageControl[] pages)
		{
			foreach (ExchangePropertyPageControl exchangePropertyPageControl in pages)
			{
				exchangePropertyPageControl.AutoScaleDimensions = ExchangeUserControl.DefaultAutoScaleDimension;
				exchangePropertyPageControl.AutoScaleMode = AutoScaleMode.Font;
			}
			DialogResult result;
			using (PropertySheetDialog propertySheetDialog = new PropertySheetDialog(caption, pages))
			{
				propertySheetDialog.HelpTopic = dialogHelpTopic;
				result = this.ShowDialog(propertySheetDialog);
			}
			return result;
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x00078A40 File Offset: 0x00076C40
		public override string ToString()
		{
			return base.LanguageIndependentName;
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x00078A48 File Offset: 0x00076C48
		protected override bool OnExpandFromLoad(SyncStatus status)
		{
			return base.OnExpandFromLoad(status);
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x00078A54 File Offset: 0x00076C54
		private void connectToServerCommand_Execute(object sender, EventArgs e)
		{
			string snapInGuidString = this.SnapIn.SnapInGuidString;
			ConnectionToRemotePSServerControl connectionToRemotePSServerControl = new ConnectionToRemotePSServerControl(this.SnapIn.RootNodeIcon);
			RemotePSDataHandler dataHandler = new RemotePSDataHandler(this.SnapIn.RootNodeDisplayName);
			connectionToRemotePSServerControl.Context = new DataContext(dataHandler);
			using (PropertySheetDialog propertySheetDialog = new PropertySheetDialog(Strings.SingleSelectionProperties(this.SnapIn.RootNodeDisplayName), new ExchangePropertyPageControl[]
			{
				connectionToRemotePSServerControl
			}))
			{
				propertySheetDialog.Name = snapInGuidString;
				propertySheetDialog.HelpTopic = connectionToRemotePSServerControl.HelpTopic;
				this.ShowDialog(propertySheetDialog);
			}
		}

		// Token: 0x04000A28 RID: 2600
		private ServicedContainer components;

		// Token: 0x04000A29 RID: 2601
		private IProgressProvider progressProvider;

		// Token: 0x04000A2A RID: 2602
		private IRefreshableNotification dataSource;

		// Token: 0x04000A2B RID: 2603
		private CommandCollection commands;

		// Token: 0x04000A2C RID: 2604
		private Command propertiesCommand;

		// Token: 0x04000A2D RID: 2605
		private Icon icon;
	}
}
