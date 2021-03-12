using System;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000BC RID: 188
	[Flags]
	internal enum ServiceType
	{
		// Token: 0x040001CE RID: 462
		FileSystemDriver = 2,
		// Token: 0x040001CF RID: 463
		KernelDriver = 1,
		// Token: 0x040001D0 RID: 464
		Win32OwnProcess = 16,
		// Token: 0x040001D1 RID: 465
		Win32ShareProcess = 32
	}
}
