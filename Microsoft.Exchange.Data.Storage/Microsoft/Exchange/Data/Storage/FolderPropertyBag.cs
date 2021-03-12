using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000095 RID: 149
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FolderPropertyBag : StoreObjectPropertyBag
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x00047880 File Offset: 0x00045A80
		protected StoreSession Session
		{
			get
			{
				return this.storeSession;
			}
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00047888 File Offset: 0x00045A88
		internal FolderPropertyBag(StoreSession session, MapiFolder mapiFolder, ICollection<PropertyDefinition> properties) : base(session, mapiFolder, properties)
		{
			this.storeSession = session;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0004789A File Offset: 0x00045A9A
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FolderPropertyBag>(this);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x000478A2 File Offset: 0x00045AA2
		internal sealed override void FlushChanges()
		{
			throw new InvalidOperationException(ServerStrings.ExFolderPropertyBagCannotSaveChanges);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x000478B3 File Offset: 0x00045AB3
		internal sealed override void SaveChanges(bool force)
		{
			throw new InvalidOperationException(ServerStrings.ExFolderPropertyBagCannotSaveChanges);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x000478C4 File Offset: 0x00045AC4
		public sealed override Stream OpenPropertyStream(PropertyDefinition propertyDefinition, PropertyOpenMode openMode)
		{
			EnumValidator.AssertValid<PropertyOpenMode>(openMode);
			NativeStorePropertyDefinition nativeStorePropertyDefinition = propertyDefinition as NativeStorePropertyDefinition;
			if (nativeStorePropertyDefinition == null)
			{
				throw new InvalidOperationException(ServerStrings.ExPropertyNotStreamable(propertyDefinition.ToString()));
			}
			return new FolderPropertyStream(base.MapiPropertyBag, nativeStorePropertyDefinition, openMode);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00047908 File Offset: 0x00045B08
		internal virtual FolderSaveResult SaveFolderPropertyBag(bool needVersionCheck)
		{
			base.CheckDisposed("SaveFolderPropertyBag");
			base.BindToMapiPropertyBag();
			LocalizedException ex = null;
			List<PropertyError> list = new List<PropertyError>();
			try
			{
				if (needVersionCheck)
				{
					this.SaveFlags |= PropertyBagSaveFlags.SaveFolderPropertyBagConditional;
				}
				else
				{
					this.SaveFlags &= ~PropertyBagSaveFlags.SaveFolderPropertyBagConditional;
				}
				base.MapiPropertyBag.SaveFlags = this.SaveFlags;
				list.AddRange(base.FlushSetProperties());
			}
			catch (FolderSaveConditionViolationException ex2)
			{
				list.AddRange(this.ConvertSetPropsToErrors(PropertyErrorCode.FolderHasChanged, ServerStrings.ExFolderSetPropsFailed(ex2.ToString())));
				return new FolderSaveResult(OperationResult.Failed, ex2, list.ToArray());
			}
			catch (StoragePermanentException ex3)
			{
				ex = ex3;
			}
			catch (StorageTransientException ex4)
			{
				ex = ex4;
			}
			finally
			{
				if (ex != null)
				{
					ExTraceGlobals.StorageTracer.TraceDebug<LocalizedException>((long)this.GetHashCode(), "FolderPropertyBag::SaveFolderPropertyBag. Exception caught while setting properties. Exception = {0}.", ex);
					PropertyErrorCode errorCode = (ex is StorageTransientException) ? PropertyErrorCode.TransientMapiCallFailed : PropertyErrorCode.MapiCallFailed;
					if (ex is ObjectExistedException && base.MemoryPropertyBag.ChangeList.Contains(InternalSchema.DisplayName))
					{
						errorCode = PropertyErrorCode.FolderNameConflict;
					}
					list.AddRange(this.ConvertSetPropsToErrors(errorCode, ServerStrings.ExFolderSetPropsFailed(ex.ToString())));
				}
			}
			LocalizedException ex5 = null;
			try
			{
				list.AddRange(base.FlushDeleteProperties());
			}
			catch (StoragePermanentException ex6)
			{
				ex5 = ex6;
			}
			catch (StorageTransientException ex7)
			{
				ex5 = ex7;
			}
			finally
			{
				if (ex5 != null)
				{
					ExTraceGlobals.StorageTracer.TraceDebug<LocalizedException>((long)this.GetHashCode(), "FolderPropertyBag::SaveFolderPropertyBag. Exception caught while deleting properties. Exception = {0}.", ex5);
					PropertyErrorCode error = (ex5 is StorageTransientException) ? PropertyErrorCode.TransientMapiCallFailed : PropertyErrorCode.MapiCallFailed;
					foreach (PropertyDefinition propertyDefinition in base.MemoryPropertyBag.DeleteList)
					{
						list.Add(new PropertyError(propertyDefinition, error, ServerStrings.ExFolderDeletePropsFailed(ex5.ToString())));
					}
				}
			}
			try
			{
				base.MapiPropertyBag.SaveChanges(false);
			}
			catch (StoragePermanentException arg)
			{
				ExTraceGlobals.StorageTracer.TraceError<StoragePermanentException>((long)this.GetHashCode(), "FolderPropertyBag::SaveFolderPropertyBag. Exception caught when calling MapiFolder.SaveChanges. Exception = {0}.", arg);
				if (this.Session != null && this.Session.IsMoveUser)
				{
					throw;
				}
			}
			catch (StorageTransientException arg2)
			{
				ExTraceGlobals.StorageTracer.TraceError<StorageTransientException>((long)this.GetHashCode(), "FolderPropertyBag::SaveFolderPropertyBag. Exception caught when calling MapiFolder.SaveChanges. Exception = {0}.", arg2);
				if (this.Session != null && this.Session.IsMoveUser)
				{
					throw;
				}
			}
			this.Clear();
			if (list.Count == 0)
			{
				return FolderPropertyBag.SuccessfulSave;
			}
			return new FolderSaveResult(OperationResult.PartiallySucceeded, ex5 ?? ex, list.ToArray());
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00047BE0 File Offset: 0x00045DE0
		protected override bool ShouldSkipProperty(PropertyDefinition property, out PropertyErrorCode? error)
		{
			return base.ShouldSkipProperty(property, out error) || !this.Session.IsValidOperation(this.Context.CoreObject, property, out error);
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x00047C09 File Offset: 0x00045E09
		public override bool CanIgnoreUnchangedProperties
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00047C0C File Offset: 0x00045E0C
		private IList<PropertyError> ConvertSetPropsToErrors(PropertyErrorCode errorCode, string errorMessage)
		{
			List<PropertyError> list = new List<PropertyError>();
			foreach (PropertyDefinition propertyDefinition in base.MemoryPropertyBag.ChangeList)
			{
				object obj = base.MemoryPropertyBag.TryGetProperty(propertyDefinition);
				if (!(obj is PropertyError))
				{
					list.Add(new PropertyError(propertyDefinition, errorCode, errorMessage));
				}
			}
			return list;
		}

		// Token: 0x0400029A RID: 666
		internal static readonly FolderSaveResult SuccessfulSave = new FolderSaveResult(OperationResult.Succeeded, null, Array<PropertyError>.Empty);

		// Token: 0x0400029B RID: 667
		private readonly StoreSession storeSession;
	}
}
