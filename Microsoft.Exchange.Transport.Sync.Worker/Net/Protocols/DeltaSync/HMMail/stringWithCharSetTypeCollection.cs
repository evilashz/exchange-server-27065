using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail
{
	// Token: 0x02000097 RID: 151
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class stringWithCharSetTypeCollection : ArrayList
	{
		// Token: 0x060005E3 RID: 1507 RVA: 0x0001988A File Offset: 0x00017A8A
		public stringWithCharSetType Add(stringWithCharSetType obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00019895 File Offset: 0x00017A95
		public stringWithCharSetType Add()
		{
			return this.Add(new stringWithCharSetType());
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x000198A2 File Offset: 0x00017AA2
		public void Insert(int index, stringWithCharSetType obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000198AC File Offset: 0x00017AAC
		public void Remove(stringWithCharSetType obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000214 RID: 532
		public stringWithCharSetType this[int index]
		{
			get
			{
				return (stringWithCharSetType)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
