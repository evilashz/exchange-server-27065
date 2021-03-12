using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020003EB RID: 1003
	[Serializable]
	internal sealed class ContractException : Exception
	{
		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06003304 RID: 13060 RVA: 0x000C2143 File Offset: 0x000C0343
		public ContractFailureKind Kind
		{
			get
			{
				return this._Kind;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06003305 RID: 13061 RVA: 0x000C214B File Offset: 0x000C034B
		public string Failure
		{
			get
			{
				return this.Message;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06003306 RID: 13062 RVA: 0x000C2153 File Offset: 0x000C0353
		public string UserMessage
		{
			get
			{
				return this._UserMessage;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06003307 RID: 13063 RVA: 0x000C215B File Offset: 0x000C035B
		public string Condition
		{
			get
			{
				return this._Condition;
			}
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x000C2163 File Offset: 0x000C0363
		private ContractException()
		{
			base.HResult = -2146233022;
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x000C2176 File Offset: 0x000C0376
		public ContractException(ContractFailureKind kind, string failure, string userMessage, string condition, Exception innerException) : base(failure, innerException)
		{
			base.HResult = -2146233022;
			this._Kind = kind;
			this._UserMessage = userMessage;
			this._Condition = condition;
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x000C21A2 File Offset: 0x000C03A2
		private ContractException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._Kind = (ContractFailureKind)info.GetInt32("Kind");
			this._UserMessage = info.GetString("UserMessage");
			this._Condition = info.GetString("Condition");
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x000C21E0 File Offset: 0x000C03E0
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Kind", this._Kind);
			info.AddValue("UserMessage", this._UserMessage);
			info.AddValue("Condition", this._Condition);
		}

		// Token: 0x04001663 RID: 5731
		private readonly ContractFailureKind _Kind;

		// Token: 0x04001664 RID: 5732
		private readonly string _UserMessage;

		// Token: 0x04001665 RID: 5733
		private readonly string _Condition;
	}
}
