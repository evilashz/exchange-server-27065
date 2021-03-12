using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AA5 RID: 2725
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADConstraintViolationException : ADOperationException
	{
		// Token: 0x06008001 RID: 32769 RVA: 0x001A4B7B File Offset: 0x001A2D7B
		public ADConstraintViolationException(string server, string errorMessage) : base(DirectoryStrings.ExceptionADConstraintViolation(server, errorMessage))
		{
			this.server = server;
			this.errorMessage = errorMessage;
		}

		// Token: 0x06008002 RID: 32770 RVA: 0x001A4B98 File Offset: 0x001A2D98
		public ADConstraintViolationException(string server, string errorMessage, Exception innerException) : base(DirectoryStrings.ExceptionADConstraintViolation(server, errorMessage), innerException)
		{
			this.server = server;
			this.errorMessage = errorMessage;
		}

		// Token: 0x06008003 RID: 32771 RVA: 0x001A4BB8 File Offset: 0x001A2DB8
		protected ADConstraintViolationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x06008004 RID: 32772 RVA: 0x001A4C0D File Offset: 0x001A2E0D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17002EC4 RID: 11972
		// (get) Token: 0x06008005 RID: 32773 RVA: 0x001A4C39 File Offset: 0x001A2E39
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x17002EC5 RID: 11973
		// (get) Token: 0x06008006 RID: 32774 RVA: 0x001A4C41 File Offset: 0x001A2E41
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x0400559E RID: 21918
		private readonly string server;

		// Token: 0x0400559F RID: 21919
		private readonly string errorMessage;
	}
}
