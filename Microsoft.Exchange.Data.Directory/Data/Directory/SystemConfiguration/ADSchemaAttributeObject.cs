using System;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000385 RID: 901
	[ObjectScope(ConfigScopes.Global)]
	[Serializable]
	public sealed class ADSchemaAttributeObject : ADNonExchangeObject
	{
		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x0600295B RID: 10587 RVA: 0x000ADEE9 File Offset: 0x000AC0E9
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADSchemaAttributeObject.schema;
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x0600295C RID: 10588 RVA: 0x000ADEF0 File Offset: 0x000AC0F0
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "attributeSchema";
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x0600295D RID: 10589 RVA: 0x000ADEF7 File Offset: 0x000AC0F7
		public Guid SchemaIDGuid
		{
			get
			{
				return (Guid)this[ADSchemaAttributeSchema.SchemaIDGuid];
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x0600295E RID: 10590 RVA: 0x000ADF09 File Offset: 0x000AC109
		public int? RangeUpper
		{
			get
			{
				return (int?)this[ADSchemaAttributeSchema.RangeUpper];
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x0600295F RID: 10591 RVA: 0x000ADF1B File Offset: 0x000AC11B
		public int? RangeLower
		{
			get
			{
				return (int?)this[ADSchemaAttributeSchema.RangeLower];
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06002960 RID: 10592 RVA: 0x000ADF2D File Offset: 0x000AC12D
		public int LinkID
		{
			get
			{
				return (int)this[ADSchemaAttributeSchema.LinkID];
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06002961 RID: 10593 RVA: 0x000ADF3F File Offset: 0x000AC13F
		public int MapiID
		{
			get
			{
				return (int)this[ADSchemaAttributeSchema.MapiID];
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06002962 RID: 10594 RVA: 0x000ADF51 File Offset: 0x000AC151
		public string LdapDisplayName
		{
			get
			{
				return (string)this[ADSchemaAttributeSchema.LdapDisplayName];
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06002963 RID: 10595 RVA: 0x000ADF63 File Offset: 0x000AC163
		public AttributeSyntax OMSyntax
		{
			get
			{
				return (AttributeSyntax)this[ADSchemaAttributeSchema.OMSyntax];
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06002964 RID: 10596 RVA: 0x000ADF75 File Offset: 0x000AC175
		public bool IsMemberOfPartialAttributeSet
		{
			get
			{
				return (bool)this[ADSchemaAttributeSchema.IsMemberOfPartialAttributeSet];
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06002965 RID: 10597 RVA: 0x000ADF87 File Offset: 0x000AC187
		public bool IsSingleValued
		{
			get
			{
				return (bool)this[ADSchemaAttributeSchema.IsSingleValued];
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06002966 RID: 10598 RVA: 0x000ADF99 File Offset: 0x000AC199
		public string AttributeID
		{
			get
			{
				return (string)this[ADSchemaAttributeSchema.AttributeID];
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06002967 RID: 10599 RVA: 0x000ADFAB File Offset: 0x000AC1AB
		public DataSyntax DataSyntax
		{
			get
			{
				return (DataSyntax)this[ADSchemaAttributeSchema.DataSyntax];
			}
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x000ADFC0 File Offset: 0x000AC1C0
		internal static object SyntaxGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[ADSchemaAttributeSchema.RawAttributeSyntax];
			int num = (int)((AttributeSyntax)propertyBag[ADSchemaAttributeSchema.OMSyntax]);
			byte[] values = (byte[])propertyBag[ADSchemaAttributeSchema.OMObjectClass];
			DataSyntax dataSyntax = DataSyntax.UnDefined;
			string key;
			switch (key = text)
			{
			case "2.5.5.0":
				dataSyntax = DataSyntax.UnDefined;
				break;
			case "2.5.5.1":
				dataSyntax = DataSyntax.DSDN;
				break;
			case "2.5.5.2":
				dataSyntax = DataSyntax.ObjectIdentifier;
				break;
			case "2.5.5.3":
				dataSyntax = DataSyntax.CaseSensitive;
				break;
			case "2.5.5.4":
				dataSyntax = DataSyntax.Teletex;
				break;
			case "2.5.5.5":
				if (num == 19)
				{
					dataSyntax = DataSyntax.Printable;
				}
				else if (num == 22)
				{
					dataSyntax = DataSyntax.IA5;
				}
				break;
			case "2.5.5.6":
				dataSyntax = DataSyntax.Numeric;
				break;
			case "2.5.5.7":
			{
				string y = ADSchemaAttributeObject.ToHexString(values);
				if (StringComparer.OrdinalIgnoreCase.Equals("2A864886F7140101010B", y))
				{
					dataSyntax = DataSyntax.DNBinary;
				}
				else if (StringComparer.OrdinalIgnoreCase.Equals("56060102050B1D", y))
				{
					dataSyntax = DataSyntax.ORName;
				}
				break;
			}
			case "2.5.5.8":
				dataSyntax = DataSyntax.Boolean;
				break;
			case "2.5.5.9":
				if (num == 2)
				{
					dataSyntax = DataSyntax.Integer;
				}
				else if (num == 10)
				{
					dataSyntax = DataSyntax.Enumeration;
				}
				break;
			case "2.5.5.10":
				if (num == 4)
				{
					dataSyntax = DataSyntax.Octet;
				}
				else if (num == 127)
				{
					dataSyntax = DataSyntax.ReplicaLink;
				}
				break;
			case "2.5.5.11":
				if (num == 23)
				{
					dataSyntax = DataSyntax.UTCTime;
				}
				else if (num == 24)
				{
					dataSyntax = DataSyntax.GeneralizedTime;
				}
				break;
			case "2.5.5.12":
				dataSyntax = DataSyntax.Unicode;
				break;
			case "2.5.5.13":
				dataSyntax = DataSyntax.PresentationAddress;
				break;
			case "2.5.5.14":
			{
				string y = ADSchemaAttributeObject.ToHexString(values);
				if (StringComparer.OrdinalIgnoreCase.Equals("2B0C0287731C00853E", y))
				{
					dataSyntax = DataSyntax.AccessPoint;
				}
				else if (StringComparer.OrdinalIgnoreCase.Equals("2A864886F7140101010C", y))
				{
					dataSyntax = DataSyntax.DNString;
				}
				break;
			}
			case "2.5.5.15":
				dataSyntax = DataSyntax.NTSecDesc;
				break;
			case "2.5.5.16":
				dataSyntax = DataSyntax.LargeInteger;
				break;
			case "2.5.5.17":
				dataSyntax = DataSyntax.Sid;
				break;
			}
			return dataSyntax;
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x000AE2A8 File Offset: 0x000AC4A8
		private static string ToHexString(byte[] values)
		{
			if (values == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(values.Length * 2);
			foreach (byte b in values)
			{
				stringBuilder.AppendFormat("{0:X2}", b);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400194D RID: 6477
		private const string MostDerivedClass = "attributeSchema";

		// Token: 0x0400194E RID: 6478
		private const string DNBinaryOmObjectClassString = "2A864886F7140101010B";

		// Token: 0x0400194F RID: 6479
		private const string ORNameOmObjectClassString = "56060102050B1D";

		// Token: 0x04001950 RID: 6480
		private const string AccessPointOmObjectClassString = "2B0C0287731C00853E";

		// Token: 0x04001951 RID: 6481
		private const string DNStringOmObjectClassString = "2A864886F7140101010C";

		// Token: 0x04001952 RID: 6482
		private static ADSchemaAttributeSchema schema = ObjectSchema.GetInstance<ADSchemaAttributeSchema>();
	}
}
