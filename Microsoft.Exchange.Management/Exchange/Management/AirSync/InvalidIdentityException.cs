using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.AirSync
{
	// Token: 0x02000E2B RID: 3627
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidIdentityException : LocalizedException
	{
		// Token: 0x0600A5DD RID: 42461 RVA: 0x00286BBD File Offset: 0x00284DBD
		public InvalidIdentityException(string smtpAddress) : base(Strings.InvalidIdentityException(smtpAddress))
		{
			this.smtpAddress = smtpAddress;
		}

		// Token: 0x0600A5DE RID: 42462 RVA: 0x00286BD2 File Offset: 0x00284DD2
		public InvalidIdentityException(string smtpAddress, Exception innerException) : base(Strings.InvalidIdentityException(smtpAddress), innerException)
		{
			this.smtpAddress = smtpAddress;
		}

		// Token: 0x0600A5DF RID: 42463 RVA: 0x00286BE8 File Offset: 0x00284DE8
		protected InvalidIdentityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.smtpAddress = (string)info.GetValue("smtpAddress", typeof(string));
		}

		// Token: 0x0600A5E0 RID: 42464 RVA: 0x00286C12 File Offset: 0x00284E12
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("smtpAddress", this.smtpAddress);
		}

		// Token: 0x1700364E RID: 13902
		// (get) Token: 0x0600A5E1 RID: 42465 RVA: 0x00286C2D File Offset: 0x00284E2D
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
		}

		// Token: 0x04005FB4 RID: 24500
		private readonly string smtpAddress;
	}
}
