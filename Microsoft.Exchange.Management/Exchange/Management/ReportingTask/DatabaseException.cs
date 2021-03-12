using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ReportingTask
{
	// Token: 0x0200115E RID: 4446
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseException : ReportingException
	{
		// Token: 0x0600B5C2 RID: 46530 RVA: 0x0029EC75 File Offset: 0x0029CE75
		public DatabaseException(int number, string error) : base(Strings.DatabaseException(number, error))
		{
			this.number = number;
			this.error = error;
		}

		// Token: 0x0600B5C3 RID: 46531 RVA: 0x0029EC92 File Offset: 0x0029CE92
		public DatabaseException(int number, string error, Exception innerException) : base(Strings.DatabaseException(number, error), innerException)
		{
			this.number = number;
			this.error = error;
		}

		// Token: 0x0600B5C4 RID: 46532 RVA: 0x0029ECB0 File Offset: 0x0029CEB0
		protected DatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.number = (int)info.GetValue("number", typeof(int));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600B5C5 RID: 46533 RVA: 0x0029ED05 File Offset: 0x0029CF05
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("number", this.number);
			info.AddValue("error", this.error);
		}

		// Token: 0x17003967 RID: 14695
		// (get) Token: 0x0600B5C6 RID: 46534 RVA: 0x0029ED31 File Offset: 0x0029CF31
		public int Number
		{
			get
			{
				return this.number;
			}
		}

		// Token: 0x17003968 RID: 14696
		// (get) Token: 0x0600B5C7 RID: 46535 RVA: 0x0029ED39 File Offset: 0x0029CF39
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040062CD RID: 25293
		private readonly int number;

		// Token: 0x040062CE RID: 25294
		private readonly string error;
	}
}
