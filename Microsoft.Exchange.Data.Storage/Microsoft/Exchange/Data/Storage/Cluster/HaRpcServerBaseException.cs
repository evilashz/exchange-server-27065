using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage.Cluster
{
	// Token: 0x020000C1 RID: 193
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public abstract class HaRpcServerBaseException : LocalizedException, IHaRpcServerBaseException, IHaRpcServerBaseExceptionInternal
	{
		// Token: 0x06001238 RID: 4664 RVA: 0x000675D7 File Offset: 0x000657D7
		public HaRpcServerBaseException(LocalizedString message) : base(message)
		{
			this.Initialize();
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x000675E6 File Offset: 0x000657E6
		public HaRpcServerBaseException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
			this.Initialize();
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x000675F6 File Offset: 0x000657F6
		protected HaRpcServerBaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.m_exceptionInfo = (HaRpcExceptionInfo)info.GetValue("exceptionInfo", typeof(HaRpcExceptionInfo));
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00067620 File Offset: 0x00065820
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("exceptionInfo", this.m_exceptionInfo, typeof(HaRpcExceptionInfo));
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x0600123C RID: 4668
		public abstract string ErrorMessage { get; }

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x00067645 File Offset: 0x00065845
		public string OriginatingServer
		{
			get
			{
				return this.m_exceptionInfo.OriginatingServer;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x00067652 File Offset: 0x00065852
		public string OriginatingStackTrace
		{
			get
			{
				return this.m_exceptionInfo.OriginatingStackTrace;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x0006765F File Offset: 0x0006585F
		public string DatabaseName
		{
			get
			{
				return this.m_exceptionInfo.DatabaseName;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x0006766C File Offset: 0x0006586C
		string IHaRpcServerBaseExceptionInternal.MessageInternal
		{
			get
			{
				return base.Message;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (set) Token: 0x06001241 RID: 4673 RVA: 0x00067674 File Offset: 0x00065874
		string IHaRpcServerBaseExceptionInternal.OriginatingServer
		{
			set
			{
				this.m_exceptionInfo.OriginatingServer = value;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (set) Token: 0x06001242 RID: 4674 RVA: 0x00067682 File Offset: 0x00065882
		string IHaRpcServerBaseExceptionInternal.OriginatingStackTrace
		{
			set
			{
				this.m_exceptionInfo.OriginatingStackTrace = value;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (set) Token: 0x06001243 RID: 4675 RVA: 0x00067690 File Offset: 0x00065890
		string IHaRpcServerBaseExceptionInternal.DatabaseName
		{
			set
			{
				this.m_exceptionInfo.DatabaseName = value;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x0006769E File Offset: 0x0006589E
		public override string Message
		{
			get
			{
				this.UpdateMessage();
				return this.m_message;
			}
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x000676AC File Offset: 0x000658AC
		public override string ToString()
		{
			this.UpdateFullString();
			return this.m_fullString;
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x000676BA File Offset: 0x000658BA
		private void UpdateFullString()
		{
			if (string.IsNullOrEmpty(this.OriginatingStackTrace) && string.IsNullOrEmpty(this.OriginatingServer))
			{
				this.m_fullString = base.ToString();
				return;
			}
			this.m_fullString = HaRpcExceptionHelper.GetFullString(this, this);
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x000676F0 File Offset: 0x000658F0
		private void UpdateMessage()
		{
			this.m_message = string.Format("{0}{1}", base.Message, HaRpcExceptionHelper.GetOriginatingServerString(this.OriginatingServer, this.DatabaseName));
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00067719 File Offset: 0x00065919
		private void Initialize()
		{
			this.m_exceptionInfo = new HaRpcExceptionInfo();
		}

		// Token: 0x04000951 RID: 2385
		private string m_message;

		// Token: 0x04000952 RID: 2386
		private string m_fullString;

		// Token: 0x04000953 RID: 2387
		protected HaRpcExceptionInfo m_exceptionInfo;
	}
}
