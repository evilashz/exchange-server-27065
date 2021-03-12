using System;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000084 RID: 132
	internal class StateInfo
	{
		// Token: 0x06000353 RID: 851 RVA: 0x0000AFD3 File Offset: 0x000091D3
		private StateInfo(Enum enumValue)
		{
			this.enumValue = enumValue;
			this.value = Convert.ToUInt32(enumValue);
			this.name = enumValue.ToString();
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000AFFA File Offset: 0x000091FA
		internal uint Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000B002 File Offset: 0x00009202
		internal Enum Enum
		{
			get
			{
				return this.enumValue;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000B00A File Offset: 0x0000920A
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000B012 File Offset: 0x00009212
		public override string ToString()
		{
			return string.Format("{0} ({1})", this.Name, this.Value);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000B02F File Offset: 0x0000922F
		internal static StateInfo Create(Enum enumValue)
		{
			return new StateInfo(enumValue);
		}

		// Token: 0x0400017B RID: 379
		private Enum enumValue;

		// Token: 0x0400017C RID: 380
		private uint value;

		// Token: 0x0400017D RID: 381
		private string name;
	}
}
