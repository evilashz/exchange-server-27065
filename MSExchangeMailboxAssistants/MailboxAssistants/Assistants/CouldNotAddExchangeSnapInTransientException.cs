using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x02000139 RID: 313
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CouldNotAddExchangeSnapInTransientException : TransientException
	{
		// Token: 0x06000CF3 RID: 3315 RVA: 0x00051A80 File Offset: 0x0004FC80
		public CouldNotAddExchangeSnapInTransientException(string snapInName) : base(Strings.CouldNotAddExchangeSnapIn(snapInName))
		{
			this.snapInName = snapInName;
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00051A95 File Offset: 0x0004FC95
		public CouldNotAddExchangeSnapInTransientException(string snapInName, Exception innerException) : base(Strings.CouldNotAddExchangeSnapIn(snapInName), innerException)
		{
			this.snapInName = snapInName;
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00051AAB File Offset: 0x0004FCAB
		protected CouldNotAddExchangeSnapInTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.snapInName = (string)info.GetValue("snapInName", typeof(string));
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00051AD5 File Offset: 0x0004FCD5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("snapInName", this.snapInName);
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x00051AF0 File Offset: 0x0004FCF0
		public string SnapInName
		{
			get
			{
				return this.snapInName;
			}
		}

		// Token: 0x04000835 RID: 2101
		private readonly string snapInName;
	}
}
