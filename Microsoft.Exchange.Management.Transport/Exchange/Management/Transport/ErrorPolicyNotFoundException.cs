using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000169 RID: 361
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ErrorPolicyNotFoundException : LocalizedException
	{
		// Token: 0x06000F0A RID: 3850 RVA: 0x00035BB9 File Offset: 0x00033DB9
		public ErrorPolicyNotFoundException(string policyParameter) : base(Strings.ErrorPolicyNotFound(policyParameter))
		{
			this.policyParameter = policyParameter;
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00035BCE File Offset: 0x00033DCE
		public ErrorPolicyNotFoundException(string policyParameter, Exception innerException) : base(Strings.ErrorPolicyNotFound(policyParameter), innerException)
		{
			this.policyParameter = policyParameter;
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00035BE4 File Offset: 0x00033DE4
		protected ErrorPolicyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.policyParameter = (string)info.GetValue("policyParameter", typeof(string));
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00035C0E File Offset: 0x00033E0E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("policyParameter", this.policyParameter);
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x00035C29 File Offset: 0x00033E29
		public string PolicyParameter
		{
			get
			{
				return this.policyParameter;
			}
		}

		// Token: 0x04000672 RID: 1650
		private readonly string policyParameter;
	}
}
