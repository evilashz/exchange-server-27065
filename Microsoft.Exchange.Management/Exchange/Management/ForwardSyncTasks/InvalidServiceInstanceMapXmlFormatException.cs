using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02001132 RID: 4402
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidServiceInstanceMapXmlFormatException : LocalizedException
	{
		// Token: 0x0600B4E4 RID: 46308 RVA: 0x0029D6D2 File Offset: 0x0029B8D2
		public InvalidServiceInstanceMapXmlFormatException(string reason) : base(Strings.InvalidServiceInstanceMapXmlFormat(reason))
		{
			this.reason = reason;
		}

		// Token: 0x0600B4E5 RID: 46309 RVA: 0x0029D6E7 File Offset: 0x0029B8E7
		public InvalidServiceInstanceMapXmlFormatException(string reason, Exception innerException) : base(Strings.InvalidServiceInstanceMapXmlFormat(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x0600B4E6 RID: 46310 RVA: 0x0029D6FD File Offset: 0x0029B8FD
		protected InvalidServiceInstanceMapXmlFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x0600B4E7 RID: 46311 RVA: 0x0029D727 File Offset: 0x0029B927
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17003939 RID: 14649
		// (get) Token: 0x0600B4E8 RID: 46312 RVA: 0x0029D742 File Offset: 0x0029B942
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x0400629F RID: 25247
		private readonly string reason;
	}
}
