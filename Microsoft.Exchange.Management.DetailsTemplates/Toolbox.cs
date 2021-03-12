using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x02000021 RID: 33
	internal class Toolbox : ExchangeUserControl, IToolboxService
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00006300 File Offset: 0x00004500
		static Toolbox()
		{
			foreach (KeyValuePair<string, Type[]> keyValuePair in Toolbox.toolboxControls)
			{
				ToolboxItem value = new ToolboxItem(keyValuePair.Value[1]);
				Toolbox.toolboxItemDictionary.Add(keyValuePair.Key, value);
				ToolboxBitmapAttribute toolboxBitmapAttribute = TypeDescriptor.GetAttributes(keyValuePair.Value[0])[typeof(ToolboxBitmapAttribute)] as ToolboxBitmapAttribute;
				Bitmap bitmap = (Bitmap)toolboxBitmapAttribute.GetImage(keyValuePair.Value[0]);
				Icon icon = Icon.FromHandle(bitmap.GetHicon());
				Toolbox.iconLibrary.Icons.Add(keyValuePair.Key, icon);
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00006633 File Offset: 0x00004833
		public Toolbox(DetailsTemplatesSurface designSurface)
		{
			this.designSurface = designSurface;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00006644 File Offset: 0x00004844
		internal void Initialize()
		{
			DetailsTemplateTypeService detailsTemplateTypeService = (DetailsTemplateTypeService)this.GetService(typeof(DetailsTemplateTypeService));
			if (detailsTemplateTypeService != null)
			{
				this.dataSource = new DataTable();
				this.dataSource.Columns.Add(Toolbox.ToolNameColumn, typeof(string));
				if (!detailsTemplateTypeService.TemplateType.Equals("Mailbox Agent"))
				{
					foreach (KeyValuePair<string, Type[]> keyValuePair in Toolbox.toolboxControls)
					{
						if (!detailsTemplateTypeService.TemplateType.Equals("Search Dialog") || !Toolbox.forbiddenSearchDialogTools.Contains(keyValuePair.Key))
						{
							DataRow dataRow = this.dataSource.NewRow();
							dataRow[Toolbox.ToolNameColumn] = keyValuePair.Key;
							this.dataSource.Rows.Add(dataRow);
						}
					}
				}
				this.toolList = new DataListView();
				this.toolList.AutoGenerateColumns = false;
				this.toolList.AvailableColumns.Add(Toolbox.ToolNameColumn, Strings.ToolNameColumnName, true);
				this.toolList.IconLibrary = Toolbox.iconLibrary;
				this.toolList.ImagePropertyName = Toolbox.ToolNameColumn;
				this.toolList.IdentityProperty = Toolbox.ToolNameColumn;
				this.toolList.MultiSelect = false;
				this.toolList.ShowSelectionPropertiesCommand = null;
				this.toolList.SelectionNameProperty = Toolbox.ToolNameColumn;
				this.toolList.DataSource = this.dataSource.DefaultView;
				this.toolList.Dock = DockStyle.Fill;
				base.Controls.Add(this.toolList);
				this.toolList.MouseDown += this.toolList_MouseDown;
				this.toolList.MouseUp += this.toolList_MouseUp;
				this.toolList.SelectionChanged += this.toolList_SelectionChanged;
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000682C File Offset: 0x00004A2C
		private void StartDragEvent(string toolName)
		{
			if (toolName != null)
			{
				ToolboxItem toolboxItem = Toolbox.toolboxItemDictionary[toolName];
				if (this.mouseClicks == 1)
				{
					DataObject data = this.SerializeToolboxItem(toolboxItem) as DataObject;
					base.DoDragDrop(data, DragDropEffects.Copy);
					DetailsTemplatesSurface.SortControls(this.designSurface.TemplateTab.SelectedTab, false);
					this.mouseClicks = 0;
					return;
				}
				if (this.mouseClicks == 2)
				{
					IDesignerHost designerHost = (IDesignerHost)this.designSurface.GetService(typeof(IDesignerHost));
					ISelectionService selectionService = (designerHost == null) ? null : (designerHost.GetService(typeof(ISelectionService)) as ISelectionService);
					if (selectionService != null)
					{
						DesignerTransaction designerTransaction = null;
						try
						{
							designerTransaction = designerHost.CreateTransaction(toolboxItem.TypeName + this.designSurface.TemplateTab.Site.Name);
							Hashtable hashtable = new Hashtable();
							hashtable["Parent"] = this.designSurface.TemplateTab.SelectedTab;
							ICollection collection = toolboxItem.CreateComponents(designerHost, hashtable);
							if (collection != null && collection.Count > 0)
							{
								selectionService.SetSelectedComponents(collection, SelectionTypes.Replace);
							}
						}
						finally
						{
							if (designerTransaction != null)
							{
								designerTransaction.Commit();
							}
						}
						DetailsTemplatesSurface.SortControls(this.designSurface.TemplateTab.SelectedTab, false);
					}
					this.mouseClicks = 0;
				}
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000697C File Offset: 0x00004B7C
		private void toolList_SelectionChanged(object sender, EventArgs e)
		{
			this.StartDragEvent(this.toolList.SelectedIdentity as string);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006994 File Offset: 0x00004B94
		private void toolList_MouseUp(object sender, MouseEventArgs e)
		{
			this.mouseClicks = 0;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000069A0 File Offset: 0x00004BA0
		private void toolList_MouseDown(object sender, MouseEventArgs e)
		{
			this.mouseClicks = e.Clicks;
			if (this.toolList.SelectedIndices.Count == 1 && this.toolList.GetItemRect(this.toolList.SelectedIndices[0]).Contains(e.Location))
			{
				this.StartDragEvent(this.toolList.SelectedIdentity as string);
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006A0E File Offset: 0x00004C0E
		public ToolboxItem GetSelectedToolboxItem(IDesignerHost host)
		{
			return null;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00006A11 File Offset: 0x00004C11
		public ToolboxItem GetSelectedToolboxItem()
		{
			return this.GetSelectedToolboxItem(null);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006A1A File Offset: 0x00004C1A
		public ToolboxItem DeserializeToolboxItem(object serializedObject, IDesignerHost host)
		{
			return (ToolboxItem)((DataObject)serializedObject).GetData(typeof(ToolboxItem));
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00006A36 File Offset: 0x00004C36
		public ToolboxItem DeserializeToolboxItem(object serializedObject)
		{
			return this.DeserializeToolboxItem(serializedObject, null);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00006A40 File Offset: 0x00004C40
		public object SerializeToolboxItem(ToolboxItem toolboxItem)
		{
			return new DataObject(toolboxItem);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006A48 File Offset: 0x00004C48
		public void SelectedToolboxItemUsed()
		{
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00006A4A File Offset: 0x00004C4A
		public void AddToolboxItem(ToolboxItem toolboxItem, string category)
		{
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00006A4C File Offset: 0x00004C4C
		public void AddToolboxItem(ToolboxItem toolboxItem)
		{
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006A4E File Offset: 0x00004C4E
		public bool IsToolboxItem(object serializedObject, IDesignerHost host)
		{
			return false;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006A51 File Offset: 0x00004C51
		public bool IsToolboxItem(object serializedObject)
		{
			return false;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006A54 File Offset: 0x00004C54
		public void SetSelectedToolboxItem(ToolboxItem toolboxItem)
		{
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00006A56 File Offset: 0x00004C56
		public CategoryNameCollection CategoryNames
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00006A59 File Offset: 0x00004C59
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00006A5C File Offset: 0x00004C5C
		public string SelectedCategory
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00006A5E File Offset: 0x00004C5E
		void IToolboxService.Refresh()
		{
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00006A60 File Offset: 0x00004C60
		public void AddLinkedToolboxItem(ToolboxItem toolboxItem, string category, IDesignerHost host)
		{
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006A62 File Offset: 0x00004C62
		public void AddLinkedToolboxItem(ToolboxItem toolboxItem, IDesignerHost host)
		{
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006A64 File Offset: 0x00004C64
		public bool IsSupported(object serializedObject, ICollection filterAttributes)
		{
			return false;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006A67 File Offset: 0x00004C67
		public bool IsSupported(object serializedObject, IDesignerHost host)
		{
			return false;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00006A6A File Offset: 0x00004C6A
		public ToolboxItemCollection GetToolboxItems(string category, IDesignerHost host)
		{
			return null;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00006A6D File Offset: 0x00004C6D
		public ToolboxItemCollection GetToolboxItems(string category)
		{
			return null;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006A70 File Offset: 0x00004C70
		public ToolboxItemCollection GetToolboxItems(IDesignerHost host)
		{
			return null;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006A73 File Offset: 0x00004C73
		public ToolboxItemCollection GetToolboxItems()
		{
			return null;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00006A76 File Offset: 0x00004C76
		public void AddCreator(ToolboxItemCreatorCallback creator, string format, IDesignerHost host)
		{
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00006A78 File Offset: 0x00004C78
		public void AddCreator(ToolboxItemCreatorCallback creator, string format)
		{
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00006A7A File Offset: 0x00004C7A
		public bool SetCursor()
		{
			return false;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00006A7D File Offset: 0x00004C7D
		public void RemoveToolboxItem(ToolboxItem toolboxItem, string category)
		{
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00006A7F File Offset: 0x00004C7F
		public void RemoveToolboxItem(ToolboxItem toolboxItem)
		{
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006A81 File Offset: 0x00004C81
		public void RemoveCreator(string format, IDesignerHost host)
		{
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00006A83 File Offset: 0x00004C83
		public void RemoveCreator(string format)
		{
		}

		// Token: 0x0400005A RID: 90
		private DataListView toolList;

		// Token: 0x0400005B RID: 91
		private DetailsTemplatesSurface designSurface;

		// Token: 0x0400005C RID: 92
		private DataTable dataSource;

		// Token: 0x0400005D RID: 93
		private int mouseClicks;

		// Token: 0x0400005E RID: 94
		private static string ToolNameColumn = "ToolName";

		// Token: 0x0400005F RID: 95
		internal static string CheckboxTool = Strings.Checkbox;

		// Token: 0x04000060 RID: 96
		internal static string EditTool = Strings.Edit;

		// Token: 0x04000061 RID: 97
		internal static string GroupboxTool = Strings.Groupbox;

		// Token: 0x04000062 RID: 98
		internal static string LabelTool = Strings.Label;

		// Token: 0x04000063 RID: 99
		internal static string ListboxTool = Strings.Listbox;

		// Token: 0x04000064 RID: 100
		internal static string MVDropdownTool = Strings.MultiValuedDD;

		// Token: 0x04000065 RID: 101
		internal static string MVListboxTool = Strings.MultiValuedLB;

		// Token: 0x04000066 RID: 102
		private static List<string> forbiddenSearchDialogTools = new List<string>(new string[]
		{
			Toolbox.ListboxTool,
			Toolbox.CheckboxTool,
			Toolbox.MVDropdownTool,
			Toolbox.MVListboxTool
		});

		// Token: 0x04000067 RID: 103
		private static KeyValuePair<string, Type[]>[] toolboxControls = new KeyValuePair<string, Type[]>[]
		{
			new KeyValuePair<string, Type[]>(Toolbox.CheckboxTool, new Type[]
			{
				typeof(CheckBox),
				typeof(CustomCheckBox)
			}),
			new KeyValuePair<string, Type[]>(Toolbox.EditTool, new Type[]
			{
				typeof(TextBox),
				typeof(CustomTextBox)
			}),
			new KeyValuePair<string, Type[]>(Toolbox.GroupboxTool, new Type[]
			{
				typeof(GroupBox),
				typeof(CustomGroupBox)
			}),
			new KeyValuePair<string, Type[]>(Toolbox.LabelTool, new Type[]
			{
				typeof(Label),
				typeof(CustomLabel)
			}),
			new KeyValuePair<string, Type[]>(Toolbox.ListboxTool, new Type[]
			{
				typeof(ListBox),
				typeof(CustomListBox)
			}),
			new KeyValuePair<string, Type[]>(Toolbox.MVDropdownTool, new Type[]
			{
				typeof(ComboBox),
				typeof(CustomComboBox)
			}),
			new KeyValuePair<string, Type[]>(Toolbox.MVListboxTool, new Type[]
			{
				typeof(ListBox),
				typeof(CustomMultiValuedListBox)
			})
		};

		// Token: 0x04000068 RID: 104
		private static Dictionary<string, ToolboxItem> toolboxItemDictionary = new Dictionary<string, ToolboxItem>();

		// Token: 0x04000069 RID: 105
		private static IconLibrary iconLibrary = new IconLibrary();
	}
}
