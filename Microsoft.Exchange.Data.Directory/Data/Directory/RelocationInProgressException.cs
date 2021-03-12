using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AC7 RID: 2759
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RelocationInProgressException : ADOperationException
	{
		// Token: 0x060080A0 RID: 32928 RVA: 0x001A5860 File Offset: 0x001A3A60
		public RelocationInProgressException(string tenantName, string permError, string suspened, string autoCompletion, string currentState, string requestedState) : base(DirectoryStrings.RelocationInProgress(tenantName, permError, suspened, autoCompletion, currentState, requestedState))
		{
			this.tenantName = tenantName;
			this.permError = permError;
			this.suspened = suspened;
			this.autoCompletion = autoCompletion;
			this.currentState = currentState;
			this.requestedState = requestedState;
		}

		// Token: 0x060080A1 RID: 32929 RVA: 0x001A58B0 File Offset: 0x001A3AB0
		public RelocationInProgressException(string tenantName, string permError, string suspened, string autoCompletion, string currentState, string requestedState, Exception innerException) : base(DirectoryStrings.RelocationInProgress(tenantName, permError, suspened, autoCompletion, currentState, requestedState), innerException)
		{
			this.tenantName = tenantName;
			this.permError = permError;
			this.suspened = suspened;
			this.autoCompletion = autoCompletion;
			this.currentState = currentState;
			this.requestedState = requestedState;
		}

		// Token: 0x060080A2 RID: 32930 RVA: 0x001A5900 File Offset: 0x001A3B00
		protected RelocationInProgressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.tenantName = (string)info.GetValue("tenantName", typeof(string));
			this.permError = (string)info.GetValue("permError", typeof(string));
			this.suspened = (string)info.GetValue("suspened", typeof(string));
			this.autoCompletion = (string)info.GetValue("autoCompletion", typeof(string));
			this.currentState = (string)info.GetValue("currentState", typeof(string));
			this.requestedState = (string)info.GetValue("requestedState", typeof(string));
		}

		// Token: 0x060080A3 RID: 32931 RVA: 0x001A59D8 File Offset: 0x001A3BD8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("tenantName", this.tenantName);
			info.AddValue("permError", this.permError);
			info.AddValue("suspened", this.suspened);
			info.AddValue("autoCompletion", this.autoCompletion);
			info.AddValue("currentState", this.currentState);
			info.AddValue("requestedState", this.requestedState);
		}

		// Token: 0x17002EDB RID: 11995
		// (get) Token: 0x060080A4 RID: 32932 RVA: 0x001A5A53 File Offset: 0x001A3C53
		public string TenantName
		{
			get
			{
				return this.tenantName;
			}
		}

		// Token: 0x17002EDC RID: 11996
		// (get) Token: 0x060080A5 RID: 32933 RVA: 0x001A5A5B File Offset: 0x001A3C5B
		public string PermError
		{
			get
			{
				return this.permError;
			}
		}

		// Token: 0x17002EDD RID: 11997
		// (get) Token: 0x060080A6 RID: 32934 RVA: 0x001A5A63 File Offset: 0x001A3C63
		public string Suspened
		{
			get
			{
				return this.suspened;
			}
		}

		// Token: 0x17002EDE RID: 11998
		// (get) Token: 0x060080A7 RID: 32935 RVA: 0x001A5A6B File Offset: 0x001A3C6B
		public string AutoCompletion
		{
			get
			{
				return this.autoCompletion;
			}
		}

		// Token: 0x17002EDF RID: 11999
		// (get) Token: 0x060080A8 RID: 32936 RVA: 0x001A5A73 File Offset: 0x001A3C73
		public string CurrentState
		{
			get
			{
				return this.currentState;
			}
		}

		// Token: 0x17002EE0 RID: 12000
		// (get) Token: 0x060080A9 RID: 32937 RVA: 0x001A5A7B File Offset: 0x001A3C7B
		public string RequestedState
		{
			get
			{
				return this.requestedState;
			}
		}

		// Token: 0x040055B5 RID: 21941
		private readonly string tenantName;

		// Token: 0x040055B6 RID: 21942
		private readonly string permError;

		// Token: 0x040055B7 RID: 21943
		private readonly string suspened;

		// Token: 0x040055B8 RID: 21944
		private readonly string autoCompletion;

		// Token: 0x040055B9 RID: 21945
		private readonly string currentState;

		// Token: 0x040055BA RID: 21946
		private readonly string requestedState;
	}
}
