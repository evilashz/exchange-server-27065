using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000223 RID: 547
	public partial class SmtpProxyAddressTemplateDialog : ProxyAddressDialog
	{
		// Token: 0x06001912 RID: 6418 RVA: 0x0006C424 File Offset: 0x0006A624
		public SmtpProxyAddressTemplateDialog()
		{
			this.InitializeComponent();
			this.Text = Strings.SmtpEmailAddressCaption;
			this.localPartCheckBox.Text = Strings.EmailAddressLocalPart;
			this.aliasRadioButton.Text = Strings.UseAlias;
			this.firstNameLastNameRadioButton.Text = Strings.FirstNameLastName;
			this.firstNameInitialLastNameRadioButton.Text = Strings.FirstNameInitialLastName;
			this.firstNameLastNameInitialRadioButton.Text = Strings.FirstNameLastNameInitial;
			this.lastNameFirstNameRadioButton.Text = Strings.LastNameFirstName;
			this.lastNameInitialFirstNameRadioButton.Text = Strings.LastNameInitialFirstName;
			this.lastNameFirstNameInitialRadioButton.Text = Strings.LastNameFirstNameInitial;
			this.acceptedDomainRadioButton.Text = Strings.AcceptedDomainRadioButtonText;
			this.customDomainRadioButton.Text = Strings.CustomDomainRadioButtonText;
			this.allOptionRadioButtons = new AutoHeightRadioButton[]
			{
				this.aliasRadioButton,
				this.firstNameLastNameRadioButton,
				this.firstNameInitialLastNameRadioButton,
				this.firstNameLastNameInitialRadioButton,
				this.lastNameFirstNameRadioButton,
				this.lastNameInitialFirstNameRadioButton,
				this.lastNameFirstNameInitialRadioButton
			};
			this.currentOption = this.aliasRadioButton;
			this.currentOption.Checked = true;
			foreach (AutoHeightRadioButton autoHeightRadioButton in this.allOptionRadioButtons)
			{
				autoHeightRadioButton.CheckedChanged += this.option_CheckedChanged;
			}
			this.localPartCheckBox.Checked = true;
			this.localPartCheckBox.CheckedChanged += this.localPartCheckBox_CheckedChanged;
			this.acceptedDomainPickerLauncherTextBox.Picker = new AutomatedObjectPicker(new AcceptedDomainConfigurable());
			((AutomatedObjectPicker)this.acceptedDomainPickerLauncherTextBox.Picker).InputValue("ExcludeExternalRelay", true);
			this.acceptedDomainPickerLauncherTextBox.ValueMember = "DomainName";
			this.acceptedDomainPickerLauncherTextBox.ValueMemberConverter = new SmtpDomainWithSubdomainsToDomainNameConverter();
			this.acceptedDomainPickerLauncherTextBox.SelectedValueChanged += delegate(object param0, EventArgs param1)
			{
				base.UpdateError();
				this.MakeDirty();
			};
			this.acceptedDomainPickerLauncherTextBox.DataBindings.Add("Enabled", this.acceptedDomainRadioButton, "Checked", true, DataSourceUpdateMode.OnPropertyChanged);
			this.customDomainTextBox.DataBindings.Add("Enabled", this.customDomainRadioButton, "Checked", true, DataSourceUpdateMode.OnPropertyChanged);
			this.customDomainTextBox.TextChanged += delegate(object param0, EventArgs param1)
			{
				base.UpdateError();
				this.MakeDirty();
			};
			this.customDomainRadioButton.CheckedChanged += delegate(object param0, EventArgs param1)
			{
				this.clearingError = true;
				base.UpdateError();
				this.MakeDirty();
				this.clearingError = false;
			};
			base.DataBindings.Add("TemplateString", base.ContentPage.BindingSource, "Address");
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001914 RID: 6420 RVA: 0x0006D416 File Offset: 0x0006B616
		// (set) Token: 0x06001915 RID: 6421 RVA: 0x0006D43C File Offset: 0x0006B63C
		[DefaultValue("")]
		public string Domain
		{
			get
			{
				if (!this.acceptedDomainRadioButton.Checked)
				{
					return this.customDomainTextBox.Text;
				}
				return this.acceptedDomainPickerLauncherTextBox.Text;
			}
			set
			{
				if (this.acceptedDomainRadioButton.Checked)
				{
					this.acceptedDomainPickerLauncherTextBox.Text = value;
					return;
				}
				this.customDomainTextBox.Text = value;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001916 RID: 6422 RVA: 0x0006D464 File Offset: 0x0006B664
		// (set) Token: 0x06001917 RID: 6423 RVA: 0x0006D46C File Offset: 0x0006B66C
		[DefaultValue("")]
		public string TemplateString
		{
			get
			{
				return this.templateString;
			}
			set
			{
				if (this.templateString != value)
				{
					this.templateString = value;
					this.UpdateControl();
				}
			}
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x0006D489 File Offset: 0x0006B689
		protected override void OnClosing(CancelEventArgs e)
		{
			if (string.IsNullOrEmpty(this.TemplateString))
			{
				this.MakeDirty();
			}
			base.OnClosing(e);
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0006D4A8 File Offset: 0x0006B6A8
		protected void UpdateControl()
		{
			if (!string.IsNullOrEmpty(this.templateString))
			{
				this.updatingControl = true;
				string text = this.templateString;
				int num = text.IndexOf('@');
				if (num < 0)
				{
					this.Domain = text;
					this.localPartCheckBox.Checked = false;
				}
				else
				{
					this.Domain = text.Substring(num + 1);
					int i;
					for (i = 0; i < this.localParts.Length; i++)
					{
						if (this.localParts[i].Length < text.Length && text.StartsWith(this.localParts[i], StringComparison.InvariantCultureIgnoreCase) && text[this.localParts[i].Length] == '@')
						{
							this.localPartCheckBox.Checked = true;
							this.allOptionRadioButtons[i].Checked = true;
							break;
						}
					}
					if (i == this.localParts.Length)
					{
						this.localPartCheckBox.Checked = false;
						this.customLocalPart = text.Substring(0, num);
					}
				}
				this.updatingControl = false;
			}
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x0006D5A0 File Offset: 0x0006B7A0
		protected void MakeDirty()
		{
			if (!this.updatingControl)
			{
				this.TemplateString = (this.localPartCheckBox.Checked ? this.localParts[this.RadioButtonIndex(this.currentOption)] : this.customLocalPart) + "@" + this.Domain;
			}
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x0006D5F4 File Offset: 0x0006B7F4
		private void option_CheckedChanged(object sender, EventArgs e)
		{
			AutoHeightRadioButton autoHeightRadioButton = (AutoHeightRadioButton)sender;
			if (autoHeightRadioButton.Checked)
			{
				this.currentOption = autoHeightRadioButton;
				this.MakeDirty();
			}
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x0006D61D File Offset: 0x0006B81D
		private int RadioButtonIndex(AutoHeightRadioButton ro)
		{
			return Array.IndexOf<AutoHeightRadioButton>(this.allOptionRadioButtons, ro);
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x0006D62C File Offset: 0x0006B82C
		private void localPartCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			foreach (AutoHeightRadioButton autoHeightRadioButton in this.allOptionRadioButtons)
			{
				autoHeightRadioButton.Enabled = checkBox.Checked;
			}
			this.MakeDirty();
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x0006D66B File Offset: 0x0006B86B
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			base.ContentPage.InputValidationProvider.SetUIValidationEnabled(base.ContentPage, true);
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x0006D68C File Offset: 0x0006B88C
		protected override UIValidationError[] GetValidationErrors()
		{
			UIValidationError uivalidationError = null;
			if (!this.clearingError)
			{
				if (this.customDomainRadioButton.Checked)
				{
					if (string.IsNullOrEmpty(this.customDomainTextBox.Text))
					{
						uivalidationError = new UIValidationError(Strings.ErrorDomainPartCannotBeEmpty, this.customDomainTextBox);
						goto IL_8F;
					}
					try
					{
						SmtpDomain.Parse(this.customDomainTextBox.Text);
						goto IL_8F;
					}
					catch (FormatException ex)
					{
						uivalidationError = new UIValidationError(new LocalizedString(ex.Message), this.customDomainTextBox);
						goto IL_8F;
					}
				}
				if (string.IsNullOrEmpty(this.acceptedDomainPickerLauncherTextBox.Text))
				{
					uivalidationError = new UIValidationError(Strings.ErrorDomainPartCannotBeEmpty, this.acceptedDomainPickerLauncherTextBox);
				}
			}
			IL_8F:
			if (uivalidationError != null)
			{
				return new UIValidationError[]
				{
					uivalidationError
				};
			}
			return UIValidationError.None;
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001920 RID: 6432 RVA: 0x0006D750 File Offset: 0x0006B950
		protected override ProxyAddressBaseDataHandler DataHandler
		{
			get
			{
				if (this.dataHandler == null)
				{
					this.dataHandler = new ProxyAddressTemplateDataHandler();
					this.dataHandler.Prefix = ProxyAddressPrefix.Smtp.ToString();
					try
					{
						this.dataHandler.Address = "%m@" + NativeHelpers.GetDomainName();
					}
					catch (CannotGetDomainInfoException)
					{
					}
				}
				return this.dataHandler;
			}
		}

		// Token: 0x0400096D RID: 2413
		private AutoHeightRadioButton[] allOptionRadioButtons;

		// Token: 0x0400096E RID: 2414
		private AutoHeightRadioButton currentOption;

		// Token: 0x0400096F RID: 2415
		private string[] localParts = new string[]
		{
			"%m",
			"%g.%s",
			"%1g%s",
			"%g%1s",
			"%s.%g",
			"%1s%g",
			"%s%1g"
		};

		// Token: 0x04000970 RID: 2416
		private string customLocalPart = string.Empty;

		// Token: 0x04000971 RID: 2417
		private bool updatingControl;

		// Token: 0x04000972 RID: 2418
		private bool clearingError;

		// Token: 0x04000973 RID: 2419
		private string templateString = string.Empty;

		// Token: 0x04000974 RID: 2420
		private ProxyAddressTemplateDataHandler dataHandler;
	}
}
