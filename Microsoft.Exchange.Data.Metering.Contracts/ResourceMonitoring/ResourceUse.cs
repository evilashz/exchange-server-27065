using System;
using System.Globalization;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x02000015 RID: 21
	internal class ResourceUse
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00002506 File Offset: 0x00000706
		public ResourceUse(ResourceIdentifier resource, UseLevel currentUseLevel, UseLevel previousUseLevel)
		{
			this.Resource = resource;
			this.CurrentUseLevel = currentUseLevel;
			this.PreviousUseLevel = previousUseLevel;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002523 File Offset: 0x00000723
		// (set) Token: 0x06000075 RID: 117 RVA: 0x0000252B File Offset: 0x0000072B
		internal ResourceIdentifier Resource { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002534 File Offset: 0x00000734
		// (set) Token: 0x06000077 RID: 119 RVA: 0x0000253C File Offset: 0x0000073C
		internal UseLevel CurrentUseLevel { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002545 File Offset: 0x00000745
		// (set) Token: 0x06000079 RID: 121 RVA: 0x0000254D File Offset: 0x0000074D
		internal UseLevel PreviousUseLevel { get; private set; }

		// Token: 0x0600007A RID: 122 RVA: 0x00002558 File Offset: 0x00000758
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "[ResourceUse: Resource={0} CurrentUseLevel={1} PreviousUseLevel={2}]", new object[]
			{
				this.Resource,
				this.CurrentUseLevel,
				this.PreviousUseLevel
			});
		}
	}
}
