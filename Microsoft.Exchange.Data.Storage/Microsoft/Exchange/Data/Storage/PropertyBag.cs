using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200008E RID: 142
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class PropertyBag : IValidatablePropertyBag, IDirectPropertyBag, ILocationIdentifierSetter
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x00046030 File Offset: 0x00044230
		private ExchangeOperationContext operationContext
		{
			get
			{
				if (this.Context == null || this.Context.Session == null)
				{
					return null;
				}
				return this.Context.Session.OperationContext;
			}
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00046059 File Offset: 0x00044259
		internal PropertyBag()
		{
			this.storageItf = new PropertyBag.BasicPropertyStore(this);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0004606D File Offset: 0x0004426D
		internal PropertyBag(PropertyBag propertyBag) : this()
		{
			if (propertyBag.context != null)
			{
				this.context = new PropertyBagContext(propertyBag.context);
			}
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0004608E File Offset: 0x0004428E
		public static explicit operator PropertyBag.BasicPropertyStore(PropertyBag propertyBag)
		{
			return propertyBag.storageItf;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00046096 File Offset: 0x00044296
		public static T CheckPropertyValue<T>(StorePropertyDefinition propertyDefinition, object propertyValue)
		{
			PropertyBag.EnsurePropertyIsNotStreamable(propertyDefinition);
			PropertyBag.ThrowIfPropertyError(propertyDefinition, propertyValue);
			return (T)((object)propertyValue);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x000460AC File Offset: 0x000442AC
		public static T CheckPropertyValue<T>(StorePropertyDefinition propertyDefinition, object propertyValue, T defaultPropertyValue)
		{
			PropertyBag.EnsurePropertyIsNotStreamable(propertyDefinition);
			if (propertyValue == null)
			{
				return defaultPropertyValue;
			}
			PropertyError propertyError = propertyValue as PropertyError;
			if (propertyError == null)
			{
				return (T)((object)propertyValue);
			}
			if (propertyError.PropertyErrorCode == PropertyErrorCode.NotFound)
			{
				return defaultPropertyValue;
			}
			throw PropertyError.ToException(new PropertyError[]
			{
				propertyError
			});
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x000460F4 File Offset: 0x000442F4
		internal static T? CheckNullablePropertyValue<T>(StorePropertyDefinition propertyDefinition, object propertyValue) where T : struct
		{
			PropertyBag.EnsurePropertyIsNotStreamable(propertyDefinition);
			if (propertyValue == null)
			{
				return null;
			}
			PropertyError propertyError = propertyValue as PropertyError;
			if (propertyError == null)
			{
				return new T?((T)((object)propertyValue));
			}
			if (propertyError.PropertyErrorCode == PropertyErrorCode.NotFound)
			{
				return null;
			}
			throw PropertyError.ToException(new PropertyError[]
			{
				propertyError
			});
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00046150 File Offset: 0x00044350
		private static void ThrowIfPropertyError(StorePropertyDefinition propertyDefinition, object propertyValue)
		{
			if (propertyValue == null)
			{
				throw PropertyError.ToException(new PropertyError[]
				{
					new PropertyError(propertyDefinition, PropertyErrorCode.NullValue)
				});
			}
			if (PropertyError.IsPropertyError(propertyValue))
			{
				throw PropertyError.ToException(new PropertyError[]
				{
					(PropertyError)propertyValue
				});
			}
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00046197 File Offset: 0x00044397
		private static void EnsurePropertyIsNotStreamable(StorePropertyDefinition propertyDefinition)
		{
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x000461A0 File Offset: 0x000443A0
		private object FixType(StorePropertyDefinition propertyDefinition, object value)
		{
			if (value == null)
			{
				return value;
			}
			Type type = propertyDefinition.Type;
			Type type2 = value.GetType();
			if (type == type2)
			{
				return value;
			}
			if (type.GetTypeInfo().IsPrimitive && type2.GetTypeInfo().IsEnum)
			{
				if (type == Enum.GetUnderlyingType(type2))
				{
					return Convert.ChangeType(value, type);
				}
			}
			else if (type.GetTypeInfo().IsEnum && type2.GetTypeInfo().IsPrimitive && Enum.GetUnderlyingType(type) == type2)
			{
				return Enum.ToObject(type, value);
			}
			if (this.operationContext != null && this.operationContext.IsMoveUser && type == typeof(string[]) && (propertyDefinition == InternalSchema.Categories || propertyDefinition == InternalSchema.Contacts))
			{
				string text = value as string;
				if (text != null)
				{
					return new string[]
					{
						text
					};
				}
				string[] array = value as string[];
				if (array != null)
				{
					if (Array.Exists<string>(array, (string valueItem) => valueItem == null))
					{
						List<string> list = new List<string>(array.Length);
						foreach (string text2 in array)
						{
							if (text2 != null)
							{
								list.Add(text2);
							}
						}
						value = list.ToArray();
					}
				}
			}
			return value;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x000462F8 File Offset: 0x000444F8
		public object[] GetProperties<T>(ICollection<T> propertyDefinitionArray) where T : PropertyDefinition
		{
			if (propertyDefinitionArray == null)
			{
				throw new ArgumentNullException("propertyDefinitionArray");
			}
			object[] array = new object[propertyDefinitionArray.Count];
			int num = 0;
			foreach (T t in propertyDefinitionArray)
			{
				array[num++] = this.TryGetProperty(t);
			}
			return array;
		}

		// Token: 0x170001DD RID: 477
		public object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				return this.GetProperty(propertyDefinition);
			}
			set
			{
				this.SetProperty(propertyDefinition, value);
			}
		}

		// Token: 0x170001DE RID: 478
		public object this[StorePropertyDefinition propertyDefinition]
		{
			get
			{
				return this.GetProperty(propertyDefinition);
			}
			set
			{
				this.SetProperty(propertyDefinition, value);
			}
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00046394 File Offset: 0x00044594
		public void SetProperty(PropertyDefinition propertyDefinition, object value)
		{
			StorePropertyDefinition propertyDefinition2 = InternalSchema.ToStorePropertyDefinition(propertyDefinition);
			this.SetProperty(propertyDefinition2, value);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x000463B0 File Offset: 0x000445B0
		private void SetProperty(StorePropertyDefinition propertyDefinition, object value)
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException("propertyDefinition");
			}
			if (this.Context.IsValidationDisabled)
			{
				propertyDefinition.SetWithoutValidation(this.storageItf, this.FixType(propertyDefinition, value));
				return;
			}
			propertyDefinition.Set(this.operationContext, this.storageItf, this.FixType(propertyDefinition, value));
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00046408 File Offset: 0x00044608
		protected object GetProperty(PropertyDefinition propertyDefinition)
		{
			StorePropertyDefinition propertyDefinition2 = InternalSchema.ToStorePropertyDefinition(propertyDefinition);
			return this.GetProperty(propertyDefinition2);
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00046424 File Offset: 0x00044624
		protected object GetProperty(StorePropertyDefinition propertyDefinition)
		{
			object obj = this.TryGetProperty(propertyDefinition);
			PropertyBag.ThrowIfPropertyError(propertyDefinition, obj);
			return obj;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00046444 File Offset: 0x00044644
		public object TryGetProperty(PropertyDefinition propertyDefinition)
		{
			StorePropertyDefinition propertyDefinition2 = InternalSchema.ToStorePropertyDefinition(propertyDefinition);
			return this.TryGetProperty(propertyDefinition2);
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0004645F File Offset: 0x0004465F
		internal object TryGetProperty(StorePropertyDefinition propertyDefinition)
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException("propertyDefinition");
			}
			return propertyDefinition.Get(this.storageItf);
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0004647C File Offset: 0x0004467C
		public void Delete(PropertyDefinition propertyDefinition)
		{
			StorePropertyDefinition propertyDefinition2 = InternalSchema.ToStorePropertyDefinition(propertyDefinition);
			this.Delete(propertyDefinition2);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00046497 File Offset: 0x00044697
		public void Delete(StorePropertyDefinition propertyDefinition)
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException("propertyDefinition");
			}
			propertyDefinition.Delete(this.storageItf);
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x000464B3 File Offset: 0x000446B3
		public void Load()
		{
			this.Load(null);
		}

		// Token: 0x060009D0 RID: 2512
		public abstract void Load(ICollection<PropertyDefinition> propsToLoad);

		// Token: 0x060009D1 RID: 2513 RVA: 0x000464BC File Offset: 0x000446BC
		public virtual PropertyValueTrackingData GetOriginalPropertyInformation(PropertyDefinition propertyDefinition)
		{
			return PropertyValueTrackingData.PropertyValueTrackDataNotTracked;
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060009D2 RID: 2514
		public abstract bool IsDirty { get; }

		// Token: 0x060009D3 RID: 2515 RVA: 0x000464C4 File Offset: 0x000446C4
		public bool IsPropertyDirty(PropertyDefinition propertyDefinition)
		{
			StorePropertyDefinition propertyDefinition2 = InternalSchema.ToStorePropertyDefinition(propertyDefinition);
			return this.IsPropertyDirty(propertyDefinition2);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x000464DF File Offset: 0x000446DF
		internal bool IsPropertyDirty(StorePropertyDefinition propertyDefinition)
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException("propertyDefinition");
			}
			return propertyDefinition.IsDirty(this.storageItf);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x000464FC File Offset: 0x000446FC
		public T GetValueOrDefault<T>(PropertyDefinition propertyDefinition)
		{
			StorePropertyDefinition propertyDefinition2 = InternalSchema.ToStorePropertyDefinition(propertyDefinition);
			return this.GetValueOrDefault<T>(propertyDefinition2);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00046518 File Offset: 0x00044718
		public T GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition)
		{
			return this.GetValueOrDefault<T>(propertyDefinition, default(T));
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00046538 File Offset: 0x00044738
		public T GetValueOrDefault<T>(PropertyDefinition propertyDefinition, T defaultValue)
		{
			StorePropertyDefinition propertyDefinition2 = InternalSchema.ToStorePropertyDefinition(propertyDefinition);
			return this.GetValueOrDefault<T>(propertyDefinition2, defaultValue);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00046554 File Offset: 0x00044754
		public T GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition, T defaultValue)
		{
			return PropertyBag.CheckPropertyValue<T>(propertyDefinition, this.TryGetProperty(propertyDefinition), defaultValue);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00046564 File Offset: 0x00044764
		public T? GetValueAsNullable<T>(PropertyDefinition propertyDefinition) where T : struct
		{
			StorePropertyDefinition propertyDefinition2 = InternalSchema.ToStorePropertyDefinition(propertyDefinition);
			return this.GetValueAsNullable<T>(propertyDefinition2);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0004657F File Offset: 0x0004477F
		internal T? GetValueAsNullable<T>(StorePropertyDefinition propertyDefinition) where T : struct
		{
			return PropertyBag.CheckNullablePropertyValue<T>(propertyDefinition, this.TryGetProperty(propertyDefinition));
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x0004658E File Offset: 0x0004478E
		PropertyBagContext IDirectPropertyBag.Context
		{
			get
			{
				return this.Context;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x00046596 File Offset: 0x00044796
		public bool IsNew
		{
			get
			{
				return this.Context.CoreState.Origin == Origin.New;
			}
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x000465AB File Offset: 0x000447AB
		bool IDirectPropertyBag.IsLoaded(NativeStorePropertyDefinition propertyDefinition)
		{
			return this.IsLoaded(propertyDefinition);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x000465B4 File Offset: 0x000447B4
		bool IDirectPropertyBag.IsDirty(AtomicStorePropertyDefinition propertyDefinition)
		{
			return this.InternalIsPropertyDirty(propertyDefinition);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x000465BD File Offset: 0x000447BD
		void IDirectPropertyBag.SetValue(StorePropertyDefinition propertyDefinition, object propertyValue)
		{
			this.SetValidatedStoreProperty(propertyDefinition, propertyValue);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x000465C7 File Offset: 0x000447C7
		object IDirectPropertyBag.GetValue(StorePropertyDefinition propertyDefinition)
		{
			return this.TryGetStoreProperty(propertyDefinition);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x000465D0 File Offset: 0x000447D0
		void IDirectPropertyBag.Delete(StorePropertyDefinition propertyDefinition)
		{
			this.DeleteStoreProperty(propertyDefinition);
		}

		// Token: 0x060009E2 RID: 2530
		protected abstract void SetValidatedStoreProperty(StorePropertyDefinition propertyDefinition, object propertyValue);

		// Token: 0x060009E3 RID: 2531
		protected abstract object TryGetStoreProperty(StorePropertyDefinition propertyDefinition);

		// Token: 0x060009E4 RID: 2532
		protected abstract void DeleteStoreProperty(StorePropertyDefinition propertyDefinition);

		// Token: 0x060009E5 RID: 2533
		protected abstract bool InternalIsPropertyDirty(AtomicStorePropertyDefinition propertyDefinition);

		// Token: 0x060009E6 RID: 2534
		protected abstract bool IsLoaded(NativeStorePropertyDefinition propertyDefinition);

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060009E7 RID: 2535
		// (set) Token: 0x060009E8 RID: 2536
		internal abstract ExTimeZone ExTimeZone { get; set; }

		// Token: 0x060009E9 RID: 2537 RVA: 0x000465D9 File Offset: 0x000447D9
		internal IStorePropertyBag AsIStorePropertyBag()
		{
			return new PropertyBag.StorePropertyBagAdaptor(this);
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x000465E1 File Offset: 0x000447E1
		internal virtual PropertyBagContext Context
		{
			get
			{
				if (this.context == null)
				{
					this.context = new PropertyBagContext();
				}
				return this.context;
			}
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000465FC File Offset: 0x000447FC
		public void SetOrDeleteProperty(PropertyDefinition propertyDefinition, object propertyValue)
		{
			StorePropertyDefinition propertyDefinition2 = InternalSchema.ToStorePropertyDefinition(propertyDefinition);
			this.SetOrDeleteProperty(propertyDefinition2, propertyValue);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00046618 File Offset: 0x00044818
		public void SetOrDeleteProperty(StorePropertyDefinition propertyDefinition, object propertyValue)
		{
			if (propertyValue == null || PropertyError.IsPropertyNotFound(propertyValue))
			{
				this.Delete(propertyDefinition);
				return;
			}
			this.SetProperty(propertyDefinition, propertyValue);
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x00046635 File Offset: 0x00044835
		public virtual bool CanIgnoreUnchangedProperties
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00046638 File Offset: 0x00044838
		void ILocationIdentifierSetter.SetLocationIdentifier(uint id)
		{
			if (this.OnLocationIdentifierReached != null)
			{
				this.OnLocationIdentifierReached(id);
			}
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0004664E File Offset: 0x0004484E
		void ILocationIdentifierSetter.SetLocationIdentifier(uint id, LastChangeAction action)
		{
			if (this.OnNamedLocationIdentifierReached != null)
			{
				this.OnNamedLocationIdentifierReached(id, action);
			}
		}

		// Token: 0x04000285 RID: 645
		private readonly PropertyBag.BasicPropertyStore storageItf;

		// Token: 0x04000286 RID: 646
		private PropertyBagContext context;

		// Token: 0x04000287 RID: 647
		internal Action<uint> OnLocationIdentifierReached;

		// Token: 0x04000288 RID: 648
		internal Action<uint, LastChangeAction> OnNamedLocationIdentifierReached;

		// Token: 0x0200008F RID: 143
		internal struct BasicPropertyStore : ILocationIdentifierSetter
		{
			// Token: 0x060009F1 RID: 2545 RVA: 0x00046665 File Offset: 0x00044865
			internal BasicPropertyStore(PropertyBag parent)
			{
				this.parent = parent;
			}

			// Token: 0x060009F2 RID: 2546 RVA: 0x0004666E File Offset: 0x0004486E
			public static explicit operator PropertyBag(PropertyBag.BasicPropertyStore propertyStore)
			{
				return propertyStore.parent;
			}

			// Token: 0x170001E5 RID: 485
			// (get) Token: 0x060009F3 RID: 2547 RVA: 0x00046677 File Offset: 0x00044877
			public PropertyBagContext Context
			{
				get
				{
					return this.parent.Context;
				}
			}

			// Token: 0x170001E6 RID: 486
			// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00046684 File Offset: 0x00044884
			public ExTimeZone TimeZone
			{
				get
				{
					return this.parent.ExTimeZone;
				}
			}

			// Token: 0x170001E7 RID: 487
			// (get) Token: 0x060009F5 RID: 2549 RVA: 0x00046691 File Offset: 0x00044891
			public bool CanIgnoreUnchangedProperties
			{
				get
				{
					return this.parent.CanIgnoreUnchangedProperties;
				}
			}

			// Token: 0x060009F6 RID: 2550 RVA: 0x0004669E File Offset: 0x0004489E
			public void SetValue(AtomicStorePropertyDefinition propertyDefinition, object propertyValue)
			{
				this.parent.SetValidatedStoreProperty(propertyDefinition, propertyValue);
			}

			// Token: 0x060009F7 RID: 2551 RVA: 0x000466AD File Offset: 0x000448AD
			public void SetOrDeleteProperty(AtomicStorePropertyDefinition propertyDefinition, object propertyValue)
			{
				this.parent.SetOrDeleteProperty(propertyDefinition, propertyValue);
			}

			// Token: 0x060009F8 RID: 2552 RVA: 0x000466BC File Offset: 0x000448BC
			public void SetValueWithFixup(AtomicStorePropertyDefinition propertyDefinition, object propertyValue)
			{
				this.parent.SetProperty(propertyDefinition, propertyValue);
			}

			// Token: 0x060009F9 RID: 2553 RVA: 0x000466CB File Offset: 0x000448CB
			public void Update(AtomicStorePropertyDefinition propertyDefinition, object propertyValue)
			{
				if (propertyValue == null || PropertyError.IsPropertyNotFound(propertyValue))
				{
					this.parent.DeleteStoreProperty(propertyDefinition);
					return;
				}
				this.parent.SetProperty(propertyDefinition, propertyValue);
			}

			// Token: 0x060009FA RID: 2554 RVA: 0x000466F2 File Offset: 0x000448F2
			public object GetValue(AtomicStorePropertyDefinition propertyDefinition)
			{
				return this.parent.TryGetStoreProperty(propertyDefinition);
			}

			// Token: 0x060009FB RID: 2555 RVA: 0x00046700 File Offset: 0x00044900
			public object GetValue(SmartPropertyDefinition propertyDefinition)
			{
				return this.parent.TryGetProperty(propertyDefinition);
			}

			// Token: 0x060009FC RID: 2556 RVA: 0x00046710 File Offset: 0x00044910
			public T GetValueOrDefault<T>(AtomicStorePropertyDefinition propertyDefinition)
			{
				return this.GetValueOrDefault<T>(propertyDefinition, default(T));
			}

			// Token: 0x060009FD RID: 2557 RVA: 0x0004672D File Offset: 0x0004492D
			public T GetValueOrDefault<T>(AtomicStorePropertyDefinition propertyDefinition, T defaultValue)
			{
				return this.parent.GetValueOrDefault<T>(propertyDefinition, defaultValue);
			}

			// Token: 0x060009FE RID: 2558 RVA: 0x0004673C File Offset: 0x0004493C
			public T? GetValueAsNullable<T>(AtomicStorePropertyDefinition propertyDefinition) where T : struct
			{
				return this.parent.GetValueAsNullable<T>(propertyDefinition);
			}

			// Token: 0x060009FF RID: 2559 RVA: 0x0004674A File Offset: 0x0004494A
			public void Delete(AtomicStorePropertyDefinition propertyDefinition)
			{
				this.parent.DeleteStoreProperty(propertyDefinition);
			}

			// Token: 0x06000A00 RID: 2560 RVA: 0x00046758 File Offset: 0x00044958
			public bool IsLoaded(NativeStorePropertyDefinition propertyDefinition)
			{
				return this.parent.IsLoaded(propertyDefinition);
			}

			// Token: 0x06000A01 RID: 2561 RVA: 0x00046766 File Offset: 0x00044966
			public bool IsDirty(AtomicStorePropertyDefinition propertyDefinition)
			{
				return this.parent.InternalIsPropertyDirty(propertyDefinition);
			}

			// Token: 0x06000A02 RID: 2562 RVA: 0x00046774 File Offset: 0x00044974
			public void SetLocationIdentifier(uint id)
			{
				((ILocationIdentifierSetter)this.parent).SetLocationIdentifier(id);
			}

			// Token: 0x06000A03 RID: 2563 RVA: 0x00046782 File Offset: 0x00044982
			public void SetLocationIdentifier(uint id, LastChangeAction action)
			{
				((ILocationIdentifierSetter)this.parent).SetLocationIdentifier(id, action);
			}

			// Token: 0x0400028A RID: 650
			private readonly PropertyBag parent;
		}

		// Token: 0x02000090 RID: 144
		protected sealed class StorePropertyBagAdaptor : IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag
		{
			// Token: 0x06000A04 RID: 2564 RVA: 0x00046791 File Offset: 0x00044991
			internal StorePropertyBagAdaptor(PropertyBag propertyBag)
			{
				this.propertyBag = propertyBag;
			}

			// Token: 0x170001E8 RID: 488
			// (get) Token: 0x06000A05 RID: 2565 RVA: 0x000467A0 File Offset: 0x000449A0
			internal PropertyBag PropertyBag
			{
				get
				{
					return this.propertyBag;
				}
			}

			// Token: 0x170001E9 RID: 489
			// (get) Token: 0x06000A06 RID: 2566 RVA: 0x000467A8 File Offset: 0x000449A8
			public bool IsDirty
			{
				get
				{
					return this.propertyBag.IsDirty;
				}
			}

			// Token: 0x06000A07 RID: 2567 RVA: 0x000467B5 File Offset: 0x000449B5
			public bool IsPropertyDirty(PropertyDefinition propertyDefinition)
			{
				return this.propertyBag.IsPropertyDirty(propertyDefinition);
			}

			// Token: 0x06000A08 RID: 2568 RVA: 0x000467C3 File Offset: 0x000449C3
			public void Load()
			{
				this.Load(null);
			}

			// Token: 0x06000A09 RID: 2569 RVA: 0x000467CC File Offset: 0x000449CC
			public void Load(ICollection<PropertyDefinition> propertyDefinitions)
			{
				this.propertyBag.Load(propertyDefinitions);
			}

			// Token: 0x06000A0A RID: 2570 RVA: 0x000467DA File Offset: 0x000449DA
			public Stream OpenPropertyStream(PropertyDefinition propertyDefinition, PropertyOpenMode openMode)
			{
				throw new NotSupportedException(ServerStrings.ExPropertyNotStreamable(propertyDefinition.ToString()));
			}

			// Token: 0x06000A0B RID: 2571 RVA: 0x000467F1 File Offset: 0x000449F1
			public object TryGetProperty(PropertyDefinition propertyDefinition)
			{
				return this.propertyBag.TryGetProperty(propertyDefinition);
			}

			// Token: 0x06000A0C RID: 2572 RVA: 0x000467FF File Offset: 0x000449FF
			public void Delete(PropertyDefinition propertyDefinition)
			{
				this.propertyBag.Delete(propertyDefinition);
			}

			// Token: 0x06000A0D RID: 2573 RVA: 0x0004680D File Offset: 0x00044A0D
			public T GetValueOrDefault<T>(PropertyDefinition propertyDefinition, T defaultValue)
			{
				return this.propertyBag.GetValueOrDefault<T>(propertyDefinition, defaultValue);
			}

			// Token: 0x06000A0E RID: 2574 RVA: 0x0004681C File Offset: 0x00044A1C
			public void SetOrDeleteProperty(PropertyDefinition propertyDefinition, object propertyValue)
			{
				this.propertyBag.SetOrDeleteProperty(propertyDefinition, propertyValue);
			}

			// Token: 0x170001EA RID: 490
			public object this[PropertyDefinition propertyDefinition]
			{
				get
				{
					return this.propertyBag[propertyDefinition];
				}
				set
				{
					this.propertyBag[propertyDefinition] = value;
				}
			}

			// Token: 0x06000A11 RID: 2577 RVA: 0x00046848 File Offset: 0x00044A48
			public void SetProperties(ICollection<PropertyDefinition> propertyDefinitionArray, object[] propertyValuesArray)
			{
				int num = 0;
				foreach (PropertyDefinition propertyDefinition in propertyDefinitionArray)
				{
					this.propertyBag[propertyDefinition] = propertyValuesArray[num++];
				}
			}

			// Token: 0x06000A12 RID: 2578 RVA: 0x000468A0 File Offset: 0x00044AA0
			public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
			{
				return this.propertyBag.GetProperties<PropertyDefinition>(propertyDefinitionArray);
			}

			// Token: 0x0400028B RID: 651
			private readonly PropertyBag propertyBag;
		}
	}
}
