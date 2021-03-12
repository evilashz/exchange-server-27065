using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000064 RID: 100
	internal sealed class ApplyOME : SetHeaderUniqueValue
	{
		// Token: 0x0600035B RID: 859 RVA: 0x000130FA File Offset: 0x000112FA
		public ApplyOME(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00013103 File Offset: 0x00011303
		public override Version MinimumVersion
		{
			get
			{
				return ApplyOME.CurrentVersion;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0001310A File Offset: 0x0001130A
		public override string Name
		{
			get
			{
				return "ApplyOME";
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00013111 File Offset: 0x00011311
		public override Type[] ArgumentsType
		{
			get
			{
				return ApplyOME.ArgumentTypes;
			}
		}

		// Token: 0x04000215 RID: 533
		private static readonly Version CurrentVersion = new Version("15.00.0007.00");

		// Token: 0x04000216 RID: 534
		private static readonly Type[] ArgumentTypes = new Type[]
		{
			typeof(string),
			typeof(string)
		};
	}
}
