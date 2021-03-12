using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage.Cluster
{
	// Token: 0x020000E1 RID: 225
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public abstract class HaRpcServerTransientBaseException : TransientException, IHaRpcServerBaseException, IHaRpcServerBaseExceptionInternal
	{
		// Token: 0x060012E1 RID: 4833 RVA: 0x0006860B File Offset: 0x0006680B
		public HaRpcServerTransientBaseException(LocalizedString message) : base(message)
		{
			this.Initialize();
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x0006861A File Offset: 0x0006681A
		public HaRpcServerTransientBaseException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
			this.Initialize();
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x0006862A File Offset: 0x0006682A
		protected HaRpcServerTransientBaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.m_exceptionInfo = (HaRpcExceptionInfo)info.GetValue("exceptionInfo", typeof(HaRpcExceptionInfo));
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x00068654 File Offset: 0x00066854
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("exceptionInfo", this.m_exceptionInfo, typeof(HaRpcExceptionInfo));
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060012E5 RID: 4837
		public abstract string ErrorMessage { get; }

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x00068679 File Offset: 0x00066879
		public string OriginatingServer
		{
			get
			{
				return this.m_exceptionInfo.OriginatingServer;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x00068686 File Offset: 0x00066886
		public string OriginatingStackTrace
		{
			get
			{
				return this.m_exceptionInfo.OriginatingStackTrace;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x00068693 File Offset: 0x00066893
		public string DatabaseName
		{
			get
			{
				return this.m_exceptionInfo.DatabaseName;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x000686A0 File Offset: 0x000668A0
		string IHaRpcServerBaseExceptionInternal.MessageInternal
		{
			get
			{
				return base.Message;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (set) Token: 0x060012EA RID: 4842 RVA: 0x000686A8 File Offset: 0x000668A8
		string IHaRpcServerBaseExceptionInternal.OriginatingServer
		{
			set
			{
				this.m_exceptionInfo.OriginatingServer = value;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (set) Token: 0x060012EB RID: 4843 RVA: 0x000686B6 File Offset: 0x000668B6
		string IHaRpcServerBaseExceptionInternal.OriginatingStackTrace
		{
			set
			{
				this.m_exceptionInfo.OriginatingStackTrace = value;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (set) Token: 0x060012EC RID: 4844 RVA: 0x000686C4 File Offset: 0x000668C4
		string IHaRpcServerBaseExceptionInternal.DatabaseName
		{
			set
			{
				this.m_exceptionInfo.DatabaseName = value;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x000686D2 File Offset: 0x000668D2
		public override string Message
		{
			get
			{
				this.UpdateMessage();
				return this.m_message;
			}
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x000686E0 File Offset: 0x000668E0
		public override string ToString()
		{
			this.UpdateFullString();
			return this.m_fullString;
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x000686EE File Offset: 0x000668EE
		private void UpdateFullString()
		{
			if (string.IsNullOrEmpty(this.OriginatingStackTrace) && string.IsNullOrEmpty(this.OriginatingServer))
			{
				this.m_fullString = base.ToString();
				return;
			}
			this.m_fullString = HaRpcExceptionHelper.GetFullString(this, this);
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x00068724 File Offset: 0x00066924
		private void UpdateMessage()
		{
			this.m_message = string.Format("{0}{1}", base.Message, HaRpcExceptionHelper.GetOriginatingServerString(this.OriginatingServer, this.DatabaseName));
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x0006874D File Offset: 0x0006694D
		private void Initialize()
		{
			this.m_exceptionInfo = new HaRpcExceptionInfo();
		}

		// Token: 0x0400096F RID: 2415
		private string m_message;

		// Token: 0x04000970 RID: 2416
		private string m_fullString;

		// Token: 0x04000971 RID: 2417
		protected HaRpcExceptionInfo m_exceptionInfo;
	}
}
