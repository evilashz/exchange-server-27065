using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001FB RID: 507
	[KnownType(typeof(ShouldContinueExceptionDetails))]
	[DataContract]
	public class ErrorInformationBase
	{
		// Token: 0x0600267F RID: 9855 RVA: 0x00077A0C File Offset: 0x00075C0C
		public ErrorInformationBase()
		{
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x00077A14 File Offset: 0x00075C14
		public ErrorInformationBase(Exception exception)
		{
			this.Exception = exception;
			if (string.IsNullOrEmpty(this.Message))
			{
				this.Message = exception.Message;
			}
			if (ErrorHandlingUtil.CanShowDebugInfo(this.Exception))
			{
				this.CallStack = this.Exception.ToTraceString();
			}
			IExceptionDetails exceptionDetails = this.Exception as IExceptionDetails;
			if (exceptionDetails != null)
			{
				this.Details = exceptionDetails.Details;
			}
		}

		// Token: 0x17001BD9 RID: 7129
		// (get) Token: 0x06002681 RID: 9857 RVA: 0x00077A80 File Offset: 0x00075C80
		// (set) Token: 0x06002682 RID: 9858 RVA: 0x00077A88 File Offset: 0x00075C88
		[DataMember(EmitDefaultValue = false)]
		public virtual string Message { get; protected set; }

		// Token: 0x17001BDA RID: 7130
		// (get) Token: 0x06002683 RID: 9859 RVA: 0x00077A91 File Offset: 0x00075C91
		// (set) Token: 0x06002684 RID: 9860 RVA: 0x00077A99 File Offset: 0x00075C99
		[DataMember(EmitDefaultValue = false)]
		public string CallStack { get; protected set; }

		// Token: 0x17001BDB RID: 7131
		// (get) Token: 0x06002685 RID: 9861 RVA: 0x00077AA2 File Offset: 0x00075CA2
		// (set) Token: 0x06002686 RID: 9862 RVA: 0x00077AAA File Offset: 0x00075CAA
		[DataMember(EmitDefaultValue = false)]
		public object Details { get; private set; }

		// Token: 0x17001BDC RID: 7132
		// (get) Token: 0x06002687 RID: 9863 RVA: 0x00077AB3 File Offset: 0x00075CB3
		// (set) Token: 0x06002688 RID: 9864 RVA: 0x00077ABB File Offset: 0x00075CBB
		public virtual Exception Exception { get; protected set; }

		// Token: 0x06002689 RID: 9865 RVA: 0x00077AC4 File Offset: 0x00075CC4
		internal void Translate(Identity translationIdentity)
		{
			this.Translate(translationIdentity, this.Message);
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x00077AD3 File Offset: 0x00075CD3
		internal void Translate(Identity translationIdentity, string newMsg)
		{
			this.Message = PowerShellMessageTranslator.Instance.Translate(translationIdentity, this.Exception, newMsg);
		}
	}
}
