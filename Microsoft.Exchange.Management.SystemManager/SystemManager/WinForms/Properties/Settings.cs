using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.Commands;

namespace Microsoft.Exchange.Management.SystemManager.WinForms.Properties
{
	// Token: 0x0200010B RID: 267
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "8.0.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x0600098D RID: 2445 RVA: 0x00021970 File Offset: 0x0001FB70
		public Settings()
		{
			this.visualEffectsCommand = new Command();
			this.visualEffectsNeverCommand = new Command();
			this.visualEffectsAutomaticCommand = new Command();
			this.visualEffectsCommand.Name = "visualEffectsCommand";
			this.visualEffectsCommand.Text = Strings.VisualEffects;
			this.visualEffectsCommand.Commands.AddRange(new Command[]
			{
				this.visualEffectsNeverCommand,
				this.visualEffectsAutomaticCommand
			});
			this.visualEffectsNeverCommand.Name = "visualEffectsNeverCommand";
			this.visualEffectsNeverCommand.Text = Strings.VisualEffectsNever;
			this.visualEffectsNeverCommand.Execute += delegate(object param0, EventArgs param1)
			{
				this.EnableVisualEffects = "Never";
			};
			this.visualEffectsAutomaticCommand.Name = "visualEffectsAutomaticCommand";
			this.visualEffectsAutomaticCommand.Text = Strings.VisualEffectsAutomatic;
			this.visualEffectsAutomaticCommand.Execute += delegate(object param0, EventArgs param1)
			{
				this.EnableVisualEffects = "Automatic";
			};
			this.OnPropertyChanged(this, new PropertyChangedEventArgs("EnableVisualEffects"));
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00021A8C File Offset: 0x0001FC8C
		protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			this.visualEffectsNeverCommand.Checked = (0 == StringComparer.InvariantCultureIgnoreCase.Compare("Never", this.EnableVisualEffects));
			this.visualEffectsAutomaticCommand.Checked = !this.visualEffectsNeverCommand.Checked;
			base.OnPropertyChanged(sender, e);
			this.Save();
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x00021AE3 File Offset: 0x0001FCE3
		public bool UseVisualEffects
		{
			get
			{
				if (this.visualEffectsNeverCommand.Checked)
				{
					return false;
				}
				if (this.visualEffectsAutomaticCommand.Checked)
				{
					return !SystemInformation.HighContrast;
				}
				return SystemInformation.DragFullWindows || !SystemInformation.TerminalServerSession;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x00021B1D File Offset: 0x0001FD1D
		public Command VisualEffectsCommands
		{
			get
			{
				return this.visualEffectsCommand;
			}
		}

		// Token: 0x04000422 RID: 1058
		private const string Never = "Never";

		// Token: 0x04000423 RID: 1059
		private const string Automatic = "Automatic";

		// Token: 0x04000424 RID: 1060
		private Command visualEffectsCommand;

		// Token: 0x04000425 RID: 1061
		private Command visualEffectsNeverCommand;

		// Token: 0x04000426 RID: 1062
		private Command visualEffectsAutomaticCommand;
	}
}
