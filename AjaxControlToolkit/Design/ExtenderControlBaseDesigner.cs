using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.Design;

namespace AjaxControlToolkit.Design
{
	// Token: 0x0200000B RID: 11
	public class ExtenderControlBaseDesigner<T> : ExtenderControlDesigner, IExtenderProvider where T : ExtenderControlBase
	{
		// Token: 0x06000040 RID: 64 RVA: 0x0000279C File Offset: 0x0000099C
		static ExtenderControlBaseDesigner()
		{
			TypeDescriptor.AddAttributes(typeof(ExtenderControlBaseDesigner<T>), new Attribute[]
			{
				new ProvidePropertyAttribute("Extender", typeof(Control))
			});
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000027E0 File Offset: 0x000009E0
		protected T ExtenderControl
		{
			get
			{
				return base.Component as T;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000027F4 File Offset: 0x000009F4
		protected virtual string ExtenderPropertyName
		{
			get
			{
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				string format = "{0} ({1})";
				object[] array = new object[2];
				array[0] = TypeDescriptor.GetComponentName(base.Component);
				object[] array2 = array;
				int num = 1;
				T extenderControl = this.ExtenderControl;
				array2[num] = extenderControl.GetType().Name;
				return string.Format(invariantCulture, format, array);
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002844 File Offset: 0x00000A44
		public bool CanExtend(object extendee)
		{
			Control control = extendee as Control;
			bool flag = false;
			if (control != null)
			{
				string id = control.ID;
				T extenderControl = this.ExtenderControl;
				flag = (id == extenderControl.TargetControlID);
				if (flag && this.renameProvider == null)
				{
					this.renameProvider = new ExtenderControlBaseDesigner<T>.ExtenderPropertyRenameDescProv(this, control);
					TypeDescriptor.AddProvider(this.renameProvider, control);
				}
			}
			return flag;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000028A2 File Offset: 0x00000AA2
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.renameProvider != null)
			{
				TypeDescriptor.RemoveProvider(this.renameProvider, base.Component);
				this.renameProvider.Dispose();
				this.renameProvider = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000028DC File Offset: 0x00000ADC
		[TypeConverter(typeof(ExtenderPropertiesTypeDescriptor))]
		[Category("Extenders")]
		[Browsable(true)]
		public object GetExtender(object control)
		{
			Control control2 = control as Control;
			if (control2 != null)
			{
				return new ExtenderPropertiesProxy(this.ExtenderControl, new string[]
				{
					"TargetControlID"
				});
			}
			return null;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002915 File Offset: 0x00000B15
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002920 File Offset: 0x00000B20
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			string[] array = new string[properties.Keys.Count];
			properties.Keys.CopyTo(array, 0);
			foreach (string text in array)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties[text];
				if (text == "TargetControlID")
				{
					TargetControlTypeAttribute targetControlTypeAttribute = (TargetControlTypeAttribute)TypeDescriptor.GetAttributes(this.ExtenderControl)[typeof(TargetControlTypeAttribute)];
					if (targetControlTypeAttribute != null && !targetControlTypeAttribute.IsDefaultAttribute())
					{
						Type type = typeof(TypedControlIDConverter<>).MakeGenericType(new Type[]
						{
							targetControlTypeAttribute.TargetControlType
						});
						properties[text] = TypeDescriptor.CreateProperty(propertyDescriptor.ComponentType, propertyDescriptor, new Attribute[]
						{
							new TypeConverterAttribute(type)
						});
					}
				}
				ExtenderControlPropertyAttribute extenderControlPropertyAttribute = (ExtenderControlPropertyAttribute)propertyDescriptor.Attributes[typeof(ExtenderControlPropertyAttribute)];
				if (extenderControlPropertyAttribute != null && extenderControlPropertyAttribute.IsScriptProperty)
				{
					BrowsableAttribute browsableAttribute = (BrowsableAttribute)propertyDescriptor.Attributes[typeof(BrowsableAttribute)];
					if (browsableAttribute.Browsable == BrowsableAttribute.Yes.Browsable)
					{
						properties[text] = TypeDescriptor.CreateProperty(propertyDescriptor.ComponentType, propertyDescriptor, new Attribute[]
						{
							BrowsableAttribute.No,
							ExtenderVisiblePropertyAttribute.Yes
						});
					}
				}
			}
		}

		// Token: 0x04000011 RID: 17
		private ExtenderControlBaseDesigner<T>.ExtenderPropertyRenameDescProv renameProvider;

		// Token: 0x0200000C RID: 12
		private class ExtenderPropertyRenameDescProv : FilterTypeDescriptionProvider<IComponent>
		{
			// Token: 0x06000049 RID: 73 RVA: 0x00002A92 File Offset: 0x00000C92
			public ExtenderPropertyRenameDescProv(ExtenderControlBaseDesigner<T> owner, IComponent target) : base(target)
			{
				this.owner = owner;
				base.FilterExtendedProperties = true;
			}

			// Token: 0x0600004A RID: 74 RVA: 0x00002AAC File Offset: 0x00000CAC
			protected override PropertyDescriptor ProcessProperty(PropertyDescriptor baseProp)
			{
				if (baseProp.Name == "Extender" && baseProp.ComponentType == this.owner.GetType() && this.owner.ExtenderPropertyName != null)
				{
					return TypeDescriptor.CreateProperty(baseProp.ComponentType, baseProp, new Attribute[]
					{
						new DisplayNameAttribute(this.owner.ExtenderPropertyName)
					});
				}
				return base.ProcessProperty(baseProp);
			}

			// Token: 0x04000012 RID: 18
			private ExtenderControlBaseDesigner<T> owner;
		}
	}
}
