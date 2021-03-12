using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200110A RID: 4362
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MissingParameterException : LocalizedException
	{
		// Token: 0x0600B423 RID: 46115 RVA: 0x0029C5A9 File Offset: 0x0029A7A9
		public MissingParameterException(string parameter) : base(Strings.messageMissingParameterException(parameter))
		{
			this.parameter = parameter;
		}

		// Token: 0x0600B424 RID: 46116 RVA: 0x0029C5BE File Offset: 0x0029A7BE
		public MissingParameterException(string parameter, Exception innerException) : base(Strings.messageMissingParameterException(parameter), innerException)
		{
			this.parameter = parameter;
		}

		// Token: 0x0600B425 RID: 46117 RVA: 0x0029C5D4 File Offset: 0x0029A7D4
		protected MissingParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.parameter = (string)info.GetValue("parameter", typeof(string));
		}

		// Token: 0x0600B426 RID: 46118 RVA: 0x0029C5FE File Offset: 0x0029A7FE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("parameter", this.parameter);
		}

		// Token: 0x17003918 RID: 14616
		// (get) Token: 0x0600B427 RID: 46119 RVA: 0x0029C619 File Offset: 0x0029A819
		public string Parameter
		{
			get
			{
				return this.parameter;
			}
		}

		// Token: 0x0400627E RID: 25214
		private readonly string parameter;
	}
}
