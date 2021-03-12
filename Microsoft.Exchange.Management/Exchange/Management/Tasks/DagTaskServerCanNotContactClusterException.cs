using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001081 RID: 4225
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskServerCanNotContactClusterException : LocalizedException
	{
		// Token: 0x0600B170 RID: 45424 RVA: 0x00298172 File Offset: 0x00296372
		public DagTaskServerCanNotContactClusterException(int numberOfOtherServers, string otherServers) : base(Strings.DagTaskServerCanNotContactClusterException(numberOfOtherServers, otherServers))
		{
			this.numberOfOtherServers = numberOfOtherServers;
			this.otherServers = otherServers;
		}

		// Token: 0x0600B171 RID: 45425 RVA: 0x0029818F File Offset: 0x0029638F
		public DagTaskServerCanNotContactClusterException(int numberOfOtherServers, string otherServers, Exception innerException) : base(Strings.DagTaskServerCanNotContactClusterException(numberOfOtherServers, otherServers), innerException)
		{
			this.numberOfOtherServers = numberOfOtherServers;
			this.otherServers = otherServers;
		}

		// Token: 0x0600B172 RID: 45426 RVA: 0x002981B0 File Offset: 0x002963B0
		protected DagTaskServerCanNotContactClusterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.numberOfOtherServers = (int)info.GetValue("numberOfOtherServers", typeof(int));
			this.otherServers = (string)info.GetValue("otherServers", typeof(string));
		}

		// Token: 0x0600B173 RID: 45427 RVA: 0x00298205 File Offset: 0x00296405
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("numberOfOtherServers", this.numberOfOtherServers);
			info.AddValue("otherServers", this.otherServers);
		}

		// Token: 0x17003889 RID: 14473
		// (get) Token: 0x0600B174 RID: 45428 RVA: 0x00298231 File Offset: 0x00296431
		public int NumberOfOtherServers
		{
			get
			{
				return this.numberOfOtherServers;
			}
		}

		// Token: 0x1700388A RID: 14474
		// (get) Token: 0x0600B175 RID: 45429 RVA: 0x00298239 File Offset: 0x00296439
		public string OtherServers
		{
			get
			{
				return this.otherServers;
			}
		}

		// Token: 0x040061EF RID: 25071
		private readonly int numberOfOtherServers;

		// Token: 0x040061F0 RID: 25072
		private readonly string otherServers;
	}
}
