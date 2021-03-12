using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E14 RID: 3604
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SecurityDescriptorAccessDeniedException : LocalizedException
	{
		// Token: 0x0600A567 RID: 42343 RVA: 0x00285FD9 File Offset: 0x002841D9
		public SecurityDescriptorAccessDeniedException(string dn) : base(Strings.SecurityDescriptorAccessDeniedException(dn))
		{
			this.dn = dn;
		}

		// Token: 0x0600A568 RID: 42344 RVA: 0x00285FEE File Offset: 0x002841EE
		public SecurityDescriptorAccessDeniedException(string dn, Exception innerException) : base(Strings.SecurityDescriptorAccessDeniedException(dn), innerException)
		{
			this.dn = dn;
		}

		// Token: 0x0600A569 RID: 42345 RVA: 0x00286004 File Offset: 0x00284204
		protected SecurityDescriptorAccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dn = (string)info.GetValue("dn", typeof(string));
		}

		// Token: 0x0600A56A RID: 42346 RVA: 0x0028602E File Offset: 0x0028422E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dn", this.dn);
		}

		// Token: 0x17003634 RID: 13876
		// (get) Token: 0x0600A56B RID: 42347 RVA: 0x00286049 File Offset: 0x00284249
		public string Dn
		{
			get
			{
				return this.dn;
			}
		}

		// Token: 0x04005F9A RID: 24474
		private readonly string dn;
	}
}
