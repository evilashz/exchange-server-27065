using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x02000002 RID: 2
	internal sealed class CustomControlDesigner : ControlDesigner
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			base.InitializeNewComponent(defaultValues);
			this.Control.Text = string.Empty;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020EC File Offset: 0x000002EC
		private DetailsTemplateControl BoundControl
		{
			get
			{
				return (this.Control as IDetailsTemplateControlBound).DetailsTemplateControl;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000210C File Offset: 0x0000030C
		[Browsable(false)]
		public override SelectionRules SelectionRules
		{
			get
			{
				SelectionRules result = SelectionRules.Visible | SelectionRules.Locked;
				DetailsTemplateTypeService detailsTemplateTypeService = (DetailsTemplateTypeService)this.GetService(typeof(DetailsTemplateTypeService));
				if (detailsTemplateTypeService == null || !detailsTemplateTypeService.TemplateType.Equals("Mailbox Agent"))
				{
					result = base.SelectionRules;
				}
				return result;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002152 File Offset: 0x00000352
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002160 File Offset: 0x00000360
		[LocDisplayName(Strings.IDs.X)]
		[LocCategory(Strings.IDs.Layout)]
		public int DialogX
		{
			get
			{
				return this.BoundControl.X;
			}
			set
			{
				this.BoundControl.X = value;
				this.Control.Location = DetailsTemplatesSurface.DialogUnitsToPixel(this.BoundControl.X, this.BoundControl.Y, (this.ParentComponent as Control).FindForm());
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000021AF File Offset: 0x000003AF
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000021BC File Offset: 0x000003BC
		[LocDisplayName(Strings.IDs.Y)]
		[LocCategory(Strings.IDs.Layout)]
		public int DialogY
		{
			get
			{
				return this.BoundControl.Y;
			}
			set
			{
				this.BoundControl.Y = value;
				this.Control.Location = DetailsTemplatesSurface.DialogUnitsToPixel(this.BoundControl.X, this.BoundControl.Y, (this.ParentComponent as Control).FindForm());
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000220B File Offset: 0x0000040B
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002218 File Offset: 0x00000418
		[LocCategory(Strings.IDs.Layout)]
		[LocDisplayName(Strings.IDs.Width)]
		public int DialogWidth
		{
			get
			{
				return this.BoundControl.Width;
			}
			set
			{
				this.BoundControl.Width = value;
				this.Control.Size = new Size(DetailsTemplatesSurface.DialogUnitsToPixel(this.BoundControl.Width, this.BoundControl.Height, (this.ParentComponent as Control).FindForm()));
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000226C File Offset: 0x0000046C
		// (set) Token: 0x0600000B RID: 11 RVA: 0x0000227C File Offset: 0x0000047C
		[LocCategory(Strings.IDs.Layout)]
		[LocDisplayName(Strings.IDs.Height)]
		public int DialogHeight
		{
			get
			{
				return this.BoundControl.Height;
			}
			set
			{
				this.BoundControl.Height = value;
				this.Control.Size = new Size(DetailsTemplatesSurface.DialogUnitsToPixel(this.BoundControl.Width, this.BoundControl.Height, (this.ParentComponent as Control).FindForm()));
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000022D0 File Offset: 0x000004D0
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000022E0 File Offset: 0x000004E0
		public int TabIndex
		{
			get
			{
				return this.Control.TabIndex;
			}
			set
			{
				this.Control.TabIndex = value;
				TabPage tabPage = this.Control.Parent as TabPage;
				DetailsTemplatesSurface.SortControls(tabPage, false);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002314 File Offset: 0x00000514
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
			PropertyDescriptorCollection properties2 = TypeDescriptor.GetProperties(base.GetType());
			foreach (object obj2 in properties2)
			{
				PropertyDescriptor propertyDescriptor3 = (PropertyDescriptor)obj2;
				if (propertyDescriptor3.ComponentType == base.GetType())
				{
					properties[propertyDescriptor3.Name] = propertyDescriptor3;
				}
			}
		}
	}
}
