using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000647 RID: 1607
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CoreAttachment : DisposableObject, ICoreState, IValidatable
	{
		// Token: 0x06004237 RID: 16951 RVA: 0x0011AA9C File Offset: 0x00118C9C
		internal CoreAttachment(CoreAttachmentCollection parentCollection, AttachmentPropertyBag propertyBag, Origin origin)
		{
			this.parentCollection = parentCollection;
			this.propertyBag = propertyBag;
			this.attachmentNumber = propertyBag.AttachmentNumber;
			this.Origin = origin;
			this.propertyBag.Context.CoreState = this;
			this.propertyBag.Context.Session = this.parentCollection.ContainerItem.Session;
		}

		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x06004238 RID: 16952 RVA: 0x0011AB08 File Offset: 0x00118D08
		public AttachmentPropertyBag PropertyBag
		{
			get
			{
				this.CheckDisposed(null);
				return this.propertyBag;
			}
		}

		// Token: 0x17001379 RID: 4985
		// (get) Token: 0x06004239 RID: 16953 RVA: 0x0011AB17 File Offset: 0x00118D17
		public StoreSession Session
		{
			get
			{
				this.CheckDisposed(null);
				return this.parentCollection.ContainerItem.Session;
			}
		}

		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x0600423A RID: 16954 RVA: 0x0011AB30 File Offset: 0x00118D30
		public AttachmentType AttachmentType
		{
			get
			{
				this.CheckDisposed(null);
				int? attachMethod = this.PropertyBag.TryGetProperty(InternalSchema.AttachMethod) as int?;
				return CoreAttachmentCollection.GetAttachmentType(attachMethod);
			}
		}

		// Token: 0x1700137B RID: 4987
		// (get) Token: 0x0600423B RID: 16955 RVA: 0x0011AB65 File Offset: 0x00118D65
		public int AttachmentNumber
		{
			get
			{
				this.CheckDisposed(null);
				return this.attachmentNumber;
			}
		}

		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x0600423C RID: 16956 RVA: 0x0011AB74 File Offset: 0x00118D74
		// (set) Token: 0x0600423D RID: 16957 RVA: 0x0011AB88 File Offset: 0x00118D88
		public PropertyBagSaveFlags SaveFlags
		{
			get
			{
				this.CheckDisposed(null);
				return this.PropertyBag.SaveFlags;
			}
			set
			{
				this.CheckDisposed(null);
				EnumValidator.ThrowIfInvalid<PropertyBagSaveFlags>(value, "value");
				this.PropertyBag.SaveFlags = value;
			}
		}

		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x0600423E RID: 16958 RVA: 0x0011ABA8 File Offset: 0x00118DA8
		// (set) Token: 0x0600423F RID: 16959 RVA: 0x0011ABB7 File Offset: 0x00118DB7
		public Origin Origin
		{
			get
			{
				this.CheckDisposed(null);
				return this.origin;
			}
			set
			{
				this.CheckDisposed(null);
				EnumValidator.ThrowIfInvalid<Origin>(value);
				this.origin = value;
			}
		}

		// Token: 0x1700137E RID: 4990
		// (get) Token: 0x06004240 RID: 16960 RVA: 0x0011ABCD File Offset: 0x00118DCD
		public ItemLevel ItemLevel
		{
			get
			{
				this.CheckDisposed(null);
				return ItemLevel.Attached;
			}
		}

		// Token: 0x1700137F RID: 4991
		// (get) Token: 0x06004241 RID: 16961 RVA: 0x0011ABD7 File Offset: 0x00118DD7
		public bool IsReadOnly
		{
			get
			{
				this.CheckDisposed(null);
				return this.parentCollection.IsReadOnly;
			}
		}

		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x06004242 RID: 16962 RVA: 0x0011ABEB File Offset: 0x00118DEB
		bool IValidatable.ValidateAllProperties
		{
			get
			{
				this.CheckDisposed(null);
				return this.enableFullValidation || this.Origin == Origin.New;
			}
		}

		// Token: 0x17001381 RID: 4993
		// (get) Token: 0x06004243 RID: 16963 RVA: 0x0011AC07 File Offset: 0x00118E07
		Schema IValidatable.Schema
		{
			get
			{
				this.CheckDisposed(null);
				return this.GetSchema();
			}
		}

		// Token: 0x17001382 RID: 4994
		// (get) Token: 0x06004244 RID: 16964 RVA: 0x0011AC16 File Offset: 0x00118E16
		internal bool IsNew
		{
			get
			{
				this.CheckDisposed(null);
				return this.Origin == Origin.New;
			}
		}

		// Token: 0x17001383 RID: 4995
		// (get) Token: 0x06004245 RID: 16965 RVA: 0x0011AC28 File Offset: 0x00118E28
		internal CoreAttachmentCollection ParentCollection
		{
			get
			{
				this.CheckDisposed(null);
				return this.parentCollection;
			}
		}

		// Token: 0x17001384 RID: 4996
		// (get) Token: 0x06004246 RID: 16966 RVA: 0x0011AC37 File Offset: 0x00118E37
		internal AttachmentId Id
		{
			get
			{
				this.CheckDisposed(null);
				return this.PropertyBag.AttachmentId;
			}
		}

		// Token: 0x17001385 RID: 4997
		// (get) Token: 0x06004247 RID: 16967 RVA: 0x0011AC4B File Offset: 0x00118E4B
		// (set) Token: 0x06004248 RID: 16968 RVA: 0x0011AC64 File Offset: 0x00118E64
		internal bool IsInline
		{
			get
			{
				this.CheckDisposed(null);
				return this.PropertyBag.GetValueOrDefault<bool>(InternalSchema.AttachmentIsInline);
			}
			set
			{
				this.CheckDisposed(null);
				this.PropertyBag.SetOrDeleteProperty(InternalSchema.AttachmentIsInline, value);
				this.PropertyBag.SetOrDeleteProperty(InternalSchema.AttachCalendarHidden, value);
			}
		}

		// Token: 0x17001386 RID: 4998
		// (get) Token: 0x06004249 RID: 16969 RVA: 0x0011AC99 File Offset: 0x00118E99
		internal bool IsCalendarException
		{
			get
			{
				return this.PropertyBag.IsCalendarException;
			}
		}

		// Token: 0x0600424A RID: 16970 RVA: 0x0011ACA8 File Offset: 0x00118EA8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.GetType().FullName);
			stringBuilder.AppendLine();
			if (base.IsDisposed)
			{
				stringBuilder.AppendLine("disposed");
			}
			else
			{
				if (this.parentCollection != null && this.parentCollection.ContainerItem != null)
				{
					stringBuilder.AppendFormat("Item: {0}", this.parentCollection.ContainerItem.ToString());
					stringBuilder.AppendLine();
				}
				if (this.Id != null)
				{
					stringBuilder.AppendFormat("Attachment id: {0}", this.Id.ToBase64String());
					stringBuilder.AppendLine();
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600424B RID: 16971 RVA: 0x0011AD48 File Offset: 0x00118F48
		public void Flush()
		{
			this.CheckDisposed(null);
			this.CoreObjectUpdate();
			PersistablePropertyBag persistablePropertyBag = CoreObject.GetPersistablePropertyBag(this.ParentCollection.ContainerItem);
			if (!persistablePropertyBag.Context.IsValidationDisabled)
			{
				ValidationContext context = new ValidationContext(this.Session);
				Validation.Validate(this, context);
			}
			this.PropertyBag.FlushChanges();
		}

		// Token: 0x0600424C RID: 16972 RVA: 0x0011AD9E File Offset: 0x00118F9E
		public void Save()
		{
			this.CheckDisposed(null);
			this.Flush();
			this.PropertyBag.SaveChanges(false);
			this.origin = Origin.Existing;
		}

		// Token: 0x0600424D RID: 16973 RVA: 0x0011ADC0 File Offset: 0x00118FC0
		public ICoreItem OpenEmbeddedItem(PropertyOpenMode openMode, params PropertyDefinition[] propertiesToLoad)
		{
			EnumValidator<PropertyOpenMode>.ThrowIfInvalid(openMode);
			this.CheckDisposed(null);
			if (openMode == PropertyOpenMode.Create)
			{
				if (this.AttachmentType != AttachmentType.EmbeddedMessage)
				{
					((IDirectPropertyBag)this.PropertyBag).SetValue(InternalSchema.AttachMethod, 5);
				}
			}
			else if (this.AttachmentType != AttachmentType.EmbeddedMessage)
			{
				throw new InvalidOperationException("Cannot get the embedded message of an attachment whose type is not AttachmentType.EmbeddedMessage.");
			}
			bool noMessageDecoding = this.parentCollection.ContainerItem.CharsetDetector.NoMessageDecoding;
			return this.PropertyBag.OpenAttachedItem(openMode, propertiesToLoad, noMessageDecoding);
		}

		// Token: 0x0600424E RID: 16974 RVA: 0x0011AE38 File Offset: 0x00119038
		public PropertyError[] CopyAttachment(CoreAttachment destinationAttachment, CopyPropertiesFlags copyPropertiesFlags, CopySubObjects copySubObjects, NativeStorePropertyDefinition[] excludeProperties)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(destinationAttachment, "destinationAttachment");
			Util.ThrowOnNullArgument(excludeProperties, "excludeProperties");
			EnumValidator.ThrowIfInvalid<CopyPropertiesFlags>(copyPropertiesFlags, "copyPropertiesFlags");
			EnumValidator.ThrowIfInvalid<CopySubObjects>(copySubObjects, "copySubObjects");
			return CoreObject.MapiCopyTo(this.PropertyBag.MapiProp, destinationAttachment.PropertyBag.MapiProp, this.Session, destinationAttachment.Session, copyPropertiesFlags, copySubObjects, excludeProperties);
		}

		// Token: 0x0600424F RID: 16975 RVA: 0x0011AEA4 File Offset: 0x001190A4
		public PropertyError[] CopyProperties(CoreAttachment destinationAttachment, CopyPropertiesFlags copyPropertiesFlags, NativeStorePropertyDefinition[] includeProperties)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(destinationAttachment, "destinationAttachment");
			Util.ThrowOnNullArgument(includeProperties, "includeProperties");
			EnumValidator.ThrowIfInvalid<CopyPropertiesFlags>(copyPropertiesFlags, "copyPropertiesFlags");
			return CoreObject.MapiCopyProps(this.PropertyBag.MapiProp, destinationAttachment.PropertyBag.MapiProp, this.Session, destinationAttachment.Session, copyPropertiesFlags, includeProperties);
		}

		// Token: 0x06004250 RID: 16976 RVA: 0x0011AF02 File Offset: 0x00119102
		void IValidatable.Validate(ValidationContext context, IList<StoreObjectValidationError> validationErrors)
		{
			this.CheckDisposed(null);
			Validation.ValidateProperties(context, this, this.PropertyBag, validationErrors);
		}

		// Token: 0x06004251 RID: 16977 RVA: 0x0011AF1C File Offset: 0x0011911C
		internal static int AttachmentTypeToAttachMethod(AttachmentType type)
		{
			switch (type)
			{
			case AttachmentType.NoAttachment:
				return 0;
			case AttachmentType.Stream:
				return 1;
			case AttachmentType.EmbeddedMessage:
				return 5;
			case AttachmentType.Ole:
				return 6;
			case AttachmentType.Reference:
				return 7;
			default:
				throw new InvalidOperationException("AttachmentTypeToAttachMethod: Invalid attachment type");
			}
		}

		// Token: 0x06004252 RID: 16978 RVA: 0x0011AF5C File Offset: 0x0011915C
		internal static Uri GetUriProperty(CoreAttachment coreAttachment, StorePropertyDefinition property)
		{
			string text = ((IDirectPropertyBag)coreAttachment.PropertyBag).GetValue(property) as string;
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			Uri result;
			if (!Uri.TryCreate(text, UriKind.RelativeOrAbsolute, out result))
			{
				ExTraceGlobals.StorageTracer.TraceError<StorePropertyDefinition, string>((long)coreAttachment.GetHashCode(), "CoreAttachment.GetUriProperty: {0} is not a valid URI\r\n'{1}'", property, text);
				return null;
			}
			return result;
		}

		// Token: 0x06004253 RID: 16979 RVA: 0x0011AFAB File Offset: 0x001191AB
		internal void SetEnableFullValidation(bool enableFullValidation)
		{
			this.CheckDisposed(null);
			this.enableFullValidation = enableFullValidation;
		}

		// Token: 0x06004254 RID: 16980 RVA: 0x0011AFBC File Offset: 0x001191BC
		internal AttachmentSchema GetSchema()
		{
			this.CheckDisposed(null);
			switch (this.AttachmentType)
			{
			case AttachmentType.EmbeddedMessage:
				return ItemAttachmentSchema.Instance;
			case AttachmentType.Reference:
				return ReferenceAttachmentSchema.Instance;
			}
			return StreamAttachmentBaseSchema.Instance;
		}

		// Token: 0x06004255 RID: 16981 RVA: 0x0011B004 File Offset: 0x00119204
		internal void CopyAttachmentContentFrom(CoreAttachment sourceAttachment)
		{
			this.CheckDisposed(null);
			sourceAttachment.PropertyBag.Load(InternalSchema.ContentConversionProperties);
			foreach (NativeStorePropertyDefinition property in sourceAttachment.PropertyBag.AllNativeProperties)
			{
				if (CoreAttachment.ShouldPropertyBeCopied(property, sourceAttachment.AttachmentType, this.AttachmentType))
				{
					PersistablePropertyBag.CopyProperty(sourceAttachment.PropertyBag, property, this.PropertyBag);
				}
			}
			if (sourceAttachment.AttachmentType == AttachmentType.EmbeddedMessage && this.AttachmentType == AttachmentType.EmbeddedMessage)
			{
				bool noMessageDecoding = sourceAttachment.ParentCollection.ContainerItem.CharsetDetector.NoMessageDecoding;
				bool noMessageDecoding2 = this.parentCollection.ContainerItem.CharsetDetector.NoMessageDecoding;
				using (ICoreItem coreItem = sourceAttachment.PropertyBag.OpenAttachedItem(PropertyOpenMode.ReadOnly, InternalSchema.ContentConversionProperties, noMessageDecoding))
				{
					using (ICoreItem coreItem2 = this.PropertyBag.OpenAttachedItem(PropertyOpenMode.Create, InternalSchema.ContentConversionProperties, noMessageDecoding2))
					{
						CoreItem.CopyItemContent(coreItem, coreItem2);
						using (Item item = Item.InternalBindCoreItem(coreItem2))
						{
							item.CharsetDetector.DetectionOptions.PreferredInternetCodePageForShiftJis = coreItem.PropertyBag.GetValueOrDefault<int>(ItemSchema.InternetCpid, 50222);
							item.LocationIdentifierHelperInstance.SetLocationIdentifier(64373U);
							item.SaveFlags = (((PersistablePropertyBag)coreItem.PropertyBag).SaveFlags | PropertyBagSaveFlags.IgnoreMapiComputedErrors | PropertyBagSaveFlags.IgnoreAccessDeniedErrors);
							item.Save(SaveMode.NoConflictResolution);
						}
					}
				}
			}
			this.PropertyBag.SaveFlags |= (PropertyBagSaveFlags.IgnoreMapiComputedErrors | PropertyBagSaveFlags.IgnoreUnresolvedHeaders);
		}

		// Token: 0x06004256 RID: 16982 RVA: 0x0011B1C8 File Offset: 0x001193C8
		internal void Validate(ValidationContext context)
		{
			this.CheckDisposed(null);
			Validation.Validate(this, context);
		}

		// Token: 0x06004257 RID: 16983 RVA: 0x0011B1D8 File Offset: 0x001193D8
		internal void UpdateParentMsgRichContentFlags(RichContentFlags richContentFlags)
		{
			this.CheckDisposed(null);
			if (this.parentCollection == null || this.parentCollection.ContainerItem == null)
			{
				return;
			}
			ICoreItem containerItem = this.parentCollection.ContainerItem;
			ICorePropertyBag corePropertyBag = containerItem.PropertyBag;
			corePropertyBag.Load(new PropertyDefinition[]
			{
				InternalSchema.RichContent
			});
			short valueOrDefault = corePropertyBag.GetValueOrDefault<short>(InternalSchema.RichContent, 0);
			short num = valueOrDefault | (short)richContentFlags;
			corePropertyBag[InternalSchema.RichContent] = num;
		}

		// Token: 0x06004258 RID: 16984 RVA: 0x0011B251 File Offset: 0x00119451
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.propertyBag.Dispose();
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06004259 RID: 16985 RVA: 0x0011B268 File Offset: 0x00119468
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<CoreAttachment>(this);
		}

		// Token: 0x0600425A RID: 16986 RVA: 0x0011B270 File Offset: 0x00119470
		[Conditional("DEBUG")]
		private static void DebugCheckContains(PropertyDefinition[] properties, PropertyDefinition property)
		{
			ICollection<NativeStorePropertyDefinition> nativePropertyDefinitions = StorePropertyDefinition.GetNativePropertyDefinitions<PropertyDefinition>(PropertyDependencyType.AllRead, new PropertyDefinition[]
			{
				property
			});
			foreach (NativeStorePropertyDefinition nativeStorePropertyDefinition in nativePropertyDefinitions)
			{
			}
		}

		// Token: 0x0600425B RID: 16987 RVA: 0x0011B2C4 File Offset: 0x001194C4
		private static bool ShouldPropertyBeCopied(PropertyDefinition property, AttachmentType sourceType, AttachmentType targetType)
		{
			return property != InternalSchema.AttachMethod && (property != InternalSchema.AttachDataObj || (targetType != AttachmentType.EmbeddedMessage && sourceType == targetType));
		}

		// Token: 0x0600425C RID: 16988 RVA: 0x0011B2E4 File Offset: 0x001194E4
		private void CoreObjectUpdate()
		{
			this.GetSchema().CoreObjectUpdate(this);
		}

		// Token: 0x04002481 RID: 9345
		private readonly AttachmentPropertyBag propertyBag;

		// Token: 0x04002482 RID: 9346
		private readonly CoreAttachmentCollection parentCollection;

		// Token: 0x04002483 RID: 9347
		private readonly int attachmentNumber;

		// Token: 0x04002484 RID: 9348
		private bool enableFullValidation = true;

		// Token: 0x04002485 RID: 9349
		private Origin origin;
	}
}
