using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001060 RID: 4192
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeederCancelDbSeedRpcFailedException : LocalizedException
	{
		// Token: 0x0600B0B3 RID: 45235 RVA: 0x00296A3E File Offset: 0x00294C3E
		public SeederCancelDbSeedRpcFailedException(string dbName, string targetMachine, string errMessage) : base(Strings.SeederCancelDbSeedRpcFailedException(dbName, targetMachine, errMessage))
		{
			this.dbName = dbName;
			this.targetMachine = targetMachine;
			this.errMessage = errMessage;
		}

		// Token: 0x0600B0B4 RID: 45236 RVA: 0x00296A63 File Offset: 0x00294C63
		public SeederCancelDbSeedRpcFailedException(string dbName, string targetMachine, string errMessage, Exception innerException) : base(Strings.SeederCancelDbSeedRpcFailedException(dbName, targetMachine, errMessage), innerException)
		{
			this.dbName = dbName;
			this.targetMachine = targetMachine;
			this.errMessage = errMessage;
		}

		// Token: 0x0600B0B5 RID: 45237 RVA: 0x00296A8C File Offset: 0x00294C8C
		protected SeederCancelDbSeedRpcFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.targetMachine = (string)info.GetValue("targetMachine", typeof(string));
			this.errMessage = (string)info.GetValue("errMessage", typeof(string));
		}

		// Token: 0x0600B0B6 RID: 45238 RVA: 0x00296B01 File Offset: 0x00294D01
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("targetMachine", this.targetMachine);
			info.AddValue("errMessage", this.errMessage);
		}

		// Token: 0x17003850 RID: 14416
		// (get) Token: 0x0600B0B7 RID: 45239 RVA: 0x00296B3E File Offset: 0x00294D3E
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17003851 RID: 14417
		// (get) Token: 0x0600B0B8 RID: 45240 RVA: 0x00296B46 File Offset: 0x00294D46
		public string TargetMachine
		{
			get
			{
				return this.targetMachine;
			}
		}

		// Token: 0x17003852 RID: 14418
		// (get) Token: 0x0600B0B9 RID: 45241 RVA: 0x00296B4E File Offset: 0x00294D4E
		public string ErrMessage
		{
			get
			{
				return this.errMessage;
			}
		}

		// Token: 0x040061B6 RID: 25014
		private readonly string dbName;

		// Token: 0x040061B7 RID: 25015
		private readonly string targetMachine;

		// Token: 0x040061B8 RID: 25016
		private readonly string errMessage;
	}
}
