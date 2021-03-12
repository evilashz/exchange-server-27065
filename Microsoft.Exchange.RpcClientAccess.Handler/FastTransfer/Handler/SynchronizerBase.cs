using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Exchange.RpcClientAccess.Handler;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x0200005B RID: 91
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class SynchronizerBase : BaseObject
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x0001C8F4 File Offset: 0x0001AAF4
		protected SynchronizerBase(ReferenceCount<CoreFolder> syncRootFolder, SyncFlag syncFlags, SyncExtraFlag extraFlags, IcsState icsState)
		{
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				disposeGuard.Add<SynchronizerBase>(this);
				this.SyncRootFolder = syncRootFolder;
				this.SyncRootFolder.AddRef();
				this.SessionAdaptor = new SessionAdaptor(this.SyncRootFolder.ReferencedObject.Session);
				this.SyncFlags = syncFlags;
				this.ExtraFlags = extraFlags;
				this.IcsState = icsState;
				disposeGuard.Success();
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001C984 File Offset: 0x0001AB84
		protected static void CheckRequiredProperties(IPropertyBag propertyBag, IEnumerable<PropertyTag> requiredProperties)
		{
			using (IEnumerator<PropertyTag> enumerator = propertyBag.WithNoValue(requiredProperties).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					PropertyTag propertyTag = enumerator.Current;
					throw new RopExecutionException(string.Format("Required property {0} is missing", propertyTag), (ErrorCode)2147942487U);
				}
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001C9F0 File Offset: 0x0001ABF0
		protected static void SetPropertyValuesFromServer(IPropertyBag propertyBag, StoreSession session, PropValue[] xsoPropValues)
		{
			NativeStorePropertyDefinition[] array = new NativeStorePropertyDefinition[xsoPropValues.Length];
			object[] array2 = new object[xsoPropValues.Length];
			for (int i = 0; i < xsoPropValues.Length; i++)
			{
				array[i] = (NativeStorePropertyDefinition)xsoPropValues[i].Property;
				array2[i] = xsoPropValues[i].Value;
			}
			ICollection<uint> first = PropertyTagCache.Cache.PropertyTagsFromPropertyDefinitions(session, array);
			ICollection<PropertyTag> propertyTags = from propertyTag in first
			select new PropertyTag(propertyTag);
			bool useUnicodeForRestrictions = true;
			PropertyValue[] array3 = MEDSPropertyTranslator.TranslatePropertyValues(session, propertyTags, array2, useUnicodeForRestrictions);
			foreach (PropertyValue property in array3)
			{
				if (!property.IsError)
				{
					propertyBag.SetProperty(property);
				}
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0001CABA File Offset: 0x0001ACBA
		protected static void TranslateFlag(SyncFlag sourceFlag, ManifestConfigFlags destinationFlag, SyncFlag sourceFlags, ref ManifestConfigFlags destinationFlags)
		{
			if ((sourceFlags & sourceFlag) == sourceFlag)
			{
				destinationFlags |= destinationFlag;
				return;
			}
			destinationFlags &= ~destinationFlag;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0001CAD4 File Offset: 0x0001ACD4
		protected byte[] ConvertIdToLongTermId(IPropertyBag propertyBag, PropertyTag property)
		{
			return this.SyncRootFolder.ReferencedObject.Session.IdConverter.GetLongTermIdFromId(propertyBag.GetAnnotatedProperty(property).PropertyValue.GetServerValue<long>());
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0001CB0F File Offset: 0x0001AD0F
		protected override void InternalDispose()
		{
			this.SyncRootFolder.Release();
			base.InternalDispose();
		}

		// Token: 0x0400013E RID: 318
		protected readonly ISession SessionAdaptor;

		// Token: 0x0400013F RID: 319
		protected readonly ReferenceCount<CoreFolder> SyncRootFolder;

		// Token: 0x04000140 RID: 320
		protected readonly SyncExtraFlag ExtraFlags;

		// Token: 0x04000141 RID: 321
		protected readonly SyncFlag SyncFlags;

		// Token: 0x04000142 RID: 322
		protected readonly IcsState IcsState;
	}
}
