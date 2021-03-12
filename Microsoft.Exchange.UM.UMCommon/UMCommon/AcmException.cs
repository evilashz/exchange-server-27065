using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Audio;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001BD RID: 445
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AcmException : AudioConversionException
	{
		// Token: 0x06000EDC RID: 3804 RVA: 0x00035B09 File Offset: 0x00033D09
		public AcmException(string failureMessage) : base(Strings.AcmFailure(failureMessage))
		{
			this.failureMessage = failureMessage;
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x00035B23 File Offset: 0x00033D23
		public AcmException(string failureMessage, Exception innerException) : base(Strings.AcmFailure(failureMessage), innerException)
		{
			this.failureMessage = failureMessage;
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x00035B3E File Offset: 0x00033D3E
		protected AcmException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.failureMessage = (string)info.GetValue("failureMessage", typeof(string));
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x00035B68 File Offset: 0x00033D68
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("failureMessage", this.failureMessage);
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x00035B83 File Offset: 0x00033D83
		public string FailureMessage
		{
			get
			{
				return this.failureMessage;
			}
		}

		// Token: 0x04000798 RID: 1944
		private readonly string failureMessage;
	}
}
