using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200108F RID: 4239
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskCouldNotDisableAccountName : LocalizedException
	{
		// Token: 0x0600B1BF RID: 45503 RVA: 0x00298AF1 File Offset: 0x00296CF1
		public DagTaskCouldNotDisableAccountName(string dagName, Exception ex) : base(Strings.DagTaskCouldNotDisableAccountName(dagName, ex))
		{
			this.dagName = dagName;
			this.ex = ex;
		}

		// Token: 0x0600B1C0 RID: 45504 RVA: 0x00298B0E File Offset: 0x00296D0E
		public DagTaskCouldNotDisableAccountName(string dagName, Exception ex, Exception innerException) : base(Strings.DagTaskCouldNotDisableAccountName(dagName, ex), innerException)
		{
			this.dagName = dagName;
			this.ex = ex;
		}

		// Token: 0x0600B1C1 RID: 45505 RVA: 0x00298B2C File Offset: 0x00296D2C
		protected DagTaskCouldNotDisableAccountName(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dagName = (string)info.GetValue("dagName", typeof(string));
			this.ex = (Exception)info.GetValue("ex", typeof(Exception));
		}

		// Token: 0x0600B1C2 RID: 45506 RVA: 0x00298B81 File Offset: 0x00296D81
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dagName", this.dagName);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x170038A0 RID: 14496
		// (get) Token: 0x0600B1C3 RID: 45507 RVA: 0x00298BAD File Offset: 0x00296DAD
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x170038A1 RID: 14497
		// (get) Token: 0x0600B1C4 RID: 45508 RVA: 0x00298BB5 File Offset: 0x00296DB5
		public Exception Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x04006206 RID: 25094
		private readonly string dagName;

		// Token: 0x04006207 RID: 25095
		private readonly Exception ex;
	}
}
