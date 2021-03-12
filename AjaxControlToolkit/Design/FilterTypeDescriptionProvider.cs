using System;
using System.ComponentModel;

namespace AjaxControlToolkit.Design
{
	// Token: 0x02000007 RID: 7
	internal class FilterTypeDescriptionProvider<T> : TypeDescriptionProvider, ICustomTypeDescriptor
	{
		// Token: 0x06000016 RID: 22 RVA: 0x0000228A File Offset: 0x0000048A
		public FilterTypeDescriptionProvider(T target) : base(TypeDescriptor.GetProvider(target))
		{
			this.target = target;
			this.baseProvider = TypeDescriptor.GetProvider(target);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022B5 File Offset: 0x000004B5
		protected T Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000022C0 File Offset: 0x000004C0
		private ICustomTypeDescriptor BaseDescriptor
		{
			get
			{
				if (this.baseDescriptor == null)
				{
					if (this.FilterExtendedProperties)
					{
						this.baseDescriptor = this.baseProvider.GetExtendedTypeDescriptor(this.Target);
					}
					else
					{
						this.baseDescriptor = this.baseProvider.GetTypeDescriptor(this.Target);
					}
				}
				return this.baseDescriptor;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000231D File Offset: 0x0000051D
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002325 File Offset: 0x00000525
		protected bool FilterExtendedProperties
		{
			get
			{
				return this.extended;
			}
			set
			{
				this.extended = value;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000232E File Offset: 0x0000052E
		public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
		{
			if (this.FilterExtendedProperties || instance != this.Target)
			{
				return this.baseProvider.GetTypeDescriptor(objectType, instance);
			}
			return this;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002355 File Offset: 0x00000555
		public override ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance)
		{
			if (this.FilterExtendedProperties && instance == this.Target)
			{
				return this;
			}
			return this.baseProvider.GetExtendedTypeDescriptor(instance);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000237B File Offset: 0x0000057B
		protected virtual PropertyDescriptor ProcessProperty(PropertyDescriptor baseProp)
		{
			return baseProp;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000237E File Offset: 0x0000057E
		public void Dispose()
		{
			this.target = default(T);
			this.baseDescriptor = null;
			this.baseProvider = null;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000239A File Offset: 0x0000059A
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return this.FilterProperties(this.BaseDescriptor.GetProperties());
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023B0 File Offset: 0x000005B0
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = this.BaseDescriptor.GetProperties(attributes);
			return this.FilterProperties(properties);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023D4 File Offset: 0x000005D4
		private PropertyDescriptorCollection FilterProperties(PropertyDescriptorCollection props)
		{
			PropertyDescriptor[] array = new PropertyDescriptor[props.Count];
			props.CopyTo(array, 0);
			bool flag = false;
			for (int i = 0; i < array.Length; i++)
			{
				PropertyDescriptor propertyDescriptor = this.ProcessProperty(array[i]);
				if (propertyDescriptor != array[i])
				{
					flag = true;
					array[i] = propertyDescriptor;
				}
			}
			if (flag)
			{
				props = new PropertyDescriptorCollection(array);
			}
			return props;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002427 File Offset: 0x00000627
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return this.BaseDescriptor.GetAttributes();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002434 File Offset: 0x00000634
		string ICustomTypeDescriptor.GetClassName()
		{
			return this.BaseDescriptor.GetClassName();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002441 File Offset: 0x00000641
		string ICustomTypeDescriptor.GetComponentName()
		{
			return this.BaseDescriptor.GetComponentName();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000244E File Offset: 0x0000064E
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return this.BaseDescriptor.GetConverter();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000245B File Offset: 0x0000065B
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return this.BaseDescriptor.GetDefaultEvent();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002468 File Offset: 0x00000668
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return this.BaseDescriptor.GetDefaultProperty();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002475 File Offset: 0x00000675
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return this.BaseDescriptor.GetEditor(editorBaseType);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002483 File Offset: 0x00000683
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return this.BaseDescriptor.GetEvents(attributes);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002491 File Offset: 0x00000691
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return this.BaseDescriptor.GetEvents();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000249E File Offset: 0x0000069E
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this.BaseDescriptor.GetPropertyOwner(pd);
		}

		// Token: 0x04000007 RID: 7
		private T target;

		// Token: 0x04000008 RID: 8
		private ICustomTypeDescriptor baseDescriptor;

		// Token: 0x04000009 RID: 9
		private TypeDescriptionProvider baseProvider;

		// Token: 0x0400000A RID: 10
		private bool extended;
	}
}
