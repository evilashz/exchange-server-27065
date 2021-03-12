using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200000F RID: 15
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnknownAssistantException : LocalizedException
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00003DD8 File Offset: 0x00001FD8
		public UnknownAssistantException(string assistantName) : base(Strings.descUnknownAssistant(assistantName))
		{
			this.assistantName = assistantName;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003DED File Offset: 0x00001FED
		public UnknownAssistantException(string assistantName, Exception innerException) : base(Strings.descUnknownAssistant(assistantName), innerException)
		{
			this.assistantName = assistantName;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003E03 File Offset: 0x00002003
		protected UnknownAssistantException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.assistantName = (string)info.GetValue("assistantName", typeof(string));
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003E2D File Offset: 0x0000202D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("assistantName", this.assistantName);
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00003E48 File Offset: 0x00002048
		public string AssistantName
		{
			get
			{
				return this.assistantName;
			}
		}

		// Token: 0x0400009D RID: 157
		private readonly string assistantName;
	}
}
