using System;

namespace System.Resources
{
	// Token: 0x0200036B RID: 875
	internal struct ResourceLocator
	{
		// Token: 0x06002C21 RID: 11297 RVA: 0x000A705F File Offset: 0x000A525F
		internal ResourceLocator(int dataPos, object value)
		{
			this._dataPos = dataPos;
			this._value = value;
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06002C22 RID: 11298 RVA: 0x000A706F File Offset: 0x000A526F
		internal int DataPosition
		{
			get
			{
				return this._dataPos;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06002C23 RID: 11299 RVA: 0x000A7077 File Offset: 0x000A5277
		// (set) Token: 0x06002C24 RID: 11300 RVA: 0x000A707F File Offset: 0x000A527F
		internal object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x000A7088 File Offset: 0x000A5288
		internal static bool CanCache(ResourceTypeCode value)
		{
			return value <= ResourceTypeCode.TimeSpan;
		}

		// Token: 0x0400119C RID: 4508
		internal object _value;

		// Token: 0x0400119D RID: 4509
		internal int _dataPos;
	}
}
