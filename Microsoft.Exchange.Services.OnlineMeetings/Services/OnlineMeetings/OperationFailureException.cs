using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x02000037 RID: 55
	[Serializable]
	internal class OperationFailureException : OnlineMeetingSchedulerException
	{
		// Token: 0x06000205 RID: 517 RVA: 0x00007972 File Offset: 0x00005B72
		public OperationFailureException()
		{
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000797A File Offset: 0x00005B7A
		public OperationFailureException(string message) : this(message, null)
		{
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00007984 File Offset: 0x00005B84
		public OperationFailureException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000798E File Offset: 0x00005B8E
		protected OperationFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00007998 File Offset: 0x00005B98
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
