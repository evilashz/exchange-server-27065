using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.ManagementConsole;
using Microsoft.ManagementConsole.Advanced;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x0200028E RID: 654
	public abstract class ExchangeExtensionSnapIn : NamespaceExtension, IExchangeSnapIn, IServiceProvider
	{
		// Token: 0x06001BB1 RID: 7089 RVA: 0x000793FB File Offset: 0x000775FB
		public ExchangeExtensionSnapIn()
		{
			this.helper = new ExchangeSnapInHelper(this, this);
			this.helper.InitializeSettingProvider();
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x0007941B File Offset: 0x0007761B
		object IServiceProvider.GetService(Type serviceType)
		{
			return this.helper.Services.GetService(serviceType);
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x0007942E File Offset: 0x0007762E
		protected override void OnInitialize()
		{
			this.helper.OnInitialize();
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x0007943B File Offset: 0x0007763B
		public virtual void Initialize(IProgressProvider progressProvider)
		{
			this.helper.Initialize(progressProvider);
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x00079449 File Offset: 0x00077649
		protected override void OnShutdown(AsyncStatus status)
		{
			this.helper.Shutdown(status);
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x00079457 File Offset: 0x00077657
		public IContainer Components
		{
			get
			{
				return this.helper.Components;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x00079464 File Offset: 0x00077664
		public IUIService ShellUI
		{
			get
			{
				return this.helper.ShellUI;
			}
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x00079471 File Offset: 0x00077671
		public DialogResult ShowDialog(CommonDialog dialog)
		{
			return this.helper.ShowDialog(dialog);
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x0007947F File Offset: 0x0007767F
		public virtual string InstanceDisplayName
		{
			get
			{
				return this.helper.InstanceDisplayName;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001BBA RID: 7098 RVA: 0x0007948C File Offset: 0x0007768C
		public ExchangeSettings Settings
		{
			get
			{
				return this.helper.Settings;
			}
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x00079499 File Offset: 0x00077699
		public virtual ExchangeSettings CreateSettings(IComponent owner)
		{
			return this.helper.CreateSettings(owner);
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x000794A7 File Offset: 0x000776A7
		protected override void OnLoadCustomData(AsyncStatus status, byte[] customData)
		{
			this.helper.OnLoadCustomData(status, customData);
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x000794B8 File Offset: 0x000776B8
		protected override byte[] OnSaveCustomData(SyncStatus status)
		{
			return this.helper.OnSaveCustomData(status);
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x000794D3 File Offset: 0x000776D3
		public override string ToString()
		{
			return this.helper.ToString();
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x000794E0 File Offset: 0x000776E0
		public int RegisterIcon(string name, Icon icon)
		{
			return this.helper.RegisterIcon(name, icon);
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x000794EF File Offset: 0x000776EF
		public ScopeNodeCollection ScopeNodeCollection
		{
			get
			{
				return base.PrimaryNode.Children;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x000794FC File Offset: 0x000776FC
		public virtual string SnapInGuidString
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x000794FF File Offset: 0x000776FF
		// (set) Token: 0x06001BC3 RID: 7107 RVA: 0x00079517 File Offset: 0x00077717
		public string RootNodeDisplayName
		{
			get
			{
				return base.PrimaryNode.Children[0].DisplayName;
			}
			set
			{
				base.PrimaryNode.Children[0].DisplayName = value;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x00079530 File Offset: 0x00077730
		public virtual Icon RootNodeIcon
		{
			get
			{
				return (base.PrimaryNode.Children[0] as ExchangeScopeNode).Icon;
			}
		}

		// Token: 0x04000A3B RID: 2619
		private ExchangeSnapInHelper helper;
	}
}
