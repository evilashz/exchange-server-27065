using System;
using System.Net.Sockets;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000164 RID: 356
	[Serializable]
	internal class IPv6AddressesNotAllowedConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06000BC0 RID: 3008 RVA: 0x00024E8C File Offset: 0x0002308C
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			IPRange iprange = value as IPRange;
			if (iprange != null && (iprange.LowerBound.AddressFamily == AddressFamily.InterNetworkV6 || iprange.UpperBound.AddressFamily == AddressFamily.InterNetworkV6))
			{
				return new PropertyConstraintViolationError(DataStrings.ConstraintViolationIpV6NotAllowed(iprange.ToString()), propertyDefinition, value, this);
			}
			return null;
		}
	}
}
