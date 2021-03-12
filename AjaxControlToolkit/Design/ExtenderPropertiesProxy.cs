using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;

namespace AjaxControlToolkit.Design
{
	// Token: 0x0200000A RID: 10
	internal class ExtenderPropertiesProxy : ICustomTypeDescriptor
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000251E File Offset: 0x0000071E
		private object Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002526 File Offset: 0x00000726
		public ExtenderPropertiesProxy(object target, params string[] propsToHide)
		{
			this.target = target;
			this.propsToHide = propsToHide;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002558 File Offset: 0x00000758
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = TypeDescriptor.GetProperties(this.Target);
			if (this.propsToHide != null && this.propsToHide.Length > 0)
			{
				List<PropertyDescriptor> list = new List<PropertyDescriptor>();
				for (int i = 0; i < propertyDescriptorCollection.Count; i++)
				{
					PropertyDescriptor prop = propertyDescriptorCollection[i];
					ExtenderControlPropertyAttribute extenderControlPropertyAttribute = (ExtenderControlPropertyAttribute)prop.Attributes[typeof(ExtenderControlPropertyAttribute)];
					if (extenderControlPropertyAttribute != null)
					{
						ExtenderVisiblePropertyAttribute extenderVisiblePropertyAttribute = (ExtenderVisiblePropertyAttribute)prop.Attributes[typeof(ExtenderVisiblePropertyAttribute)];
						if (extenderVisiblePropertyAttribute != null && extenderVisiblePropertyAttribute.Value)
						{
							int num = Array.FindIndex<string>(this.propsToHide, (string s) => s == prop.Name);
							if (num == -1)
							{
								IDReferencePropertyAttribute idreferencePropertyAttribute = (IDReferencePropertyAttribute)prop.Attributes[typeof(IDReferencePropertyAttribute)];
								Attribute attribute = prop.Attributes[typeof(TypeConverterAttribute)];
								if (idreferencePropertyAttribute != null && !idreferencePropertyAttribute.IsDefaultAttribute())
								{
									Type type = typeof(TypedControlIDConverter<Control>).GetGenericTypeDefinition();
									type = type.MakeGenericType(new Type[]
									{
										idreferencePropertyAttribute.ReferencedControlType
									});
									attribute = new TypeConverterAttribute(type);
								}
								prop = TypeDescriptor.CreateProperty(prop.ComponentType, prop, new Attribute[]
								{
									BrowsableAttribute.Yes,
									attribute
								});
								list.Add(prop);
							}
						}
					}
				}
				propertyDescriptorCollection = new PropertyDescriptorCollection(list.ToArray());
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002710 File Offset: 0x00000910
		System.ComponentModel.AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this.Target);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000271D File Offset: 0x0000091D
		string ICustomTypeDescriptor.GetClassName()
		{
			return TypeDescriptor.GetClassName(this.Target);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000272A File Offset: 0x0000092A
		string ICustomTypeDescriptor.GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this.Target);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002737 File Offset: 0x00000937
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return TypeDescriptor.GetConverter(this.Target);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002744 File Offset: 0x00000944
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this.Target);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002751 File Offset: 0x00000951
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this.Target);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000275E File Offset: 0x0000095E
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this.Target, editorBaseType);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000276C File Offset: 0x0000096C
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this.Target, attributes);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000277A File Offset: 0x0000097A
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return TypeDescriptor.GetEvents(this.Target);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002787 File Offset: 0x00000987
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return TypeDescriptor.GetProperties(this.Target);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002794 File Offset: 0x00000994
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this.Target;
		}

		// Token: 0x0400000F RID: 15
		private object target;

		// Token: 0x04000010 RID: 16
		private string[] propsToHide;
	}
}
