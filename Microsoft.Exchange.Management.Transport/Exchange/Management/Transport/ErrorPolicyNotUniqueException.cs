using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200016A RID: 362
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ErrorPolicyNotUniqueException : LocalizedException
	{
		// Token: 0x06000F0F RID: 3855 RVA: 0x00035C31 File Offset: 0x00033E31
		public ErrorPolicyNotUniqueException(string policyParameter) : base(Strings.ErrorPolicyNotUnique(policyParameter))
		{
			this.policyParameter = policyParameter;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x00035C46 File Offset: 0x00033E46
		public ErrorPolicyNotUniqueException(string policyParameter, Exception innerException) : base(Strings.ErrorPolicyNotUnique(policyParameter), innerException)
		{
			this.policyParameter = policyParameter;
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00035C5C File Offset: 0x00033E5C
		protected ErrorPolicyNotUniqueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.policyParameter = (string)info.GetValue("policyParameter", typeof(string));
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x00035C86 File Offset: 0x00033E86
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("policyParameter", this.policyParameter);
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x00035CA1 File Offset: 0x00033EA1
		public string PolicyParameter
		{
			get
			{
				return this.policyParameter;
			}
		}

		// Token: 0x04000673 RID: 1651
		private readonly string policyParameter;
	}
}
