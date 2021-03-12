using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000FCD RID: 4045
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IllegalResumptionException : LocalizedException
	{
		// Token: 0x0600ADD7 RID: 44503 RVA: 0x002924D2 File Offset: 0x002906D2
		public IllegalResumptionException(string oldVerb, string newVerb) : base(Strings.IllegalResumptionException(oldVerb, newVerb))
		{
			this.oldVerb = oldVerb;
			this.newVerb = newVerb;
		}

		// Token: 0x0600ADD8 RID: 44504 RVA: 0x002924EF File Offset: 0x002906EF
		public IllegalResumptionException(string oldVerb, string newVerb, Exception innerException) : base(Strings.IllegalResumptionException(oldVerb, newVerb), innerException)
		{
			this.oldVerb = oldVerb;
			this.newVerb = newVerb;
		}

		// Token: 0x0600ADD9 RID: 44505 RVA: 0x00292510 File Offset: 0x00290710
		protected IllegalResumptionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.oldVerb = (string)info.GetValue("oldVerb", typeof(string));
			this.newVerb = (string)info.GetValue("newVerb", typeof(string));
		}

		// Token: 0x0600ADDA RID: 44506 RVA: 0x00292565 File Offset: 0x00290765
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("oldVerb", this.oldVerb);
			info.AddValue("newVerb", this.newVerb);
		}

		// Token: 0x170037C0 RID: 14272
		// (get) Token: 0x0600ADDB RID: 44507 RVA: 0x00292591 File Offset: 0x00290791
		public string OldVerb
		{
			get
			{
				return this.oldVerb;
			}
		}

		// Token: 0x170037C1 RID: 14273
		// (get) Token: 0x0600ADDC RID: 44508 RVA: 0x00292599 File Offset: 0x00290799
		public string NewVerb
		{
			get
			{
				return this.newVerb;
			}
		}

		// Token: 0x04006126 RID: 24870
		private readonly string oldVerb;

		// Token: 0x04006127 RID: 24871
		private readonly string newVerb;
	}
}
