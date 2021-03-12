using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200003E RID: 62
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MissingCapabilitiesException : OperationLevelTransientException
	{
		// Token: 0x06000129 RID: 297 RVA: 0x00003C14 File Offset: 0x00001E14
		public MissingCapabilitiesException(string missingCapabilitiesMsg) : base(CXStrings.MissingCapabilitiesError(missingCapabilitiesMsg))
		{
			this.missingCapabilitiesMsg = missingCapabilitiesMsg;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00003C2E File Offset: 0x00001E2E
		public MissingCapabilitiesException(string missingCapabilitiesMsg, Exception innerException) : base(CXStrings.MissingCapabilitiesError(missingCapabilitiesMsg), innerException)
		{
			this.missingCapabilitiesMsg = missingCapabilitiesMsg;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00003C49 File Offset: 0x00001E49
		protected MissingCapabilitiesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.missingCapabilitiesMsg = (string)info.GetValue("missingCapabilitiesMsg", typeof(string));
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00003C73 File Offset: 0x00001E73
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("missingCapabilitiesMsg", this.missingCapabilitiesMsg);
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00003C8E File Offset: 0x00001E8E
		public string MissingCapabilitiesMsg
		{
			get
			{
				return this.missingCapabilitiesMsg;
			}
		}

		// Token: 0x040000D6 RID: 214
		private readonly string missingCapabilitiesMsg;
	}
}
