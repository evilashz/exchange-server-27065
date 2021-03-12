using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020001AC RID: 428
	[Serializable]
	internal class TopologyServiceServerSettings : ADServerSettings, ICloneable, IEquatable<TopologyServiceServerSettings>
	{
		// Token: 0x060011F5 RID: 4597 RVA: 0x00057745 File Offset: 0x00055945
		private TopologyServiceServerSettings()
		{
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0005774D File Offset: 0x0005594D
		internal static TopologyServiceServerSettings CreateTopologyServiceServerSettings()
		{
			return new TopologyServiceServerSettings();
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00057754 File Offset: 0x00055954
		protected override bool EnforceIsUpdatableByADSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x00057757 File Offset: 0x00055957
		// (set) Token: 0x060011F9 RID: 4601 RVA: 0x0005775A File Offset: 0x0005595A
		internal override ADObjectId RecipientViewRoot
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotSupportedException("RecipientViewRoot setter is not supported on TopologyServiceServerSettings");
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x00057766 File Offset: 0x00055966
		// (set) Token: 0x060011FB RID: 4603 RVA: 0x00057769 File Offset: 0x00055969
		internal override bool ViewEntireForest
		{
			get
			{
				return true;
			}
			set
			{
				throw new NotSupportedException("ViewEntireForest setter is not supported on TopologyServiceServerSettings");
			}
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00057778 File Offset: 0x00055978
		public override object Clone()
		{
			TopologyServiceServerSettings topologyServiceServerSettings = new TopologyServiceServerSettings();
			this.CopyTo(topologyServiceServerSettings);
			return topologyServiceServerSettings;
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00057793 File Offset: 0x00055993
		public bool Equals(TopologyServiceServerSettings other)
		{
			return base.Equals(other);
		}
	}
}
