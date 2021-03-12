using System;
using System.Management.Automation;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000B0 RID: 176
	[Serializable]
	internal class ThrowTerminatingErrorException : ExCmdletInvocationException
	{
		// Token: 0x06000724 RID: 1828 RVA: 0x0001A920 File Offset: 0x00018B20
		internal ThrowTerminatingErrorException(ErrorRecord errorRecord) : base(errorRecord)
		{
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001A929 File Offset: 0x00018B29
		protected ThrowTerminatingErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001A933 File Offset: 0x00018B33
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
