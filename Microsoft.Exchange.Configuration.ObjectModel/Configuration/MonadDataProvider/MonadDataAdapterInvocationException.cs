using System;
using System.Management.Automation;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001CB RID: 459
	[Serializable]
	internal class MonadDataAdapterInvocationException : ExCmdletInvocationException
	{
		// Token: 0x06001051 RID: 4177 RVA: 0x00031B50 File Offset: 0x0002FD50
		internal MonadDataAdapterInvocationException(ErrorRecord errorRecord, string commandText) : base(errorRecord)
		{
			this.commandText = commandText;
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x00031B60 File Offset: 0x0002FD60
		protected MonadDataAdapterInvocationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x00031B6A File Offset: 0x0002FD6A
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x00031B74 File Offset: 0x0002FD74
		public override string Message
		{
			get
			{
				if (this.ErrorRecord != null && this.ErrorRecord.Exception != null && !string.IsNullOrEmpty(this.ErrorRecord.Exception.Message) && !string.IsNullOrEmpty(this.commandText))
				{
					return Strings.InvocationExceptionDescription(this.ErrorRecord.Exception.Message, this.commandText).ToString();
				}
				if (!string.IsNullOrEmpty(this.commandText))
				{
					return Strings.InvocationExceptionDescriptionWithoutError(this.commandText).ToString();
				}
				return base.Message;
			}
		}

		// Token: 0x0400037E RID: 894
		private string commandText;
	}
}
