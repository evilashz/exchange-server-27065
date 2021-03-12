using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Management.Automation;
using System.Management.Automation.Remoting;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000123 RID: 291
	public class UIService : IUIService
	{
		// Token: 0x06000B81 RID: 2945 RVA: 0x000291F1 File Offset: 0x000273F1
		public UIService(IWin32Window parentWindow)
		{
			this.parentWindow = parentWindow;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00029200 File Offset: 0x00027400
		public bool CanShowComponentEditor(object component)
		{
			ComponentEditor componentEditor = (ComponentEditor)TypeDescriptor.GetEditor(component, typeof(ComponentEditor));
			return null != componentEditor;
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0002922A File Offset: 0x0002742A
		public IWin32Window GetDialogOwnerWindow()
		{
			return this.parentWindow;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00029232 File Offset: 0x00027432
		public virtual void SetUIDirty()
		{
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00029234 File Offset: 0x00027434
		public bool ShowComponentEditor(object component, IWin32Window parent)
		{
			bool result = false;
			ComponentEditor componentEditor = (ComponentEditor)TypeDescriptor.GetEditor(component, typeof(ComponentEditor));
			if (componentEditor != null)
			{
				WindowsFormsComponentEditor windowsFormsComponentEditor = componentEditor as WindowsFormsComponentEditor;
				if (windowsFormsComponentEditor != null)
				{
					if (parent == null)
					{
						parent = this.GetDialogOwnerWindow();
					}
					result = windowsFormsComponentEditor.EditComponent(component, parent);
				}
				else
				{
					result = componentEditor.EditComponent(component);
				}
			}
			return result;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00029285 File Offset: 0x00027485
		protected virtual DialogResult OnShowDialog(Form form)
		{
			return form.ShowDialog(this.GetDialogOwnerWindow());
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00029294 File Offset: 0x00027494
		public DialogResult ShowDialog(Form form)
		{
			IContainer container = null;
			ContainerControl containerControl = this.GetDialogOwnerWindow() as ContainerControl;
			if (containerControl != null)
			{
				container = containerControl.Container;
				if (container == null && containerControl.ParentForm != null)
				{
					container = containerControl.ParentForm.Container;
				}
				if (container != null)
				{
					container.Add(form, form.Name + form.GetHashCode().ToString());
				}
			}
			if (string.IsNullOrEmpty(form.Text))
			{
				form.Text = UIService.DefaultCaption;
			}
			Control focusedControlOnPropertyPage = this.GetFocusedControlOnPropertyPage();
			DialogResult result;
			try
			{
				result = this.OnShowDialog(form);
			}
			finally
			{
				if (container != null)
				{
					container.Remove(form);
				}
				if (focusedControlOnPropertyPage != null)
				{
					focusedControlOnPropertyPage.Focus();
				}
			}
			return result;
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00029344 File Offset: 0x00027544
		private Control GetFocusedControlOnPropertyPage()
		{
			Control control = this.GetDialogOwnerWindow() as Control;
			if (control != null && control.Parent != null)
			{
				while (control.Parent.Parent != null)
				{
					control = control.Parent;
				}
				if (control is ExchangePropertyPageControl)
				{
					return ((ExchangePropertyPageControl)control).FocusedControl;
				}
			}
			return null;
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00029394 File Offset: 0x00027594
		public void ShowError(Exception ex, string message)
		{
			if (ex is ADServerSettingsChangedException || ex.InnerException is ADServerSettingsChangedException)
			{
				ADServerSettingsSingleton.GetInstance().ADServerSettings.SetDefaultSettings();
				this.ShowError(Strings.ADServerSettingsChangedException);
				return;
			}
			if (ex is CmdletInvocationException || DataAccessHelper.IsDataAccessKnownException(ex))
			{
				this.ShowError(ex.Message);
				return;
			}
			if (ExceptionHelper.IsWellknownCommandExecutionException(ex))
			{
				this.ShowError(ex.InnerException.Message);
				return;
			}
			if (ex is VersionMismatchException)
			{
				this.ShowError(ex.Message);
				return;
			}
			if (ex is SupportedVersionListFormatException)
			{
				this.ShowError(ex.Message);
				return;
			}
			if (ex is PooledConnectionOpenTimeoutException)
			{
				this.ShowError(ex.Message);
				return;
			}
			if (ex is PSRemotingTransportException)
			{
				this.ShowError(ex.Message);
				return;
			}
			if (ex is PSRemotingDataStructureException)
			{
				this.ShowError(ex.Message);
				return;
			}
			if (ex is PSInvalidOperationException)
			{
				this.ShowError(ex.Message);
				return;
			}
			if (ex is OperationCanceledException)
			{
				this.ShowError(ex.Message);
				return;
			}
			using (ExceptionDialog exceptionDialog = new ExceptionDialog())
			{
				if (ExceptionHelper.IsWellknownExceptionFromServer(ex.InnerException))
				{
					exceptionDialog.Exception = ex.InnerException;
				}
				else
				{
					exceptionDialog.Exception = ex;
				}
				if (!string.IsNullOrEmpty(message))
				{
					exceptionDialog.Message = string.Format("{0}\r\n\r\n{1}", message, exceptionDialog.Message);
				}
				((IUIService)this).ShowDialog(exceptionDialog);
			}
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0002950C File Offset: 0x0002770C
		public void ShowError(Exception ex)
		{
			((IUIService)this).ShowError(ex, string.Empty);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0002951C File Offset: 0x0002771C
		internal static bool ShowError(string errorMessage, string warningMessage, IList<WorkUnit> errors, IUIService uiService)
		{
			if (errors.Count > 0)
			{
				bool flag = true;
				for (int i = 0; i < errors.Count; i++)
				{
					if (errors[i].Errors.Count > 0)
					{
						flag = false;
						break;
					}
				}
				using (TridentErrorDialog tridentErrorDialog = new TridentErrorDialog())
				{
					tridentErrorDialog.Message = (flag ? warningMessage : errorMessage);
					tridentErrorDialog.Errors = errors;
					tridentErrorDialog.IsWarningOnly = flag;
					uiService.ShowDialog(tridentErrorDialog);
				}
				return !flag;
			}
			return false;
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x000295A8 File Offset: 0x000277A8
		public static string DefaultCaption
		{
			get
			{
				return Strings.MicrosoftExchange;
			}
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x000295B4 File Offset: 0x000277B4
		protected static MessageBoxDefaultButton GetDefaultButton(MessageBoxButtons buttons)
		{
			MessageBoxDefaultButton result;
			switch (buttons)
			{
			default:
				result = MessageBoxDefaultButton.Button1;
				break;
			case MessageBoxButtons.OKCancel:
			case MessageBoxButtons.AbortRetryIgnore:
			case MessageBoxButtons.YesNo:
				result = MessageBoxDefaultButton.Button2;
				break;
			}
			return result;
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x000295EC File Offset: 0x000277EC
		protected virtual DialogResult MessageBox(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			DialogResult result;
			using (MessageBoxDialog messageBoxDialog = new MessageBoxDialog(message, caption, buttons, icon, UIService.GetDefaultButton(buttons)))
			{
				result = this.ShowDialog(messageBoxDialog);
			}
			return result;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00029630 File Offset: 0x00027830
		public void ShowError(string message)
		{
			this.MessageBox(message, UIService.DefaultCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00029644 File Offset: 0x00027844
		public DialogResult ShowMessage(string message, string caption, MessageBoxButtons buttons)
		{
			MessageBoxIcon icon = (buttons == MessageBoxButtons.OK) ? MessageBoxIcon.Asterisk : MessageBoxIcon.Exclamation;
			return this.MessageBox(message, caption, buttons, icon);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00029665 File Offset: 0x00027865
		public void ShowMessage(string message, string caption)
		{
			this.MessageBox(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00029673 File Offset: 0x00027873
		public void ShowMessage(string message)
		{
			this.MessageBox(message, UIService.DefaultCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00029685 File Offset: 0x00027885
		public virtual bool ShowToolWindow(Guid toolWindow)
		{
			return false;
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00029688 File Offset: 0x00027888
		public virtual IDictionary Styles
		{
			get
			{
				if (this.styles == null)
				{
					this.styles = new Hashtable(2);
					this.styles["DialogFont"] = SystemFonts.DialogFont;
					this.styles["HighlightColor"] = SystemColors.Highlight;
				}
				return this.styles;
			}
		}

		// Token: 0x040004C3 RID: 1219
		private IWin32Window parentWindow;

		// Token: 0x040004C4 RID: 1220
		private Hashtable styles;
	}
}
