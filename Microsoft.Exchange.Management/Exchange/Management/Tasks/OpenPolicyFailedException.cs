using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E0F RID: 3599
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OpenPolicyFailedException : LocalizedException
	{
		// Token: 0x0600A549 RID: 42313 RVA: 0x00285BD9 File Offset: 0x00283DD9
		public OpenPolicyFailedException(uint err, string account, string dom) : base(Strings.OpenPolicyFailedException(err, account, dom))
		{
			this.err = err;
			this.account = account;
			this.dom = dom;
		}

		// Token: 0x0600A54A RID: 42314 RVA: 0x00285BFE File Offset: 0x00283DFE
		public OpenPolicyFailedException(uint err, string account, string dom, Exception innerException) : base(Strings.OpenPolicyFailedException(err, account, dom), innerException)
		{
			this.err = err;
			this.account = account;
			this.dom = dom;
		}

		// Token: 0x0600A54B RID: 42315 RVA: 0x00285C28 File Offset: 0x00283E28
		protected OpenPolicyFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.err = (uint)info.GetValue("err", typeof(uint));
			this.account = (string)info.GetValue("account", typeof(string));
			this.dom = (string)info.GetValue("dom", typeof(string));
		}

		// Token: 0x0600A54C RID: 42316 RVA: 0x00285C9D File Offset: 0x00283E9D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("err", this.err);
			info.AddValue("account", this.account);
			info.AddValue("dom", this.dom);
		}

		// Token: 0x1700362A RID: 13866
		// (get) Token: 0x0600A54D RID: 42317 RVA: 0x00285CDA File Offset: 0x00283EDA
		public uint Err
		{
			get
			{
				return this.err;
			}
		}

		// Token: 0x1700362B RID: 13867
		// (get) Token: 0x0600A54E RID: 42318 RVA: 0x00285CE2 File Offset: 0x00283EE2
		public string Account
		{
			get
			{
				return this.account;
			}
		}

		// Token: 0x1700362C RID: 13868
		// (get) Token: 0x0600A54F RID: 42319 RVA: 0x00285CEA File Offset: 0x00283EEA
		public string Dom
		{
			get
			{
				return this.dom;
			}
		}

		// Token: 0x04005F90 RID: 24464
		private readonly uint err;

		// Token: 0x04005F91 RID: 24465
		private readonly string account;

		// Token: 0x04005F92 RID: 24466
		private readonly string dom;
	}
}
