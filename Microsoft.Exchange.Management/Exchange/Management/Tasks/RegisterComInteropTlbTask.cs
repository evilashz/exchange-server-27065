using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000327 RID: 807
	[LocDescription(Strings.IDs.RegisterComInteropTlbTask)]
	[Cmdlet("Register", "ComInteropTlb")]
	public sealed class RegisterComInteropTlbTask : ComInteropTlbTaskBase
	{
		// Token: 0x06001B85 RID: 7045 RVA: 0x0007A789 File Offset: 0x00078989
		public RegisterComInteropTlbTask() : base(true)
		{
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x0007A792 File Offset: 0x00078992
		[Postcondition(ExpectedResult = true, FailureDescriptionId = Strings.IDs.ComInteropTlbNotFound)]
		internal FileExistsCondition ComInteropTlbExistCheck
		{
			get
			{
				return new FileExistsCondition(Path.Combine(ConfigurationContext.Setup.BinPath, "ComInterop.tlb"));
			}
		}
	}
}
