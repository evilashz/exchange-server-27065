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
		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002A33F File Offset: 0x0002853F
		public ExchangeSettings(IComponent owner) : base(owner)
		{
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002A353 File Offset: 0x00028553
		public void DoBeginInit()
		{
			this.OnBeginInit(EventArgs.Empty);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002A360 File Offset: 0x00028560
		protected virtual void OnBeginInit(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)this.Events[ExchangeSettings.EventBeginInit];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06000BB5 RID: 2997 RVA: 0x0002A38E File Offset: 0x0002858E
		// (remove) Token: 0x06000BB6 RID: 2998 RVA: 0x0002A3A1 File Offset: 0x000285A1
		public event EventHandler BeginInit
		{
			add
			{
				this.Events.AddHandler(ExchangeSettings.EventBeginInit, value);
			}
			remove
			{
				this.Events.RemoveHandler(ExchangeSettings.EventBeginInit, value);
			}
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002A3B4 File Offset: 0x000285B4
		public void DoEndInit(bool cancelAutoRefresh)
		{
			this.OnEndInit(new CancelEventArgs(cancelAutoRefresh));
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002A3C4 File Offset: 0x000285C4
		protected virtual void OnEndInit(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)this.Events[ExchangeSettings.EventEndInit];
			if (cancelEventHandler != null)
			{
				cancelEventHandler(this, e);
			}
		}

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06000BB9 RID: 3001 RVA: 0x0002A3F2 File Offset: 0x000285F2
		// (remove) Token: 0x06000BBA RID: 3002 RVA: 0x0002A405 File Offset: 0x00028605
		public event CancelEventHandler EndInit
		{
			add
			{
				this.Events.AddHandler(ExchangeSettings.EventEndInit, value);
			}
			remove
			{
				this.Events.RemoveHandler(ExchangeSettings.EventEndInit, value);
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0002A418 File Offset: 0x00028618
		protected EventHandlerList Events
		{
			get
			{
				return this.events;
			}
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0002A420 File Offset: 0x00028620
		public void UpdateProviders(ISettingsProviderService provSvc)
		{
			if (provSvc != null)
			{
				foreach (object obj in this.Properties)
				{
					SettingsProperty settingsProperty = (SettingsProperty)obj;
					SettingsProvider settingsProvider = provSvc.GetSettingsProvider(settingsProperty);
					if (settingsProvider != null)
					{
						settingsProperty.Provider = settingsProvider;
					}
				}
				this.Providers.Clear();
				foreach (object obj2 in this.Properties)
				{
					SettingsProperty settingsProperty2 = (SettingsProperty)obj2;
					if (this.Providers[settingsProperty2.Provider.Name] == null)
					{
						this.Providers.Add(settingsProperty2.Provider);
					}
				}
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x0002A50C File Offset: 0x0002870C
		// (set) Token: 0x06000BBE RID: 3006 RVA: 0x0002A514 File Offset: 0x00028714
		public string InstanceDisplayName { get; set; }

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06000BBF RID: 3007 RVA: 0x0002A51D File Offset: 0x0002871D
		// (remove) Token: 0x06000BC0 RID: 3008 RVA: 0x0002A530 File Offset: 0x00028730
		public event CustomDataRefreshEventHandler RefreshResultPane
		{
			add
			{
				SynchronizedDelegate.Combine(this.Events, ExchangeSettings.EventRefreshResultPane, value);
			}
			remove
			{
				SynchronizedDelegate.Remove(this.Events, ExchangeSettings.EventRefreshResultPane, value);
			}
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0002A544 File Offset: 0x00028744
		public void RaiseRefreshResultPane(CustomDataRefreshEventArgs e)
		{
			EventHandler eventHandler = (EventHandler)this.Events[ExchangeSettings.EventRefreshResultPane];
			if (eventHandler != null)
			{
				eventHandler.DynamicInvoke(new object[]
				{
					this,
					e
				});
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0002A581 File Offset: 0x00028781
		// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x0002A5A2 File Offset: 0x000287A2
		[UserScopedSetting]
		public bool IsCommandLoggingEnabled
		{
			get
			{
				return this[ExchangeSettings.isCommandLoggingEnabled] == null || (bool)this[ExchangeSettings.isCommandLoggingEnabled];
			}
			set
			{
				if (!value.Equals(this[ExchangeSettings.isCommandLoggingEnabled]))
				{
					this[ExchangeSettings.isCommandLoggingEnabled] = value;
				}
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x0002A5C9 File Offset: 0x000287C9
		// (set) Token: 0x06000BC5 RID: 3013 RVA: 0x0002A5EE File Offset: 0x000287EE
		[UserScopedSetting]
		public int MaximumRecordCount
		{
			get
			{
				if (this[ExchangeSettings.maximumRecordCount] == null)
				{
					return CommandLoggingSession.DefaultMaximumRecordCount;
				}
				return (int)this[ExchangeSettings.maximumRecordCount];
			}
			set
			{
				if (!value.Equals(this[ExchangeSettings.maximumRecordCount]))
				{
					this[ExchangeSettings.maximumRecordCount] = value;
				}
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0002A618 File Offset: 0x00028818
		// (set) Token: 0x06000BC7 RID: 3015 RVA: 0x0002A669 File Offset: 0x00028869
		[UserScopedSetting]
		public Point CommandLoggingDialogLocation
		{
			get
			{
				Point point = (this[ExchangeSettings.commandLoggingDialogLocation] != null) ? ((Point)this[ExchangeSettings.commandLoggingDialogLocation]) : CommandLoggingDialog.DefaultDialogLocation;
				if (!Screen.PrimaryScreen.WorkingArea.Contains(point))
				{
					point = new Point(0, 0);
				}
				return point;
			}
			set
			{
				if (!object.Equals(value, this[ExchangeSettings.commandLoggingDialogLocation]))
				{
					this[ExchangeSettings.commandLoggingDialogLocation] = value;
				}
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x0002A694 File Offset: 0x00028894
		// (set) Token: 0x06000BC9 RID: 3017 RVA: 0x0002A71A File Offset: 0x0002891A
		[UserScopedSetting]
		public Size CommandLoggingDialogSize
		{
			get
			{
				Size size = (this[ExchangeSettings.commandLoggingDialogSize] != null) ? ((Size)this[ExchangeSettings.commandLoggingDialogSize]) : CommandLoggingDialog.DefaultDialogSize;
				int width = Screen.PrimaryScreen.WorkingArea.Width;
				int height = Screen.PrimaryScreen.WorkingArea.Height;
				return new Size((size.Width > width) ? width : size.Width, (size.Height > height) ? height : size.Height);
			}
			set
			{
				if (!object.Equals(value, this[ExchangeSettings.commandLoggingDialogSize]))
				{
					this[ExchangeSettings.commandLoggingDialogSize] = value;
				}
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x0002A745 File Offset: 0x00028945
		// (set) Token: 0x06000BCB RID: 3019 RVA: 0x0002A76A File Offset: 0x0002896A
		[UserScopedSetting]
		public float CommandLoggingDialogSplitterDistanceScale
		{
			get
			{
				if (this[ExchangeSettings.commandLoggingDialogSplitterDistanceScale] == null)
				{
					return CommandLoggingDialog.DefaultSplitterDistanceScale;
				}
				return (float)this[ExchangeSettings.commandLoggingDialogSplitterDistanceScale];
			}
			set
			{
				if (value != this.CommandLoggingDialogSplitterDistanceScale)
				{
					this[ExchangeSettings.commandLoggingDialogSplitterDistanceScale] = value;
				}
			}
		}

		// Token: 0x040004CA RID: 1226
		private static readonly object EventBeginInit = new object();

		// Token: 0x040004CB RID: 1227
		private static readonly object EventEndInit = new object();

		// Token: 0x040004CC RID: 1228
		private EventHandlerList events = new EventHandlerList();

		// Token: 0x040004CD RID: 1229
		private static readonly object EventRefreshResultPane = new object();

		// Token: 0x040004CE RID: 1230
		private static string isCommandLoggingEnabled = "IsCommandLoggingEnabled";

		// Token: 0x040004CF RID: 1231
		private static string maximumRecordCount = "MaximumRecordCount";

		// Token: 0x040004D0 RID: 1232
		private static string commandLoggingDialogLocation = "CommandLoggingDialogLocation";

		// Token: 0x040004D1 RID: 1233
		private static string commandLoggingDialogSize = "CommandLoggingDialogSize";

		// Token: 0x040004D2 RID: 1234
		private static string commandLoggingDialogSplitterDistanceScale = "CommandLoggingDialogSplitterDistanceScale";
	}
}
