using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000002 RID: 2
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AdrEntry
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		internal unsafe AdrEntry(_AdrEntry* padrEntry)
		{
			SPropValue* pspva = padrEntry->pspva;
			this.propValues = new PropValue[padrEntry->cValues];
			uint num = 0U;
			while ((ulong)num < (ulong)((long)padrEntry->cValues))
			{
				this.propValues[(int)((UIntPtr)num)] = new PropValue(pspva + (ulong)num * (ulong)((long)sizeof(SPropValue)) / (ulong)sizeof(SPropValue));
				num += 1U;
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002133 File Offset: 0x00000333
		// (set) Token: 0x06000003 RID: 3 RVA: 0x0000213B File Offset: 0x0000033B
		public PropValue[] Values
		{
			get
			{
				return this.propValues;
			}
			set
			{
				this.propValues = value;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002144 File Offset: 0x00000344
		public AdrEntry(params PropValue[] propValues)
		{
			this.propValues = propValues;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002154 File Offset: 0x00000354
		public bool IsEqualTo(AdrEntry aeOther)
		{
			if (this.propValues.Length != aeOther.propValues.Length)
			{
				return false;
			}
			Dictionary<PropTag, PropValue> dictionary = new Dictionary<PropTag, PropValue>();
			foreach (PropValue value in aeOther.propValues)
			{
				dictionary.Add(value.PropTag, value);
			}
			foreach (PropValue propValue in this.propValues)
			{
				PropValue propOther = dictionary[propValue.PropTag];
				if (!propValue.IsEqualTo(propOther))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002200 File Offset: 0x00000400
		internal int GetBytesToMarshal()
		{
			int num = _AdrEntry.SizeOf + 7 & -8;
			for (int i = 0; i < this.propValues.Length; i++)
			{
				num += this.propValues[i].GetBytesToMarshal();
			}
			return num;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002240 File Offset: 0x00000440
		internal unsafe void MarshalToNative(_AdrEntry* padrEntry, ref SPropValue* psprop, ref byte* pbExtra)
		{
			padrEntry->ulReserved1 = 0;
			padrEntry->cValues = this.propValues.Length;
			padrEntry->pspva = psprop;
			for (int i = 0; i < this.propValues.Length; i++)
			{
				this.propValues[i].MarshalToNative(psprop, ref pbExtra);
				psprop += (IntPtr)sizeof(SPropValue);
			}
		}

		// Token: 0x04000001 RID: 1
		internal PropValue[] propValues;
	}
}
