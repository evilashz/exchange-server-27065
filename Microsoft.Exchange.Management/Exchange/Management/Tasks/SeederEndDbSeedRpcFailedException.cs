using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200105F RID: 4191
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeederEndDbSeedRpcFailedException : LocalizedException
	{
		// Token: 0x0600B0AC RID: 45228 RVA: 0x00296925 File Offset: 0x00294B25
		public SeederEndDbSeedRpcFailedException(string dbName, string targetMachine, string errMessage) : base(Strings.SeederEndDbSeedRpcFailedException(dbName, targetMachine, errMessage))
		{
			this.dbName = dbName;
			this.targetMachine = targetMachine;
			this.errMessage = errMessage;
		}

		// Token: 0x0600B0AD RID: 45229 RVA: 0x0029694A File Offset: 0x00294B4A
		public SeederEndDbSeedRpcFailedException(string dbName, string targetMachine, string errMessage, Exception innerException) : base(Strings.SeederEndDbSeedRpcFailedException(dbName, targetMachine, errMessage), innerException)
		{
			this.dbName = dbName;
			this.targetMachine = targetMachine;
			this.errMessage = errMessage;
		}

		// Token: 0x0600B0AE RID: 45230 RVA: 0x00296974 File Offset: 0x00294B74
		protected SeederEndDbSeedRpcFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.targetMachine = (string)info.GetValue("targetMachine", typeof(string));
			this.errMessage = (string)info.GetValue("errMessage", typeof(string));
		}

		// Token: 0x0600B0AF RID: 45231 RVA: 0x002969E9 File Offset: 0x00294BE9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("targetMachine", this.targetMachine);
			info.AddValue("errMessage", this.errMessage);
		}

		// Token: 0x1700384D RID: 14413
		// (get) Token: 0x0600B0B0 RID: 45232 RVA: 0x00296A26 File Offset: 0x00294C26
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x1700384E RID: 14414
		// (get) Token: 0x0600B0B1 RID: 45233 RVA: 0x00296A2E File Offset: 0x00294C2E
		public string TargetMachine
		{
			get
			{
				return this.targetMachine;
			}
		}

		// Token: 0x1700384F RID: 14415
		// (get) Token: 0x0600B0B2 RID: 45234 RVA: 0x00296A36 File Offset: 0x00294C36
		public string ErrMessage
		{
			get
			{
				return this.errMessage;
			}
		}

		// Token: 0x040061B3 RID: 25011
		private readonly string dbName;

		// Token: 0x040061B4 RID: 25012
		private readonly string targetMachine;

		// Token: 0x040061B5 RID: 25013
		private readonly string errMessage;
	}
}
