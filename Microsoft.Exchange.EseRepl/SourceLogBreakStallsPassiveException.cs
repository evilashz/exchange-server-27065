using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000053 RID: 83
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SourceLogBreakStallsPassiveException : TransientException
	{
		// Token: 0x06000294 RID: 660 RVA: 0x000096D1 File Offset: 0x000078D1
		public SourceLogBreakStallsPassiveException(string sourceServerName, string error) : base(Strings.SourceLogBreakStallsPassiveError(sourceServerName, error))
		{
			this.sourceServerName = sourceServerName;
			this.error = error;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000096EE File Offset: 0x000078EE
		public SourceLogBreakStallsPassiveException(string sourceServerName, string error, Exception innerException) : base(Strings.SourceLogBreakStallsPassiveError(sourceServerName, error), innerException)
		{
			this.sourceServerName = sourceServerName;
			this.error = error;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000970C File Offset: 0x0000790C
		protected SourceLogBreakStallsPassiveException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.sourceServerName = (string)info.GetValue("sourceServerName", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00009761 File Offset: 0x00007961
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("sourceServerName", this.sourceServerName);
			info.AddValue("error", this.error);
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000978D File Offset: 0x0000798D
		public string SourceServerName
		{
			get
			{
				return this.sourceServerName;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00009795 File Offset: 0x00007995
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400016E RID: 366
		private readonly string sourceServerName;

		// Token: 0x0400016F RID: 367
		private readonly string error;
	}
}
