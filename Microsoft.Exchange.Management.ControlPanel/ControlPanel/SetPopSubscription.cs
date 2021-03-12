using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002D7 RID: 727
	[DataContract]
	public class SetPopSubscription : PopSubscriptionBaseParameter
	{
		// Token: 0x17001DFC RID: 7676
		// (get) Token: 0x06002CB9 RID: 11449 RVA: 0x000899CB File Offset: 0x00087BCB
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-PopSubscription";
			}
		}

		// Token: 0x17001DFD RID: 7677
		// (get) Token: 0x06002CBA RID: 11450 RVA: 0x000899D2 File Offset: 0x00087BD2
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}

		// Token: 0x17001DFE RID: 7678
		// (get) Token: 0x06002CBB RID: 11451 RVA: 0x000899D9 File Offset: 0x00087BD9
		// (set) Token: 0x06002CBC RID: 11452 RVA: 0x000899F4 File Offset: 0x00087BF4
		[DataMember]
		public string ValidateSecret
		{
			get
			{
				return (string)(base["ValidateSecret"] ?? string.Empty);
			}
			set
			{
				base["ValidateSecret"] = value;
			}
		}

		// Token: 0x17001DFF RID: 7679
		// (get) Token: 0x06002CBD RID: 11453 RVA: 0x00089A02 File Offset: 0x00087C02
		// (set) Token: 0x06002CBE RID: 11454 RVA: 0x00089A1E File Offset: 0x00087C1E
		[DataMember]
		public bool ResendVerification
		{
			get
			{
				return (bool)(base["ResendVerification"] ?? false);
			}
			set
			{
				base["ResendVerification"] = value;
			}
		}
	}
}
