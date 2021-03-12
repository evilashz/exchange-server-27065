using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x0200000C RID: 12
	[Designer(typeof(CustomTabPage.CustomTabDesigner))]
	internal sealed class CustomTabPage : TabPage
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002ABB File Offset: 0x00000CBB
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002AC8 File Offset: 0x00000CC8
		[DefaultValue("")]
		public override string Text
		{
			get
			{
				return this.detailsTemplatePage.Text;
			}
			set
			{
				this.detailsTemplatePage.Text = (value ?? string.Empty);
				base.Text = (value ?? string.Empty);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002AEF File Offset: 0x00000CEF
		protected override void OnDragEnter(DragEventArgs drgevent)
		{
			if (!drgevent.Data.GetDataPresent(typeof(CustomTabPage)))
			{
				base.OnDragEnter(drgevent);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002B0F File Offset: 0x00000D0F
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002B1C File Offset: 0x00000D1C
		public int HelpContext
		{
			get
			{
				return this.detailsTemplatePage.HelpContext;
			}
			set
			{
				this.detailsTemplatePage.HelpContext = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002B2A File Offset: 0x00000D2A
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002B32 File Offset: 0x00000D32
		[Browsable(false)]
		public Page DetailsTemplateTab
		{
			get
			{
				return this.detailsTemplatePage;
			}
			set
			{
				this.detailsTemplatePage = value;
				base.Text = this.detailsTemplatePage.Text;
			}
		}

		// Token: 0x0400000A RID: 10
		public const string TabPageIndex = "TabPageIndex";

		// Token: 0x0400000B RID: 11
		private Page detailsTemplatePage = new Page();

		// Token: 0x0200000D RID: 13
		private sealed class CustomTabDesigner : ScrollableControlDesigner
		{
			// Token: 0x17000023 RID: 35
			// (get) Token: 0x06000056 RID: 86 RVA: 0x00002B5F File Offset: 0x00000D5F
			public override SelectionRules SelectionRules
			{
				get
				{
					return this.selectionRules;
				}
			}

			// Token: 0x06000057 RID: 87 RVA: 0x00002B68 File Offset: 0x00000D68
			public override void InitializeNewComponent(IDictionary defaultValues)
			{
				base.InitializeNewComponent(defaultValues);
				this.Control.Text = string.Empty;
				int num = 0;
				if (defaultValues != null && defaultValues["TabPageIndex"] != null)
				{
					num = (int)defaultValues["TabPageIndex"];
				}
				TabControl tabControl = (this.Control == null) ? null : (this.Control.Parent as TabControl);
				if (tabControl != null)
				{
					if (num < 0)
					{
						num = 0;
					}
					else if (num >= tabControl.TabCount)
					{
						num = tabControl.TabCount - 1;
					}
					TabPage tabPage = this.Control as TabPage;
					if (num != tabControl.TabPages.IndexOf(tabPage))
					{
						tabControl.TabPages.Remove(tabPage);
						tabControl.TabPages.Insert(num, tabPage);
					}
					tabControl.SelectedTab = tabPage;
				}
			}

			// Token: 0x06000058 RID: 88 RVA: 0x00002C24 File Offset: 0x00000E24
			protected override void PreFilterProperties(IDictionary properties)
			{
				base.PreFilterProperties(properties);
				Collection<DictionaryEntry> collection = new Collection<DictionaryEntry>();
				foreach (object obj in properties)
				{
					DictionaryEntry item = (DictionaryEntry)obj;
					PropertyDescriptor propertyDescriptor = item.Value as PropertyDescriptor;
					if (propertyDescriptor.ComponentType != base.Component.GetType())
					{
						collection.Add(item);
					}
				}
				foreach (DictionaryEntry dictionaryEntry in collection)
				{
					PropertyDescriptor propertyDescriptor2 = dictionaryEntry.Value as PropertyDescriptor;
					properties[dictionaryEntry.Key] = TypeDescriptor.CreateProperty(propertyDescriptor2.ComponentType, propertyDescriptor2, new Attribute[]
					{
						BrowsableAttribute.No
					});
				}
			}

			// Token: 0x0400000C RID: 12
			private SelectionRules selectionRules;
		}
	}
}
