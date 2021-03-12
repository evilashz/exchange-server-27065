using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Supervision
{
	// Token: 0x0200008F RID: 143
	[Serializable]
	public sealed class SupervisionPolicy : ConfigurableObject
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x00013FD9 File Offset: 0x000121D9
		public SupervisionPolicy(string orgname) : base(new SupervisionPolicyPropertyBag())
		{
			this.Identity = new SupervisionPolicyId(orgname);
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00013FF2 File Offset: 0x000121F2
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return SupervisionPolicy.schema;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00013FF9 File Offset: 0x000121F9
		private new bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00013FFC File Offset: 0x000121FC
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x00014013 File Offset: 0x00012213
		public new ObjectId Identity
		{
			get
			{
				return (ObjectId)this.propertyBag[SupervisionPolicySchema.Identity];
			}
			internal set
			{
				this.propertyBag[SupervisionPolicySchema.Identity] = value;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00014026 File Offset: 0x00012226
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x0001403D File Offset: 0x0001223D
		public bool ClosedCampusInboundPolicyEnabled
		{
			get
			{
				return (bool)this.propertyBag[SupervisionPolicySchema.ClosedCampusInboundPolicyEnabled];
			}
			internal set
			{
				this.propertyBag[SupervisionPolicySchema.ClosedCampusInboundPolicyEnabled] = value;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00014055 File Offset: 0x00012255
		// (set) Token: 0x0600051C RID: 1308 RVA: 0x0001406C File Offset: 0x0001226C
		public MultiValuedProperty<SmtpDomain> ClosedCampusInboundPolicyDomainExceptions
		{
			get
			{
				return (MultiValuedProperty<SmtpDomain>)this.propertyBag[SupervisionPolicySchema.ClosedCampusInboundDomainExceptions];
			}
			internal set
			{
				this.propertyBag[SupervisionPolicySchema.ClosedCampusInboundDomainExceptions] = value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x0001407F File Offset: 0x0001227F
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x00014096 File Offset: 0x00012296
		public MultiValuedProperty<SmtpAddress> ClosedCampusInboundPolicyGroupExceptions
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)this.propertyBag[SupervisionPolicySchema.ClosedCampusInboundGroupExceptions];
			}
			internal set
			{
				this.propertyBag[SupervisionPolicySchema.ClosedCampusInboundGroupExceptions] = value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x000140A9 File Offset: 0x000122A9
		// (set) Token: 0x06000520 RID: 1312 RVA: 0x000140C0 File Offset: 0x000122C0
		public bool ClosedCampusOutboundPolicyEnabled
		{
			get
			{
				return (bool)this.propertyBag[SupervisionPolicySchema.ClosedCampusOutboundPolicyEnabled];
			}
			internal set
			{
				this.propertyBag[SupervisionPolicySchema.ClosedCampusOutboundPolicyEnabled] = value;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x000140D8 File Offset: 0x000122D8
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x000140EF File Offset: 0x000122EF
		public MultiValuedProperty<SmtpDomain> ClosedCampusOutboundPolicyDomainExceptions
		{
			get
			{
				return (MultiValuedProperty<SmtpDomain>)this.propertyBag[SupervisionPolicySchema.ClosedCampusOutboundDomainExceptions];
			}
			internal set
			{
				this.propertyBag[SupervisionPolicySchema.ClosedCampusOutboundDomainExceptions] = value;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00014102 File Offset: 0x00012302
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x00014119 File Offset: 0x00012319
		public MultiValuedProperty<SmtpAddress> ClosedCampusOutboundPolicyGroupExceptions
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)this.propertyBag[SupervisionPolicySchema.ClosedCampusOutboundGroupExceptions];
			}
			internal set
			{
				this.propertyBag[SupervisionPolicySchema.ClosedCampusOutboundGroupExceptions] = value;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0001412C File Offset: 0x0001232C
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x00014143 File Offset: 0x00012343
		public bool BadWordsPolicyEnabled
		{
			get
			{
				return (bool)this.propertyBag[SupervisionPolicySchema.BadWordsPolicyEnabled];
			}
			internal set
			{
				this.propertyBag[SupervisionPolicySchema.BadWordsPolicyEnabled] = value;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0001415B File Offset: 0x0001235B
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x00014172 File Offset: 0x00012372
		public string BadWordsList
		{
			get
			{
				return (string)this.propertyBag[SupervisionPolicySchema.BadWordsList];
			}
			internal set
			{
				this.propertyBag[SupervisionPolicySchema.BadWordsList] = value;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00014185 File Offset: 0x00012385
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x0001419C File Offset: 0x0001239C
		public bool AntiBullyingPolicyEnabled
		{
			get
			{
				return (bool)this.propertyBag[SupervisionPolicySchema.AntiBullyingPolicyEnabled];
			}
			internal set
			{
				this.propertyBag[SupervisionPolicySchema.AntiBullyingPolicyEnabled] = value;
			}
		}

		// Token: 0x040001B2 RID: 434
		internal static readonly string BadWordsSeparator = ",";

		// Token: 0x040001B3 RID: 435
		private static SupervisionPolicySchema schema = new SupervisionPolicySchema();
	}
}
