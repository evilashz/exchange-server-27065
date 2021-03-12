using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200114F RID: 4431
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TSCrashDumpsOnlyAvailableOnLocalMachineException : LocalizedException
	{
		// Token: 0x0600B575 RID: 46453 RVA: 0x0029E4BA File Offset: 0x0029C6BA
		public TSCrashDumpsOnlyAvailableOnLocalMachineException() : base(Strings.TSCrashDumpsOnlyAvailableOnLocalMachine)
		{
		}

		// Token: 0x0600B576 RID: 46454 RVA: 0x0029E4C7 File Offset: 0x0029C6C7
		public TSCrashDumpsOnlyAvailableOnLocalMachineException(Exception innerException) : base(Strings.TSCrashDumpsOnlyAvailableOnLocalMachine, innerException)
		{
		}

		// Token: 0x0600B577 RID: 46455 RVA: 0x0029E4D5 File Offset: 0x0029C6D5
		protected TSCrashDumpsOnlyAvailableOnLocalMachineException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B578 RID: 46456 RVA: 0x0029E4DF File Offset: 0x0029C6DF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
