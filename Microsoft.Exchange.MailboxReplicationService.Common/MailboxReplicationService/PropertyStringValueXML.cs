using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000C1 RID: 193
	[Serializable]
	public sealed class PropertyStringValueXML : PropertyValueBaseXML
	{
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x0000BD67 File Offset: 0x00009F67
		// (set) Token: 0x06000788 RID: 1928 RVA: 0x0000BD6F File Offset: 0x00009F6F
		[XmlText]
		public string StrValue { get; set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x0000BD78 File Offset: 0x00009F78
		internal override object RawValue
		{
			get
			{
				return this.StrValue;
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0000BD80 File Offset: 0x00009F80
		internal override bool TryGetValue(ProviderPropertyDefinition pdef, out object result)
		{
			if (this.StrValue == null)
			{
				result = null;
				return true;
			}
			ADPropertyDefinition adpropertyDefinition = pdef as ADPropertyDefinition;
			IFormatProvider formatProvider = (adpropertyDefinition != null) ? adpropertyDefinition.FormatProvider : null;
			result = null;
			Exception ex = null;
			if (!ADValueConvertor.TryConvertValueFromString(this.StrValue, pdef.Type, formatProvider, out result, out ex))
			{
				MrsTracer.Common.Warning("Failed to convert {0} from string '{1}': {2}", new object[]
				{
					pdef.Name,
					this.StrValue,
					CommonUtils.FullExceptionMessage(ex)
				});
				return false;
			}
			return true;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0000BE04 File Offset: 0x0000A004
		internal override bool HasValue()
		{
			return !string.IsNullOrEmpty(this.StrValue);
		}
	}
}
