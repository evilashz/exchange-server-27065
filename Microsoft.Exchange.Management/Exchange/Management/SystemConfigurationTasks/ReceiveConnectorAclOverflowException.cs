using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F84 RID: 3972
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReceiveConnectorAclOverflowException : LocalizedException
	{
		// Token: 0x0600AC72 RID: 44146 RVA: 0x0029042B File Offset: 0x0028E62B
		public ReceiveConnectorAclOverflowException(string exception) : base(Strings.ReceiveConnectorAclOverflow(exception))
		{
			this.exception = exception;
		}

		// Token: 0x0600AC73 RID: 44147 RVA: 0x00290440 File Offset: 0x0028E640
		public ReceiveConnectorAclOverflowException(string exception, Exception innerException) : base(Strings.ReceiveConnectorAclOverflow(exception), innerException)
		{
			this.exception = exception;
		}

		// Token: 0x0600AC74 RID: 44148 RVA: 0x00290456 File Offset: 0x0028E656
		protected ReceiveConnectorAclOverflowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.exception = (string)info.GetValue("exception", typeof(string));
		}

		// Token: 0x0600AC75 RID: 44149 RVA: 0x00290480 File Offset: 0x0028E680
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("exception", this.exception);
		}

		// Token: 0x1700377F RID: 14207
		// (get) Token: 0x0600AC76 RID: 44150 RVA: 0x0029049B File Offset: 0x0028E69B
		public string Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x040060E5 RID: 24805
		private readonly string exception;
	}
}
