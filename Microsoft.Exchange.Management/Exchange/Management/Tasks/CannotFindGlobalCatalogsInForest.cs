using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E20 RID: 3616
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotFindGlobalCatalogsInForest : LocalizedException
	{
		// Token: 0x0600A5A6 RID: 42406 RVA: 0x00286682 File Offset: 0x00284882
		public CannotFindGlobalCatalogsInForest(string forestFqdn) : base(Strings.CannotFindGlobalCatalogsInForest(forestFqdn))
		{
			this.forestFqdn = forestFqdn;
		}

		// Token: 0x0600A5A7 RID: 42407 RVA: 0x00286697 File Offset: 0x00284897
		public CannotFindGlobalCatalogsInForest(string forestFqdn, Exception innerException) : base(Strings.CannotFindGlobalCatalogsInForest(forestFqdn), innerException)
		{
			this.forestFqdn = forestFqdn;
		}

		// Token: 0x0600A5A8 RID: 42408 RVA: 0x002866AD File Offset: 0x002848AD
		protected CannotFindGlobalCatalogsInForest(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.forestFqdn = (string)info.GetValue("forestFqdn", typeof(string));
		}

		// Token: 0x0600A5A9 RID: 42409 RVA: 0x002866D7 File Offset: 0x002848D7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("forestFqdn", this.forestFqdn);
		}

		// Token: 0x17003643 RID: 13891
		// (get) Token: 0x0600A5AA RID: 42410 RVA: 0x002866F2 File Offset: 0x002848F2
		public string ForestFqdn
		{
			get
			{
				return this.forestFqdn;
			}
		}

		// Token: 0x04005FA9 RID: 24489
		private readonly string forestFqdn;
	}
}
