using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02001105 RID: 4357
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NSPIOperationException : LocalizedException
	{
		// Token: 0x0600B403 RID: 46083 RVA: 0x0029C108 File Offset: 0x0029A308
		public NSPIOperationException(string operation, int returnValue, string serverId, string userName) : base(Strings.messageNSPIOperationException(operation, returnValue, serverId, userName))
		{
			this.operation = operation;
			this.returnValue = returnValue;
			this.serverId = serverId;
			this.userName = userName;
		}

		// Token: 0x0600B404 RID: 46084 RVA: 0x0029C137 File Offset: 0x0029A337
		public NSPIOperationException(string operation, int returnValue, string serverId, string userName, Exception innerException) : base(Strings.messageNSPIOperationException(operation, returnValue, serverId, userName), innerException)
		{
			this.operation = operation;
			this.returnValue = returnValue;
			this.serverId = serverId;
			this.userName = userName;
		}

		// Token: 0x0600B405 RID: 46085 RVA: 0x0029C168 File Offset: 0x0029A368
		protected NSPIOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.operation = (string)info.GetValue("operation", typeof(string));
			this.returnValue = (int)info.GetValue("returnValue", typeof(int));
			this.serverId = (string)info.GetValue("serverId", typeof(string));
			this.userName = (string)info.GetValue("userName", typeof(string));
		}

		// Token: 0x0600B406 RID: 46086 RVA: 0x0029C200 File Offset: 0x0029A400
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("operation", this.operation);
			info.AddValue("returnValue", this.returnValue);
			info.AddValue("serverId", this.serverId);
			info.AddValue("userName", this.userName);
		}

		// Token: 0x1700390C RID: 14604
		// (get) Token: 0x0600B407 RID: 46087 RVA: 0x0029C259 File Offset: 0x0029A459
		public string Operation
		{
			get
			{
				return this.operation;
			}
		}

		// Token: 0x1700390D RID: 14605
		// (get) Token: 0x0600B408 RID: 46088 RVA: 0x0029C261 File Offset: 0x0029A461
		public int ReturnValue
		{
			get
			{
				return this.returnValue;
			}
		}

		// Token: 0x1700390E RID: 14606
		// (get) Token: 0x0600B409 RID: 46089 RVA: 0x0029C269 File Offset: 0x0029A469
		public string ServerId
		{
			get
			{
				return this.serverId;
			}
		}

		// Token: 0x1700390F RID: 14607
		// (get) Token: 0x0600B40A RID: 46090 RVA: 0x0029C271 File Offset: 0x0029A471
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x04006272 RID: 25202
		private readonly string operation;

		// Token: 0x04006273 RID: 25203
		private readonly int returnValue;

		// Token: 0x04006274 RID: 25204
		private readonly string serverId;

		// Token: 0x04006275 RID: 25205
		private readonly string userName;
	}
}
