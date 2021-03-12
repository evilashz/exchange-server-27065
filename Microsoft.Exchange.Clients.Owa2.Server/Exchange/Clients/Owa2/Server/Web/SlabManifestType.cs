using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x0200049F RID: 1183
	public class SlabManifestType
	{
		// Token: 0x06002863 RID: 10339 RVA: 0x00095823 File Offset: 0x00093A23
		public SlabManifestType(string manifestName)
		{
			this.manifestName = manifestName;
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06002864 RID: 10340 RVA: 0x00095832 File Offset: 0x00093A32
		public string Name
		{
			get
			{
				return this.manifestName;
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06002865 RID: 10341 RVA: 0x0009583A File Offset: 0x00093A3A
		public static SlabManifestType Standard
		{
			get
			{
				if (SlabManifestType.standard == null)
				{
					SlabManifestType.standard = new SlabManifestType("slabmanifest.standard");
				}
				return SlabManifestType.standard;
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06002866 RID: 10342 RVA: 0x00095857 File Offset: 0x00093A57
		public static SlabManifestType Pal
		{
			get
			{
				if (SlabManifestType.pal == null)
				{
					SlabManifestType.pal = new SlabManifestType("slabmanifest.pal");
				}
				return SlabManifestType.pal;
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06002867 RID: 10343 RVA: 0x00095874 File Offset: 0x00093A74
		public static SlabManifestType Anonymous
		{
			get
			{
				if (SlabManifestType.anonymous == null)
				{
					SlabManifestType.anonymous = new SlabManifestType("slabmanifest.anonymous");
				}
				return SlabManifestType.anonymous;
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06002868 RID: 10344 RVA: 0x00095891 File Offset: 0x00093A91
		public static SlabManifestType PreBoot
		{
			get
			{
				if (SlabManifestType.preBoot == null)
				{
					SlabManifestType.preBoot = new SlabManifestType("slabmanifest.preboot");
				}
				return SlabManifestType.preBoot;
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06002869 RID: 10345 RVA: 0x000958AE File Offset: 0x00093AAE
		public static SlabManifestType GenericMail
		{
			get
			{
				if (SlabManifestType.genericMail == null)
				{
					SlabManifestType.genericMail = new SlabManifestType("slabmanifest.genericmail");
				}
				return SlabManifestType.genericMail;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x0600286A RID: 10346 RVA: 0x000958CB File Offset: 0x00093ACB
		public static SlabManifestType SharedHoverCard
		{
			get
			{
				if (SlabManifestType.sharedHoverCard == null)
				{
					SlabManifestType.sharedHoverCard = new SlabManifestType("slabmanifest.standard");
				}
				return SlabManifestType.sharedHoverCard;
			}
		}

		// Token: 0x04001760 RID: 5984
		private static SlabManifestType standard;

		// Token: 0x04001761 RID: 5985
		private static SlabManifestType pal;

		// Token: 0x04001762 RID: 5986
		private static SlabManifestType anonymous;

		// Token: 0x04001763 RID: 5987
		private static SlabManifestType preBoot;

		// Token: 0x04001764 RID: 5988
		private static SlabManifestType genericMail;

		// Token: 0x04001765 RID: 5989
		private static SlabManifestType sharedHoverCard;

		// Token: 0x04001766 RID: 5990
		private readonly string manifestName;
	}
}
