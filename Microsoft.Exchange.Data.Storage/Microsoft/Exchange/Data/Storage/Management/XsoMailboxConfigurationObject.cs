using System;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009FF RID: 2559
	[Serializable]
	public abstract class XsoMailboxConfigurationObject : ConfigurableObject
	{
		// Token: 0x06005D82 RID: 23938 RVA: 0x0018C4FB File Offset: 0x0018A6FB
		public XsoMailboxConfigurationObject() : base(new SimplePropertyBag(XsoMailboxConfigurationObjectSchema.MailboxOwnerId, UserConfigurationObjectSchema.ObjectState, UserConfigurationObjectSchema.ExchangeVersion))
		{
		}

		// Token: 0x17001996 RID: 6550
		// (get) Token: 0x06005D83 RID: 23939
		internal abstract XsoMailboxConfigurationObjectSchema Schema { get; }

		// Token: 0x17001997 RID: 6551
		// (get) Token: 0x06005D84 RID: 23940 RVA: 0x0018C517 File Offset: 0x0018A717
		internal sealed override ObjectSchema ObjectSchema
		{
			get
			{
				return this.Schema;
			}
		}

		// Token: 0x17001998 RID: 6552
		// (get) Token: 0x06005D85 RID: 23941 RVA: 0x0018C51F File Offset: 0x0018A71F
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17001999 RID: 6553
		// (get) Token: 0x06005D86 RID: 23942 RVA: 0x0018C526 File Offset: 0x0018A726
		// (set) Token: 0x06005D87 RID: 23943 RVA: 0x0018C538 File Offset: 0x0018A738
		public ADObjectId MailboxOwnerId
		{
			get
			{
				return (ADObjectId)this[XsoMailboxConfigurationObjectSchema.MailboxOwnerId];
			}
			internal set
			{
				this[XsoMailboxConfigurationObjectSchema.MailboxOwnerId] = value;
			}
		}

		// Token: 0x06005D88 RID: 23944 RVA: 0x0018C548 File Offset: 0x0018A748
		internal void LoadDataFromXsoRows(ADObjectId mailboxOwnerId, object[] objectRow, PropertyDefinition[] xsoPropertyDefinitions)
		{
			if (mailboxOwnerId == null)
			{
				throw new ArgumentNullException("mailboxOwnerId");
			}
			if (objectRow == null)
			{
				throw new ArgumentNullException("objectRow");
			}
			if (xsoPropertyDefinitions == null)
			{
				throw new ArgumentNullException("xsoPropertyDefinitions");
			}
			if (objectRow.Length != xsoPropertyDefinitions.Length)
			{
				throw new ArgumentException("xsoPropertyDefinitions and objectRow length mismatch");
			}
			base.InstantiationErrors.Clear();
			this.MailboxOwnerId = mailboxOwnerId;
			for (int i = 0; i < xsoPropertyDefinitions.Length; i++)
			{
				XsoDriverPropertyDefinition relatedWrapperProperty = this.Schema.GetRelatedWrapperProperty(xsoPropertyDefinitions[i]);
				try
				{
					object obj = objectRow[i];
					StorePropertyDefinition propertyDefinition = InternalSchema.ToStorePropertyDefinition(xsoPropertyDefinitions[i]);
					if (obj != null)
					{
						this.propertyBag.SetField(relatedWrapperProperty, PropertyBag.CheckPropertyValue<object>(propertyDefinition, obj, null));
					}
				}
				catch (StoragePermanentException ex)
				{
					base.InstantiationErrors.Add(new PropertyValidationError(ex.LocalizedString, relatedWrapperProperty, null));
				}
			}
		}

		// Token: 0x06005D89 RID: 23945 RVA: 0x0018C618 File Offset: 0x0018A818
		internal void LoadDataFromXso(ADObjectId mailboxOwnerId, StoreObject storeObject)
		{
			if (storeObject == null)
			{
				throw new ArgumentNullException("storeObject");
			}
			PropertyDefinition[] allDependentXsoProperties = this.Schema.AllDependentXsoProperties;
			this.LoadDataFromXsoRows(mailboxOwnerId, storeObject.GetProperties(allDependentXsoProperties), allDependentXsoProperties);
		}

		// Token: 0x06005D8A RID: 23946 RVA: 0x0018C650 File Offset: 0x0018A850
		internal void SaveDataToXso(StoreObject storeObject, ReadOnlyCollection<XsoDriverPropertyDefinition> ignoredProperties)
		{
			if (storeObject == null)
			{
				throw new ArgumentNullException("storeObject");
			}
			foreach (PropertyDefinition propertyDefinition in this.Schema.AllProperties)
			{
				if (propertyDefinition is XsoDriverPropertyDefinition)
				{
					XsoDriverPropertyDefinition xsoDriverPropertyDefinition = (XsoDriverPropertyDefinition)propertyDefinition;
					if (!xsoDriverPropertyDefinition.IsReadOnly && (ignoredProperties == null || !ignoredProperties.Contains(xsoDriverPropertyDefinition)) && (PropertyFlags.ReadOnly & xsoDriverPropertyDefinition.StorePropertyDefinition.PropertyFlags) == PropertyFlags.None && base.IsChanged(xsoDriverPropertyDefinition))
					{
						object obj = null;
						this.propertyBag.TryGetField((ProviderPropertyDefinition)propertyDefinition, ref obj);
						if (obj != null)
						{
							storeObject[xsoDriverPropertyDefinition.StorePropertyDefinition] = obj;
						}
						else
						{
							storeObject.Delete(xsoDriverPropertyDefinition.StorePropertyDefinition);
						}
					}
				}
			}
		}
	}
}
