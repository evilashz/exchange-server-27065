using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E37 RID: 3639
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExceptionWhileReadingInputFile : LocalizedException
	{
		// Token: 0x0600A61D RID: 42525 RVA: 0x002872A9 File Offset: 0x002854A9
		public ExceptionWhileReadingInputFile(string filename, string exMessage) : base(Strings.ExceptionWhileReadingInputFile(filename, exMessage))
		{
			this.filename = filename;
			this.exMessage = exMessage;
		}

		// Token: 0x0600A61E RID: 42526 RVA: 0x002872C6 File Offset: 0x002854C6
		public ExceptionWhileReadingInputFile(string filename, string exMessage, Exception innerException) : base(Strings.ExceptionWhileReadingInputFile(filename, exMessage), innerException)
		{
			this.filename = filename;
			this.exMessage = exMessage;
		}

		// Token: 0x0600A61F RID: 42527 RVA: 0x002872E4 File Offset: 0x002854E4
		protected ExceptionWhileReadingInputFile(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filename = (string)info.GetValue("filename", typeof(string));
			this.exMessage = (string)info.GetValue("exMessage", typeof(string));
		}

		// Token: 0x0600A620 RID: 42528 RVA: 0x00287339 File Offset: 0x00285539
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filename", this.filename);
			info.AddValue("exMessage", this.exMessage);
		}

		// Token: 0x1700365E RID: 13918
		// (get) Token: 0x0600A621 RID: 42529 RVA: 0x00287365 File Offset: 0x00285565
		public string Filename
		{
			get
			{
				return this.filename;
			}
		}

		// Token: 0x1700365F RID: 13919
		// (get) Token: 0x0600A622 RID: 42530 RVA: 0x0028736D File Offset: 0x0028556D
		public string ExMessage
		{
			get
			{
				return this.exMessage;
			}
		}

		// Token: 0x04005FC4 RID: 24516
		private readonly string filename;

		// Token: 0x04005FC5 RID: 24517
		private readonly string exMessage;
	}
}
