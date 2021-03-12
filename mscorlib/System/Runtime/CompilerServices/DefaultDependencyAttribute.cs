using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000897 RID: 2199
	[AttributeUsage(AttributeTargets.Assembly)]
	[Serializable]
	public sealed class DefaultDependencyAttribute : Attribute
	{
		// Token: 0x06005CA0 RID: 23712 RVA: 0x00144D6F File Offset: 0x00142F6F
		public DefaultDependencyAttribute(LoadHint loadHintArgument)
		{
			this.loadHint = loadHintArgument;
		}

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x06005CA1 RID: 23713 RVA: 0x00144D7E File Offset: 0x00142F7E
		public LoadHint LoadHint
		{
			get
			{
				return this.loadHint;
			}
		}

		// Token: 0x04002975 RID: 10613
		private LoadHint loadHint;
	}
}
