using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000092 RID: 146
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class PersistablePropertyBag : PropertyBag, ICorePropertyBag, ILocationIdentifierSetter, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000A22 RID: 2594 RVA: 0x000468AE File Offset: 0x00044AAE
		protected PersistablePropertyBag()
		{
			StorageGlobals.TraceConstructIDisposable(this);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x000468D3 File Offset: 0x00044AD3
		public DisposeTracker DisposeTracker
		{
			get
			{
				return this.disposeTracker;
			}
		}

		// Token: 0x06000A24 RID: 2596
		public abstract DisposeTracker GetDisposeTracker();

		// Token: 0x06000A25 RID: 2597 RVA: 0x000468DB File Offset: 0x00044ADB
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x000468F0 File Offset: 0x00044AF0
		protected void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00046912 File Offset: 0x00044B12
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00046921 File Offset: 0x00044B21
		private void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.isDisposed, disposing);
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00046946 File Offset: 0x00044B46
		protected virtual void InternalDispose(bool disposing)
		{
			if (disposing && this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000A2A RID: 2602
		public abstract ICollection<PropertyDefinition> AllFoundProperties { get; }

		// Token: 0x06000A2B RID: 2603 RVA: 0x0004695E File Offset: 0x00044B5E
		public virtual void Reload()
		{
			throw new NotSupportedException("Currently this is only supported by AcrPropertyBag and StoreObjectPropertyBag.");
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x0004696A File Offset: 0x00044B6A
		// (set) Token: 0x06000A2D RID: 2605 RVA: 0x0004697D File Offset: 0x00044B7D
		internal virtual ICollection<PropertyDefinition> PrefetchPropertyArray
		{
			get
			{
				this.CheckDisposed("PrefetchPropertyArray::get");
				return this.prefetchProperties;
			}
			set
			{
				this.CheckDisposed("PrefetchPropertyArray::set");
				this.prefetchProperties = (value ?? PersistablePropertyBag.empty);
			}
		}

		// Token: 0x06000A2E RID: 2606
		internal abstract void FlushChanges();

		// Token: 0x06000A2F RID: 2607
		internal abstract void SaveChanges(bool force);

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000A30 RID: 2608
		internal abstract ICollection<NativeStorePropertyDefinition> AllNativeProperties { get; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000A31 RID: 2609
		public abstract bool HasAllPropertiesLoaded { get; }

		// Token: 0x06000A32 RID: 2610
		public abstract void Clear();

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000A33 RID: 2611
		// (set) Token: 0x06000A34 RID: 2612
		internal abstract PropertyBagSaveFlags SaveFlags { get; set; }

		// Token: 0x06000A35 RID: 2613
		internal abstract void SetUpdateImapIdFlag();

		// Token: 0x06000A36 RID: 2614
		public abstract Stream OpenPropertyStream(PropertyDefinition propertyDefinition, PropertyOpenMode openMode);

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000A37 RID: 2615
		internal abstract MapiProp MapiProp { get; }

		// Token: 0x06000A38 RID: 2616 RVA: 0x0004699C File Offset: 0x00044B9C
		T ICorePropertyBag.GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition)
		{
			return base.GetValueOrDefault<T>(propertyDefinition, default(T));
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x000469B9 File Offset: 0x00044BB9
		T ICorePropertyBag.GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition, T defaultValue)
		{
			return PropertyBag.CheckPropertyValue<T>(propertyDefinition, base.TryGetProperty(propertyDefinition), defaultValue);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x000469C9 File Offset: 0x00044BC9
		T? ICorePropertyBag.GetValueAsNullable<T>(StorePropertyDefinition propertyDefinition)
		{
			return PropertyBag.CheckNullablePropertyValue<T>(propertyDefinition, base.TryGetProperty(propertyDefinition));
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x000469D8 File Offset: 0x00044BD8
		internal static void CopyProperty(PersistablePropertyBag source, PropertyDefinition property, PersistablePropertyBag destination)
		{
			object obj = source.TryGetProperty(property);
			PropertyError propertyError = obj as PropertyError;
			if (propertyError == null)
			{
				destination[property] = obj;
				return;
			}
			if (PropertyError.IsPropertyValueTooBig(propertyError))
			{
				Stream stream;
				Stream readStream = stream = source.OpenPropertyStream(property, PropertyOpenMode.ReadOnly);
				try
				{
					Stream stream2;
					Stream writeStream = stream2 = destination.OpenPropertyStream(property, PropertyOpenMode.Create);
					try
					{
						Util.StreamHandler.CopyStreamData(readStream, writeStream);
					}
					finally
					{
						if (stream2 != null)
						{
							((IDisposable)stream2).Dispose();
						}
					}
				}
				finally
				{
					if (stream != null)
					{
						((IDisposable)stream).Dispose();
					}
				}
			}
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00046A64 File Offset: 0x00044C64
		internal static void CopyProperties(PersistablePropertyBag source, PersistablePropertyBag destination, params PropertyDefinition[] properties)
		{
			foreach (PropertyDefinition property in properties)
			{
				PersistablePropertyBag.CopyProperty(source, property, destination);
			}
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00046A8D File Offset: 0x00044C8D
		internal static PersistablePropertyBag GetPersistablePropertyBag(ICorePropertyBag corePropertyBag)
		{
			return (PersistablePropertyBag)corePropertyBag;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00046A98 File Offset: 0x00044C98
		internal byte[] GetLargeBinaryProperty(PropertyDefinition propertyDefinition)
		{
			object obj = base.TryGetProperty(propertyDefinition);
			byte[] array = obj as byte[];
			if (array != null)
			{
				return array;
			}
			if (PropertyError.IsPropertyValueTooBig(obj))
			{
				ExTraceGlobals.StorageTracer.Information<PropertyDefinition>((long)this.GetHashCode(), "PersitablePropertyBag::GetLargeBinaryProperty, {0} too big to fit in GetProp, streaming it", propertyDefinition);
				using (Stream stream = this.OpenPropertyStream(propertyDefinition, PropertyOpenMode.ReadOnly))
				{
					return Util.ReadByteArray(stream);
				}
			}
			PropertyError propertyError = obj as PropertyError;
			if (propertyError != null && propertyError.PropertyErrorCode == PropertyErrorCode.NotFound)
			{
				ExTraceGlobals.StorageTracer.Information<PropertyDefinition>((long)this.GetHashCode(), "PersitablePropertyBag::GetLargeBinaryProperty, {0} not found", propertyDefinition);
				return null;
			}
			ExTraceGlobals.StorageTracer.TraceError<PropertyDefinition>((long)this.GetHashCode(), "PersitablePropertyBag::GetLargeBinaryProperty, Error when accessing {0}", propertyDefinition);
			throw new CorruptDataException(ServerStrings.ErrorAccessingLargeProperty);
		}

		// Token: 0x0400028C RID: 652
		private static ICollection<PropertyDefinition> empty = Array<PropertyDefinition>.Empty;

		// Token: 0x0400028D RID: 653
		private ICollection<PropertyDefinition> prefetchProperties = PersistablePropertyBag.empty;

		// Token: 0x0400028E RID: 654
		private bool isDisposed;

		// Token: 0x0400028F RID: 655
		private readonly DisposeTracker disposeTracker;
	}
}
