using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200003F RID: 63
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnexpectedCapabilitiesException : OperationLevelTransientException
	{
		// Token: 0x0600012E RID: 302 RVA: 0x00003C96 File Offset: 0x00001E96
		public UnexpectedCapabilitiesException(string unexpectedCapabilitiesMsg) : base(CXStrings.UnexpectedCapabilitiesError(unexpectedCapabilitiesMsg))
		{
			this.unexpectedCapabilitiesMsg = unexpectedCapabilitiesMsg;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00003CB0 File Offset: 0x00001EB0
		public UnexpectedCapabilitiesException(string unexpectedCapabilitiesMsg, Exception innerException) : base(CXStrings.UnexpectedCapabilitiesError(unexpectedCapabilitiesMsg), innerException)
		{
			this.unexpectedCapabilitiesMsg = unexpectedCapabilitiesMsg;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00003CCB File Offset: 0x00001ECB
		protected UnexpectedCapabilitiesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.unexpectedCapabilitiesMsg = (string)info.GetValue("unexpectedCapabilitiesMsg", typeof(string));
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00003CF5 File Offset: 0x00001EF5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("unexpectedCapabilitiesMsg", this.unexpectedCapabilitiesMsg);
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00003D10 File Offset: 0x00001F10
		public string UnexpectedCapabilitiesMsg
		{
			get
			{
				return this.unexpectedCapabilitiesMsg;
			}
		}

		// Token: 0x040000D7 RID: 215
		private readonly string unexpectedCapabilitiesMsg;
	}
}
