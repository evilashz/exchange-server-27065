using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000155 RID: 341
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CompliancePolicyAlreadyExistsException : LocalizedException
	{
		// Token: 0x06000EAD RID: 3757 RVA: 0x00035464 File Offset: 0x00033664
		public CompliancePolicyAlreadyExistsException(string policyName) : base(Strings.CompliancePolicyAlreadyExists(policyName))
		{
			this.policyName = policyName;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00035479 File Offset: 0x00033679
		public CompliancePolicyAlreadyExistsException(string policyName, Exception innerException) : base(Strings.CompliancePolicyAlreadyExists(policyName), innerException)
		{
			this.policyName = policyName;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0003548F File Offset: 0x0003368F
		protected CompliancePolicyAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.policyName = (string)info.GetValue("policyName", typeof(string));
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x000354B9 File Offset: 0x000336B9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("policyName", this.policyName);
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x000354D4 File Offset: 0x000336D4
		public string PolicyName
		{
			get
			{
				return this.policyName;
			}
		}

		// Token: 0x04000665 RID: 1637
		private readonly string policyName;
	}
}
