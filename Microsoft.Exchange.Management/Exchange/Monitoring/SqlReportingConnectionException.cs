using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F08 RID: 3848
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SqlReportingConnectionException : LocalizedException
	{
		// Token: 0x0600AA11 RID: 43537 RVA: 0x0028CBDA File Offset: 0x0028ADDA
		public SqlReportingConnectionException(string errorMsg) : base(Strings.CheckReportingServerDatabaseParameters(errorMsg))
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x0600AA12 RID: 43538 RVA: 0x0028CBEF File Offset: 0x0028ADEF
		public SqlReportingConnectionException(string errorMsg, Exception innerException) : base(Strings.CheckReportingServerDatabaseParameters(errorMsg), innerException)
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x0600AA13 RID: 43539 RVA: 0x0028CC05 File Offset: 0x0028AE05
		protected SqlReportingConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x0600AA14 RID: 43540 RVA: 0x0028CC2F File Offset: 0x0028AE2F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x1700370E RID: 14094
		// (get) Token: 0x0600AA15 RID: 43541 RVA: 0x0028CC4A File Offset: 0x0028AE4A
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x04006074 RID: 24692
		private readonly string errorMsg;
	}
}
