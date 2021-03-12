using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.ProvisioningAgent;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000069 RID: 105
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorsDuringAdminLogProvisioningHandlerValidateException : AdminAuditLogException
	{
		// Token: 0x06000300 RID: 768 RVA: 0x000115B6 File Offset: 0x0000F7B6
		public ErrorsDuringAdminLogProvisioningHandlerValidateException(string error) : base(Strings.ErrorsDuringAdminLogProvisioningHandlerValidate(error))
		{
			this.error = error;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000115CB File Offset: 0x0000F7CB
		public ErrorsDuringAdminLogProvisioningHandlerValidateException(string error, Exception innerException) : base(Strings.ErrorsDuringAdminLogProvisioningHandlerValidate(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000115E1 File Offset: 0x0000F7E1
		protected ErrorsDuringAdminLogProvisioningHandlerValidateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0001160B File Offset: 0x0000F80B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00011626 File Offset: 0x0000F826
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040001BB RID: 443
		private readonly string error;
	}
}
