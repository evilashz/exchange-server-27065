using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MessageSecurity
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	internal sealed class MessageSecurityException : ApplicationException
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000026C5 File Offset: 0x000008C5
		public MessageSecurityException()
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000026CD File Offset: 0x000008CD
		public MessageSecurityException(string message) : base(message)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000026D6 File Offset: 0x000008D6
		public MessageSecurityException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000026E0 File Offset: 0x000008E0
		public MessageSecurityException(string message, int hr) : base(message)
		{
			base.HResult = hr;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000026F0 File Offset: 0x000008F0
		internal MessageSecurityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026FA File Offset: 0x000008FA
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
