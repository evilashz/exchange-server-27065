using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x020001A3 RID: 419
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SingleMemberMultiValuePropertyBag : IPropertyBag
	{
		// Token: 0x06000858 RID: 2136 RVA: 0x0001D550 File Offset: 0x0001B750
		public SingleMemberMultiValuePropertyBag(PropertyTag requestedPropertyTag, int elementCount, IPropertyBag propertyBag)
		{
			this.requestedPropertyTag = requestedPropertyTag;
			this.multiValues = SingleMemberMultiValuePropertyBag.CreateArrayForMultiValue(this.requestedPropertyTag, elementCount);
			this.elementPropertyTag = new PropertyTag(this.requestedPropertyTag.PropertyId, this.requestedPropertyTag.ElementPropertyType);
			this.propertyBag = propertyBag;
			this.elementIndex = 0;
			if (elementCount <= 0)
			{
				this.FlushProperty();
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x0001D5B5 File Offset: 0x0001B7B5
		public PropertyTag ElementPropertyTag
		{
			get
			{
				return this.elementPropertyTag;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x0001D5BD File Offset: 0x0001B7BD
		private int ElementCount
		{
			get
			{
				return this.multiValues.Length;
			}
		}

		// Token: 0x17000165 RID: 357
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x0001D5CA File Offset: 0x0001B7CA
		public int ElementIndex
		{
			set
			{
				this.elementIndex = value;
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001D5D3 File Offset: 0x0001B7D3
		AnnotatedPropertyValue IPropertyBag.GetAnnotatedProperty(PropertyTag propertyTag)
		{
			throw new NotSupportedException("SingleMemberMultiValuePropertyBag does not support IPropertyBag.GetAnnotatedProperty");
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001D5DF File Offset: 0x0001B7DF
		IEnumerable<AnnotatedPropertyValue> IPropertyBag.GetAnnotatedProperties()
		{
			throw new NotSupportedException("SingleMemberMultiValuePropertyBag does not support IPropertyBag.GetAnnotatedProperties");
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001D5EC File Offset: 0x0001B7EC
		void IPropertyBag.SetProperty(PropertyValue propertyValue)
		{
			if (this.elementIndex >= this.ElementCount)
			{
				throw new RopExecutionException("Cannot set more elements then defined in constructor.  This is misuse and a programming error.", ErrorCode.FxUnexpectedMarker);
			}
			this.CheckCorrectProperty(propertyValue.PropertyTag);
			this.multiValues.SetValue(propertyValue.Value, this.elementIndex);
			if (this.elementIndex + 1 >= this.ElementCount)
			{
				this.FlushProperty();
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0001D652 File Offset: 0x0001B852
		void IPropertyBag.Delete(PropertyTag property)
		{
			throw new NotSupportedException("SingleMemberMultiValuePropertyBag does not support IPropertyBag.Delete.");
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0001D65E File Offset: 0x0001B85E
		Stream IPropertyBag.GetPropertyStream(PropertyTag property)
		{
			throw new NotSupportedException("SingleMemberMultiValuePropertyBag does not support IPropertyBag.GetPropertyStream.");
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001D66A File Offset: 0x0001B86A
		Stream IPropertyBag.SetPropertyStream(PropertyTag property, long dataSizeEstimate)
		{
			this.CheckCorrectProperty(property);
			return MemoryPropertyBag.WrapPropertyWriteStream(this, property, dataSizeEstimate);
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x0001D67B File Offset: 0x0001B87B
		ISession IPropertyBag.Session
		{
			get
			{
				throw new NotSupportedException("SingleMemberMultiValuePropertyBag does not have a session and doesn't support named properties.");
			}
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001D688 File Offset: 0x0001B888
		private static Array CreateArrayForMultiValue(PropertyTag propertyTag, int count)
		{
			PropertyType propertyType = propertyTag.PropertyType;
			if (propertyType <= PropertyType.MultiValueUnicode)
			{
				switch (propertyType)
				{
				case PropertyType.MultiValueInt16:
					return new short[count];
				case PropertyType.MultiValueInt32:
					return new int[count];
				case PropertyType.MultiValueFloat:
					return new float[count];
				case PropertyType.MultiValueDouble:
					return new double[count];
				case PropertyType.MultiValueCurrency:
				case PropertyType.MultiValueAppTime:
					break;
				default:
					if (propertyType == PropertyType.MultiValueInt64)
					{
						return new long[count];
					}
					switch (propertyType)
					{
					case PropertyType.MultiValueString8:
					case PropertyType.MultiValueUnicode:
						return new string[count];
					}
					break;
				}
			}
			else
			{
				if (propertyType == PropertyType.MultiValueSysTime)
				{
					return new ExDateTime[count];
				}
				if (propertyType == PropertyType.MultiValueGuid)
				{
					return new Guid[count];
				}
				if (propertyType == PropertyType.MultiValueBinary)
				{
					return new byte[count][];
				}
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0001D745 File Offset: 0x0001B945
		private void CheckCorrectProperty(PropertyTag propertyTag)
		{
			if (propertyTag != this.elementPropertyTag)
			{
				throw new RopExecutionException(string.Format("SingleMemberMultiValuePropertyBag can only store a value for property {0}. Trying to set {1} is a misuse and a programming error.", this.requestedPropertyTag, propertyTag), ErrorCode.FxUnexpectedMarker);
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0001D77C File Offset: 0x0001B97C
		private void FlushProperty()
		{
			PropertyTag propertyTag = this.requestedPropertyTag;
			if (propertyTag.ElementPropertyType == PropertyType.String8)
			{
				propertyTag = propertyTag.ChangeElementPropertyType(PropertyType.Unicode);
			}
			this.propertyBag.SetProperty(new PropertyValue(propertyTag, this.multiValues));
		}

		// Token: 0x040003F8 RID: 1016
		private readonly PropertyTag requestedPropertyTag;

		// Token: 0x040003F9 RID: 1017
		private readonly PropertyTag elementPropertyTag;

		// Token: 0x040003FA RID: 1018
		private readonly Array multiValues;

		// Token: 0x040003FB RID: 1019
		private readonly IPropertyBag propertyBag;

		// Token: 0x040003FC RID: 1020
		private int elementIndex;
	}
}
