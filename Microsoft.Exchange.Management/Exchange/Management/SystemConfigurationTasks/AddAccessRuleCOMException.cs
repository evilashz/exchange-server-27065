using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F4A RID: 3914
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AddAccessRuleCOMException : LocalizedException
	{
		// Token: 0x0600AB6C RID: 43884 RVA: 0x0028F0D5 File Offset: 0x0028D2D5
		public AddAccessRuleCOMException(string thumbprint) : base(Strings.AddAccessRuleCOM(thumbprint))
		{
			this.thumbprint = thumbprint;
		}

		// Token: 0x0600AB6D RID: 43885 RVA: 0x0028F0EA File Offset: 0x0028D2EA
		public AddAccessRuleCOMException(string thumbprint, Exception innerException) : base(Strings.AddAccessRuleCOM(thumbprint), innerException)
		{
			this.thumbprint = thumbprint;
		}

		// Token: 0x0600AB6E RID: 43886 RVA: 0x0028F100 File Offset: 0x0028D300
		protected AddAccessRuleCOMException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.thumbprint = (string)info.GetValue("thumbprint", typeof(string));
		}

		// Token: 0x0600AB6F RID: 43887 RVA: 0x0028F12A File Offset: 0x0028D32A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("thumbprint", this.thumbprint);
		}

		// Token: 0x17003761 RID: 14177
		// (get) Token: 0x0600AB70 RID: 43888 RVA: 0x0028F145 File Offset: 0x0028D345
		public string Thumbprint
		{
			get
			{
				return this.thumbprint;
			}
		}

		// Token: 0x040060C7 RID: 24775
		private readonly string thumbprint;
	}
}
