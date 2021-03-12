using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000525 RID: 1317
	[DataContract]
	public class ManagementScopeFilter : WebServiceParameters
	{
		// Token: 0x06003EDE RID: 16094 RVA: 0x000BD538 File Offset: 0x000BB738
		public ManagementScopeFilter()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x17002495 RID: 9365
		// (get) Token: 0x06003EDF RID: 16095 RVA: 0x000BD55A File Offset: 0x000BB75A
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-ManagementScope";
			}
		}

		// Token: 0x17002496 RID: 9366
		// (get) Token: 0x06003EE0 RID: 16096 RVA: 0x000BD561 File Offset: 0x000BB761
		// (set) Token: 0x06003EE1 RID: 16097 RVA: 0x000BD573 File Offset: 0x000BB773
		[DataMember]
		public bool Exclusive
		{
			get
			{
				return (bool)base["Exclusive"];
			}
			set
			{
				base["Exclusive"] = value;
			}
		}

		// Token: 0x17002497 RID: 9367
		// (get) Token: 0x06003EE2 RID: 16098 RVA: 0x000BD586 File Offset: 0x000BB786
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}

		// Token: 0x06003EE3 RID: 16099 RVA: 0x000BD58D File Offset: 0x000BB78D
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			base["Exclusive"] = false;
		}

		// Token: 0x040028B0 RID: 10416
		internal const string GetCmdlet = "Get-ManagementScope";
	}
}
