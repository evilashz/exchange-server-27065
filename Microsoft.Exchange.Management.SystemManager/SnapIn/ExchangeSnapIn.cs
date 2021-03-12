using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.ManagementConsole;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x0200028C RID: 652
	public abstract class ExchangeSnapIn : SnapIn, IExchangeSnapIn, IServiceProvider
	{
		// Token: 0x06001B98 RID: 7064 RVA: 0x0007919C File Offset: 0x0007739C
		public ExchangeSnapIn()
		{
			this.helper = new ExchangeSnapInHelper(this, this);
			this.helper.InitializeSettingProvider();
			PSConnectionInfoSingleton.GetInstance().Enabled = true;
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x000791C7 File Offset: 0x000773C7
		object IServiceProvider.GetService(Type serviceType)
		{
			return this.helper.Services.GetService(serviceType);
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x000791DA File Offset: 0x000773DA
		protected override void OnInitialize()
		{
			this.helper.OnInitialize();
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x000791E7 File Offset: 0x000773E7
		public virtual void Initialize(IProgressProvider progressProvider)
		{
			this.helper.Initialize(progressProvider);
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x000791F5 File Offset: 0x000773F5
		protected override void OnShutdown(AsyncStatus status)
		{
			ManagementGuiSqmSession.Instance.Close();
			this.helper.Shutdown(status);
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x0007920D File Offset: 0x0007740D
		public IContainer Components
		{
			get
			{
				return this.helper.Components;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001B9E RID: 7070 RVA: 0x0007921A File Offset: 0x0007741A
		public IUIService ShellUI
		{
			get
			{
				return this.helper.ShellUI;
			}
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x00079227 File Offset: 0x00077427
		public DialogResult ShowDialog(CommonDialog dialog)
		{
			return this.helper.ShowDialog(dialog);
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x00079235 File Offset: 0x00077435
		public virtual string InstanceDisplayName
		{
			get
			{
				return this.helper.InstanceDisplayName;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x00079242 File Offset: 0x00077442
		public ExchangeSettings Settings
		{
			get
			{
				return this.helper.Settings;
			}
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0007924F File Offset: 0x0007744F
		public virtual ExchangeSettings CreateSettings(IComponent owner)
		{
			return this.helper.CreateSettings(owner);
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x0007925D File Offset: 0x0007745D
		protected override void OnLoadCustomData(AsyncStatus status, byte[] customData)
		{
			this.helper.OnLoadCustomData(status, customData);
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x0007926C File Offset: 0x0007746C
		protected override byte[] OnSaveCustomData(SyncStatus status)
		{
			return this.helper.OnSaveCustomData(status);
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x00079287 File Offset: 0x00077487
		public override string ToString()
		{
			return this.helper.ToString();
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x00079294 File Offset: 0x00077494
		public int RegisterIcon(string name, Icon icon)
		{
			return this.helper.RegisterIcon(name, icon);
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x000792A3 File Offset: 0x000774A3
		public virtual ScopeNodeCollection ScopeNodeCollection
		{
			get
			{
				if (this.scopeNodeCollection == null)
				{
					this.scopeNodeCollection = new ScopeNodeCollection();
					this.scopeNodeCollection.Add(base.RootNode);
				}
				return this.scopeNodeCollection;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x000792D0 File Offset: 0x000774D0
		public virtual string SnapInGuidString
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x000792D3 File Offset: 0x000774D3
		public virtual string RootNodeDisplayName
		{
			get
			{
				return base.RootNode.DisplayName;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x000792E0 File Offset: 0x000774E0
		public virtual Icon RootNodeIcon
		{
			get
			{
				return (base.RootNode as ExchangeScopeNode).Icon;
			}
		}

		// Token: 0x04000A39 RID: 2617
		private ExchangeSnapInHelper helper;

		// Token: 0x04000A3A RID: 2618
		private ScopeNodeCollection scopeNodeCollection;
	}
}
