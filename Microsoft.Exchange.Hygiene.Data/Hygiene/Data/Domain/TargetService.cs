using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Domain
{
	// Token: 0x0200012C RID: 300
	[Serializable]
	internal class TargetService : ConfigurablePropertyBag, ISerializable
	{
		// Token: 0x06000B97 RID: 2967 RVA: 0x00025204 File Offset: 0x00023404
		public TargetService()
		{
			this.WhenChangedUTC = null;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00025228 File Offset: 0x00023428
		public TargetService(SerializationInfo info, StreamingContext ctxt)
		{
			this.TargetServiceId = (Guid)info.GetValue(DomainSchema.TargetServiceId.Name, typeof(Guid));
			this.DomainKey = (string)info.GetValue(DomainSchema.DomainKey.Name, typeof(string));
			this.DomainName = (string)info.GetValue(DomainSchema.DomainName.Name, typeof(string));
			this.TenantId = (Guid)info.GetValue(DomainSchema.TenantId.Name, typeof(Guid));
			this.WhenChangedUTC = (DateTime?)info.GetValue(DomainSchema.WhenChangedProp.Name, typeof(DateTime?));
			Dictionary<int, Dictionary<int, string>> dictionary = (Dictionary<int, Dictionary<int, string>>)info.GetValue(DomainSchema.PropertiesAsId.Name, typeof(Dictionary<int, Dictionary<int, string>>));
			this.Properties = new Dictionary<int, Dictionary<int, string>>(dictionary);
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x00025320 File Offset: 0x00023520
		public override ObjectId Identity
		{
			get
			{
				return DomainSchema.GetObjectId(this.TargetServiceId);
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0002532D File Offset: 0x0002352D
		public override ObjectState ObjectState
		{
			get
			{
				return (ObjectState)this[DomainSchema.ObjectStateProp];
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0002533F File Offset: 0x0002353F
		// (set) Token: 0x06000B9C RID: 2972 RVA: 0x00025351 File Offset: 0x00023551
		public Guid TargetServiceId
		{
			get
			{
				return (Guid)this[DomainSchema.TargetServiceId];
			}
			private set
			{
				this[DomainSchema.TargetServiceId] = value;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x00025364 File Offset: 0x00023564
		// (set) Token: 0x06000B9E RID: 2974 RVA: 0x00025376 File Offset: 0x00023576
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

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x00025389 File Offset: 0x00023589
		// (set) Token: 0x06000BA0 RID: 2976 RVA: 0x0002539B File Offset: 0x0002359B
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

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x000253AE File Offset: 0x000235AE
		// (set) Token: 0x06000BA2 RID: 2978 RVA: 0x000253C0 File Offset: 0x000235C0
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

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x000253D3 File Offset: 0x000235D3
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x000253E5 File Offset: 0x000235E5
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

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x000253F8 File Offset: 0x000235F8
		// (set) Token: 0x06000BA6 RID: 2982 RVA: 0x0002540A File Offset: 0x0002360A
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

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00025418 File Offset: 0x00023618
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(DomainSchema.TargetServiceId.Name, this.TargetServiceId);
			info.AddValue(DomainSchema.DomainKey.Name, this.DomainKey);
			info.AddValue(DomainSchema.DomainName.Name, this.DomainName);
			info.AddValue(DomainSchema.TenantId.Name, this.TenantId);
			info.AddValue(DomainSchema.WhenChangedProp.Name, this.WhenChangedUTC);
			info.AddValue(DomainSchema.PropertiesAsId.Name, this.Properties);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x000254B8 File Offset: 0x000236B8
		public override IEnumerable<PropertyDefinition> GetPropertyDefinitions(bool isChangedOnly)
		{
			if (isChangedOnly)
			{
				return base.GetPropertyDefinitions(isChangedOnly);
			}
			return TargetService.propertydefinitions;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x000254CA File Offset: 0x000236CA
		public override string ToString()
		{
			return this.ConvertToString();
		}

		// Token: 0x040005F4 RID: 1524
		private static readonly PropertyDefinition[] propertydefinitions = new PropertyDefinition[]
		{
			DomainSchema.TargetServiceId,
			DomainSchema.DomainKey,
			DomainSchema.DomainName,
			DomainSchema.TenantId,
			DomainSchema.PropertiesAsId,
			DomainSchema.ObjectStateProp,
			DomainSchema.IsTracerTokenProp,
			DomainSchema.WhenChangedProp
		};
	}
}
