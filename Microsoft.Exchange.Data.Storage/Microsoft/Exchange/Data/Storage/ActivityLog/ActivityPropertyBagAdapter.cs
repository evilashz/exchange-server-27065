using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage.ActivityLog
{
	// Token: 0x0200033D RID: 829
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ActivityPropertyBagAdapter : IPropertyBag
	{
		// Token: 0x060024CD RID: 9421 RVA: 0x000947DA File Offset: 0x000929DA
		public ActivityPropertyBagAdapter(MemoryPropertyBag propertyBag)
		{
			Util.ThrowOnNullArgument(propertyBag, "propertyBag");
			this.propertyBag = propertyBag;
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x000947F4 File Offset: 0x000929F4
		public AnnotatedPropertyValue GetAnnotatedProperty(PropertyTag propertyTag)
		{
			if (propertyTag == PropertyTag.Mid)
			{
				return new AnnotatedPropertyValue(propertyTag, new PropertyValue(propertyTag, 0L), null);
			}
			object obj = this.propertyBag.TryGetProperty(ActivityPropertyBagAdapter.PropDefFromPropTag(propertyTag));
			PropertyValue propertyValue = PropertyError.IsPropertyError(obj) ? PropertyValue.Error(propertyTag.PropertyId, (ErrorCode)2147746063U) : new PropertyValue(propertyTag, obj);
			return new AnnotatedPropertyValue(propertyTag, propertyValue, null);
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x00094A54 File Offset: 0x00092C54
		public IEnumerable<AnnotatedPropertyValue> GetAnnotatedProperties()
		{
			foreach (KeyValuePair<PropertyDefinition, object> prop in ((IEnumerable<KeyValuePair<PropertyDefinition, object>>)this.propertyBag))
			{
				KeyValuePair<PropertyDefinition, object> keyValuePair = prop;
				if (!PropertyError.IsPropertyError(keyValuePair.Value))
				{
					KeyValuePair<PropertyDefinition, object> keyValuePair2 = prop;
					PropertyTag propertyTag = new PropertyTag(((PropertyTagPropertyDefinition)keyValuePair2.Key).PropertyTag);
					PropertyTag propertyTag2 = propertyTag;
					PropertyTag propertyTag3 = propertyTag;
					KeyValuePair<PropertyDefinition, object> keyValuePair3 = prop;
					yield return new AnnotatedPropertyValue(propertyTag2, new PropertyValue(propertyTag3, keyValuePair3.Value), null);
				}
			}
			yield break;
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x00094A71 File Offset: 0x00092C71
		public void SetProperty(PropertyValue propertyValue)
		{
			this.propertyBag.SetOrDeleteProperty(ActivityPropertyBagAdapter.PropDefFromPropTag(propertyValue.PropertyTag), propertyValue.Value);
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x00094A91 File Offset: 0x00092C91
		public void Delete(PropertyTag property)
		{
			this.propertyBag.Delete(ActivityPropertyBagAdapter.PropDefFromPropTag(property));
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x00094AA4 File Offset: 0x00092CA4
		public Stream GetPropertyStream(PropertyTag property)
		{
			throw new NotSupportedException("Reading properties as streams is not supported for activities.");
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x00094AB0 File Offset: 0x00092CB0
		public Stream SetPropertyStream(PropertyTag property, long dataSizeEstimate)
		{
			if (property.PropertyType != PropertyType.Binary && property.PropertyType != PropertyType.Unicode)
			{
				throw new NotSupportedException("Writing properties as streams is only supported for binary and unicode properties for activities.");
			}
			return new PropertyBagStream(this.propertyBag, ActivityPropertyBagAdapter.PropDefFromPropTag(property), property.PropertyType, (int)dataSizeEstimate);
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x060024D4 RID: 9428 RVA: 0x00094AF0 File Offset: 0x00092CF0
		public ISession Session
		{
			get
			{
				throw new NotSupportedException("Named properties are not supported for activities.");
			}
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x00094AFC File Offset: 0x00092CFC
		private static PropertyTagPropertyDefinition PropDefFromPropTag(PropertyTag property)
		{
			return PropertyTagPropertyDefinition.InternalCreateCustom(string.Empty, (PropTag)property, PropertyFlags.None, NativeStorePropertyDefinition.TypeCheckingFlag.DisableTypeCheck);
		}

		// Token: 0x04001657 RID: 5719
		private readonly MemoryPropertyBag propertyBag;
	}
}
