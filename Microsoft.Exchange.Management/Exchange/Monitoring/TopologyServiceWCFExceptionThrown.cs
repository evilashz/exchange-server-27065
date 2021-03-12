using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F29 RID: 3881
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TopologyServiceWCFExceptionThrown : LocalizedException
	{
		// Token: 0x0600AAC2 RID: 43714 RVA: 0x0028DF8D File Offset: 0x0028C18D
		public TopologyServiceWCFExceptionThrown(string e) : base(Strings.messageTopologyServiceWCFExceptionThrown(e))
		{
			this.e = e;
		}

		// Token: 0x0600AAC3 RID: 43715 RVA: 0x0028DFA2 File Offset: 0x0028C1A2
		public TopologyServiceWCFExceptionThrown(string e, Exception innerException) : base(Strings.messageTopologyServiceWCFExceptionThrown(e), innerException)
		{
			this.e = e;
		}

		// Token: 0x0600AAC4 RID: 43716 RVA: 0x0028DFB8 File Offset: 0x0028C1B8
		protected TopologyServiceWCFExceptionThrown(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.e = (string)info.GetValue("e", typeof(string));
		}

		// Token: 0x0600AAC5 RID: 43717 RVA: 0x0028DFE2 File Offset: 0x0028C1E2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("e", this.e);
		}

		// Token: 0x1700373B RID: 14139
		// (get) Token: 0x0600AAC6 RID: 43718 RVA: 0x0028DFFD File Offset: 0x0028C1FD
		public string E
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x040060A1 RID: 24737
		private readonly string e;
	}
}
