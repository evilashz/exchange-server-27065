using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001171 RID: 4465
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MultipleOrgMbxesWithCapabilityException : LocalizedException
	{
		// Token: 0x0600B61F RID: 46623 RVA: 0x0029F4DC File Offset: 0x0029D6DC
		public MultipleOrgMbxesWithCapabilityException(string capability) : base(Strings.MultipleOrgMbxesWithCapability(capability))
		{
			this.capability = capability;
		}

		// Token: 0x0600B620 RID: 46624 RVA: 0x0029F4F1 File Offset: 0x0029D6F1
		public MultipleOrgMbxesWithCapabilityException(string capability, Exception innerException) : base(Strings.MultipleOrgMbxesWithCapability(capability), innerException)
		{
			this.capability = capability;
		}

		// Token: 0x0600B621 RID: 46625 RVA: 0x0029F507 File Offset: 0x0029D707
		protected MultipleOrgMbxesWithCapabilityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.capability = (string)info.GetValue("capability", typeof(string));
		}

		// Token: 0x0600B622 RID: 46626 RVA: 0x0029F531 File Offset: 0x0029D731
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("capability", this.capability);
		}

		// Token: 0x17003978 RID: 14712
		// (get) Token: 0x0600B623 RID: 46627 RVA: 0x0029F54C File Offset: 0x0029D74C
		public string Capability
		{
			get
			{
				return this.capability;
			}
		}

		// Token: 0x040062DE RID: 25310
		private readonly string capability;
	}
}
