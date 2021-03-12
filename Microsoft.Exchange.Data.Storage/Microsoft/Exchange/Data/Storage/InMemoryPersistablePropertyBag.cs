using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AF3 RID: 2803
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class InMemoryPersistablePropertyBag : PersistablePropertyBag
	{
		// Token: 0x060065C6 RID: 26054 RVA: 0x001B0AC6 File Offset: 0x001AECC6
		internal InMemoryPersistablePropertyBag(ICollection<PropertyDefinition> propsToReturn)
		{
			this.isDirty = false;
			this.memoryPropertyBag = new MemoryPropertyBag();
			if (propsToReturn != null)
			{
				this.Load(propsToReturn);
			}
		}

		// Token: 0x060065C7 RID: 26055 RVA: 0x001B0AF5 File Offset: 0x001AECF5
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<InMemoryPersistablePropertyBag>(this);
		}

		// Token: 0x060065C8 RID: 26056 RVA: 0x001B0B00 File Offset: 0x001AED00
		public override Stream OpenPropertyStream(PropertyDefinition propertyDefinition, PropertyOpenMode openMode)
		{
			Stream stream = null;
			StorePropertyDefinition storePropertyDefinition = InternalSchema.ToStorePropertyDefinition(propertyDefinition);
			if (openMode != PropertyOpenMode.Create)
			{
				object value = ((IDirectPropertyBag)this.memoryPropertyBag).GetValue(storePropertyDefinition);
				PropertyError propertyError = value as PropertyError;
				if (propertyError == null)
				{
					stream = this.WrapValueInStream(value);
				}
				else if (propertyError.PropertyErrorCode == PropertyErrorCode.RequireStreamed)
				{
					stream = this.streamList[storePropertyDefinition];
				}
				else if (openMode == PropertyOpenMode.ReadOnly)
				{
					throw new ObjectNotFoundException(ServerStrings.StreamPropertyNotFound(storePropertyDefinition.ToString()));
				}
			}
			if (stream == null)
			{
				stream = new MemoryStream();
				((IDirectPropertyBag)this.memoryPropertyBag).SetValue(storePropertyDefinition, new PropertyError(storePropertyDefinition, PropertyErrorCode.RequireStreamed));
				this.streamList[storePropertyDefinition] = stream;
			}
			if (openMode != PropertyOpenMode.ReadOnly)
			{
				this.isDirty = true;
			}
			stream.Seek(0L, SeekOrigin.Begin);
			return new StreamWrapper(stream, false);
		}

		// Token: 0x060065C9 RID: 26057 RVA: 0x001B0BAD File Offset: 0x001AEDAD
		public override PropertyValueTrackingData GetOriginalPropertyInformation(PropertyDefinition propertyDefinition)
		{
			return new PropertyValueTrackingData(PropertyTrackingInformation.Unchanged, null);
		}

		// Token: 0x060065CA RID: 26058 RVA: 0x001B0BB6 File Offset: 0x001AEDB6
		internal override void FlushChanges()
		{
			this.isDirty = false;
			this.memoryPropertyBag.ClearChangeInfo();
		}

		// Token: 0x060065CB RID: 26059 RVA: 0x001B0BCA File Offset: 0x001AEDCA
		internal override void SaveChanges(bool force)
		{
		}

		// Token: 0x17001C15 RID: 7189
		// (get) Token: 0x060065CC RID: 26060 RVA: 0x001B0BCC File Offset: 0x001AEDCC
		// (set) Token: 0x060065CD RID: 26061 RVA: 0x001B0BD4 File Offset: 0x001AEDD4
		internal override PropertyBagSaveFlags SaveFlags
		{
			get
			{
				return this.saveFlags;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<PropertyBagSaveFlags>(value, "value");
				this.saveFlags = value;
			}
		}

		// Token: 0x060065CE RID: 26062 RVA: 0x001B0BE8 File Offset: 0x001AEDE8
		internal override void SetUpdateImapIdFlag()
		{
		}

		// Token: 0x17001C16 RID: 7190
		// (get) Token: 0x060065CF RID: 26063 RVA: 0x001B0BEA File Offset: 0x001AEDEA
		internal override MapiProp MapiProp
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001C17 RID: 7191
		// (get) Token: 0x060065D0 RID: 26064 RVA: 0x001B0BED File Offset: 0x001AEDED
		internal override ICollection<NativeStorePropertyDefinition> AllNativeProperties
		{
			get
			{
				return this.memoryPropertyBag.AllNativeProperties;
			}
		}

		// Token: 0x17001C18 RID: 7192
		// (get) Token: 0x060065D1 RID: 26065 RVA: 0x001B0BFA File Offset: 0x001AEDFA
		public override bool HasAllPropertiesLoaded
		{
			get
			{
				base.CheckDisposed("HasAllPropertiesLoaded::get");
				return this.memoryPropertyBag.HasAllPropertiesLoaded;
			}
		}

		// Token: 0x060065D2 RID: 26066 RVA: 0x001B0C12 File Offset: 0x001AEE12
		public override void Load(ICollection<PropertyDefinition> propsToLoad)
		{
			if (propsToLoad == InternalSchema.ContentConversionProperties)
			{
				this.memoryPropertyBag.SetAllPropertiesLoaded();
				return;
			}
			this.memoryPropertyBag.Load(propsToLoad);
		}

		// Token: 0x060065D3 RID: 26067 RVA: 0x001B0C34 File Offset: 0x001AEE34
		public override void Clear()
		{
			this.memoryPropertyBag.Clear();
		}

		// Token: 0x060065D4 RID: 26068 RVA: 0x001B0C41 File Offset: 0x001AEE41
		protected override void DeleteStoreProperty(StorePropertyDefinition propertyDefinition)
		{
			((IDirectPropertyBag)this.memoryPropertyBag).Delete(propertyDefinition);
			this.isDirty = true;
		}

		// Token: 0x060065D5 RID: 26069 RVA: 0x001B0C56 File Offset: 0x001AEE56
		protected override void SetValidatedStoreProperty(StorePropertyDefinition propertyDefinition, object propertyValue)
		{
			((IDirectPropertyBag)this.memoryPropertyBag).SetValue(propertyDefinition, propertyValue);
			this.isDirty = true;
		}

		// Token: 0x060065D6 RID: 26070 RVA: 0x001B0C6C File Offset: 0x001AEE6C
		protected override object TryGetStoreProperty(StorePropertyDefinition propertyDefinition)
		{
			return ((IDirectPropertyBag)this.memoryPropertyBag).GetValue(propertyDefinition);
		}

		// Token: 0x17001C19 RID: 7193
		// (get) Token: 0x060065D7 RID: 26071 RVA: 0x001B0C7A File Offset: 0x001AEE7A
		public override bool IsDirty
		{
			get
			{
				return this.isDirty;
			}
		}

		// Token: 0x060065D8 RID: 26072 RVA: 0x001B0C82 File Offset: 0x001AEE82
		protected override bool InternalIsPropertyDirty(AtomicStorePropertyDefinition propertyDefinition)
		{
			return ((IDirectPropertyBag)this.memoryPropertyBag).IsDirty(propertyDefinition);
		}

		// Token: 0x17001C1A RID: 7194
		// (get) Token: 0x060065D9 RID: 26073 RVA: 0x001B0C90 File Offset: 0x001AEE90
		public override ICollection<PropertyDefinition> AllFoundProperties
		{
			get
			{
				return this.memoryPropertyBag.AllFoundProperties;
			}
		}

		// Token: 0x060065DA RID: 26074 RVA: 0x001B0CA0 File Offset: 0x001AEEA0
		private Stream WrapValueInStream(object value)
		{
			MemoryStream memoryStream = new MemoryStream();
			if (value != null)
			{
				byte[] array = value as byte[];
				if (array != null)
				{
					memoryStream.Write(array, 0, array.Length);
					memoryStream.Position = 0L;
				}
				else
				{
					string text = value as string;
					if (text == null)
					{
						ExTraceGlobals.StorageTracer.TraceError<Type>((long)this.GetHashCode(), "InMemoryPersistablePropertyBag::WrapValueInStream. The property value cannot be streamed. Type = {0}.", value.GetType());
						throw new InvalidOperationException(ServerStrings.NotStreamablePropertyValue(value.GetType()));
					}
					byte[] bytes = ConvertUtils.UnicodeEncoding.GetBytes(text);
					memoryStream.Write(bytes, 0, bytes.Length);
					memoryStream.Position = 0L;
				}
			}
			return memoryStream;
		}

		// Token: 0x17001C1B RID: 7195
		// (get) Token: 0x060065DB RID: 26075 RVA: 0x001B0D33 File Offset: 0x001AEF33
		// (set) Token: 0x060065DC RID: 26076 RVA: 0x001B0D40 File Offset: 0x001AEF40
		internal override ExTimeZone ExTimeZone
		{
			get
			{
				return this.memoryPropertyBag.ExTimeZone;
			}
			set
			{
				this.memoryPropertyBag.ExTimeZone = value;
			}
		}

		// Token: 0x060065DD RID: 26077 RVA: 0x001B0D4E File Offset: 0x001AEF4E
		protected override bool IsLoaded(NativeStorePropertyDefinition propertyDefinition)
		{
			return ((IDirectPropertyBag)this.memoryPropertyBag).IsLoaded(propertyDefinition);
		}

		// Token: 0x040039DF RID: 14815
		private MemoryPropertyBag memoryPropertyBag;

		// Token: 0x040039E0 RID: 14816
		private readonly Dictionary<PropertyDefinition, Stream> streamList = new Dictionary<PropertyDefinition, Stream>();

		// Token: 0x040039E1 RID: 14817
		private bool isDirty;

		// Token: 0x040039E2 RID: 14818
		private PropertyBagSaveFlags saveFlags;
	}
}
