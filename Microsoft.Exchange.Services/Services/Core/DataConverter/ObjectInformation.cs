using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200020D RID: 525
	internal sealed class ObjectInformation : XmlElementInformation
	{
		// Token: 0x06000D9B RID: 3483 RVA: 0x00043EC5 File Offset: 0x000420C5
		internal ObjectInformation(string localName, ExchangeVersion effectiveVersion, Type associatedType, StoreObjectType[] associatedStoreObjectTypes, Shape.CreateShapeCallback createShape, Shape.CreateShapeForPropertyBagCallback createShapeForPropertyBag, Shape.CreateShapeForStoreObjectCallback createShapeForStoreObject, ObjectInformation priorVersionObjectInformation) : base(localName, string.Empty, effectiveVersion)
		{
			this.associatedType = associatedType;
			this.associatedStoreObjectTypes = associatedStoreObjectTypes;
			this.createShape = createShape;
			this.createShapeForPropertyBag = createShapeForPropertyBag;
			this.createShapeForStoreObject = createShapeForStoreObject;
			this.priorVersionObjectInformation = priorVersionObjectInformation;
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00043F04 File Offset: 0x00042104
		internal ObjectInformation(string localName, ExchangeVersion effectiveVersion, Type associatedType, StoreObjectType[] associatedStoreObjectTypes, Shape.CreateShapeCallback createShape, ObjectInformation priorVersionObjectInformation) : this(localName, effectiveVersion, associatedType, associatedStoreObjectTypes, createShape, null, null, priorVersionObjectInformation)
		{
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x00043F22 File Offset: 0x00042122
		internal Type AssociatedType
		{
			get
			{
				return this.associatedType;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x00043F2A File Offset: 0x0004212A
		internal StoreObjectType[] AssociatedStoreObjectTypes
		{
			get
			{
				return this.associatedStoreObjectTypes;
			}
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00043F34 File Offset: 0x00042134
		private ObjectInformation GetRequestVersionObjectInformation()
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<ExchangeVersion, ExchangeVersion>(0L, "ObjectInformation.GetRequestVersionObjectInformation: ObjectInformation effectiveVersion = {0}, Request version = {1}", this.effectiveVersion, ExchangeVersion.Current);
			if (ExchangeVersion.Current.Supports(this.effectiveVersion))
			{
				return this;
			}
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "ObjectInformation.GetRequestVersionObjectInformation: ObjectInformation not supported in request version. Examine priorVersionObjectInformation.");
			return this.priorVersionObjectInformation.GetRequestVersionObjectInformation();
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00043F90 File Offset: 0x00042190
		internal Shape CreateShape(bool forPropertyBag)
		{
			ObjectInformation requestVersionObjectInformation = this.GetRequestVersionObjectInformation();
			Shape result;
			if (forPropertyBag && requestVersionObjectInformation.createShapeForPropertyBag != null)
			{
				result = requestVersionObjectInformation.createShapeForPropertyBag();
			}
			else
			{
				result = requestVersionObjectInformation.createShape();
			}
			return result;
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00043FCC File Offset: 0x000421CC
		private Shape PrivateCreateShape(StoreObject storeObject)
		{
			ObjectInformation requestVersionObjectInformation = this.GetRequestVersionObjectInformation();
			Shape result;
			if (requestVersionObjectInformation.createShapeForStoreObject != null)
			{
				result = requestVersionObjectInformation.createShapeForStoreObject(storeObject);
			}
			else
			{
				result = requestVersionObjectInformation.createShape();
			}
			return result;
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00044004 File Offset: 0x00042204
		internal static Shape CreateShape(StoreObject storeObject)
		{
			ObjectInformation objectInformation = Schema.GetObjectInformation(storeObject);
			return objectInformation.PrivateCreateShape(storeObject);
		}

		// Token: 0x04000AAA RID: 2730
		private Type associatedType;

		// Token: 0x04000AAB RID: 2731
		private StoreObjectType[] associatedStoreObjectTypes;

		// Token: 0x04000AAC RID: 2732
		private Shape.CreateShapeCallback createShape;

		// Token: 0x04000AAD RID: 2733
		private Shape.CreateShapeForPropertyBagCallback createShapeForPropertyBag;

		// Token: 0x04000AAE RID: 2734
		private Shape.CreateShapeForStoreObjectCallback createShapeForStoreObject;

		// Token: 0x04000AAF RID: 2735
		private ObjectInformation priorVersionObjectInformation;

		// Token: 0x04000AB0 RID: 2736
		internal static readonly ObjectInformation NoPriorVersionObjectInformation;
	}
}
