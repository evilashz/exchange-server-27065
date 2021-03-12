using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x020000CA RID: 202
	public enum ExtendedTypeCode : byte
	{
		// Token: 0x04000775 RID: 1909
		Invalid,
		// Token: 0x04000776 RID: 1910
		Boolean,
		// Token: 0x04000777 RID: 1911
		Int16,
		// Token: 0x04000778 RID: 1912
		Int32,
		// Token: 0x04000779 RID: 1913
		Int64,
		// Token: 0x0400077A RID: 1914
		Single,
		// Token: 0x0400077B RID: 1915
		Double,
		// Token: 0x0400077C RID: 1916
		DateTime,
		// Token: 0x0400077D RID: 1917
		Guid,
		// Token: 0x0400077E RID: 1918
		String,
		// Token: 0x0400077F RID: 1919
		Binary,
		// Token: 0x04000780 RID: 1920
		MVFlag = 16,
		// Token: 0x04000781 RID: 1921
		MVInt16 = 18,
		// Token: 0x04000782 RID: 1922
		MVInt32,
		// Token: 0x04000783 RID: 1923
		MVInt64,
		// Token: 0x04000784 RID: 1924
		MVSingle,
		// Token: 0x04000785 RID: 1925
		MVDouble,
		// Token: 0x04000786 RID: 1926
		MVDateTime,
		// Token: 0x04000787 RID: 1927
		MVGuid,
		// Token: 0x04000788 RID: 1928
		MVString,
		// Token: 0x04000789 RID: 1929
		MVBinary
	}
}
