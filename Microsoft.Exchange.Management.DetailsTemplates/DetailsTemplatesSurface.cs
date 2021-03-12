using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x02000010 RID: 16
	internal class DetailsTemplatesSurface : DesignSurface, IMessageFilter
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002EAE File Offset: 0x000010AE
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002EB6 File Offset: 0x000010B6
		internal ExchangeForm ExchangeForm
		{
			get
			{
				return this.exchangeForm;
			}
			set
			{
				this.exchangeForm = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002EBF File Offset: 0x000010BF
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002EC7 File Offset: 0x000010C7
		internal DataContext DataContext
		{
			get
			{
				return this.dataContext;
			}
			set
			{
				this.dataContext = value;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002ED0 File Offset: 0x000010D0
		static DetailsTemplatesSurface()
		{
			DetailsTemplatesSurface.controlMapping[typeof(ButtonControl)] = typeof(CustomButton);
			DetailsTemplatesSurface.controlMapping[typeof(EditControl)] = typeof(CustomTextBox);
			DetailsTemplatesSurface.controlMapping[typeof(LabelControl)] = typeof(CustomLabel);
			DetailsTemplatesSurface.controlMapping[typeof(GroupboxControl)] = typeof(CustomGroupBox);
			DetailsTemplatesSurface.controlMapping[typeof(CheckboxControl)] = typeof(CustomCheckBox);
			DetailsTemplatesSurface.controlMapping[typeof(ListboxControl)] = typeof(CustomListBox);
			DetailsTemplatesSurface.controlMapping[typeof(MultiValuedDropdownControl)] = typeof(CustomComboBox);
			DetailsTemplatesSurface.controlMapping[typeof(MultiValuedListboxControl)] = typeof(CustomMultiValuedListBox);
			DetailsTemplatesSurface.keyToCommandId = new Dictionary<Keys, CommandID>();
			DetailsTemplatesSurface.keyToCommandId[Keys.Up] = MenuCommands.KeyMoveUp;
			DetailsTemplatesSurface.keyToCommandId[Keys.Down] = MenuCommands.KeyMoveDown;
			DetailsTemplatesSurface.keyToCommandId[Keys.Right] = MenuCommands.KeyMoveRight;
			DetailsTemplatesSurface.keyToCommandId[Keys.Left] = MenuCommands.KeyMoveLeft;
			DetailsTemplatesSurface.keyToCommandId[Keys.RButton | Keys.MButton | Keys.Space | Keys.Control] = MenuCommands.KeyNudgeUp;
			DetailsTemplatesSurface.keyToCommandId[Keys.Back | Keys.Space | Keys.Control] = MenuCommands.KeyNudgeDown;
			DetailsTemplatesSurface.keyToCommandId[Keys.LButton | Keys.RButton | Keys.MButton | Keys.Space | Keys.Control] = MenuCommands.KeyNudgeRight;
			DetailsTemplatesSurface.keyToCommandId[Keys.LButton | Keys.MButton | Keys.Space | Keys.Control] = MenuCommands.KeyNudgeLeft;
			DetailsTemplatesSurface.keyToCommandId[Keys.RButton | Keys.MButton | Keys.Space | Keys.Shift] = MenuCommands.KeySizeHeightDecrease;
			DetailsTemplatesSurface.keyToCommandId[Keys.Back | Keys.Space | Keys.Shift] = MenuCommands.KeySizeHeightIncrease;
			DetailsTemplatesSurface.keyToCommandId[Keys.LButton | Keys.RButton | Keys.MButton | Keys.Space | Keys.Shift] = MenuCommands.KeySizeWidthIncrease;
			DetailsTemplatesSurface.keyToCommandId[Keys.LButton | Keys.MButton | Keys.Space | Keys.Shift] = MenuCommands.KeySizeWidthDecrease;
			DetailsTemplatesSurface.keyToCommandId[Keys.RButton | Keys.MButton | Keys.Space | Keys.Shift | Keys.Control] = MenuCommands.KeyNudgeHeightDecrease;
			DetailsTemplatesSurface.keyToCommandId[Keys.Back | Keys.Space | Keys.Shift | Keys.Control] = MenuCommands.KeyNudgeHeightIncrease;
			DetailsTemplatesSurface.keyToCommandId[Keys.LButton | Keys.RButton | Keys.MButton | Keys.Space | Keys.Shift | Keys.Control] = MenuCommands.KeyNudgeWidthIncrease;
			DetailsTemplatesSurface.keyToCommandId[Keys.LButton | Keys.MButton | Keys.Space | Keys.Shift | Keys.Control] = MenuCommands.KeyNudgeWidthDecrease;
			DetailsTemplatesSurface.keyToCommandId[Keys.Tab] = DetailsTemplatesMenuService.SelectNextControl;
			DetailsTemplatesSurface.keyToCommandId[Keys.LButton | Keys.Back | Keys.Shift] = DetailsTemplatesMenuService.SelectPreviousControl;
			DetailsTemplatesSurface.keyToCommandId[Keys.LButton | Keys.Back | Keys.Control] = DetailsTemplatesMenuService.SwitchTabPage;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000314E File Offset: 0x0000134E
		internal TabControl TemplateTab
		{
			get
			{
				return this.templateTab;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003158 File Offset: 0x00001358
		internal static Point DialogUnitsToPixel(int XDialog, int YDialog, Form CurrentForm)
		{
			int num = (int)CurrentForm.AutoScaleDimensions.Width;
			int num2 = (int)CurrentForm.AutoScaleDimensions.Height;
			int x = (int)((double)(XDialog * num) / 4.0);
			int y = (int)((double)(YDialog * num2) / 8.0);
			return new Point(x, y);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000031B0 File Offset: 0x000013B0
		internal static Point PixelToDialogUnits(int XPixel, int YPixel, Form CurrentForm)
		{
			int num = (int)CurrentForm.AutoScaleDimensions.Width;
			int num2 = (int)CurrentForm.AutoScaleDimensions.Height;
			int x = (int)((double)XPixel * 4.0 / (double)num);
			int y = (int)((double)YPixel * 8.0 / (double)num2);
			return new Point(x, y);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003208 File Offset: 0x00001408
		internal DetailsTemplatesSurface(IServiceProvider services) : base(services)
		{
			this.SetupServices();
			base.BeginLoad(typeof(DetailsTemplatesRootControl));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003228 File Offset: 0x00001428
		private void SetupServices()
		{
			IDesignerHost designerHost = base.GetService(typeof(IDesignerHost)) as IDesignerHost;
			designerHost.AddService(typeof(INameCreationService), new DetailsTemplatesNameCreationService());
			this.detailsTemplatesMenuService = new DetailsTemplatesMenuService(this);
			this.detailsTemplatesMenuService.Enabled = false;
			designerHost.AddService(typeof(IMenuCommandService), this.detailsTemplatesMenuService);
			designerHost.AddService(typeof(IDesignerSerializationService), new DetailsTemplatesSerializationService(this));
			designerHost.AddService(typeof(ComponentSerializationService), new CodeDomComponentSerializationService(this));
			UndoEngine undoEngine = new DetailsTemplateUndoEngine(this);
			undoEngine.Enabled = false;
			designerHost.AddService(typeof(UndoEngine), undoEngine);
			Application.AddMessageFilter(this);
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000032DF File Offset: 0x000014DF
		public DetailsTemplatesMenuService DetailsTemplatesMenuService
		{
			get
			{
				return this.detailsTemplatesMenuService;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000032E7 File Offset: 0x000014E7
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00003304 File Offset: 0x00001504
		[DefaultValue(true)]
		public bool ReadOnly
		{
			get
			{
				return this.detailsTemplatesMenuService == null || !this.detailsTemplatesMenuService.Enabled;
			}
			set
			{
				if (this.ReadOnly != value)
				{
					if (this.DetailsTemplatesMenuService != null)
					{
						this.DetailsTemplatesMenuService.Enabled = !value;
					}
					UndoEngine undoEngine = base.GetService(typeof(UndoEngine)) as UndoEngine;
					if (undoEngine != null)
					{
						undoEngine.Enabled = !value;
					}
				}
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003354 File Offset: 0x00001554
		private void changeService_ComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			this.needUpdatingPropertyGrid = true;
			Control control = e.Component as Control;
			if (control is IDetailsTemplateControlBound)
			{
				DetailsTemplateControl detailsTemplateControl = (control as IDetailsTemplateControlBound).DetailsTemplateControl;
				control.Location = new Point(Math.Max(control.Location.X, 0), Math.Max(control.Location.Y, 0));
				Point point = DetailsTemplatesSurface.PixelToDialogUnits(control.Size.Width, control.Size.Height, this.ExchangeForm);
				Point point2 = DetailsTemplatesSurface.PixelToDialogUnits(control.Location.X, control.Location.Y, this.ExchangeForm);
				string name;
				if ((name = e.Member.Name) != null)
				{
					if (!(name == "Size") && !(name == "Width") && !(name == "Height"))
					{
						if (name == "Location")
						{
							detailsTemplateControl.X = point2.X;
							detailsTemplateControl.Y = point2.Y;
						}
					}
					else
					{
						detailsTemplateControl.Width = point.X;
						detailsTemplateControl.Height = point.Y;
						detailsTemplateControl.X = point2.X;
						detailsTemplateControl.Y = point2.Y;
					}
				}
			}
			if (!(e.Component is TabControl))
			{
				this.DataContext.IsDirty = true;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000034CC File Offset: 0x000016CC
		private void changeService_ComponentAdded(object sender, ComponentEventArgs e)
		{
			Control control = e.Component as Control;
			if (control is IDetailsTemplateControlBound)
			{
				DetailsTemplateControl detailsTemplateControl = (control as IDetailsTemplateControlBound).DetailsTemplateControl;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(control)["AttributeName"];
				if (propertyDescriptor != null)
				{
					TypeDescriptorContext context = new TypeDescriptorContext(this, control, propertyDescriptor);
					TypeConverter.StandardValuesCollection standardValues = propertyDescriptor.Converter.GetStandardValues(context);
					if (standardValues != null && standardValues.Count > 0)
					{
						propertyDescriptor.SetValue(control, standardValues[0]);
					}
				}
			}
			this.DataContext.IsDirty = true;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003549 File Offset: 0x00001749
		private void changeService_ComponentRemoved(object sender, ComponentEventArgs e)
		{
			this.DataContext.IsDirty = true;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003558 File Offset: 0x00001758
		private void selectionService_SelectionChanged(object sender, EventArgs e)
		{
			PropertyGrid propertyGrid = this.GetPropertyGrid();
			if (this.selectionService != null && propertyGrid != null)
			{
				ICollection selectedComponents = this.selectionService.GetSelectedComponents();
				object[] array = new object[selectedComponents.Count];
				int num = 0;
				bool flag = true;
				foreach (object obj in selectedComponents)
				{
					if (obj is TabControl)
					{
						TabControl tabControl = obj as TabControl;
						if (tabControl.SelectedTab != null)
						{
							array[num] = tabControl.SelectedTab;
						}
						else
						{
							flag = false;
						}
					}
					else
					{
						array[num] = obj;
					}
					num++;
				}
				if (flag)
				{
					propertyGrid.SelectedObjects = array;
				}
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003620 File Offset: 0x00001820
		internal static IComponent CreateComponent(ToolboxItem currentTool, IDesignerHost host)
		{
			return currentTool.CreateComponents(host)[0];
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000362B File Offset: 0x0000182B
		internal static IComponent CreateComponent(Type componentType, IDesignerHost host)
		{
			return DetailsTemplatesSurface.CreateComponent(new ToolboxItem(componentType), host);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000363C File Offset: 0x0000183C
		internal void LoadTemplate(DetailsTemplate currentTemplate)
		{
			IDesignerHost designerHost = base.GetService(typeof(IDesignerHost)) as IDesignerHost;
			Control control = designerHost.RootComponent as Control;
			CustomTabControl customTabControl = (CustomTabControl)DetailsTemplatesSurface.CreateComponent(typeof(CustomTabControl), designerHost);
			customTabControl.Parent = control;
			int num = 0;
			int num2 = 0;
			customTabControl.SuspendLayout();
			customTabControl.TabPages.Clear();
			if (currentTemplate.Pages != null)
			{
				foreach (Page page in currentTemplate.Pages)
				{
					CustomTabPage customTabPage = (CustomTabPage)DetailsTemplatesSurface.CreateComponent(typeof(CustomTabPage), designerHost);
					customTabPage.DetailsTemplateTab = page;
					foreach (DetailsTemplateControl detailsTemplateControl in page.Controls)
					{
						Type type = detailsTemplateControl.GetType();
						Control control2 = (Control)DetailsTemplatesSurface.CreateComponent(DetailsTemplatesSurface.controlMapping[type], designerHost);
						control2.Location = DetailsTemplatesSurface.DialogUnitsToPixel(detailsTemplateControl.X, detailsTemplateControl.Y, this.ExchangeForm);
						control2.Size = new Size(DetailsTemplatesSurface.DialogUnitsToPixel(detailsTemplateControl.Width, detailsTemplateControl.Height, this.ExchangeForm));
						customTabPage.Controls.Add(control2);
						(control2 as IDetailsTemplateControlBound).DetailsTemplateControl = detailsTemplateControl;
						int val = control2.Location.X + control2.Size.Width;
						int val2 = control2.Location.Y + control2.Size.Height;
						num = Math.Max(num, val);
						num2 = Math.Max(num2, val2);
					}
					customTabControl.TabPages.Add(customTabPage);
					DetailsTemplatesSurface.SortControls(customTabPage, false);
				}
			}
			num += 25;
			num2 += 50;
			num = Math.Max(num, 200);
			num2 = Math.Max(num2, 200);
			control.Size = new Size(num, num2);
			control.MinimumSize = new Size(200, 200);
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(customTabControl)["Size"];
			propertyDescriptor.SetValue(customTabControl, new Size(num, num2));
			this.templateTab = customTabControl;
			this.MonitorService();
			this.templateTab.SelectedIndexChanged += this.selectionService_SelectionChanged;
			this.selectionService_SelectionChanged(null, null);
			this.templateTab.Resize += this.templateTab_Resize;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003914 File Offset: 0x00001B14
		private void MonitorService()
		{
			if (this.changeService == null)
			{
				this.changeService = (IComponentChangeService)base.ServiceContainer.GetService(typeof(IComponentChangeService));
				if (this.changeService != null)
				{
					this.changeService.ComponentAdded += this.changeService_ComponentAdded;
					this.changeService.ComponentRemoved += this.changeService_ComponentRemoved;
					this.changeService.ComponentChanged += this.changeService_ComponentChanged;
				}
			}
			if (this.selectionService == null)
			{
				this.selectionService = (ISelectionService)base.GetService(typeof(ISelectionService));
				if (this.selectionService != null)
				{
					this.selectionService.SelectionChanged += this.selectionService_SelectionChanged;
				}
			}
			Application.Idle += this.Application_Idle;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000039EC File Offset: 0x00001BEC
		private void Application_Idle(object sender, EventArgs e)
		{
			if (this.needUpdatingPropertyGrid)
			{
				this.needUpdatingPropertyGrid = false;
				PropertyGrid propertyGrid = this.GetPropertyGrid();
				if (propertyGrid != null)
				{
					propertyGrid.Refresh();
				}
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003A18 File Offset: 0x00001C18
		private PropertyGrid GetPropertyGrid()
		{
			return base.GetService(typeof(PropertyGrid)) as PropertyGrid;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003A30 File Offset: 0x00001C30
		private void UnMonitorService()
		{
			Application.RemoveMessageFilter(this);
			if (this.selectionService != null)
			{
				this.selectionService.SelectionChanged -= this.selectionService_SelectionChanged;
			}
			if (this.changeService != null)
			{
				this.changeService.ComponentAdded -= this.changeService_ComponentAdded;
				this.changeService.ComponentRemoved += this.changeService_ComponentRemoved;
				this.changeService.ComponentChanged -= this.changeService_ComponentChanged;
			}
			Application.Idle -= this.Application_Idle;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003AC0 File Offset: 0x00001CC0
		private void templateTab_Resize(object sender, EventArgs e)
		{
			IDesignerHost designerHost = base.GetService(typeof(IDesignerHost)) as IDesignerHost;
			Control control = designerHost.RootComponent as Control;
			control.Size = this.templateTab.Size;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003B00 File Offset: 0x00001D00
		public static void SortControls(TabPage tabPage, bool win32Sort)
		{
			if (tabPage != null)
			{
				ICollection controls = tabPage.Controls;
				Control[] array = new Control[controls.Count];
				controls.CopyTo(array, 0);
				Array.Sort<Control>(array, new TabIndexComparer(win32Sort));
				tabPage.Controls.Clear();
				tabPage.Controls.AddRange(array);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003B4E File Offset: 0x00001D4E
		protected override void Dispose(bool disposing)
		{
			this.UnMonitorService();
			base.Dispose(disposing);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003B5D File Offset: 0x00001D5D
		public ContextMenu GetContextMenu()
		{
			if (this.exchangeForm != null)
			{
				return (this.exchangeForm as DetailsTemplatesEditor).EditorContextMenu;
			}
			return null;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003B7C File Offset: 0x00001D7C
		bool IMessageFilter.PreFilterMessage(ref Message m)
		{
			DetailsTemplatesEditor detailsTemplatesEditor = this.ExchangeForm as DetailsTemplatesEditor;
			bool result = false;
			if (detailsTemplatesEditor != null && m.Msg == 256 && base.View is Control && (base.View as Control).Focused)
			{
				Keys key = (Keys)((int)m.WParam | (int)Control.ModifierKeys);
				if (DetailsTemplatesSurface.keyToCommandId.ContainsKey(key))
				{
					result = detailsTemplatesEditor.ExecuteCommand(DetailsTemplatesSurface.keyToCommandId[key]);
				}
			}
			return result;
		}

		// Token: 0x0400000E RID: 14
		private const int minimumTabControlHeight = 200;

		// Token: 0x0400000F RID: 15
		private const int minimumTabControlWidth = 200;

		// Token: 0x04000010 RID: 16
		private DataContext dataContext;

		// Token: 0x04000011 RID: 17
		private TabControl templateTab;

		// Token: 0x04000012 RID: 18
		private ISelectionService selectionService;

		// Token: 0x04000013 RID: 19
		private IComponentChangeService changeService;

		// Token: 0x04000014 RID: 20
		internal static Dictionary<Type, Type> controlMapping = new Dictionary<Type, Type>();

		// Token: 0x04000015 RID: 21
		private static Dictionary<Keys, CommandID> keyToCommandId;

		// Token: 0x04000016 RID: 22
		private ExchangeForm exchangeForm;

		// Token: 0x04000017 RID: 23
		private DetailsTemplatesMenuService detailsTemplatesMenuService;

		// Token: 0x04000018 RID: 24
		private bool needUpdatingPropertyGrid;
	}
}
