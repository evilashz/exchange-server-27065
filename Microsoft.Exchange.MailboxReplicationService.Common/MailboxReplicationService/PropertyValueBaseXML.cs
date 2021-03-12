using System;
using System.Security.AccessControl;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000BE RID: 190
	[Serializable]
	public abstract class PropertyValueBaseXML : XMLSerializableBase
	{
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000765 RID: 1893
		internal abstract object RawValue { get; }

		// Token: 0x06000766 RID: 1894 RVA: 0x0000BA23 File Offset: 0x00009C23
		public override string ToString()
		{
			return string.Format("{0}", this.RawValue);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0000BA38 File Offset: 0x00009C38
		internal static PropertyValueBaseXML Create(ProviderPropertyDefinition pdef, object value)
		{
			ADPropertyDefinition adpropertyDefinition = pdef as ADPropertyDefinition;
			IFormatProvider formatProvider = (adpropertyDefinition != null) ? adpropertyDefinition.FormatProvider : null;
			ADObjectId adobjectId = value as ADObjectId;
			if (adobjectId != null)
			{
				return ADObjectIdXML.Serialize(adobjectId);
			}
			OrganizationId organizationId = value as OrganizationId;
			if (organizationId != null)
			{
				return OrganizationIdXML.Serialize(organizationId);
			}
			RawSecurityDescriptor rawSecurityDescriptor = value as RawSecurityDescriptor;
			if (rawSecurityDescriptor != null)
			{
				return new PropertyStringValueXML
				{
					StrValue = CommonUtils.GetSDDLString(rawSecurityDescriptor)
				};
			}
			Exception ex;
			if (pdef.IsBinary)
			{
				byte[] binValue;
				if (ADValueConvertor.TryConvertValueToBinary(value, formatProvider, out binValue, out ex))
				{
					return new PropertyBinaryValueXML
					{
						BinValue = binValue
					};
				}
				MrsTracer.Common.Warning("Failed to convert {0} to binary, will try string: {1}", new object[]
				{
					pdef.Name,
					CommonUtils.FullExceptionMessage(ex)
				});
			}
			PropertyStringValueXML propertyStringValueXML = new PropertyStringValueXML();
			string text;
			if (!ADValueConvertor.TryConvertValueToString(value, formatProvider, out text, out ex))
			{
				text = value.ToString();
				MrsTracer.Common.Warning("Failed to convert {0} to string, defaulting to '{1}': {2}", new object[]
				{
					pdef.Name,
					text,
					CommonUtils.FullExceptionMessage(ex)
				});
			}
			propertyStringValueXML.StrValue = text;
			return propertyStringValueXML;
		}

		// Token: 0x06000768 RID: 1896
		internal abstract bool TryGetValue(ProviderPropertyDefinition pdef, out object result);

		// Token: 0x06000769 RID: 1897
		internal abstract bool HasValue();
	}
}
