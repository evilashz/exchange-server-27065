using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000127 RID: 295
	[SettingsProvider(typeof(ExchangeSettingsProvider))]
	public partial class ExchangeSettings : ApplicationSettingsBase
	{
	}
}
