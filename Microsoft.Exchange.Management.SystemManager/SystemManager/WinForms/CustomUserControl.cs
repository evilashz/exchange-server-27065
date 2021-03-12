using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000DB RID: 219
	public class CustomUserControl : UserControl, ICustomTypeDescriptor
	{
		// Token: 0x060007A3 RID: 1955 RVA: 0x00019968 File Offset: 0x00017B68
		public CustomUserControl()
		{
			base.Name = "CustomUserControl";
			this.VisualEffectsLinearGradientMode = LinearGradientMode.Vertical;
			if (this.EnableVisualEffects)
			{
				Extensions.EnsureDoubleBuffer(this);
			}
			base.ControlAdded += this.CustomUserControl_ControlAdded;
			Theme.UseVisualEffectsChanged += this.Theme_UseVisualEffectsChanged;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x000199BE File Offset: 0x00017BBE
		public virtual PropertyDescriptorCollection GetCustomProperties(Attribute[] attributes)
		{
			return new PropertyDescriptorCollection(null);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x000199C6 File Offset: 0x00017BC6
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x000199CF File Offset: 0x00017BCF
		string ICustomTypeDescriptor.GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x000199D8 File Offset: 0x00017BD8
		string ICustomTypeDescriptor.GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x000199E1 File Offset: 0x00017BE1
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x000199EA File Offset: 0x00017BEA
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000199F3 File Offset: 0x00017BF3
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x000199FC File Offset: 0x00017BFC
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00019A06 File Offset: 0x00017C06
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00019A0F File Offset: 0x00017C0F
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00019A19 File Offset: 0x00017C19
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00019A24 File Offset: 0x00017C24
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
			foreach (object obj in TypeDescriptor.GetProperties(this, attributes, true))
			{
				PropertyDescriptor value = (PropertyDescriptor)obj;
				propertyDescriptorCollection.Add(value);
			}
			foreach (object obj2 in this.GetCustomProperties(attributes))
			{
				PropertyDescriptor value2 = (PropertyDescriptor)obj2;
				propertyDescriptorCollection.Add(value2);
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00019ADC File Offset: 0x00017CDC
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor propertyDescriptor)
		{
			return this;
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x00019ADF File Offset: 0x00017CDF
		private Color VisualEffectsLinearGradientBeginColor
		{
			get
			{
				if (this.VisualEffectsLinearGradientMode == LinearGradientMode.Horizontal)
				{
					return ControlPaint.LightLight(this.BackColor);
				}
				if (this.VisualEffectsLinearGradientMode == LinearGradientMode.Vertical)
				{
					return this.BackColor;
				}
				return Color.Empty;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00019B0A File Offset: 0x00017D0A
		private Color VisualEffectsLinearGradientEndColor
		{
			get
			{
				if (this.VisualEffectsLinearGradientMode == LinearGradientMode.Horizontal)
				{
					return this.BackColor;
				}
				if (this.VisualEffectsLinearGradientMode == LinearGradientMode.Vertical)
				{
					return ControlPaint.LightLight(this.BackColor);
				}
				return Color.Empty;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00019B35 File Offset: 0x00017D35
		// (set) Token: 0x060007B4 RID: 1972 RVA: 0x00019B3D File Offset: 0x00017D3D
		[DefaultValue(LinearGradientMode.Vertical)]
		public LinearGradientMode VisualEffectsLinearGradientMode
		{
			get
			{
				return this.visualEffectsLinearGradientMode;
			}
			set
			{
				if (value != LinearGradientMode.Vertical && value != LinearGradientMode.Horizontal)
				{
					throw new NotSupportedException();
				}
				if (this.visualEffectsLinearGradientMode != value)
				{
					this.visualEffectsLinearGradientMode = value;
					this.Theme_UseVisualEffectsChanged(this, EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00019B68 File Offset: 0x00017D68
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x00019B70 File Offset: 0x00017D70
		[DefaultValue(false)]
		public bool EnableVisualEffects
		{
			get
			{
				return this.enableVisualEffects;
			}
			set
			{
				if (this.enableVisualEffects != value)
				{
					this.enableVisualEffects = value;
					Extensions.SetDoubleBuffer(this, value);
					this.Theme_UseVisualEffectsChanged(this, EventArgs.Empty);
				}
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00019B95 File Offset: 0x00017D95
		private void Theme_UseVisualEffectsChanged(object sender, EventArgs e)
		{
			base.Invalidate(true);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00019B9E File Offset: 0x00017D9E
		private void CustomUserControl_ControlAdded(object sender, ControlEventArgs e)
		{
			if (this.EnableVisualEffects)
			{
				Extensions.EnsureDoubleBuffer(e.Control);
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00019BB3 File Offset: 0x00017DB3
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Theme.UseVisualEffectsChanged -= this.Theme_UseVisualEffectsChanged;
			}
			base.Dispose(disposing);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00019BD0 File Offset: 0x00017DD0
		protected override void OnPaint(PaintEventArgs e)
		{
			if (Theme.UseVisualEffects && this.EnableVisualEffects && base.Width != 0 && base.Height != 0)
			{
				Rectangle rect = new Rectangle(0, 0, base.Width, base.Height);
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, this.VisualEffectsLinearGradientBeginColor, this.VisualEffectsLinearGradientEndColor, this.VisualEffectsLinearGradientMode))
				{
					if (LayoutHelper.IsRightToLeft(this) && this.VisualEffectsLinearGradientMode == LinearGradientMode.Horizontal)
					{
						linearGradientBrush.RotateTransform(180f);
					}
					e.Graphics.FillRectangle(linearGradientBrush, rect);
				}
			}
			base.OnPaint(e);
		}

		// Token: 0x0400039A RID: 922
		private LinearGradientMode visualEffectsLinearGradientMode;

		// Token: 0x0400039B RID: 923
		private bool enableVisualEffects;
	}
}
