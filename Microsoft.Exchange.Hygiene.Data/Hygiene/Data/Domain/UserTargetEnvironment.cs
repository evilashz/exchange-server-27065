using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Domain
{
	// Token: 0x02000130 RID: 304
	[Serializable]
	internal class UserTargetEnvironment : ConfigurablePropertyBag, ISerializable
	{
		// Token: 0x06000BCC RID: 3020 RVA: 0x000258D4 File Offset: 0x00023AD4
		public UserTargetEnvironment()
		{
			this.WhenChangedUTC = null;
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x000258F8 File Offset: 0x00023AF8
		public UserTargetEnvironment(SerializationInfo info, StreamingContext ctxt)
		{
			this.UserTargetEnvironmentId = (Guid)info.GetValue(DomainSchema.UserTargetEnvironmentId.Name, typeof(Guid));
			this.UserKey = (string)info.GetValue(DomainSchema.UserKey.Name, typeof(string));
			this.MSAUserName = (string)info.GetValue(DomainSchema.MSAUserName.Name, typeof(string));
			this.TenantId = (Guid)info.GetValue(DomainSchema.TenantId.Name, typeof(Guid));
			this.WhenChangedUTC = (DateTime?)info.GetValue(DomainSchema.WhenChangedProp.Name, typeof(DateTime?));
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x000259C4 File Offset: 0x00023BC4
		public override ObjectId Identity
		{
			get
			{
				return DomainSchema.GetObjectId(this.UserTargetEnvironmentId);
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x000259D1 File Offset: 0x00023BD1
		public override ObjectState ObjectState
		{
			get
			{
				return (ObjectState)this[DomainSchema.ObjectStateProp];
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x000259E3 File Offset: 0x00023BE3
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x000259F5 File Offset: 0x00023BF5
		public Guid UserTargetEnvironmentId
		{
			get
			{
				return (Guid)this[DomainSchema.UserTargetEnvironmentId];
			}
			private set
			{
				this[DomainSchema.UserTargetEnvironmentId] = value;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x00025A08 File Offset: 0x00023C08
		// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x00025A1A File Offset: 0x00023C1A
		public string UserKey
		{
			get
			{
				return this[DomainSchema.UserKey] as string;
			}
			set
			{
				this[DomainSchema.UserKey] = DomainSchema.GetNullIfStringEmpty(value);
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x00025A2D File Offset: 0x00023C2D
		// (set) Token: 0x06000BD5 RID: 3029 RVA: 0x00025A3F File Offset: 0x00023C3F
		public string MSAUserName
		{
			get
			{
				return this[DomainSchema.MSAUserName] as string;
			}
			set
			{
				this[DomainSchema.MSAUserName] = DomainSchema.GetNullIfStringEmpty(value);
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x00025A52 File Offset: 0x00023C52
		// (set) Token: 0x06000BD7 RID: 3031 RVA: 0x00025A64 File Offset: 0x00023C64
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

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x00025A77 File Offset: 0x00023C77
		// (set) Token: 0x06000BD9 RID: 3033 RVA: 0x00025A89 File Offset: 0x00023C89
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

		// Token: 0x06000BDA RID: 3034 RVA: 0x00025A9C File Offset: 0x00023C9C
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(DomainSchema.UserTargetEnvironmentId.Name, this.UserTargetEnvironmentId);
			info.AddValue(DomainSchema.UserKey.Name, this.UserKey);
			info.AddValue(DomainSchema.MSAUserName.Name, this.MSAUserName);
			info.AddValue(DomainSchema.TenantId.Name, this.TenantId);
			info.AddValue(DomainSchema.WhenChangedProp.Name, this.WhenChangedUTC);
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00025B26 File Offset: 0x00023D26
		public override IEnumerable<PropertyDefinition> GetPropertyDefinitions(bool isChangedOnly)
		{
			if (isChangedOnly)
			{
				return base.GetPropertyDefinitions(isChangedOnly);
			}
			return UserTargetEnvironment.propertydefinitions;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00025B38 File Offset: 0x00023D38
		public override string ToString()
		{
			return this.ConvertToString();
		}

		// Token: 0x040005F9 RID: 1529
		private static readonly PropertyDefinition[] propertydefinitions = new PropertyDefinition[]
		{
			DomainSchema.UserTargetEnvironmentId,
			DomainSchema.UserKey,
			DomainSchema.MSAUserName,
			DomainSchema.TenantId,
			DomainSchema.ObjectStateProp,
			DomainSchema.IsTracerTokenProp,
			DomainSchema.WhenChangedProp
		};
	}
}
