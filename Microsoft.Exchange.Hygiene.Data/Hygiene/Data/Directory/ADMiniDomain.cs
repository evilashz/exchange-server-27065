using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000BF RID: 191
	[Serializable]
	internal class ADMiniDomain : ConfigurablePropertyBag, ISerializable
	{
		// Token: 0x06000634 RID: 1588 RVA: 0x000144E2 File Offset: 0x000126E2
		public ADMiniDomain()
		{
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x000144EC File Offset: 0x000126EC
		protected ADMiniDomain(SerializationInfo info, StreamingContext ctxt)
		{
			this.DomainId = (ADObjectId)info.GetValue(ADMiniDomainSchema.DomainIdProp.Name, typeof(ADObjectId));
			this.TenantId = (ADObjectId)info.GetValue(ADMiniDomainSchema.TenantIdProp.Name, typeof(ADObjectId));
			this.ConfigurationId = (ADObjectId)info.GetValue(ADMiniDomainSchema.ConfigurationIdProp.Name, typeof(ADObjectId));
			this.ParentDomainId = (ADObjectId)info.GetValue(ADMiniDomainSchema.ParentDomainIdProp.Name, typeof(ADObjectId));
			this.DomainName = info.GetString(ADMiniDomainSchema.DomainNameProp.Name);
			this.EdgeBlockMode = (EdgeBlockMode)info.GetValue(ADMiniDomainSchema.EdgeBlockModeProp.Name, typeof(EdgeBlockMode));
			this.CatchAll = info.GetBoolean(ADMiniDomainSchema.IsCatchAllProp.Name);
			this.IsInitialDomain = info.GetBoolean(ADMiniDomainSchema.IsInitialDomainProp.Name);
			this.IsDefaultDomain = info.GetBoolean(ADMiniDomainSchema.IsDefaultDomainProp.Name);
			this.MailServer = info.GetString(ADMiniDomainSchema.MailServerProp.Name);
			this.LiveType = info.GetString(ADMiniDomainSchema.LiveTypeProp.Name);
			this.LiveNetId = info.GetString(ADMiniDomainSchema.LiveNetIdProp.Name);
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00014652 File Offset: 0x00012852
		public override ObjectId Identity
		{
			get
			{
				return this.DomainId;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0001465A File Offset: 0x0001285A
		public override ObjectState ObjectState
		{
			get
			{
				return (ObjectState)this[ADMiniDomainSchema.ObjectStateProp];
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0001466C File Offset: 0x0001286C
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x0001467E File Offset: 0x0001287E
		public ADObjectId DomainId
		{
			get
			{
				return this[ADMiniDomainSchema.DomainIdProp] as ADObjectId;
			}
			set
			{
				this[ADMiniDomainSchema.DomainIdProp] = value;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0001468C File Offset: 0x0001288C
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x0001469E File Offset: 0x0001289E
		public ADObjectId TenantId
		{
			get
			{
				return this[ADMiniDomainSchema.TenantIdProp] as ADObjectId;
			}
			set
			{
				this[ADMiniDomainSchema.TenantIdProp] = value;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x000146AC File Offset: 0x000128AC
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x000146BE File Offset: 0x000128BE
		public ADObjectId ConfigurationId
		{
			get
			{
				return this[ADMiniDomainSchema.ConfigurationIdProp] as ADObjectId;
			}
			set
			{
				this[ADMiniDomainSchema.ConfigurationIdProp] = value;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x000146CC File Offset: 0x000128CC
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x000146DE File Offset: 0x000128DE
		public ADObjectId ParentDomainId
		{
			get
			{
				return this[ADMiniDomainSchema.ParentDomainIdProp] as ADObjectId;
			}
			set
			{
				this[ADMiniDomainSchema.ParentDomainIdProp] = value;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x000146EC File Offset: 0x000128EC
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x000146FE File Offset: 0x000128FE
		public string DomainName
		{
			get
			{
				return this[ADMiniDomainSchema.DomainNameProp] as string;
			}
			set
			{
				this[ADMiniDomainSchema.DomainNameProp] = value;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0001470C File Offset: 0x0001290C
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x0001471E File Offset: 0x0001291E
		public int DomainFlags
		{
			get
			{
				return (int)this[ADMiniDomainSchema.DomainFlagsProperty];
			}
			set
			{
				this[ADMiniDomainSchema.DomainFlagsProperty] = value;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00014731 File Offset: 0x00012931
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x0001474D File Offset: 0x0001294D
		public EdgeBlockMode EdgeBlockMode
		{
			get
			{
				return (EdgeBlockMode)(this[ADMiniDomainSchema.EdgeBlockModeProp] ?? EdgeBlockMode.None);
			}
			set
			{
				this[ADMiniDomainSchema.EdgeBlockModeProp] = value;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00014760 File Offset: 0x00012960
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x00014772 File Offset: 0x00012972
		public ADObjectId HygieneConfigurationLink
		{
			get
			{
				return this[ADMiniDomainSchema.HygieneConfigurationLink] as ADObjectId;
			}
			set
			{
				this[ADMiniDomainSchema.HygieneConfigurationLink] = value;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x00014780 File Offset: 0x00012980
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x0001479C File Offset: 0x0001299C
		public bool CatchAll
		{
			get
			{
				return (bool)(this[ADMiniDomainSchema.IsCatchAllProp] ?? false);
			}
			set
			{
				this[ADMiniDomainSchema.IsCatchAllProp] = value;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x000147AF File Offset: 0x000129AF
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x000147CB File Offset: 0x000129CB
		public bool IsDefaultDomain
		{
			get
			{
				return (bool)(this[ADMiniDomainSchema.IsDefaultDomainProp] ?? false);
			}
			set
			{
				this[ADMiniDomainSchema.IsDefaultDomainProp] = value;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x000147DE File Offset: 0x000129DE
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x000147F0 File Offset: 0x000129F0
		public string MailServer
		{
			get
			{
				return this[ADMiniDomainSchema.MailServerProp] as string;
			}
			set
			{
				this[ADMiniDomainSchema.MailServerProp] = value;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x000147FE File Offset: 0x000129FE
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x0001481A File Offset: 0x00012A1A
		public bool IsInitialDomain
		{
			get
			{
				return (bool)(this[ADMiniDomainSchema.IsInitialDomainProp] ?? false);
			}
			set
			{
				this[ADMiniDomainSchema.IsInitialDomainProp] = value;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x0001482D File Offset: 0x00012A2D
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x0001483F File Offset: 0x00012A3F
		public string LiveType
		{
			get
			{
				return this[ADMiniDomainSchema.LiveTypeProp] as string;
			}
			set
			{
				this[ADMiniDomainSchema.LiveTypeProp] = value;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0001484D File Offset: 0x00012A4D
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x0001485F File Offset: 0x00012A5F
		public string LiveNetId
		{
			get
			{
				return this[ADMiniDomainSchema.LiveNetIdProp] as string;
			}
			set
			{
				this[ADMiniDomainSchema.LiveNetIdProp] = value;
			}
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001486D File Offset: 0x00012A6D
		public override Type GetSchemaType()
		{
			return typeof(ADMiniDomainSchema);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001487C File Offset: 0x00012A7C
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(ADMiniDomainSchema.DomainIdProp.Name, this.DomainId);
			info.AddValue(ADMiniDomainSchema.TenantIdProp.Name, this.TenantId);
			info.AddValue(ADMiniDomainSchema.ConfigurationIdProp.Name, this.ConfigurationId);
			info.AddValue(ADMiniDomainSchema.ParentDomainIdProp.Name, this.ParentDomainId);
			info.AddValue(ADMiniDomainSchema.DomainNameProp.Name, this.DomainName);
			info.AddValue(ADMiniDomainSchema.EdgeBlockModeProp.Name, this.EdgeBlockMode);
			info.AddValue(ADMiniDomainSchema.IsCatchAllProp.Name, this.CatchAll);
			info.AddValue(ADMiniDomainSchema.IsDefaultDomainProp.Name, this.IsDefaultDomain);
			info.AddValue(ADMiniDomainSchema.IsInitialDomainProp.Name, this.IsInitialDomain);
			info.AddValue(ADMiniDomainSchema.MailServerProp.Name, this.MailServer);
			info.AddValue(ADMiniDomainSchema.LiveTypeProp.Name, this.LiveType);
			info.AddValue(ADMiniDomainSchema.LiveNetIdProp.Name, this.LiveNetId);
		}
	}
}
