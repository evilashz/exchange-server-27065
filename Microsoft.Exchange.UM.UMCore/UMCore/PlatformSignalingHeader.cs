using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002FA RID: 762
	internal abstract class PlatformSignalingHeader
	{
		// Token: 0x06001747 RID: 5959 RVA: 0x0006379E File Offset: 0x0006199E
		public PlatformSignalingHeader(string name, string value)
		{
			ValidateArgument.NotNullOrEmpty(name, "name");
			ValidateArgument.NotNullOrEmpty(value, "value");
			this.Name = name;
			this.Value = value.Trim();
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x000637CF File Offset: 0x000619CF
		// (set) Token: 0x06001749 RID: 5961 RVA: 0x000637D7 File Offset: 0x000619D7
		public string Name { get; private set; }

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x0600174A RID: 5962 RVA: 0x000637E0 File Offset: 0x000619E0
		// (set) Token: 0x0600174B RID: 5963 RVA: 0x000637E8 File Offset: 0x000619E8
		public string Value { get; private set; }

		// Token: 0x0600174C RID: 5964
		public abstract PlatformSipUri ParseUri();

		// Token: 0x0600174D RID: 5965 RVA: 0x000637F1 File Offset: 0x000619F1
		public override string ToString()
		{
			return string.Format("{0}:{1}", this.Name, this.Value);
		}
	}
}
