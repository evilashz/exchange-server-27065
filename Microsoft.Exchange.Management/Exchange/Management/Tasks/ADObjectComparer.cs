using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000448 RID: 1096
	internal class ADObjectComparer<T> : IEqualityComparer<T> where T : ADObject
	{
		// Token: 0x060026BC RID: 9916 RVA: 0x00099640 File Offset: 0x00097840
		bool IEqualityComparer<!0>.Equals(T x, T y)
		{
			return x.Id.Equals(y.Id);
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x00099661 File Offset: 0x00097861
		int IEqualityComparer<!0>.GetHashCode(T obj)
		{
			return obj.Id.GetHashCode();
		}
	}
}
