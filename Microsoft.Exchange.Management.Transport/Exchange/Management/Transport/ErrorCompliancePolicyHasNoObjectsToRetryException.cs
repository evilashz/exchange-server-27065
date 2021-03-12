using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000176 RID: 374
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ErrorCompliancePolicyHasNoObjectsToRetryException : LocalizedException
	{
		// Token: 0x06000F49 RID: 3913 RVA: 0x00036144 File Offset: 0x00034344
		public ErrorCompliancePolicyHasNoObjectsToRetryException(string name) : base(Strings.ErrorCompliancePolicyHasNoObjectsToRetry(name))
		{
			this.name = name;
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x00036159 File Offset: 0x00034359
		public ErrorCompliancePolicyHasNoObjectsToRetryException(string name, Exception innerException) : base(Strings.ErrorCompliancePolicyHasNoObjectsToRetry(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0003616F File Offset: 0x0003436F
		protected ErrorCompliancePolicyHasNoObjectsToRetryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x00036199 File Offset: 0x00034399
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x000361B4 File Offset: 0x000343B4
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0400067D RID: 1661
		private readonly string name;
	}
}
