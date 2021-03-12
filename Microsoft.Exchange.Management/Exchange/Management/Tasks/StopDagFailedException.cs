using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001094 RID: 4244
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class StopDagFailedException : LocalizedException
	{
		// Token: 0x0600B1E0 RID: 45536 RVA: 0x00298FD1 File Offset: 0x002971D1
		public StopDagFailedException(string errorServers, string dagName) : base(Strings.StopDagFailedException(errorServers, dagName))
		{
			this.errorServers = errorServers;
			this.dagName = dagName;
		}

		// Token: 0x0600B1E1 RID: 45537 RVA: 0x00298FEE File Offset: 0x002971EE
		public StopDagFailedException(string errorServers, string dagName, Exception innerException) : base(Strings.StopDagFailedException(errorServers, dagName), innerException)
		{
			this.errorServers = errorServers;
			this.dagName = dagName;
		}

		// Token: 0x0600B1E2 RID: 45538 RVA: 0x0029900C File Offset: 0x0029720C
		protected StopDagFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorServers = (string)info.GetValue("errorServers", typeof(string));
			this.dagName = (string)info.GetValue("dagName", typeof(string));
		}

		// Token: 0x0600B1E3 RID: 45539 RVA: 0x00299061 File Offset: 0x00297261
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorServers", this.errorServers);
			info.AddValue("dagName", this.dagName);
		}

		// Token: 0x170038AD RID: 14509
		// (get) Token: 0x0600B1E4 RID: 45540 RVA: 0x0029908D File Offset: 0x0029728D
		public string ErrorServers
		{
			get
			{
				return this.errorServers;
			}
		}

		// Token: 0x170038AE RID: 14510
		// (get) Token: 0x0600B1E5 RID: 45541 RVA: 0x00299095 File Offset: 0x00297295
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x04006213 RID: 25107
		private readonly string errorServers;

		// Token: 0x04006214 RID: 25108
		private readonly string dagName;
	}
}
