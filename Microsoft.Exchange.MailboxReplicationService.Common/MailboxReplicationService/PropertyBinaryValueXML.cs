using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000C2 RID: 194
	[Serializable]
	public sealed class PropertyBinaryValueXML : PropertyValueBaseXML
	{
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x0000BE1C File Offset: 0x0000A01C
		// (set) Token: 0x0600078E RID: 1934 RVA: 0x0000BE29 File Offset: 0x0000A029
		[XmlText]
		public string StrValue
		{
			get
			{
				return Convert.ToBase64String(this.BinValue);
			}
			set
			{
				this.BinValue = Convert.FromBase64String(value);
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x0000BE37 File Offset: 0x0000A037
		// (set) Token: 0x06000790 RID: 1936 RVA: 0x0000BE3F File Offset: 0x0000A03F
		[XmlIgnore]
		public byte[] BinValue { get; set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x0000BE48 File Offset: 0x0000A048
		internal override object RawValue
		{
			get
			{
				return this.BinValue;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0000BE50 File Offset: 0x0000A050
		internal Guid? AsGuid
		{
			get
			{
				if (this.BinValue != null && this.BinValue.Length == 16)
				{
					return new Guid?(new Guid(this.BinValue));
				}
				return null;
			}
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0000BE8B File Offset: 0x0000A08B
		public override string ToString()
		{
			return string.Format("{0}", TraceUtils.DumpEntryId(this.BinValue));
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0000BEA4 File Offset: 0x0000A0A4
		internal override bool TryGetValue(ProviderPropertyDefinition pdef, out object result)
		{
			if (this.BinValue == null)
			{
				result = null;
				return true;
			}
			ADPropertyDefinition adpropertyDefinition = pdef as ADPropertyDefinition;
			IFormatProvider formatProvider = (adpropertyDefinition != null) ? adpropertyDefinition.FormatProvider : null;
			result = null;
			Exception ex = null;
			if (!ADValueConvertor.TryConvertValueFromBinary(this.BinValue, pdef.Type, formatProvider, out result, out ex))
			{
				MrsTracer.Common.Warning("Failed to convert {0} from binary ({1} bytes): {2}", new object[]
				{
					pdef.Name,
					this.BinValue.Length,
					CommonUtils.FullExceptionMessage(ex)
				});
				return false;
			}
			return true;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0000BF2F File Offset: 0x0000A12F
		internal override bool HasValue()
		{
			return this.BinValue != null && this.BinValue.Length > 0;
		}
	}
}
