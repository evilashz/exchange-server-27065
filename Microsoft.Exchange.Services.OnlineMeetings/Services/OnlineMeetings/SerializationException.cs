using System;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x02000039 RID: 57
	[Serializable]
	internal class SerializationException : OnlineMeetingSchedulerException
	{
		// Token: 0x06000213 RID: 531 RVA: 0x00007A57 File Offset: 0x00005C57
		public SerializationException(string message) : this(message, null)
		{
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007A61 File Offset: 0x00005C61
		public SerializationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00007A6B File Offset: 0x00005C6B
		protected SerializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00007A75 File Offset: 0x00005C75
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00007A7D File Offset: 0x00005C7D
		public HttpWebResponse HttpResponse { get; set; }

		// Token: 0x06000218 RID: 536 RVA: 0x00007A86 File Offset: 0x00005C86
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
