using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02001158 RID: 4440
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotCreateMsoSyncServiceException : LocalizedException
	{
		// Token: 0x0600B5A3 RID: 46499 RVA: 0x0029E94A File Offset: 0x0029CB4A
		public CouldNotCreateMsoSyncServiceException(string reason) : base(Strings.CouldNotCreateMsoSyncService(reason))
		{
			this.reason = reason;
		}

		// Token: 0x0600B5A4 RID: 46500 RVA: 0x0029E95F File Offset: 0x0029CB5F
		public CouldNotCreateMsoSyncServiceException(string reason, Exception innerException) : base(Strings.CouldNotCreateMsoSyncService(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x0600B5A5 RID: 46501 RVA: 0x0029E975 File Offset: 0x0029CB75
		protected CouldNotCreateMsoSyncServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x0600B5A6 RID: 46502 RVA: 0x0029E99F File Offset: 0x0029CB9F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17003960 RID: 14688
		// (get) Token: 0x0600B5A7 RID: 46503 RVA: 0x0029E9BA File Offset: 0x0029CBBA
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x040062C6 RID: 25286
		private readonly string reason;
	}
}
