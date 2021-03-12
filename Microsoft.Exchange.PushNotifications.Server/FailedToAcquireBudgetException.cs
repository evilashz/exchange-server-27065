using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.PushNotifications.Server.Commands;
using Microsoft.Exchange.PushNotifications.Server.LocStrings;

namespace Microsoft.Exchange.PushNotifications.Server
{
	// Token: 0x02000035 RID: 53
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToAcquireBudgetException : ServiceCommandPermanentException
	{
		// Token: 0x06000132 RID: 306 RVA: 0x00004BAF File Offset: 0x00002DAF
		public FailedToAcquireBudgetException(string windowsIdentity, string principal) : base(Strings.FailedToAcquireBudget(windowsIdentity, principal))
		{
			this.windowsIdentity = windowsIdentity;
			this.principal = principal;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004BCC File Offset: 0x00002DCC
		public FailedToAcquireBudgetException(string windowsIdentity, string principal, Exception innerException) : base(Strings.FailedToAcquireBudget(windowsIdentity, principal), innerException)
		{
			this.windowsIdentity = windowsIdentity;
			this.principal = principal;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004BEC File Offset: 0x00002DEC
		protected FailedToAcquireBudgetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.windowsIdentity = (string)info.GetValue("windowsIdentity", typeof(string));
			this.principal = (string)info.GetValue("principal", typeof(string));
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004C41 File Offset: 0x00002E41
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("windowsIdentity", this.windowsIdentity);
			info.AddValue("principal", this.principal);
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00004C6D File Offset: 0x00002E6D
		public string WindowsIdentity
		{
			get
			{
				return this.windowsIdentity;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00004C75 File Offset: 0x00002E75
		public string Principal
		{
			get
			{
				return this.principal;
			}
		}

		// Token: 0x0400006D RID: 109
		private readonly string windowsIdentity;

		// Token: 0x0400006E RID: 110
		private readonly string principal;
	}
}
