using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000347 RID: 839
	[Serializable]
	public class ADEmailTransport : ADLegacyVersionableObject
	{
		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x060026EF RID: 9967 RVA: 0x000A502F File Offset: 0x000A322F
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADEmailTransport.schema;
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x060026F0 RID: 9968 RVA: 0x000A5036 File Offset: 0x000A3236
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADEmailTransport.mostDerivedClass;
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x060026F1 RID: 9969 RVA: 0x000A5040 File Offset: 0x000A3240
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, Pop3AdConfiguration.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, Imap4AdConfiguration.MostDerivedClass)
				});
			}
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x000A5080 File Offset: 0x000A3280
		internal static object ServerGetter(IPropertyBag propertyBag)
		{
			object result;
			try
			{
				ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
				if (adobjectId == null && (ObjectState)propertyBag[ADObjectSchema.ObjectState] != ObjectState.New)
				{
					throw new InvalidOperationException(DirectoryStrings.IdIsNotSet);
				}
				result = ((adobjectId == null) ? null : adobjectId.AncestorDN(3));
			}
			catch (InvalidOperationException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("Server", ex.Message), ADEmailTransportSchema.Server, propertyBag[ADObjectSchema.Id]), ex);
			}
			return result;
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x060026F3 RID: 9971 RVA: 0x000A5114 File Offset: 0x000A3314
		public ADObjectId Server
		{
			get
			{
				return (ADObjectId)this[ADEmailTransportSchema.Server];
			}
		}

		// Token: 0x040017C0 RID: 6080
		private static readonly string mostDerivedClass = "protocolCfg";

		// Token: 0x040017C1 RID: 6081
		private static readonly ADObjectSchema schema = ObjectSchema.GetInstance<ADEmailTransportProperties>();
	}
}
