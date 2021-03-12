using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000030 RID: 48
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class CoreObject : DisposableObject, ICoreObject, ICoreState, IValidatable, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x00025898 File Offset: 0x00023A98
		internal CoreObject(StoreSession session, PersistablePropertyBag propertyBag, StoreObjectId storeObjectId, byte[] changeKey, Origin origin, ItemLevel itemLevel, ICollection<PropertyDefinition> prefetchProperties)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.session = session;
				this.propertyBag = propertyBag;
				this.itemLevel = itemLevel;
				this.Origin = origin;
				if (propertyBag.DisposeTracker != null)
				{
					propertyBag.DisposeTracker.AddExtraDataWithStackTrace("CoreObject owns PersistablePropertyBag propertyBag at");
				}
				((IDirectPropertyBag)CoreObject.GetPersistablePropertyBag(this)).Context.CoreObject = this;
				((IDirectPropertyBag)CoreObject.GetPersistablePropertyBag(this)).Context.Session = this.Session;
				if (prefetchProperties != null)
				{
					if (prefetchProperties == CoreObjectSchema.AllPropertiesOnStore)
					{
						this.propertyBag.PrefetchPropertyArray = CoreObjectSchema.AllPropertiesOnStore;
					}
					else
					{
						this.propertyBag.PrefetchPropertyArray = StorePropertyDefinition.GetNativePropertyDefinitions<PropertyDefinition>(PropertyDependencyType.AllRead, prefetchProperties).ToArray<NativeStorePropertyDefinition>();
					}
					this.propertyBag.Load(prefetchProperties);
				}
				this.storeObjectId = storeObjectId;
				this.id = ((changeKey != null) ? new VersionedId(this.storeObjectId, changeKey) : null);
				disposeGuard.Success();
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x000259A4 File Offset: 0x00023BA4
		public ICorePropertyBag PropertyBag
		{
			get
			{
				this.CheckDisposed(null);
				return this.propertyBag;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x000259B3 File Offset: 0x00023BB3
		public StoreSession Session
		{
			get
			{
				this.CheckDisposed(null);
				return this.session;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x000259C2 File Offset: 0x00023BC2
		public VersionedId Id
		{
			get
			{
				this.CheckDisposed(null);
				if (this.id == null)
				{
					this.UpdateIds();
				}
				return this.id;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x000259DF File Offset: 0x00023BDF
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x000259EE File Offset: 0x00023BEE
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
				EnumValidator.ThrowIfInvalid<Origin>(value, "origin");
				this.origin = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x00025A09 File Offset: 0x00023C09
		public ItemLevel ItemLevel
		{
			get
			{
				this.CheckDisposed(null);
				return this.itemLevel;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00025A18 File Offset: 0x00023C18
		public virtual bool IsDirty
		{
			get
			{
				return this.propertyBag != null && this.propertyBag.IsDirty;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x00025A2F File Offset: 0x00023C2F
		Schema IValidatable.Schema
		{
			get
			{
				this.CheckDisposed(null);
				return ((ICoreObject)this).GetCorrectSchemaForStoreObject();
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00025A3E File Offset: 0x00023C3E
		bool IValidatable.ValidateAllProperties
		{
			get
			{
				this.CheckDisposed(null);
				return this.enableFullValidation || this.origin == Origin.New;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x00025A5A File Offset: 0x00023C5A
		StoreObjectId ICoreObject.StoreObjectId
		{
			get
			{
				this.CheckDisposed(null);
				if (this.storeObjectId == null)
				{
					this.UpdateIds();
				}
				return this.storeObjectId;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00025A77 File Offset: 0x00023C77
		StoreObjectId ICoreObject.InternalStoreObjectId
		{
			get
			{
				this.CheckDisposed(null);
				return this.storeObjectId;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000430 RID: 1072
		protected abstract StorePropertyDefinition IdProperty { get; }

		// Token: 0x06000431 RID: 1073 RVA: 0x00025A86 File Offset: 0x00023C86
		void IValidatable.Validate(ValidationContext context, IList<StoreObjectValidationError> validationErrors)
		{
			this.CheckDisposed(null);
			Validation.ValidateProperties(context, this, CoreObject.GetPersistablePropertyBag(this), validationErrors);
			this.ValidateContainedObjects(validationErrors);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00025AA4 File Offset: 0x00023CA4
		void ICoreObject.SetEnableFullValidation(bool enableFullValidation)
		{
			this.CheckDisposed(null);
			this.enableFullValidation = enableFullValidation;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00025AB4 File Offset: 0x00023CB4
		Schema ICoreObject.GetCorrectSchemaForStoreObject()
		{
			this.CheckDisposed(null);
			return this.GetSchema();
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00025AC3 File Offset: 0x00023CC3
		void ICoreObject.ResetId()
		{
			this.CheckDisposed(null);
			this.id = null;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00025AD3 File Offset: 0x00023CD3
		internal static PersistablePropertyBag GetPersistablePropertyBag(ICoreObject coreObject)
		{
			return PersistablePropertyBag.GetPersistablePropertyBag(coreObject.PropertyBag);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00025AE0 File Offset: 0x00023CE0
		internal static PropertyError[] MapiCopyTo(MapiProp source, MapiProp destination, StoreSession sourceSession, StoreSession destSession, CopyPropertiesFlags copyPropertiesFlags, CopySubObjects copySubObjects, params NativeStorePropertyDefinition[] excludeProperties)
		{
			Util.ThrowOnNullArgument(source, "sources");
			Util.ThrowOnNullArgument(destination, "destination");
			ICollection<PropTag> excludeTags = PropertyTagCache.Cache.PropTagsFromPropertyDefinitions(source, sourceSession, true, excludeProperties);
			PropProblem[] problems = null;
			bool flag = false;
			try
			{
				if (sourceSession != null)
				{
					sourceSession.BeginMapiCall();
					sourceSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				try
				{
					problems = source.CopyTo(destination, false, CoreObject.ToCopyPropertiesFlags(copyPropertiesFlags), copySubObjects == CopySubObjects.Copy, excludeTags);
				}
				catch (MapiExceptionNamedPropsQuotaExceeded)
				{
					PropTag[] propList = source.GetPropList();
					NativeStorePropertyDefinition[] propertyDefinitions = PropertyTagCache.Cache.PropertyDefinitionsFromPropTags(NativeStorePropertyDefinition.TypeCheckingFlag.DisableTypeCheck, source, sourceSession, propList);
					ICollection<PropTag> collection = PropertyTagCache.Cache.PropTagsFromPropertyDefinitions(destination, destSession, true, true, true, propertyDefinitions);
					List<PropTag> list = new List<PropTag>(collection.Count);
					int num = 0;
					foreach (PropTag propTag in collection)
					{
						if (propTag == PropTag.Unresolved)
						{
							list.Add(propList[num]);
						}
						num++;
					}
					problems = source.CopyTo(destination, list);
				}
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCopyMapiProps, ex, sourceSession, source, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("CoreItem::MapiCopyTo.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCopyMapiProps, ex2, sourceSession, source, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("CoreItem::MapiCopyTo.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (sourceSession != null)
					{
						sourceSession.EndMapiCall();
						if (flag)
						{
							sourceSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			CoreObject.ProcessCopyPropertyProblems(problems, sourceSession, source);
			return CoreObject.ToXsoPropertyErrors(destSession, destination, problems);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00025D38 File Offset: 0x00023F38
		internal static PropertyError[] MapiCopyProps(MapiProp source, MapiProp destination, StoreSession sourceSession, StoreSession destSession, CopyPropertiesFlags copyPropertiesFlags, params NativeStorePropertyDefinition[] includedProperties)
		{
			Util.ThrowOnNullArgument(source, "sources");
			Util.ThrowOnNullArgument(destination, "destination");
			ICollection<PropTag> collection = PropertyTagCache.Cache.PropTagsFromPropertyDefinitions(source, sourceSession, true, includedProperties);
			PropProblem[] problems = null;
			bool flag = false;
			try
			{
				if (sourceSession != null)
				{
					sourceSession.BeginMapiCall();
					sourceSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				CopyPropertiesFlags copyPropertiesFlags2 = CoreObject.ToCopyPropertiesFlags(copyPropertiesFlags);
				try
				{
					problems = source.CopyProps(destination, copyPropertiesFlags2, collection);
				}
				catch (MapiExceptionNamedPropsQuotaExceeded)
				{
					List<PropTag> list = new List<PropTag>(collection.Count);
					foreach (PropTag propTag in collection)
					{
						if (propTag != PropTag.Unresolved)
						{
							list.Add(propTag);
						}
					}
					problems = source.CopyProps(destination, copyPropertiesFlags2, list);
				}
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCopyMapiProps, ex, sourceSession, source, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("CoreItem::MapiCopyProperties.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCopyMapiProps, ex2, sourceSession, source, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("CoreItem::MapiCopyProperties.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (sourceSession != null)
					{
						sourceSession.EndMapiCall();
						if (flag)
						{
							sourceSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			CoreObject.ProcessCopyPropertyProblems(problems, sourceSession, source);
			return CoreObject.ToXsoPropertyErrors(destSession, destination, problems);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00025F50 File Offset: 0x00024150
		protected override void InternalDispose(bool disposing)
		{
			if (this.propertyBag != null && this.propertyBag.DisposeTracker != null)
			{
				this.propertyBag.DisposeTracker.AddExtraDataWithStackTrace(string.Format(CultureInfo.InvariantCulture, "CoreObject.InternalDispose({0}) called with stack", new object[]
				{
					disposing
				}));
			}
			if (disposing)
			{
				Util.DisposeIfPresent(this.propertyBag);
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00025FB8 File Offset: 0x000241B8
		protected void ValidateCoreObject()
		{
			ValidationContext context = new ValidationContext(this.Session);
			Validation.Validate(this, context);
		}

		// Token: 0x0600043A RID: 1082
		protected abstract Schema GetSchema();

		// Token: 0x0600043B RID: 1083 RVA: 0x00025FD8 File Offset: 0x000241D8
		protected virtual void ValidateContainedObjects(IList<StoreObjectValidationError> validationErrors)
		{
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00025FDA File Offset: 0x000241DA
		protected void UpdateIds()
		{
			this.id = this.PropertyBag.GetValueOrDefault<VersionedId>(this.IdProperty);
			if (this.storeObjectId == null && this.id != null)
			{
				this.storeObjectId = this.id.ObjectId;
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00026014 File Offset: 0x00024214
		private static void ProcessCopyPropertyProblems(PropProblem[] problems, StoreSession sourceSession, MapiProp source)
		{
			if (problems != null)
			{
				for (int i = 0; i < problems.Length; i++)
				{
					int scode = problems[i].Scode;
					if (scode != -2147221233 && scode != -2147221222)
					{
						throw PropertyError.ToException(ServerStrings.MapiCopyFailedProperties, StoreObjectPropertyBag.MapiPropProblemsToPropertyErrors(sourceSession, source, problems));
					}
				}
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00026064 File Offset: 0x00024264
		private static CopyPropertiesFlags ToCopyPropertiesFlags(CopyPropertiesFlags copyPropertiesFlags)
		{
			switch (copyPropertiesFlags)
			{
			case CopyPropertiesFlags.None:
				return CopyPropertiesFlags.None;
			case CopyPropertiesFlags.Move:
				return CopyPropertiesFlags.Move;
			case CopyPropertiesFlags.NoReplace:
				return CopyPropertiesFlags.NoReplace;
			default:
				throw new ArgumentException("copyPropertiesFlags");
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00026098 File Offset: 0x00024298
		private static PropertyError[] ToXsoPropertyErrors(StoreSession session, MapiProp destMapiProp, PropProblem[] problems)
		{
			if (problems == null || problems.Length == 0)
			{
				return MapiPropertyBag.EmptyPropertyErrorArray;
			}
			PropTag[] array = new PropTag[problems.Length];
			int[] array2 = new int[problems.Length];
			for (int i = 0; i < problems.Length; i++)
			{
				array2[i] = problems[i].Scode;
				array[i] = problems[i].PropTag;
			}
			NativeStorePropertyDefinition[] array3 = PropertyTagCache.Cache.PropertyDefinitionsFromPropTags(NativeStorePropertyDefinition.TypeCheckingFlag.DisableTypeCheck, destMapiProp, session, array);
			PropertyError[] array4 = new PropertyError[problems.Length];
			for (int j = 0; j < problems.Length; j++)
			{
				string errorDescription;
				PropertyErrorCode error = MapiPropertyHelper.MapiErrorToXsoError(array2[j], out errorDescription);
				array4[j] = new PropertyError(array3[j], error, errorDescription);
			}
			return array4;
		}

		// Token: 0x04000140 RID: 320
		private readonly PersistablePropertyBag propertyBag;

		// Token: 0x04000141 RID: 321
		private readonly StoreSession session;

		// Token: 0x04000142 RID: 322
		private readonly ItemLevel itemLevel;

		// Token: 0x04000143 RID: 323
		private bool enableFullValidation = true;

		// Token: 0x04000144 RID: 324
		private Origin origin;

		// Token: 0x04000145 RID: 325
		private VersionedId id;

		// Token: 0x04000146 RID: 326
		private StoreObjectId storeObjectId;
	}
}
