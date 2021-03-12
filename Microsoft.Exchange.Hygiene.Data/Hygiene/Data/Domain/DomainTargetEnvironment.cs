using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Domain
{
	// Token: 0x02000128 RID: 296
	[Serializable]
	internal class DomainTargetEnvironment : ConfigurablePropertyBag, ISerializable
	{
		// Token: 0x06000B5A RID: 2906 RVA: 0x00024A04 File Offset: 0x00022C04
		public DomainTargetEnvironment()
		{
			this.WhenChangedUTC = null;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00024A28 File Offset: 0x00022C28
		public DomainTargetEnvironment(SerializationInfo info, StreamingContext ctxt)
		{
			this.DomainTargetEnvironmentId = (Guid)info.GetValue(DomainSchema.DomainTargetEnvironmentId.Name, typeof(Guid));
			this.DomainKey = (string)info.GetValue(DomainSchema.DomainKey.Name, typeof(string));
			this.DomainName = (string)info.GetValue(DomainSchema.DomainName.Name, typeof(string));
			this.TenantId = (Guid)info.GetValue(DomainSchema.TenantId.Name, typeof(Guid));
			this.WhenChangedUTC = (DateTime?)info.GetValue(DomainSchema.WhenChangedProp.Name, typeof(DateTime?));
			Dictionary<int, Dictionary<int, string>> dictionary = (Dictionary<int, Dictionary<int, string>>)info.GetValue(DomainSchema.PropertiesAsId.Name, typeof(Dictionary<int, Dictionary<int, string>>));
			this.Properties = new Dictionary<int, Dictionary<int, string>>(dictionary);
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x00024B20 File Offset: 0x00022D20
		public override ObjectId Identity
		{
			get
			{
				return DomainSchema.GetObjectId(this.DomainTargetEnvironmentId);
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x00024B2D File Offset: 0x00022D2D
		public override ObjectState ObjectState
		{
			get
			{
				return (ObjectState)this[DomainSchema.ObjectStateProp];
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x00024B3F File Offset: 0x00022D3F
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x00024B51 File Offset: 0x00022D51
		public Guid DomainTargetEnvironmentId
		{
			get
			{
				return (Guid)this[DomainSchema.DomainTargetEnvironmentId];
			}
			private set
			{
				this[DomainSchema.DomainTargetEnvironmentId] = value;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x00024B64 File Offset: 0x00022D64
		// (set) Token: 0x06000B61 RID: 2913 RVA: 0x00024B76 File Offset: 0x00022D76
		public string DomainKey
		{
			get
			{
				return this[DomainSchema.DomainKey] as string;
			}
			set
			{
				this[DomainSchema.DomainKey] = DomainSchema.GetNullIfStringEmpty(value);
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x00024B89 File Offset: 0x00022D89
		// (set) Token: 0x06000B63 RID: 2915 RVA: 0x00024B9B File Offset: 0x00022D9B
		public int DomainKeyFlags
		{
			get
			{
				return (int)this[DomainSchema.DomainKeyFlags];
			}
			set
			{
				this[DomainSchema.DomainKeyFlags] = value;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x00024BAE File Offset: 0x00022DAE
		// (set) Token: 0x06000B65 RID: 2917 RVA: 0x00024BC0 File Offset: 0x00022DC0
		public string DomainName
		{
			get
			{
				return this[DomainSchema.DomainName] as string;
			}
			set
			{
				this[DomainSchema.DomainName] = DomainSchema.GetNullIfStringEmpty(value);
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x00024BD3 File Offset: 0x00022DD3
		// (set) Token: 0x06000B67 RID: 2919 RVA: 0x00024BE5 File Offset: 0x00022DE5
		public Guid TenantId
		{
			get
			{
				return DomainSchema.GetGuidEmptyIfNull(this[DomainSchema.TenantId]);
			}
			set
			{
				this[DomainSchema.TenantId] = DomainSchema.GetNullIfGuidEmpty(value);
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x00024BF8 File Offset: 0x00022DF8
		// (set) Token: 0x06000B69 RID: 2921 RVA: 0x00024C0A File Offset: 0x00022E0A
		public DateTime? WhenChangedUTC
		{
			get
			{
				return (DateTime?)this[DomainSchema.WhenChangedProp];
			}
			set
			{
				this[DomainSchema.WhenChangedProp] = value;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x00024C1D File Offset: 0x00022E1D
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x00024C2F File Offset: 0x00022E2F
		public Dictionary<int, Dictionary<int, string>> Properties
		{
			get
			{
				return this[DomainSchema.PropertiesAsId] as Dictionary<int, Dictionary<int, string>>;
			}
			set
			{
				this[DomainSchema.PropertiesAsId] = value;
			}
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00024C40 File Offset: 0x00022E40
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(DomainSchema.DomainTargetEnvironmentId.Name, this.DomainTargetEnvironmentId);
			info.AddValue(DomainSchema.DomainKey.Name, this.DomainKey);
			info.AddValue(DomainSchema.DomainName.Name, this.DomainName);
			info.AddValue(DomainSchema.TenantId.Name, this.TenantId);
			info.AddValue(DomainSchema.WhenChangedProp.Name, this.WhenChangedUTC);
			info.AddValue(DomainSchema.PropertiesAsId.Name, this.Properties);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00024CE0 File Offset: 0x00022EE0
		public override IEnumerable<PropertyDefinition> GetPropertyDefinitions(bool isChangedOnly)
		{
			if (isChangedOnly)
			{
				return base.GetPropertyDefinitions(isChangedOnly);
			}
			return DomainTargetEnvironment.propertydefinitions;
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00024CF2 File Offset: 0x00022EF2
		public override string ToString()
		{
			return this.ConvertToString();
		}

		// Token: 0x040005F1 RID: 1521
		private static readonly PropertyDefinition[] propertydefinitions = new PropertyDefinition[]
		{
			DomainSchema.DomainTargetEnvironmentId,
			DomainSchema.DomainKey,
			DomainSchema.DomainName,
			DomainSchema.TenantId,
			DomainSchema.PropertiesAsId,
			DomainSchema.ObjectStateProp,
			DomainSchema.DomainKeyFlags,
			DomainSchema.UpdateDomainKey,
			DomainSchema.IsTracerTokenProp,
			DomainSchema.WhenChangedProp
		};
	}
}
