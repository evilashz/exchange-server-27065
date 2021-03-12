using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020002C9 RID: 713
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CommandExecutionException : LocalizedException
	{
		// Token: 0x06001954 RID: 6484 RVA: 0x0005CF0C File Offset: 0x0005B10C
		public CommandExecutionException(int innerErrorCode, string command) : base(Strings.ExceptionMDACommandExcutionError(innerErrorCode, command))
		{
			this.innerErrorCode = innerErrorCode;
			this.command = command;
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0005CF29 File Offset: 0x0005B129
		public CommandExecutionException(int innerErrorCode, string command, Exception innerException) : base(Strings.ExceptionMDACommandExcutionError(innerErrorCode, command), innerException)
		{
			this.innerErrorCode = innerErrorCode;
			this.command = command;
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0005CF48 File Offset: 0x0005B148
		protected CommandExecutionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.innerErrorCode = (int)info.GetValue("innerErrorCode", typeof(int));
			this.command = (string)info.GetValue("command", typeof(string));
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0005CF9D File Offset: 0x0005B19D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("innerErrorCode", this.innerErrorCode);
			info.AddValue("command", this.command);
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001958 RID: 6488 RVA: 0x0005CFC9 File Offset: 0x0005B1C9
		public int InnerErrorCode
		{
			get
			{
				return this.innerErrorCode;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001959 RID: 6489 RVA: 0x0005CFD1 File Offset: 0x0005B1D1
		public string Command
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x04000995 RID: 2453
		private readonly int innerErrorCode;

		// Token: 0x04000996 RID: 2454
		private readonly string command;
	}
}
