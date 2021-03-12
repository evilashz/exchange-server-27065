using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001DA RID: 474
	public class ExchangeColumnHeader : ColumnHeader
	{
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x00057BE6 File Offset: 0x00055DE6
		// (set) Token: 0x0600153C RID: 5436 RVA: 0x00057BEE File Offset: 0x00055DEE
		[DefaultValue(150)]
		public new int Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				base.Width = ((value > 0) ? value : 150);
			}
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x00057C24 File Offset: 0x00055E24
		public ExchangeColumnHeader()
		{
			this.filterOperator2MenuItemMapping = new Hashtable();
			this.contextMenu = new ContextMenu();
			MenuItem menuItem = new MenuItem();
			MenuItem menuItem2 = new MenuItem();
			MenuItem menuItem3 = new MenuItem();
			MenuItem menuItem4 = new MenuItem();
			MenuItem menuItem5 = new MenuItem();
			MenuItem menuItem6 = new MenuItem();
			MenuItem menuItem7 = new MenuItem();
			MenuItem menuItem8 = new MenuItem();
			MenuItem menuItem9 = new MenuItem();
			menuItem.Name = "containsMenuItem";
			menuItem.Text = Strings.Contains;
			menuItem.Click += this.filterTypeMenuItem_Click;
			menuItem.Checked = true;
			menuItem.Tag = 1;
			this.filterOperator2MenuItemMapping.Add(1, menuItem);
			menuItem2.Name = "doesNotContainMenuItem";
			menuItem2.Text = Strings.DoesNotContain;
			menuItem2.Click += this.filterTypeMenuItem_Click;
			menuItem2.Tag = 2;
			this.filterOperator2MenuItemMapping.Add(2, menuItem2);
			menuItem3.Name = "startsWithMenuItem";
			menuItem3.Text = Strings.StartsWith;
			menuItem3.Click += this.filterTypeMenuItem_Click;
			menuItem3.Tag = 4;
			this.filterOperator2MenuItemMapping.Add(4, menuItem3);
			menuItem4.Name = "endsWithMenuItem";
			menuItem4.Text = Strings.EndsWith;
			menuItem4.Click += this.filterTypeMenuItem_Click;
			menuItem4.Tag = 8;
			this.filterOperator2MenuItemMapping.Add(8, menuItem4);
			menuItem5.Name = "isExactlyMenuItem";
			menuItem5.Text = Strings.IsExactly;
			menuItem5.Click += this.filterTypeMenuItem_Click;
			menuItem5.Tag = 16;
			this.filterOperator2MenuItemMapping.Add(16, menuItem5);
			menuItem6.Name = "isNotMenuItem";
			menuItem6.Text = Strings.IsNot;
			menuItem6.Click += this.filterTypeMenuItem_Click;
			menuItem6.Tag = 32;
			this.filterOperator2MenuItemMapping.Add(32, menuItem6);
			menuItem7.Name = "separatorMenuItem";
			menuItem7.Text = "-";
			menuItem8.Name = "clearFilterMenuItem";
			menuItem8.Text = Strings.ClearFilter;
			menuItem8.Click += delegate(object param0, EventArgs param1)
			{
				this.ListView.ClearFilter(base.Index);
			};
			menuItem9.Name = "clearAllFiltersMenuItem";
			menuItem9.Text = Strings.ClearAllFilters;
			menuItem9.Click += delegate(object param0, EventArgs param1)
			{
				this.ListView.ClearAllFilters();
			};
			this.contextMenu.MenuItems.AddRange(new MenuItem[]
			{
				menuItem,
				menuItem2,
				menuItem3,
				menuItem4,
				menuItem5,
				menuItem6,
				menuItem7,
				menuItem8,
				menuItem9
			});
			this.contextMenu.Name = "contextMenu";
			this.selectedMenuItem = menuItem;
			this.Width = 150;
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x00057F7C File Offset: 0x0005617C
		public ExchangeColumnHeader(string name, string text, int width) : this(name, text, width, true)
		{
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x00057F88 File Offset: 0x00056188
		public ExchangeColumnHeader(string name, string text) : this(name, text, 150)
		{
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x00057F97 File Offset: 0x00056197
		public ExchangeColumnHeader(string name, string text, bool isDefault) : this(name, text, -2, isDefault, string.Empty)
		{
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x00057FA9 File Offset: 0x000561A9
		public ExchangeColumnHeader(string name, string text, int width, bool isDefault) : this(name, text, width, isDefault, string.Empty)
		{
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x00057FBC File Offset: 0x000561BC
		public ExchangeColumnHeader(string name, string text, int width, bool isDefault, string defaultEmptyText) : this(name, text, width, isDefault, defaultEmptyText, null, null, null)
		{
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x00057FDC File Offset: 0x000561DC
		public ExchangeColumnHeader(string name, string text, int width, bool isDefault, string defaultEmptyText, ICustomFormatter customFormatter, string formatString, IFormatProvider formatProvider) : this()
		{
			base.Name = name;
			base.Text = text;
			this.Width = width;
			this.Default = isDefault;
			this.DefaultEmptyText = defaultEmptyText;
			this.CustomFormatter = customFormatter;
			this.FormatString = formatString;
			this.FormatProvider = formatProvider;
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0005802C File Offset: 0x0005622C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.ContextMenu.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001545 RID: 5445 RVA: 0x00058044 File Offset: 0x00056244
		public string BindingListViewFilter
		{
			get
			{
				string filterValue = this.FilterValue;
				string result = null;
				if (!string.IsNullOrEmpty(filterValue))
				{
					string text = this.EscapeForColumnName(base.Name);
					string text2 = this.EscapeForUserDefinedValue(filterValue);
					FilterOperator filterOperator = (FilterOperator)this.selectedMenuItem.Tag;
					string format;
					if (filterOperator <= 8)
					{
						switch (filterOperator)
						{
						case 1:
							format = "{0} LIKE '*{1}*'";
							goto IL_AD;
						case 2:
							format = "{0} NOT LIKE '*{1}*'";
							goto IL_AD;
						case 3:
							break;
						case 4:
							format = "{0} LIKE '{1}*'";
							goto IL_AD;
						default:
							if (filterOperator == 8)
							{
								format = "{0} LIKE '*{1}'";
								goto IL_AD;
							}
							break;
						}
					}
					else
					{
						if (filterOperator == 16)
						{
							format = "{0} = '{1}'";
							goto IL_AD;
						}
						if (filterOperator == 32)
						{
							format = "NOT ({0} = '{1}')";
							goto IL_AD;
						}
					}
					throw new ArgumentOutOfRangeException();
					IL_AD:
					result = string.Format(CultureInfo.InvariantCulture, format, new object[]
					{
						text,
						text2
					});
				}
				return result;
			}
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x00058120 File Offset: 0x00056320
		private string EscapeForColumnName(string name)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			stringBuilder.Append(name.Replace("]", "\\]"));
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x00058168 File Offset: 0x00056368
		private string EscapeForUserDefinedValue(string value)
		{
			StringBuilder stringBuilder = new StringBuilder(value.Length * 3);
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				char c2 = c;
				switch (c2)
				{
				case '%':
					goto IL_61;
				case '&':
					goto IL_7F;
				case '\'':
					stringBuilder.Append("''");
					break;
				default:
					if (c2 == '*')
					{
						goto IL_61;
					}
					switch (c2)
					{
					case '[':
					case ']':
						goto IL_61;
					case '\\':
						goto IL_7F;
					default:
						goto IL_7F;
					}
					break;
				}
				IL_8D:
				i++;
				continue;
				IL_61:
				stringBuilder.Append("[").Append(c).Append("]");
				goto IL_8D;
				IL_7F:
				stringBuilder.Append(c.ToString());
				goto IL_8D;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x00058218 File Offset: 0x00056418
		// (set) Token: 0x06001549 RID: 5449 RVA: 0x0005822A File Offset: 0x0005642A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public FilterOperator FilterOperator
		{
			get
			{
				return (FilterOperator)this.selectedMenuItem.Tag;
			}
			set
			{
				if (this.FilterOperator != value)
				{
					this.filterTypeMenuItem_Click((MenuItem)this.filterOperator2MenuItemMapping[value], EventArgs.Empty);
				}
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x00058256 File Offset: 0x00056456
		public new DataListView ListView
		{
			get
			{
				return (DataListView)base.ListView;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x00058263 File Offset: 0x00056463
		// (set) Token: 0x0600154C RID: 5452 RVA: 0x0005827B File Offset: 0x0005647B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string FilterValue
		{
			get
			{
				return this.ListView.FilterValues[base.Index];
			}
			set
			{
				this.ListView.FilterValues[base.Index] = value;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x00058294 File Offset: 0x00056494
		public ContextMenu ContextMenu
		{
			get
			{
				return this.contextMenu;
			}
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x0005829C File Offset: 0x0005649C
		private void filterTypeMenuItem_Click(object sender, EventArgs e)
		{
			this.selectedMenuItem.Checked = false;
			this.selectedMenuItem = (MenuItem)sender;
			this.selectedMenuItem.Checked = true;
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x000582C2 File Offset: 0x000564C2
		// (set) Token: 0x06001550 RID: 5456 RVA: 0x000582CA File Offset: 0x000564CA
		[DefaultValue(true)]
		public bool IsSortable
		{
			get
			{
				return this.isSortable;
			}
			set
			{
				this.isSortable = value;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x000582D3 File Offset: 0x000564D3
		// (set) Token: 0x06001552 RID: 5458 RVA: 0x000582DB File Offset: 0x000564DB
		[DefaultValue(true)]
		public bool IsReorderable
		{
			get
			{
				return this.isReorderable;
			}
			set
			{
				this.isReorderable = value;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x000582E4 File Offset: 0x000564E4
		// (set) Token: 0x06001554 RID: 5460 RVA: 0x000582EC File Offset: 0x000564EC
		[DefaultValue(false)]
		public bool Visible
		{
			get
			{
				return this.visible;
			}
			set
			{
				if (this.Visible != value)
				{
					this.visible = value;
					this.OnVisibleChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0005830C File Offset: 0x0005650C
		protected virtual void OnVisibleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ExchangeColumnHeader.EventVisibleChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000087 RID: 135
		// (add) Token: 0x06001556 RID: 5462 RVA: 0x0005833A File Offset: 0x0005653A
		// (remove) Token: 0x06001557 RID: 5463 RVA: 0x0005834D File Offset: 0x0005654D
		public event EventHandler VisibleChanged
		{
			add
			{
				base.Events.AddHandler(ExchangeColumnHeader.EventVisibleChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ExchangeColumnHeader.EventVisibleChanged, value);
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x00058360 File Offset: 0x00056560
		// (set) Token: 0x06001559 RID: 5465 RVA: 0x00058368 File Offset: 0x00056568
		[DefaultValue(false)]
		public bool Default
		{
			get
			{
				return this.isDefault;
			}
			set
			{
				if (this.isDefault != value)
				{
					this.isDefault = value;
					if (this.isDefault)
					{
						this.Visible = true;
						this.SetDefaultDisplayIndex();
					}
				}
			}
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0005838F File Offset: 0x0005658F
		public void SetDefaultDisplayIndex()
		{
			if (this.Default && this.ListView != null)
			{
				this.defaultDisplayIndex = base.DisplayIndex;
				return;
			}
			this.defaultDisplayIndex = -1;
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x000583B5 File Offset: 0x000565B5
		public int DefaultDisplayIndex
		{
			get
			{
				return this.defaultDisplayIndex;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x000583BD File Offset: 0x000565BD
		// (set) Token: 0x0600155D RID: 5469 RVA: 0x000583C5 File Offset: 0x000565C5
		[DefaultValue("")]
		public string DefaultEmptyText
		{
			get
			{
				return this.defaultEmptyText;
			}
			set
			{
				this.defaultEmptyText = value;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x000583CE File Offset: 0x000565CE
		// (set) Token: 0x0600155F RID: 5471 RVA: 0x000583D6 File Offset: 0x000565D6
		[DefaultValue(null)]
		public ICustomFormatter CustomFormatter
		{
			get
			{
				return this.customFormatter;
			}
			set
			{
				this.customFormatter = value;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x000583DF File Offset: 0x000565DF
		// (set) Token: 0x06001561 RID: 5473 RVA: 0x000583E7 File Offset: 0x000565E7
		[DefaultValue(null)]
		public IToColorFormatter ToColorFormatter { get; set; }

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x000583F0 File Offset: 0x000565F0
		// (set) Token: 0x06001563 RID: 5475 RVA: 0x000583F8 File Offset: 0x000565F8
		[DefaultValue(null)]
		public string FormatString
		{
			get
			{
				return this.formatString;
			}
			set
			{
				this.formatString = value;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x00058401 File Offset: 0x00056601
		// (set) Token: 0x06001565 RID: 5477 RVA: 0x00058409 File Offset: 0x00056609
		[DefaultValue(null)]
		public IFormatProvider FormatProvider
		{
			get
			{
				return this.formatProvider;
			}
			set
			{
				this.formatProvider = value;
			}
		}

		// Token: 0x040007B5 RID: 1973
		private const int defaultColumnWidth = 150;

		// Token: 0x040007B6 RID: 1974
		private const bool DefaultVisible = false;

		// Token: 0x040007B7 RID: 1975
		private ContextMenu contextMenu;

		// Token: 0x040007B8 RID: 1976
		private Hashtable filterOperator2MenuItemMapping;

		// Token: 0x040007B9 RID: 1977
		private MenuItem selectedMenuItem;

		// Token: 0x040007BA RID: 1978
		private bool isSortable = true;

		// Token: 0x040007BB RID: 1979
		private bool isReorderable = true;

		// Token: 0x040007BC RID: 1980
		private bool visible;

		// Token: 0x040007BD RID: 1981
		private static readonly object EventVisibleChanged = new object();

		// Token: 0x040007BE RID: 1982
		private bool isDefault;

		// Token: 0x040007BF RID: 1983
		private int defaultDisplayIndex = -1;

		// Token: 0x040007C0 RID: 1984
		private string defaultEmptyText = string.Empty;

		// Token: 0x040007C1 RID: 1985
		private ICustomFormatter customFormatter;

		// Token: 0x040007C2 RID: 1986
		private string formatString;

		// Token: 0x040007C3 RID: 1987
		private IFormatProvider formatProvider;
	}
}
