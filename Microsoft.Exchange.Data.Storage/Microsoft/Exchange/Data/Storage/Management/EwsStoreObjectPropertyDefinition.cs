using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009CA RID: 2506
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class EwsStoreObjectPropertyDefinition : SimplePropertyDefinition
	{
		// Token: 0x06005C95 RID: 23701 RVA: 0x001818B8 File Offset: 0x0017FAB8
		public EwsStoreObjectPropertyDefinition(string name, ExchangeObjectVersion versionAdded, Type type, PropertyDefinitionFlags flags, object defaultValue, object initialValue, PropertyDefinitionBase storePropertyDefinition) : this(name, versionAdded, type, flags, defaultValue, initialValue, storePropertyDefinition, null)
		{
		}

		// Token: 0x06005C96 RID: 23702 RVA: 0x001818D8 File Offset: 0x0017FAD8
		public EwsStoreObjectPropertyDefinition(string name, ExchangeObjectVersion versionAdded, Type type, PropertyDefinitionFlags flags, object defaultValue, object initialValue, PropertyDefinitionBase storePropertyDefinition, Action<Item, object> itemPropertySetter) : base(name, versionAdded ?? EwsHelper.EwsVersionToExchangeObjectVersion(storePropertyDefinition.Version), type, flags, defaultValue, initialValue)
		{
			if (storePropertyDefinition == null)
			{
				throw new ArgumentNullException("storePropertyDefinition");
			}
			if (storePropertyDefinition is PropertyDefinition)
			{
				if (itemPropertySetter == null && !this.IsReadOnly && storePropertyDefinition != ItemSchema.Attachments)
				{
					throw new ArgumentException("ItemPropertySetter must be provided for writable first class property.");
				}
			}
			else if (storePropertyDefinition is ExtendedPropertyDefinition && itemPropertySetter != null)
			{
				throw new ArgumentException("ItemPropertySetter shouldn't be provided for extended property.");
			}
			this.storePropertyDefinition = storePropertyDefinition;
			this.ItemPropertySetter = itemPropertySetter;
		}

		// Token: 0x1700196E RID: 6510
		// (get) Token: 0x06005C97 RID: 23703 RVA: 0x00181964 File Offset: 0x0017FB64
		public PropertyDefinitionBase StorePropertyDefinition
		{
			get
			{
				return this.storePropertyDefinition;
			}
		}

		// Token: 0x1700196F RID: 6511
		// (get) Token: 0x06005C98 RID: 23704 RVA: 0x0018196C File Offset: 0x0017FB6C
		// (set) Token: 0x06005C99 RID: 23705 RVA: 0x00181974 File Offset: 0x0017FB74
		public Action<Item, object> ItemPropertySetter { get; private set; }

		// Token: 0x17001970 RID: 6512
		// (get) Token: 0x06005C9A RID: 23706 RVA: 0x0018197D File Offset: 0x0017FB7D
		public bool ReturnOnBind
		{
			get
			{
				return (base.PropertyDefinitionFlags & PropertyDefinitionFlags.ReturnOnBind) != PropertyDefinitionFlags.None;
			}
		}

		// Token: 0x06005C9B RID: 23707 RVA: 0x00181991 File Offset: 0x0017FB91
		public bool GetItemProperty(Item item, out object result)
		{
			if (this.storePropertyDefinition == ItemSchema.Attachments)
			{
				return this.TryGetAttachmentItemProperty(item, out result);
			}
			return item.TryGetProperty(this.storePropertyDefinition, ref result);
		}

		// Token: 0x06005C9C RID: 23708 RVA: 0x001819B8 File Offset: 0x0017FBB8
		public void SetItemProperty(Item item, object value)
		{
			if (value != null && this.StorePropertyDefinition.Type != value.GetType())
			{
				value = Convert.ChangeType(value, EwsStoreValueConverter.GetStorePropertyDefinitionActualType(this));
				if (value is Array && ((Array)value).Length == 0)
				{
					value = null;
				}
			}
			if (this.ItemPropertySetter != null)
			{
				this.ItemPropertySetter(item, value);
				return;
			}
			if (this.storePropertyDefinition == ItemSchema.Attachments)
			{
				this.SetAttachmentItemProperty(item, value);
				return;
			}
			EwsStoreObjectPropertyDefinition.SetExtendedPropertyItemProperty(item, (ExtendedPropertyDefinition)this.storePropertyDefinition, value);
		}

		// Token: 0x06005C9D RID: 23709 RVA: 0x00181A44 File Offset: 0x0017FC44
		private static void SetExtendedPropertyItemProperty(Item item, ExtendedPropertyDefinition storeProperty, object value)
		{
			if (value == null)
			{
				item.RemoveExtendedProperty(storeProperty);
				return;
			}
			item.SetExtendedProperty(storeProperty, value);
		}

		// Token: 0x06005C9E RID: 23710 RVA: 0x00181A5C File Offset: 0x0017FC5C
		private static FileAttachment GetPropertyAttachment(Item item, string propertyName)
		{
			FileAttachment fileAttachment = null;
			if (item.Attachments != null)
			{
				for (int i = 0; i < item.Attachments.Count; i++)
				{
					if (item.Attachments[i].Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase))
					{
						fileAttachment = (item.Attachments[i] as FileAttachment);
						if (fileAttachment != null)
						{
							break;
						}
					}
				}
			}
			return fileAttachment;
		}

		// Token: 0x06005C9F RID: 23711 RVA: 0x00181ABC File Offset: 0x0017FCBC
		private bool TryGetAttachmentItemProperty(Item item, out object result)
		{
			FileAttachment propertyAttachment = EwsStoreObjectPropertyDefinition.GetPropertyAttachment(item, base.Name);
			result = null;
			if (propertyAttachment != null)
			{
				int num = 2;
				Exception ex;
				do
				{
					ex = null;
					try
					{
						propertyAttachment.Load();
						goto IL_64;
					}
					catch (ServiceRemoteException ex2)
					{
						if (ex2 is ServiceResponseException && ((ServiceResponseException)ex2).ErrorCode == 131)
						{
							goto IL_64;
						}
						ex = ex2;
					}
					catch (ServiceLocalException ex3)
					{
						ex = ex3;
					}
				}
				while (num-- > 0);
				throw new DataSourceOperationException(new LocalizedString(ex.Message), ex);
				IL_64:
				if (base.Type != typeof(byte[]))
				{
					if (propertyAttachment.Content != null && propertyAttachment.Content.Length > 0)
					{
						result = EwsStoreValueConverter.DeserializeFromBinary(propertyAttachment.Content);
					}
				}
				else
				{
					result = propertyAttachment.Content;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06005CA0 RID: 23712 RVA: 0x00181B90 File Offset: 0x0017FD90
		private void SetAttachmentItemProperty(Item item, object value)
		{
			FileAttachment propertyAttachment = EwsStoreObjectPropertyDefinition.GetPropertyAttachment(item, base.Name);
			if (value == null)
			{
				if (propertyAttachment != null)
				{
					item.Attachments.Remove(propertyAttachment);
					return;
				}
			}
			else
			{
				byte[] array = value as byte[];
				if (array == null)
				{
					array = EwsStoreValueConverter.SerializeToBinary(value);
				}
				if (propertyAttachment != null)
				{
					item.Attachments.Remove(propertyAttachment);
				}
				item.Attachments.AddFileAttachment(base.Name, array);
			}
		}

		// Token: 0x040032F4 RID: 13044
		[NonSerialized]
		private PropertyDefinitionBase storePropertyDefinition;
	}
}
