using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E25 RID: 3621
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RmsSharedIdentityInconsistentStateException : LocalizedException
	{
		// Token: 0x0600A5BF RID: 42431 RVA: 0x002868E8 File Offset: 0x00284AE8
		public RmsSharedIdentityInconsistentStateException(LocalizedString details) : base(Strings.RmsSharedIdentityInconsistentState(details))
		{
			this.details = details;
		}

		// Token: 0x0600A5C0 RID: 42432 RVA: 0x002868FD File Offset: 0x00284AFD
		public RmsSharedIdentityInconsistentStateException(LocalizedString details, Exception innerException) : base(Strings.RmsSharedIdentityInconsistentState(details), innerException)
		{
			this.details = details;
		}

		// Token: 0x0600A5C1 RID: 42433 RVA: 0x00286913 File Offset: 0x00284B13
		protected RmsSharedIdentityInconsistentStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.details = (LocalizedString)info.GetValue("details", typeof(LocalizedString));
		}

		// Token: 0x0600A5C2 RID: 42434 RVA: 0x0028693D File Offset: 0x00284B3D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("details", this.details);
		}

		// Token: 0x17003648 RID: 13896
		// (get) Token: 0x0600A5C3 RID: 42435 RVA: 0x0028695D File Offset: 0x00284B5D
		public LocalizedString Details
		{
			get
			{
				return this.details;
			}
		}

		// Token: 0x04005FAE RID: 24494
		private readonly LocalizedString details;
	}
}
