using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E38 RID: 3640
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExceptionWhileWritingOutputFile : LocalizedException
	{
		// Token: 0x0600A623 RID: 42531 RVA: 0x00287375 File Offset: 0x00285575
		public ExceptionWhileWritingOutputFile(string filename, string exMessage) : base(Strings.ExceptionWhileWritingOutputFile(filename, exMessage))
		{
			this.filename = filename;
			this.exMessage = exMessage;
		}

		// Token: 0x0600A624 RID: 42532 RVA: 0x00287392 File Offset: 0x00285592
		public ExceptionWhileWritingOutputFile(string filename, string exMessage, Exception innerException) : base(Strings.ExceptionWhileWritingOutputFile(filename, exMessage), innerException)
		{
			this.filename = filename;
			this.exMessage = exMessage;
		}

		// Token: 0x0600A625 RID: 42533 RVA: 0x002873B0 File Offset: 0x002855B0
		protected ExceptionWhileWritingOutputFile(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filename = (string)info.GetValue("filename", typeof(string));
			this.exMessage = (string)info.GetValue("exMessage", typeof(string));
		}

		// Token: 0x0600A626 RID: 42534 RVA: 0x00287405 File Offset: 0x00285605
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filename", this.filename);
			info.AddValue("exMessage", this.exMessage);
		}

		// Token: 0x17003660 RID: 13920
		// (get) Token: 0x0600A627 RID: 42535 RVA: 0x00287431 File Offset: 0x00285631
		public string Filename
		{
			get
			{
				return this.filename;
			}
		}

		// Token: 0x17003661 RID: 13921
		// (get) Token: 0x0600A628 RID: 42536 RVA: 0x00287439 File Offset: 0x00285639
		public string ExMessage
		{
			get
			{
				return this.exMessage;
			}
		}

		// Token: 0x04005FC6 RID: 24518
		private readonly string filename;

		// Token: 0x04005FC7 RID: 24519
		private readonly string exMessage;
	}
}
