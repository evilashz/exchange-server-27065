using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200011C RID: 284
	internal class FxPropertyBag : IPropertyBag
	{
		// Token: 0x060009DF RID: 2527 RVA: 0x00014143 File Offset: 0x00012343
		public FxPropertyBag(ISession session)
		{
			this.values = new Dictionary<PropertyTag, object>(10);
			this.session = session;
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0001415F File Offset: 0x0001235F
		public ISession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x1700031F RID: 799
		internal object this[PropertyTag propertyTag]
		{
			get
			{
				object result;
				if (!this.values.TryGetValue(propertyTag, out result))
				{
					return null;
				}
				return result;
			}
			set
			{
				this.values[propertyTag] = value;
			}
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00014198 File Offset: 0x00012398
		AnnotatedPropertyValue IPropertyBag.GetAnnotatedProperty(PropertyTag propertyTag)
		{
			NamedProperty namedProperty = null;
			object obj = this[propertyTag];
			PropertyValue propertyValue = (obj != null) ? new PropertyValue(propertyTag, obj) : PropertyValue.Error(propertyTag.PropertyId, (ErrorCode)2147746063U);
			if (propertyTag.IsNamedProperty)
			{
				this.session.TryResolveToNamedProperty(propertyTag, out namedProperty);
				if (namedProperty == null)
				{
					propertyValue = PropertyValue.Error(propertyTag.PropertyId, (ErrorCode)2147746063U);
				}
			}
			return new AnnotatedPropertyValue(propertyTag, propertyValue, namedProperty);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x000143A4 File Offset: 0x000125A4
		IEnumerable<AnnotatedPropertyValue> IPropertyBag.GetAnnotatedProperties()
		{
			foreach (KeyValuePair<PropertyTag, object> kvp in this.values)
			{
				KeyValuePair<PropertyTag, object> keyValuePair = kvp;
				yield return ((IPropertyBag)this).GetAnnotatedProperty(keyValuePair.Key);
			}
			yield break;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x000143C1 File Offset: 0x000125C1
		void IPropertyBag.Delete(PropertyTag property)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x000143C8 File Offset: 0x000125C8
		Stream IPropertyBag.GetPropertyStream(PropertyTag propertyTag)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x000143CF File Offset: 0x000125CF
		Stream IPropertyBag.SetPropertyStream(PropertyTag propertyTag, long dataSizeEstimate)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x000143D6 File Offset: 0x000125D6
		void IPropertyBag.SetProperty(PropertyValue propertyValue)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040005D9 RID: 1497
		private readonly IDictionary<PropertyTag, object> values;

		// Token: 0x040005DA RID: 1498
		private readonly ISession session;
	}
}
