using System;
using System.Globalization;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.SchemaConverter;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x020001F8 RID: 504
	internal abstract class XsoProperty : PropertyBase
	{
		// Token: 0x060013BE RID: 5054 RVA: 0x00071B9E File Offset: 0x0006FD9E
		public XsoProperty(StorePropertyDefinition propertyDef)
		{
			this.PropertyDef = propertyDef;
			if (this.PropertyDef != null)
			{
				this.prefetchPropertyDef = new PropertyDefinition[1];
				this.prefetchPropertyDef[0] = this.PropertyDef;
			}
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00071BD6 File Offset: 0x0006FDD6
		public XsoProperty(StorePropertyDefinition propertyDef, PropertyType type)
		{
			this.PropertyDef = propertyDef;
			this.type = type;
			if (this.PropertyDef != null)
			{
				this.prefetchPropertyDef = new PropertyDefinition[1];
				this.prefetchPropertyDef[0] = this.PropertyDef;
			}
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00071C15 File Offset: 0x0006FE15
		public XsoProperty(StorePropertyDefinition propertyDef, PropertyDefinition[] prefetchPropDef)
		{
			this.PropertyDef = propertyDef;
			this.prefetchPropertyDef = prefetchPropDef;
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x00071C32 File Offset: 0x0006FE32
		public XsoProperty(StorePropertyDefinition propertyDef, PropertyType type, PropertyDefinition[] prefetchPropDef)
		{
			this.PropertyDef = propertyDef;
			this.type = type;
			this.prefetchPropertyDef = prefetchPropDef;
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x00071C56 File Offset: 0x0006FE56
		public StorePropertyDefinition StorePropertyDefinition
		{
			get
			{
				return this.PropertyDef;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x00071C60 File Offset: 0x0006FE60
		// (set) Token: 0x060013C4 RID: 5060 RVA: 0x00071CCA File Offset: 0x0006FECA
		public PropertyType Type
		{
			get
			{
				if (this.type != PropertyType.ReadOnlyForNonDraft)
				{
					return this.type;
				}
				bool flag = false;
				if (this.item != null)
				{
					object obj;
					if ((obj = this.item.TryGetProperty(MessageItemSchema.IsDraft)) is PropertyError)
					{
						AirSyncDiagnostics.TraceError<PropertyErrorCode>(ExTraceGlobals.XsoTracer, this, "Error retrieving IsDraft property for item. ErrorCode:{0}", ((PropertyError)obj).PropertyErrorCode);
					}
					else
					{
						flag = (bool)obj;
					}
				}
				if (!flag)
				{
					return PropertyType.ReadOnly;
				}
				return PropertyType.ReadWrite;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x00071CD3 File Offset: 0x0006FED3
		// (set) Token: 0x060013C6 RID: 5062 RVA: 0x00071CDB File Offset: 0x0006FEDB
		public string[] SupportedItemClasses
		{
			get
			{
				return this.supportedItemClasses;
			}
			set
			{
				this.supportedItemClasses = value;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x00071CE4 File Offset: 0x0006FEE4
		// (set) Token: 0x060013C8 RID: 5064 RVA: 0x00071CEC File Offset: 0x0006FEEC
		protected StorePropertyDefinition PropertyDef
		{
			get
			{
				return this.propertyDef;
			}
			set
			{
				this.propertyDef = value;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x00071CF5 File Offset: 0x0006FEF5
		// (set) Token: 0x060013CA RID: 5066 RVA: 0x00071CFD File Offset: 0x0006FEFD
		protected StoreObject XsoItem
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
			}
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x00071D06 File Offset: 0x0006FF06
		public virtual void Bind(StoreObject item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this.XsoItem = item;
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x00071D20 File Offset: 0x0006FF20
		public void ComputePropertyState()
		{
			if (this.supportedItemClasses != null)
			{
				bool flag = false;
				foreach (string value in this.supportedItemClasses)
				{
					if (this.item.ClassName.StartsWith(value, StringComparison.OrdinalIgnoreCase))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					base.State = PropertyState.NotSupported;
					return;
				}
			}
			base.State = PropertyState.Modified;
			if (this.PropertyDef != null)
			{
				PropertyError propertyError = this.XsoItem.TryGetProperty(this.PropertyDef) as PropertyError;
				CalendarItemOccurrence calendarItemOccurrence;
				if (propertyError != null)
				{
					if (propertyError.PropertyErrorCode == PropertyErrorCode.NotFound || propertyError.PropertyErrorCode == PropertyErrorCode.GetCalculatedPropertyError)
					{
						base.State = PropertyState.SetToDefault;
						return;
					}
					if (propertyError.PropertyErrorCode != PropertyErrorCode.NotEnoughMemory)
					{
						throw new ConversionException(string.Format(CultureInfo.InvariantCulture, "Property = {0} could not be fetched, Property Error code = {1}, Description = {2}", new object[]
						{
							this.PropertyDef,
							propertyError.PropertyErrorCode,
							propertyError.PropertyErrorDescription
						}));
					}
					if (this is XsoStringProperty)
					{
						base.State = PropertyState.Stream;
						return;
					}
					base.State = PropertyState.SetToDefault;
					return;
				}
				else if ((calendarItemOccurrence = (this.XsoItem as CalendarItemOccurrence)) != null)
				{
					if (calendarItemOccurrence.IsModifiedProperty(this.PropertyDef))
					{
						return;
					}
					foreach (PropertyDefinition propertyDefinition in this.prefetchPropertyDef)
					{
						if (calendarItemOccurrence.IsModifiedProperty(propertyDefinition))
						{
							return;
						}
					}
					base.State = PropertyState.Unmodified;
				}
			}
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x00071E78 File Offset: 0x00070078
		public override void CopyFrom(IProperty srcProperty)
		{
			if (srcProperty == null)
			{
				throw new ArgumentNullException("srcProperty");
			}
			if (srcProperty.SchemaLinkId != base.SchemaLinkId)
			{
				throw new ConversionException("Schema link id's must match in CopyFrom()");
			}
			if (this.XsoItem == null)
			{
				throw new ConversionException("Cannot copy property into null XSO Item");
			}
			switch (srcProperty.State)
			{
			case PropertyState.Uninitialized:
				throw new ConversionException("Can't CopyFrom uninitialized property");
			case PropertyState.SetToDefault:
				if (this.type == PropertyType.ReadAndRequiredForWrite)
				{
					throw new ConversionException(4, string.Format(CultureInfo.InvariantCulture, "Property {0} is required to be present in client side changes", new object[]
					{
						this
					}));
				}
				this.InternalSetToDefault(srcProperty);
				break;
			case PropertyState.Modified:
				this.InternalCopyFromModified(srcProperty);
				return;
			case PropertyState.Unmodified:
				return;
			case PropertyState.Stream:
				break;
			default:
				return;
			}
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x00071F2B File Offset: 0x0007012B
		public PropertyDefinition[] GetPrefetchProperties()
		{
			return this.prefetchPropertyDef;
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00071F34 File Offset: 0x00070134
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"( Base: ",
				base.ToString(),
				", propertyDefinition: ",
				this.PropertyDef,
				", type: ",
				this.type,
				", item: ",
				this.XsoItem,
				", state: ",
				base.State,
				")"
			});
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x00071FB8 File Offset: 0x000701B8
		public override void Unbind()
		{
			try
			{
				base.State = PropertyState.Uninitialized;
				this.XsoItem = null;
			}
			finally
			{
				base.Unbind();
			}
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x00071FEC File Offset: 0x000701EC
		protected virtual void InternalCopyFromModified(IProperty srcProperty)
		{
			if (this.PropertyDef == null)
			{
				throw new ConversionException("this.propertyDef is null");
			}
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x00072004 File Offset: 0x00070204
		protected virtual bool IsItemDelegated()
		{
			if (this.XsoItem is MeetingRequest || this.XsoItem is MeetingCancellation)
			{
				MeetingMessage meetingMessage = this.XsoItem as MeetingMessage;
				if (meetingMessage != null && meetingMessage.IsDelegated())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x00072048 File Offset: 0x00070248
		protected virtual void InternalSetToDefault(IProperty srcProperty)
		{
			if (this.PropertyDef is NativeStorePropertyDefinition)
			{
				try
				{
					this.XsoItem.DeleteProperties(new PropertyDefinition[]
					{
						this.PropertyDef
					});
				}
				catch (NotSupportedException ex)
				{
					AirSyncDiagnostics.TraceError(ExTraceGlobals.XsoTracer, this, "Error trying to delete properties in XsoProperty.InternalSetToDefault\r\nType of XsoProperty: {0}\r\nType of XsoItem: {1}\r\nClass of XsoItem: {2}\r\nPropertyDef: {3}\r\nError: {4}", new object[]
					{
						base.GetType(),
						this.XsoItem.GetType(),
						this.XsoItem.ClassName,
						this.PropertyDef,
						ex
					});
					throw;
				}
			}
		}

		// Token: 0x04000C42 RID: 3138
		private StoreObject item;

		// Token: 0x04000C43 RID: 3139
		private PropertyDefinition[] prefetchPropertyDef;

		// Token: 0x04000C44 RID: 3140
		private StorePropertyDefinition propertyDef;

		// Token: 0x04000C45 RID: 3141
		private PropertyType type = PropertyType.ReadWrite;

		// Token: 0x04000C46 RID: 3142
		private string[] supportedItemClasses;
	}
}
