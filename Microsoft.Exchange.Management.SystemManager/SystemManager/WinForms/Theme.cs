using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Exchange.Management.SystemManager.WinForms.Properties;
using Microsoft.ManagementGUI.Commands;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200020E RID: 526
	public static class Theme
	{
		// Token: 0x060017D2 RID: 6098 RVA: 0x00064800 File Offset: 0x00062A00
		static Theme()
		{
			SystemEvents.DisplaySettingsChanged += Theme.OnVisualSettingsChanged;
			SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(Theme.OnVisualSettingsChanged);
			SystemEvents.SessionSwitch += new SessionSwitchEventHandler(Theme.OnVisualSettingsChanged);
			Settings.Default.PropertyChanged += new PropertyChangedEventHandler(Theme.OnVisualSettingsChanged);
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060017D3 RID: 6099 RVA: 0x00064856 File Offset: 0x00062A56
		public static bool UseVisualEffects
		{
			get
			{
				return Settings.Default.UseVisualEffects;
			}
		}

		// Token: 0x1400009E RID: 158
		// (add) Token: 0x060017D4 RID: 6100 RVA: 0x00064864 File Offset: 0x00062A64
		// (remove) Token: 0x060017D5 RID: 6101 RVA: 0x00064898 File Offset: 0x00062A98
		public static event EventHandler UseVisualEffectsChanged;

		// Token: 0x060017D6 RID: 6102 RVA: 0x000648CC File Offset: 0x00062ACC
		private static void OnVisualSettingsChanged(object sender, EventArgs e)
		{
			EventHandler useVisualEffectsChanged = Theme.UseVisualEffectsChanged;
			if (useVisualEffectsChanged != null)
			{
				useVisualEffectsChanged(null, EventArgs.Empty);
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060017D7 RID: 6103 RVA: 0x000648EE File Offset: 0x00062AEE
		public static Command VisualEffectsCommands
		{
			get
			{
				return Settings.Default.VisualEffectsCommands;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x000648FA File Offset: 0x00062AFA
		public static ControlStyles UserPaintStyle
		{
			get
			{
				return ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer;
			}
		}
	}
}
