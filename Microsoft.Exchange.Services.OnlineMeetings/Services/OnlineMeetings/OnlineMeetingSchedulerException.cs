using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x02000036 RID: 54
	internal abstract class OnlineMeetingSchedulerException : Exception
	{
		// Token: 0x06000200 RID: 512 RVA: 0x00007942 File Offset: 0x00005B42
		protected OnlineMeetingSchedulerException()
		{
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000794A File Offset: 0x00005B4A
		protected OnlineMeetingSchedulerException(string message) : this(message, null)
		{
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00007954 File Offset: 0x00005B54
		protected OnlineMeetingSchedulerException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000795E File Offset: 0x00005B5E
		protected OnlineMeetingSchedulerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00007968 File Offset: 0x00005B68
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
