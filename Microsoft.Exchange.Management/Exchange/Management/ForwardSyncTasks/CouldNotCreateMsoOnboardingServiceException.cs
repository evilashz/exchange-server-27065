using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02001131 RID: 4401
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotCreateMsoOnboardingServiceException : LocalizedException
	{
		// Token: 0x0600B4DF RID: 46303 RVA: 0x0029D65A File Offset: 0x0029B85A
		public CouldNotCreateMsoOnboardingServiceException(string reason) : base(Strings.CouldNotCreateMSOOnboardingService(reason))
		{
			this.reason = reason;
		}

		// Token: 0x0600B4E0 RID: 46304 RVA: 0x0029D66F File Offset: 0x0029B86F
		public CouldNotCreateMsoOnboardingServiceException(string reason, Exception innerException) : base(Strings.CouldNotCreateMSOOnboardingService(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x0600B4E1 RID: 46305 RVA: 0x0029D685 File Offset: 0x0029B885
		protected CouldNotCreateMsoOnboardingServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x0600B4E2 RID: 46306 RVA: 0x0029D6AF File Offset: 0x0029B8AF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17003938 RID: 14648
		// (get) Token: 0x0600B4E3 RID: 46307 RVA: 0x0029D6CA File Offset: 0x0029B8CA
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x0400629E RID: 25246
		private readonly string reason;
	}
}
