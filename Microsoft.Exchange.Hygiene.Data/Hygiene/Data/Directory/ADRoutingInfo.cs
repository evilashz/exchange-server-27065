using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000FD RID: 253
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class ADRoutingInfo : ADObject
	{
		// Token: 0x060009CE RID: 2510 RVA: 0x0001E206 File Offset: 0x0001C406
		public ADRoutingInfo()
		{
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0001E20E File Offset: 0x0001C40E
		internal ADRoutingInfo(IConfigurationSession session, string tenantId)
		{
			this.m_Session = session;
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0001E229 File Offset: 0x0001C429
		internal ADRoutingInfo(string tenantId)
		{
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x0001E23D File Offset: 0x0001C43D
		public override ObjectId Identity
		{
			get
			{
				return this.InfoId;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x0001E245 File Offset: 0x0001C445
		// (set) Token: 0x060009D3 RID: 2515 RVA: 0x0001E257 File Offset: 0x0001C457
		public ADObjectId InfoId
		{
			get
			{
				return (ADObjectId)this[ADRoutingInfoSchema.IdProp];
			}
			set
			{
				this[ADRoutingInfoSchema.IdProp] = value;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x0001E265 File Offset: 0x0001C465
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADRoutingInfo.schema;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x0001E26C File Offset: 0x0001C46C
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADRoutingInfo.mostDerivedClass;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x0001E273 File Offset: 0x0001C473
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0001E27A File Offset: 0x0001C47A
		internal override bool ShouldValidatePropertyLinkInSameOrganization(ADPropertyDefinition property)
		{
			return false;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0001E27D File Offset: 0x0001C47D
		protected override void ValidateWrite(List<ValidationError> errors)
		{
		}

		// Token: 0x0400052C RID: 1324
		private static readonly ADRoutingInfoSchema schema = ObjectSchema.GetInstance<ADRoutingInfoSchema>();

		// Token: 0x0400052D RID: 1325
		private static string mostDerivedClass = "ADRoutingInfo";
	}
}
