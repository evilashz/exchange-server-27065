using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x02000305 RID: 773
	[ComVisible(true)]
	[Serializable]
	public enum WindowsBuiltInRole
	{
		// Token: 0x04000FC8 RID: 4040
		Administrator = 544,
		// Token: 0x04000FC9 RID: 4041
		User,
		// Token: 0x04000FCA RID: 4042
		Guest,
		// Token: 0x04000FCB RID: 4043
		PowerUser,
		// Token: 0x04000FCC RID: 4044
		AccountOperator,
		// Token: 0x04000FCD RID: 4045
		SystemOperator,
		// Token: 0x04000FCE RID: 4046
		PrintOperator,
		// Token: 0x04000FCF RID: 4047
		BackupOperator,
		// Token: 0x04000FD0 RID: 4048
		Replicator
	}
}
