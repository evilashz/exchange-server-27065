using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AEC RID: 2796
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class AttachmentPropertyBag : PersistablePropertyBag
	{
		// Token: 0x06006572 RID: 25970 RVA: 0x001AF954 File Offset: 0x001ADB54
		internal AttachmentPropertyBag(IAttachmentProvider attachmentProvider, int attachmentNumber, PropertyBag attachmentTablePropertyBag, ICollection<NativeStorePropertyDefinition> attachmentTablePropertySet, bool useCreateFlagOnConnect)
		{
			Util.ThrowOnNullArgument(attachmentProvider, "attachmentProvider");
			Util.ThrowOnNullArgument(attachmentTablePropertyBag, "attachmentTablePropertyBag");
			Util.ThrowOnNullArgument(attachmentTablePropertySet, "attachmentTablePropertySet");
			this.attachmentProvider = attachmentProvider;
			this.attachmentNumber = attachmentNumber;
			this.attachmentTablePropertyBag = attachmentTablePropertyBag;
			this.attachmentTablePropertySet = attachmentTablePropertySet;
			this.persistablePropertyBag = null;
			this.useCreateFlagOnConnect = useCreateFlagOnConnect;
			this.PrefetchPropertyArray = attachmentTablePropertySet.ToArray<NativeStorePropertyDefinition>();
		}

		// Token: 0x06006573 RID: 25971 RVA: 0x001AF9C4 File Offset: 0x001ADBC4
		internal AttachmentPropertyBag(IAttachmentProvider attachmentProvider, int attachmentNumber, PersistablePropertyBag persistablePropertyBag, bool useCreateFlagOnConnect)
		{
			Util.ThrowOnNullArgument(attachmentProvider, "attachmentProvider");
			Util.ThrowOnNullArgument(persistablePropertyBag, "persistablePropertyBag");
			this.attachmentProvider = attachmentProvider;
			this.attachmentNumber = attachmentNumber;
			this.attachmentTablePropertyBag = null;
			this.attachmentTablePropertySet = null;
			this.persistablePropertyBag = persistablePropertyBag;
			this.useCreateFlagOnConnect = useCreateFlagOnConnect;
			this.PrefetchPropertyArray = persistablePropertyBag.PrefetchPropertyArray;
		}

		// Token: 0x06006574 RID: 25972 RVA: 0x001AFA24 File Offset: 0x001ADC24
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.attachmentProvider.OnCollectionDisposed(this, this.persistablePropertyBag);
				this.persistablePropertyBag = null;
				this.saveFlags = PropertyBagSaveFlags.Default;
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06006575 RID: 25973 RVA: 0x001AFA50 File Offset: 0x001ADC50
		public void ReleaseConnection()
		{
			this.attachmentProvider.OnAttachmentDisconnected(this, this.persistablePropertyBag);
			this.persistablePropertyBag = null;
			this.saveFlags = PropertyBagSaveFlags.Default;
		}

		// Token: 0x06006576 RID: 25974 RVA: 0x001AFA74 File Offset: 0x001ADC74
		public override void Load(ICollection<PropertyDefinition> properties)
		{
			base.CheckDisposed("Load");
			if (!this.attachmentProvider.ExistsInCollection(this))
			{
				throw new ObjectNotFoundException(ServerStrings.InvalidAttachmentNumber);
			}
			ICollection<NativeStorePropertyDefinition> collection = null;
			if (properties != InternalSchema.ContentConversionProperties)
			{
				collection = StorePropertyDefinition.GetNativePropertyDefinitions<PropertyDefinition>(PropertyDependencyType.AllRead, properties);
				properties = collection.ToArray<NativeStorePropertyDefinition>();
			}
			if (this.persistablePropertyBag == null)
			{
				bool flag = false;
				if (collection != null)
				{
					IDirectPropertyBag currentPropertyBag = this.CurrentPropertyBag;
					using (IEnumerator<NativeStorePropertyDefinition> enumerator = collection.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							NativeStorePropertyDefinition propertyDefinition = enumerator.Current;
							if (!currentPropertyBag.IsLoaded(propertyDefinition))
							{
								flag = true;
								break;
							}
						}
						goto IL_8A;
					}
				}
				flag = true;
				IL_8A:
				if (flag)
				{
					this.OnOpenConnection(properties);
					return;
				}
			}
			else
			{
				this.persistablePropertyBag.Load(properties);
				this.attachmentProvider.OnAttachmentLoad(this);
			}
		}

		// Token: 0x06006577 RID: 25975 RVA: 0x001AFB40 File Offset: 0x001ADD40
		protected override object TryGetStoreProperty(StorePropertyDefinition propertyDefinition)
		{
			base.CheckDisposed("AttachmentPropertyBag::TryGetStoreProperty");
			if (this.persistablePropertyBag == null)
			{
				NativeStorePropertyDefinition nativeStorePropertyDefinition = propertyDefinition as NativeStorePropertyDefinition;
				if (nativeStorePropertyDefinition == null)
				{
					return propertyDefinition.Get((PropertyBag.BasicPropertyStore)this);
				}
				if (this.attachmentTablePropertySet.Contains(nativeStorePropertyDefinition))
				{
					object value = ((IDirectPropertyBag)this.attachmentTablePropertyBag).GetValue(nativeStorePropertyDefinition);
					PropertyError propertyError = value as PropertyError;
					if (propertyError == null || propertyError.PropertyErrorCode != PropertyErrorCode.PropertyValueTruncated)
					{
						return value;
					}
				}
				this.OnOpenConnection();
			}
			return ((IDirectPropertyBag)this.CurrentPropertyBag).GetValue(propertyDefinition);
		}

		// Token: 0x06006578 RID: 25976 RVA: 0x001AFBBA File Offset: 0x001ADDBA
		protected override void SetValidatedStoreProperty(StorePropertyDefinition propertyDefinition, object propertyValue)
		{
			base.CheckDisposed("SetValidatedStoreProperty");
			this.OnDataChanged();
			((IDirectPropertyBag)this.CurrentPropertyBag).SetValue(propertyDefinition, propertyValue);
		}

		// Token: 0x06006579 RID: 25977 RVA: 0x001AFBDA File Offset: 0x001ADDDA
		protected override void DeleteStoreProperty(StorePropertyDefinition propertyDefinition)
		{
			base.CheckDisposed("DeleteStoreProperty");
			this.OnDataChanged();
			((IDirectPropertyBag)this.CurrentPropertyBag).Delete(propertyDefinition);
		}

		// Token: 0x0600657A RID: 25978 RVA: 0x001AFBF9 File Offset: 0x001ADDF9
		public override void Clear()
		{
			if (this.persistablePropertyBag != null)
			{
				this.persistablePropertyBag.Clear();
			}
			this.attachmentTablePropertyBag = null;
			this.saveFlags = PropertyBagSaveFlags.Default;
		}

		// Token: 0x0600657B RID: 25979 RVA: 0x001AFC1C File Offset: 0x001ADE1C
		public override void Reload()
		{
			this.Clear();
			this.Load(null);
		}

		// Token: 0x17001BFC RID: 7164
		// (get) Token: 0x0600657C RID: 25980 RVA: 0x001AFC2B File Offset: 0x001ADE2B
		public override ICollection<PropertyDefinition> AllFoundProperties
		{
			get
			{
				base.CheckDisposed("AllFoundProperties");
				this.OnOpenConnection();
				return this.persistablePropertyBag.AllFoundProperties;
			}
		}

		// Token: 0x17001BFD RID: 7165
		// (get) Token: 0x0600657D RID: 25981 RVA: 0x001AFC49 File Offset: 0x001ADE49
		internal override ICollection<NativeStorePropertyDefinition> AllNativeProperties
		{
			get
			{
				base.CheckDisposed("AllNativeProperties");
				if (this.persistablePropertyBag == null)
				{
					return this.attachmentTablePropertySet;
				}
				return this.persistablePropertyBag.AllNativeProperties;
			}
		}

		// Token: 0x17001BFE RID: 7166
		// (get) Token: 0x0600657E RID: 25982 RVA: 0x001AFC70 File Offset: 0x001ADE70
		internal override PropertyBagContext Context
		{
			get
			{
				if (this.persistablePropertyBag != null)
				{
					return this.persistablePropertyBag.Context;
				}
				return this.attachmentTablePropertyBag.Context;
			}
		}

		// Token: 0x0600657F RID: 25983 RVA: 0x001AFC91 File Offset: 0x001ADE91
		protected override bool IsLoaded(NativeStorePropertyDefinition propertyDefinition)
		{
			base.CheckDisposed("IsLoaded");
			return ((IDirectPropertyBag)this.CurrentPropertyBag).IsLoaded(propertyDefinition);
		}

		// Token: 0x17001BFF RID: 7167
		// (get) Token: 0x06006580 RID: 25984 RVA: 0x001AFCAA File Offset: 0x001ADEAA
		public override bool HasAllPropertiesLoaded
		{
			get
			{
				base.CheckDisposed("HasAllPropertiesLoaded::get");
				return this.persistablePropertyBag != null && this.persistablePropertyBag.HasAllPropertiesLoaded;
			}
		}

		// Token: 0x17001C00 RID: 7168
		// (get) Token: 0x06006581 RID: 25985 RVA: 0x001AFCCC File Offset: 0x001ADECC
		internal override MapiProp MapiProp
		{
			get
			{
				base.CheckDisposed("StoreObject");
				this.OnOpenConnection();
				return this.persistablePropertyBag.MapiProp;
			}
		}

		// Token: 0x17001C01 RID: 7169
		// (get) Token: 0x06006582 RID: 25986 RVA: 0x001AFCEA File Offset: 0x001ADEEA
		internal PersistablePropertyBag PersistablePropertyBag
		{
			get
			{
				base.CheckDisposed("PersistablePropertyBag");
				this.OnOpenConnection();
				return this.persistablePropertyBag;
			}
		}

		// Token: 0x06006583 RID: 25987 RVA: 0x001AFD03 File Offset: 0x001ADF03
		public override Stream OpenPropertyStream(PropertyDefinition propertyDefinition, PropertyOpenMode openMode)
		{
			base.CheckDisposed("OpenPropertyStream");
			EnumValidator.AssertValid<PropertyOpenMode>(openMode);
			this.OnOpenConnection();
			return this.persistablePropertyBag.OpenPropertyStream(propertyDefinition, openMode);
		}

		// Token: 0x06006584 RID: 25988 RVA: 0x001AFD29 File Offset: 0x001ADF29
		public void UpdateAttachmentTableData(PropertyBag attachmentTableData, ICollection<NativeStorePropertyDefinition> attachmentTablePropertySet, bool forceReset)
		{
			base.CheckDisposed("UpdateAttachmentTableData");
			if (this.attachmentTablePropertyBag == null || forceReset)
			{
				this.attachmentTablePropertyBag = attachmentTableData;
				this.attachmentTablePropertySet = attachmentTablePropertySet;
			}
		}

		// Token: 0x06006585 RID: 25989 RVA: 0x001AFD50 File Offset: 0x001ADF50
		internal override void FlushChanges()
		{
			base.CheckDisposed("FlushChanges");
			this.OnOpenConnection();
			this.savedData = this.ComputeSavedData();
			this.attachmentProvider.OnBeforeAttachmentSave(this);
			this.persistablePropertyBag.SaveFlags = this.saveFlags;
			this.persistablePropertyBag.FlushChanges();
		}

		// Token: 0x06006586 RID: 25990 RVA: 0x001AFDA2 File Offset: 0x001ADFA2
		internal override void SaveChanges(bool force)
		{
			base.CheckDisposed("SaveChanges");
			this.persistablePropertyBag.SaveChanges(force);
			this.attachmentTablePropertyBag = null;
			this.saveFlags = PropertyBagSaveFlags.Default;
			this.attachmentProvider.OnAfterAttachmentSave(this);
			this.useCreateFlagOnConnect = false;
		}

		// Token: 0x06006587 RID: 25991 RVA: 0x001AFDDC File Offset: 0x001ADFDC
		private AttachmentPropertyBag.SavedData ComputeSavedData()
		{
			AttachmentPropertyBag.SavedData savedData = new AttachmentPropertyBag.SavedData();
			savedData.AttachmentId = this.AttachmentId;
			savedData.AttachMethod = this.AttachMethod;
			StringBuilder stringBuilder = new StringBuilder();
			this.ComputeCharsetDetectionData(stringBuilder);
			savedData.CharsetDetectionString = stringBuilder.ToString();
			int? num = ((IDirectPropertyBag)this.CurrentPropertyBag).GetValue(InternalSchema.AttachCalendarFlags) as int?;
			savedData.IsCalendarException = CoreAttachmentCollection.IsCalendarException((num != null) ? num.Value : 0);
			bool? flag = base.TryGetProperty(InternalSchema.AttachmentIsInline) as bool?;
			savedData.IsInline = (flag != null && flag.Value);
			return savedData;
		}

		// Token: 0x06006588 RID: 25992 RVA: 0x001AFE8C File Offset: 0x001AE08C
		internal void ComputeCharsetDetectionData(StringBuilder stringBuilder)
		{
			bool flag = this.persistablePropertyBag == null;
			if (flag && this.attachmentTablePropertyBag == null)
			{
				stringBuilder.Append(this.savedData.CharsetDetectionString);
				return;
			}
			foreach (StorePropertyDefinition propertyDefinition in this.Schema.DetectCodepageProperties)
			{
				string text = this.TryGetStoreProperty(propertyDefinition) as string;
				if (text != null)
				{
					stringBuilder.AppendLine(text);
				}
			}
			if (flag && this.persistablePropertyBag != null)
			{
				this.ReleaseConnection();
			}
		}

		// Token: 0x17001C02 RID: 7170
		// (get) Token: 0x06006589 RID: 25993 RVA: 0x001AFF28 File Offset: 0x001AE128
		public override bool IsDirty
		{
			get
			{
				base.CheckDisposed("IsDirty::get");
				return this.CurrentPropertyBag.IsDirty;
			}
		}

		// Token: 0x17001C03 RID: 7171
		// (get) Token: 0x0600658A RID: 25994 RVA: 0x001AFF40 File Offset: 0x001AE140
		internal bool IsCalendarException
		{
			get
			{
				if (this.attachmentTablePropertyBag != null)
				{
					int? num = this.TryGetStoreProperty(InternalSchema.AttachCalendarFlags) as int?;
					return CoreAttachmentCollection.IsCalendarException((num != null) ? num.Value : 0);
				}
				return this.savedData.IsCalendarException;
			}
		}

		// Token: 0x17001C04 RID: 7172
		// (get) Token: 0x0600658B RID: 25995 RVA: 0x001AFF90 File Offset: 0x001AE190
		internal bool IsInline
		{
			get
			{
				if (this.attachmentTablePropertyBag != null)
				{
					bool? flag = base.TryGetProperty(InternalSchema.AttachmentIsInline) as bool?;
					return flag != null && flag.Value;
				}
				return this.savedData.IsInline;
			}
		}

		// Token: 0x0600658C RID: 25996 RVA: 0x001AFFD9 File Offset: 0x001AE1D9
		protected override bool InternalIsPropertyDirty(AtomicStorePropertyDefinition propertyDefinition)
		{
			base.CheckDisposed("InternalIsPropertyDirty");
			return ((IDirectPropertyBag)this.CurrentPropertyBag).IsDirty(propertyDefinition);
		}

		// Token: 0x17001C05 RID: 7173
		// (get) Token: 0x0600658D RID: 25997 RVA: 0x001AFFF2 File Offset: 0x001AE1F2
		// (set) Token: 0x0600658E RID: 25998 RVA: 0x001AFFFF File Offset: 0x001AE1FF
		internal override ExTimeZone ExTimeZone
		{
			get
			{
				return this.CurrentPropertyBag.ExTimeZone;
			}
			set
			{
				this.CurrentPropertyBag.ExTimeZone = value;
			}
		}

		// Token: 0x17001C06 RID: 7174
		// (get) Token: 0x0600658F RID: 25999 RVA: 0x001B000D File Offset: 0x001AE20D
		internal bool UseCreateFlagOnConnect
		{
			get
			{
				return this.useCreateFlagOnConnect;
			}
		}

		// Token: 0x17001C07 RID: 7175
		// (get) Token: 0x06006590 RID: 26000 RVA: 0x001B0015 File Offset: 0x001AE215
		// (set) Token: 0x06006591 RID: 26001 RVA: 0x001B0028 File Offset: 0x001AE228
		internal override PropertyBagSaveFlags SaveFlags
		{
			get
			{
				base.CheckDisposed("AttachmentPropertyBag.SaveFlags.get");
				return this.saveFlags;
			}
			set
			{
				base.CheckDisposed("AttachmentPropertyBag.SaveFlags.set");
				EnumValidator.ThrowIfInvalid<PropertyBagSaveFlags>(value, "value");
				this.saveFlags = value;
			}
		}

		// Token: 0x06006592 RID: 26002 RVA: 0x001B0047 File Offset: 0x001AE247
		internal override void SetUpdateImapIdFlag()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006593 RID: 26003 RVA: 0x001B0050 File Offset: 0x001AE250
		public ICoreItem OpenAttachedItem(PropertyOpenMode openMode, ICollection<PropertyDefinition> propertiesToLoad, bool noMessageDecoding)
		{
			ICoreItem coreItem = this.attachmentProvider.OpenAttachedItem(propertiesToLoad, this, openMode == PropertyOpenMode.Create);
			coreItem.CharsetDetector.NoMessageDecoding = noMessageDecoding;
			return coreItem;
		}

		// Token: 0x17001C08 RID: 7176
		// (get) Token: 0x06006594 RID: 26004 RVA: 0x001B007C File Offset: 0x001AE27C
		internal int AttachmentNumber
		{
			get
			{
				return this.attachmentNumber;
			}
		}

		// Token: 0x17001C09 RID: 7177
		// (get) Token: 0x06006595 RID: 26005 RVA: 0x001B0084 File Offset: 0x001AE284
		internal AttachmentId AttachmentId
		{
			get
			{
				PropertyBag currentPropertyBag = this.CurrentPropertyBag;
				if (currentPropertyBag == null)
				{
					return this.savedData.AttachmentId;
				}
				byte[] array = ((IDirectPropertyBag)currentPropertyBag).GetValue(InternalSchema.RecordKey) as byte[];
				if (array == null)
				{
					return null;
				}
				return new AttachmentId(array);
			}
		}

		// Token: 0x17001C0A RID: 7178
		// (get) Token: 0x06006596 RID: 26006 RVA: 0x001B00C4 File Offset: 0x001AE2C4
		internal int AttachMethod
		{
			get
			{
				PropertyBag currentPropertyBag = this.CurrentPropertyBag;
				if (currentPropertyBag == null)
				{
					return this.savedData.AttachMethod;
				}
				int? num = ((IDirectPropertyBag)currentPropertyBag).GetValue(InternalSchema.AttachMethod) as int?;
				if (num != null)
				{
					return num.Value;
				}
				throw new InvalidOperationException("AttachMethod is not found");
			}
		}

		// Token: 0x17001C0B RID: 7179
		// (get) Token: 0x06006597 RID: 26007 RVA: 0x001B0118 File Offset: 0x001AE318
		internal Schema Schema
		{
			get
			{
				return CoreAttachmentCollection.GetAttachmentSchema(this.AttachMethod);
			}
		}

		// Token: 0x17001C0C RID: 7180
		// (get) Token: 0x06006598 RID: 26008 RVA: 0x001B0125 File Offset: 0x001AE325
		internal bool IsEmpty
		{
			get
			{
				base.CheckDisposed("IsEmpty");
				return this.CurrentPropertyBag == null;
			}
		}

		// Token: 0x17001C0D RID: 7181
		// (get) Token: 0x06006599 RID: 26009 RVA: 0x001B013B File Offset: 0x001AE33B
		private PropertyBag CurrentPropertyBag
		{
			get
			{
				return this.persistablePropertyBag ?? this.attachmentTablePropertyBag;
			}
		}

		// Token: 0x0600659A RID: 26010 RVA: 0x001B0150 File Offset: 0x001AE350
		private static Schema GetAttachmentSchema(int attachMethod)
		{
			switch (attachMethod)
			{
			case 2:
			case 3:
			case 4:
			case 7:
				return ReferenceAttachmentSchema.Instance;
			case 5:
				return ItemAttachmentSchema.Instance;
			}
			return StreamAttachmentBaseSchema.Instance;
		}

		// Token: 0x0600659B RID: 26011 RVA: 0x001B0196 File Offset: 0x001AE396
		private void OnDataChanged()
		{
			this.OnOpenConnection();
		}

		// Token: 0x0600659C RID: 26012 RVA: 0x001B019E File Offset: 0x001AE39E
		private void OnOpenConnection()
		{
			this.OnOpenConnection(null);
		}

		// Token: 0x0600659D RID: 26013 RVA: 0x001B01A8 File Offset: 0x001AE3A8
		private void OnOpenConnection(ICollection<PropertyDefinition> propertiesToLoad)
		{
			if (this.persistablePropertyBag == null)
			{
				Schema attachmentSchema = AttachmentPropertyBag.GetAttachmentSchema(this.AttachMethod);
				propertiesToLoad = InternalSchema.Combine<PropertyDefinition>(attachmentSchema.AutoloadProperties, propertiesToLoad);
				this.persistablePropertyBag = this.attachmentProvider.OpenAttachment(propertiesToLoad, this);
				this.persistablePropertyBag.Context.Session = this.attachmentTablePropertyBag.Context.Session;
				this.persistablePropertyBag.Context.CoreState = this.attachmentTablePropertyBag.Context.CoreState;
				this.UpdateAttachmentTableCache();
			}
			else if (propertiesToLoad != null)
			{
				this.persistablePropertyBag.Load(propertiesToLoad);
			}
			this.attachmentProvider.OnAttachmentLoad(this);
		}

		// Token: 0x0600659E RID: 26014 RVA: 0x001B0250 File Offset: 0x001AE450
		private void UpdateAttachmentTableCache()
		{
			if (this.attachmentTablePropertyBag != null)
			{
				bool flag = false;
				foreach (NativeStorePropertyDefinition propertyDefinition in this.attachmentTablePropertySet)
				{
					PropertyError propertyError = ((IDirectPropertyBag)this.attachmentTablePropertyBag).GetValue(propertyDefinition) as PropertyError;
					if (propertyError != null && propertyError.PropertyErrorCode == PropertyErrorCode.PropertyValueTruncated)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return;
				}
			}
			MemoryPropertyBag memoryPropertyBag = new MemoryPropertyBag();
			IDirectPropertyBag directPropertyBag = memoryPropertyBag;
			foreach (NativeStorePropertyDefinition propertyDefinition2 in this.attachmentTablePropertySet)
			{
				object value = ((IDirectPropertyBag)this.persistablePropertyBag).GetValue(propertyDefinition2);
				directPropertyBag.SetValue(propertyDefinition2, value);
			}
			memoryPropertyBag.ClearChangeInfo();
			memoryPropertyBag.Context.Session = this.attachmentTablePropertyBag.Context.Session;
			memoryPropertyBag.Context.CoreState = this.attachmentTablePropertyBag.Context.CoreState;
			this.attachmentTablePropertyBag = memoryPropertyBag;
		}

		// Token: 0x0600659F RID: 26015 RVA: 0x001B0370 File Offset: 0x001AE570
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<AttachmentPropertyBag>(this);
		}

		// Token: 0x040039C5 RID: 14789
		private int attachmentNumber;

		// Token: 0x040039C6 RID: 14790
		private bool useCreateFlagOnConnect;

		// Token: 0x040039C7 RID: 14791
		private AttachmentPropertyBag.SavedData savedData;

		// Token: 0x040039C8 RID: 14792
		private PropertyBagSaveFlags saveFlags;

		// Token: 0x040039C9 RID: 14793
		private PropertyBag attachmentTablePropertyBag;

		// Token: 0x040039CA RID: 14794
		private ICollection<NativeStorePropertyDefinition> attachmentTablePropertySet;

		// Token: 0x040039CB RID: 14795
		private PersistablePropertyBag persistablePropertyBag;

		// Token: 0x040039CC RID: 14796
		private IAttachmentProvider attachmentProvider;

		// Token: 0x02000AED RID: 2797
		private class SavedData
		{
			// Token: 0x040039CD RID: 14797
			public AttachmentId AttachmentId;

			// Token: 0x040039CE RID: 14798
			public string CharsetDetectionString;

			// Token: 0x040039CF RID: 14799
			public int AttachMethod;

			// Token: 0x040039D0 RID: 14800
			public bool IsCalendarException;

			// Token: 0x040039D1 RID: 14801
			public bool IsInline;
		}
	}
}
