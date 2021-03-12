using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02001159 RID: 4441
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidMsoSyncServiceResponseException : LocalizedException
	{
		// Token: 0x0600B5A8 RID: 46504 RVA: 0x0029E9C2 File Offset: 0x0029CBC2
		public InvalidMsoSyncServiceResponseException(string reason) : base(Strings.InvalidMsoSyncServiceResponse(reason))
		{
			this.reason = reason;
		}

		// Token: 0x0600B5A9 RID: 46505 RVA: 0x0029E9D7 File Offset: 0x0029CBD7
		public InvalidMsoSyncServiceResponseException(string reason, Exception innerException) : base(Strings.InvalidMsoSyncServiceResponse(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x0600B5AA RID: 46506 RVA: 0x0029E9ED File Offset: 0x0029CBED
		protected InvalidMsoSyncServiceResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x0600B5AB RID: 46507 RVA: 0x0029EA17 File Offset: 0x0029CC17
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17003961 RID: 14689
		// (get) Token: 0x0600B5AC RID: 46508 RVA: 0x0029EA32 File Offset: 0x0029CC32
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x040062C7 RID: 25287
		private readonly string reason;
	}
}
