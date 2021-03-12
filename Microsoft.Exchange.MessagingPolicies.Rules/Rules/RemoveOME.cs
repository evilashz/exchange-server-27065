using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000061 RID: 97
	internal sealed class RemoveOME : SetHeaderUniqueValue
	{
		// Token: 0x0600034C RID: 844 RVA: 0x00012E94 File Offset: 0x00011094
		public RemoveOME(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00012E9D File Offset: 0x0001109D
		public override Version MinimumVersion
		{
			get
			{
				return RemoveOME.CurrentVersion;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00012EA4 File Offset: 0x000110A4
		public override string Name
		{
			get
			{
				return "RemoveOME";
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00012EAB File Offset: 0x000110AB
		public override Type[] ArgumentsType
		{
			get
			{
				return RemoveOME.ArgumentTypes;
			}
		}

		// Token: 0x04000212 RID: 530
		private static readonly Version CurrentVersion = new Version("15.00.0007.00");

		// Token: 0x04000213 RID: 531
		private static readonly Type[] ArgumentTypes = new Type[]
		{
			typeof(string),
			typeof(string)
		};
	}
}
