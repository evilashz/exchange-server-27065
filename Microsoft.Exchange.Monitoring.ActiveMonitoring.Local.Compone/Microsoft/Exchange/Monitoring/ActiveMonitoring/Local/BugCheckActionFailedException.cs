using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x020005AA RID: 1450
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class BugCheckActionFailedException : RecoveryActionExceptionCommon
	{
		// Token: 0x060026EC RID: 9964 RVA: 0x000DE2F4 File Offset: 0x000DC4F4
		public BugCheckActionFailedException(string errMsg) : base(Strings.BugCheckActionFailed(errMsg))
		{
			this.errMsg = errMsg;
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x000DE30E File Offset: 0x000DC50E
		public BugCheckActionFailedException(string errMsg, Exception innerException) : base(Strings.BugCheckActionFailed(errMsg), innerException)
		{
			this.errMsg = errMsg;
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x000DE329 File Offset: 0x000DC529
		protected BugCheckActionFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x000DE353 File Offset: 0x000DC553
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x060026F0 RID: 9968 RVA: 0x000DE36E File Offset: 0x000DC56E
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x04001C80 RID: 7296
		private readonly string errMsg;
	}
}
