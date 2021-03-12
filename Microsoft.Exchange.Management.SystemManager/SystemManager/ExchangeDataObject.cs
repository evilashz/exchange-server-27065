using System;
using System.ComponentModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000038 RID: 56
	public abstract class ExchangeDataObject : IConfigurable, INotifyPropertyChanged
	{
		// Token: 0x06000221 RID: 545 RVA: 0x0000896C File Offset: 0x00006B6C
		public ExchangeDataObject()
		{
			this.propertyBag = new ADPropertyBag();
			this.objectState = ObjectState.Unchanged;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00008991 File Offset: 0x00006B91
		internal PropertyBag PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x1700008B RID: 139
		internal virtual object this[ProviderPropertyDefinition key]
		{
			get
			{
				return this.PropertyBag[key];
			}
			set
			{
				bool flag = object.Equals(this.PropertyBag[key], value);
				this.propertyBag[key] = value;
				if (!flag)
				{
					this.OnPropertyChanged(new PropertyChangedEventArgs(key.Name));
				}
			}
		}

		// Token: 0x1700008C RID: 140
		public virtual object this[string key]
		{
			get
			{
				foreach (PropertyDefinition propertyDefinition in this.Schema.AllProperties)
				{
					ProviderPropertyDefinition providerPropertyDefinition = (ProviderPropertyDefinition)propertyDefinition;
					if (providerPropertyDefinition.Name.Equals(key))
					{
						return this.PropertyBag[providerPropertyDefinition];
					}
				}
				return null;
			}
			set
			{
				foreach (PropertyDefinition propertyDefinition in this.Schema.AllProperties)
				{
					ProviderPropertyDefinition providerPropertyDefinition = (ProviderPropertyDefinition)propertyDefinition;
					if (providerPropertyDefinition.Name.Equals(key))
					{
						this.PropertyBag[providerPropertyDefinition] = value;
						break;
					}
				}
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000227 RID: 551
		internal abstract ObjectSchema Schema { get; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00008AD8 File Offset: 0x00006CD8
		public virtual ObjectId Identity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00008ADB File Offset: 0x00006CDB
		public virtual ValidationError[] Validate()
		{
			return ValidationError.None;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00008AE2 File Offset: 0x00006CE2
		public bool IsValid
		{
			get
			{
				return 0 == this.Validate().Length;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00008AF0 File Offset: 0x00006CF0
		public ObjectState ObjectState
		{
			get
			{
				if (this.objectState == ObjectState.Changed)
				{
					return ObjectState.Changed;
				}
				foreach (object obj in this.propertyBag.Keys)
				{
					ProviderPropertyDefinition key = (ProviderPropertyDefinition)obj;
					if (this.propertyBag.IsChanged(key))
					{
						this.objectState = ObjectState.Changed;
						return ObjectState.Changed;
					}
				}
				return ObjectState.Unchanged;
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00008B70 File Offset: 0x00006D70
		public void CopyChangesFrom(IConfigurable source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			ExchangeDataObject exchangeDataObject = source as ExchangeDataObject;
			if (exchangeDataObject == null)
			{
				throw new ArgumentOutOfRangeException("source", "Dev Error: Copying changes from invalid object type");
			}
			foreach (object obj in exchangeDataObject.propertyBag.Keys)
			{
				ProviderPropertyDefinition key = (ProviderPropertyDefinition)obj;
				if (exchangeDataObject.propertyBag.IsChanged(key))
				{
					this[key] = exchangeDataObject[key];
				}
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00008C0C File Offset: 0x00006E0C
		public void CopyFrom(ExchangeDataObject source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.propertyBag = (PropertyBag)source.PropertyBag.Clone();
			this.ResetChangeTracking();
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00008C38 File Offset: 0x00006E38
		public void ResetChangeTracking()
		{
			this.propertyBag.ResetChangeTracking();
			this.objectState = ObjectState.Unchanged;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00008C4C File Offset: 0x00006E4C
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			PropertyChangedEventHandler propertyChangedEventHandler = (PropertyChangedEventHandler)this.Events[ExchangeDataObject.EventPropertyChanged];
			if (propertyChangedEventHandler != null)
			{
				propertyChangedEventHandler(this, e);
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000230 RID: 560 RVA: 0x00008C7A File Offset: 0x00006E7A
		// (remove) Token: 0x06000231 RID: 561 RVA: 0x00008C8D File Offset: 0x00006E8D
		public event PropertyChangedEventHandler PropertyChanged
		{
			add
			{
				this.Events.AddHandler(ExchangeDataObject.EventPropertyChanged, value);
			}
			remove
			{
				this.Events.RemoveHandler(ExchangeDataObject.EventPropertyChanged, value);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00008CA0 File Offset: 0x00006EA0
		protected EventHandlerList Events
		{
			get
			{
				return this.events;
			}
		}

		// Token: 0x04000080 RID: 128
		private PropertyBag propertyBag;

		// Token: 0x04000081 RID: 129
		private ObjectState objectState;

		// Token: 0x04000082 RID: 130
		private static readonly object EventPropertyChanged = new object();

		// Token: 0x04000083 RID: 131
		private EventHandlerList events = new EventHandlerList();
	}
}
