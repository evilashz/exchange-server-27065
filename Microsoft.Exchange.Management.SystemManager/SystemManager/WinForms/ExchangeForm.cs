using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.Sqm;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.Services;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000118 RID: 280
	public partial class ExchangeForm : Form, IServiceProvider
	{
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x00025717 File Offset: 0x00023917
		// (set) Token: 0x06000A90 RID: 2704 RVA: 0x0002571F File Offset: 0x0002391F
		[DefaultValue(true)]
		public bool NeedRestoreOwnerAfterModeless
		{
			get
			{
				return this.needRestoreOwnerAfterModeless;
			}
			set
			{
				this.needRestoreOwnerAfterModeless = value;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x00025737 File Offset: 0x00023937
		// (set) Token: 0x06000A93 RID: 2707 RVA: 0x00025748 File Offset: 0x00023948
		public override RightToLeft RightToLeft
		{
			get
			{
				if (!LayoutHelper.CultureInfoIsRightToLeft)
				{
					return base.RightToLeft;
				}
				return RightToLeft.Yes;
			}
			set
			{
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x0002574A File Offset: 0x0002394A
		// (set) Token: 0x06000A95 RID: 2709 RVA: 0x00025752 File Offset: 0x00023952
		public override bool RightToLeftLayout
		{
			get
			{
				return LayoutHelper.IsRightToLeft(this);
			}
			set
			{
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x00025754 File Offset: 0x00023954
		public IUIService ShellUI
		{
			get
			{
				IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
				if (iuiservice == null)
				{
					iuiservice = this.CreateUIService();
				}
				return iuiservice;
			}
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00025782 File Offset: 0x00023982
		protected virtual IUIService CreateUIService()
		{
			return new UIService(this);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0002578A File Offset: 0x0002398A
		object IServiceProvider.GetService(Type serviceType)
		{
			return this.GetService(serviceType);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00025793 File Offset: 0x00023993
		public void ShowError(string message)
		{
			this.ShellUI.ShowError(message);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x000257A1 File Offset: 0x000239A1
		public void ShowMessage(string message)
		{
			this.ShellUI.ShowMessage(message);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x000257AF File Offset: 0x000239AF
		public DialogResult ShowMessage(string message, MessageBoxButtons buttons)
		{
			return this.ShellUI.ShowMessage(message, UIService.DefaultCaption, buttons);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x000257C4 File Offset: 0x000239C4
		public IProgress CreateProgress(LocalizedString operationName)
		{
			IProgressProvider progressProvider = (IProgressProvider)this.GetService(typeof(IProgressProvider));
			if (progressProvider != null)
			{
				return progressProvider.CreateProgress(operationName);
			}
			return NullProgress.Value;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x000257FC File Offset: 0x000239FC
		[Obsolete("Use ShowModeless instead")]
		public new void Show()
		{
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x000257FE File Offset: 0x000239FE
		[Obsolete("Use ShowModeless instead")]
		public new void Show(IWin32Window parentWindow)
		{
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00025800 File Offset: 0x00023A00
		public void ShowModeless(IServiceProvider parentProvider)
		{
			this.ShowModeless(parentProvider, null);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0002580C File Offset: 0x00023A0C
		public void ShowModeless(IServiceProvider parentProvider, Control owner)
		{
			ServiceContainer serviceContainer = new ServiceContainer(parentProvider);
			serviceContainer.AddService(typeof(IUIService), new UIService(this));
			ServicedContainer servicedContainer = new ServicedContainer(serviceContainer);
			servicedContainer.Add(this, this.GetHashCode().ToString());
			this.restoreAfterModeless = (owner ?? (parentProvider as Control));
			if (owner == null)
			{
				base.Show();
				return;
			}
			base.Show(owner);
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x00025874 File Offset: 0x00023A74
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x00025890 File Offset: 0x00023A90
		public string HelpTopic
		{
			get
			{
				if (this.helpTopic == null)
				{
					this.helpTopic = this.DefaultHelpTopic;
				}
				return this.helpTopic;
			}
			set
			{
				value = (value ?? "");
				this.helpTopic = value;
			}
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x000258A5 File Offset: 0x00023AA5
		private bool ShouldSerializeHelpTopic()
		{
			return this.HelpTopic != this.DefaultHelpTopic;
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x000258B8 File Offset: 0x00023AB8
		private void ResetHelpTopic()
		{
			this.HelpTopic = this.DefaultHelpTopic;
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x000258C6 File Offset: 0x00023AC6
		protected virtual string DefaultHelpTopic
		{
			get
			{
				return base.GetType().FullName;
			}
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x000258D3 File Offset: 0x00023AD3
		protected override void OnHelpRequested(HelpEventArgs hevent)
		{
			if (!hevent.Handled && !string.IsNullOrEmpty(this.HelpTopic))
			{
				ExchangeHelpService.ShowHelpFromHelpTopicId(this, this.HelpTopic);
			}
			hevent.Handled = true;
			base.OnHelpRequested(hevent);
		}

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06000AA7 RID: 2727 RVA: 0x00025904 File Offset: 0x00023B04
		// (remove) Token: 0x06000AA8 RID: 2728 RVA: 0x00025938 File Offset: 0x00023B38
		public static event EventHandler Test_FormShown;

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0002596C File Offset: 0x00023B6C
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			if (ExchangeForm.Test_FormShown != null)
			{
				ExchangeForm.Test_FormShown(this, EventArgs.Empty);
			}
			if (ManagementGuiSqmSession.Instance.Enabled)
			{
				ManagementGuiSqmSession.Instance.AddToStreamDataPoint(SqmDataID.DATAID_EMC_GUI_ACTION, new object[]
				{
					3U,
					base.GetType().Name
				});
			}
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x000259D1 File Offset: 0x00023BD1
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (this.ExchangeFormOwner != null)
			{
				this.ExchangeFormOwner.OnExchangeFormLoad(this);
			}
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000259EE File Offset: 0x00023BEE
		protected override void OnHandleCreated(EventArgs e)
		{
			ExchangeForm.AddToOpenForms(this);
			base.OnHandleCreated(e);
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x000259FD File Offset: 0x00023BFD
		private IExchangeFormOwner ExchangeFormOwner
		{
			get
			{
				return (IExchangeFormOwner)this.GetService(typeof(IExchangeFormOwner));
			}
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00025A14 File Offset: 0x00023C14
		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			base.OnFormClosed(e);
			if (this.ExchangeFormOwner != null)
			{
				this.ExchangeFormOwner.OnExchangeFormClosed(this);
			}
			this.BringRestoredWindowToFront();
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00025A37 File Offset: 0x00023C37
		protected override void OnHandleDestroyed(EventArgs e)
		{
			ExchangeForm.RemoveFromOpenForms(this);
			base.OnHandleDestroyed(e);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00025A46 File Offset: 0x00023C46
		private void BringRestoredWindowToFront()
		{
			if (this.restoreAfterModeless != null && this.NeedRestoreOwnerAfterModeless)
			{
				this.restoreAfterModeless.Focus();
				this.restoreAfterModeless = null;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x00025A6B File Offset: 0x00023C6B
		private static List<ExchangeForm> OpenForms
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			get
			{
				if (ExchangeForm.openForms == null)
				{
					ExchangeForm.openForms = new List<ExchangeForm>();
				}
				return ExchangeForm.openForms;
			}
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00025A83 File Offset: 0x00023C83
		[MethodImpl(MethodImplOptions.Synchronized)]
		private static void AddToOpenForms(ExchangeForm form)
		{
			if (!ExchangeForm.OpenForms.Contains(form))
			{
				ExchangeForm.OpenForms.Add(form);
			}
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00025A9D File Offset: 0x00023C9D
		[MethodImpl(MethodImplOptions.Synchronized)]
		private static void RemoveFromOpenForms(ExchangeForm form)
		{
			if (ExchangeForm.OpenForms.Contains(form))
			{
				ExchangeForm.OpenForms.Remove(form);
			}
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00025AB8 File Offset: 0x00023CB8
		[MethodImpl(MethodImplOptions.Synchronized)]
		private static ExchangeForm GetOpenForm(string formName)
		{
			foreach (ExchangeForm exchangeForm in ExchangeForm.OpenForms)
			{
				if (exchangeForm.Name == formName)
				{
					return exchangeForm;
				}
			}
			return null;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00025B18 File Offset: 0x00023D18
		internal static bool ActivateSingleInstanceForm(string formName)
		{
			ExchangeForm openForm = ExchangeForm.GetOpenForm(formName);
			if (openForm != null)
			{
				openForm.Activate();
			}
			return openForm != null;
		}

		// Token: 0x04000468 RID: 1128
		private bool needRestoreOwnerAfterModeless = true;

		// Token: 0x04000469 RID: 1129
		private string helpTopic;

		// Token: 0x0400046B RID: 1131
		private Control restoreAfterModeless;

		// Token: 0x0400046C RID: 1132
		private static List<ExchangeForm> openForms;
	}
}
