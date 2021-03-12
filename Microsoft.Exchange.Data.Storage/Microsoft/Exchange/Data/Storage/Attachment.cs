using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime.Internal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200034B RID: 843
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class Attachment : IDisposeTrackable, IAttachment, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x0600252D RID: 9517 RVA: 0x00095CCE File Offset: 0x00093ECE
		internal Attachment(CoreAttachment coreAttachment)
		{
			StorageGlobals.TraceConstructIDisposable(this);
			this.disposeTracker = this.GetDisposeTracker();
			this.coreAttachment = coreAttachment;
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x00095CF0 File Offset: 0x00093EF0
		public static bool TryFindFileExtension(string filename, out string extension, out string rest)
		{
			if (string.IsNullOrEmpty(filename))
			{
				extension = null;
				rest = null;
				return false;
			}
			int length = filename.Length;
			for (int i = length - 1; i >= 0; i--)
			{
				char c = filename[i];
				if (c == '.')
				{
					extension = filename.Substring(i, length - i);
					rest = filename.Substring(0, i);
					return true;
				}
				if (Util.IsAttachmentSeparator(c))
				{
					break;
				}
			}
			extension = null;
			rest = filename;
			return false;
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x00095D58 File Offset: 0x00093F58
		public static string TrimFilename(string filename)
		{
			if (string.IsNullOrEmpty(filename))
			{
				return string.Empty;
			}
			char[] array = filename.ToCharArray();
			for (int num = 0; num != array.Length; num++)
			{
				if (char.IsSeparator(array[num]))
				{
					array[num] = ' ';
				}
			}
			return filename.TrimStart(new char[0]).TrimEnd(new char[]
			{
				'.',
				' '
			});
		}

		// Token: 0x06002530 RID: 9520
		public abstract DisposeTracker GetDisposeTracker();

		// Token: 0x06002531 RID: 9521 RVA: 0x00095DBA File Offset: 0x00093FBA
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x00095DCF File Offset: 0x00093FCF
		public override string ToString()
		{
			return this.coreAttachment.ToString();
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x00095DDC File Offset: 0x00093FDC
		private static CoreAttachment CopyAttachment(CoreAttachment sourceAttachment, ICollection<NativeStorePropertyDefinition> excludeProperties)
		{
			CoreAttachment coreAttachment = null;
			if (sourceAttachment != null)
			{
				coreAttachment = sourceAttachment.ParentCollection.Create(AttachmentType.Stream);
				sourceAttachment.PropertyBag.Load(InternalSchema.ContentConversionProperties);
				foreach (NativeStorePropertyDefinition nativeStorePropertyDefinition in sourceAttachment.PropertyBag.AllNativeProperties)
				{
					if (excludeProperties == null || !excludeProperties.Contains(nativeStorePropertyDefinition))
					{
						PersistablePropertyBag.CopyProperty(sourceAttachment.PropertyBag, nativeStorePropertyDefinition, coreAttachment.PropertyBag);
					}
				}
			}
			return coreAttachment;
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x00095E68 File Offset: 0x00094068
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x00095E77 File Offset: 0x00094077
		private void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.isDisposed, disposing);
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x00095E9C File Offset: 0x0009409C
		protected virtual void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.coreAttachment != null)
				{
					this.coreAttachment.Dispose();
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
			}
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x00095EC7 File Offset: 0x000940C7
		protected void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06002538 RID: 9528 RVA: 0x00095EE9 File Offset: 0x000940E9
		internal bool IsDisposed
		{
			get
			{
				return this.isDisposed;
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06002539 RID: 9529 RVA: 0x00095EF1 File Offset: 0x000940F1
		internal CoreAttachment CoreAttachment
		{
			get
			{
				return this.coreAttachment;
			}
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x0600253A RID: 9530 RVA: 0x00095EF9 File Offset: 0x000940F9
		public AttachmentId Id
		{
			get
			{
				this.CheckDisposed("Id::get");
				return this.coreAttachment.Id;
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x0600253B RID: 9531 RVA: 0x00095F11 File Offset: 0x00094111
		public virtual AttachmentType AttachmentType
		{
			get
			{
				this.CheckDisposed("AttachmentType::get");
				return AttachmentType.Stream;
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x0600253C RID: 9532 RVA: 0x00095F20 File Offset: 0x00094120
		// (set) Token: 0x0600253D RID: 9533 RVA: 0x00095F60 File Offset: 0x00094160
		public string FileName
		{
			get
			{
				this.CheckDisposed("FileName::get");
				string valueOrDefault = this.GetValueOrDefault<string>(InternalSchema.AttachLongFileName);
				if (valueOrDefault == null)
				{
					valueOrDefault = this.GetValueOrDefault<string>(InternalSchema.AttachFileName, string.Empty);
				}
				return Attachment.TrimFilename(valueOrDefault);
			}
			set
			{
				this.CheckDisposed("FileName::set");
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				string text = Attachment.TrimFilename(value);
				string text2;
				string text3;
				Attachment.TryFindFileExtension(text, out text2, out text3);
				if (string.IsNullOrEmpty(text3))
				{
					text3 = Attachment.GenerateFilename();
					text = text3 + text2;
				}
				this.PropertyBag[InternalSchema.AttachLongFileName] = text;
				this.PropertyBag[InternalSchema.DisplayName] = text;
				this.PropertyBag[InternalSchema.AttachFileName] = Attachment.Make8x3FileName(text, this.Session != null && this.Session.IsMoveUser);
				this.PropertyBag[InternalSchema.AttachExtension] = (text2 ?? string.Empty);
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x0600253E RID: 9534 RVA: 0x00096018 File Offset: 0x00094218
		public bool IsContactPhoto
		{
			get
			{
				this.CheckDisposed("IsContactPhoto::get");
				return this.GetValueOrDefault<bool>(InternalSchema.IsContactPhoto);
			}
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x0600253F RID: 9535 RVA: 0x00096040 File Offset: 0x00094240
		public string FileExtension
		{
			get
			{
				this.CheckDisposed("FileExtension::get");
				string fileName = this.FileName;
				string text;
				string text2;
				Attachment.TryFindFileExtension(fileName, out text, out text2);
				return text ?? string.Empty;
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06002540 RID: 9536 RVA: 0x00096074 File Offset: 0x00094274
		public Uri ContentBase
		{
			get
			{
				this.CheckDisposed("ContentBase::get");
				return CoreAttachment.GetUriProperty(this.coreAttachment, InternalSchema.AttachContentBase);
			}
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06002541 RID: 9537 RVA: 0x00096091 File Offset: 0x00094291
		// (set) Token: 0x06002542 RID: 9538 RVA: 0x000960AE File Offset: 0x000942AE
		public string ContentId
		{
			get
			{
				this.CheckDisposed("ContentId::get");
				return this.GetValueOrDefault<string>(InternalSchema.AttachContentId, string.Empty);
			}
			set
			{
				this.CheckDisposed("ContentId::set");
				this.PropertyBag.SetOrDeleteProperty(InternalSchema.AttachContentId, value);
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06002543 RID: 9539 RVA: 0x000960CC File Offset: 0x000942CC
		// (set) Token: 0x06002544 RID: 9540 RVA: 0x000960E5 File Offset: 0x000942E5
		public int RenderingPosition
		{
			get
			{
				this.CheckDisposed("RenderingPosition::get");
				return this.GetValueOrDefault<int>(InternalSchema.RenderingPosition, -1);
			}
			set
			{
				this.CheckDisposed("RenderingPosition::set");
				this.PropertyBag.SetOrDeleteProperty(InternalSchema.RenderingPosition, value);
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06002545 RID: 9541 RVA: 0x00096108 File Offset: 0x00094308
		public Uri ContentLocation
		{
			get
			{
				this.CheckDisposed("ContentLocation::get");
				return CoreAttachment.GetUriProperty(this.coreAttachment, InternalSchema.AttachContentLocation);
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06002546 RID: 9542 RVA: 0x00096125 File Offset: 0x00094325
		// (set) Token: 0x06002547 RID: 9543 RVA: 0x0009613D File Offset: 0x0009433D
		public virtual string ContentType
		{
			get
			{
				this.CheckDisposed("ContentType::get");
				return this.GetValueOrDefault<string>(InternalSchema.AttachMimeTag);
			}
			set
			{
				this.CheckDisposed("ContentType::set");
				this.PropertyBag.SetOrDeleteProperty(InternalSchema.AttachMimeTag, value);
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06002548 RID: 9544 RVA: 0x0009615B File Offset: 0x0009435B
		public string CalculatedContentType
		{
			get
			{
				this.CheckDisposed("CalculatedContentType");
				return this.CalculateContentType();
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06002549 RID: 9545 RVA: 0x0009616E File Offset: 0x0009436E
		// (set) Token: 0x0600254A RID: 9546 RVA: 0x00096186 File Offset: 0x00094386
		public bool IsInline
		{
			get
			{
				this.CheckDisposed("IsInline::get");
				return this.coreAttachment.IsInline;
			}
			set
			{
				this.CheckDisposed("IsInline::set");
				this.coreAttachment.IsInline = value;
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x0600254B RID: 9547 RVA: 0x0009619F File Offset: 0x0009439F
		public string DisplayName
		{
			get
			{
				this.CheckDisposed("DisplayName::get");
				return this.GetValueOrDefault<string>(InternalSchema.DisplayName, string.Empty);
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x0600254C RID: 9548 RVA: 0x000961BC File Offset: 0x000943BC
		public long Size
		{
			get
			{
				this.CheckDisposed("Size::get");
				return (long)this.GetValueOrDefault<int>(InternalSchema.AttachSize);
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x0600254D RID: 9549 RVA: 0x000961D5 File Offset: 0x000943D5
		public ExDateTime CreationTime
		{
			get
			{
				this.CheckDisposed("CreationTime::get");
				return this.GetValueOrDefault<ExDateTime>(InternalSchema.CreationTime, ExDateTime.Now);
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x0600254E RID: 9550 RVA: 0x000961F2 File Offset: 0x000943F2
		public ExDateTime LastModifiedTime
		{
			get
			{
				this.CheckDisposed("LastModifiedTime::get");
				return this.GetValueOrDefault<ExDateTime>(InternalSchema.LastModifiedTime, ExDateTime.Now);
			}
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x00096210 File Offset: 0x00094410
		public StoreObjectValidationError[] Validate()
		{
			this.CheckDisposed("Validate");
			ValidationContext context = new ValidationContext(this.Session);
			return Validation.CreateStoreObjectValiationErrorArray(this.coreAttachment, context);
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x00096240 File Offset: 0x00094440
		public void Save()
		{
			this.CheckDisposed("Save");
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(ServerStrings.CannotSaveReadOnlyAttachment);
			}
			ExTraceGlobals.StorageTracer.Information<int>((long)this.GetHashCode(), "Attachment::Save. HashCode = {0}", this.GetHashCode());
			this.OnBeforeSave();
			this.coreAttachment.Save();
			this.OnAfterSave();
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06002551 RID: 9553 RVA: 0x000962A4 File Offset: 0x000944A4
		public Charset TextCharset
		{
			get
			{
				string valueOrDefault = this.GetValueOrDefault<string>(InternalSchema.TextAttachmentCharset);
				Charset charset;
				if (valueOrDefault != null && Charset.TryGetCharset(valueOrDefault, out charset) && charset.IsAvailable)
				{
					return charset;
				}
				return null;
			}
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x000962D8 File Offset: 0x000944D8
		public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
		{
			this.CheckDisposed("GetProperties");
			if (propertyDefinitionArray == null)
			{
				throw new ArgumentNullException("propertyDefinitionArray");
			}
			object[] array = new object[propertyDefinitionArray.Count];
			int num = 0;
			foreach (PropertyDefinition property in propertyDefinitionArray)
			{
				array[num++] = this.TryGetProperty(property);
			}
			return array;
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x00096350 File Offset: 0x00094550
		public void SetProperties(ICollection<PropertyDefinition> propertyDefinitionArray, object[] propertyValuesArray)
		{
			this.CheckDisposed("SetProperties");
			if (propertyDefinitionArray == null || propertyValuesArray == null || propertyDefinitionArray.Count != propertyValuesArray.Length)
			{
				throw new ArgumentException(ServerStrings.PropertyDefinitionsValuesNotMatch);
			}
			int num = 0;
			foreach (PropertyDefinition propertyDefinition in propertyDefinitionArray)
			{
				this[propertyDefinition] = propertyValuesArray[num++];
			}
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x000963D0 File Offset: 0x000945D0
		public virtual Stream OpenPropertyStream(PropertyDefinition propertyDefinition, PropertyOpenMode openMode)
		{
			this.CheckDisposed("OpenProperyStream");
			EnumValidator.ThrowIfInvalid<PropertyOpenMode>(openMode, "openMode");
			InternalSchema.ToStorePropertyDefinition(propertyDefinition);
			if (!(propertyDefinition is NativeStorePropertyDefinition))
			{
				throw new NotSupportedException(ServerStrings.ExCalculatedPropertyStreamAccessNotSupported(propertyDefinition.Name));
			}
			return this.PropertyBag.OpenPropertyStream(propertyDefinition, openMode);
		}

		// Token: 0x17000C7E RID: 3198
		public object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				this.CheckDisposed("this::get[PropertyDefinition]");
				object obj = this.TryGetProperty(propertyDefinition);
				PropertyError propertyError = obj as PropertyError;
				if (propertyError != null)
				{
					throw PropertyError.ToException(new PropertyError[]
					{
						propertyError
					});
				}
				return obj;
			}
			set
			{
				this.CheckDisposed("this::set[PropertyDefinition]");
				this.SetProperty(propertyDefinition, value);
			}
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x0009647A File Offset: 0x0009467A
		public void Delete(PropertyDefinition propertyDefinition)
		{
			this.CheckDisposed("Delete");
			this.PropertyBag.Delete(propertyDefinition);
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06002558 RID: 9560 RVA: 0x00096493 File Offset: 0x00094693
		public bool IsDirty
		{
			get
			{
				this.CheckDisposed("IsDirty");
				return this.PropertyBag.IsDirty;
			}
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x000964AB File Offset: 0x000946AB
		public bool IsPropertyDirty(PropertyDefinition propertyDefinition)
		{
			this.CheckDisposed("IsPropertyDirty");
			return this.PropertyBag.IsPropertyDirty(propertyDefinition);
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x000964C4 File Offset: 0x000946C4
		public object TryGetProperty(PropertyDefinition property)
		{
			this.CheckDisposed("TryGetProperty");
			return this.PropertyBag.TryGetProperty(property);
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x000964DD File Offset: 0x000946DD
		public void Load()
		{
			this.Load(null);
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x000964E6 File Offset: 0x000946E6
		public void Load(params PropertyDefinition[] properties)
		{
			this.Load((ICollection<PropertyDefinition>)properties);
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x000964F4 File Offset: 0x000946F4
		public void Load(ICollection<PropertyDefinition> properties)
		{
			this.CheckDisposed("Load");
			this.PropertyBag.Load(properties);
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x0009650D File Offset: 0x0009470D
		public T GetValueOrDefault<T>(PropertyDefinition propertyDefinition, T defaultValue)
		{
			this.CheckDisposed("GetValueOrDefault");
			return this.PropertyBag.GetValueOrDefault<T>(propertyDefinition, defaultValue);
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x00096527 File Offset: 0x00094727
		public void SetOrDeleteProperty(PropertyDefinition propertyDefinition, object propertyValue)
		{
			this.CheckDisposed("SetOrDeleteProperty");
			this.PropertyBag.SetOrDeleteProperty(propertyDefinition, propertyValue);
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x00096544 File Offset: 0x00094744
		protected OpenPropertyFlags CalculateOpenPropertyFlags()
		{
			OpenPropertyFlags result;
			if (this.IsNew)
			{
				result = OpenPropertyFlags.Create;
			}
			else if (!this.IsReadOnly)
			{
				result = OpenPropertyFlags.Modify;
			}
			else
			{
				result = OpenPropertyFlags.None;
			}
			return result;
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x0009656C File Offset: 0x0009476C
		protected PropertyOpenMode CalculateOpenMode()
		{
			PropertyOpenMode result;
			if (this.IsNew)
			{
				result = PropertyOpenMode.Create;
			}
			else if (!this.IsReadOnly)
			{
				result = PropertyOpenMode.Modify;
			}
			else
			{
				result = PropertyOpenMode.ReadOnly;
			}
			return result;
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x00096594 File Offset: 0x00094794
		internal virtual Attachment CreateCopy(AttachmentCollection collection, BodyFormat? targetBodyFormat)
		{
			return (Attachment)Attachment.CreateCopy(this, collection, new AttachmentType?(this.AttachmentType));
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x000965AD File Offset: 0x000947AD
		protected static IAttachment CreateCopy(Attachment attachment, AttachmentCollection collection, AttachmentType? newAttachmentType)
		{
			return collection.Create(newAttachmentType, attachment);
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x000965B8 File Offset: 0x000947B8
		internal T GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition)
		{
			return this.GetValueOrDefault<T>(propertyDefinition, default(T));
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x000965D5 File Offset: 0x000947D5
		internal T GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition, T defaultValue)
		{
			this.CheckDisposed("GetValueOrDefault");
			return this.PropertyBag.GetValueOrDefault<T>(propertyDefinition, defaultValue);
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x000965EF File Offset: 0x000947EF
		internal T? GetValueAsNullable<T>(StorePropertyDefinition propertyDefinition) where T : struct
		{
			return this.PropertyBag.GetValueAsNullable<T>(propertyDefinition);
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06002567 RID: 9575 RVA: 0x000965FD File Offset: 0x000947FD
		internal PersistablePropertyBag PropertyBag
		{
			get
			{
				this.CheckDisposed("Attachment.PropertyBag.get");
				return this.coreAttachment.PropertyBag;
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06002568 RID: 9576 RVA: 0x00096615 File Offset: 0x00094815
		// (set) Token: 0x06002569 RID: 9577 RVA: 0x0009662D File Offset: 0x0009482D
		internal PropertyBagSaveFlags SaveFlags
		{
			get
			{
				this.CheckDisposed("Attachment.SaveFlags.get");
				return this.PropertyBag.SaveFlags;
			}
			set
			{
				this.CheckDisposed("Attachment.SaveFlags.set");
				EnumValidator.ThrowIfInvalid<PropertyBagSaveFlags>(value, "value");
				this.PropertyBag.SaveFlags = value;
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x0600256A RID: 9578 RVA: 0x00096651 File Offset: 0x00094851
		internal bool IsNew
		{
			get
			{
				return this.coreAttachment.IsNew;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x0600256B RID: 9579 RVA: 0x0009665E File Offset: 0x0009485E
		public bool IsCalendarException
		{
			get
			{
				this.CheckDisposed("IsCalendarException::get");
				return this.coreAttachment.IsCalendarException;
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x0600256C RID: 9580 RVA: 0x00096676 File Offset: 0x00094876
		internal MapiAttach MapiAttach
		{
			get
			{
				return (MapiAttach)this.PropertyBag.MapiProp;
			}
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x0600256D RID: 9581 RVA: 0x00096688 File Offset: 0x00094888
		internal ICollection<NativeStorePropertyDefinition> AllNativeProperties
		{
			get
			{
				return this.PropertyBag.AllNativeProperties;
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x0600256E RID: 9582 RVA: 0x00096695 File Offset: 0x00094895
		protected StoreSession Session
		{
			get
			{
				return this.coreAttachment.Session;
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x0600256F RID: 9583 RVA: 0x000966A2 File Offset: 0x000948A2
		protected virtual Schema Schema
		{
			get
			{
				return AttachmentSchema.Instance;
			}
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x000966AC File Offset: 0x000948AC
		internal static object Make8x3FileName(object value, bool isMoveUser)
		{
			Util.ThrowOnNullArgument(value, "value");
			string text = (string)value;
			if (!isMoveUser && text.Length > 256)
			{
				throw new PropertyTooBigException(InternalSchema.AttachLongFileName);
			}
			int num = text.LastIndexOf('.');
			string text2;
			string text3;
			if (num == 0)
			{
				text2 = string.Empty;
				text3 = text.Remove(0, 1);
			}
			else if (num >= 0)
			{
				num++;
				char[] array = new char[text.Length - num];
				char[] array2 = new char[num - 1];
				text.CopyTo(num, array, 0, text.Length - num);
				text2 = new string(array);
				text.CopyTo(0, array2, 0, num - 1);
				text3 = new string(array2);
			}
			else
			{
				text2 = string.Empty;
				text3 = text;
			}
			bool flag = false;
			int num2 = 0;
			while (num2 < 3 && num2 < text2.Length)
			{
				if ("+,;=[]".IndexOf(text2[num2]) >= 0)
				{
					text2 = text2.Remove(num2, 1);
					text2 = text2.Insert(num2, "_");
					flag = true;
					num2++;
				}
				else if (" .'\"*<>?:|".IndexOf(text2[num2]) >= 0 || text2[num2] > '\u007f')
				{
					text2 = text2.Remove(num2, 1);
					flag = true;
				}
				else
				{
					num2++;
				}
			}
			if (text2.Length > 3)
			{
				text2 = text2.Substring(0, 3);
				flag = true;
			}
			int num3 = 0;
			while (num3 < 8 && num3 < text3.Length)
			{
				if ("+,;=[]".IndexOf(text3[num3]) >= 0)
				{
					text3 = text3.Remove(num3, 1);
					text3 = text3.Insert(num3, "_");
					flag = true;
					num3++;
				}
				else if (" .'\"*<>?:|".IndexOf(text3[num3]) >= 0 || text3[num3] > '\u007f')
				{
					text3 = text3.Remove(num3, 1);
					flag = true;
				}
				else
				{
					num3++;
				}
			}
			if (text3.Length == 0)
			{
				text3 = "DEF0";
			}
			if (text3.Length > 8 || flag)
			{
				if (text3.Length > 6)
				{
					text3 = text3.Substring(0, 6);
				}
				text3 += "~1";
			}
			if (text2.Length > 0)
			{
				text3 = text3 + "." + text2;
			}
			return text3;
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x000968CC File Offset: 0x00094ACC
		internal static string GenerateFilename()
		{
			if (Attachment.staticRandom == null)
			{
				Attachment.staticRandom = new Random((int)ExDateTime.Now.UtcTicks);
			}
			int num = Attachment.staticRandom.Next() % 100000;
			return EmailMessageHelpers.GenerateFileName(ref num);
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x00096910 File Offset: 0x00094B10
		protected virtual void OnBeforeSave()
		{
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x00096914 File Offset: 0x00094B14
		protected virtual void OnAfterSave()
		{
			CalendarItemBase calendarItemBase = this.coreAttachment.ParentCollection.ContainerItem as CalendarItemBase;
			if (calendarItemBase != null)
			{
				if (this.IsNew)
				{
					calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(51061U, LastChangeAction.AttachmentAdded);
					return;
				}
				calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(47989U, LastChangeAction.AttachmentChange);
			}
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x00096966 File Offset: 0x00094B66
		private void SetProperty(PropertyDefinition propertyDefinition, object value)
		{
			this.PropertyBag.SetProperty(propertyDefinition, value);
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x00096975 File Offset: 0x00094B75
		private string CalculateContentType()
		{
			return Attachment.CalculateContentType(this.FileExtension);
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x00096984 File Offset: 0x00094B84
		internal static string CalculateContentType(string extension)
		{
			if (!StandaloneFuzzing.IsEnabled)
			{
				if (Attachment.contentTypeMapper == null)
				{
					Attachment.contentTypeMapper = ExtensionToContentTypeMapper.Instance;
				}
				if (extension != null)
				{
					if (extension.StartsWith("."))
					{
						extension = extension.Substring(1);
					}
					return Attachment.contentTypeMapper.GetContentTypeByExtension(extension);
				}
			}
			return "application/octet-stream";
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06002577 RID: 9591 RVA: 0x000969D3 File Offset: 0x00094BD3
		private bool IsReadOnly
		{
			get
			{
				this.CheckDisposed("IsReadOnly::get");
				return this.coreAttachment.IsReadOnly;
			}
		}

		// Token: 0x040016A0 RID: 5792
		private const string ConvertToUnder = "+,;=[]";

		// Token: 0x040016A1 RID: 5793
		private const string IllegalDos = " .'\"*<>?:|";

		// Token: 0x040016A2 RID: 5794
		private const string DefaultFileName = "DEF0";

		// Token: 0x040016A3 RID: 5795
		private readonly CoreAttachment coreAttachment;

		// Token: 0x040016A4 RID: 5796
		private bool isDisposed;

		// Token: 0x040016A5 RID: 5797
		private readonly DisposeTracker disposeTracker;

		// Token: 0x040016A6 RID: 5798
		private static ExtensionToContentTypeMapper contentTypeMapper = null;

		// Token: 0x040016A7 RID: 5799
		[ThreadStatic]
		private static Random staticRandom;
	}
}
