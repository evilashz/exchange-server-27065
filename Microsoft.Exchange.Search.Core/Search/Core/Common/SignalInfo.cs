using System;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000082 RID: 130
	internal class SignalInfo
	{
		// Token: 0x0600034A RID: 842 RVA: 0x0000AF43 File Offset: 0x00009143
		private SignalInfo(Enum enumValue, SignalPriority priority)
		{
			this.enumValue = enumValue;
			this.value = Convert.ToUInt32(enumValue);
			this.name = enumValue.ToString();
			this.priority = priority;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000AF71 File Offset: 0x00009171
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0000AF79 File Offset: 0x00009179
		internal uint Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000AF82 File Offset: 0x00009182
		internal Enum Enum
		{
			get
			{
				return this.enumValue;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000AF8A File Offset: 0x0000918A
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000AF92 File Offset: 0x00009192
		internal SignalPriority Priority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000AF9A File Offset: 0x0000919A
		public override string ToString()
		{
			return string.Format("{0} ({1})", this.Name, this.Value);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000AFB7 File Offset: 0x000091B7
		internal static SignalInfo Create(Enum enumValue, SignalPriority priority)
		{
			return new SignalInfo(enumValue, priority);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000AFC0 File Offset: 0x000091C0
		internal SignalInfo Clone()
		{
			return new SignalInfo(this.enumValue, this.priority);
		}

		// Token: 0x04000172 RID: 370
		private Enum enumValue;

		// Token: 0x04000173 RID: 371
		private uint value;

		// Token: 0x04000174 RID: 372
		private string name;

		// Token: 0x04000175 RID: 373
		private SignalPriority priority;
	}
}
