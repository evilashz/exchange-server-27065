using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x0200111E RID: 4382
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MissingParameterException : LocalizedException
	{
		// Token: 0x0600B484 RID: 46212 RVA: 0x0029CE67 File Offset: 0x0029B067
		public MissingParameterException(string parameter) : base(Strings.MigrationMissingParameterException(parameter))
		{
			this.parameter = parameter;
		}

		// Token: 0x0600B485 RID: 46213 RVA: 0x0029CE7C File Offset: 0x0029B07C
		public MissingParameterException(string parameter, Exception innerException) : base(Strings.MigrationMissingParameterException(parameter), innerException)
		{
			this.parameter = parameter;
		}

		// Token: 0x0600B486 RID: 46214 RVA: 0x0029CE92 File Offset: 0x0029B092
		protected MissingParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.parameter = (string)info.GetValue("parameter", typeof(string));
		}

		// Token: 0x0600B487 RID: 46215 RVA: 0x0029CEBC File Offset: 0x0029B0BC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("parameter", this.parameter);
		}

		// Token: 0x17003929 RID: 14633
		// (get) Token: 0x0600B488 RID: 46216 RVA: 0x0029CED7 File Offset: 0x0029B0D7
		public string Parameter
		{
			get
			{
				return this.parameter;
			}
		}

		// Token: 0x0400628F RID: 25231
		private readonly string parameter;
	}
}
