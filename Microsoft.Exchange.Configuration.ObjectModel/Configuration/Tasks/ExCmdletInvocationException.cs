using System;
using System.Management.Automation;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200000E RID: 14
	[Serializable]
	internal abstract class ExCmdletInvocationException : CmdletInvocationException
	{
		// Token: 0x0600008A RID: 138 RVA: 0x0000413A File Offset: 0x0000233A
		internal ExCmdletInvocationException(ErrorRecord errorRecord) : base((errorRecord.Exception != null) ? errorRecord.Exception.Message : null, errorRecord.Exception)
		{
			this.errorRecord = errorRecord;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004165 File Offset: 0x00002365
		protected ExCmdletInvocationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorRecord = (ErrorRecord)info.GetValue("errorRecord", typeof(ErrorRecord));
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000418F File Offset: 0x0000238F
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorRecord", this.ErrorRecord);
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000041AA File Offset: 0x000023AA
		public override string Message
		{
			get
			{
				if (this.ErrorRecord != null && this.ErrorRecord.Exception != null)
				{
					return this.ErrorRecord.Exception.Message;
				}
				return base.Message;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000041D8 File Offset: 0x000023D8
		public override ErrorRecord ErrorRecord
		{
			get
			{
				return this.errorRecord;
			}
		}

		// Token: 0x0400003A RID: 58
		protected ErrorRecord errorRecord;
	}
}
