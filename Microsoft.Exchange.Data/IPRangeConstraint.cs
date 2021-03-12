using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000161 RID: 353
	[Serializable]
	internal class IPRangeConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06000B7D RID: 2941 RVA: 0x000244AF File Offset: 0x000226AF
		public IPRangeConstraint(ulong maxIPRange = 256UL)
		{
			this.maxIPRange = maxIPRange;
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x000244BE File Offset: 0x000226BE
		public ulong MaxIpRange
		{
			get
			{
				return this.maxIPRange;
			}
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x000244C8 File Offset: 0x000226C8
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			IPvxAddress pvxAddress = new IPvxAddress(0UL, this.maxIPRange);
			IPRange iprange = value as IPRange;
			if (iprange != null && pvxAddress.CompareTo(iprange.Size) < 0)
			{
				return new PropertyConstraintViolationError(DataStrings.ConstraintViolationIpRangeNotAllowed(iprange.ToString(), this.maxIPRange), propertyDefinition, value, this);
			}
			return null;
		}

		// Token: 0x04000713 RID: 1811
		private const ulong DefaultMaxIPRange = 256UL;

		// Token: 0x04000714 RID: 1812
		private readonly ulong maxIPRange;
	}
}
