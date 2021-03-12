using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x020001A4 RID: 420
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SingleMemberPropertyBag : IPropertyBag
	{
		// Token: 0x06000866 RID: 2150 RVA: 0x0001D7BC File Offset: 0x0001B9BC
		public SingleMemberPropertyBag(PropertyTag requestedProperyTag)
		{
			if (requestedProperyTag.IsNamedProperty)
			{
				throw new ArgumentException("Should not be a named property", "requestedProperyTag");
			}
			this.requestedProperyTag = requestedProperyTag;
			this.ResetValue();
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x0001D7EA File Offset: 0x0001B9EA
		public PropertyValue PropertyValue
		{
			get
			{
				return this.propertyValue;
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001D7F4 File Offset: 0x0001B9F4
		AnnotatedPropertyValue IPropertyBag.GetAnnotatedProperty(PropertyTag propertyTag)
		{
			PropertyValue property = this.GetProperty(propertyTag);
			return new AnnotatedPropertyValue(propertyTag, property, null);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001D814 File Offset: 0x0001BA14
		IEnumerable<AnnotatedPropertyValue> IPropertyBag.GetAnnotatedProperties()
		{
			return new AnnotatedPropertyValue[]
			{
				((IPropertyBag)this).GetAnnotatedProperty(this.propertyValue.PropertyTag)
			};
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001D846 File Offset: 0x0001BA46
		void IPropertyBag.SetProperty(PropertyValue propertyValue)
		{
			this.CheckCorrectProperty(propertyValue.PropertyTag);
			this.propertyValue = propertyValue;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001D85C File Offset: 0x0001BA5C
		void IPropertyBag.Delete(PropertyTag property)
		{
			this.CheckCorrectProperty(property);
			this.ResetValue();
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001D86B File Offset: 0x0001BA6B
		Stream IPropertyBag.GetPropertyStream(PropertyTag property)
		{
			this.CheckCorrectProperty(property);
			return MemoryPropertyBag.WrapPropertyReadStream(MemoryPropertyBag.ConvertToRequestedType(this.propertyValue, property.PropertyType));
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0001D88B File Offset: 0x0001BA8B
		Stream IPropertyBag.SetPropertyStream(PropertyTag property, long dataSizeEstimate)
		{
			this.CheckCorrectProperty(property);
			return MemoryPropertyBag.WrapPropertyWriteStream(this, property, dataSizeEstimate);
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x0001D89C File Offset: 0x0001BA9C
		ISession IPropertyBag.Session
		{
			get
			{
				throw new NotSupportedException("SingleMemberPropertyBag does not have a session and doesn't support named properties");
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0001D8A8 File Offset: 0x0001BAA8
		private void CheckCorrectProperty(PropertyTag propertyTag)
		{
			if (propertyTag.PropertyId != this.requestedProperyTag.PropertyId)
			{
				throw new RopExecutionException(string.Format("SingleMemberPropertyBag can only store a value for property {0}. Asking for {1} is a misuse and a programming error", this.requestedProperyTag.PropertyId, propertyTag.PropertyId), ErrorCode.FxUnexpectedMarker);
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001D900 File Offset: 0x0001BB00
		private void ResetValue()
		{
			this.propertyValue = PropertyValue.Error(this.requestedProperyTag.PropertyId, (ErrorCode)2147746063U);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001D92B File Offset: 0x0001BB2B
		private PropertyValue GetProperty(PropertyTag property)
		{
			this.CheckCorrectProperty(property);
			return MemoryPropertyBag.ConvertToRequestedType(this.propertyValue, property.PropertyType);
		}

		// Token: 0x040003FD RID: 1021
		private readonly PropertyTag requestedProperyTag;

		// Token: 0x040003FE RID: 1022
		private PropertyValue propertyValue;
	}
}
