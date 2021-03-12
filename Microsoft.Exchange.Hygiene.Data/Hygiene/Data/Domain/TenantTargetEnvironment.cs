using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Domain
{
	// Token: 0x0200012E RID: 302
	[Serializable]
	internal class TenantTargetEnvironment : ConfigurablePropertyBag, ISerializable
	{
		// Token: 0x06000BB5 RID: 2997 RVA: 0x000255E0 File Offset: 0x000237E0
		public TenantTargetEnvironment()
		{
			this.WhenChangedUTC = null;
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00025604 File Offset: 0x00023804
		public TenantTargetEnvironment(SerializationInfo info, StreamingContext ctxt)
		{
			this.TenantTargetEnvironmentId = (Guid)info.GetValue(DomainSchema.TenantTargetEnvironmentId.Name, typeof(Guid));
			this.TenantId = (Guid)info.GetValue(DomainSchema.TenantId.Name, typeof(Guid));
			this.WhenChangedUTC = (DateTime?)info.GetValue(DomainSchema.WhenChangedProp.Name, typeof(DateTime?));
			Dictionary<int, Dictionary<int, string>> dictionary = (Dictionary<int, Dictionary<int, string>>)info.GetValue(DomainSchema.PropertiesAsId.Name, typeof(Dictionary<int, Dictionary<int, string>>));
			this.Properties = new Dictionary<int, Dictionary<int, string>>(dictionary);
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x000256B2 File Offset: 0x000238B2
		public override ObjectId Identity
		{
			get
			{
				return DomainSchema.GetObjectId(this.TenantTargetEnvironmentId);
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x000256BF File Offset: 0x000238BF
		public override ObjectState ObjectState
		{
			get
			{
				return (ObjectState)this[DomainSchema.ObjectStateProp];
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x000256D1 File Offset: 0x000238D1
		// (set) Token: 0x06000BBA RID: 3002 RVA: 0x000256E3 File Offset: 0x000238E3
		public Guid TenantTargetEnvironmentId
		{
			get
			{
				return (Guid)this[DomainSchema.TenantTargetEnvironmentId];
			}
			private set
			{
				this[DomainSchema.TenantTargetEnvironmentId] = value;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x000256F6 File Offset: 0x000238F6
		// (set) Token: 0x06000BBC RID: 3004 RVA: 0x00025708 File Offset: 0x00023908
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

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x0002571B File Offset: 0x0002391B
		// (set) Token: 0x06000BBE RID: 3006 RVA: 0x0002572D File Offset: 0x0002392D
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

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x00025740 File Offset: 0x00023940
		// (set) Token: 0x06000BC0 RID: 3008 RVA: 0x00025752 File Offset: 0x00023952
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

		// Token: 0x06000BC1 RID: 3009 RVA: 0x00025760 File Offset: 0x00023960
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(DomainSchema.TenantTargetEnvironmentId.Name, this.TenantTargetEnvironmentId);
			info.AddValue(DomainSchema.TenantId.Name, this.TenantId);
			info.AddValue(DomainSchema.WhenChangedProp.Name, this.WhenChangedUTC);
			info.AddValue(DomainSchema.PropertiesAsId.Name, this.Properties);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x000257D4 File Offset: 0x000239D4
		public override IEnumerable<PropertyDefinition> GetPropertyDefinitions(bool isChangedOnly)
		{
			if (isChangedOnly)
			{
				return base.GetPropertyDefinitions(isChangedOnly);
			}
			return TenantTargetEnvironment.propertydefinitions;
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x000257E6 File Offset: 0x000239E6
		public override string ToString()
		{
			return this.ConvertToString();
		}

		// Token: 0x040005F6 RID: 1526
		private static readonly PropertyDefinition[] propertydefinitions = new PropertyDefinition[]
		{
			DomainSchema.TenantTargetEnvironmentId,
			DomainSchema.TenantId,
			DomainSchema.PropertiesAsId,
			DomainSchema.ObjectStateProp,
			DomainSchema.IsTracerTokenProp,
			DomainSchema.WhenChangedProp
		};
	}
}
