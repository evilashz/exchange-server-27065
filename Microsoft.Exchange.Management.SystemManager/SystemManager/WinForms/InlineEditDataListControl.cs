using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.Commands;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000140 RID: 320
	public class InlineEditDataListControl : DataListControl, IFormatModeProvider, IBindableComponent, IComponent, IDisposable
	{
		// Token: 0x06000CA4 RID: 3236 RVA: 0x0002DEA4 File Offset: 0x0002C0A4
		public InlineEditDataListControl()
		{
			base.Name = "InlineEditDataListControl";
			this.addCommand = new Command();
			this.addCommand.Name = "add";
			this.addCommand.Text = Strings.ListEditAdd;
			this.addCommand.Description = Strings.ListEditAddDescription;
			this.addCommand.Execute += this.addCommand_Execute;
			this.addCommand.Enabled = false;
			this.addCommand.Icon = Icons.Add;
			base.DataListView.AllowRemove = true;
			base.DataListView.ContextMenu.MenuItems.AddRange(new MenuItem[]
			{
				new CommandMenuItem(base.DataListView.InlineEditCommand, base.Components),
				new CommandMenuItem(base.DataListView.RemoveCommand, base.Components)
			});
			base.DataListView.SelectionChanged += this.DataListView_SelectionChanged;
			base.DataListView.BeforeLabelEdit += this.DataListView_BeforeLabelEdit;
			base.DataListView.AfterLabelEdit += this.DataListView_AfterLabelEdit;
			base.DataListView.LabelEdit = false;
			base.EditTextBox.AcceptsReturn = true;
			base.EditTextBox.Cue = Strings.ListEditAddCue;
			base.EditTextBox.Enabled = false;
			base.HandleCreated += delegate(object param0, EventArgs param1)
			{
				base.EditTextBox.Visible = true;
			};
			base.EditTextBox.TextChanged += this.textBox_TextChanged;
			base.EditTextBox.KeyPress += this.textBox_KeyPress;
			base.EditTextBox.FormatModeChanged += delegate(object param0, EventArgs param1)
			{
				this.OnFormatModeChanged(EventArgs.Empty);
			};
			base.ToolStripItems.AddRange(new ToolStripItem[]
			{
				new CommandToolStripButton(this.addCommand),
				new CommandToolStripButton(base.DataListView.InlineEditCommand),
				new CommandToolStripButton(base.DataListView.RemoveCommand)
			});
			base.EditTextBox.MouseClick += delegate(object param0, MouseEventArgs param1)
			{
				base.EditTextBox.Focus();
			};
			new TextBoxConstraintProvider(this, "DataSource", base.EditTextBox);
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x0002E0F4 File Offset: 0x0002C2F4
		// (set) Token: 0x06000CA6 RID: 3238 RVA: 0x0002E101 File Offset: 0x0002C301
		[DefaultValue("")]
		public string TextBoxText
		{
			get
			{
				return base.EditTextBox.Text;
			}
			set
			{
				base.EditTextBox.Text = value;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x0002E10F File Offset: 0x0002C30F
		// (set) Token: 0x06000CA8 RID: 3240 RVA: 0x0002E11C File Offset: 0x0002C31C
		public string TextBoxCue
		{
			get
			{
				return base.EditTextBox.Cue;
			}
			set
			{
				base.EditTextBox.Cue = value;
			}
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0002E12A File Offset: 0x0002C32A
		private bool ShouldSerializeTextBoxCue()
		{
			return Strings.ListEditAddCue != this.TextBoxCue;
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0002E141 File Offset: 0x0002C341
		private void ResetTextBoxCue()
		{
			this.TextBoxCue = Strings.ListEditAddCue;
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0002E154 File Offset: 0x0002C354
		private void addCommand_Execute(object sender, EventArgs e)
		{
			object obj = this.ParseString(base.EditTextBox.Text);
			if (obj != null && this.InternalAddValue(obj))
			{
				this.TextBoxText = "";
			}
			base.EditTextBox.Select();
			base.EditTextBox.SelectAll();
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0002E1A0 File Offset: 0x0002C3A0
		private void DataListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			if (!string.IsNullOrEmpty(e.Label))
			{
				object obj = this.ParseString(e.Label);
				if (obj != null)
				{
					base.InternalEditValue(e.Item, obj, true);
				}
			}
			else if (e.Label != null)
			{
				base.ShowErrorAsync(Strings.InlineEditCannotBlankEntry);
			}
			e.CancelEdit = true;
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0002E1FC File Offset: 0x0002C3FC
		private void DataListView_BeforeLabelEdit(object sender, LabelEditEventArgs e)
		{
			HandleRef handleRef = new HandleRef(base.DataListView, base.DataListView.Handle);
			IntPtr handle = UnsafeNativeMethods.SendMessage(handleRef, 4120, (IntPtr)0, (IntPtr)0);
			HandleRef handleRef2 = new HandleRef(base.DataListView, handle);
			UnsafeNativeMethods.SendMessage(handleRef2, 197, (IntPtr)base.EditTextBox.MaxLength, (IntPtr)0);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0002E269 File Offset: 0x0002C469
		private void DataListView_SelectionChanged(object sender, EventArgs e)
		{
			if (base.DataList.Count == 0)
			{
				base.EditTextBox.Focus();
			}
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0002E284 File Offset: 0x0002C484
		protected override void OnDataSourceChanged(EventArgs e)
		{
			this.ResetParseFunctionality();
			base.OnDataSourceChanged(e);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0002E294 File Offset: 0x0002C494
		private void ResetParseFunctionality()
		{
			bool flag = null != base.DataList;
			bool flag2 = flag && this.CanParse;
			bool flag3 = flag && !base.DataList.IsFixedSize;
			base.EditTextBox.Enabled = (flag2 && flag3);
			base.DataListView.LabelEdit = flag2;
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0002E2EE File Offset: 0x0002C4EE
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			return (keyData != Keys.Return || !base.EditTextBox.Focused || string.IsNullOrEmpty(base.EditTextBox.Text)) && base.ProcessDialogKey(keyData);
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0002E31D File Offset: 0x0002C51D
		private void textBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				e.Handled = true;
				if (this.addCommand.Enabled)
				{
					this.addCommand.Invoke();
				}
			}
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0002E348 File Offset: 0x0002C548
		private void textBox_TextChanged(object sender, EventArgs e)
		{
			this.addCommand.Enabled = (base.EditTextBox.Enabled && !string.IsNullOrEmpty(base.EditTextBox.Text));
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x0002E378 File Offset: 0x0002C578
		protected virtual bool CanParse
		{
			get
			{
				if (base.Events[InlineEditDataListControl.EventParse] != null)
				{
					return true;
				}
				MethodInfo method = base.ItemType.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, null, new Type[]
				{
					typeof(string)
				}, null);
				if (null != method)
				{
					return true;
				}
				TypeConverter converter = TypeDescriptor.GetConverter(base.ItemType);
				if (converter != null && converter.CanConvertFrom(typeof(string)))
				{
					return true;
				}
				ConstructorInfo constructor = base.ItemType.GetConstructor(new Type[]
				{
					typeof(string)
				});
				return null != constructor;
			}
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0002E424 File Offset: 0x0002C624
		private object ParseString(string value)
		{
			try
			{
				ConvertEventHandler convertEventHandler = (ConvertEventHandler)base.Events[InlineEditDataListControl.EventParse];
				if (convertEventHandler != null)
				{
					ConvertEventArgs convertEventArgs = new ConvertEventArgs(value, base.ItemType);
					convertEventHandler(this, convertEventArgs);
					return convertEventArgs.Value;
				}
				MethodInfo method = base.ItemType.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, null, new Type[]
				{
					typeof(string)
				}, null);
				if (null != method)
				{
					try
					{
						return method.Invoke(null, new object[]
						{
							value
						});
					}
					catch (TargetInvocationException ex)
					{
						throw new FormatException(ex.InnerException.Message, ex.InnerException);
					}
				}
				TypeConverter converter = TypeDescriptor.GetConverter(base.ItemType);
				if (converter != null && converter.CanConvertFrom(typeof(string)))
				{
					return converter.ConvertFrom(null, CultureInfo.CurrentUICulture, value);
				}
				ConstructorInfo constructor = base.ItemType.GetConstructor(new Type[]
				{
					typeof(string)
				});
				if (null != constructor)
				{
					try
					{
						return constructor.Invoke(new object[]
						{
							value
						});
					}
					catch (TargetInvocationException ex2)
					{
						throw new FormatException(ex2.InnerException.Message, ex2.InnerException);
					}
				}
				return value;
			}
			catch (FormatException ex3)
			{
				base.ShowErrorAsync(ex3.Message);
			}
			catch (ArgumentException ex4)
			{
				base.ShowErrorAsync(ex4.Message);
			}
			return null;
		}

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06000CB6 RID: 3254 RVA: 0x0002E604 File Offset: 0x0002C804
		// (remove) Token: 0x06000CB7 RID: 3255 RVA: 0x0002E61D File Offset: 0x0002C81D
		public event ConvertEventHandler Parse
		{
			add
			{
				base.Events.AddHandler(InlineEditDataListControl.EventParse, value);
				this.ResetParseFunctionality();
			}
			remove
			{
				base.Events.RemoveHandler(InlineEditDataListControl.EventParse, value);
				this.ResetParseFunctionality();
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x0002E636 File Offset: 0x0002C836
		[DefaultValue(null)]
		[Browsable(false)]
		public Command AddCommand
		{
			get
			{
				return this.addCommand;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0002E63E File Offset: 0x0002C83E
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x0002E64B File Offset: 0x0002C84B
		[DefaultValue(0)]
		public DisplayFormatMode FormatMode
		{
			get
			{
				return base.EditTextBox.FormatMode;
			}
			set
			{
				base.EditTextBox.FormatMode = value;
			}
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0002E65C File Offset: 0x0002C85C
		protected virtual void OnFormatModeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[InlineEditDataListControl.EventFormatModeChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06000CBC RID: 3260 RVA: 0x0002E68A File Offset: 0x0002C88A
		// (remove) Token: 0x06000CBD RID: 3261 RVA: 0x0002E69D File Offset: 0x0002C89D
		public event EventHandler FormatModeChanged
		{
			add
			{
				base.Events.AddHandler(InlineEditDataListControl.EventFormatModeChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(InlineEditDataListControl.EventFormatModeChanged, value);
			}
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0002E6C6 File Offset: 0x0002C8C6
		void IFormatModeProvider.add_BindingContextChanged(EventHandler A_1)
		{
			base.BindingContextChanged += A_1;
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0002E6CF File Offset: 0x0002C8CF
		void IFormatModeProvider.remove_BindingContextChanged(EventHandler A_1)
		{
			base.BindingContextChanged -= A_1;
		}

		// Token: 0x0400051D RID: 1309
		private Command addCommand;

		// Token: 0x0400051E RID: 1310
		private static readonly object EventParse = new object();

		// Token: 0x0400051F RID: 1311
		private static readonly object EventFormatModeChanged = new object();
	}
}
