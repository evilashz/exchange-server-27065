using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F16 RID: 3862
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CasHealthErrorPopulatingMailboxException : LocalizedException
	{
		// Token: 0x0600AA5D RID: 43613 RVA: 0x0028D48A File Offset: 0x0028B68A
		public CasHealthErrorPopulatingMailboxException(string exceptionMessage) : base(Strings.CasHealthErrorPopulatingMailbox(exceptionMessage))
		{
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x0600AA5E RID: 43614 RVA: 0x0028D49F File Offset: 0x0028B69F
		public CasHealthErrorPopulatingMailboxException(string exceptionMessage, Exception innerException) : base(Strings.CasHealthErrorPopulatingMailbox(exceptionMessage), innerException)
		{
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x0600AA5F RID: 43615 RVA: 0x0028D4B5 File Offset: 0x0028B6B5
		protected CasHealthErrorPopulatingMailboxException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.exceptionMessage = (string)info.GetValue("exceptionMessage", typeof(string));
		}

		// Token: 0x0600AA60 RID: 43616 RVA: 0x0028D4DF File Offset: 0x0028B6DF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("exceptionMessage", this.exceptionMessage);
		}

		// Token: 0x17003722 RID: 14114
		// (get) Token: 0x0600AA61 RID: 43617 RVA: 0x0028D4FA File Offset: 0x0028B6FA
		public string ExceptionMessage
		{
			get
			{
				return this.exceptionMessage;
			}
		}

		// Token: 0x04006088 RID: 24712
		private readonly string exceptionMessage;
	}
}
