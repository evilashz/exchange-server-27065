using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011D7 RID: 4567
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TopologyDiscoveryProblem : LocalizedException
	{
		// Token: 0x0600B910 RID: 47376 RVA: 0x002A55C8 File Offset: 0x002A37C8
		public TopologyDiscoveryProblem(string s) : base(Strings.TopologyDiscoveryProblem(s))
		{
			this.s = s;
		}

		// Token: 0x0600B911 RID: 47377 RVA: 0x002A55DD File Offset: 0x002A37DD
		public TopologyDiscoveryProblem(string s, Exception innerException) : base(Strings.TopologyDiscoveryProblem(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x0600B912 RID: 47378 RVA: 0x002A55F3 File Offset: 0x002A37F3
		protected TopologyDiscoveryProblem(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x0600B913 RID: 47379 RVA: 0x002A561D File Offset: 0x002A381D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x17003A31 RID: 14897
		// (get) Token: 0x0600B914 RID: 47380 RVA: 0x002A5638 File Offset: 0x002A3838
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x0400644C RID: 25676
		private readonly string s;
	}
}
